using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <i>BOOL</i> Logix atomic data type, or a type analogous to a <see cref="bool"/>. This object is meant
/// to wrap the DataValue or DataValueMember data for the L5X tag data structure.
/// </summary>
[TypeConverter(typeof(BoolConverter))]
public sealed class BOOL : AtomicType, IEquatable<BOOL>, IComparable<BOOL>, IComparable
{
    private bool Bit => BitConverter.ToBoolean(ToBytes());

    /// <summary>
    /// Creates a new default <see cref="BOOL"/> type.
    /// </summary>
    public BOOL() : base(nameof(BOOL), Radix.Decimal, BitConverter.GetBytes(default(bool)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="BOOL"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public BOOL(Radix radix) : base(nameof(BOOL), radix, BitConverter.GetBytes(default(bool)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="BOOL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix"></param>
    public BOOL(bool value, Radix? radix = null) : base(nameof(BOOL), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
    }

    /// <summary>
    /// Creates a new instance of a BOOL with the provided number.
    /// </summary>
    /// <param name="value">A number that will be evaluated to <c>false</c> if 0, otherwise <c>true</c>.</param>
    public BOOL(int value) : base(nameof(BOOL), Radix.Decimal, BitConverter.GetBytes(value != 0))
    {
    }

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Parses the provided string value to a new <see cref="BOOL"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="BOOL"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static BOOL Parse(string value)
    {
        if (bool.TryParse(value, out var result))
            return new BOOL(result);

        switch (value)
        {
            case "1":
                return new BOOL(true);
            case "0":
                return new BOOL();
            default:
                var radix = Radix.Infer(value);
                var atomic = radix.Parse(value);
                var converted = (TypeDescriptor.GetConverter(typeof(BOOL)).ConvertFrom(atomic) as BOOL)!;
                return new BOOL(converted, radix);
        }
    }

    /// <inheritdoc />
    public bool Equals(BOOL? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Bit == other.Bit;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    { 
        switch (obj)
        {
            case null:
                return false;
            case BOOL value:
                return Bit.Equals(value.Bit);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as BOOL;
                return Equals(converted?.Bit);
            default:
                throw new ArgumentException($"Cannot compare object of type {obj.GetType()} with {GetType()}.");
        }
    }

    /// <inheritdoc />
    public override int GetHashCode() => Bit.GetHashCode();

    /// <summary>
    /// Determines whether the objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are equal, otherwise, false.</returns>
    public static bool operator ==(BOOL? left, BOOL? right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(BOOL? left, BOOL? right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(BOOL? other) => 
        ReferenceEquals(null, other) ? 1 : ReferenceEquals(this, other) ? 0 : Bit.CompareTo(other.Bit);

    /// <inheritdoc />
    public int CompareTo(object obj)
    {
        switch (obj)
        {
            case null:
                return 1;
            case BOOL value:
                return Bit.CompareTo(value.Bit);
            case AtomicType atomic:
                var converted = TypeDescriptor.GetConverter(GetType()).ConvertFrom(atomic) as BOOL;
                return Bit.CompareTo(converted?.Bit);
            default:
                throw new ArgumentException($"Cannot compare object of type {obj.GetType()} with {GetType()}.");
        }
    }

    #region Conversions

    /// <summary>
    /// Implicitly converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> value.</returns>
    public static implicit operator BOOL(bool value) => new(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="bool"/> type value.</returns>
    public static implicit operator bool(BOOL atomic) => atomic.Bit;

    /// <summary>
    /// Implicitly converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> value.</returns>
    public static implicit operator BOOL(int value) => new(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="bool"/> type value.</returns>
    public static implicit operator int(BOOL atomic) => atomic.Bit ? 1 : 0;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="BOOL"/> value.</returns>
    public static implicit operator BOOL(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(BOOL value) => value.ToString();
    
    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static implicit operator SINT(BOOL atomic) => atomic.Bit ? new SINT(1) : new SINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static implicit operator USINT(BOOL atomic) => atomic.Bit ? new USINT(1) : new USINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static implicit operator INT(BOOL atomic) => atomic.Bit ? new INT(1) : new INT();
    
    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static implicit operator UINT(BOOL atomic) => atomic.Bit ? new UINT(1) : new UINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(BOOL atomic) => atomic.Bit ? new DINT(1) : new DINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static implicit operator UDINT(BOOL atomic) => atomic.Bit ? new UDINT(1) : new UDINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(BOOL atomic) => atomic.Bit ? new LINT(1) : new LINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static implicit operator ULINT(BOOL atomic) => atomic.Bit ? new ULINT(1) : new ULINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(BOOL atomic) => atomic.Bit ? new REAL(1) : new REAL();

    #endregion
}