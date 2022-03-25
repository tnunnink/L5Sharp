using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="UDINT"/> object.
    /// </summary>
    internal class ULintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(ushort) ||
                   sourceType == typeof(uint) ||
                   sourceType == typeof(ulong) ||
                   sourceType == typeof(USINT) ||
                   sourceType == typeof(UINT) ||
                   sourceType == typeof(UDINT) ||
                   sourceType == typeof(ULINT) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new ULINT(v),
                ushort v => new ULINT(v),
                uint v => new ULINT(v),
                ulong v => new ULINT(v),
                USINT v => new ULINT(v.Value),
                UINT v => new ULINT(v.Value),
                UDINT v => new ULINT(v.Value),
                ULINT v => v,
                string v => ulong.TryParse(v, out var result) ? new ULINT(result) : Radix.ParseValue<ULINT>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(ulong) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(ULINT) ||
                   destinationType == typeof(REAL) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not ULINT typed)
                throw new InvalidOperationException($"Value must be of type {typeof(ULINT)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(ulong) => typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(ULINT) => new ULINT(typed.Value),
                not null when destinationType == typeof(REAL) => new REAL(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}