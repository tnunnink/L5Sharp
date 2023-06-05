using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>USINT</b> Logix atomic data type, or a type analogous to a <see cref="byte"/>.
/// </summary>
[TypeConverter(typeof(USintConverter))]
public sealed class USINT : AtomicType, IEquatable<USINT>, IComparable<USINT>
{
    private readonly byte _value;

    /// <summary>
    /// Creates a new default <see cref="USINT"/> type.
    /// </summary>
    public USINT() : base(nameof(USINT), Radix.Decimal, new[] { default(byte) })
    {
        _value = ToBytes()[0];
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public USINT(Radix radix) : base(nameof(USINT), radix, new[] { default(byte) })
    {
        _value = ToBytes()[0];
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public USINT(byte value, Radix? radix = null) : base(nameof(USINT), radix ?? Radix.Decimal, new[] { value })
    {
        _value = ToBytes()[0];
    }

    /// <summary>
    /// Gets a <see cref="BOOL"/> at the specified bit index.
    /// </summary>
    /// <param name="bit">The bit index to access</param>
    public BOOL this[int bit] => new(Value[bit]);

    /// <summary>
    /// Represents the largest possible value of <see cref="USINT"/>.
    /// </summary>
    public const byte MaxValue = byte.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="USINT"/>.
    /// </summary>
    public const byte MinValue = byte.MinValue;

    /// <summary>
    /// Converts the provided <see cref="byte"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> value.</returns>
    public static implicit operator USINT(byte value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="byte"/> type value.</returns>
    public static implicit operator byte(USINT atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="ULINT"/> value.</returns>
    public static implicit operator USINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="ULINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(USINT value) => value.ToString();

    /// <summary>
    /// Parses the provided string value to a new <see cref="USINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="USINT"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">The converted value returned null.</exception>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static USINT Parse(string value)
    {
        var radix = Radix.Infer(value);

        var converter = TypeDescriptor.GetConverter(typeof(USINT));

        var type = converter.ConvertFrom(value) ??
                   throw new ArgumentException($"The provided value '{value}' returned a null value after conversion.");

        return new USINT((byte)(USINT)type, radix);
    }

    /// <inheritdoc />
    public bool Equals(USINT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as USINT);

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
    public static bool operator ==(USINT left, USINT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(USINT left, USINT right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(USINT? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
    }
}