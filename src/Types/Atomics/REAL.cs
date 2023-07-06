using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>REAL</b> Logix atomic data type, or a type analogous to a <see cref="float"/>.
/// </summary>
public sealed class REAL : AtomicType, IComparable
{
    private float _value;
    
    /// <summary>
    /// Creates a new default <see cref="REAL"/> type.
    /// </summary>
    public REAL()
    {
        _value = 0;
        Radix = Radix.Float;
    }

    /// <summary>
    /// Creates a new <see cref="REAL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public REAL(float value)
    {
        _value = value;
        Radix = Radix.Float;
    }

    /// <summary>
    /// Creates a new <see cref="REAL"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public REAL(Radix radix)
    {
        _value = 0;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="REAL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public REAL(float value, Radix radix)
    {
        _value = value;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }
    
    /// <inheritdoc />
    public override string Name => nameof(REAL);

    /// <inheritdoc />
    public override Radix Radix { get; }

    /// <summary>
    /// Represents the largest possible value of <see cref="REAL"/>.
    /// </summary>
    public const float MaxValue = float.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="REAL"/>.
    /// </summary>
    public const float MinValue = float.MinValue;

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <inheritdoc />
    public int CompareTo(object obj)
    {
        return obj switch
        {
            null => 1,
            REAL typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((float)Convert.ChangeType(atomic, typeof(float))),
            ValueType value => _value.CompareTo((float)Convert.ChangeType(value, typeof(float))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            REAL value => Math.Abs(_value - value._value) < float.Epsilon,
            AtomicType atomic => base.Equals(atomic),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(float))),
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

        _value = type is REAL value ? value._value : BitConverter.ToSingle( SetBytes(atomic.GetBytes()));
        RaiseDataChanged();
        return this;
    }

    /// <summary>
    /// Parses the provided string value to a new <see cref="REAL"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="REAL"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static REAL Parse(string value)
    {
        if (value.Contains("QNAN")) return new REAL(float.NaN);

        if (float.TryParse(value, out var result))
            return new REAL(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (float)Convert.ChangeType(atomic, typeof(float));
        return new REAL(converted, radix);
    }

    #region Conversions

    /// <inheritdoc />
    public override long ToInt64(IFormatProvider provider) => (long)_value;
    
    /// <inheritdoc />
    public override ulong ToUInt64(IFormatProvider provider) => (ulong)_value;

    /// <inheritdoc />
    public override double ToDouble(IFormatProvider provider) => _value;

    /// <summary>
    /// Converts the provided <see cref="float"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> value.</returns>
    public static implicit operator REAL(float value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="float"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="float"/> type value.</returns>
    public static implicit operator float(REAL atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="REAL"/> value.</returns>
    public static implicit operator REAL(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="REAL"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(REAL value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(REAL atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(REAL atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(REAL atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(REAL atomic) => new((short)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(REAL atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator DINT(REAL atomic) => new((int)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(REAL atomic) => new((uint)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static explicit operator LINT(REAL atomic) => new((long)atomic._value);

    #endregion
}