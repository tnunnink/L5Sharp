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
public sealed class BOOL : AtomicType, IEquatable<BOOL>, IComparable<BOOL>
{
    private bool GetValue => BitConverter.ToBoolean(GetBytes());

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
                return atomic switch
                {
                    BOOL v => v,
                    SINT v => new BOOL(v != 0, radix),
                    _ => throw new ArgumentOutOfRangeException(nameof(atomic),
                        $"The parsed atomic type is not convertable to a {typeof(BOOL)}.")
                };
        }
    }

    /// <inheritdoc />
    public bool Equals(BOOL? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetValue == other.GetValue;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as BOOL);

    /// <inheritdoc />
    public override int GetHashCode() => GetValue.GetHashCode();

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
    public int CompareTo(BOOL? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : GetValue.CompareTo(other.GetValue);
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
    public static implicit operator bool(BOOL atomic) => atomic.GetValue;

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
    public static implicit operator int(BOOL atomic) => atomic.GetValue ? 1 : 0;

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

    #endregion
}