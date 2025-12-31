using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents an <b>INT</b> Logix atomic data type or a type analogous to a <see cref="short"/>.
/// </summary>
[LogixData(nameof(INT), true)]
public sealed class INT : AtomicData, IComparable, IConvertible, IAtomicValue<short>
{
    /// <inheritdoc />
    public INT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new default <see cref="INT"/> type.
    /// </summary>
    public INT() : base(nameof(INT))
    {
    }

    /// <summary>
    /// Creates a new <see cref="INT"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public INT(short value) : this()
    {
        Element.SetAttributeValue(L5XName.Value, value.ToString());
    }

    /// <inheritdoc />
    public override int Size => sizeof(short);

    /// <inheritdoc />
    public short Value => GetAtomicValue<short>();

    /// <inheritdoc />
    public override int Update(byte[] data, int offset)
    {
        // If the size of this type overflows the boundary, we need to start at the next interval.
        // This can happen for only typs larger than 1 byte.
        offset = (offset + Size - 1) & ~(Size - 1);

        var value = BitConverter.ToInt16(data, offset);
        Update(value);

        return offset + Size;
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            INT typed => Value.CompareTo(typed.Value),
            AtomicData atomic => Value.CompareTo((short)Convert.ChangeType(atomic, typeof(short))),
            ValueType value => Value.CompareTo((short)Convert.ChangeType(value, typeof(short))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            INT value => value.Value == Value,
            AtomicData atomic => Value.Equals((short)Convert.ChangeType(atomic, typeof(short))),
            ValueType value => Value.Equals(Convert.ChangeType(value, typeof(short))),
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
    /// Parses the specified string representation of a <see cref="INT"/> value into its corresponding <see cref="INT"/> object.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="INT"/> value to parse.</param>
    /// <returns>A <see cref="INT"/> object that represents the parsed value.</returns>
    public new static INT Parse(string value)
    {
        var radix = Radix.Infer(value);
        var typed = radix.Parse<short>(value);
        var formatted = radix.Format(typed);
        return new INT(CreateDataElement(nameof(INT), radix, formatted));
    }

    /// <summary>
    /// Attempts to parse a string representation of a <see cref="INT"/> value and creates an instance of the <see cref="INT"/> class if successful.
    /// </summary>
    /// <param name="value">The string value to be parsed.</param>
    /// <param name="atomic">When this method returns, contains the <see cref="INT"/> instance equivalent to the string value,
    /// if the parse operation succeeded; otherwise, null.</param>
    /// <returns>True if the value was successfully parsed; otherwise, false.</returns>
    public static bool TryParse(string? value, out INT atomic)
    {
        atomic = null!;

        if (value is null || value.IsEmpty())
            return false;

        if (Radix.TryInfer(value, out var radix))
        {
            var typed = radix.Parse<short>(value);
            var formatted = radix.Format(typed);
            atomic = new INT(CreateDataElement(nameof(INT), radix, formatted));
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
    public static implicit operator short(INT atomic) => atomic.Value;

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
            _ when conversionType == typeof(INT) => new INT(Value),
            _ when conversionType == typeof(DINT) => new DINT(Value),
            _ when conversionType == typeof(LINT) => new LINT(Value),
            _ when conversionType == typeof(REAL) => new REAL(Value),
            _ when conversionType == typeof(LREAL) => new LREAL(Value),
            _ when conversionType == typeof(USINT) => new USINT((byte)Value),
            _ when conversionType == typeof(UINT) => new UINT((ushort)Value),
            _ when conversionType == typeof(UDINT) => new UDINT((uint)Value),
            _ when conversionType == typeof(ULINT) => new ULINT((ulong)Value),
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