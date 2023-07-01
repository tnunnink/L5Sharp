using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>UINT</b> Logix atomic data type, or a type analogous to a <see cref="ushort"/>.
/// </summary>
[TypeConverter(typeof(UIntConverter))]
public class UINT : AtomicType, IComparable
{
    private ushort Number => BitConverter.ToUInt16(ToBytes());
    
    /// <summary>
    /// Creates a new default <see cref="UINT"/> type.
    /// </summary>
    public UINT() : base(nameof(UINT), Radix.Decimal, BitConverter.GetBytes(default(ushort)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="UINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public UINT(Radix radix) : base(nameof(UINT), radix, BitConverter.GetBytes(default(ushort)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="UINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public UINT(ushort value, Radix? radix = null) : base(nameof(UINT), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
    }

    /// <summary>
    /// Gets the <see cref="BOOL"/> representing the bit value at the specified index.
    /// </summary>
    /// <param name="bit">The bit index to access.</param>
    public BOOL this[int bit]
    {
        get => Value[bit];
        set => Value[bit] = value;
    }

    /// <summary>
    /// Represents the largest possible value of <see cref="UINT"/>.
    /// </summary>
    public const ushort MaxValue = ushort.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="UINT"/>.
    /// </summary>
    public const ushort MinValue = ushort.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="UINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="UINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static UINT Parse(string value)
    {
        if (ushort.TryParse(value, out var result))
            return new UINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (TypeDescriptor.GetConverter(typeof(UINT)).ConvertFrom(atomic) as UINT)!;
        return new UINT(converted, radix);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        switch (obj)
        {
            case null:
                return 1;
            case UINT value:
                return Number.CompareTo(value.Number);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as UINT;
                return Number.CompareTo(converted?.Number);
            default:
                throw new ArgumentException($"Cannot compare object of type {obj.GetType()} with {GetType()}.");
        }
    }

    #region Conversions
    
    /// <summary>
    /// Converts the provided <see cref="bool"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static implicit operator UINT(bool value) => value ? new UINT(1) : new UINT();
    
    /// <summary>
    /// Converts the provided <see cref="byte"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static implicit operator UINT(byte value) => new(value);
    
    /// <summary>
    /// Converts the provided <see cref="sbyte"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static explicit operator UINT(sbyte value) => new((ushort)value);
    
    /// <summary>
    /// Converts the provided <see cref="short"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static explicit operator UINT(short value) => new((ushort)value);

    /// <summary>
    /// Converts the provided <see cref="ushort"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static implicit operator UINT(ushort value) => new(value);
    
    /// <summary>
    /// Converts the provided <see cref="int"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static explicit operator UINT(int value) => new((ushort)value);
    
    /// <summary>
    /// Converts the provided <see cref="uint"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static explicit operator UINT(uint value) => new((ushort)value);
    
    /// <summary>
    /// Converts the provided <see cref="long"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static explicit operator UINT(long value) => new((ushort)value);
    
    /// <summary>
    /// Converts the provided <see cref="ulong"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static explicit operator UINT(ulong value) => new((ushort)value);
    
    /// <summary>
    /// Converts the provided <see cref="float"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static explicit operator UINT(float value) => new((ushort)value);

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="UINT"/> value.</returns>
    public static implicit operator UINT(string value) => Parse(value);
    
    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="sbyte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="sbyte"/> type value.</returns>
    public static explicit operator bool(UINT atomic) => atomic.Number != 0;
    
    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="sbyte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="sbyte"/> type value.</returns>
    public static explicit operator sbyte(UINT atomic) => (sbyte)atomic.Number;
    
    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="byte"/> type value.</returns>
    public static explicit operator byte(UINT atomic) => (byte)atomic.Number;
    
    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="short"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="short"/> type value.</returns>
    public static explicit operator short(UINT atomic) => (short)atomic.Number;

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="ushort"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ushort"/> type value.</returns>
    public static implicit operator ushort(UINT atomic) => atomic.Number;
    
    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="short"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="short"/> type value.</returns>
    public static implicit operator int(UINT atomic) => atomic.Number;

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="ushort"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ushort"/> type value.</returns>
    public static implicit operator uint(UINT atomic) => atomic.Number;
    
    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="short"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="short"/> type value.</returns>
    public static implicit operator long(UINT atomic) => atomic.Number;

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="ushort"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ushort"/> type value.</returns>
    public static implicit operator ulong(UINT atomic) => atomic.Number;

    /// <summary>
    /// Implicitly converts the provided <see cref="UINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(UINT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(UINT atomic) => new(atomic.Number != 0);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(UINT atomic) => new((sbyte)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(UINT atomic) => new((byte)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(UINT atomic) => new((short)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(UINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static implicit operator UDINT(UINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(UINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static implicit operator ULINT(UINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(UINT atomic) => new(atomic.Number);

    #endregion
}