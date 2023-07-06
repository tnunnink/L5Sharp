using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>LREAL</b> Logix atomic data type, or a type analogous to a <see cref="double"/>.
/// </summary>
public sealed class LREAL : AtomicType, IComparable
{
    private double _value;

    /// <summary>
    /// Creates a new default <see cref="LREAL"/> type.
    /// </summary>
    public LREAL()
    {
        _value = 0;
        Radix = Radix.Float;
    }

    /// <summary>
    /// Creates a new <see cref="LREAL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public LREAL(double value)
    {
        _value = value;
        Radix = Radix.Float;
    }

    /// <summary>
    /// Creates a new <see cref="LREAL"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public LREAL(Radix radix)
    {
        _value = 0;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public LREAL(double value, Radix radix)
    {
        _value = value;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }

    /// <inheritdoc />
    public override string Name => nameof(LREAL);

    /// <inheritdoc />
    public override Radix Radix { get; }

    /// <summary>
    /// Represents the largest possible value of <see cref="LREAL"/>.
    /// </summary>
    public const double MaxValue = double.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="LREAL"/>.
    /// </summary>
    public const double MinValue = double.MinValue;

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <inheritdoc />
    public int CompareTo(object obj)
    {
        return obj switch
        {
            null => 1,
            LREAL typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((double)Convert.ChangeType(atomic, typeof(double))),
            ValueType value => _value.CompareTo((double)Convert.ChangeType(value, typeof(double))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            LREAL value => Math.Abs(_value - value._value) < double.Epsilon,
            AtomicType atomic => base.Equals(atomic),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(double))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);

    /// <inheritdoc />
    public override int GetHashCode() => base.GetHashCode();

    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set logix type {GetType().Name} with {type.GetType().Name}.");

        _value = type is LREAL value ? value._value : BitConverter.ToDouble( SetBytes(atomic.GetBytes()));
        RaiseDataChanged();
        return this;
    }

    /// <summary>
    /// Parses the provided string value to a new <see cref="LREAL"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="LREAL"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static LREAL Parse(string value)
    {
        if (value.Contains("QNAN")) return new LREAL(float.NaN);

        if (double.TryParse(value, out var result))
            return new LREAL(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (double)Convert.ChangeType(atomic, typeof(double));
        return new LREAL(converted, radix);
    }

    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="double"/> to a <see cref="LREAL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LREAL"/> value.</returns>
    public static implicit operator LREAL(double value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="double"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="double"/> type value.</returns>
    public static implicit operator double(LREAL atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="LREAL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="LREAL"/> value.</returns>
    public static implicit operator LREAL(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="LREAL"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(LREAL value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(LREAL atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(LREAL atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(LREAL atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(LREAL atomic) => new((short)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(LREAL atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator DINT(LREAL atomic) => new((int)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(LREAL atomic) => new((uint)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static explicit operator LINT(LREAL atomic) => new((long)atomic._value);
    
    /// <summary>
    /// Converts the provided <see cref="LREAL"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static explicit operator REAL(LREAL atomic) => new((float)atomic._value);
    
    #endregion
}