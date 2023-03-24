using System;
using System.Globalization;
using System.Text;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Extensions;

/// <summary>
/// Extensions for <see cref="AtomicType"/> object.
/// </summary>
public static class AtomicTypeExtensions
{
    /// <summary>
    /// Converts the current <see cref="AtomicType"/> to the specified base number.
    /// </summary>
    /// <param name="atomicType">The value type to convert.</param>
    /// <param name="baseNumber">The base number to convert to.</param>
    /// <returns>A <see cref="string"/> value representing the value in the specified base.</returns>
    public static string ToBase(this AtomicType atomicType, int baseNumber)
    {
        var bitsPerByte = baseNumber switch
        {
            2 => 8,
            8 => 3,
            _ => 2
        };
            
        var bytes = atomicType.GetBytes();

        var builder = new StringBuilder();
            
        for (var ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
        {
            var byteString = Convert.ToString(bytes[ctr], baseNumber).PadLeft(bitsPerByte, '0');
            builder.Append(byteString);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts the current value type to an array of bytes.
    /// </summary>
    /// <param name="atomicType">The current value type to convert.</param>
    /// <returns>An array of bytes representing the value.</returns>
    private static byte[] GetBytes(this AtomicType atomicType)
    {
        return atomicType switch
        {
            BOOL value => BitConverter.GetBytes((bool)value),
            SINT value => new[] { byte.Parse(((sbyte)value).ToString("X2"), NumberStyles.HexNumber) },
            USINT value => new[] { (byte)value },
            INT value => BitConverter.GetBytes(value),
            UINT value => BitConverter.GetBytes(value),
            DINT value => BitConverter.GetBytes(value),
            UDINT value => BitConverter.GetBytes(value),
            LINT value => BitConverter.GetBytes(value),
            ULINT value => BitConverter.GetBytes(value),
            _ => throw new NotSupportedException($"The provided atomic type {atomicType.GetType()} is not supported.")
        };
    }
}