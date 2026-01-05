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
using libplctag.NativeImport;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Gateway;

/// <inheritdoc cref="IPlcClient" />
public class PlcClient : IPlcClient
{
    /// <summary>
    /// Indicates whether the current instance of <see cref="PlcClient"/> has been disposed.
    /// This variable is used to track whether resources associated with the object have been released
    /// to prevent further usage and manage object lifecycle effectively.
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// A delegate used as a callback function for handling PLC tag-related events.
    /// This delegate is invoked by the underlying <see cref="libplctag.NativeImport.plctag"/> library
    /// when a specific event occurs, facilitating communication between the library and the client.
    /// </summary>
    private readonly plctag.callback_func_ex _tagEventCallback;

    /// <summary>
    /// Specifies the timeout duration, in milliseconds, for asynchronous operations initiated by the <see cref="PlcClient"/>.
    /// When libplctag functions are executed with a 0ms timeout, they will return immediately, and we will use the tag
    /// event callback to update the result of the operation. 
    /// </summary>
    private const int AsyncTimeout = 0;

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
    private readonly string _baseUri;

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
    /// <exception cref="ArgumentException">Thrown when <paramref name="ip"/> is not a valid IP.</exception>
    public PlcClient(string ip, ushort slot = 0, PlcOptions? options = null)
    {
        if (!IPAddress.TryParse(ip, out var address))
            throw new ArgumentException($"Unable to parse IP address: {ip}");

        _ip = address;
        _options = options ?? new PlcOptions();
        _baseUri = $"protocol=ab_eip&gateway={_ip}&path=1,{slot}&plc=controllogix";
        _tagEventCallback = TagEventCallback;
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
        if (tag is null)
            throw new ArgumentNullException(nameof(tag));

        ThrowIfDisposed();

        var results = new List<(TagName, TagResult)>();
        var members = tag.Members(t => LogixType.IsAtomic(t.DataType)).ToList();
        var tasks = members.Select(m => (Member: m, Read: ReadTagValue(m, token))).ToList();

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks.Select(t => t.Read));
        timer.Stop();

        foreach (var task in tasks)
        {
            results.Add((task.Member.TagName, task.Read.Result));
        }

        return TagResponse.Aggregate(tag, results);
    }


    /// <inheritdoc />
    public Task<TagResponse> WriteTag(Tag tag, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ITagWatch WatchTags(ICollection<Tag> tags)
    {
        ThrowIfDisposed();

        if (tags is null)
            throw new ArgumentNullException(nameof(tags));

        var watch = new TagWatch(tags, _baseUri, _options);
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
            //todo should this be awaited? WE are not in a task here.
            var result = plctag.plc_tag_destroy(handle.TagId).AsResult();
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
    private async Task<TagResult> ReadTagValue(Tag tag, CancellationToken token)
    {
        var tagName = tag.DetermineTagName();
        var handle = await GetOrCreateHandle(tagName, token);

        // Execute the create function in asynchronously.
        var result = (await ExecuteAsync(tagName, () => plctag.plc_tag_read(handle, AsyncTimeout), token)).AsResult();

        TagException.ThrowIfRequested(result, _options.ThrowOn);

        if (result == TagResult.Ok)
            tag.ReadValue(handle);

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
        // Get cached handle if exists to avoid recreating (most expensive part of the operation according to libplctag)
        if (_handles.TryGetValue(tagName, out var cached))
            return cached.TagId;

        // Build the Uri route to the PLC tag.
        var route = $"{_baseUri}&name={tagName}";

        // Pass the tag name string as the unmanaged pointer.
        // This user data will be available in the event callback, which is needed for lookup.
        var tagPtr = Marshal.StringToHGlobalAnsi(tagName);

        // Execute the create function asynchronously.
        var tagId = await ExecuteAsync(tagName,
            () => plctag.plc_tag_create_ex(route, _tagEventCallback, tagPtr, AsyncTimeout),
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
        _handles.TryAdd(tagName, new TagHandle(tagId, tagPtr));
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
                pending.TrySetException(new TagException(TagResult.Timeout, tagName));
        }

        if (_handles.TryGetValue(tagName, out var handle))
        {
            var result = plctag.plc_tag_abort(handle.TagId).AsResult();
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
        if (!TagEvent.IsComplete(eventId) || statusId.AsResult() == TagResult.Pending || userData == IntPtr.Zero)
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