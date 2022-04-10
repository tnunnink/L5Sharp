using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A generic atomic <see cref="TypeConverter"/> implementation that specifies the conversion
    /// of <see cref="IAtomicType{T}"/> objects. Since the implementation of <see cref="CanConvertFrom"/>,
    /// <see cref="CanConvertTo"/>, and <see cref="ConvertTo"/> are the same for all atomics, we can create
    /// a base abstract class to contain the implementations. Note that we are allowing conversions to and from
    /// all primitive CLR types and the corresponding Logix types. These are checked conversions, meaning they will
    /// throw an <see cref="OverflowException"/> when the values are out of range for the destination type. 
    /// </summary>
    internal abstract class AtomicConverter<TAtomic> : TypeConverter where TAtomic : IAtomicType
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(sbyte) ||
                   sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(ushort) ||
                   sourceType == typeof(int) ||
                   sourceType == typeof(uint) ||
                   sourceType == typeof(long) ||
                   sourceType == typeof(ulong) ||
                   sourceType == typeof(float) ||
                   sourceType == typeof(SINT) ||
                   sourceType == typeof(USINT) ||
                   sourceType == typeof(INT) ||
                   sourceType == typeof(UINT) ||
                   sourceType == typeof(DINT) ||
                   sourceType == typeof(UDINT) ||
                   sourceType == typeof(LINT) ||
                   sourceType == typeof(ULINT) ||
                   sourceType == typeof(REAL) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(sbyte) ||
                   destinationType == typeof(byte) ||
                   destinationType == typeof(short) ||
                   destinationType == typeof(ushort) ||
                   destinationType == typeof(int) ||
                   destinationType == typeof(uint) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(ulong) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(SINT) ||
                   destinationType == typeof(USINT) ||
                   destinationType == typeof(INT) ||
                   destinationType == typeof(UINT) ||
                   destinationType == typeof(DINT) ||
                   destinationType == typeof(UDINT) ||
                   destinationType == typeof(LINT) ||
                   destinationType == typeof(ULINT) ||
                   destinationType == typeof(REAL) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not TAtomic typed)
                throw new InvalidOperationException($"Value must be of type {typeof(TAtomic)}.");

            checked
            {
                return destinationType switch
                {
                    not null when destinationType == typeof(sbyte) => Convert.ToSByte(typed.Value),
                    not null when destinationType == typeof(byte) => Convert.ToByte(typed.Value),
                    not null when destinationType == typeof(short) => Convert.ToInt16(typed.Value),
                    not null when destinationType == typeof(ushort) => Convert.ToUInt16(typed.Value),
                    not null when destinationType == typeof(int) => Convert.ToInt32(typed.Value),
                    not null when destinationType == typeof(uint) => Convert.ToUInt32(typed.Value),
                    not null when destinationType == typeof(long) => Convert.ToInt64(typed.Value),
                    not null when destinationType == typeof(ulong) => Convert.ToUInt64(typed.Value),
                    not null when destinationType == typeof(float) => Convert.ToSingle(typed.Value),
                    not null when destinationType == typeof(SINT) => new SINT(Convert.ToSByte(typed.Value)),
                    not null when destinationType == typeof(USINT) => new USINT(Convert.ToByte(typed.Value)),
                    not null when destinationType == typeof(INT) => new INT(Convert.ToInt16(typed.Value)),
                    not null when destinationType == typeof(UINT) => new UINT(Convert.ToUInt16(typed.Value)),
                    not null when destinationType == typeof(DINT) => new DINT(Convert.ToInt32(typed.Value)),
                    not null when destinationType == typeof(UDINT) => new UDINT(Convert.ToUInt32(typed.Value)),
                    not null when destinationType == typeof(LINT) => new LINT(Convert.ToInt64(typed.Value)),
                    not null when destinationType == typeof(ULINT) => new ULINT(Convert.ToUInt64(typed.Value)),
                    not null when destinationType == typeof(REAL) => new REAL(Convert.ToSingle(typed.Value)),
                    not null when destinationType == typeof(string) => typed.Format(),
                    _ => base.ConvertTo(context, culture, value, destinationType!) ??
                         throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}