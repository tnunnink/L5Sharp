using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="UDint"/> object.
    /// </summary>
    internal class ULintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(ushort) ||
                   sourceType == typeof(uint) ||
                   sourceType == typeof(ulong) ||
                   sourceType == typeof(USint) ||
                   sourceType == typeof(UInt) ||
                   sourceType == typeof(UDint) ||
                   sourceType == typeof(ULint) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new ULint(v),
                ushort v => new ULint(v),
                uint v => new ULint(v),
                ulong v => new ULint(v),
                USint v => new ULint(v.Value),
                UInt v => new ULint(v.Value),
                UDint v => new ULint(v.Value),
                ULint v => v,
                string v => ulong.TryParse(v, out var result) ? new ULint(result) : Radix.ParseValue<ULint>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(ulong) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(ULint) ||
                   destinationType == typeof(Real) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not ULint typed)
                throw new InvalidOperationException($"Value must be of type {typeof(ULint)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(ulong) => typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
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