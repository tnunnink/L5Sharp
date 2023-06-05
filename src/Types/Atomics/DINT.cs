using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>DINT</b> Logix atomic data type, or a type analogous to a <see cref="int"/>.
/// </summary>
[TypeConverter(typeof(DintConverter))]
public sealed class DINT : AtomicType, IEquatable<DINT>, IComparable<DINT>
{
    private readonly int _value;

    /// <summary>
    /// Creates a new default <see cref="DINT"/> type.
    /// </summary>
    public DINT() : base(nameof(DINT), Radix.Decimal, BitConverter.GetBytes(default(int)))
    {
        _value = BitConverter.ToInt32(ToBytes());
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public DINT(Radix radix) : base(nameof(DINT), radix, BitConverter.GetBytes(default(int)))
    {
        _value = BitConverter.ToInt32(ToBytes());
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public DINT(int value, Radix? radix = null) : base(nameof(DINT), radix ?? Radix.Decimal,
        BitConverter.GetBytes(value))
    {
        _value = BitConverter.ToInt32(ToBytes());
    }

    /// <summary>
    /// Gets a <see cref="BOOL"/> at the specified bit index.
    /// </summary>
    /// <param name="bit">The bit index to access</param>
    public BOOL this[int bit] => new(Value[bit]);

    /// <summary>
    /// Represents the largest possible value of <see cref="DINT"/>.
    /// </summary>
    public const int MaxValue = int.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="DINT"/>.
    /// </summary>
    public const int MinValue = int.MinValue;

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
    public static implicit operator int(DINT atomic) => atomic._value;

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
    /// Parses the provided string value to a new <see cref="DINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="DINT"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">The converted value returned null.</exception>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static DINT Parse(string value)
    {
        var radix = Radix.Infer(value);

        var converter = TypeDescriptor.GetConverter(typeof(DINT));

        var type = converter.ConvertFrom(value) ??
                   throw new ArgumentException($"The provided value '{value}' returned a null value after conversion.");

        return new DINT((int)(DINT)type, radix);
    }

    /// <inheritdoc />
    public bool Equals(DINT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as DINT);

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
    public static bool operator ==(DINT left, DINT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(DINT left, DINT right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(DINT? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
    }
}