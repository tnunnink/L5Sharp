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
    public string Decode(int error)
    {
        return plctag.plc_tag_decode_error(error);
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
    public int GetAttribute(int handle, string attributeName, int defaultValue)
    {
        return plctag.plc_tag_get_int_attribute(handle, attributeName, defaultValue);
    }

    /// <inheritdoc />
    public int SetAttribute(int handle, string attributeName, int newValue)
    {
        return plctag.plc_tag_set_int_attribute(handle, attributeName, newValue);
    }

    /// <inheritdoc />
    public int GetBit(int handle, int offsetBit)
    {
        return plctag.plc_tag_get_bit(handle, offsetBit);
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
    public int GetInt(int handle, int offSet)
    {
        return plctag.plc_tag_get_int32(handle, offSet);
    }

    /// <inheritdoc />
    public long GetLong(int handle, int offSet)
    {
        return plctag.plc_tag_get_int64(handle, offSet);
    }

    /// <inheritdoc />
    public float GetFloat(int handle, int offSet)
    {
        return plctag.plc_tag_get_float32(handle, offSet);
    }

    /// <inheritdoc />
    public double GetDouble(int handle, int offSet)
    {
        return plctag.plc_tag_get_float64(handle, offSet);
    }

    /// <inheritdoc />
    public byte GetByte(int handle, int offSet)
    {
        return plctag.plc_tag_get_uint8(handle, offSet);
    }

    /// <inheritdoc />
    public ushort GetUShort(int handle, int offSet)
    {
        return plctag.plc_tag_get_uint16(handle, offSet);
    }

    /// <inheritdoc />
    public uint GetUInt(int handle, int offSet)
    {
        return plctag.plc_tag_get_uint32(handle, offSet);
    }

    /// <inheritdoc />
    public ulong GetULong(int handle, int offSet)
    {
        return plctag.plc_tag_get_uint64(handle, offSet);
    }

    /// <inheritdoc />
    public int GetRawBytes(int handle, int start, byte[] buffer, int length)
    {
        return plctag.plc_tag_get_raw_bytes(handle, start, buffer, length);
    }

    /// <inheritdoc />
    public int GetStringLength(int handle, int offset)
    {
        return plctag.plc_tag_get_string_length(handle, offset);
    }

    /// <inheritdoc />
    public int GetString(int handle, int offset, StringBuilder buffer, int length)
    {
        return plctag.plc_tag_get_string(handle, offset, buffer, length);
    }

    /// <inheritdoc />
    public int GetStringTotalLength(int handle, int offset)
    {
        return plctag.plc_tag_get_string_total_length(handle, offset);
    }

    /// <inheritdoc />
    public int GetStringCapacity(int handle, int offset)
    {
        return plctag.plc_tag_get_string_capacity(handle, offset);
    }

    /// <inheritdoc />
    public int SetBit(int handle, int offSetBit, int val)
    {
        return plctag.plc_tag_set_bit(handle, offSetBit, val);
    }

    /// <inheritdoc />
    public int SetSByte(int handle, int offSet, sbyte val)
    {
        return plctag.plc_tag_set_int8(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetShort(int handle, int offSet, short val)
    {
        return plctag.plc_tag_set_int16(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetInt(int handle, int offSet, int val)
    {
        return plctag.plc_tag_set_int32(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetLong(int handle, int offSet, long val)
    {
        return plctag.plc_tag_set_int64(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetFloat(int handle, int offSet, float val)
    {
        return plctag.plc_tag_set_float32(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetDouble(int handle, int offSet, double val)
    {
        return plctag.plc_tag_set_float64(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetByte(int handle, int offSet, byte val)
    {
        return plctag.plc_tag_set_uint8(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetUShort(int handle, int offSet, ushort val)
    {
        return plctag.plc_tag_set_uint16(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetUInt(int handle, int offSet, uint val)
    {
        return plctag.plc_tag_set_uint32(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetULong(int handle, int offSet, ulong val)
    {
        return plctag.plc_tag_set_uint64(handle, offSet, val);
    }

    /// <inheritdoc />
    public int SetRawBytes(int handle, int offset, byte[] buffer, int length)
    {
        return plctag.plc_tag_set_raw_bytes(handle, offset, buffer, length);
    }

    /// <inheritdoc />
    public int SetString(int handle, int offset, string value)
    {
        return plctag.plc_tag_set_string(handle, offset, value);
    }
}