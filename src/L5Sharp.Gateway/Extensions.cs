using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using libplctag.NativeImport;

namespace L5Sharp.Gateway;

/// <summary>
/// Provides extension methods for working with tags in a Rockwell Logix-based PLC environment.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Converts an integer code to its corresponding <see cref="TagResult"/> value.
    /// </summary>
    /// <param name="code">The integer code to be converted to a <see cref="TagResult"/>.</param>
    /// <returns>The <see cref="TagResult"/> that corresponds to the provided code.</returns>
    public static TagResult AsResult(this int code)
    {
        return (TagResult)code;
    }

    /// <summary>
    /// Creates a real-time watch for the provided collection of tags in a specified PLC configuration.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects to monitor.</param>
    /// <param name="ip">The IP address of the target PLC to connect to.</param>
    /// <param name="slot">The slot number of the controller module. Defaults to 0 if not specified.</param>
    /// <param name="rate">The refresh interval in milliseconds for reading tag updates. Defaults to 1000 milliseconds.</param>
    /// <returns>An instance of <see cref="ITagWatch"/> that can be used to monitor and manage updates to the specified tags.</returns>
    public static ITagWatch Watch(this IEnumerable<Tag> tags, string ip, ushort slot = 0, int rate = 1000)
    {
        //todo var composite = new CompositeDisposable();
        using var client = new PlcClient(ip, slot, new PlcOptions { ReadInterval = rate });
        return client.WatchTags(tags.ToList());
    }

    /// <summary>
    /// Determines the fully qualified tag name based on the provided tag's scope and name.
    /// </summary>
    /// <param name="tag">The tag for which the name needs to be determined.</param>
    /// <returns>Returns the fully qualified tag name as a string.</returns>
    internal static TagName DetermineTagName(this Tag tag)
    {
        if (tag.Scope.IsProgram)
        {
            return $"Program:{tag.Scope.Container}.{tag.TagName}";
        }

        return tag.TagName;
    }

    /// <summary>
    /// Reads data from the specified tag handle and updates the tag value and status accordingly.
    /// </summary>
    /// <param name="tag">The tag instance to be updated with the read data.</param>
    /// <param name="handle">The handle of the tag to read data from.</param>
    internal static void ReadValue(this Tag tag, int handle)
    {
        switch (tag.Value)
        {
            case BOOL atomic:
                atomic.Update(plctag.plc_tag_get_int8(handle, 0) != 0);
                break;
            case SINT atomic:
                atomic.Update(plctag.plc_tag_get_int8(handle, 0));
                break;
            case USINT atomic:
                atomic.Update(plctag.plc_tag_get_uint8(handle, 0));
                break;
            case INT atomic:
                atomic.Update(plctag.plc_tag_get_int16(handle, 0));
                break;
            case UINT atomic:
                atomic.Update(plctag.plc_tag_get_uint16(handle, 0));
                break;
            case DINT atomic:
                atomic.Update(plctag.plc_tag_get_int32(handle, 0));
                break;
            case UDINT atomic:
                atomic.Update(plctag.plc_tag_get_uint32(handle, 0));
                break;
            case LINT atomic:
                atomic.Update(plctag.plc_tag_get_int64(handle, 0));
                break;
            case ULINT atomic:
                atomic.Update(plctag.plc_tag_get_uint64(handle, 0));
                break;
            case REAL atomic:
                atomic.Update(plctag.plc_tag_get_float32(handle, 0));
                break;
            case LREAL atomic:
                atomic.Update(plctag.plc_tag_get_float64(handle, 0));
                break;
            case DT atomic:
                atomic.Update(plctag.plc_tag_get_int64(handle, 0));
                break;
            case LDT atomic:
                atomic.Update(plctag.plc_tag_get_int64(handle, 0));
                break;
            case TIME32 atomic:
                atomic.Update(plctag.plc_tag_get_int32(handle, 0));
                break;
            case TIME atomic:
                atomic.Update(plctag.plc_tag_get_int64(handle, 0));
                break;
            case LTIME atomic:
                atomic.Update(plctag.plc_tag_get_int64(handle, 0));
                break;
        }
    }

    /// <summary>
    /// Reads data from the specified tag handle and updates the tag value and status accordingly.
    /// </summary>
    /// <param name="tag">The tag instance to be updated with the read data.</param>
    /// <param name="handle">The handle of the tag to read data from.</param>
    internal static int WriteValue(this Tag tag, int handle)
    {
        return tag.Value switch
        {
            BOOL atomic => plctag.plc_tag_set_uint8(handle, 0, atomic.ToBytes()[0]),
            SINT atomic => plctag.plc_tag_set_int8(handle, 0, atomic.Value),
            USINT atomic => plctag.plc_tag_set_uint16(handle, 0, atomic.Value),
            INT atomic => plctag.plc_tag_set_int16(handle, 0, atomic.Value),
            UINT atomic => plctag.plc_tag_set_uint16(handle, 0, atomic.Value),
            DINT atomic => plctag.plc_tag_set_int32(handle, 0, atomic.Value),
            UDINT atomic => plctag.plc_tag_set_uint32(handle, 0, atomic.Value),
            LINT atomic => plctag.plc_tag_set_int64(handle, 0, atomic.Value),
            ULINT atomic => plctag.plc_tag_set_uint64(handle, 0, atomic.Value),
            REAL atomic => plctag.plc_tag_set_float32(handle, 0, atomic.Value),
            LREAL atomic => plctag.plc_tag_set_float64(handle, 0, atomic.Value),
            DT atomic => plctag.plc_tag_set_int64(handle, 0, atomic.Value),
            LDT atomic => plctag.plc_tag_set_int64(handle, 0, atomic.Value),
            TIME32 atomic => plctag.plc_tag_set_int32(handle, 0, atomic.Value),
            TIME atomic => plctag.plc_tag_set_int64(handle, 0, atomic.Value),
            LTIME atomic => plctag.plc_tag_set_int64(handle, 0, atomic.Value),
            _ => throw new ArgumentOutOfRangeException("")
        };
    }
}