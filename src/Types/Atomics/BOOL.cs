using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <i>BOOL</i> Logix atomic data type, or a type analogous to a <see cref="bool"/>. This object is meant
/// to wrap the DataValue or DataValueMember data for the L5X tag data structure.
/// </summary>
public sealed class BOOL : AtomicType, IComparable
{
    private readonly bool _value;

    /// <summary>
    /// Creates a new default <see cref="BOOL"/> type.
    /// </summary>
    public BOOL()
    {
        _value = false;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="BOOL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public BOOL(bool value)
    {
        _value = value;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="BOOL"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public BOOL(Radix radix)
    {
        _value = false;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this)) throw new ArgumentException("", nameof(radix));
        Radix = radix;
    }
    
    /// <summary>
    /// Creates a new <see cref="BOOL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public BOOL(bool value, Radix radix)
    {
        _value = value;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this)) throw new ArgumentException("", nameof(radix));
        Radix = radix;
    }

    /// <inheritdoc />
    public override string Name => nameof(BOOL);

    /// <inheritdoc />
    public override Radix Radix { get; }

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Parses the provided string value to a new <see cref="BOOL"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="BOOL"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static BOOL Parse(string value)
    {
        if (bool.TryParse(value, out var result))
            return new BOOL(result);

        switch (value)
        {
            case "1":
                return new BOOL(true);
            case "0":
                return new BOOL();
            default:
                var radix = Radix.Infer(value);
                var atomic = radix.Parse(value);
                var converted = (bool)Convert.ChangeType(atomic, typeof(bool));
                return new BOOL(converted, radix);
        }
    }

    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        if (type is BOOL value)
            return new BOOL((bool)value, value.Radix);

        var bytes = SetBytes(atomic.GetBytes());
        var converted = BitConverter.ToBoolean(bytes);
        return new BOOL(converted, atomic.Radix);
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            BOOL value => value._value == _value,
            AtomicType value => base.Equals(value),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(bool))),
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
            BOOL typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((bool)Convert.ChangeType(atomic, typeof(bool))),
            ValueType value => _value.CompareTo((bool)Convert.ChangeType(value, typeof(bool))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    #region Conversions
    
    /// <inheritdoc />
    public override short ToInt16(IFormatProvider provider) => _value ? (short)1 : default;

    /// <inheritdoc />
    public override ushort ToUInt16(IFormatProvider provider) => _value ? (ushort)1 : default;

    /// <inheritdoc />
    public override int ToInt32(IFormatProvider provider) => _value ? 1 : default;

    /// <inheritdoc />
    public override uint ToUInt32(IFormatProvider provider) => _value ? (uint)1 : default;

    /// <inheritdoc />
    public override long ToInt64(IFormatProvider provider) => _value ? (long)1 : default;

    /// <inheritdoc />
    public override ulong ToUInt64(IFormatProvider provider) => _value ? (ulong)1 : default;

    /// <inheritdoc />
    public override float ToSingle(IFormatProvider provider) => _value ? (float)1 : default;
    
    /// <inheritdoc />
    public override double ToDouble(IFormatProvider provider) => _value ? (double)1 : default;

    /// <summary>
    /// Implicitly converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> value.</returns>
    public static implicit operator BOOL(bool value) => new(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="bool"/> type value.</returns>
    public static implicit operator bool(BOOL atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> value.</returns>
    public static implicit operator BOOL(int value) => new(value != 0);

    /// <summary>
    /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="bool"/> type value.</returns>
    public static implicit operator int(BOOL atomic) => atomic._value ? 1 : 0;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="BOOL"/> value.</returns>
    public static implicit operator BOOL(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(BOOL value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static implicit operator SINT(BOOL atomic) => atomic._value ? new SINT(1) : new SINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static implicit operator USINT(BOOL atomic) => atomic._value ? new USINT(1) : new USINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static implicit operator INT(BOOL atomic) => atomic._value ? new INT(1) : new INT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static implicit operator UINT(BOOL atomic) => atomic._value ? new UINT(1) : new UINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(BOOL atomic) => atomic._value ? new DINT(1) : new DINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static implicit operator UDINT(BOOL atomic) => atomic._value ? new UDINT(1) : new UDINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(BOOL atomic) => atomic._value ? new LINT(1) : new LINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static implicit operator ULINT(BOOL atomic) => atomic._value ? new ULINT(1) : new ULINT();

    /// <summary>
    /// Converts the provided <see cref="BOOL"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(BOOL atomic) => atomic._value ? new REAL(1) : new REAL();

    #endregion
}