using System;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Handles mapping and conversion of tag values between internal representations and external data handles.
/// Provides functionality for reading from and writing to handle values associated with specific tags.
/// </summary>
internal class TagBuffer(ITagService service)
{
    /// <summary>
    /// Reads the value of a specified tag from the given handle and updates the tag's value accordingly.
    /// </summary>
    /// <param name="tag">The tag instance to be updated with the data obtained from the handle.</param>
    /// <param name="handle">The handle identifier from which the tag value will be retrieved.</param>
    internal void ReadValue(Tag tag, int handle)
    {
        switch (tag.Value)
        {
            case BOOL atomic: atomic.Update(service.GetByte(handle, 0) != 0); break;
            case SINT atomic: atomic.Update(service.GetSByte(handle, 0)); break;
            case INT atomic: atomic.Update(service.GetShort(handle, 0)); break;
            case DINT atomic: atomic.Update(service.GetInt(handle, 0)); break;
            case LINT atomic: atomic.Update(service.GetLong(handle, 0)); break;
            case REAL atomic: atomic.Update(service.GetFloat(handle, 0)); break;
            case LREAL atomic: atomic.Update(service.GetDouble(handle, 0)); break;
            case USINT atomic: atomic.Update(service.GetByte(handle, 0)); break;
            case UINT atomic: atomic.Update(service.GetUShort(handle, 0)); break;
            case UDINT atomic: atomic.Update(service.GetUInt(handle, 0)); break;
            case ULINT atomic: atomic.Update(service.GetULong(handle, 0)); break;
            case DT atomic: atomic.Update(service.GetLong(handle, 0)); break;
            case LDT atomic: atomic.Update(service.GetLong(handle, 0)); break;
            case TIME32 atomic: atomic.Update(service.GetInt(handle, 0)); break;
            case TIME atomic: atomic.Update(service.GetLong(handle, 0)); break;
            case LTIME atomic: atomic.Update(service.GetLong(handle, 0)); break;
        }
    }

    /// <summary>
    /// Writes the value of a specified tag to the given handle by converting the tag's data to a supported format.
    /// </summary>
    /// <param name="tag">The tag instance containing the data that will be written to the handle.</param>
    /// <param name="handle">The handle identifier where the tag's value will be written.</param>
    /// <returns>The status code of the write operation, indicating success or failure.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the tag's data type is not supported for writing.</exception>
    internal int WriteValue(Tag tag, int handle)
    {
        return tag.Value switch
        {
            BOOL atomic => service.SetByte(handle, 0, atomic.ToBytes()[0]),
            SINT atomic => service.SetSByte(handle, 0, atomic.Value),
            INT atomic => service.SetShort(handle, 0, atomic.Value),
            DINT atomic => service.SetInt(handle, 0, atomic.Value),
            LINT atomic => service.SetLong(handle, 0, atomic.Value),
            REAL atomic => service.SetFloat(handle, 0, atomic.Value),
            LREAL atomic => service.SetDouble(handle, 0, atomic.Value),
            USINT atomic => service.SetByte(handle, 0, atomic.Value),
            UINT atomic => service.SetUShort(handle, 0, atomic.Value),
            UDINT atomic => service.SetUInt(handle, 0, atomic.Value),
            ULINT atomic => service.SetULong(handle, 0, atomic.Value),
            DT atomic => service.SetLong(handle, 0, atomic.Value),
            LDT atomic => service.SetLong(handle, 0, atomic.Value),
            TIME32 atomic => service.SetInt(handle, 0, atomic.Value),
            TIME atomic => service.SetLong(handle, 0, atomic.Value),
            LTIME atomic => service.SetLong(handle, 0, atomic.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(tag), tag.Value,
                $"Unsupported tag data type: {tag.Value.GetType().Name}")
        };
    }
}