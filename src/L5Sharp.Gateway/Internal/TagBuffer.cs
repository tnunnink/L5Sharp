using System;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;

namespace L5Sharp.Gateway.Internal;

/// <summary>
/// Handles mapping and conversion of tag values between internal representations and external data handles.
/// Provides functionality for reading from and writing to handle values associated with specific tags.
/// </summary>
internal class TagBuffer(ITagService service)
{
    /// <summary>
    /// Retrieves and updates the value of the specified tag from the given handle.
    /// </summary>
    /// <param name="tag">The tag instance whose value is to be read and updated.</param>
    /// <param name="handle">The identifier for the data source from which the tag value is retrieved.</param>
    /// <returns>A boolean value indicating whether the tag value was changed.</returns>
    internal bool ReadValue(Tag tag, int handle)
    {
        switch (tag.Value)
        {
            case BOOL atomic when Update(atomic, service.GetByte(handle, 0) != 0): break;
            case SINT atomic when Update(atomic, service.GetSByte(handle, 0)): break;
            case INT atomic when Update(atomic, service.GetShort(handle, 0)): break;
            case DINT atomic when Update(atomic, service.GetInt(handle, 0)): break;
            case LINT atomic when Update(atomic, service.GetLong(handle, 0)): break;
            case REAL atomic when Update(atomic, service.GetFloat(handle, 0)): break;
            case LREAL atomic when Update(atomic, service.GetDouble(handle, 0)): break;
            case USINT atomic when Update(atomic, service.GetByte(handle, 0)): break;
            case UINT atomic when Update(atomic, service.GetUShort(handle, 0)): break;
            case UDINT atomic when Update(atomic, service.GetUInt(handle, 0)): break;
            case ULINT atomic when Update(atomic, service.GetULong(handle, 0)): break;
            case DT atomic when Update(atomic, service.GetLong(handle, 0)): break;
            case LDT atomic when Update(atomic, service.GetLong(handle, 0)): break;
            case TIME32 atomic when Update(atomic, service.GetInt(handle, 0)): break;
            case TIME atomic when Update(atomic, service.GetLong(handle, 0)): break;
            case LTIME atomic when Update(atomic, service.GetLong(handle, 0)): break;
            case StringData str:
                var value = service.GetString(handle, 0);
                if (string.Equals(value, str.ToString(), StringComparison.Ordinal)) return false;
                str.Update(value);
                break;
            default: return false;
        }

        return true;

        static bool Update<T>(IAtomicValue<T> atomic, T value) where T : struct, IEquatable<T>
        {
            if (atomic.Value.Equals(value)) return false;
            atomic.Value = value;
            return true;
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
            BOOL atomic => service.SetByte(handle, 0, (byte)(atomic.Value ? 1 : 0)),
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
            StringData stringData => service.SetString(handle, 0, stringData.ToString()),
            _ => throw new ArgumentOutOfRangeException(nameof(tag), tag.Value,
                $"Unsupported tag data type: {tag.Value.GetType().Name}")
        };
    }
}