using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;
using L5Sharp.Types;

[assembly: InternalsVisibleTo("L5Sharp.Converters.Tests")]

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="Types.Dint"/> object.
    /// </summary>
    internal class DintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(int) ||
                   sourceType == typeof(Sint) ||
                   sourceType == typeof(Int) ||
                   sourceType == typeof(Dint) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new Dint(v),
                short v => new Dint(v),
                int v => new Dint(v),
                Sint v => new Dint(v.Value),
                Int v => new Dint(v.Value),
                Dint v => v,
                string v => Radix.ParseValue<Dint>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
        
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(int) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(Dint) ||
                   destinationType == typeof(Lint) ||
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
                not null when destinationType == typeof(int) => (int)typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(Dint) => new Dint(typed.Value),
                not null when destinationType == typeof(Lint) => new Lint(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}