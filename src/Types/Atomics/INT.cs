using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>INT</b> Logix atomic data type, or a type analogous to a <see cref="short"/>.
/// </summary>
[TypeConverter(typeof(IntConverter))]
public sealed class INT : AtomicType, IEquatable<INT>, IComparable<INT>
{
    private readonly short _value;

    /// <summary>
    /// Creates a new default <see cref="INT"/> type.
    /// </summary>
    public INT() : base(nameof(INT), Radix.Decimal, BitConverter.GetBytes(default(short)))
    {
        _value = BitConverter.ToInt16(ToBytes());
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public INT(Radix radix) : base(nameof(INT), radix, BitConverter.GetBytes(default(short)))
    {
        _value = BitConverter.ToInt16(ToBytes());
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public INT(short value, Radix? radix = null) : base(nameof(INT), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
        _value = BitConverter.ToInt16(ToBytes());
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
    /// Gets or sets a <see cref="BOOL"/> at the specified bit index.
    /// </summary>
    /// <param name="bit">The bit index to access</param>
    public BOOL this[int bit] => new(Value[bit]);

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
    public static implicit operator short(INT atomic) => atomic._value;

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
    /// Parses the provided string value to a new <see cref="INT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="INT"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">The converted value returned null.</exception>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static INT Parse(string value)
    {
        var radix = Radix.Infer(value);

        var converter = TypeDescriptor.GetConverter(typeof(INT));

        var type = converter.ConvertFrom(value) ??
                   throw new ArgumentException($"The provided value '{value}' returned a null value after conversion.");

        return new INT((short)(INT)type, radix);
    }

    /// <summary>
    /// Parses the provided string value to a new <see cref="INT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="radix">The radix format of the string</param>
    /// <returns>A <see cref="INT"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">The converted value returned null.</exception>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static INT Parse(string value, Radix radix)
    {
        var atomic = radix.Parse(value);

        var converter = TypeDescriptor.GetConverter(typeof(INT));

        var type = converter.ConvertFrom(atomic) ??
                   throw new ArgumentException($"The provided value '{value}' returned a null value after conversion.");

        return new INT((short)(INT)type, radix);
    }

    /// <inheritdoc />
    public bool Equals(INT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as INT);

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
    public static bool operator ==(INT left, INT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(INT left, INT right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(INT? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
    }
}