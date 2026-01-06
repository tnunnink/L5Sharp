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
    private readonly plctag.callback_func _onEvent;
    private readonly plctag.callback_func_ex _onEventEx;
    private readonly plctag.log_callback_func? _onLog;

    /// <summary>
    /// Initializes a new instance of the <see cref="NativeTagService"/> class with optional callbacks for events and logging.
    /// </summary>
    /// <param name="onEvent">Optional callback function invoked for tag events. Receives tag ID, event type, and status code.</param>
    /// <param name="onEventEx">Optional extended callback function invoked for tag events with user data. Receives tag ID, event type, status code, and user data pointer.</param>
    /// <param name="onLog">Optional callback function invoked for logging messages. Receives log level, message ID, and log message text.</param>
    public NativeTagService(
        Action<int, int, int>? onEvent = null,
        Action<int, int, int, IntPtr>? onEventEx = null,
        Action<int, int, string>? onLog = null)
    {
        // Pinned delegates to prevent GC collection
        _onEvent = new plctag.callback_func(onEvent ?? ((_, _, _) => { }));
        _onEventEx = new plctag.callback_func_ex(onEventEx ?? ((_, _, _, _) => { }));
        _onLog = new plctag.log_callback_func(onLog ?? ((_, _, _) => { }));
    }

    /// <inheritdoc />
    public int Abort(int tag)
    {
        return plctag.plc_tag_abort(tag);
    }

    /// <inheritdoc />
    public int CheckVersion(int major, int minor, int patch)
    {
        return plctag.plc_tag_check_lib_version(major, minor, patch);
    }

    /// <inheritdoc />
    public int Create(string path, int timeout)
    {
        return plctag.plc_tag_create(path, timeout);
    }

    /// <inheritdoc />
    public int Create(string path, Action<int, int, int, IntPtr> callback, IntPtr userData, int timeout)
    {
        // This implementation uses the callback provided in the constructor because libplctag requires the delegate
        // be pinned to prevent GC reclaiming the reference.
        return plctag.plc_tag_create_ex(path, _onEventEx, userData, timeout);
    }

    /// <inheritdoc />
    public string Decode(int error)
    {
        return plctag.plc_tag_decode_error(error);
    }

    /// <inheritdoc />
    public int Read(int tag, int timeout)
    {
        return plctag.plc_tag_read(tag, timeout);
    }

    /// <inheritdoc />
    public int Write(int tag, int timeout)
    {
        return plctag.plc_tag_write(tag, timeout);
    }

    /// <inheritdoc />
    public int Status(int tag)
    {
        return plctag.plc_tag_status(tag);
    }

    /// <inheritdoc />
    public int RegisterCallback(int tag, Action<int, int, int> callback)
    {
        // This implementation uses the callback provided in the constructor because libplctag requires the delegate
        // be pinned to prevent GC reclaiming the reference.
        return plctag.plc_tag_register_callback(tag, _onEvent);
    }

    /// <inheritdoc />
    public int UnregisterCallback(int tag)
    {
        return plctag.plc_tag_unregister_callback(tag);
    }

    /// <param name="logger"></param>
    /// <inheritdoc />
    public int RegisterLogger(Action<int, int, string> logger)
    {
        // This implementation uses the callback provided in the constructor because libplctag requires the delegate
        // be pinned to prevent GC reclaiming the reference.
        return plctag.plc_tag_register_logger(_onLog);
    }

    /// <inheritdoc />
    public int UnregisterLogger(int tag)
    {
        // I really don't know why this method takes a tag handle when the logger is global.
        return plctag.plc_tag_unregister_logger(tag);
    }

    /// <inheritdoc />
    public int Lock(int tag)
    {
        return plctag.plc_tag_lock(tag);
    }

    /// <inheritdoc />
    public int Unlock(int tag)
    {
        return plctag.plc_tag_unlock(tag);
    }

    /// <inheritdoc />
    public int Destroy(int tag)
    {
        return plctag.plc_tag_destroy(tag);
    }

    /// <inheritdoc />
    public int Shutdown()
    {
        return plctag.plc_tag_shutdown();
    }

    /// <inheritdoc />
    public void SetDebugLevel(int debugLevel)
    {
        plctag.plc_tag_set_debug_level(debugLevel);
    }

    /// <inheritdoc />
    public int GetAttribute(int tag, string attributeName, int defaultValue)
    {
        return plctag.plc_tag_get_int_attribute(tag, attributeName, defaultValue);
    }

    /// <inheritdoc />
    public int SetAttribute(int tag, string attributeName, int newValue)
    {
        return plctag.plc_tag_set_int_attribute(tag, attributeName, newValue);
    }

    /// <inheritdoc />
    public int GetByteArrayAttribute(int tag, string attributeName, byte[] buffer, int length)
    {
        return plctag.plc_tag_get_raw_bytes(tag, 0, buffer, length);
    }

    /// <inheritdoc />
    public int GetSize(int tag)
    {
        return plctag.plc_tag_get_size(tag);
    }

    /// <inheritdoc />
    public int SetSize(int tag, int size)
    {
        return plctag.plc_tag_set_size(tag, size);
    }

    /// <inheritdoc />
    public int GetBit(int tag, int offSetBit)
    {
        return plctag.plc_tag_get_bit(tag, offSetBit);
    }

    /// <inheritdoc />
    public sbyte GetSByte(int tag, int offSet)
    {
        return plctag.plc_tag_get_int8(tag, offSet);
    }

    /// <inheritdoc />
    public short GetShort(int tag, int offSet)
    {
        return plctag.plc_tag_get_int16(tag, offSet);
    }

    /// <inheritdoc />
    public int GetInt(int tag, int offSet)
    {
        return plctag.plc_tag_get_int32(tag, offSet);
    }

    /// <inheritdoc />
    public long GetLong(int tag, int offSet)
    {
        return plctag.plc_tag_get_int64(tag, offSet);
    }

    /// <inheritdoc />
    public float GetFloat(int tag, int offSet)
    {
        return plctag.plc_tag_get_float32(tag, offSet);
    }

    /// <inheritdoc />
    public double GetDouble(int tag, int offSet)
    {
        return plctag.plc_tag_get_float64(tag, offSet);
    }

    /// <inheritdoc />
    public byte GetByte(int tag, int offSet)
    {
        return plctag.plc_tag_get_uint8(tag, offSet);
    }

    /// <inheritdoc />
    public ushort GetUShort(int tag, int offSet)
    {
        return plctag.plc_tag_get_uint16(tag, offSet);
    }

    /// <inheritdoc />
    public uint GetUInt(int tag, int offSet)
    {
        return plctag.plc_tag_get_uint32(tag, offSet);
    }

    /// <inheritdoc />
    public ulong GetULong(int tag, int offSet)
    {
        return plctag.plc_tag_get_uint64(tag, offSet);
    }

    /// <inheritdoc />
    public int GetRawBytes(int tag, int start, byte[] buffer, int length)
    {
        return plctag.plc_tag_get_raw_bytes(tag, start, buffer, length);
    }

    /// <inheritdoc />
    public int GetStringLength(int tag, int offset)
    {
        return plctag.plc_tag_get_string_length(tag, offset);
    }

    /// <inheritdoc />
    public int GetString(int tag, int offset, StringBuilder buffer, int length)
    {
        return plctag.plc_tag_get_string(tag, offset, buffer, length);
    }

    /// <inheritdoc />
    public int GetStringTotalLength(int tag, int offset)
    {
        return plctag.plc_tag_get_string_total_length(tag, offset);
    }

    /// <inheritdoc />
    public int GetStringCapacity(int tag, int offset)
    {
        return plctag.plc_tag_get_string_capacity(tag, offset);
    }

    /// <inheritdoc />
    public int SetBit(int tag, int offSetBit, int val)
    {
        return plctag.plc_tag_set_bit(tag, offSetBit, val);
    }

    /// <inheritdoc />
    public int SetSByte(int tag, int offSet, sbyte val)
    {
        return plctag.plc_tag_set_int8(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetShort(int tag, int offSet, short val)
    {
        return plctag.plc_tag_set_int16(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetInt(int tag, int offSet, int val)
    {
        return plctag.plc_tag_set_int32(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetLong(int tag, int offSet, long val)
    {
        return plctag.plc_tag_set_int64(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetFloat(int tag, int offSet, float val)
    {
        return plctag.plc_tag_set_float32(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetDouble(int tag, int offSet, double val)
    {
        return plctag.plc_tag_set_float64(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetByte(int tag, int offSet, byte val)
    {
        return plctag.plc_tag_set_uint8(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetUShort(int tag, int offSet, ushort val)
    {
        return plctag.plc_tag_set_uint16(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetUInt(int tag, int offSet, uint val)
    {
        return plctag.plc_tag_set_uint32(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetULong(int tag, int offSet, ulong val)
    {
        return plctag.plc_tag_set_uint64(tag, offSet, val);
    }

    /// <inheritdoc />
    public int SetRawBytes(int tag, int offset, byte[] buffer, int length)
    {
        return plctag.plc_tag_set_raw_bytes(tag, offset, buffer, length);
    }

    /// <inheritdoc />
    public int SetString(int tag, int offset, string value)
    {
        return plctag.plc_tag_set_string(tag, offset, value);
    }
}