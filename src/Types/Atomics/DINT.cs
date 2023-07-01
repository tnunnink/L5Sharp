using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>DINT</b> Logix atomic data type, or a type analogous to a <see cref="int"/>.
/// </summary>
[TypeConverter(typeof(DintConverter))]
public sealed class DINT : AtomicType, IComparable
{
    private int Number => BitConverter.ToInt32(ToBytes());

    /// <summary>
    /// Creates a new default <see cref="DINT"/> type.
    /// </summary>
    public DINT() : base(nameof(DINT), Radix.Decimal, BitConverter.GetBytes(default(int)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public DINT(Radix radix) : base(nameof(DINT), radix, BitConverter.GetBytes(default(int)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public DINT(int value, Radix? radix = null) : base(nameof(DINT), radix ?? Radix.Decimal,
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
    /// Represents the largest possible value of <see cref="DINT"/>.
    /// </summary>
    public const int MaxValue = int.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="DINT"/>.
    /// </summary>
    public const int MinValue = int.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="DINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="DINT"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">The converted value returned null.</exception>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static DINT Parse(string value)
    {
        if (int.TryParse(value, out var result))
            return new DINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (TypeDescriptor.GetConverter(typeof(DINT)).ConvertFrom(atomic) as DINT)!;
        return new DINT(converted, radix);
    }

    /*/// <inheritdoc />
    public bool Equals(DINT? other)
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
            case DINT value:
                return Number.Equals(value.Number);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as DINT;
                return Number.Equals(converted?.Number);
            default:
                return false;
        }
    }

    /// <inheritdoc />
    public override int GetHashCode() => Number.GetHashCode();*/

    /*
    /// <inheritdoc />
    public int CompareTo(DINT? other) => 
        ReferenceEquals(null, other) ? 1 : ReferenceEquals(this, other) ? 0 : Number.CompareTo(other.Number);*/

    /// <inheritdoc />
    public int CompareTo(object obj)
    {
        switch (obj)
        {
            case null:
                return 1;
            case DINT value:
                return Number.CompareTo(value.Number);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as DINT;
                return Number.CompareTo(converted?.Number);
            default:
                throw new ArgumentException($"Cannot compare object of type {obj.GetType()} with {GetType()}.");
        }
    }

    #region Operators

    /*/// <summary>
    /// Determines whether the objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are equal, otherwise, false.</returns>
    public static bool operator ==(DINT left, DINT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(DINT left, DINT right) => !Equals(left, right);*/

    /// <summary>
    /// Compares two objects and determines if a is greater than b.
    /// </summary>
    /// <param name="a">An atomic value to compare.</param>
    /// <param name="b">An atomic value to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >(DINT a, DINT b) => !ReferenceEquals(null, a) && a.CompareTo(b) > 0;

    /// <summary>
    /// Compares two objects and determines if a is less than b.
    /// </summary>
    /// <param name="a">An atomic value to compare.</param>
    /// <param name="b">An atomic value to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <(DINT a, DINT b) => !ReferenceEquals(null, a) && a.CompareTo(b) < 0;

    /// <summary>
    /// Compares two objects and determines if a is greater than or equal to b.
    /// </summary>
    /// <param name="a">An atomic value to compare.</param>
    /// <param name="b">An atomic value to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >=(DINT a, DINT b) => !ReferenceEquals(null, a) && a.CompareTo(b) >= 0;

    /// <summary>
    /// Compares two objects and determines if a is less than or equal to b.
    /// </summary>
    /// <param name="a">An atomic value to compare.</param>
    /// <param name="b">An atomic value to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <=(DINT a, DINT b) => !ReferenceEquals(null, a) && a.CompareTo(b) >= 0;

    #endregion

    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="int"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> value.</returns>
    public static implicit operator DINT(int value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="int"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="int"/> type value.</returns>
    public static implicit operator int(DINT atomic) => atomic.Number;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="DINT"/> value.</returns>
    public static implicit operator DINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="DINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(DINT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(DINT atomic) => new(atomic.Number != 0);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(DINT atomic) => new((sbyte)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(DINT atomic) => new((byte)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator INT(DINT atomic) => new((short)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(DINT atomic) => new((ushort)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(DINT atomic) => new((uint)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(DINT atomic) => new(atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static explicit operator ULINT(DINT atomic) => new((ulong)atomic.Number);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(DINT atomic) => new(atomic.Number);

    #endregion
}