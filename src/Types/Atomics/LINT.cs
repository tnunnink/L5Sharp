using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>LINT</b> Logix atomic data type, or a type analogous to a <see cref="long"/>.
/// </summary>
public sealed class LINT : AtomicType, IComparable
{
    private long _value;

    /// <summary>
    /// Creates a new default <see cref="LINT"/> type.
    /// </summary>
    public LINT()
    {
        _value = default;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="LINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public LINT(long value)
    {
        _value = value;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="LINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public LINT(Radix radix)
    {
        _value = default;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="LINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public LINT(long value, Radix radix)
    {
        _value = value;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
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
    /// Gets the bit value as a <see cref="BOOL"/> at the specified zero based bit index of the atomic type.
    /// </summary>
    /// <param name="bit">The zero based bit index of the value to get.</param>
    /// <returns>A <see cref="BOOL"/> representing the value of the specified bit.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public BOOL Bit(int bit)
    {
        if (bit is < 0 or >= 64)
            throw new ArgumentOutOfRangeException($"The bit {bit} is out of range for type {Name}", nameof(bit));
        
        return new BOOL((_value & 1 << bit) != 0);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
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
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);

    /// <inheritdoc />
    public override int GetHashCode() => base.GetHashCode();
    
    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        _value = type is LINT value ? value._value : BitConverter.ToInt64( SetBytes(atomic.GetBytes()));
        RaiseDataChanged();
        return this;
    }
    
    /// <summary>
    /// Sets the specified bit of the atomic type to the provided <see cref="BOOL"/> value. 
    /// </summary>
    /// <param name="bit">The zero based bit index to set.</param>
    /// <param name="value">The <see cref="BOOL"/> value to set.</param>
    /// <returns>A new <see cref="LINT"/> with the updated value.</returns>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public LINT Set(int bit, BOOL value)
    {
        if (value is null) 
            throw new ArgumentNullException(nameof(value));

        if (bit is < 0 or >= 64)
            throw new ArgumentOutOfRangeException($"The bit {bit} is out of range for type {Name}", nameof(bit));
        
        _value = value ? _value | (long)1 << bit : _value & ~(1 << bit);
        RaiseDataChanged();
        return this;
    }

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

    #region Conversions

    /// <inheritdoc />
    public override DateTime ToDateTime(IFormatProvider provider)
    {
        var milliseconds = _value / 1000;
        var microseconds = _value % 1000;
        var ticks = microseconds * (TimeSpan.TicksPerMillisecond / 1000);
        return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks).DateTime;
    }

    /// <inheritdoc />
    public override float ToSingle(IFormatProvider provider) => _value;
    
    /// <inheritdoc />
    public override double ToDouble(IFormatProvider provider) => _value;

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
    
    /// <summary>
    /// Converts the provided <see cref="LINT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator LREAL(LINT atomic) => new(atomic._value);

    #endregion
}