using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a <i>BOOL</i> Logix atomic data type or a type analogous to a <see cref="bool"/>. This object is meant
/// to wrap the DataValue or DataValueMember data for the L5X tag data structure.
/// </summary>
[LogixData(nameof(BOOL), true)]
public sealed class BOOL : AtomicData, IComparable, IConvertible, IAtomicValue<bool>
{
    /// <inheritdoc />
    public BOOL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new default <see cref="BOOL"/> type.
    /// </summary>
    public BOOL() : base(nameof(BOOL))
    {
    }

    /// <summary>
    /// Creates a new <see cref="BOOL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    public BOOL(bool value) : this()
    {
        Element.SetAttributeValue(L5XName.Value, value ? "1" : "0");
    }

    /// <summary>
    /// Creates a new <see cref="BOOL"/> with the provided value.
    /// </summary>
    /// <param name="value">The <see cref="int"/> value to initialize the type with. All non-zero value
    /// will be evaluated as <c>true</c>.</param>
    public BOOL(int value) : this()
    {
        Element.SetAttributeValue(L5XName.Value, value != 0 ? "1" : "0");
    }

    /// <inheritdoc />
    public bool Value
    {
        get => GetAtomicValue<bool>();
        set => SetAtomicValue(value);
    }

    /// <inheritdoc />
    public override int GetSize() => sizeof(bool);


    /// <inheritdoc />
    /// <remarks>
    /// Standalone BOOL tag logic (takes 1 byte in Logix)
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        if (offset < 0 || offset > data.Length - 1)
            throw new ArgumentException("Offset is out of range for the provided data array.", nameof(offset));

        var value = data[offset] != 0;
        UpdateData(value);
        return offset + GetSize();
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            BOOL typed => Value.CompareTo(typed.Value),
            AtomicData atomic => Value.CompareTo((bool)Convert.ChangeType(atomic, typeof(bool))),
            ValueType value => Value.CompareTo((bool)Convert.ChangeType(value, typeof(bool))),
            _ => throw new ArgumentException($"Cannot compare logix type {obj.GetType().Name} with {GetType().Name}.")
        };
    }
    
    /// <inheritdoc />
    public override void ClearData()
    {
        var value = Radix.Format(0);
        Element.SetAttributeValue(L5XName.Value, value);
        Element.Annotation<XAttribute>()?.SetValue(value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            BOOL value => value.Value == Value,
            AtomicData value => Value.Equals((bool)Convert.ChangeType(value, typeof(bool))),
            ValueType value => Value.Equals(Convert.ChangeType(value, typeof(bool))),
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
    /// Parses the specified string representation of a <see cref="BOOL"/> value into its
    /// corresponding <see cref="BOOL"/> object.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="BOOL"/> value to parse.</param>
    /// <returns>A <see cref="BOOL"/> object that represents the parsed value.</returns>
    public new static BOOL Parse(string value)
    {
        var radix = Radix.Infer(value);
        var typed = radix.Parse<bool>(value);
        var formatted = radix.Format(typed);
        return new BOOL(CreateDataElement(nameof(BOOL), radix, formatted));
    }

    /// <summary>
    /// Attempts to parse a string representation of a <see cref="BOOL"/> value and creates an instance of
    /// the <see cref="BOOL"/> if successful.
    /// </summary>
    /// <param name="value">The string value to be parsed.</param>
    /// <param name="atomic">When this method returns, contains the <see cref="BOOL"/> instance equivalent to the
    /// string value, if the parse operation succeeded; otherwise, null.</param>
    /// <returns>True if the value was successfully parsed; otherwise, false.</returns>
    public static bool TryParse(string? value, out BOOL atomic)
    {
        atomic = null!;

        if (value is null || value.IsEmpty())
            return false;

        if (Radix.TryInfer(value, out var radix))
        {
            var typed = radix.Parse<bool>(value);
            var formatted = radix.Format(typed);
            atomic = new BOOL(CreateDataElement(nameof(BOOL), radix, formatted));
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
    public static implicit operator bool(BOOL atomic) => atomic.Value;

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
    public static implicit operator int(BOOL atomic) => atomic.Value ? 1 : 0;

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
    // atomic type to avoid polluting the API and to have the implementation as performant as possible.
    // To perform conversion, use the recommended .NET Convert.ChangeType() method and specify the target type.

    #region Convertible

    /// <inheritdoc />
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <inheritdoc />
    bool IConvertible.ToBoolean(IFormatProvider? provider) => Value;

    /// <inheritdoc />
    byte IConvertible.ToByte(IFormatProvider? provider) => Value ? (byte)1 : default;

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
    double IConvertible.ToDouble(IFormatProvider? provider) => Value ? (double)1 : 0;

    /// <inheritdoc />
    short IConvertible.ToInt16(IFormatProvider? provider) => Value ? (short)1 : default;

    /// <inheritdoc />
    int IConvertible.ToInt32(IFormatProvider? provider) => Value ? 1 : 0;

    /// <inheritdoc />
    long IConvertible.ToInt64(IFormatProvider? provider) => Value ? (long)1 : 0;

    /// <inheritdoc />
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => Value ? (sbyte)1 : default;

    /// <inheritdoc />
    float IConvertible.ToSingle(IFormatProvider? provider) => Value ? (float)1 : 0;

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
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => Value ? (ushort)1 : default;

    /// <inheritdoc />
    uint IConvertible.ToUInt32(IFormatProvider? provider) => Value ? (uint)1 : 0;

    /// <inheritdoc />
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => Value ? (ulong)1 : 0;

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
            _ when conversionType == typeof(BOOL) => new BOOL(Value),
            _ when conversionType == typeof(SINT) => new SINT(Value ? (sbyte)1 : default),
            _ when conversionType == typeof(INT) => new INT(Value ? (short)1 : default),
            _ when conversionType == typeof(DINT) => new DINT(Value ? 1 : 0),
            _ when conversionType == typeof(LINT) => new LINT(Value ? 1 : 0),
            _ when conversionType == typeof(REAL) => new REAL(Value ? 1 : 0),
            _ when conversionType == typeof(LREAL) => new LREAL(Value ? 1 : 0),
            _ when conversionType == typeof(USINT) => new USINT(Value ? (byte)1 : default),
            _ when conversionType == typeof(UINT) => new UINT(Value ? (ushort)1 : default),
            _ when conversionType == typeof(UDINT) => new UDINT(Value ? (uint)1 : 0),
            _ when conversionType == typeof(ULINT) => new ULINT(Value ? (ulong)1 : 0),
            _ when conversionType == typeof(DT) => new DT(Value ? 1 : 0),
            _ when conversionType == typeof(LDT) => new LDT(Value ? 1 : 0),
            _ when conversionType == typeof(TIME32) => new TIME32(Value ? 1 : 0),
            _ when conversionType == typeof(TIME) => new TIME(Value ? 1 : 0),
            _ when conversionType == typeof(LTIME) => new LTIME(Value ? 1 : 0),
            _ => throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.")
        };
    }

    #endregion
}