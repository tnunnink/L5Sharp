using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>USINT</b> Logix atomic data type, or a type analogous to a <see cref="byte"/>.
/// </summary>
public sealed class USINT : AtomicType, IComparable
{
    private readonly byte _value;

    /// <summary>
    /// Creates a new default <see cref="USINT"/> type.
    /// </summary>
    public USINT()
    {
        _value = 0;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public USINT(Radix radix)
    {
        _value = 0;
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public USINT(byte value, Radix? radix = null)
    {
        Radix = radix ?? Radix.Decimal;
        _value = value;
    }

    /// <inheritdoc />
    public override string Name => nameof(USINT);

    /// <inheritdoc />
    public override Radix Radix { get; }

    /// <summary>
    /// Represents the largest possible value of <see cref="USINT"/>.
    /// </summary>
    public const byte MaxValue = byte.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="USINT"/>.
    /// </summary>
    public const byte MinValue = byte.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="USINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="USINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static USINT Parse(string value)
    {
        if (byte.TryParse(value, out var result))
            return new USINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (byte)Convert.ChangeType(atomic, typeof(byte));
        return new USINT(converted, radix);
    }

    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        if (type is USINT value)
            return new USINT((byte)value, value.Radix);

        var bytes = SetBytes(atomic.GetBytes());
        var converted = bytes[0];
        return new USINT(converted, atomic.Radix);
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => new[] { _value };

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            USINT value => value._value == _value,
            AtomicType value => base.Equals(value),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(byte))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public int CompareTo(object obj)
    {
        return obj switch
        {
            null => 1,
            USINT typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((byte)Convert.ChangeType(atomic, typeof(byte))),
            ValueType value => _value.CompareTo((byte)Convert.ChangeType(value, typeof(byte))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    #region Conversions

    /// <inheritdoc />
    public override short ToInt16(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override ushort ToUInt16(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override int ToInt32(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override uint ToUInt32(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override long ToInt64(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override ulong ToUInt64(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override float ToSingle(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override double ToDouble(IFormatProvider provider) => _value;

    /// <summary>
    /// Converts the provided <see cref="byte"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> value.</returns>
    public static implicit operator USINT(byte value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="byte"/> type value.</returns>
    public static implicit operator byte(USINT atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="USINT"/> value.</returns>
    public static implicit operator USINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="USINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(USINT value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(USINT atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(USINT atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static implicit operator INT(USINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static implicit operator UINT(USINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(USINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static implicit operator UDINT(USINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(USINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static implicit operator ULINT(USINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="USINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(USINT atomic) => new(atomic._value);

    #endregion
}