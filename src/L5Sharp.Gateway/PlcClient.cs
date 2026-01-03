using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
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
    private readonly ConcurrentDictionary<TagName, int> _handles = [];

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
    }

    /// <inheritdoc />
    public Task<bool> PingAsync(CancellationToken token = default)
    {
        return Task.Run(async () =>
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(_ip).ConfigureAwait(false);
            return reply.Status == IPStatus.Success;
        }, token);
    }

    /// <inheritdoc />
    public Task<TagStatus> ReadAsync(Tag tag, CancellationToken token = default)
    {
        if (tag is null)
            throw new ArgumentNullException(nameof(tag));

        return Task.Run(() =>
        {
            var tagName = tag.DetermineTagName();
            var handle = _handles.GetOrAdd(tagName, CreateTagHandle);
            
            if (handle <= 0)
            {
                return new TagStatus(0, tag.TagName, handle.AsResult());
            }
            

            using (token.Register(() => AbortOperation(handle)))
            {
                var result = plctag.plc_tag_read(handle, _options.Timeout).AsResult();
                TagException.ThrowIfRequested(result, _options.ThrowOn);
                return tag.Refresh(handle);
            }
        }, token);
    }

    /// <inheritdoc />
    public Task<TagStatus> WriteAsync(Tag tag, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ITagWatch CreateWatch(ICollection<Tag> tags, string? name = null)
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
            var result = plctag.plc_tag_destroy(handle).AsResult();
            TagException.ThrowIfRequested(result, _options.ThrowOn);
        }

        _handles.Clear();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Creates a new handle for a PLC tag by constructing a route using the
    /// base URI and tag name and invoking the corresponding native library method.
    /// </summary>
    /// <param name="tagName">The name of the tag for which the handle should be created. This is appended to the base URI to form the route.</param>
    /// <returns>An integer representing the handle for the specified tag. A value less than zero indicates an error in handle creation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the handle creation fails and the result is less than zero.</exception>
    private int CreateTagHandle(TagName tagName)
    {
        var route = $"{_baseUri}&name={tagName}";
        var result = plctag.plc_tag_create(route, _options.Timeout).AsResult();
        TagException.ThrowIfRequested(result, _options.ThrowOn, tagName);
        return (int)result;
    }

    /// <summary>
    /// Aborts the operation associated with the specified handle.
    /// </summary>
    /// <param name="handle">The handle of the tag operation to abort.</param>
    /// <exception cref="TagException">Thrown if the abort operation fails based on the provided exception codes.</exception>
    private void AbortOperation(int handle)
    {
        var result = plctag.plc_tag_abort(handle).AsResult();
        TagException.ThrowIfRequested(result, _options.ThrowOn);
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