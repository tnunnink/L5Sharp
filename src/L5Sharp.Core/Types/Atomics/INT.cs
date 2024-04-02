using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>INT</b> Logix atomic data type, or a type analogous to a <see cref="short"/>.
/// </summary>
public sealed class INT : AtomicType, IComparable, IConvertible, ILogixParsable<INT>
{
    /// <summary>
    /// The underlying primitive value which is set upon construction and not changed.
    /// </summary>
    private readonly short _value;

    /// <summary>
    /// Creates a new default <see cref="INT"/> type.
    /// </summary>
    public INT()
    {
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public INT(short value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Core.Radix"/> number format of the value.</param>
    public INT(Radix radix) : base(radix)
    {
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public INT(short value, Radix radix) : base(radix)
    {
        _value = value;
    }
    
    /// <inheritdoc />
    public override string Name => nameof(INT);

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            INT typed => _value.CompareTo(typed._value),
            AtomicType atomic => _value.CompareTo((short)Convert.ChangeType(atomic, typeof(short))),
            ValueType value => _value.CompareTo((short)Convert.ChangeType(value, typeof(short))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            INT value => value._value == _value,
            AtomicType atomic => _value.Equals((short)Convert.ChangeType(atomic, typeof(short))),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(short))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Sets the specified bit of the atomic type to the provided <see cref="BOOL"/> value. 
    /// </summary>
    /// <param name="bit">The zero based bit index to set.</param>
    /// <param name="value">The <see cref="BOOL"/> value to set.</param>
    /// <returns>A new <see cref="INT"/> with the updated value.</returns>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public INT Set(int bit, BOOL value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (bit is < 0 or >= 16)
            throw new ArgumentOutOfRangeException(nameof(bit), $"The bit {bit} is out of range for type {Name}");

        var result = (short)(value ? _value | (short)(1 << bit) : _value & (short)~(1 << bit));
        return new INT(result, Radix);
    }
    
    /// <summary>
    /// Parses a string into a <see cref="INT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="INT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public new static INT Parse(string value)
    {
        if (short.TryParse(value, out var result))
            return new INT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.ParseValue(value);
        var converted = (short)Convert.ChangeType(atomic, typeof(short));
        return new INT(converted, radix);
    }

    /// <summary>
    /// Tries to parse a string into a <see cref="INT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed <see cref="INT"/> value if successful; Otherwise, <c>null</c>.</returns>
    public new static INT? TryParse(string? value)
    {
        if (value is null || value.IsEmpty())
            return default;

        if (short.TryParse(value, out var primitive))
            return new INT(primitive);

        if (!Radix.TryInfer(value, out var radix))
            return default;

        var parsed = radix.ParseValue(value);
        var converted = (short)Convert.ChangeType(parsed, typeof(short));
        return new INT(converted, radix);
    }
    
    // Contains the implicit .NET conversions for the type.

    #region Convertsions

    /// <summary>
    /// Converts the provided <see cref="short"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="INT"/> value.</returns>
    public static implicit operator INT(short value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="short"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="short"/> type value.</returns>
    public static implicit operator short(INT atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="INT"/> value.</returns>
    public static implicit operator INT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="INT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(INT value) => value.ToString();
    
    /*
    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(INT atomic) => new(atomic._value != 0);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(INT atomic) => new((sbyte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(INT atomic) => new((byte)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(INT atomic) => new((ushort)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static implicit operator DINT(INT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(INT atomic) => new((uint)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static implicit operator LINT(INT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="ULINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ULINT"/> type value.</returns>
    public static explicit operator ULINT(INT atomic) => new((ulong)atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> type value.</returns>
    public static implicit operator REAL(INT atomic) => new(atomic._value);

    /// <summary>
    /// Converts the provided <see cref="INT"/> to a <see cref="LREAL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LREAL"/> type value.</returns>
    public static implicit operator LREAL(INT atomic) => new(atomic._value);*/

    #endregion
    
    // Contains the IConvertible implementation for the type. I am explicitly implementing this interface for each
    // atomic type to avoid polluting the API, and to have the implementation as performant as possible.
    // To perform conversion, use the recommended .NET Convert.ChangeType() method and specify the target type.

    #region Convertible

    /// <inheritdoc />
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <inheritdoc />
    bool IConvertible.ToBoolean(IFormatProvider? provider) => _value != 0;

    /// <inheritdoc />
    byte IConvertible.ToByte(IFormatProvider? provider) => (byte)_value;

    /// <inheritdoc />
    char IConvertible.ToChar(IFormatProvider? provider) => (char)_value;

    /// <inheritdoc />
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(DateTime)} is not supported.");

    /// <inheritdoc />
    decimal IConvertible.ToDecimal(IFormatProvider? provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Decimal)} is not supported.");

    /// <inheritdoc />
    double IConvertible.ToDouble(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    short IConvertible.ToInt16(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => (sbyte)_value;

    /// <inheritdoc />
    float IConvertible.ToSingle(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    string IConvertible.ToString(IFormatProvider? provider) => ToString();

    /// <inheritdoc />
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
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
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => (ushort)_value;

    /// <inheritdoc />
    uint IConvertible.ToUInt32(IFormatProvider? provider) => (uint)_value;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => (ulong)_value;
    
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
            return new USINT((byte)_value);
        if (conversionType == typeof(UINT))
            return new UINT((ushort)_value);
        if (conversionType == typeof(UDINT))
            return new UDINT((uint)_value);
        if (conversionType == typeof(ULINT))
            return new ULINT((ulong)_value);
        
        throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.");
    }

    #endregion
}