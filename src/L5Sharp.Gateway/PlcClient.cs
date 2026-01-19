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
    /// timeout settings, and read interval for PLC operations.
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
    /// timeout settings, and read interval for PLC operations.
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
    public Task<TagResponse> ReadTag<TData>(TagName tagName, CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        ThrowIfDisposed();

        //Create new virtual tag instance to collect data.
        var tag = Tag.New<TData>(tagName);

        return ReadAllTags([tag], token);
    }

    /// <inheritdoc />
    public Task<TagResponse> ReadTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        return ReadAllTags([tag], token);
    }

    /// <inheritdoc />
    public Task<TagResponse> ReadTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        return ReadAllTags(tags.ToArray(), token);
    }

    /// <inheritdoc />
    public Task<TagResponse> WriteTag<TData>(TagName tagName, Action<TData> update, CancellationToken token = default)
        where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (update is null) throw new ArgumentNullException(nameof(update));
        ThrowIfDisposed();

        //Create new virtual tag instance to update data.
        var tag = Tag.New<TData>(tagName);
        update(tag.Value.As<TData>());

        return WriteAllTags([tag], token);
    }

    /// <inheritdoc />
    public Task<TagResponse> WriteTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        return WriteAllTags([tag], token);
    }

    /// <inheritdoc />
    public Task<TagResponse> WriteTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        return WriteAllTags(tags.ToArray(), token);
    }

    /// <inheritdoc />
    public Task<IDisposable> WatchTag<TData>(TagName tagName, Action<Tag>? onChange = null,
        CancellationToken token = default) where TData : LogixData, new()
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        ThrowIfDisposed();

        //Create new virtual tag instance to collect data.
        var tag = Tag.New<TData>(tagName);

        return WatchAllTags([tag], onChange, token);
    }

    /// <inheritdoc />
    public Task<IDisposable> WatchTag(Tag tag, Action<Tag>? onChange = null, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        return WatchAllTags([tag], onChange, token);
    }

    /// <inheritdoc />
    public Task<IDisposable> WatchTags(IEnumerable<Tag> tags, Action<Tag>? onChange = null,
        CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        return WatchAllTags(tags.ToArray(), onChange, token);
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
    /// Reads the values of the specified collection of tag members and returns a response containing the results.
    /// </summary>
    private async Task<TagResponse> ReadAllTags(ICollection<Tag> tags, CancellationToken token = default)
    {
        var members = tags.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToList();
        if (members.Count == 0) return TagResponse.NoData(tags);

        var tasks = members.Select(m => (Member: m, Read: ReadTagValue(m, token))).ToList();

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks.Select(t => t.Read));
        timer.Stop();

        var results = tasks.Select(t => (t.Member.TagName, t.Read.Result)).ToList();
        return TagResponse.Aggregate(tags, results, timer.Elapsed);
    }

    /// <summary>
    /// Writes a collection of tag members to the PLC and returns a response containing the results of the operation.
    /// </summary>
    private async Task<TagResponse> WriteAllTags(ICollection<Tag> tags, CancellationToken token = default)
    {
        var members = tags.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToList();
        if (members.Count == 0) return TagResponse.NoData(tags);

        var tasks = members.Select(m => (Member: m, Write: WriteTagValue(m, token))).ToList();

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks.Select(t => t.Write));
        timer.Stop();

        var results = tasks.Select(t => (t.Member.TagName, t.Write.Result)).ToList();
        return TagResponse.Aggregate(tags, results, timer.Elapsed);
    }

    /// <summary>
    /// Subscribes to changes in a collection of tag members and invokes a callback when changes occur.
    /// </summary>
    private async Task<IDisposable> WatchAllTags(ICollection<Tag> tags, Action<Tag>? onChanged, CancellationToken token)
    {
        var members = tags.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToList();
        if (members.Count == 0)
            throw new InvalidOperationException("No atomic tag data members exist in the provided set of tags.");

        var tasks = members.Select(m => (Member: m, Create: GetOrCreateHandle(m.TagName, token))).ToList();
        await Task.WhenAll(tasks.Select(t => t.Create));

        foreach (var (member, create) in tasks)
        {
            var handle = create.Result;
            if (handle <= 1) continue;

            var watch = _watches.GetOrAdd(
                handle,
                h => new TagWatch(h, member, _options.ReadInterval, _tagService, _tagBuffer)
            );

            watch.Increment();
            if (onChanged is not null) watch.OnChanged += onChanged;
        }

        return new Unsubscriber(() =>
        {
            // If the caller already disposed of the client, then we can return.
            if (_disposed) return;

            foreach (var task in tasks)
            {
                var handle = task.Create.Result;

                if (!_watches.TryGetValue(handle, out var watch))
                    continue;

                if (onChanged is not null) watch.OnChanged -= onChanged;
                watch.Decrement();
                if (watch.IsIdle) _watches.TryRemove(watch.Handle, out _);
            }
        });
    }

    /// <summary>
    /// Reads the value of a specified tag from the PLC. The provided tag is expected to be an atomic type tag
    /// that has public access. If not, we might encounter an error, which we either throw or return depending on
    /// the configured options.
    /// </summary>
    private async Task<TagStatus> ReadTagValue(Tag tag, CancellationToken token)
    {
        var tagName = tag.TagName;
        var handle = await GetOrCreateHandle(tagName, token);

        var status = (await ExecuteAsync(tagName, () => _tagService.Read(handle, AsyncTimeout), token)).AsStatus();
        TagException.ThrowIfRequested(status, _options.ThrowOn);

        if (status == TagStatus.Ok)
        {
            _tagBuffer.ReadValue(tag, handle);
        }

        return status;
    }

    /// <summary>
    /// Writes the value of the specified tag to the PLC asynchronously.
    /// </summary>
    /// <param name="tag">The tag object containing the value and metadata to be written.</param>
    /// <param name="token">The cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A <see cref="TagStatus"/> representing the status of the write operation.</returns>
    /// <exception cref="TagException">Thrown if the write operation fails and exceptions are configured to be thrown.</exception>
    private async Task<TagStatus> WriteTagValue(Tag tag, CancellationToken token)
    {
        var tagName = tag.TagName;
        var handle = await GetOrCreateHandle(tagName, token);

        var written = _tagBuffer.WriteValue(tag, handle).AsStatus();
        if (written < 0)
        {
            TagException.ThrowIfRequested(written, _options.ThrowOn);
            return written;
        }

        var status = (await ExecuteAsync(tagName, () => _tagService.Write(handle, AsyncTimeout), token)).AsStatus();
        TagException.ThrowIfRequested(status, _options.ThrowOn);
        return status;
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
        // This user data will be available in the event callback, which is needed for lookup.
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
        // If so and this event was a completed read, then notify the watch.
        if (_watches.TryGetValue(handle, out var watch) && eventId == TagEvent.ReadCompleted)
        {
            watch.Notify(statusId.AsStatus());
        }
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

    /// <summary>
    /// Provides a mechanism to manage and handle the unsubscription of tag-related callbacks in a monitoring system.
    /// This class is used internally to dispose of subscriptions when they are no longer needed.
    /// </summary>
    private class Unsubscriber(Action unsubscribe) : IDisposable
    {
        public void Dispose() => unsubscribe();
    }
}