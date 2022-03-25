using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="DINT"/> object.
    /// </summary>
    internal class DintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(sbyte) ||
                   sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(ushort) ||
                   sourceType == typeof(int) ||
                   sourceType == typeof(SINT) ||
                   sourceType == typeof(USINT) ||
                   sourceType == typeof(INT) ||
                   sourceType == typeof(UINT) ||
                   sourceType == typeof(DINT) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                sbyte v => new DINT(v),
                byte v => new DINT(v),
                short v => new DINT(v),
                ushort v => new DINT(v),
                int v => new DINT(v),
                SINT v => new DINT(v.Value),
                USINT v => new DINT(v.Value),
                INT v => new DINT(v.Value),
                UINT v => new DINT(v.Value),
                DINT v => v,
                string v => int.TryParse(v, out var result) ? new DINT(result) : Radix.ParseValue<DINT>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
        
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(int) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(DINT) ||
                   destinationType == typeof(LINT) ||
                   destinationType == typeof(REAL) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not DINT typed)
                throw new InvalidOperationException($"Value must be of type {typeof(DINT)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(int) => typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(DINT) => new DINT(typed.Value),
                not null when destinationType == typeof(LINT) => new LINT(typed.Value),
                not null when destinationType == typeof(REAL) => new REAL(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}