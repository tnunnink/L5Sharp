using System;
using System.Collections;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>SINT</b> Logix atomic data type, or a type analogous to <see cref="sbyte"/>.
/// </summary>
[TypeConverter(typeof(SintConverter))]
public sealed class SINT : AtomicType, IEquatable<SINT>, IComparable<SINT>
{
    private readonly sbyte _value;

    /// <summary>
    /// Creates a new default <see cref="SINT"/> type.
    /// </summary>
    public SINT() : base(nameof(SINT), Radix.Decimal, new[] { default(byte) })
    {
        _value = (sbyte)ToBytes()[0];
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public SINT(Radix radix) : base(nameof(SINT), radix, new[] { default(byte) })
    {
        _value = (sbyte)ToBytes()[0];
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public SINT(sbyte value, Radix? radix = null) : base(nameof(SINT), radix ?? Radix.Decimal, new[] { (byte)value })
    {
        _value = (sbyte)ToBytes()[0];
    }

    /// <summary>
    /// Gets a <see cref="BOOL"/> at the specified bit index.
    /// </summary>
    /// <param name="bit">The bit index to access</param>
    public BOOL this[int bit] => new(Value[bit]);

    /// <summary>
    /// Represents the largest possible value of <see cref="SINT"/>.
    /// </summary>
    public const sbyte MaxValue = sbyte.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="SINT"/>.
    /// </summary>
    public const sbyte MinValue = sbyte.MinValue;

    /// <summary>
    /// Implicitly converts the provided <see cref="sbyte"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> value.</returns>
    public static implicit operator SINT(sbyte value) => new(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="SINT"/> to a <see cref="sbyte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="sbyte"/> type value.</returns>
    public static implicit operator sbyte(SINT atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="SINT"/> value.</returns>
    public static implicit operator SINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="SINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(SINT value) => value.ToString();

    /// <summary>
    /// Parses the provided string value to a new <see cref="SINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="SINT"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">The converted value returned null.</exception>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static SINT Parse(string value)
    {
        var radix = Radix.Infer(value);

        var converter = TypeDescriptor.GetConverter(typeof(SINT));

        var type = converter.ConvertFrom(value) ??
                   throw new ArgumentException($"The provided value '{value}' returned a null value after conversion.");

        return new SINT((sbyte)(SINT)type, radix);
    }

    /// <inheritdoc />
    public bool Equals(SINT? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as SINT);

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
    public static bool operator ==(SINT left, SINT right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(SINT left, SINT right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(SINT? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
    }
}