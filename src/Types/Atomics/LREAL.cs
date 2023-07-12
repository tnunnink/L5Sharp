using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>LREAL</b> Logix atomic data type, or a type analogous to a <see cref="double"/>.
/// </summary>
public sealed class LREAL : AtomicType, IComparable, IConvertible
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
    
    // Contains the implicit .NET conversions for the type.

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
    byte IConvertible.ToByte(IFormatProvider provider) => (byte)_value;

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
    short IConvertible.ToInt16(IFormatProvider provider) => (short)_value;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider provider) => (int)_value;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider provider) => (long)_value;

    /// <inheritdoc />
    sbyte IConvertible.ToSByte(IFormatProvider provider) => (sbyte)_value;

    /// <inheritdoc />
    float IConvertible.ToSingle(IFormatProvider provider) => (float)_value;

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
    ushort IConvertible.ToUInt16(IFormatProvider provider) => (ushort)_value;

    /// <inheritdoc />
    uint IConvertible.ToUInt32(IFormatProvider provider) => (uint)_value;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider provider) => (ulong)_value;

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
            return new REAL((float)_value);
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