using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>INT</b> Logix atomic data type, or a type analogous to a <see cref="short"/>.
/// </summary>
[TypeConverter(typeof(IntConverter))]
public sealed class INT : AtomicType, IEquatable<INT>, IComparable<INT>, IComparable
{
    private short Number => BitConverter.ToInt16(ToBytes());

    /// <summary>
    /// Creates a new default <see cref="INT"/> type.
    /// </summary>
    public INT() : base(nameof(INT), Radix.Decimal, BitConverter.GetBytes(default(short)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public INT(Radix radix) : base(nameof(INT), radix, BitConverter.GetBytes(default(short)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public INT(short value, Radix? radix = null) : base(nameof(INT), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
    }

    /// <summary>
    /// Represents the largest possible value of <see cref="INT"/>.
    /// </summary>
    public const short MaxValue = short.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="INT"/>.
    /// </summary>
    public const short MinValue = short.MinValue;

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
    /// Parses the provided string value to a new <see cref="INT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="INT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static INT Parse(string value)
    {
        if (short.TryParse(value, out var result))
            return new INT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (TypeDescriptor.GetConverter(typeof(INT)).ConvertFrom(atomic) as INT)!;
        return new INT(converted, radix);
    }

    /// <inheritdoc />
    public bool Equals(INT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Number == other.Number;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        switch (obj)
        {
            case INT value:
                return Number.Equals(value.Number);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as INT;
                return Number.Equals(converted?.Number);
            default:
                return false;
        }
    }

    /// <inheritdoc />
    public override int GetHashCode() => Number.GetHashCode();

    /// <inheritdoc />
    public int CompareTo(INT? other) => 
        ReferenceEquals(null, other) ? 1 : ReferenceEquals(this, other) ? 0 : Number.CompareTo(other.Number);
    
    /// <inheritdoc />
    public int CompareTo(object obj)
    {
        switch (obj)
        {
            case null:
                return 1;
            case INT value:
                return Number.CompareTo(value.Number);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as INT;
                return Number.CompareTo(converted?.Number);
            default:
                throw new ArgumentException($"Cannot compare object of type {obj.GetType()} with {GetType()}.");
        }
    }

    /// <summary>
    /// Determines whether the objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are equal, otherwise, false.</returns>
    public static bool operator ==(INT left, INT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(INT left, INT right) => !Equals(left, right);

    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="short"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="INT"/> value.</returns>
    public static implicit operator INT(short value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="short"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="short"/> type value.</returns>
    public static implicit operator short(INT atomic) => atomic.Number;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="INT"/> value.</returns>
    public static implicit operator INT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="INT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(INT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(INT atomic) => new(atomic.Number != 0);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(INT atomic) => new((sbyte)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(INT atomic) => new((byte)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(INT atomic) => new((ushort)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(INT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(INT atomic) => new((uint)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(INT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static explicit operator ULINT(INT atomic) => new((ulong)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(INT atomic) => new(atomic.Number);

    #endregion
}