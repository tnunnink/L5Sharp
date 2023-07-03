using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>ULINT</b> Logix atomic data type, or a type analogous to a <see cref="ulong"/>.
/// </summary>
public class ULINT : AtomicType, IComparable
{
    private readonly ulong _value;

    /// <summary>
    /// Creates a new default <see cref="ULINT"/> type.
    /// </summary>
    public ULINT()
    {
        _value = 0;
        Radix = Radix.Decimal;
    }
    
    /// <summary>
    /// Creates a new <see cref="ULINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public ULINT(Radix radix)
    {
        _value = 0;
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="ULINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public ULINT(ulong value, Radix? radix = null)
    {
        Radix = radix ?? Radix.Decimal;
        _value = value;
    }
    
    /// <inheritdoc />
    public override string Name => nameof(ULINT);

    /// <inheritdoc />
    public override Radix Radix { get; }

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
        var atomic = radix.Parse(value);
        var converted = (ulong)Convert.ChangeType(atomic, typeof(ulong));
        return new ULINT(converted, radix);
    }
    
    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        if (type is ULINT value)
            return new ULINT((ulong)value, value.Radix);

        var bytes = SetBytes(atomic.GetBytes());
        var converted = BitConverter.ToUInt64(bytes);
        return new ULINT(converted, atomic.Radix);
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            ULINT value => value._value == _value,
            AtomicType value => base.Equals(value),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(ulong))),
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
            ULINT typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((ulong)Convert.ChangeType(atomic, typeof(ulong))),
            ValueType value => _value.CompareTo((ulong)Convert.ChangeType(value, typeof(ulong))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

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
    public static implicit operator ulong(ULINT atomic) => atomic._value;

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
    public static explicit operator BOOL(ULINT atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(ULINT atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(ULINT atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(ULINT atomic) => new((short)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(ULINT atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator DINT(ULINT atomic) => new((int)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(ULINT atomic) => new((uint)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static explicit operator LINT(ULINT atomic) => new((long)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="ULINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(ULINT atomic) => new(atomic._value);

    #endregion
}