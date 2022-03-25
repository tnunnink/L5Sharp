using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="INT"/> object.
    /// </summary>
    internal class IntConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(sbyte) ||
                   sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(SINT) ||
                   sourceType == typeof(USINT) ||
                   sourceType == typeof(INT) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                sbyte v => new INT(v),
                byte v => new INT(v),
                short v => new INT(v),
                SINT v => new INT(v.Value),
                USINT v => new INT(v.Value),
                INT v => v,
                string v => short.TryParse(v, out var result) ? new INT(result) : Radix.ParseValue<INT>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(short) ||
                   destinationType == typeof(int) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(float) ||
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
            if (value is not INT typed)
                throw new InvalidOperationException($"Value must be of type {typeof(INT)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(short) => typed.Value,
                not null when destinationType == typeof(int) => (int)typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(INT) => typed,
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