using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>LINT</b> Logix atomic data type, or a type analogous to a <see cref="long"/>.
/// </summary>
[TypeConverter(typeof(LintConverter))]
public sealed class LINT : AtomicType, IEquatable<LINT>, IComparable<LINT>
{
    private readonly long _value;

    /// <summary>
    /// Creates a new default <see cref="LINT"/> type.
    /// </summary>
    public LINT() : base(nameof(LINT), Radix.Decimal, BitConverter.GetBytes(default(long)))
    {
        _value = BitConverter.ToInt64(ToBytes());
    }

    /// <summary>
    /// Creates a new <see cref="LINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public LINT(Radix radix) : base(nameof(LINT), radix, BitConverter.GetBytes(default(long)))
    {
        _value = BitConverter.ToInt64(ToBytes());
    }

    /// <summary>
    /// Creates a new <see cref="LINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public LINT(long value, Radix? radix = null) : base(nameof(LINT), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
        _value = BitConverter.ToInt64(ToBytes());
    }

    /// <summary>
    /// Gets a <see cref="BOOL"/> at the specified bit index.
    /// </summary>
    /// <param name="bit">The bit index to access</param>
    public BOOL this[int bit] => new(Value[bit]);

    /// <summary>
    /// Represents the largest possible value of <see cref="LINT"/>.
    /// </summary>
    public const long MaxValue = long.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="LINT"/>.
    /// </summary>
    public const long MinValue = long.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="LINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="LINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static LINT Parse(string value)
    {
        if (long.TryParse(value, out var result))
            return new LINT(result);

        var radix = Radix.Infer(value);
        var atomic = (LINT)radix.Parse(value);
        return new LINT(atomic, radix);
    }

    /// <inheritdoc />
    public bool Equals(LINT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as LINT);

    /// <inheritdoc />
    // ReSharper disable once NonReadonlyMemberInGetHashCode
    // NOT sure how else to handle since it needs to be settable and used for equality.
    // This would only be a problem if you created a hash table of atomic types.
    // NOT sure anyone would need to do that.
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Determines whether the objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are equal, otherwise, false.</returns>
    public static bool operator ==(LINT left, LINT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(LINT left, LINT right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(LINT? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
    }
    
    #region Conversions
     
    /// <summary>
    /// Converts the provided <see cref="long"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> value.</returns>
    public static implicit operator LINT(long value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="long"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="long"/> type value.</returns>
    public static implicit operator long(LINT atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="LINT"/> value.</returns>
    public static implicit operator LINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="LINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(LINT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(LINT atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(LINT atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(LINT atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(LINT atomic) => new((short)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(LINT atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static explicit operator DINT(LINT atomic) => new((int)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(LINT atomic) => new((uint)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static explicit operator ULINT(LINT atomic) => new((ulong)atomic._value);


    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(LINT atomic) => new(atomic._value);

    #endregion
}