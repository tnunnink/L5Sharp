using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;
using L5Sharp.Gateway.Internal;
using L5Sharp.Gateway.Services;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Gateway;

/// <inheritdoc cref="IPlcClient" />
public class PlcClient : IPlcClient
{
    /// <summary>
    /// Specifies the timeout duration, in milliseconds, for asynchronous operations initiated by the <see cref="PlcClient"/>.
    /// When libplctag functions are executed with a 0ms timeout, they will return immediately, and we will use the tag
    /// event callback to update the result of the operation. 
    /// </summary>
    private const int AsyncTimeout = 0;

    /// <summary>
    /// Indicates whether the current instance of <see cref="PlcClient"/> has been disposed.
    /// This variable is used to track whether resources associated with the object have been released
    /// to prevent further usage and manage object lifecycle effectively.
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Represents the configuration options used by the <see cref="PlcClient"/> for interacting with a PLC.
    /// This variable stores an instance of <see cref="PlcOptions"/> to define settings such as timeouts
    /// and communication intervals for PLC operations.
    /// </summary>
    private readonly PlcOptions _options;

    /// <summary>
    /// Provides access to tag-related operations for the <see cref="PlcClient"/> class.
    /// This variable serves as a dependency for managing tag interactions, enabling creation, reading,
    /// writing, and other operations crucial for interacting with PLC tags.
    /// </summary>
    private readonly ITagService _tagService;

    /// <summary>
    /// Represents a private instance of <see cref="TagBuffer"/> used internally for managing tag-related operations.
    /// This buffer facilitates the reading and writing of tag values by interfacing with an underlying <see cref="ITagService"/>.
    /// </summary>
    private readonly TagBuffer _tagBuffer;

    /// <summary>
    /// The base URI for establishing a connection to the PLC client.
    /// This URI contains information such as the protocol, gateway (IP address),
    /// path (slot number), and the type of PLC being connected to.
    /// </summary>
    private readonly string _uri;

    /// <summary>
    /// Maintains a mapping between tag names and their associated handles
    /// for interacting with a PLC. This dictionary is used to efficiently
    /// manage and reuse handles during read and write operations, avoiding
    /// the overhead of repeatedly creating new handles for the same tag.
    /// </summary>
    private readonly ConcurrentDictionary<TagName, TagHandle> _handles = [];

    /// <summary>
    /// Represents a collection of pending asynchronous operations mapped to tag names.
    /// This dictionary associates a <see cref="TagName"/> with a <see cref="TaskCompletionSource{TResult}"/>
    /// that facilitates the management and synchronization of PLC operations.
    /// </summary>
    private readonly ConcurrentDictionary<OperationKey, TaskCompletionSource<int>> _operations = [];

    /// <summary>
    /// Maintains a thread-safe collection of active <see cref="TagWatch"/> instances associated with their respective handles.
    /// This is used to manage and track tag watch operations within the <see cref="PlcClient"/>.
    /// </summary>
    private readonly ConcurrentDictionary<TagName, TagWatch> _watches = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="PlcClient"/> class with the specified configuration options.
    /// </summary>
    /// <param name="options">
    /// The configuration options that define connection parameters such as IP address, slot number,
    /// timeout settings, and poll rate for PLC operations.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="options"/> parameter is null.</exception>
    public PlcClient(PlcOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _tagService = new NativeTagService(onEvent: TagEventCallback);
        _tagBuffer = new TagBuffer(_tagService);
        _uri = $"protocol=ab_eip&gateway={_options.IP}&path=1,{_options.Slot}&plc=controllogix";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlcClient"/> class with the specified configuration options
    /// and a custom tag service implementation for dependency injection or testing purposes.
    /// </summary>
    /// <param name="options">
    /// The configuration options that define connection parameters such as IP address, slot number,
    /// timeout settings, and read poll rate for PLC operations.
    /// </param>
    /// <param name="tagService">
    /// The tag service implementation to use for tag operations, allowing for custom or mock implementations.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="options"/> or <paramref name="tagService"/> parameter is null.
    /// </exception>
    public PlcClient(PlcOptions options, ITagService tagService)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
        _tagBuffer = new TagBuffer(_tagService);
        _uri = $"protocol=ab_eip&gateway={_options.IP}&path=1,{_options.Slot}&plc=controllogix";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlcClient"/> class with the specified IP address and optional slot number.
    /// This is a convenience constructor that creates a default <see cref="PlcOptions"/> configuration internally.
    /// </summary>
    /// <param name="ip">
    /// The IP address of the PLC to connect to. This parameter is required and cannot be null.
    /// </param>
    /// <param name="slot">
    /// The slot number of the PLC in the chassis. Defaults to 0 if not specified.
    /// For CompactLogix controllers, this is typically 0. For ControlLogix controllers in a chassis,
    /// specify the slot number where the controller is located.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="ip"/> parameter is null.
    /// </exception>
    public PlcClient(string ip, ushort slot = 0) : this(new PlcOptions { IP = ip, Slot = slot })
    {
    }

    /// <inheritdoc />
    public Task<bool> Ping(CancellationToken token = default)
    {
        return Task.Run(async () =>
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(_options.IP).ConfigureAwait(false);
            return reply.Status == IPStatus.Success;
        }, token);
    }

    /// <inheritdoc />
    public async Task<TagResult> ReadTag<TData>(TagName tagName, CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        ThrowIfDisposed();

        var tag = Tag.New<TData>(tagName);
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var readErrors = await ReadAllTags(members, token);
        stopwatch.Stop();

        var tagErrors = readErrors.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResult> ReadTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var readErrors = await ReadAllTags(members, token);
        stopwatch.Stop();

        var tagErrors = readErrors.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResults> ReadTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        var tagCollection = tags.ToArray();
        var members = tagCollection.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var readErrors = await ReadAllTags(members, token);
        stopwatch.Stop();

        // Regroup all the tags and corresponding errors into a tag result to aggregate.
        var results = tagCollection.Select(t =>
        {
            var tagErrors = readErrors.TryGetValue(t.TagName, out var value) ? value : [];
            return TagResult.Create(t, tagErrors, stopwatch.Elapsed);
        });

        return TagResults.Aggregate(results.ToArray());
    }

    /// <inheritdoc />
    public async Task<TagResult> WriteTag<TData>(TagName tagName, TData data, CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (data is null) throw new ArgumentNullException(nameof(data));
        ThrowIfDisposed();

        var tag = Tag.Named(tagName).WithValue(data).Build();
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var writeErrors = await WriteAllTags(members, token);
        stopwatch.Stop();

        var tagErrors = writeErrors.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResult> WriteTag<TData>(TagName tagName, Action<TData> update,
        CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (update is null) throw new ArgumentNullException(nameof(update));
        ThrowIfDisposed();

        var tag = Tag.New<TData>(tagName);
        update(tag.Value.As<TData>());
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var writeErrors = await WriteAllTags(members, token);
        stopwatch.Stop();

        var tagErrors = writeErrors.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResult> WriteTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var writeErrors = await WriteAllTags(members, token);
        stopwatch.Stop();

        var tagErrors = writeErrors.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResults> WriteTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        var tagCollection = tags.ToArray();
        var members = tagCollection.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var writeErrors = await WriteAllTags(members, token);
        stopwatch.Stop();

        var results = tagCollection.Select(t =>
        {
            var tagErrors = writeErrors.TryGetValue(t.TagName, out var e) ? e : [];
            return TagResult.Create(t, tagErrors, stopwatch.Elapsed);
        });

        return TagResults.Aggregate(results.ToArray());
    }

    /// <inheritdoc />
    public Task<TagResult> UpdateTag<TData>(TagName tagName, IReadOnlyList<(TagName Member, AtomicData Value)> updates,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (updates is null) throw new ArgumentNullException(nameof(updates));

        var tag = Tag.New<TData>(tagName);
        return UpdateTag(tag, updates, token);
    }

    /// <inheritdoc />
    public async Task<TagResult> UpdateTag(Tag tag, IReadOnlyList<(TagName Member, AtomicData Value)> updates,
        CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        if (updates is null) throw new ArgumentNullException(nameof(updates));
        ThrowIfDisposed();
        ThrowIfNotBaseTag(tag.TagName);

        var members = GetUpdatedMembersFor(tag, updates);
        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var writeErrors = await WriteAllTags(members, token);
        stopwatch.Stop();

        var tagErrors = writeErrors.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public Task<TagResult> UpdateTag<TData>(TagName tagName, Action<TData> update,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (update is null) throw new ArgumentNullException(nameof(update));

        var tag = Tag.New<TData>(tagName);
        return UpdateTag(tag, update, token);
    }

    /// <inheritdoc />
    public async Task<TagResult> UpdateTag<TData>(Tag tag, Action<TData> update, CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        if (update is null) throw new ArgumentNullException(nameof(update));
        ThrowIfDisposed();
        ThrowIfNotBaseTag(tag.TagName);

        var members = GetUpdatedMembersFor(tag, update);
        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var writeErrors = await WriteAllTags(members, token);
        stopwatch.Stop();

        var tagErrors = writeErrors.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public Task<TagMonitor> MonitorTag<TData>(TagName tagName, CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        ThrowIfDisposed();

        var tag = Tag.New<TData>(tagName);
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();
        ThrowIfInvalidMemberSet(members);

        return CreateMonitor(members, token);
    }

    /// <inheritdoc />
    public Task<TagMonitor> MonitorTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();
        ThrowIfInvalidMemberSet(members);
        return CreateMonitor(members, token);
    }

    /// <inheritdoc />
    public Task<TagMonitor> MonitorTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        var members = tags.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToArray();
        ThrowIfInvalidMemberSet(members);
        return CreateMonitor(members, token);
    }

    /// <inheritdoc />
    public Task<TagResult> PollTag<TData>(TagName tagName, TimeSpan duration, CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        ThrowIfDisposed();

        var tag = Tag.New<TData>(tagName);

        return PollForDuration(tag, duration, token);
    }

    /// <inheritdoc />
    public Task<TagResult> PollTag(Tag tag, TimeSpan duration, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        return PollForDuration(tag, duration, token);
    }

    /// <inheritdoc />
    public async Task<TagResult> PollTag<TData>(TagName tagName, Func<TData, bool> predicate,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        ThrowIfDisposed();

        var tag = Tag.New<TData>(tagName);
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();
        ThrowIfInvalidMemberSet(members);

        // Kick off a tag monitor to stream updates into the tag instance.
        using var monitor = await CreateMonitor(members, token).ConfigureAwait(false);

        return await PollUntilCondition(tag, monitor, predicate, token);
    }

    /// <inheritdoc />
    public async Task<TagResult> PollTag<TData>(Tag tag, Func<TData, bool> predicate,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();
        ThrowIfInvalidMemberSet(members);

        // Kick off a tag monitor to stream updates into the tag instance.
        using var monitor = await CreateMonitor(members, token).ConfigureAwait(false);

        return await PollUntilCondition(tag, monitor, predicate, token);
    }

    /// <inheritdoc />
    public async Task<TagResult> PollTag<TData>(TagName tagName, Func<TData, bool> predicate, TimeSpan duration,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        ThrowIfDisposed();

        var tag = Tag.New<TData>(tagName);
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();
        ThrowIfInvalidMemberSet(members);

        // Kick off a tag monitor to stream updates into the tag instance.
        using var monitor = await CreateMonitor(members, token).ConfigureAwait(false);

        // Create two separate tasks and await the first to respond.
        var waiter = Task.Delay(duration, token);
        var poller = PollUntilCondition(tag, monitor, predicate, token);
        var task = await Task.WhenAny(poller, waiter).ConfigureAwait(false);

        // If the predicate hit, return the result of the task.
        if (task is Task<TagResult> resultTask)
        {
            return await resultTask;
        }

        // Otherwise the delay expired - return the current result of the monitor and dispose.
        return monitor.GetResult(tag.TagName);
    }

    /// <inheritdoc />
    public async Task<TagResult> PollTag<TData>(Tag tag, Func<TData, bool> predicate, TimeSpan duration,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();
        ThrowIfInvalidMemberSet(members);

        // Kick off a tag monitor to stream updates into the tag instance.
        using var monitor = await CreateMonitor(members, token).ConfigureAwait(false);

        // Create two separate tasks and await the first to respond.
        var waiter = Task.Delay(duration, token);
        var poller = PollUntilCondition(tag, monitor, predicate, token);
        var task = await Task.WhenAny(poller, waiter).ConfigureAwait(false);

        // If the predicate hit, return the result of the task.
        if (task is Task<TagResult> resultTask)
        {
            return await resultTask;
        }

        // Otherwise the delay expired - return the current result of the monitor and dispose.
        return monitor.GetResult(tag.TagName);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed) return;

        foreach (var watch in _watches.Values)
        {
            watch.Dispose();
        }

        foreach (var handle in _handles.Values)
        {
            var status = handle.Free(_tagService);
            TagException.ThrowIfRequested(status, _options.ThrowOn);
        }

        _watches.Clear();
        _handles.Clear();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Retrieves the updated members of a tag based on the provided updates collection. This will both validate
    /// and update all the members to be written to the PLC.
    /// </summary>
    private static Tag[] GetUpdatedMembersFor(Tag tag, IReadOnlyList<(TagName Member, AtomicData Value)> updates)
    {
        return updates.Select(x =>
        {
            if (x.Member is null || x.Member.IsEmpty)
                throw new ArgumentException(
                    "Member paths must be non-empty and relative to the base tag.",
                    nameof(updates)
                );

            // This throws if the member path is invalid, which is good feedback to the caller.
            var member = tag[x.Member];

            if (!member.Value.IsAtomic())
                throw new ArgumentException($"Member '{x.Member}' is not an atomic value.", nameof(updates));

            member.Value = x.Value;
            return member;
        }).ToArray();
    }

    /// <summary>
    /// Identifies and returns the collection of members of a given tag that are modified as a result of the
    /// specified update action.
    /// </summary>
    private static Tag[] GetUpdatedMembersFor<TData>(Tag tag, Action<TData> update) where TData : LogixData, new()
    {
        // Create a list of tag names to track which members are updated.
        var memberNames = new HashSet<TagName>();

        // Subscribe to changes on the underlying XElement to record each member change.
        tag.Serialize().Changed += OnTagValueChanged;

        try
        {
            // Apply the update action to the tag instance.
            update(tag.Value.As<TData>());
        }
        finally
        {
            // Clean up the event handler
            tag.Serialize().Changed -= OnTagValueChanged;
        }

        // After the update completes, return the collection of member tags relative to the base tag that were updated.
        return tag.Members(t => memberNames.Contains(t.LocalPath)).ToArray();

        void OnTagValueChanged(object? sender, XObjectChangeEventArgs args)
        {
            //We only care about value changes (todo need to check with string CDATA though)
            if (args.ObjectChange != XObjectChange.Value) return;

            // Should be an XObject from which we can get the parent element which should be the data member.
            if (sender is not XObject obj) return;
            var element = obj.Parent;

            var tagName = element?.GetTagNamePath();

            if (tagName is not null)
            {
                memberNames.Add(tagName);
            }
        }
    }

    /// <summary>
    /// Monitors a specified tag for the given duration and returns the final result of the tag's state.
    /// </summary>
    private async Task<TagResult> PollForDuration(Tag tag, TimeSpan duration, CancellationToken token)
    {
        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(duration), duration, "Duration must be non-negative.");

        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();
        ThrowIfInvalidMemberSet(members);

        // Kick off tag monitoring to stream changes to the tag instance
        using var monitor = await CreateMonitor(members, token).ConfigureAwait(false);

        // Wait the full duration unless canceled by the caller.
        await Task.Delay(duration, token).ConfigureAwait(false);

        // Get the base tag result from the monitor and return.
        return monitor.GetResult(tag.TagName);
    }

    /// <summary>
    /// Polls the tag using the provided monitor until the specified condition is met. This is achieved using
    /// a task completion source to await the on update event asynchronously.  
    /// </summary>
    private static async Task<TagResult> PollUntilCondition<TData>(
        Tag tag,
        TagMonitor monitor,
        Func<TData, bool> predicate,
        CancellationToken token = default) where TData : LogixData
    {
        // We'll use another task completion source to await the callback event asynchronously.
        var poller = new TaskCompletionSource<TagResult>(TaskCreationOptions.RunContinuationsAsynchronously);

        // Response to all updates and get the base tag to evaluate the entire data structure.
        // Once the predicate is satisfied, mark the polling task complete, or errored on exception
        monitor.OnUpdate(tag.TagName, r =>
        {
            try
            {
                var value = r.Tag.Value.As<TData>();

                if (predicate(value))
                {
                    poller.TrySetResult(r);
                }
            }
            catch (Exception ex)
            {
                poller.TrySetException(ex);
            }
        });

        // Register the poller cancellation with the provided token and await the task completion. 
        using (token.Register(() => poller.TrySetCanceled(token)))
        {
            return await poller.Task.ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Reads a collection of tag members to the PLC and returns any error results encountered during the read operations.
    /// This method processes a bulk group of atomic tag members, using cached handles to optimize performance for successive operations.
    /// </summary>
    private async Task<Dictionary<TagName, TagError[]>> ReadAllTags(Tag[] members, CancellationToken token = default)
    {
        // Pair up each tag member with a read value operation
        var tasks = members.Select(m => (Member: m, Read: ReadTagValue(m, token))).ToList();

        // Await all member read results before processing
        await Task.WhenAll(tasks.Select(t => t.Read));

        // Aggregate errors into a lookup to format the results.
        return tasks.Where(t => t.Read.Result < 0)
            .GroupBy(t => t.Member.Base.TagName)
            .ToDictionary(g => g.Key, g => g.Select(x => new TagError(x.Member.TagName, x.Read.Result)).ToArray());

        async Task<TagStatus> ReadTagValue(Tag tag, CancellationToken ct)
        {
            var tagName = tag.TagName;
            var handle = await GetOrCreateHandle(tagName, ct);

            var key = OperationKey.Read(tagName);
            var status = (await ExecuteAsync(key, () => _tagService.Read(handle.TagId, AsyncTimeout), ct)).AsStatus();
            TagException.ThrowIfRequested(status, _options.ThrowOn);

            if (status == TagStatus.Ok)
            {
                _tagBuffer.ReadValue(tag, handle.TagId);
            }

            return status;
        }
    }

    /// <summary>
    /// Writes a collection of tag members to the PLC and returns any error results encountered during the write operations.
    /// This method processes a bulk group of atomic tag members, using cached handles to optimize performance for successive operations.
    /// </summary>
    private async Task<Dictionary<TagName, TagError[]>> WriteAllTags(Tag[] members, CancellationToken token = default)
    {
        // Pair up each tag member with a write value operation
        var tasks = members.Select(m => (Member: m, Write: WriteTagValue(m, token))).ToList();

        // Await all member write results before processing
        await Task.WhenAll(tasks.Select(t => t.Write));

        // Aggregate errors into a lookup to format the results.
        return tasks.Where(t => t.Write.Result < 0)
            .GroupBy(t => t.Member.Base.TagName)
            .ToDictionary(g => g.Key, g => g.Select(x => new TagError(x.Member.TagName, x.Write.Result)).ToArray());

        async Task<TagStatus> WriteTagValue(Tag tag, CancellationToken ct)
        {
            var tagName = tag.TagName;
            var handle = await GetOrCreateHandle(tagName, ct);

            var written = _tagBuffer.WriteValue(tag, handle.TagId).AsStatus();
            if (written < 0)
            {
                TagException.ThrowIfRequested(written, _options.ThrowOn);
                return written;
            }

            var key = OperationKey.Write(tagName);
            var status = (await ExecuteAsync(key, () => _tagService.Write(handle.TagId, AsyncTimeout), ct)).AsStatus();
            TagException.ThrowIfRequested(status, _options.ThrowOn);
            return status;
        }
    }

    /// <summary>
    /// Creates a new <see cref="TagMonitor"/> object initialized with a collection of <see cref="TagWatch"/> for the
    /// provided tag members. This method will spin up a watch for each tag member in the collection and
    /// once all are completed, return a monitor instance that will manage the lifetime of the watch and provide
    /// insight on the status/health of all tags. This is the primary mechanism through which we will stream tag data
    /// into the Tag objects at a specified rate.
    /// </summary>
    private async Task<TagMonitor> CreateMonitor(Tag[] members, CancellationToken token)
    {
        var tasks = members.Select(t => GetOrCreateWatch(t, token)).ToList();
        var watches = await Task.WhenAll(tasks);
        return new TagMonitor(watches.ToList(), w => w.StopOrDecrement());
    }

    /// <summary>
    /// Attempts to get a <see cref="TagWatch"/> for the specified tag instance from the internal cache. If not found,
    /// then creates and caches a new watch instance and returns it. Watches will create separate tag handles to
    /// prevent cross-talk with other read/write operations. We are caching these instances to avoid creating multiple
    /// watches/handles per tag. If more than one monitor subscribes to the same tag, the watch will internally track that
    /// to maintain polling until all monitors (or this client) are disposed of. This method will also start or increment
    /// the subscriber count so that it does not need to be handled externally.
    /// </summary>
    private async Task<TagWatch> GetOrCreateWatch(Tag tag, CancellationToken token)
    {
        var tagName = tag.TagName;

        // Check the watch cache for an existing instance for the specified tag name.
        // If found, increase the subscriber count and return.
        if (_watches.TryGetValue(tagName, out var cached) && !cached.IsErrored)
        {
            cached.StartOrIncrement();
            return cached;
        }

        // Create a separate handle for tag watch instances to avoid other operations from interfering
        // with the internal tag buffer.
        var handle = await CreateHandle(tagName, token);

        var watch = _watches.AddOrUpdate(tagName,
            new TagWatch(handle, tag, _options.PollRate, _tagService, _tagBuffer),
            (_, w) =>
            {
                if (w.IsErrored)
                {
                    return new TagWatch(handle, tag, _options.PollRate, _tagService, _tagBuffer);
                }

                return w;
            });

        watch.StartOrIncrement();
        return watch;
    }

    /// <summary>
    /// Attempts to get a <see cref="TagHandle"/> for the specified tag name from the internal cache. If not found,
    /// then create and cache a new handle and return. We are caching tag handles to avoid reallocating new memory
    /// each time we want to interact with the same tag for this PLC connection. This is the most expensive part of the
    /// tag lifecycle, so this is a key performance optimization.
    /// </summary>
    private async Task<TagHandle> GetOrCreateHandle(TagName tagName, CancellationToken token)
    {
        // If a valid handle is ready, yield that back to the caller.
        if (_handles.TryGetValue(tagName, out var cached) && cached.IsValid)
            return cached;

        // Try to create a new tag handle. In this case it either didn't exist
        // or was errored the last time an attempt was made.
        var handle = await CreateHandle(tagName, token);

        // Attempt to cache the handle.
        _handles.AddOrUpdate(tagName, handle, (_, h) =>
        {
            // If for some reason (like race conditions) a valid handle was cached already,
            // then release the duplicate memory pointer and return the cached handle instead.
            if (h.IsValid)
            {
                Marshal.FreeHGlobal(handle.TagPtr);
                return h;
            }

            // Otherwise, the previous handle failed, so replace it.
            return handle;
        });

        return handle;
    }

    /// <summary>
    /// Creates a new <see cref="TagHandle"/> for the provided tag name path. This registers a handle with libplctag
    /// so that we can interact with (read/write) tag data. We will pass the event callback and tag name pointer to
    /// the create function. This lets us await the result without blocking any threads and use the pinned tag name
    /// associated with the handle for lookup of pending operations or watches.
    /// </summary>
    private async Task<TagHandle> CreateHandle(TagName tagName, CancellationToken token)
    {
        // Build the Uri route to the PLC tag.
        var route = $"{_uri}&name={tagName}";

        // Pass the tag name string as the unmanaged pointer.
        // This user data will be available in the event callback, which is needed for lookups.
        var tagPtr = Marshal.StringToHGlobalAnsi(tagName);

        var tagId = await ExecuteAsync(
            OperationKey.Create(tagName),
            () => _tagService.Create(route, TagEventCallback, tagPtr, AsyncTimeout),
            token
        );

        // If failed, clean up immediately - return an error handle.
        if (tagId <= 0)
        {
            Marshal.FreeHGlobal(tagPtr);
            TagException.ThrowIfRequested(tagId.AsStatus(), _options.ThrowOn, tagName);
            return TagHandle.Error(tagId);
        }

        return new TagHandle(tagId, tagPtr);
    }

    /// <summary>
    /// Executes a native operation asynchronously by queuing a task completion source for the operation
    /// associated with the specified operation key.
    /// </summary>
    /// <param name="key">An <see cref="OperationKey"/> that uniquely identifies the operation by tag name and event type.</param>
    /// <param name="native">A delegate representing the native function to be executed.</param>
    /// <param name="token">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>An integer representing the result of the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an operation is already in progress for the specified operation key.</exception>
    private async Task<int> ExecuteAsync(OperationKey key, Func<int> native, CancellationToken token)
    {
        var operation = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!_operations.TryAdd(key, operation))
            throw new InvalidOperationException($"An operation is already in progress for '{key.Tag}'.");

        try
        {
            // Start the native operation first. 
            // Native method with no timeout will return immediately with pending or error.
            var status = native();

            // If an error returned, we can return without awaiting the task.
            if (status < 0) return status;

            // Emulate the timeout with a linked cancellation for the specified timeout.
            using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(token);
            cancellation.CancelAfter(_options.Timeout);

            // When canceled, call the abort operation to stop and remove the task from the pending lookup.
            using (cancellation.Token.Register(() => AbortOperation(key, token)))
            {
                return await operation.Task.ConfigureAwait(false);
            }
        }
        finally
        {
            _operations.TryRemove(key, out _);
        }
    }

    /// <summary>
    /// Aborts an ongoing operation associated with the specified operation key and cleans up related resources.
    /// </summary>
    /// <param name="key">The operation key that uniquely identifies the operation by tag name and event type.</param>
    /// <param name="token">The cancellation token used to signal the operation should be canceled.</param>
    /// <exception cref="TagException">Thrown if the operation results in a timeout for the specified tag.</exception>
    private void AbortOperation(OperationKey key, CancellationToken token)
    {
        if (_operations.TryGetValue(key, out var pending))
        {
            if (token.IsCancellationRequested)
                pending.TrySetCanceled();
            else
                pending.TrySetException(new TagException(TagStatus.Timeout, key.Tag));
        }

        if (_handles.TryGetValue(key.Tag, out var handle))
        {
            var status = _tagService.Abort(handle.TagId).AsStatus();
            TagException.ThrowIfRequested(status, _options.ThrowOn);
        }
    }

    /// <summary>
    /// Handles tag-related events triggered by the PLC, such as creation, read completion, or write completion.
    /// </summary>
    /// <param name="handle">An identifier associated with the operation or event.</param>
    /// <param name="eventId">The event type identifier, indicating what kind of operation or event occurred.</param>
    /// <param name="statusId">The status code of the operation, typically used to determine the operation result.</param>
    /// <param name="userData">A pointer to user-defined data associated with the event, typically containing tag-specific details.</param>
    private void TagEventCallback(int handle, int eventId, int statusId, IntPtr userData)
    {
        // We only want to process events that are "completed" (read/write/create) and not pending or starting,
        // and that have the required user data tag pointer.
        if (!TagEvent.IsComplete(eventId) || statusId.AsStatus() == TagStatus.Pending || userData == IntPtr.Zero)
            return;

        // Get the tag name from the pinned memory location.
        var tagName = Marshal.PtrToStringAnsi(userData);
        var key = new OperationKey(tagName, eventId);

        // At this point we should attempt to "dequeue" an operation if it exists.
        if (_operations.TryRemove(key, out var operation))
        {
            switch (eventId)
            {
                // For a successful create operation, we want to set the result to the returned tag handle.
                case TagEvent.Created when statusId == 0:
                    operation.TrySetResult(handle);
                    break;
                // For all other operations/cases, we want the set the result to the returned status.
                default:
                    operation.TrySetResult(statusId);
                    return;
            }
        }

        // Also, see if any tag watch exists for the tag name and notify of the resulting status.
        if (_watches.TryGetValue(tagName, out var watch))
        {
            watch.Notify(statusId.AsStatus());
        }
    }

    /// <summary>
    /// Ensures that the provided <see cref="TagName"/> represents a base tag and throws an exception if it does not.
    /// </summary>
    /// <param name="tagName">
    /// The <see cref="TagName"/> to validate. A base tag has a depth of zero and represents the root of a tag hierarchy.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the specified <paramref name="tagName"/> does not represent a base tag. Update operations require
    /// a base tag. For operations on atomic members, use the WriteTag method instead.
    /// </exception>
    private static void ThrowIfNotBaseTag(TagName tagName)
    {
        if (tagName.Depth == 0)
            return;

        throw new ArgumentException(
            "UpdateTag requires a base tag. Use WriteTag for atomic member writes.",
            nameof(tagName)
        );
    }

    /// <summary>
    /// Validates the set of tag members and checks for duplicates within the provided collection of tags.
    /// </summary>
    private static void ThrowIfInvalidMemberSet(ICollection<Tag> members)
    {
        if (members.Count == 0)
            throw new ArgumentException("No atomic members found in the provided member set.");

        var duplicates = members.GroupBy(m => m.TagName)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key.ToString())
            .ToArray();

        if (duplicates.Length > 0)
            throw new ArgumentException(
                $"Member set contains duplicate tag names. '{string.Join(", ", duplicates)}");
    }

    /// <summary>
    /// Ensures that the <see cref="PlcClient"/> instance has not been disposed before proceeding with the operation.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="PlcClient"/> has already been disposed and can no longer be used.
    /// </exception>
    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new InvalidOperationException("The client has been disposed and can no longer perform actions.");
        }
    }
}