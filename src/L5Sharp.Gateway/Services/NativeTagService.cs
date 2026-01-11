using System;
using System.Text;
using L5Sharp.Gateway.Abstractions;
using libplctag.NativeImport;

namespace L5Sharp.Gateway.Services;

/// <summary>
/// Represents a service for interacting with native tags in the system. This class provides methods for creating,
/// reading, writing, locking, unlocking, and managing attributes of tags. It also allows registering custom
/// callbacks for events and logging functionality.
/// </summary>
public class NativeTagService : ITagService
{
    private readonly plctag.callback_func_ex _onEvent;

    /// <summary>
    /// Initializes a new instance of the <see cref="NativeTagService"/> class with optional callbacks for events and logging.
    /// </summary>
    /// <param name="onEvent">Optional callback function invoked for tag events. Receives tag ID, event type, and status code.</param>
    public NativeTagService(Action<int, int, int, IntPtr>? onEvent)
    {
        // Pinned delegates to prevent GC collection
        _onEvent = new plctag.callback_func_ex(onEvent ?? ((_, _, _, _) => { }));
    }

    /// <inheritdoc />
    public int Abort(int handle)
    {
        return plctag.plc_tag_abort(handle);
    }

    /// <inheritdoc />
    public int Create(string path, Action<int, int, int, IntPtr> callback, IntPtr userData, int timeout)
    {
        // This implementation uses the callback provided in the constructor because libplctag requires the delegate
        // be pinned to prevent GC reclaiming the reference.
        return plctag.plc_tag_create_ex(path, _onEvent, userData, timeout);
    }

    /// <inheritdoc />
    public int Read(int handle, int timeout)
    {
        return plctag.plc_tag_read(handle, timeout);
    }

    /// <inheritdoc />
    public int Write(int handle, int timeout)
    {
        return plctag.plc_tag_write(handle, timeout);
    }

    /// <inheritdoc />
    public int Status(int handle)
    {
        return plctag.plc_tag_status(handle);
    }

    /// <inheritdoc />
    public int Destroy(int handle)
    {
        return plctag.plc_tag_destroy(handle);
    }

    /// <inheritdoc />
    public int SetAttribute(int handle, string attributeName, int newValue)
    {
        return plctag.plc_tag_set_int_attribute(handle, attributeName, newValue);
    }

    /// <inheritdoc />
    public sbyte GetSByte(int handle, int offset)
    {
        return plctag.plc_tag_get_int8(handle, offset);
    }

    /// <inheritdoc />
    public short GetShort(int handle, int offset)
    {
        return plctag.plc_tag_get_int16(handle, offset);
    }

    /// <inheritdoc />
    public int GetInt(int handle, int offset)
    {
        return plctag.plc_tag_get_int32(handle, offset);
    }

    /// <inheritdoc />
    public long GetLong(int handle, int offset)
    {
        return plctag.plc_tag_get_int64(handle, offset);
    }

    /// <inheritdoc />
    public float GetFloat(int handle, int offset)
    {
        return plctag.plc_tag_get_float32(handle, offset);
    }

    /// <inheritdoc />
    public double GetDouble(int handle, int offset)
    {
        return plctag.plc_tag_get_float64(handle, offset);
    }

    /// <inheritdoc />
    public byte GetByte(int handle, int offset)
    {
        return plctag.plc_tag_get_uint8(handle, offset);
    }

    /// <inheritdoc />
    public ushort GetUShort(int handle, int offset)
    {
        return plctag.plc_tag_get_uint16(handle, offset);
    }

    /// <inheritdoc />
    public uint GetUInt(int handle, int offset)
    {
        return plctag.plc_tag_get_uint32(handle, offset);
    }

    /// <inheritdoc />
    public ulong GetULong(int handle, int offset)
    {
        return plctag.plc_tag_get_uint64(handle, offset);
    }

    /// <inheritdoc />
    public string GetString(int handle, int offset)
    {
        var length = plctag.plc_tag_get_string_length(handle, offset);
        if (length < 0) return string.Empty;

        var builder = new StringBuilder(length);
        var result = plctag.plc_tag_get_string(handle, offset, builder, length);

        return result == 0 ? builder.ToString() : string.Empty;
    }

    /// <inheritdoc />
    public int SetSByte(int handle, int offset, sbyte value)
    {
        return plctag.plc_tag_set_int8(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetShort(int handle, int offset, short value)
    {
        return plctag.plc_tag_set_int16(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetInt(int handle, int offset, int value)
    {
        return plctag.plc_tag_set_int32(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetLong(int handle, int offset, long value)
    {
        return plctag.plc_tag_set_int64(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetFloat(int handle, int offset, float value)
    {
        return plctag.plc_tag_set_float32(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetDouble(int handle, int offset, double value)
    {
        return plctag.plc_tag_set_float64(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetByte(int handle, int offset, byte value)
    {
        return plctag.plc_tag_set_uint8(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetUShort(int handle, int offset, ushort value)
    {
        return plctag.plc_tag_set_uint16(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetUInt(int handle, int offset, uint value)
    {
        return plctag.plc_tag_set_uint32(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetULong(int handle, int offset, ulong value)
    {
        return plctag.plc_tag_set_uint64(handle, offset, value);
    }

    /// <inheritdoc />
    public int SetString(int handle, int offset, string value)
    {
        return plctag.plc_tag_set_string(handle, offset, value);
    }
}