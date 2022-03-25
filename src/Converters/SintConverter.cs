using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="SINT"/> object.
    /// </summary>
    internal class SintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(sbyte) ||
                   sourceType == typeof(SINT) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                sbyte v => new SINT(v),
                SINT v => v,
                string v => sbyte.TryParse(v, out var result) ? new SINT(result) : Radix.ParseValue<SINT>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(sbyte) ||
                   destinationType == typeof(short) ||
                   destinationType == typeof(int) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(SINT) ||
                   destinationType == typeof(INT) ||
                   destinationType == typeof(DINT) ||
                   destinationType == typeof(LINT) ||
                   destinationType == typeof(REAL) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not SINT typed)
                throw new InvalidOperationException($"Value must be of type {typeof(SINT)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(sbyte) => typed.Value,
                not null when destinationType == typeof(short) => (short)typed.Value,
                not null when destinationType == typeof(int) => (int)typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(SINT) => new SINT(typed.Value),
                not null when destinationType == typeof(INT) => new INT(typed.Value),
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