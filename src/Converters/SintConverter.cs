using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="Types.Sint"/> object.
    /// </summary>
    internal class SintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(Sint) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new Sint(v),
                Sint v => v,
                string v => Radix.ParseValue<Sint>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(byte) ||
                   destinationType == typeof(short) ||
                   destinationType == typeof(int) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(Sint) ||
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
            if (value is not Sint typed)
                throw new InvalidOperationException($"Value must be of type {typeof(Sint)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(byte) => typed.Value,
                not null when destinationType == typeof(short) => (short)typed.Value,
                not null when destinationType == typeof(int) => (int)typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(Sint) => new Sint(typed.Value),
                not null when destinationType == typeof(Int) => new Int(typed.Value),
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