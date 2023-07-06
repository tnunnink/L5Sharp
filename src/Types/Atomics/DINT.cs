using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>DINT</b> Logix atomic data type, or a type analogous to a <see cref="int"/>.
/// </summary>
public sealed class DINT : AtomicType, IComparable
{
    private int _value;

    /// <summary>
    /// Creates a new default <see cref="DINT"/> type.
    /// </summary>
    public DINT()
    {
        _value = 0;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public DINT(int value)
    {
        _value = value;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    /// <exception cref="ArgumentNullException"><c>radix</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>radix</c> is not supported by the atomic type.</exception>
    public DINT(Radix radix)
    {
        _value = 0;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> with the provided value and radix format.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    /// <exception cref="ArgumentNullException"><c>radix</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>radix</c> is not supported by the atomic type.</exception>
    public DINT(int value, Radix radix)
    {
        _value = value;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }

    /// <inheritdoc />
    public override string Name => nameof(DINT);

    /// <inheritdoc />
    public override Radix Radix { get; }

    /// <summary>
    /// Represents the largest possible value of <see cref="DINT"/>.
    /// </summary>
    public const int MaxValue = int.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="DINT"/>.
    /// </summary>
    public const int MinValue = int.MinValue;

    /// <summary>
    /// Gets the bit value as a <see cref="BOOL"/> at the specified zero based bit index of the atomic type.
    /// </summary>
    /// <param name="bit">The zero based bit index of the value to get.</param>
    /// <returns>A <see cref="BOOL"/> representing the value of the specified bit.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public BOOL Bit(int bit)
    {
        if (bit is < 0 or >= 32)
            throw new ArgumentOutOfRangeException($"The bit {bit} is out of range for type {Name}", nameof(bit));
        
        return new BOOL((_value & 1 << bit) != 0);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            DINT typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((int)Convert.ChangeType(atomic, typeof(int))),
            ValueType value => _value.CompareTo((int)Convert.ChangeType(value, typeof(int))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            DINT value => _value == value._value,
            AtomicType atomic => base.Equals(atomic),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(int))),
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
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        _value = type is DINT value ? value._value : BitConverter.ToInt32( SetBytes(atomic.GetBytes()));
        RaiseDataChanged();
        return this;
    }

    /// <summary>
    /// Sets the specified bit of the atomic type to the provided <see cref="BOOL"/> value. 
    /// </summary>
    /// <param name="bit">The zero based bit index to set.</param>
    /// <param name="value">The <see cref="BOOL"/> value to set.</param>
    /// <returns>A new <see cref="DINT"/> with the updated value.</returns>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public DINT Set(int bit, BOOL value)
    {
        if (value is null) 
            throw new ArgumentNullException(nameof(value));

        if (bit is < 0 or >= 32)
            throw new ArgumentOutOfRangeException($"The bit {bit} is out of range for type {Name}", nameof(bit));
        
        _value = value ? _value | 1 << bit : _value & ~(1 << bit);
        RaiseDataChanged();
        return this;
    }

    /// <summary>
    /// Parses the provided string value to a new <see cref="DINT"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="DINT"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">The converted value returned null.</exception>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static DINT Parse(string value)
    {
        if (int.TryParse(value, out var result))
            return new DINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.Parse(value);
        var converted = (int)Convert.ChangeType(atomic, typeof(int));
        return new DINT(converted, radix);
    }

    #region Conversions

    /// <inheritdoc />
    public override long ToInt64(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override ulong ToUInt64(IFormatProvider provider) => (ulong)_value;

    /// <inheritdoc />
    public override float ToSingle(IFormatProvider provider) => _value;

    /// <inheritdoc />
    public override double ToDouble(IFormatProvider provider) => _value;

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
    /// Converts the provided <see cref="DINT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(DINT atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(DINT atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(DINT atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator INT(DINT atomic) => new((short)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(DINT atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(DINT atomic) => new((uint)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(DINT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static explicit operator ULINT(DINT atomic) => new((ulong)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(DINT atomic) => new(atomic._value);
    
    /// <summary>
    /// Converts the provided <see cref="DINT"/> to a <see cref="LREAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LREAL"/> type value.</returns>
    public static implicit operator LREAL(DINT atomic) => new(atomic._value);

    #endregion
}