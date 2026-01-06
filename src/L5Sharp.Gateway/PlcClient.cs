using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;
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
    /// Represents the IP address of the PLC being communicated with.
    /// This address is used to establish a connection to the target PLC
    /// for sending and receiving tag data.
    /// </summary>
    private readonly IPAddress _ip;

    /// <summary>
    /// Represents the configuration options used by the <see cref="PlcClient"/> for interacting with a PLC.
    /// This variable stores an instance of <see cref="PlcOptions"/> to define settings such as timeouts
    /// and communication intervals for PLC operations.
    /// </summary>
    private readonly PlcOptions _options;

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
    /// Stores pending read operations initiated by the <see cref="PlcClient"/> instance,
    /// associating a unique identifier with a <see cref="TaskCompletionSource{TResult}"/>
    /// to track and resolve the completion of each read request asynchronously.
    /// </summary>
    private readonly ConcurrentDictionary<string, TaskCompletionSource<int>> _operations = [];

    /// <summary>
    /// Maintains a collection of active <see cref="ITagWatch"/> instances created by the <see cref="PlcClient"/>.
    /// This variable is used to manage and track watches that monitor changes to PLC tags, ensuring
    /// their lifecycle is appropriately handled and allowing centralized access for operations such as disposal.
    /// </summary>
    private readonly ConcurrentBag<ITagWatch> _watches = [];

    /// <summary>
    /// Creates a new <see cref="PlcClient"/> instance with the provided IP address and optional slot number.
    /// </summary>
    /// <param name="ip">The IP address of the PLC to connect to.</param>
    /// <param name="slot">The slot of the PLC in the backplane. Default is '0'.</param>
    /// <param name="options"></param>
    /// <param name="tagService"></param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="ip"/> is not a valid IP.</exception>
    public PlcClient(string ip, ushort slot = 0, PlcOptions? options = null, ITagService? tagService = null)
    {
        if (!IPAddress.TryParse(ip, out var address))
            throw new ArgumentException($"Unable to parse IP address: {ip}");

        _tagService = tagService ?? new NativeTagService(onEventEx: TagEventCallback);
        _tagBuffer = new TagBuffer(_tagService);
        _ip = address;
        _options = options ?? new PlcOptions();
        _uri = $"protocol=ab_eip&gateway={address}&path=1,{slot}&plc=controllogix";
    }

    /// <inheritdoc />
    public Task<bool> Ping(CancellationToken token = default)
    {
        return Task.Run(async () =>
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(_ip).ConfigureAwait(false);
            return reply.Status == IPStatus.Success;
        }, token);
    }

    /// <inheritdoc />
    public async Task<TagResponse> ReadTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToList();
        var tasks = members.Select(m => (Member: m, Read: ReadTagValue(m, token))).ToList();

        if (tasks.Count == 0)
            return TagResponse.NoData(tag.TagName);

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks.Select(t => t.Read));
        timer.Stop();

        var results = tasks.Select(t => (t.Member.TagName, t.Read.Result)).ToList();
        return TagResponse.Aggregate(results, timer.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResponse> ReadTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        var members = tags.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToList();
        var tasks = members.Select(m => (Member: m, Read: ReadTagValue(m, token))).ToList();

        if (tasks.Count == 0)
            return TagResponse.NoData(members.Select(t => t.TagName).ToArray());

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks.Select(t => t.Read));
        timer.Stop();

        var results = tasks.Select(t => (t.Member.TagName, t.Read.Result)).ToList();
        return TagResponse.Aggregate(results, timer.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResponse> WriteTag(Tag tag, CancellationToken token = default)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        ThrowIfDisposed();

        var members = tag.Members(m => m.Value.IsAtomic()).ToList();
        var tasks = members.Select(m => (Member: m, Write: WriteTagValue(m, token))).ToList();

        if (tasks.Count == 0)
            return TagResponse.NoData(tag.TagName);

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks.Select(t => t.Write));
        timer.Stop();

        var results = tasks.Select(t => (t.Member.TagName, t.Write.Result)).ToList();
        return TagResponse.Aggregate(results, timer.Elapsed);
    }

    /// <inheritdoc />
    public async Task<TagResponse> WriteTags(IEnumerable<Tag> tags, CancellationToken token = default)
    {
        if (tags is null) throw new ArgumentNullException(nameof(tags));
        ThrowIfDisposed();

        var members = tags.SelectMany(t => t.Members(m => m.Value.IsAtomic())).ToList();
        var tasks = members.Select(m => (Member: m, Write: WriteTagValue(m, token))).ToList();

        if (tasks.Count == 0)
            return TagResponse.NoData(members.Select(t => t.TagName).ToArray());

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks.Select(t => t.Write));
        timer.Stop();

        var results = tasks.Select(t => (t.Member.TagName, t.Write.Result)).ToList();
        return TagResponse.Aggregate(results, timer.Elapsed); 
    }

    /// <inheritdoc />
    public ITagWatch WatchTags(ICollection<Tag> tags)
    {
        ThrowIfDisposed();

        if (tags is null)
            throw new ArgumentNullException(nameof(tags));

        var watch = new TagWatch(tags, _uri, _options);
        _watches.Add(watch);
        return watch;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed) return;

        foreach (var watch in _watches)
        {
            watch.Dispose();
        }

        foreach (var handle in _handles.Values)
        {
            var result = _tagService.Destroy(handle.TagId).AsResult();
            TagException.ThrowIfRequested(result, _options.ThrowOn);
            Marshal.FreeHGlobal(handle.TagPtr);
        }

        _handles.Clear();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Reads the value of a specified tag from the PLC. The provided tag is expected to be an atomic type tag
    /// that has public access. If not, we might encounter an error, which we either throw or return depending on
    /// the configured options.
    /// </summary>
    private async Task<TagStatus> ReadTagValue(Tag tag, CancellationToken token)
    {
        var tagName = tag.DetermineTagName();
        var handle = await GetOrCreateHandle(tagName, token);

        var result = (await ExecuteAsync(tagName, () => _tagService.Read(handle, AsyncTimeout), token)).AsResult();
        TagException.ThrowIfRequested(result, _options.ThrowOn);

        if (result == TagStatus.Ok)
        {
            _tagBuffer.ReadValue(tag, handle);
        }

        return result;
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
        var tagName = tag.DetermineTagName();
        var handle = await GetOrCreateHandle(tagName, token);

        var written = _tagBuffer.WriteValue(tag, handle).AsResult();
        if (written < 0)
        {
            TagException.ThrowIfRequested(written, _options.ThrowOn);
            return written;
        }

        var result = (await ExecuteAsync(tagName, () => _tagService.Write(handle, AsyncTimeout), token)).AsResult();
        TagException.ThrowIfRequested(result, _options.ThrowOn);
        return result;
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
            TagException.ThrowIfRequested(tagId.AsResult(), _options.ThrowOn, tagName);
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

            // If not pending, we can return without awaiting the task.
            //todo still not 100% sure about this. Might still want to wait.
            if (status < 0) return status;

            // Emulate the timeout with a linked cancellation for the specified timeout.
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(token);
            cts.CancelAfter(_options.Timeout);

            // When canceled, call the abort operation to stop and remove the task from the pending lookup.
            using (cts.Token.Register(() => AbortOperation(tagName, token)))
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
            var result = _tagService.Abort(handle.TagId).AsResult();
            TagException.ThrowIfRequested(result, _options.ThrowOn);
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
        // We only want to process events that are "completed" and not pending or starting,
        // and that have the required user data tag pointer.
        if (!TagEvent.IsComplete(eventId) || statusId.AsResult() == TagStatus.Pending || userData == IntPtr.Zero)
            return;

        // Get the tag name from the pinned memory location.
        var tagName = Marshal.PtrToStringAnsi(userData);

        // At this point we should attempt to "dequeue" the operation. If we fail, then something went wrong.
        if (!_operations.TryRemove(tagName, out var operation))
            return;

        switch (eventId)
        {
            // For a successful create operation, we want to set the result to the returned tag handle.
            case TagEvent.Created when statusId == 0:
                operation.TrySetResult(handle);
                break;
            // For all other operations/cases, we want the set the result to the returned status.
            default:
                operation.TrySetResult(statusId);
                break;
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
}