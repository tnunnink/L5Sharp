using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>SINT</b> Logix atomic data type, or a type analogous to <see cref="sbyte"/>.
/// </summary>
[TypeConverter(typeof(SintConverter))]
public sealed class SINT : AtomicType, IComparable
{
    private sbyte Number => (sbyte)ToBytes()[0];

    /// <summary>
    /// Creates a new default <see cref="SINT"/> type.
    /// </summary>
    public SINT() : base(nameof(SINT), Radix.Decimal, new[] { default(byte) })
    {
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public SINT(Radix radix) : base(nameof(SINT), radix, new[] { default(byte) })
    {
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public SINT(sbyte value, Radix? radix = null) : base(nameof(SINT), radix ?? Radix.Decimal, new[] { (byte)value })
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
    /// Represents the largest possible value of <see cref="SINT"/>.
    /// </summary>
    public const sbyte MaxValue = sbyte.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="SINT"/>.
    /// </summary>
    public const sbyte MinValue = sbyte.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="SINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="SINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static SINT Parse(string value)
    {
        if (sbyte.TryParse(value, out var result))
            return new SINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (TypeDescriptor.GetConverter(typeof(SINT)).ConvertFrom(atomic) as SINT)!;
        return new SINT(converted, radix);
    }
    
    /*/// <inheritdoc />
    public override bool Equals(object? obj)
    {
        switch (obj)
        {
            case SINT value:
                return Number.Equals(value.Number);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as SINT;
                return Number.Equals(converted?.Number);
            default:
                return false;
        }
    }*/

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        switch (obj)
        {
            case null:
                return 1;
            case SINT value:
                return Number.CompareTo(value.Number);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as SINT;
                return Number.CompareTo(converted?.Number);
            default:
                throw new ArgumentException($"Cannot compare object of type {obj.GetType()} with {GetType()}.");
        }
    }

    /// <inheritdoc />
    public override int GetHashCode() => Number.GetHashCode();

    #region Conversions

    /// <summary>
    /// Implicitly converts the provided <see cref="sbyte"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> value.</returns>
    public static implicit operator SINT(sbyte value) => new(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="SINT"/> to a <see cref="sbyte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="sbyte"/> type value.</returns>
    public static implicit operator sbyte(SINT atomic) => atomic.Number;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="SINT"/> value.</returns>
    public static implicit operator SINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="SINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(SINT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(SINT atomic) => new(atomic.Number != 0);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(SINT atomic) => new((byte)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static implicit operator INT(SINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(SINT atomic) => new((ushort)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(SINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(SINT atomic) => new((uint)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(SINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static explicit operator ULINT(SINT atomic) => new((ulong)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(SINT atomic) => new(atomic.Number);

    #endregion
}