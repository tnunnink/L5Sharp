﻿using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>UINT</b> Logix atomic data type, or a type analogous to a <see cref="ushort"/>.
/// </summary>
public sealed class UINT : AtomicData, IComparable, IConvertible, ILogixParsable<UINT>
{
    /// <summary>
    /// The underlying primitive value which is set upon construction and not changed.
    /// </summary>
    private readonly ushort _value;

    /// <summary>
    /// Creates a new default <see cref="UINT"/> type.
    /// </summary>
    public UINT()
    {
    }

    /// <summary>
    /// Creates a new <see cref="UINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public UINT(ushort value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates a new <see cref="UINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Core.Radix"/> number format of the value.</param>
    public UINT(Radix radix) : base(radix)
    {
    }

    /// <summary>
    /// Creates a new <see cref="UINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public UINT(ushort value, Radix radix) : base(radix)
    {
        _value = value;
    }
    
    /// <inheritdoc />
    public override string Name => nameof(UINT);

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            UINT typed => _value.CompareTo(typed._value),
            AtomicData atomic => _value.CompareTo((ushort)Convert.ChangeType(atomic, typeof(ushort))),
            ValueType value => _value.CompareTo((ushort)Convert.ChangeType(value, typeof(ushort))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            UINT value => _value == value._value,
            AtomicData atomic => _value.Equals((ushort)Convert.ChangeType(atomic, typeof(ushort))),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(ushort))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Parses a string into a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="UINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public new static UINT Parse(string value)
    {
        if (ushort.TryParse(value, out var result))
            return new UINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.ParseValue(value);
        var converted = (ushort)Convert.ChangeType(atomic, typeof(ushort));
        return new UINT(converted, radix);
    }

    /// <summary>
    /// Tries to parse a string into a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed <see cref="UINT"/> value if successful; Otherwise, <c>null</c>.</returns>
    public new static UINT? TryParse(string? value)
    {
        if (value is null || value.IsEmpty())
            return default;

        if (ushort.TryParse(value, out var primitive))
            return new UINT(primitive);

        if (!Radix.TryInfer(value, out var radix))
            return default;

        var parsed = radix.ParseValue(value);
        var converted = (ushort)Convert.ChangeType(parsed, typeof(ushort));
        return new UINT(converted, radix);
    }

    // Contains the implicit .NET conversions for the type.

    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="ushort"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> value.</returns>
    public static implicit operator UINT(ushort value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="UINT"/> to a <see cref="ushort"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="ushort"/> type value.</returns>
    public static implicit operator ushort(UINT atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="UINT"/> value.</returns>
    public static implicit operator UINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="UINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(UINT value) => value.ToString();

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
    short IConvertible.ToInt16(IFormatProvider? provider) => (short)_value;

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
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    uint IConvertible.ToUInt32(IFormatProvider? provider) => _value;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => _value;

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
            return new UINT(_value);
        if (conversionType == typeof(UDINT))
            return new UDINT(_value);
        if (conversionType == typeof(ULINT))
            return new ULINT(_value);

        throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.");
    }

    #endregion
}