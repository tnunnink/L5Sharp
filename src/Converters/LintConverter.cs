using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="LINT"/> object.
    /// </summary>
    internal class LintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(sbyte) ||
                   sourceType == typeof(byte) ||
                   sourceType == typeof(short) ||
                   sourceType == typeof(ushort) ||
                   sourceType == typeof(int) ||
                   sourceType == typeof(uint) ||
                   sourceType == typeof(long) ||
                   sourceType == typeof(SINT) ||
                   sourceType == typeof(USINT) ||
                   sourceType == typeof(INT) ||
                   sourceType == typeof(UINT) ||
                   sourceType == typeof(DINT) ||
                   sourceType == typeof(UDINT) ||
                   sourceType == typeof(LINT) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                sbyte v => new LINT(v),
                byte v => new LINT(v),
                short v => new LINT(v),
                ushort v => new LINT(v),
                int v => new LINT(v),
                uint v => new LINT(v),
                long v => new LINT(v),
                SINT v => new LINT(v.Value),
                USINT v => new LINT(v.Value),
                INT v => new LINT(v.Value),
                UINT v => new LINT(v.Value),
                DINT v => new LINT(v.Value),
                UDINT v => new LINT(v.Value),
                LINT v => v,
                string v => long.TryParse(v, out var result) ? new LINT(result) : Radix.ParseValue<LINT>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(long) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(LINT) ||
                   destinationType == typeof(REAL) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not LINT typed)
                throw new InvalidOperationException($"Value must be of type {typeof(LINT)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(long) => typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
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