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
    /// A <see cref="TypeConverter"/> for the <see cref="Types.Lint"/> object.
    /// </summary>
    internal class LintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(int) ||
                   sourceType == typeof(long) ||
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
                byte v => new Lint(v),
                short v => new Lint(v),
                int v => new Lint(v),
                long v => new Lint(v),
                Sint v => new Lint(v.Value),
                Int v => new Lint(v.Value),
                Dint v => new Lint(v.Value),
                Lint v => v,
                string v => Radix.ParseValue<Lint>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(long) ||
                   destinationType == typeof(Lint) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not Lint typed)
                throw new InvalidOperationException($"Value must be of type {typeof(Lint)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(long) => typed.Value,
                not null when destinationType == typeof(Lint) => new Lint(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}