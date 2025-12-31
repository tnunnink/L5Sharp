using System;
using System.Globalization;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>REAL</b> Logix atomic data type or a type analogous to a <see cref="float"/>.
/// </summary>
[LogixData(nameof(REAL), true)]
public sealed class REAL : AtomicData, IComparable, IConvertible, IAtomicValue<float>
{
    private const string SingleFormat = "0.0######";

    /// <inheritdoc />
    public REAL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new default <see cref="REAL"/> type.
    /// </summary>
    public REAL() : base(nameof(REAL), "0.0")
    {
    }

    /// <summary>
    /// Creates a new <see cref="REAL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public REAL(float value) : this()
    {
        Element.SetAttributeValue(L5XName.Value, value.ToString(SingleFormat, CultureInfo.InvariantCulture));
    }
    
    /// <inheritdoc />
    public override int Size => sizeof(float);

    /// <inheritdoc />
    public float Value => Element.Attribute(L5XName.Value)?.Value.Contains("QNAN") is false
        ? GetAtomicValue<float>()
        : float.NaN;
    
    /// <inheritdoc />
    public override int Update(byte[] data, int offset)
    {
        // If the size of this type overflows the boundary, we need to start at the next interval.
        // This can happen for only typs larger than 1 byte.
        offset = (offset + Size - 1) & ~(Size - 1);

        var value = BitConverter.ToSingle(data, offset);
        Update(value);

        return offset + Size;
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            REAL typed => Value.CompareTo(typed.Value),
            AtomicData atomic => Value.CompareTo((float)Convert.ChangeType(atomic, typeof(float))),
            ValueType value => Value.CompareTo((float)Convert.ChangeType(value, typeof(float))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            REAL value => Math.Abs(Value - value.Value) < float.Epsilon,
            AtomicData atomic => Value.Equals((float)Convert.ChangeType(atomic, typeof(float))),
            ValueType value => Value.Equals(Convert.ChangeType(value, typeof(float))),
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
    public override string ToString(Radix radix) => radix.Format(Value);

    /// <summary>
    /// Parses the specified string representation of a <see cref="REAL"/> value into its corresponding <see cref="REAL"/> object.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="REAL"/> value to parse.</param>
    /// <returns>A <see cref="REAL"/> object that represents the parsed value.</returns>
    public new static REAL Parse(string value)
    {
        var radix = Radix.Infer(value);
        var typed = radix.Parse<float>(value);
        var formatted = radix.Format(typed);
        return new REAL(CreateDataElement(nameof(REAL), radix, formatted));
    }

    /// <summary>
    /// Attempts to parse a string representation of a <see cref="REAL"/> value and creates an instance of the <see cref="REAL"/> class if successful.
    /// </summary>
    /// <param name="value">The string value to be parsed.</param>
    /// <param name="atomic">When this method returns, contains the <see cref="REAL"/> instance equivalent to the string value, if the parse operation succeeded; otherwise, null.</param>
    /// <returns>True if the value was successfully parsed; otherwise, false.</returns>
    public static bool TryParse(string? value, out REAL atomic)
    {
        atomic = null!;

        if (value is null || value.IsEmpty())
            return false;

        if (Radix.TryInfer(value, out var radix))
        {
            var typed = radix.Parse<float>(value);
            atomic = new REAL(typed);
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
    public static implicit operator float(REAL atomic) => atomic.Value;

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
    bool IConvertible.ToBoolean(IFormatProvider? provider) => Value != 0;

    /// <inheritdoc />
    byte IConvertible.ToByte(IFormatProvider? provider) => (byte)Value;

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
    double IConvertible.ToDouble(IFormatProvider? provider) => Value;

    /// <inheritdoc />
    short IConvertible.ToInt16(IFormatProvider? provider) => (short)Value;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider? provider) => (int)Value;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider? provider) => (long)Value;

    /// <inheritdoc />
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => (sbyte)Value;

    /// <inheritdoc />
    float IConvertible.ToSingle(IFormatProvider? provider) => Value;

    /// <inheritdoc />
    string IConvertible.ToString(IFormatProvider? provider) => ToString();

    /// <inheritdoc />
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
    {
        IConvertible convertible = this;

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
            TypeCode.DBNull => throw new InvalidCastException("Conversion for type code 'DbNull' not supported."),
            _ => throw new InvalidCastException($"Conversion for {conversionType.Name} not supported.")
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
        return conversionType switch
        {
            _ when conversionType == typeof(BOOL) => new BOOL(Value != 0),
            _ when conversionType == typeof(SINT) => new SINT((sbyte)Value),
            _ when conversionType == typeof(INT) => new INT((short)Value),
            _ when conversionType == typeof(DINT) => new DINT((int)Value),
            _ when conversionType == typeof(LINT) => new LINT((long)Value),
            _ when conversionType == typeof(REAL) => new REAL(Value),
            _ when conversionType == typeof(LREAL) => new LREAL(Value),
            _ when conversionType == typeof(USINT) => new USINT((byte)Value),
            _ when conversionType == typeof(UINT) => new UINT((ushort)Value),
            _ when conversionType == typeof(UDINT) => new UDINT((uint)Value),
            _ when conversionType == typeof(ULINT) => new ULINT((ulong)Value),
            _ when conversionType == typeof(DT) => new DT((long)Value),
            _ when conversionType == typeof(LDT) => new LDT((long)Value),
            _ when conversionType == typeof(TIME32) => new TIME32((int)Value),
            _ when conversionType == typeof(TIME) => new TIME((long)Value),
            _ when conversionType == typeof(LTIME) => new LTIME((long)Value),
            _ => throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.")
        };
    }

    #endregion
}