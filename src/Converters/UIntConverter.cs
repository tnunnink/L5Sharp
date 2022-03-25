using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="UINT"/> object.
    /// </summary>
    internal class UIntConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(ushort) ||
                   sourceType == typeof(USINT) ||
                   sourceType == typeof(UINT) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new UINT(v),
                ushort v => new UINT(v),
                USINT v => new UINT(v.Value),
                UINT v => v,
                string v => ushort.TryParse(v, out var result) ? new UINT(result) : Radix.ParseValue<UINT>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(ushort) ||
                   destinationType == typeof(int) ||
                   destinationType == typeof(uint) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(ulong) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(UINT) ||
                   destinationType == typeof(DINT) ||
                   destinationType == typeof(UDINT) ||
                   destinationType == typeof(LINT) ||
                   destinationType == typeof(ULINT) ||
                   destinationType == typeof(REAL) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not UINT typed)
                throw new InvalidOperationException($"Value must be of type {typeof(UINT)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(ushort) => typed.Value,
                not null when destinationType == typeof(int) => (int)typed.Value,
                not null when destinationType == typeof(uint) => (uint)typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(ulong) => (ulong)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(UINT) => typed,
                not null when destinationType == typeof(UINT) => typed,
                not null when destinationType == typeof(DINT) => new DINT(typed.Value),
                not null when destinationType == typeof(UDINT) => new UDINT(typed.Value),
                not null when destinationType == typeof(LINT) => new LINT(typed.Value),
                not null when destinationType == typeof(ULINT) => new ULINT(typed.Value),
                not null when destinationType == typeof(REAL) => new REAL(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}