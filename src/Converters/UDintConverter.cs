using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Atomics;
using L5Sharp.Enums;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="UDint"/> object.
    /// </summary>
    internal class UDintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(ushort) ||
                   sourceType == typeof(uint) ||
                   sourceType == typeof(USint) ||
                   sourceType == typeof(UInt) ||
                   sourceType == typeof(UDint) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new UDint(v),
                ushort v => new UDint(v),
                uint v => new UDint(v),
                USint v => new UDint(v.Value),
                UInt v => new UDint(v.Value),
                UDint v => v,
                string v => uint.TryParse(v, out var result) ? new UDint(result) : Radix.ParseValue<UDint>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
        
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(uint) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(ulong) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(UDint) ||
                   destinationType == typeof(Lint) ||
                   destinationType == typeof(ULint) ||
                   destinationType == typeof(Real) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not UDint typed)
                throw new InvalidOperationException($"Value must be of type {typeof(UDint)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(uint) => typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(ulong) => (ulong)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(UDint) => new UDint(typed.Value),
                not null when destinationType == typeof(Lint) => new Lint(typed.Value),
                not null when destinationType == typeof(ULint) => new ULint(typed.Value),
                not null when destinationType == typeof(Real) => new Real(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}