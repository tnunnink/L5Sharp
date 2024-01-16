using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>REAL</b> Logix atomic data type, or a type analogous to a <see cref="float"/>.
/// </summary>
public sealed class REAL : AtomicType, IComparable, IConvertible, ILogixParsable<REAL>
{
    private readonly float _value;

    /// <summary>
    /// Creates a new default <see cref="REAL"/> type.
    /// </summary>
    public REAL()
    {
        _value = default;
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
    /// <param name="radix">The <see cref="Core.Radix"/> number format of the value.</param>
    public REAL(Radix radix)
    {
        _value = default;
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

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => Enumerable.Empty<LogixMember>();

    /// <inheritdoc />
    public int CompareTo(object? obj)
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
            AtomicType atomic => _value.Equals((float)Convert.ChangeType(atomic, typeof(float))),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(float))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();
    
    /// <summary>
    /// Parses a string into a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="REAL"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public new static REAL Parse(string value)
    {
        if (value.Contains("QNAN")) return new REAL(float.NaN);
        
        if (float.TryParse(value, out var result))
            return new REAL(result);

        var radix = Radix.Infer(value);
        var atomic = radix.ParseValue(value);
        var converted = (float)Convert.ChangeType(atomic, typeof(float));
        return new REAL(converted, radix);
    }

    /// <summary>
    /// Tries to parse a string into a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed <see cref="REAL"/> value if successful; Otherwise, <c>null</c>.</returns>
    public new static REAL? TryParse(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return default;
        
        if (value.Contains("QNAN")) return new REAL(float.NaN);

        if (float.TryParse(value, out var primitive))
            return new REAL(primitive);

        if (!Radix.TryInfer(value, out var radix))
            return default;

        var parsed = radix.ParseValue(value);
        var converted = (float)Convert.ChangeType(parsed, typeof(float));
        return new REAL(converted, radix);
    }

    // Contains the implicit .NET conversions for the type.

    #region Conversions

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
    char IConvertible.ToChar(IFormatProvider? provider) => 
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Char)} is not supported.");

    /// <inheritdoc />
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(DateTime)} is not supported.");

    /// <inheritdoc />
    decimal IConvertible.ToDecimal(IFormatProvider? provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Decimal)} is not supported.");

    /// <inheritdoc />
    double IConvertible.ToDouble(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    short IConvertible.ToInt16(IFormatProvider? provider) => (short)_value;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider? provider) => (int)_value;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider? provider) => (long)_value;

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
            return new INT((short)_value);
        if (conversionType == typeof(DINT))
            return new DINT((int)_value);
        if (conversionType == typeof(LINT))
            return new LINT((long)_value);
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