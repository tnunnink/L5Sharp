using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;
using L5Sharp.Gateway.Internal;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Gateway.Services;

/// <summary>
/// A mock implementation of the <see cref="ITagService"/> interface used for testing or simulation purposes.
/// Provides various methods for managing and manipulating tag data, including creating, reading, writing,
/// and configuring attributes of tags.
/// </summary>
public class VirtualTagService : ITagService
{
    /// <summary>
    /// Represents an internal data structure used for managing tags within the context of a virtual tag service.
    /// Encapsulates information about a tag, its status, associated attributes, and optional callback details.
    /// Provides methods for handling notification events triggered by changes or operations on the tag.
    /// </summary>
    private class TagStore(int handle, Tag tag, Action<int, int, int, IntPtr> callback, IntPtr userData)
    {
        private CancellationTokenSource? _pendingOperation;

        public int Handle { get; } = handle;
        public TagStatus Status { get; private set; } = TagStatus.Ok;
        public byte[] Buffer { get; private set; } = [];

        /// <summary>
        /// Initiates a new operation for the current tag instance, setting its status to pending and
        /// canceling any previously running operation. Returns a cancellation token associated with the new operation.
        /// </summary>
        /// <returns>
        /// A <see cref="CancellationToken"/> that can be used to monitor or cancel the newly started operation.
        /// </returns>
        public CancellationToken StartOperation()
        {
            Status = TagStatus.Pending;
            _pendingOperation?.Cancel();
            _pendingOperation = new CancellationTokenSource();
            return _pendingOperation.Token;
        }

        /// <summary>
        /// Cancels any currently active or pending operation for the tag associated with this instance,
        /// changing its status to indicate the operation was aborted.
        /// </summary>
        public void CancelOperation()
        {
            Status = TagStatus.Abort;
            _pendingOperation?.Cancel();
            _pendingOperation = null;
        }

        /// <summary>
        /// Notifies the tag service of an event or status change, updating the tag's status and invoking the associated callback.
        /// </summary>
        /// <param name="eventId">The identifier of the event that triggered the notification.</param>
        /// <param name="statusId">The current status identifier of the tag, used to update its status.</param>
        public void Notify(int eventId, TagStatus statusId)
        {
            Status = statusId;
            callback(Handle, eventId, statusId.AsValue(), userData);
        }

        /// <summary>
        /// Converts the tag's value into a byte array and updates the internal data storage of the tag instance.
        /// This method retrieves the value of the associated tag, transforms it into a byte array, and assigns
        /// the resulting byte array to the internal `Data` property of the tag.
        /// </summary>
        public void ReadBytes()
        {
            Buffer = tag.Value.ToBytes();
        }

        /// <summary>
        /// Writes the contents of the current buffer to the associated tag's value, starting at the specified offset.
        /// This operation updates the tag with the data stored in the buffer to ensure it reflects the latest state.
        /// </summary>
        public void WriteBytes()
        {
            tag.Value.Update(Buffer, 0);
        }
    }

    /// <summary>
    /// A private field used to store an instance of the L5X class, which represents the internal state or data
    /// required by the MockTagService for managing and processing tags.
    /// </summary>
    private readonly L5X _storage;

    /// <summary>
    /// A private field that serves as an in-memory storage for managing and tracking tags within the MockTagService.
    /// It uses a thread-safe dictionary to store tag instances, allowing retrieval and modification by tag identifiers.
    /// </summary>
    private readonly ConcurrentDictionary<int, TagStore> _memory = [];

    /// <summary>
    /// It seems like the handles start at 11 and increment from there. Set to 10 and increment on the first create.
    /// </summary>
    private int _handleCounter = 10;

    /// <summary>
    /// A private field representing the simulated latency for operations in the <see cref="VirtualTagService"/>.
    /// It is used to mimic a delay in processing requests, simulating real-world conditions in testing or simulation.
    /// </summary>
    private readonly TimeSpan _latency;


    /// <summary>
    /// Provides a virtual implementation of the <see cref="ITagService"/> interface, allowing for the management
    /// and manipulation of tags in a simulated environment. This class is typically used for testing or simulation purposes.
    /// </summary>
    public VirtualTagService(L5X storage, TimeSpan? latency = null)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _latency = latency ?? TimeSpan.FromMilliseconds(50);
    }

    /// <summary>
    /// Creates and initializes a new instance of <see cref="ITagService"/> using the specified file path and optional latency.
    /// The service is built based on the provided L5X file.
    /// </summary>
    /// <param name="filePath">The file path to the L5X file containing the tag definitions.</param>
    /// <param name="latency">An optional latency configuration for the service. Defaults to <c>null</c>.</param>
    /// <returns>
    /// An instance of <see cref="ITagService"/> initialized with the tag definitions from the specified L5X file.
    /// </returns>
    public static ITagService Upload(string filePath, TimeSpan? latency = null)
    {
        var storage = L5X.Load(filePath);
        return new VirtualTagService(storage, latency);
    }

    /// <inheritdoc />
    public int Abort(int handle)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        store.CancelOperation();

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int Create(string path, Action<int, int, int, IntPtr> callback, IntPtr userData, int timeout)
    {
        if (!TryExtractName(path, out var tagName))
            return TagStatus.BadParam.AsValue();

        //todo this does not account for the formatting of tag names in libplctag
        // which is can be Program:ProgramName.TagName (should this be supported by core?)
        if (!_storage.TryGet<Tag>(tagName, out var tag))
            return TagStatus.NotFound.AsValue();

        var store = new TagStore(++_handleCounter, tag, callback, userData);
        var token = store.StartOperation();

        if (!_memory.TryAdd(store.Handle, store))
            return TagStatus.CreateErr.AsValue();

        SimulateLatency(() => { store.Notify(TagEvent.Created, TagStatus.Ok); }, token);

        return store.Handle;
    }

    /// <inheritdoc />
    public string Decode(int error)
    {
        // might have to manually map these
        return string.Empty;
    }

    /// <inheritdoc />
    public int Read(int handle, int timeout)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return (int)TagStatus.NotFound;

        var token = store.StartOperation();

        store.Notify(TagEvent.ReadStarted, TagStatus.Pending);
        store.ReadBytes();

        SimulateLatency(() => { store.Notify(TagEvent.ReadCompleted, TagStatus.Ok); }, token);

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int Write(int handle, int timeout)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return (int)TagStatus.NotFound;

        var token = store.StartOperation();

        store.Notify(TagEvent.WriteStarted, TagStatus.Pending);
        store.WriteBytes();

        SimulateLatency(() => { store.Notify(TagEvent.WriteCompleted, TagStatus.Ok); }, token);

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int Status(int handle)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int Destroy(int handle)
    {
        if (!_memory.TryRemove(handle, out var store))
            return TagStatus.NotFound.AsValue();

        store.CancelOperation();

        return TagStatus.Ok.AsValue();
    }

    /// <inheritdoc />
    public int GetAttribute(int handle, string attributeName, int defaultValue)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetAttribute(int handle, string attributeName, int newValue)
    {
        /*if (attributeName != "auto_sync_read_ms")
            return TagStatus.Ok.AsValue();

        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        store.EnableAutoSync(newValue);
        return TagStatus.Ok.AsValue();*/

        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetBit(int handle, int offsetBit)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return sbyte.MinValue;

        if (offsetBit < 0 || store.Buffer.Length < offsetBit + sizeof(sbyte))
        {
            //todo not sure how to emulate what the library does yet.
            //store.Status = TagStatus.OutOfBounds;
            return sbyte.MinValue;
        }

        return (sbyte)store.Buffer[offsetBit];
    }

    /// <inheritdoc />
    public sbyte GetSByte(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return sbyte.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(sbyte))
        {
            //todo not sure how to emulate what the library does yet.
            //store.Status = TagStatus.OutOfBounds;
            return sbyte.MinValue;
        }

        return (sbyte)store.Buffer[offset];
    }

    /// <inheritdoc />
    public short GetShort(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return short.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(short))
        {
            //todo not sure how to emulate what the library does yet.
            //store.Status = TagStatus.OutOfBounds;
            return short.MinValue;
        }

        return BitConverter.ToInt16(store.Buffer, offset);
    }

    /// <inheritdoc />
    public int GetInt(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public long GetLong(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public float GetFloat(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public double GetDouble(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public byte GetByte(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ushort GetUShort(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public uint GetUInt(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ulong GetULong(int handle, int offSet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetRawBytes(int handle, int start, byte[] buffer, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetStringLength(int handle, int offset)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetString(int handle, int offset, StringBuilder buffer, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetStringTotalLength(int handle, int offset)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetStringCapacity(int handle, int offset)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetBit(int handle, int offSetBit, int val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetSByte(int handle, int offSet, sbyte val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetShort(int handle, int offSet, short val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetInt(int handle, int offSet, int val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetLong(int handle, int offSet, long val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetFloat(int handle, int offSet, float val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetDouble(int handle, int offSet, double val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetByte(int handle, int offSet, byte val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetUShort(int handle, int offSet, ushort val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetUInt(int handle, int offSet, uint val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetULong(int handle, int offSet, ulong val)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetRawBytes(int handle, int offset, byte[] buffer, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SetString(int handle, int offset, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Simulates a network or processing latency by introducing a delay before executing the specified action.
    /// The delay duration is defined by the internal latency setting of the service.
    /// </summary>
    /// <param name="onComplete">An <see cref="Action"/> delegate to execute after the delay completes.</param>
    /// <param name="token">A <see cref="CancellationToken"/> that can be used to cancel the delay or execution of the action.</param>
    private void SimulateLatency(Action onComplete, CancellationToken token)
    {
        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(_latency, token);
                onComplete();
            }
            catch (OperationCanceledException)
            {
            }
        }, token);
    }

    /// <summary>
    /// Attempts to extract a tag name from the given path. If the extraction succeeds, the tag name
    /// will be output through the <paramref name="tagName"/> parameter, otherwise the method will return
    /// an error status.
    /// </summary>
    /// <param name="path">The input path string from which the tag name will be extracted.</param>
    /// <param name="tagName">
    /// When the method returns successfully, this parameter will contain the extracted tag name.
    /// If the extraction fails, this parameter will be set to null.
    /// </param>
    /// <returns>
    /// An integer representing the status of the operation. Possible values include success status or error
    /// codes from <see cref="TagStatus"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="path"/> parameter is null.</exception>
    private static bool TryExtractName(string path, out TagName tagName)
    {
        var attributes = path.Split('&')
            .Select(a => a.Split('='))
            .Where(pair => pair.Length == 2)
            .ToDictionary(pair => pair[0], pair => pair[1]);

        if (attributes.TryGetValue("name", out var name))
        {
            tagName = new TagName(name);
            return true;
        }

        tagName = null!;
        return false;
    }
}