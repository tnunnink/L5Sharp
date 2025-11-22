using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>UDINT</b> Logix atomic data type or a type analogous to a <see cref="uint"/>.
/// </summary>
[LogixData(nameof(UDINT), true)]
public sealed class UDINT : AtomicData, IComparable, IConvertible, IAtomicValue<uint>
{
    /// <inheritdoc />
    public UDINT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new default <see cref="UDINT"/> type.
    /// </summary>
    public UDINT() : base(nameof(UDINT))
    {
    }

    /// <summary>
    /// Creates a new <see cref="UDINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public UDINT(uint value) : this()
    {
        Element.SetAttributeValue(L5XName.Value, value);
    }

    /// <summary>
    /// Creates a new <see cref="UDINT"/> from the provided string value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <remarks>
    /// The radix format will be set based on the format of the provided value.
    /// </remarks>
    public UDINT(string value) : this()
    {
        var radix = Radix.Infer(value);
        var converted = radix.Parse<uint>(value);

        Element.SetAttributeValue(L5XName.Radix, radix);
        Element.SetAttributeValue(L5XName.Value, converted);
    }

    /// <inheritdoc />
    public uint Value => GetAtomicValue<uint>();

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            UDINT typed => Value.CompareTo(typed.Value),
            AtomicData atomic => Value.CompareTo((uint)Convert.ChangeType(atomic, typeof(uint))),
            ValueType value => Value.CompareTo((uint)Convert.ChangeType(value, typeof(uint))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            UDINT value => Value == value.Value,
            AtomicData atomic => Value.Equals((uint)Convert.ChangeType(atomic, typeof(uint))),
            ValueType value => Value.Equals(Convert.ChangeType(value, typeof(uint))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Return the atomic value formatted using the current <see cref="Radix"/> format.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public override string ToString() => Radix.Format(Value);

    /// <summary>
    /// Returns the atomic value formatted in the specified <see cref="Core.Radix"/> format.
    /// </summary>
    /// <param name="radix">The radix format.</param>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public string ToString(Radix radix) => radix.Format(Value);

    /// <summary>
    /// Parses the specified string representation of a <see cref="UDINT"/> value into its corresponding <see cref="UDINT"/> object.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="UDINT"/> value to parse.</param>
    /// <returns>A <see cref="UDINT"/> object that represents the parsed value.</returns>
    public static UDINT Parse(string value) => new(value);

    /// <summary>
    /// Attempts to parse a string representation of a <see cref="UDINT"/> value and creates an instance of the <see cref="UDINT"/> class if successful.
    /// </summary>
    /// <param name="value">The string value to be parsed.</param>
    /// <param name="atomic">When this method returns, contains the <see cref="UDINT"/> instance equivalent to the string value, if the parse operation succeeded; otherwise, null.</param>
    /// <returns>True if the value was successfully parsed; otherwise, false.</returns>
    public static bool TryParse(string? value, out UDINT atomic)
    {
        atomic = null!;

        if (value is null || value.IsEmpty())
            return false;

        if (Radix.TryInfer(value, out var radix))
        {
            var typed = radix.Parse<uint>(value);
            atomic = new UDINT(typed);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public override byte[] ToBytes()
    {
        return BitConverter.GetBytes(Value);
    }

    // Contains the implicit .NET conversions for the type.

    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="uint"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> value.</returns>
    public static implicit operator UDINT(uint value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="UDINT"/> to a <see cref="uint"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="uint"/> type value.</returns>
    public static implicit operator uint(UDINT atomic) => atomic.Value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="UDINT"/> value.</returns>
    public static implicit operator UDINT(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided <see cref="UDINT"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A new <see cref="string"/> value.</returns>
    public static implicit operator string(UDINT value) => value.ToString();

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
    int IConvertible.ToInt32(IFormatProvider? provider) => (int)Value;

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
    uint IConvertible.ToUInt32(IFormatProvider? provider) => Value;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => Value;

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
            return new DINT((int)Value);
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
            return new UDINT(Value);
        if (conversionType == typeof(ULINT))
            return new ULINT(Value);

        throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.");
    }

    #endregion
}