using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>SINT</b> Logix atomic data type, or a type analogous to <see cref="sbyte"/>.
/// </summary>
public sealed class SINT : AtomicType, IComparable
{
    private readonly sbyte _value;

    /// <summary>
    /// Creates a new default <see cref="SINT"/> type.
    /// </summary>
    public SINT()
    {
        _value = 0;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public SINT(Radix radix)
    {
        _value = 0;
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public SINT(sbyte value, Radix? radix = null)
    {
        _value = value;
        Radix = radix ?? Radix.Decimal;
    }

    /// <inheritdoc />
    public override string Name => nameof(SINT);

    /// <inheritdoc />
    public override Radix Radix { get; }

    /// <summary>
    /// Represents the largest possible value of <see cref="SINT"/>.
    /// </summary>
    public const sbyte MaxValue = sbyte.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="SINT"/>.
    /// </summary>
    public const sbyte MinValue = sbyte.MinValue;

    /// <summary>
    /// Parses the provided string value to a new <see cref="SINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="SINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static SINT Parse(string value)
    {
        if (sbyte.TryParse(value, out var result))
            return new SINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (sbyte)Convert.ChangeType(atomic, typeof(sbyte));
        return new SINT(converted, radix);
    }

    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        if (type is SINT value)
            return new SINT((sbyte)value, value.Radix);

        var bytes = SetBytes(atomic.GetBytes());
        var converted = unchecked((sbyte)bytes[0]);
        return new SINT(converted, atomic.Radix);
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => unchecked(new[] { (byte)_value });

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            SINT value => value._value == _value,
            AtomicType value => base.Equals(value),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(sbyte))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            SINT typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((sbyte)Convert.ChangeType(atomic, typeof(sbyte))),
            ValueType value => _value.CompareTo((sbyte)Convert.ChangeType(value, typeof(sbyte))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    #region Conversions

    /// <inheritdoc />
    public override short ToInt16(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override ushort ToUInt16(IFormatProvider provider) => (ushort)_value;

    /// <inheritdoc />
    public override int ToInt32(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override uint ToUInt32(IFormatProvider provider) => (ushort)_value;

    /// <inheritdoc />
    public override long ToInt64(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override ulong ToUInt64(IFormatProvider provider) => (ushort)_value;

    /// <inheritdoc />
    public override float ToSingle(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override double ToDouble(IFormatProvider provider) => _value;

    /// <summary>
    /// Converts the provided <see cref="sbyte"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> value.</returns>
    public static implicit operator SINT(sbyte value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="sbyte"/> value.
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
    /// Converts the provided <see cref="SINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(SINT atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(SINT atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static implicit operator INT(SINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(SINT atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(SINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(SINT atomic) => new((uint)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(SINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static explicit operator ULINT(SINT atomic) => new((ulong)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(SINT atomic) => new(atomic._value);

    #endregion
}