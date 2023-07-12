using System;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>USINT</b> Logix atomic data type, or a type analogous to a <see cref="byte"/>.
/// </summary>
public sealed class USINT : AtomicType, IComparable, IConvertible
{
    private byte _value;

    /// <summary>
    /// Creates a new default <see cref="USINT"/> type.
    /// </summary>
    public USINT()
    {
        _value = 0;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public USINT(byte value)
    {
        _value = value;
        Radix = Radix.Decimal;
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public USINT(Radix radix)
    {
        _value = 0;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public USINT(byte value, Radix radix)
    {
        _value = value;
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"Invalid Radix {radix} for atomic type {Name}.", nameof(radix));
        Radix = radix;
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
    /// Gets the bit value as a <see cref="BOOL"/> at the specified zero based bit index of the atomic type.
    /// </summary>
    /// <param name="bit">The zero based bit index of the value to get.</param>
    /// <returns>A <see cref="BOOL"/> representing the value of the specified bit.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public BOOL Bit(int bit)
    {
        if (bit is < 0 or >= 8)
            throw new ArgumentOutOfRangeException($"The bit {bit} is out of range for type {Name}", nameof(bit));
        
        return new BOOL((_value & 1 << bit) != 0);
    }

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
    public override byte[] GetBytes() => new[] { _value };

    /// <inheritdoc />
    public override int GetHashCode() => base.GetHashCode();

    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not AtomicType atomic)
            throw new ArgumentException($"Can not set {GetType().Name} with type {type.GetType().Name}");

        _value = type is USINT value ? value._value : SetBytes(atomic.GetBytes())[0];
        RaiseDataChanged();
        return this;
    }
    
    /// <summary>
    /// Sets the specified bit of the atomic type to the provided <see cref="BOOL"/> value. 
    /// </summary>
    /// <param name="bit">The zero based bit index to set.</param>
    /// <param name="value">The <see cref="BOOL"/> value to set.</param>
    /// <returns>A new <see cref="USINT"/> with the updated value.</returns>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public USINT Set(int bit, BOOL value)
    {
        if (value is null) 
            throw new ArgumentNullException(nameof(value));

        if (bit is < 0 or >= 8)
            throw new ArgumentOutOfRangeException($"The bit {bit} is out of range for type {Name}", nameof(bit));
        
        _value = (byte)(value ? _value | (byte)(1 << bit) : _value & (byte)~(1 << bit));
        RaiseDataChanged();
        return this;
    }

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
    
    // Contains the implicit .NET conversions for the type.

    #region Conversions

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

    #endregion
    
    // Contains the IConvertible implementation for the type. I am explicitly implementing this interface for each
    // atomic type to avoid polluting the API, and to have the implementation as performant as possible.
    // To perform conversion, use the recommended .NET Convert.ChangeType() method and specify the target type.
    
    #region Convertible

    /// <inheritdoc />
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <inheritdoc />
    bool IConvertible.ToBoolean(IFormatProvider provider) => _value != 0;

    /// <inheritdoc />
    byte IConvertible.ToByte(IFormatProvider provider) => _value;

    /// <inheritdoc />
    char IConvertible.ToChar(IFormatProvider provider) => (char)_value;

    /// <inheritdoc />
    DateTime IConvertible.ToDateTime(IFormatProvider provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(DateTime)} is not supported.");

    /// <inheritdoc />
    decimal IConvertible.ToDecimal(IFormatProvider provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Decimal)} is not supported.");

    /// <inheritdoc />
    double IConvertible.ToDouble(IFormatProvider provider) => _value;

    /// <inheritdoc />
    short IConvertible.ToInt16(IFormatProvider provider) => _value;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider provider) => _value;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider provider) => _value;

    /// <inheritdoc />
    sbyte IConvertible.ToSByte(IFormatProvider provider) => (sbyte)_value;

    /// <inheritdoc />
    float IConvertible.ToSingle(IFormatProvider provider) => _value;

    /// <inheritdoc />
    string IConvertible.ToString(IFormatProvider provider) => ToString();

    /// <inheritdoc />
    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
        var convertible = (IConvertible)this;
        
        return Type.GetTypeCode(conversionType) switch
        {
            TypeCode.Boolean => convertible.ToBoolean(provider),
            TypeCode.Byte => convertible.ToByte(provider),
            TypeCode.Char => convertible.ToChar(provider),
            TypeCode.DateTime => convertible.ToDateTime(provider),
            TypeCode.Decimal => convertible.ToDecimal(provider),
            TypeCode.Double => convertible.ToDouble(provider),
            TypeCode.Empty => throw new ArgumentNullException(nameof(conversionType)),
            TypeCode.Int16 => convertible.ToInt16(provider),
            TypeCode.Int32 => convertible.ToInt32(provider),
            TypeCode.Int64 => convertible.ToInt64(provider),
            TypeCode.Object => ToAtomic(conversionType),
            TypeCode.SByte => convertible.ToSByte(provider),
            TypeCode.Single => convertible.ToSingle(provider),
            TypeCode.String => ToString(),
            TypeCode.UInt16 => convertible.ToUInt16(provider),
            TypeCode.UInt32 => convertible.ToUInt32(provider),
            TypeCode.UInt64 => convertible.ToUInt64(provider),
            TypeCode.DBNull => throw new InvalidCastException(
                "Conversion for type code 'DbNull' not supported by AtomicType."),
            _ => throw new InvalidCastException($"Conversion for {conversionType.Name} not supported by AtomicType.")
        };
    }

    /// <inheritdoc />
    ushort IConvertible.ToUInt16(IFormatProvider provider) => _value;

    /// <inheritdoc />
    uint IConvertible.ToUInt32(IFormatProvider provider) => _value;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider provider) => _value;
    
    /// <summary>
    /// Converts the current atomic type to the specified atomic type.
    /// </summary>
    /// <param name="conversionType">The atomic type to convert to.</param>
    /// <returns>A <see cref="object"/> representing the converted atomic type value.</returns>
    /// <exception cref="InvalidCastException">The specified type is not a valid atomic type.</exception>
    private object ToAtomic(Type conversionType)
    {
        if (conversionType == typeof(BOOL))
            return new BOOL(_value != 0);
        if (conversionType == typeof(SINT))
            return new SINT((sbyte)_value);
        if (conversionType == typeof(INT))
            return new INT(_value);
        if (conversionType == typeof(DINT))
            return new DINT(_value);
        if (conversionType == typeof(LINT))
            return new LINT(_value);
        if (conversionType == typeof(REAL))
            return new REAL(_value);
        if (conversionType == typeof(LREAL))
            return new LREAL(_value);
        if (conversionType == typeof(USINT))
            return new USINT(_value);
        if (conversionType == typeof(UINT))
            return new UINT(_value);
        if (conversionType == typeof(UDINT))
            return new UDINT(_value);
        if (conversionType == typeof(ULINT))
            return new ULINT(_value);
        
        throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.");
    }

    #endregion
}