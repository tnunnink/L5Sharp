using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>SINT</b> Logix atomic data type, or a type analogous to <see cref="sbyte"/>.
/// </summary>
public sealed class SINT : AtomicData, IComparable, IConvertible, ILogixParsable<SINT>
{
    /// <summary>
    /// The underlying primitive value which is set upon construction and not changed.
    /// </summary>
    private readonly sbyte _value;

    /// <summary>
    /// Creates a new default <see cref="SINT"/> type.
    /// </summary>
    public SINT()
    {
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public SINT(sbyte value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Core.Radix"/> number format of the value.</param>
    public SINT(Radix radix) : base(radix)
    {
    }

    /// <summary>
    /// Creates a new <see cref="SINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public SINT(sbyte value, Radix radix) : base(radix)
    {
        _value = value;
    }
    
    /// <inheritdoc />
    public override string Name => nameof(SINT);

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            SINT typed => _value.CompareTo(typed._value),
            AtomicData atomic => _value.CompareTo((sbyte)Convert.ChangeType(atomic, typeof(sbyte))),
            ValueType value => _value.CompareTo((sbyte)Convert.ChangeType(value, typeof(sbyte))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            SINT value => value._value == _value,
            AtomicData atomic => _value.Equals((sbyte)Convert.ChangeType(atomic, typeof(sbyte))),
            ValueType value => _value.Equals(Convert.ChangeType(value, typeof(sbyte))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Parses a string into a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="SINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public new static SINT Parse(string value)
    {
        if (sbyte.TryParse(value, out var result))
            return new SINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.ParseValue(value);
        var converted = (sbyte)Convert.ChangeType(atomic, typeof(sbyte));
        return new SINT(converted, radix);
    }

    /// <summary>
    /// Tries to parse a string into a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed <see cref="SINT"/> value if successful; Otherwise, <c>null</c>.</returns>
    public new static SINT? TryParse(string? value)
    {
        if (value is null || value.IsEmpty())
            return default;

        if (sbyte.TryParse(value, out var primitive))
            return new SINT(primitive);

        if (!Radix.TryInfer(value, out var radix))
            return default;

        var parsed = radix.ParseValue(value);
        var converted = (sbyte)Convert.ChangeType(parsed, typeof(sbyte));
        return new SINT(converted, radix);
    }

    // Contains the implicit .NET conversions for the type.

    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="sbyte"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> value.</returns>
    public static implicit operator SINT(sbyte value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="SINT"/> to a <see cref="sbyte"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="sbyte"/> type value.</returns>
    public static implicit operator sbyte(SINT atomic) => atomic._value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="SINT"/> value.</returns>
    public static implicit operator SINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="SINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(SINT value) => value.ToString();

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
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => _value;

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
            return new SINT(_value);
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