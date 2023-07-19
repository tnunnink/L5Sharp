using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <i>BOOL</i> Logix atomic data type, or a type analogous to a <see cref="bool"/>. This object is meant
/// to wrap the DataValue or DataValueMember data for the L5X tag data structure.
/// </summary>
public sealed class BOOL : AtomicType, IComparable, IConvertible
{
    private readonly bool _value;

    /// <summary>
    /// Creates a new default <see cref="BOOL"/> type.
    /// </summary>
    public BOOL()
    {
        _value = default;
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
        _value = default;
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
    public override IEnumerable<LogixMember> Members => Enumerable.Empty<LogixMember>();

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
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

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

    // Contains the implicit .NET conversions for the type.

    #region Conversions

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

    #endregion

    // Contains the IConvertible implementation for the type. I am explicitly implementing this interface for each
    // atomic type to avoid polluting the API, and to have the implementation as performant as possible.
    // To perform conversion, use the recommended .NET Convert.ChangeType() method and specify the target type.

    #region Convertible

    /// <inheritdoc />
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <inheritdoc />
    bool IConvertible.ToBoolean(IFormatProvider provider) => _value;

    /// <inheritdoc />
    byte IConvertible.ToByte(IFormatProvider provider) => _value ? (byte)1 : default;

    /// <inheritdoc />
    char IConvertible.ToChar(IFormatProvider provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Char)} is not supported.");

    /// <inheritdoc />
    DateTime IConvertible.ToDateTime(IFormatProvider provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(DateTime)} is not supported.");

    /// <inheritdoc />
    decimal IConvertible.ToDecimal(IFormatProvider provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Decimal)} is not supported.");

    /// <inheritdoc />
    double IConvertible.ToDouble(IFormatProvider provider) => _value ? (double)1 : default;

    /// <inheritdoc />
    short IConvertible.ToInt16(IFormatProvider provider) => _value ? (short)1 : default;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider provider) => _value ? 1 : default;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider provider) => _value ? (long)1 : default;

    /// <inheritdoc />
    sbyte IConvertible.ToSByte(IFormatProvider provider) => _value ? (sbyte)1 : default;

    /// <inheritdoc />
    float IConvertible.ToSingle(IFormatProvider provider) => _value ? (float)1 : default;

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
    ushort IConvertible.ToUInt16(IFormatProvider provider) => _value ? (ushort)1 : default;

    /// <inheritdoc />
    uint IConvertible.ToUInt32(IFormatProvider provider) => _value ? (uint)1 : default;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider provider) => _value ? (ulong)1 : default;

    /// <summary>
    /// Converts the current atomic type to the specified atomic type.
    /// </summary>
    /// <param name="conversionType">The atomic type to convert to.</param>
    /// <returns>A <see cref="object"/> representing the converted atomic type value.</returns>
    /// <exception cref="InvalidCastException">The specified type is not a valid atomic type.</exception>
    private object ToAtomic(Type conversionType)
    {
        if (conversionType == typeof(BOOL))
            return new BOOL(_value);
        if (conversionType == typeof(SINT))
            return new SINT(_value ? (sbyte)1 : default);
        if (conversionType == typeof(INT))
            return new INT(_value ? (short)1 : default);
        if (conversionType == typeof(DINT))
            return new DINT(_value ? 1 : default);
        if (conversionType == typeof(LINT))
            return new LINT(_value ? (long)1 : default);
        if (conversionType == typeof(REAL))
            return new REAL(_value ? (float)1 : default);
        if (conversionType == typeof(LREAL))
            return new LREAL(_value ? (double)1 : default);
        if (conversionType == typeof(USINT))
            return new USINT(_value ? (byte)1 : default);
        if (conversionType == typeof(UINT))
            return new UINT(_value ? (ushort)1 : default);
        if (conversionType == typeof(UDINT))
            return new UDINT(_value ? (uint)1 : default);
        if (conversionType == typeof(ULINT))
            return new ULINT(_value ? (ulong)1 : default);

        throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.");
    }

    #endregion
}