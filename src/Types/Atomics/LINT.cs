using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>LINT</b> Logix atomic data type, or a type analogous to a <see cref="long"/>.
/// </summary>
public sealed class LINT : AtomicType, IComparable
{
    private readonly long _value;

    /// <summary>
    /// Creates a new default <see cref="LINT"/> type.
    /// </summary>
    public LINT()
    {
        _value = 0;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="LINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public LINT(Radix radix)
    {
        _value = 0;
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="LINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public LINT(long value, Radix? radix = null)
    {
        Radix = radix ?? Radix.Decimal;
        _value = value;
    }

    /// <inheritdoc />
    public override string Name => nameof(LINT);

    /// <inheritdoc />
    public override Radix Radix { get; }

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
        var atomic = radix.Parse(value);
        var converted = (long)Convert.ChangeType(atomic, typeof(long));
        return new LINT(converted, radix);
    }

    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        if (type is LINT value)
            return new LINT((long)value, value.Radix);

        var bytes = SetBytes(atomic.GetBytes());
        var converted = BitConverter.ToInt64(bytes);
        return new LINT(converted, atomic.Radix);
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            LINT value => _value == value._value,
            AtomicType atomic => base.Equals(atomic),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(long))),
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
            LINT typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((long)Convert.ChangeType(atomic, typeof(long))),
            ValueType value => _value.CompareTo((long)Convert.ChangeType(value, typeof(long))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    #region Conversions

    /// <inheritdoc />
    public override DateTime ToDateTime(IFormatProvider provider)
    {
        var milliseconds = _value / 1000;
        var microseconds = _value % 1000;
        var ticks = microseconds * (TimeSpan.TicksPerMillisecond / 1000);
        return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks).DateTime;
    }
    
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