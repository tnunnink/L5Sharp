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
        private CancellationTokenSource? _autoSync;

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
        public void CancelOperation(TagStatus status)
        {
            Status = status;
            _pendingOperation?.Cancel();
            _pendingOperation = null;
        }

        /// <summary>
        /// Notifies the tag store of an event or status change, updating the tag's status and invoking the associated callback.
        /// </summary>
        /// <param name="eventId">The identifier of the event that triggered the notification.</param>
        /// <param name="statusId">The current status identifier of the tag, used to update its status.</param>
        public void Notify(int eventId, TagStatus statusId)
        {
            Status = statusId;

            if (eventId > 0)
            {
                callback(Handle, eventId, statusId.AsValue(), userData);
            }
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
            tag.Value.UpdateData(Buffer, 0);
        }

        /// <summary>
        /// Configures and starts an automatic synchronization operation for the tag at a specified interval.
        /// Cancels any existing synchronization operation before starting the new one. The synchronization process
        /// periodically performs read operations on the tag and simulates completion using the provided
        /// delegate function.
        /// </summary>
        /// <param name="interval">
        /// The time interval, in milliseconds, between each synchronization operation. If the interval is less than
        /// or equal to zero, synchronization will not be started.
        /// </param>
        /// <param name="simulate">
        /// A delegate function that simulates the synchronization operation. The function is invoked
        /// with an action to execute during synchronization and a cancellation token for aborting the operation if required.
        /// </param>
        public void SetAutoSync(int interval, Func<Action, CancellationToken, Task> simulate)
        {
            _autoSync?.Cancel();
            if (interval <= 0) return;

            _autoSync = new CancellationTokenSource();
            var token = _autoSync.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(interval, token);

                    Notify(TagEvent.ReadStarted, TagStatus.Pending);

                    await simulate(() =>
                    {
                        ReadBytes();
                        Notify(TagEvent.ReadCompleted, TagStatus.Ok);
                    }, token);
                }
            }, token);
        }
    }

    /// <summary>
    /// A private field that acts as the primary data storage for the VirtualTagService.
    /// It is responsible for maintaining and managing tag instances, enabling operations
    /// such as reading, writing, and configuration of tag attributes.
    /// </summary>
    private readonly L5X _storage;

    /// <summary>
    /// A private field that serves as an in-memory store for tag handles and their associated metadata.
    /// This field leverages a thread-safe dictionary to enable efficient retrieval, addition, and modification
    /// of tag-related data, which is crucial for operations such as creation, reading, writing, and destruction
    /// of virtual tags within the service.
    /// </summary>
    private readonly ConcurrentDictionary<int, TagStore> _memory = [];

    /// <summary>
    /// A private field used to generate unique handles for tag operations within the <see cref="VirtualTagService"/>.
    /// It serves as a counter, incrementing with each new handle creation to maintain distinct identifiers
    /// for tag-related operations such as creation, reading, writing, and destruction.
    /// </summary>
    /// <remarks>
    /// It seems like the handles start at 11 and increment from there.
    /// Set to 10 and increment on the first creation.
    /// </remarks>
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
    private VirtualTagService(L5X storage, TimeSpan? latency = null)
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

        store.CancelOperation(TagStatus.Abort);
        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int Create(string path, Action<int, int, int, IntPtr> callback, IntPtr userData, int timeout)
    {
        if (!TryExtractName(path, out var tagName))
            return TagStatus.BadParam.AsValue();

        if (!_storage.TryGet<Tag>(tagName, out var tag))
            return TagStatus.NotFound.AsValue();

        var store = new TagStore(++_handleCounter, tag, callback, userData);
        var token = store.StartOperation();

        var work = SimulateLatency(() =>
        {
            if (!_memory.TryAdd(store.Handle, store))
            {
                store.Notify(TagEvent.None, TagStatus.CreateErr);
                return;
            }

            store.Notify(TagEvent.Created, TagStatus.Ok);

            //libplctag will read once after creating the handle to get data.
            store.ReadBytes();
            store.Notify(TagEvent.ReadCompleted, TagStatus.Ok);
        }, token);

        // Return without waiting for the operation to complete (this is how libplctag works)
        if (timeout == 0) return store.Handle;

        try
        {
            if (work.Wait(timeout, token)) return store.Handle;
            store.CancelOperation(TagStatus.Timeout);
            return store.Status.AsValue();
        }
        catch (OperationCanceledException)
        {
            // If canceled from the abort method, then the status should be set to abort.
            return store.Status.AsValue();
        }
    }

    /// <inheritdoc />
    public int Read(int handle, int timeout)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        var token = store.StartOperation();
        store.Notify(TagEvent.ReadStarted, TagStatus.Pending);

        var work = SimulateLatency(() =>
        {
            store.ReadBytes();
            store.Notify(TagEvent.ReadCompleted, TagStatus.Ok);
        }, token);

        if (timeout == 0) return TagStatus.Pending.AsValue();

        try
        {
            if (work.Wait(timeout, token)) return store.Status.AsValue();
            store.CancelOperation(TagStatus.Timeout);
            return store.Status.AsValue();
        }
        catch (OperationCanceledException)
        {
            // If canceled from the abort method, then the status should be set to abort.
            return store.Status.AsValue();
        }
    }

    /// <inheritdoc />
    public int Write(int handle, int timeout)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        var token = store.StartOperation();
        store.Notify(TagEvent.WriteStarted, TagStatus.Pending);

        var work = SimulateLatency(() =>
        {
            store.WriteBytes();
            store.Notify(TagEvent.WriteCompleted, TagStatus.Ok);
        }, token);

        if (timeout == 0) return TagStatus.Pending.AsValue();

        try
        {
            if (work.Wait(timeout, token)) return store.Status.AsValue();
            store.CancelOperation(TagStatus.Timeout);
            return store.Status.AsValue();
        }
        catch (OperationCanceledException)
        {
            // If canceled from the abort method, then the status should be set to abort.
            return store.Status.AsValue();
        }
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

        store.CancelOperation(TagStatus.Abort);

        return TagStatus.Ok.AsValue();
    }

    /// <inheritdoc />
    public int SetAttribute(int handle, string attributeName, int newValue)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (attributeName == "auto_sync_read_ms")
        {
            store.SetAutoSync(newValue, SimulateLatency);
        }

        return TagStatus.Ok.AsValue();
    }

    /// <inheritdoc />
    public sbyte GetSByte(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return sbyte.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(sbyte))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return sbyte.MinValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return (sbyte)store.Buffer[offset];
    }

    /// <inheritdoc />
    public short GetShort(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return short.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(short))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return short.MinValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToInt16(store.Buffer, offset);
    }

    /// <inheritdoc />
    public int GetInt(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return int.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(int))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return int.MinValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToInt32(store.Buffer, offset);
    }

    /// <inheritdoc />
    public long GetLong(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return long.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(long))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return long.MinValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToInt64(store.Buffer, offset);
    }

    /// <inheritdoc />
    public float GetFloat(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return float.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(float))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return float.MinValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToSingle(store.Buffer, offset);
    }

    /// <inheritdoc />
    public double GetDouble(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return double.MinValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(double))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return double.MinValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToDouble(store.Buffer, offset);
    }

    /// <inheritdoc />
    public byte GetByte(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return byte.MaxValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(byte))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return byte.MaxValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return store.Buffer[offset];
    }

    /// <inheritdoc />
    public ushort GetUShort(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return ushort.MaxValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(ushort))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return ushort.MaxValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToUInt16(store.Buffer, offset);
    }

    /// <inheritdoc />
    public uint GetUInt(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return uint.MaxValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(uint))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return uint.MaxValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToUInt32(store.Buffer, offset);
    }

    /// <inheritdoc />
    public ulong GetULong(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return ulong.MaxValue;

        if (offset < 0 || store.Buffer.Length < offset + sizeof(ulong))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return ulong.MaxValue;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return BitConverter.ToUInt64(store.Buffer, offset);
    }

    /// <inheritdoc />
    public string GetString(int handle, int offset)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return string.Empty;

        var bytes = store.Buffer;

        // Read the LEN (first 4 bytes)
        var length = BitConverter.ToInt32(bytes, offset);

        // Ensure we don't read past the end of the actual buffer
        var readLength = Math.Min(length, store.Buffer.Length - (offset + 4));
        if (readLength <= 0)
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return string.Empty;
        }

        store.Notify(TagEvent.None, TagStatus.Ok);
        return Encoding.ASCII.GetString(bytes, offset + 4, readLength);
    }

    /// <inheritdoc />
    public int SetSByte(int handle, int offset, sbyte value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(sbyte))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            Array.Copy(new[] { (byte)value }, 0, store.Buffer, offset, sizeof(sbyte));
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetShort(int handle, int offset, short value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(short))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetInt(int handle, int offset, int value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(int))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetLong(int handle, int offset, long value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(long))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetFloat(int handle, int offset, float value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(float))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetDouble(int handle, int offset, double value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(double))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetByte(int handle, int offset, byte value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(byte))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            Array.Copy(new[] { value }, 0, store.Buffer, offset, sizeof(byte));
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetUShort(int handle, int offset, ushort value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(ushort))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetUInt(int handle, int offset, uint value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(uint))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetULong(int handle, int offset, ulong value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        if (offset < 0 || store.Buffer.Length < offset + sizeof(ulong))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
        }
        else
        {
            BitConverter.GetBytes(value).CopyTo(store.Buffer, offset);
            store.Notify(TagEvent.None, TagStatus.Ok);
        }

        return store.Status.AsValue();
    }

    /// <inheritdoc />
    public int SetString(int handle, int offset, string value)
    {
        if (!_memory.TryGetValue(handle, out var store))
            return TagStatus.NotFound.AsValue();

        // 1. Write the length (LEN) as a 4-byte DINT
        var length = BitConverter.GetBytes(value.Length);
        if (offset < 0 || store.Buffer.Length < offset + sizeof(int))
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return store.Status.AsValue();
        }

        length.CopyTo(store.Buffer, offset);

        // 2. Write the character bytes (DATA) using ASCII encoding
        var bytes = Encoding.ASCII.GetBytes(value);
        var writeLength = Math.Min(bytes.Length, store.Buffer.Length - (offset + 4));
        if (writeLength <= 0)
        {
            store.Notify(TagEvent.None, TagStatus.OutOfBounds);
            return store.Status.AsValue();
        }

        Array.Copy(bytes, 0, store.Buffer, offset + 4, writeLength);
        store.Notify(TagEvent.None, TagStatus.Ok);
        return store.Status.AsValue();
    }

    /// <summary>
    /// Simulates a network or processing latency by introducing a delay before executing the specified action.
    /// The delay duration is defined by the internal latency setting of the service.
    /// </summary>
    /// <param name="onComplete">An <see cref="Action"/> delegate to execute after the delay completes.</param>
    /// <param name="token">A <see cref="CancellationToken"/> that can be used to cancel the delay or execution of the action.</param>
    private Task SimulateLatency(Action onComplete, CancellationToken token)
    {
        return Task.Run(async () =>
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