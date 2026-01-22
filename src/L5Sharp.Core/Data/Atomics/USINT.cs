using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <b>USINT</b> Logix atomic data type or a type analogous to a <see cref="byte"/>.
/// </summary>
[LogixData(nameof(USINT), true)]
public sealed class USINT : AtomicData, IComparable, IConvertible, IAtomicValue<byte>
{
    /// <inheritdoc />
    public USINT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new default <see cref="USINT"/> type.
    /// </summary>
    public USINT() : base(nameof(USINT))
    {
    }

    /// <summary>
    /// Creates a new <see cref="USINT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public USINT(byte value) : this()
    {
        Element.SetAttributeValue(L5XName.Value, value);
    }

    /// <inheritdoc />
    public override int Size => sizeof(byte);

    /// <inheritdoc />
    public byte Value
    {
        get => GetAtomicValue<byte>();
        set => SetAtomicValue(value);
    }

    /// <inheritdoc />
    public override int Update(byte[] data, int offset)
    {
        if (offset < 0 || offset > data.Length - 1)
            throw new ArgumentException("Offset is out of range for the provided data array.", nameof(offset));

        var value = data[offset];
        Update(value);
        return offset + Size;
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            USINT typed => Value.CompareTo(typed.Value),
            AtomicData atomic => Value.CompareTo((byte)Convert.ChangeType(atomic, typeof(byte))),
            ValueType value => Value.CompareTo((byte)Convert.ChangeType(value, typeof(byte))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            USINT value => value.Value == Value,
            AtomicData atomic => Value.Equals((byte)Convert.ChangeType(atomic, typeof(byte))),
            ValueType value => Value.Equals(Convert.ChangeType(value, typeof(byte))),
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
    /// Parses the specified string representation of a <see cref="USINT"/> value into its corresponding <see cref="USINT"/> object.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="USINT"/> value to parse.</param>
    /// <returns>A <see cref="USINT"/> object that represents the parsed value.</returns>
    public new static USINT Parse(string value)
    {
        var radix = Radix.Infer(value);
        var typed = radix.Parse<byte>(value);
        var formatted = radix.Format(typed);
        return new USINT(CreateDataElement(nameof(USINT), radix, formatted));
    }

    /// <summary>
    /// Attempts to parse a string representation of a <see cref="USINT"/> value and creates an instance of the <see cref="USINT"/> class if successful.
    /// </summary>
    /// <param name="value">The string value to be parsed.</param>
    /// <param name="atomic">When this method returns, contains the <see cref="USINT"/> instance equivalent to the string value, if the parse operation succeeded; otherwise, null.</param>
    /// <returns>True if the value was successfully parsed; otherwise, false.</returns>
    public static bool TryParse(string? value, out USINT atomic)
    {
        atomic = null!;

        if (value is null || value.IsEmpty())
            return false;

        if (Radix.TryInfer(value, out var radix))
        {
            var typed = radix.Parse<byte>(value);
            atomic = new USINT(typed);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public override byte[] ToBytes()
    {
        return [Value];
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
    public static implicit operator byte(USINT atomic) => atomic.Value;

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
    // atomic type to avoid polluting the API and to have the implementation as performant as possible.
    // To perform conversion, use the recommended .NET Convert.ChangeType() method and specify the target type.

    #region Convertible

    /// <inheritdoc />
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <inheritdoc />
    bool IConvertible.ToBoolean(IFormatProvider? provider) => Value != 0;

    /// <inheritdoc />
    byte IConvertible.ToByte(IFormatProvider? provider) => Value;

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
    short IConvertible.ToInt16(IFormatProvider? provider) => Value;

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
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => Value;

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
        return conversionType switch
        {
            _ when conversionType == typeof(BOOL) => new BOOL(Value != 0),
            _ when conversionType == typeof(SINT) => new SINT((sbyte)Value),
            _ when conversionType == typeof(INT) => new INT(Value),
            _ when conversionType == typeof(DINT) => new DINT(Value),
            _ when conversionType == typeof(LINT) => new LINT(Value),
            _ when conversionType == typeof(REAL) => new REAL(Value),
            _ when conversionType == typeof(LREAL) => new LREAL(Value),
            _ when conversionType == typeof(USINT) => new USINT(Value),
            _ when conversionType == typeof(UINT) => new UINT(Value),
            _ when conversionType == typeof(UDINT) => new UDINT(Value),
            _ when conversionType == typeof(ULINT) => new ULINT(Value),
            _ when conversionType == typeof(DT) => new DT(Value),
            _ when conversionType == typeof(LDT) => new LDT(Value),
            _ when conversionType == typeof(TIME32) => new TIME32(Value),
            _ when conversionType == typeof(TIME) => new TIME(Value),
            _ when conversionType == typeof(LTIME) => new LTIME(Value),
            _ => throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.")
        };
    }

    #endregion
}