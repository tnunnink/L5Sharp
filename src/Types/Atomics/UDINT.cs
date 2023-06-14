using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>UDINT</b> Logix atomic data type, or a type analogous to a <see cref="uint"/>.
/// </summary>
[TypeConverter(typeof(UDintConverter))]
public class UDINT : AtomicType, IEquatable<UDINT>, IComparable<UDINT>
{
    private uint GetValue => BitConverter.ToUInt32(ToBytes());

    /// <summary>
    /// Creates a new default <see cref="UDINT"/> type.
    /// </summary>
    public UDINT() : base(nameof(UDINT), Radix.Decimal, BitConverter.GetBytes(default(uint)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="UDINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public UDINT(Radix radix) : base(nameof(UDINT), radix, BitConverter.GetBytes(default(uint)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="UDINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public UDINT(uint value, Radix? radix = null) : base(nameof(UDINT), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
    }

    /// <summary>
    /// Gets a <see cref="BOOL"/> at the specified bit index.
    /// </summary>
    /// <param name="bit">The bit index to access</param>
    public BOOL this[int bit] => new(Value[bit]);

    /// <summary>
    /// Represents the largest possible value of <see cref="UDINT"/>.
    /// </summary>
    public const uint MaxValue = uint.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="UDINT"/>.
    /// </summary>
    public const uint MinValue = uint.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="UDINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="UDINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static UDINT Parse(string value)
    {
        if (uint.TryParse(value, out var result))
            return new UDINT(result);

        var radix = Radix.Infer(value);
        var atomic = (UDINT)radix.Parse(value);
        return new UDINT(atomic, radix);
    }

    /// <inheritdoc />
    public bool Equals(UDINT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetValue == other.GetValue;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as UDINT);

    /// <inheritdoc />
    // ReSharper disable once NonReadonlyMemberInGetHashCode
    // NOT sure how else to handle since it needs to be settable and used for equality.
    // This would only be a problem if you created a hash table of atomic types.
    // NOT sure anyone would need to do that.
    public override int GetHashCode() => GetValue.GetHashCode();

    /// <summary>
    /// Determines whether the objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are equal, otherwise, false.</returns>
    public static bool operator ==(UDINT left, UDINT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(UDINT left, UDINT right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(UDINT? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : GetValue.CompareTo(other.GetValue);
    }

    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="uint"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> value.</returns>
    public static implicit operator UDINT(uint value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="uint"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="uint"/> type value.</returns>
    public static implicit operator uint(UDINT atomic) => atomic.GetValue;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="UDINT"/> value.</returns>
    public static implicit operator UDINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="UDINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(UDINT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(UDINT atomic) => new(atomic.GetValue != 0);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(UDINT atomic) => new((sbyte)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(UDINT atomic) => new((byte)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(UDINT atomic) => new((short)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(UDINT atomic) => new((ushort)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator DINT(UDINT atomic) => new((int)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(UDINT atomic) => new(atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static implicit operator ULINT(UDINT atomic) => new(atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(UDINT atomic) => new(atomic.GetValue);

    #endregion
}