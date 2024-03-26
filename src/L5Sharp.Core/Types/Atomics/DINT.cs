using System;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>DINT</b> Logix atomic data type, or a type analogous to a <see cref="int"/>.
/// </summary>
[L5XType(nameof(DINT))]
public sealed class DINT : AtomicType, IComparable, IConvertible, ILogixParsable<DINT>
{
    /// <summary>
    /// The value of the underlying data parsed to the corresponding primitive value type.
    /// </summary>
    private new int Value
    {
        get
        {
            var value = Radix.ParseValue(base.Value);
            return value is int typed ? typed : (int)Convert.ChangeType(value, typeof(int));
        }
    }

    /// <inheritdoc />
    public DINT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new default <see cref="DINT"/> type.
    /// </summary>
    public DINT() : base(CreateElement(nameof(DINT), Radix.Decimal, 0))
    {
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public DINT(int value) : base(CreateElement(nameof(DINT), Radix.Decimal, value))
    {
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Core.Radix"/> number format of the value.</param>
    /// <exception cref="ArgumentNullException"><c>radix</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>radix</c> is not supported by the atomic type.</exception>
    public DINT(Radix radix) : base(CreateElement(nameof(DINT), radix, 0))
    {
    }

    /// <summary>
    /// Creates a new <see cref="DINT"/> with the provided value and radix format.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    /// <exception cref="ArgumentNullException"><c>radix</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>radix</c> is not supported by the atomic type.</exception>
    public DINT(int value, Radix radix) : base(CreateElement(nameof(DINT), radix, value))
    {
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            DINT typed => Value.CompareTo(typed.Value),
            AtomicType atomic => Value.CompareTo((int)Convert.ChangeType(atomic, typeof(int))),
            ValueType value => Value.CompareTo((int)Convert.ChangeType(value, typeof(int))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            DINT value => Value == value.Value,
            AtomicType atomic => Value.Equals((int)Convert.ChangeType(atomic, typeof(int))),
            ValueType value => Value.Equals(Convert.ChangeType(value, typeof(int))),
            string value => ToString(Radix.Infer(value)).Equals(value, StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Parses a string into a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="DINT"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public new static DINT Parse(string value)
    {
        if (int.TryParse(value, out var result))
            return new DINT(result);

        var radix = Radix.Infer(value);
        var atomic = radix.ParseValue(value);
        var converted = (int)Convert.ChangeType(atomic, typeof(int));
        return new DINT(converted, radix);
    }

    /// <summary>
    /// Tries to parse a string into a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed <see cref="DINT"/> value if successful; Otherwise, <c>null</c>.</returns>
    public new static DINT? TryParse(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return default;

        if (int.TryParse(value, out var primitive))
            return new DINT(primitive);

        if (!Radix.TryInfer(value, out var radix))
            return default;

        var parsed = radix.ParseValue(value);
        var converted = (int)Convert.ChangeType(parsed, typeof(int));
        return new DINT(converted, radix);
    }

    // Contains the implicit .NET conversions for the type.

    #region Conversions

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
    public static implicit operator int(DINT atomic) => atomic.Value;

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

    #endregion

    // Contains the IConvertible implementation for the type. I am explicitly implementing this interface for each
    // atomic type to avoid polluting the API, and to have the implementation as performant as possible.
    // To perform conversion, use the recommended .NET Convert.ChangeType() method and specify the target type.

    #region Convertible

    /// <inheritdoc />
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <inheritdoc />
    bool IConvertible.ToBoolean(IFormatProvider? provider) => Value != 0;

    /// <inheritdoc />
    byte IConvertible.ToByte(IFormatProvider? provider) => (byte)Value;

    /// <inheritdoc />
    char IConvertible.ToChar(IFormatProvider? provider) => (char)Value;

    /// <inheritdoc />
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(DateTime)} is not supported.");

    /// <inheritdoc />
    decimal IConvertible.ToDecimal(IFormatProvider? provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Decimal)} is not supported.");

    /// <inheritdoc />
    double IConvertible.ToDouble(IFormatProvider? provider) => Value;

    /// <inheritdoc />
    short IConvertible.ToInt16(IFormatProvider? provider) => (short)Value;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider? provider) => Value;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider? provider) => Value;

    /// <inheritdoc />
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => (sbyte)Value;

    /// <inheritdoc />
    float IConvertible.ToSingle(IFormatProvider? provider) => Value;

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
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => (ushort)Value;

    /// <inheritdoc />
    uint IConvertible.ToUInt32(IFormatProvider? provider) => (uint)Value;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => (ulong)Value;

    /// <summary>
    /// Converts the current atomic type to the specified atomic type.
    /// </summary>
    /// <param name="conversionType">The atomic type to convert to.</param>
    /// <returns>A <see cref="object"/> representing the converted atomic type value.</returns>
    /// <exception cref="InvalidCastException">The specified type is not a valid atomic type.</exception>
    private object ToAtomic(Type conversionType)
    {
        if (conversionType == typeof(BOOL))
            return new BOOL(Value != 0);
        if (conversionType == typeof(SINT))
            return new SINT((sbyte)Value);
        if (conversionType == typeof(INT))
            return new INT((short)Value);
        if (conversionType == typeof(DINT))
            return new DINT(Value);
        if (conversionType == typeof(LINT))
            return new LINT(Value);
        if (conversionType == typeof(REAL))
            return new REAL(Value);
        if (conversionType == typeof(LREAL))
            return new LREAL(Value);
        if (conversionType == typeof(USINT))
            return new USINT((byte)Value);
        if (conversionType == typeof(UINT))
            return new UINT((ushort)Value);
        if (conversionType == typeof(UDINT))
            return new UDINT((uint)Value);
        if (conversionType == typeof(ULINT))
            return new ULINT((ulong)Value);

        throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.");
    }

    #endregion
}