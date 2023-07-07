using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="L5Sharp.LogixType"/> that represents value type object.
/// </summary>
/// <remarks>
/// Logix atomic types are types that have value (i.e. BOOL, SINT, INT, DINT, REAL, etc.).
/// These type are synonymous with value types in .NET. This is the common abstract class for all atomic types.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class AtomicType : LogixType, IConvertible
{
    /// <inheritdoc />
    public sealed override DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public sealed override DataTypeClass Class => DataTypeClass.Atomic;

    /// <inheritdoc />
    public override IEnumerable<Member> Members
    {
        get
        {
            var bits = new BitArray(GetBytes());
            for (var i = 0; i < bits.Count; i++)
            {
                var member = new Member(i.ToString(), new BOOL(bits[i]));
                member.DataType.DataChanged += OnMemberDataChanged;
                yield return member;
            }
        }
    }

    /// <summary>
    /// The radix format for the <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> representing the format of the atomic type value.</value>
    public abstract Radix Radix { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not AtomicType atomic) return false;

        var a = GetBytes();
        var b = atomic.GetBytes();

        var max = Math.Max(a.Length, b.Length);

        for (var i = 0; i < max; i++)
        {
            var left = i < a.Length ? a[i] : (byte)0;
            var right = i < b.Length ? b[i] : (byte)0;
            if (left != right) return false;
        }

        return true;
    }

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as an array of <see cref="byte"/> values.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the value of the type.</returns>
    public abstract byte[] GetBytes();

    /// <inheritdoc />
    public override int GetHashCode() => GetBytes().Aggregate(0, (i, b) => i ^ b.GetHashCode());

    /// <summary>
    /// Return the atomic value formatted using the current <see cref="Radix"/> format.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public override string ToString() => Radix.Format(this);

    /// <summary>
    /// Returns the atomic value formatted in the specified <see cref="Enums.Radix"/> format.
    /// </summary>
    /// <param name="radix">The radix format.</param>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public string ToString(Radix radix) => radix.Format(this);

    /// <summary>
    /// Serialized the atomic type as the DataValue <see cref="XElement"/>.
    /// </summary>
    /// <returns>A <see cref="XElement"/> containing the data for the atomic type.</returns>
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, Name));
        element.Add(new XAttribute(L5XName.Radix, Radix));
        element.Add(new XAttribute(L5XName.Value, ToString()));
        return element;
    }

    /// <summary>
    /// Returns a new byte array updated with the provided byte array data.
    /// </summary>
    /// <param name="other">The array of <see cref="byte"/> to update the underlying value with.</param>
    /// <returns>A new array of <see cref="byte"/> containing the updated data.</returns>
    /// <remarks>
    /// This method can be used when setting data between mismatched atomic types (i.e. type conversion).
    /// Obviously data loss or overflow may occur depending on the length and values of the byte arrays.
    /// This will always return a byte array of the same length as the byte array for the current value to
    /// ensure it can be converted via <see cref="BitConverter"/> back into the .NET integral type.
    /// </remarks>
    protected byte[] SetBytes(byte[] other)
    {
        var value = GetBytes();

        for (var i = 0; i < value.Length; i++)
            value[i] = i < other.Length ? other[i] : default;

        return value;
    }

    /// <inheritdoc />
    public TypeCode GetTypeCode() => TypeCode.Object;

    /// <summary>
    /// Converts the current atomic type to the specified atomic type.
    /// </summary>
    /// <param name="conversionType">The atomic type to convert to.</param>
    /// <param name="provider"></param>
    /// <returns>A <see cref="object"/> representing the converted atomic type value.</returns>
    /// <exception cref="InvalidCastException">The specified type is not a valid atomic type.</exception>
    private object ToAtomic(Type conversionType, IFormatProvider provider)
    {
        if (conversionType == typeof(BOOL))
            return new BOOL(ToBoolean(provider));
        if (conversionType == typeof(SINT))
            return new SINT(ToSByte(provider));
        if (conversionType == typeof(INT))
            return new INT(ToInt16(provider));
        if (conversionType == typeof(DINT))
            return new DINT(ToInt32(provider));
        if (conversionType == typeof(LINT))
            return new LINT(ToInt64(provider));
        if (conversionType == typeof(REAL))
            return new REAL(ToSingle(provider));
        if (conversionType == typeof(USINT))
            return new USINT(ToByte(provider));
        if (conversionType == typeof(UINT))
            return new UINT(ToUInt16(provider));
        if (conversionType == typeof(UDINT))
            return new UDINT(ToUInt32(provider));
        if (conversionType == typeof(ULINT))
            return new ULINT(ToUInt64(provider));
        if (conversionType == typeof(LREAL))
            return new LREAL(ToDouble(provider));

        throw new InvalidCastException($"Cannot convert from {GetType().Name} to {conversionType.Name}.");
    }

    /// <inheritdoc />
    public virtual bool ToBoolean(IFormatProvider provider) => BitConverter.ToBoolean(GetBytes());

    /// <inheritdoc />
    public virtual byte ToByte(IFormatProvider provider) => GetBytes()[0];

    /// <inheritdoc />
    public virtual char ToChar(IFormatProvider provider) => BitConverter.ToChar(GetBytes());

    /// <inheritdoc />
    public virtual DateTime ToDateTime(IFormatProvider provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(DateTime)} is not supported.");

    /// <inheritdoc />
    public decimal ToDecimal(IFormatProvider provider) =>
        throw new InvalidCastException($"Conversion from {Name} to {nameof(Decimal)} is not supported.");

    /// <inheritdoc />
    public virtual double ToDouble(IFormatProvider provider) => BitConverter.ToDouble(GetBytes());

    /// <inheritdoc />
    public virtual short ToInt16(IFormatProvider provider) => BitConverter.ToInt16(GetBytes());

    /// <inheritdoc />
    public virtual int ToInt32(IFormatProvider provider) => BitConverter.ToInt32(GetBytes());

    /// <inheritdoc />
    public virtual long ToInt64(IFormatProvider provider) => BitConverter.ToInt64(GetBytes());

    /// <inheritdoc />
    public virtual sbyte ToSByte(IFormatProvider provider) => unchecked((sbyte)GetBytes()[0]);

    /// <inheritdoc />
    public virtual float ToSingle(IFormatProvider provider) => BitConverter.ToSingle(GetBytes());

    /// <inheritdoc />
    string IConvertible.ToString(IFormatProvider provider) => ToString();

    /// <inheritdoc />
    public object ToType(Type conversionType, IFormatProvider provider)
    {
        switch (Type.GetTypeCode(conversionType))
        {
            case TypeCode.Boolean:
                return ToBoolean(provider);
            case TypeCode.Byte:
                return ToByte(provider);
            case TypeCode.Char:
                return ToChar(provider);
            case TypeCode.DateTime:
                return ToDateTime(provider);
            case TypeCode.Decimal:
                return ToDecimal(provider);
            case TypeCode.Double:
                return ToDouble(provider);
            case TypeCode.Empty:
                throw new ArgumentNullException(nameof(conversionType));
            case TypeCode.Int16:
                return ToInt16(provider);
            case TypeCode.Int32:
                return ToInt32(provider);
            case TypeCode.Int64:
                return ToInt64(provider);
            case TypeCode.Object:
                return ToAtomic(conversionType, provider);
            case TypeCode.SByte:
                return ToSByte(provider);
            case TypeCode.Single:
                return ToSingle(provider);
            case TypeCode.String:
                IConvertible iconv = this;
                return iconv.ToString(provider);
            case TypeCode.UInt16:
                return ToUInt16(provider);
            case TypeCode.UInt32:
                return ToUInt32(provider);
            case TypeCode.UInt64:
                return ToUInt64(provider);
            case TypeCode.DBNull:
                throw new InvalidCastException("Conversion for type code 'DbNull' not supported by AtomicType.");
            default:
                throw new InvalidCastException($"Conversion for {conversionType.Name} not supported by AtomicType.");
        }
    }

    /// <inheritdoc />
    public virtual ushort ToUInt16(IFormatProvider provider) => BitConverter.ToUInt16(GetBytes());

    /// <inheritdoc />
    public virtual uint ToUInt32(IFormatProvider provider) => BitConverter.ToUInt32(GetBytes());

    /// <inheritdoc />
    public virtual ulong ToUInt64(IFormatProvider provider) => BitConverter.ToUInt64(GetBytes());
    
    private void OnMemberDataChanged(object sender, EventArgs e)
    {
        RaiseDataChanged();
    }
}