using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="REAL"/> object.
    /// </summary>
    internal class RealConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(int) ||
                   sourceType == typeof(long) ||
                   sourceType == typeof(float) ||
                   sourceType == typeof(SINT) ||
                   sourceType == typeof(INT) ||
                   sourceType == typeof(DINT) ||
                   sourceType == typeof(LINT) ||
                   sourceType == typeof(REAL) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new REAL(v),
                short v => new REAL(v),
                int v => new REAL(v),
                long v => new REAL(v),
                float v => new REAL(v),
                SINT v => new REAL(v.Value),
                INT v => new REAL(v.Value),
                DINT v => new REAL(v.Value),
                LINT v => new REAL(v.Value),
                REAL v => v,
                string v => float.TryParse(v, out var result) ? new REAL(result) : Radix.ParseValue<REAL>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(float) ||
                   destinationType == typeof(REAL) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not REAL typed)
                throw new InvalidOperationException($"Value must be of type {typeof(REAL)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(float) => typed.Value,
                not null when destinationType == typeof(REAL) => new REAL(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}