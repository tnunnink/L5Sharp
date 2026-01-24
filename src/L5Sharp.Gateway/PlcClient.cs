using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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
    private readonly ConcurrentDictionary<TagName, TaskCompletionSource<int>> _operations = [];

    /// <summary>
    /// Maintains a thread-safe collection of active <see cref="TagWatch"/> instances associated with their respective handles.
    /// This is used to manage and track tag watch operations within the <see cref="PlcClient"/>.
    /// </summary>
    private readonly ConcurrentDictionary<int, TagWatch> _watches = [];

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
        var errorMap = await ReadAllTag(members, token);
        stopwatch.Stop();

        var tagErrors = errorMap.TryGetValue(tag.TagName, out var e) ? e : [];
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
        var errorMap = await ReadAllTag(members, token);
        stopwatch.Stop();

        var tagErrors = errorMap.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, tagErrors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResults> ReadTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        var tagGroup = tags.ToArray();
        var members = tagGroup.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var errorMap = await ReadAllTag(members, token);
        stopwatch.Stop();

        // Regroup all the tags and corresponding errors into a tag result to aggregate.
        var results = tagGroup.Select(g =>
        {
            var errors = errorMap.TryGetValue(g.TagName, out var value) ? value : [];
            return TagResult.Create(g, errors, stopwatch.Elapsed);
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

        var tag = Tag.Create(tagName).WithValue(data).Build();
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var errorMap = await WriteAllTags(members, token);
        stopwatch.Stop();

        var errors = errorMap.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, errors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResult> WriteTag<TData>(TagName tagName, Action<TData> update,
        CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (update is null) throw new ArgumentNullException(nameof(update));
        ThrowIfDisposed();

        //Create new virtual tag instance to update data.
        var tag = Tag.New<TData>(tagName);
        update(tag.Value.As<TData>());
        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var errorMap = await WriteAllTags(members, token);
        stopwatch.Stop();

        var errors = errorMap.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, errors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResult> WriteTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var errorMap = await WriteAllTags(members, token);
        stopwatch.Stop();

        var errors = errorMap.TryGetValue(tag.TagName, out var e) ? e : [];
        return TagResult.Create(tag, errors, stopwatch.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResults> WriteTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        var tagGroup = tags.ToArray();
        var members = tagGroup.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToArray();

        ThrowIfInvalidMemberSet(members);

        var stopwatch = Stopwatch.StartNew();
        var errorMap = await WriteAllTags(members, token);
        stopwatch.Stop();

        var results = tagGroup.Select(g =>
        {
            var errors = errorMap.TryGetValue(g.TagName, out var e) ? e : [];
            return TagResult.Create(g, errors, stopwatch.Elapsed);
        });

        return TagResults.Aggregate(results.ToArray());
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
    public Task<TagResult> PollTag<TData>(TagName tagName, Func<TData, bool> predicate,
        TimeSpan? timeout = null,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        ThrowIfDisposed();

        var tag = Tag.New<TData>(tagName);
        
        return PullUntilCondition(tag, predicate, timeout, token);
    }

    /// <inheritdoc />
    public Task<TagResult> PollTag<TData>(Tag tag, Func<TData, bool> predicate, TimeSpan? timeout = null,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        ThrowIfDisposed();

        return PullUntilCondition(tag, predicate, timeout, token);
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
            Marshal.FreeHGlobal(handle.TagPtr);
            var status = _tagService.Destroy(handle.TagId).AsStatus();
            TagException.ThrowIfRequested(status, _options.ThrowOn);
        }

        _watches.Clear();
        _handles.Clear();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Reads a collection of tag members to the PLC and returns any error results encountered during the read operations.
    /// This method processes a bulk group of atomic tag members, using cached handles to optimize performance for successive operations.
    /// </summary>
    private async Task<Dictionary<TagName, TagError[]>> ReadAllTag(Tag[] members, CancellationToken token = default)
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

            var status = (await ExecuteAsync(tagName, () => _tagService.Read(handle, AsyncTimeout), ct)).AsStatus();
            TagException.ThrowIfRequested(status, _options.ThrowOn);

            if (status == TagStatus.Ok)
            {
                _tagBuffer.ReadValue(tag, handle);
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

        async Task<TagStatus> WriteTagValue(Tag tag, CancellationToken t)
        {
            var tagName = tag.TagName;
            var handle = await GetOrCreateHandle(tagName, t);

            var written = _tagBuffer.WriteValue(tag, handle).AsStatus();
            if (written < 0)
            {
                TagException.ThrowIfRequested(written, _options.ThrowOn);
                return written;
            }

            var status = (await ExecuteAsync(tagName, () => _tagService.Write(handle, AsyncTimeout), t)).AsStatus();
            TagException.ThrowIfRequested(status, _options.ThrowOn);
            return status;
        }
    }

    /// <summary>
    /// Monitors a specified tag for the given duration and returns the final result of the tag's state.
    /// </summary>
    private async Task<TagResult> PollForDuration(Tag tag, TimeSpan duration, CancellationToken token)
    {
        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(duration), duration, "Duration must be non-negative.");

        // Kick off tag monitoring to stream changes to the tag instance
        using var monitor = await CreateMonitor([tag], token).ConfigureAwait(false);

        // Wait the full duration unless canceled by the caller.
        if (duration > TimeSpan.Zero)
            await Task.Delay(duration, token).ConfigureAwait(false);

        // Get the base tag result from the monitor and return.
        return monitor.GetResult(tag.TagName);
    }

    /// <summary>
    /// Monitors a specified tag until it meets the provided condition defined by the predicate
    /// and returns the corresponding tag result upon satisfaction of the condition.
    /// </summary>
    private async Task<TagResult> PullUntilCondition<TData>(Tag tag,
        Func<TData, bool> predicate,
        TimeSpan? timeout = null,
        CancellationToken token = default) where TData : LogixData
    {
        // Link the provided token with a timeout cancellation if specified.
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(token);
        if (timeout.HasValue) cancellation.CancelAfter(timeout.Value);

        // Kick off a tag monitor to stream updates into the tag instance.
        using var monitor = await CreateMonitor([tag], token).ConfigureAwait(false);

        // Create a task completion source to make the event callback awaitable. Register the cancellation callback.
        var poller = new TaskCompletionSource<TagResult>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var registration = cancellation.Token.Register(() => poller.TrySetCanceled(token));

        // Response to any change on the tag and get the base tag to evaluate the entire data structure.
        monitor.OnChange(tag.TagName, r =>
        {
            try
            {
                var value = r.Tag.Value.As<TData>();
                if (predicate(value)) poller.TrySetResult(r);
            }
            catch (Exception ex)
            {
                poller.TrySetException(ex);
            }
        });

        return await poller.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new tag subscription for the specified tags and starts monitoring their values.
    /// </summary>
    private async Task<TagMonitor> CreateMonitor(Tag[] members, CancellationToken token)
    {
        var tasks = members.Select(m => (Member: m, Create: GetOrCreateHandle(m.TagName, token))).ToList();
        await Task.WhenAll(tasks.Select(t => t.Create));

        var watches = new List<TagWatch>();

        foreach (var (tag, create) in tasks)
        {
            var handle = create.Result;

            if (handle <= 1)
                throw new InvalidOperationException(
                    $"Failed to create handle for tag '{tag.TagName}' due to error {handle}");

            var watch = _watches.GetOrAdd(
                handle,
                h => new TagWatch(h, tag, _options.PollRate, _tagService, _tagBuffer)
            );

            // This will make sure the watch reads the initial buffer value.
            watch.Notify(TagStatus.Ok);
            // This will increase the subscriber count and start polling.
            watch.Increment();

            watches.Add(watch);
        }

        return new TagMonitor(watches, w =>
        {
            if (_disposed) return;
            w.Decrement();
            if (w.IsIdle) _watches.TryRemove(w.Handle, out _);
        });
    }

    /// <summary>
    /// Retrieves an existing handle for the specified tag or creates a new one if it does not exist.
    /// </summary>
    /// <param name="tagName">The name of the tag for which the handle is being requested.</param>
    /// <param name="token">A <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
    /// <returns>An integer representing the handle associated with the specified tag.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an error occurs during handle creation or if the operation fails.</exception>
    /// <exception cref="TagException">Thrown when a tag-level error is encountered during handle creation.</exception>
    private async Task<int> GetOrCreateHandle(TagName tagName, CancellationToken token)
    {
        // Get cached handle if exists to avoid recreating
        if (_handles.TryGetValue(tagName, out var cached))
            return cached.TagId;

        // Build the Uri route to the PLC tag.
        var route = $"{_uri}&name={tagName}";

        // Pass the tag name string as the unmanaged pointer.
        // This user data will be available in the event callback, which is needed for lookups.
        var tagPtr = Marshal.StringToHGlobalAnsi(tagName);

        // Execute the create function asynchronously.
        var tagId = await ExecuteAsync(tagName,
            () => _tagService.Create(route, TagEventCallback, tagPtr, AsyncTimeout),
            token
        );

        // If failed, clean up immediately.
        if (tagId <= 0)
        {
            Marshal.FreeHGlobal(tagPtr);
            TagException.ThrowIfRequested(tagId.AsStatus(), _options.ThrowOn, tagName);
            return tagId;
        }

        // If successful, cache the handle and return.
        if (!_handles.TryAdd(tagName, new TagHandle(tagId, tagPtr)))
        {
            // Another thread won the race while we were awaiting
            // Free our redundant pointer to prevent memory leak.
            Marshal.FreeHGlobal(tagPtr);
            return _handles[tagName].TagId;
        }

        return tagId;
    }

    /// <summary>
    /// Executes a native operation asynchronously using by queuing a task completion source for the operation
    /// associated with the specified tag name.
    /// </summary>
    /// <param name="tagName">The name of the tag for which the operation is being executed.</param>
    /// <param name="native">A delegate representing the native function to be executed.</param>
    /// <param name="token">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>An integer representing the result of the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an operation is already in progress for the specified tag name.</exception>
    private async Task<int> ExecuteAsync(string tagName, Func<int> native, CancellationToken token)
    {
        var operation = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!_operations.TryAdd(tagName, operation))
            throw new InvalidOperationException($"An operation is already in progress for '{tagName}'.");

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
            using (cancellation.Token.Register(() => AbortOperation(tagName, token)))
            {
                return await operation.Task.ConfigureAwait(false);
            }
        }
        finally
        {
            _operations.TryRemove(tagName, out _);
        }
    }

    /// <summary>
    /// Aborts an ongoing operation associated with the specified tag name and cleans up related resources.
    /// </summary>
    /// <param name="tagName">The name of the tag for which the operation should be aborted.</param>
    /// <param name="token">The cancellation token used to signal the operation should be canceled.</param>
    /// <exception cref="TagException">Thrown if the operation results in a timeout for the specified tag.</exception>
    private void AbortOperation(string tagName, CancellationToken token)
    {
        if (_operations.TryGetValue(tagName, out var pending))
        {
            if (token.IsCancellationRequested)
                pending.TrySetCanceled();
            else
                pending.TrySetException(new TagException(TagStatus.Timeout, tagName));
        }

        if (_handles.TryGetValue(tagName, out var handle))
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

        // At this point we should attempt to "dequeue" an operation if it exists.
        if (_operations.TryRemove(tagName, out var operation))
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

        // At this point we should see if any tag watch exists for this handle.
        if (_watches.TryGetValue(handle, out var watch))
        {
            watch.Notify(statusId.AsStatus());
        }
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