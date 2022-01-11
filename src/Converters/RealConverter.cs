using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="Types.Real"/> object.
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
                   sourceType == typeof(Sint) ||
                   sourceType == typeof(Int) ||
                   sourceType == typeof(Dint) ||
                   sourceType == typeof(Lint) ||
                   sourceType == typeof(Real) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new Real(v),
                short v => new Real(v),
                int v => new Real(v),
                long v => new Real(v),
                float v => new Real(v),
                Sint v => new Real(v.Value),
                Int v => new Real(v.Value),
                Dint v => new Real(v.Value),
                Lint v => new Real(v.Value),
                Real v => v,
                string v => Radix.ParseValue<Real>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(float) ||
                   destinationType == typeof(Real) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not Real typed)
                throw new InvalidOperationException($"Value must be of type {typeof(Real)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(float) => typed.Value,
                not null when destinationType == typeof(Real) => new Real(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}