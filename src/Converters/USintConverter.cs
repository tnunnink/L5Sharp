using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="USINT"/> object.
    /// </summary>
    internal class USintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(USINT) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new USINT(v),
                USINT v => v,
                string v => byte.TryParse(v, out var result) ? new USINT(result) : Radix.ParseValue<USINT>(v),
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(byte) ||
                   destinationType == typeof(short) ||
                   destinationType == typeof(ushort) ||
                   destinationType == typeof(int) ||
                   destinationType == typeof(uint) ||
                   destinationType == typeof(long) ||
                   destinationType == typeof(ulong) ||
                   destinationType == typeof(float) ||
                   destinationType == typeof(USINT) ||
                   destinationType == typeof(INT) ||
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
            if (value is not USINT typed)
                throw new InvalidOperationException($"Value must be of type {typeof(USINT)}.");

            return destinationType switch
            {
                not null when destinationType == typeof(byte) => typed.Value,
                not null when destinationType == typeof(short) => (short)typed.Value,
                not null when destinationType == typeof(ushort) => (ushort)typed.Value,
                not null when destinationType == typeof(int) => (int)typed.Value,
                not null when destinationType == typeof(uint) => (uint)typed.Value,
                not null when destinationType == typeof(long) => (long)typed.Value,
                not null when destinationType == typeof(ulong) => (ulong)typed.Value,
                not null when destinationType == typeof(float) => (float)typed.Value,
                not null when destinationType == typeof(USINT) => new USINT(typed.Value),
                not null when destinationType == typeof(INT) => new INT(typed.Value),
                not null when destinationType == typeof(UINT) => new UINT(typed.Value),
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