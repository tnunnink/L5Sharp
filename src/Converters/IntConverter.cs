using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Atomics;
using L5Sharp.Enums;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="Int"/> object.
    /// </summary>
    internal class IntConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(sbyte) ||
                   sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(Sint) ||
                   sourceType == typeof(USint) ||
                   sourceType == typeof(Int) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                sbyte v => new Int(v),
                byte v => new Int(v),
                short v => new Int(v),
                Sint v => new Int(v.Value),
                USint v => new Int(v.Value),
                Int v => v,
                string v => short.TryParse(v, out var result) ? new Int(result) : Radix.ParseValue<Int>(v),
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
                   destinationType == typeof(Int) ||
                   destinationType == typeof(Dint) ||
                   destinationType == typeof(Lint) ||
                   destinationType == typeof(Real) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not Int typed)
                throw new InvalidOperationException($"Value must be of type {typeof(Int)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(short) => typed.Value,
                not null when destinationType == typeof(int) => (int)typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(Int) => typed,
                not null when destinationType == typeof(Dint) => new Dint(typed.Value),
                not null when destinationType == typeof(Lint) => new Lint(typed.Value),
                not null when destinationType == typeof(Real) => new Real(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}