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
    /// A <see cref="TypeConverter"/> for the <see cref="Types.Bool"/> object.
    /// </summary>
    internal class BoolConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(bool) ||
                   sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(int) ||
                   sourceType == typeof(long) ||
                   sourceType == typeof(Bool) ||
                   sourceType == typeof(Sint) ||
                   sourceType == typeof(Int) ||
                   sourceType == typeof(Dint) ||
                   sourceType == typeof(Lint) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                bool v => new Bool(v),
                byte v => new Bool(v != 0),
                short v => new Bool(v != 0),
                int v => new Bool(v != 0),
                long v => new Bool(v != 0),
                Bool v => v,
                Sint v => new Bool(v != 0),
                Int v => new Bool(v != 0),
                Dint v => new Bool(v != 0),
                Lint v => new Bool(v != 0),
                string v => Radix.ParseValue<Bool>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(bool) ||
                   destinationType == typeof(Bool) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not Bool typed)
                throw new InvalidOperationException($"Value must be of type {typeof(Bool)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(bool) => typed.Value,
                not null when destinationType == typeof(Bool) => typed,
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}