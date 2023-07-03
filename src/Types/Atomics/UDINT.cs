using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>UDINT</b> Logix atomic data type, or a type analogous to a <see cref="uint"/>.
/// </summary>
public class UDINT : AtomicType, IComparable
{
    private readonly uint _value;

    /// <summary>
    /// Creates a new default <see cref="UDINT"/> type.
    /// </summary>
    public UDINT()
    {
        _value = 0;
        Radix = Radix.Decimal;
    }
    
    /// <summary>
    /// Creates a new <see cref="UDINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public UDINT(Radix radix)
    {
        _value = 0;
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="UDINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public UDINT(uint value, Radix? radix = null)
    {
        Radix = radix ?? Radix.Decimal;
        _value = value;
    }
    
    /// <inheritdoc />
    public override string Name => nameof(UDINT);

    /// <inheritdoc />
    public override Radix Radix { get; }

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
        var atomic = radix.Parse(value);
        var converted = (uint)Convert.ChangeType(atomic, typeof(uint));
        return new UDINT(converted, radix);
    }
    
    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        if (type is UDINT value)
            return new UDINT((uint)value, value.Radix);

        var bytes = SetBytes(atomic.GetBytes());
        var converted = BitConverter.ToUInt32(bytes);
        return new UDINT(converted, atomic.Radix);
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            UDINT value => _value == value._value,
            AtomicType atomic => base.Equals(atomic),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(uint))),
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
            UDINT typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((uint)Convert.ChangeType(atomic, typeof(uint))),
            ValueType value => _value.CompareTo((uint)Convert.ChangeType(value, typeof(uint))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    #region Conversions
    
    /// <inheritdoc />
    public override long ToInt64(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override ulong ToUInt64(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override float ToSingle(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override double ToDouble(IFormatProvider provider) => _value;

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
    public static implicit operator uint(UDINT atomic) => atomic._value;

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
    public static explicit operator BOOL(UDINT atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(UDINT atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(UDINT atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(UDINT atomic) => new((short)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(UDINT atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator DINT(UDINT atomic) => new((int)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(UDINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static implicit operator ULINT(UDINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(UDINT atomic) => new(atomic._value);

    #endregion
}