using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>ULINT</b> Logix atomic data type, or a type analogous to a <see cref="ulong"/>.
/// </summary>
[TypeConverter(typeof(ULintConverter))]
public class ULINT : AtomicType, IEquatable<ULINT>, IComparable<ULINT>
{
    private ulong GetValue => BitConverter.ToUInt64(ToBytes());
    
    /// <summary>
    /// Creates a new default <see cref="ULINT"/> type.
    /// </summary>
    public ULINT() : base(nameof(ULINT), Radix.Decimal, BitConverter.GetBytes(default(ulong)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="ULINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public ULINT(Radix radix) : base(nameof(ULINT), radix, BitConverter.GetBytes(default(ulong)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="ULINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public ULINT(ulong value, Radix? radix = null) : base(nameof(ULINT), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
    }

    /// <summary>
    /// Gets a <see cref="BOOL"/> at the specified bit index.
    /// </summary>
    /// <param name="bit">The bit index to access</param>
    public BOOL this[int bit] => new(Value[bit]);

    /// <summary>
    /// Represents the largest possible value of <see cref="ULINT"/>.
    /// </summary>
    public const ulong MaxValue = ulong.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="ULINT"/>.
    /// </summary>
    public const ulong MinValue = ulong.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="ULINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="ULINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static ULINT Parse(string value)
    {
        if (ulong.TryParse(value, out var result))
            return new ULINT(result);

        var radix = Radix.Infer(value);
        var atomic = (ULINT)radix.Parse(value);
        return new ULINT(atomic, radix);
    }

    /// <inheritdoc />
    public bool Equals(ULINT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetValue == other.GetValue;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as ULINT);

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
    public static bool operator ==(ULINT left, ULINT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(ULINT left, ULINT right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(ULINT? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : GetValue.CompareTo(other.GetValue);
    }
    
    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="ulong"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> value.</returns>
    public static implicit operator ULINT(ulong value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ulong"/> type value.</returns>
    public static implicit operator ulong(ULINT atomic) => atomic.GetValue;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="ULINT"/> value.</returns>
    public static implicit operator ULINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="ULINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(ULINT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(ULINT atomic) => new(atomic.GetValue != 0);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(ULINT atomic) => new((sbyte)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(ULINT atomic) => new((byte)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(ULINT atomic) => new((short)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(ULINT atomic) => new((ushort)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator DINT(ULINT atomic) => new((int)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(ULINT atomic) => new((uint)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static explicit operator LINT(ULINT atomic) => new((long)atomic.GetValue);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(ULINT atomic) => new(atomic.GetValue);

    #endregion
}