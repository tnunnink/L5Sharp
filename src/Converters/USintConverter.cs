using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="USint"/> object.
    /// </summary>
    internal class USintConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(byte) ||
                   sourceType == typeof(USint) ||
                   sourceType == typeof(string) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                byte v => new USint(v),
                USint v => v,
                string v => byte.TryParse(v, out var result) ? new USint(result) : Radix.ParseValue<USint>(v),
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
                   destinationType == typeof(USint) ||
                   destinationType == typeof(Int) ||
                   destinationType == typeof(UInt) ||
                   destinationType == typeof(Dint) ||
                   destinationType == typeof(UDint) ||
                   destinationType == typeof(Lint) ||
                   destinationType == typeof(ULint) ||
                   destinationType == typeof(Real) ||
                   destinationType == typeof(string) ||
                   base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not USint typed)
                throw new InvalidOperationException($"Value must be of type {typeof(USint)}.");

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
                not null when destinationType == typeof(USint) => new USint(typed.Value),
                not null when destinationType == typeof(Int) => new Int(typed.Value),
                not null when destinationType == typeof(UInt) => new UInt(typed.Value),
                not null when destinationType == typeof(Dint) => new Dint(typed.Value),
                not null when destinationType == typeof(UDint) => new UDint(typed.Value),
                not null when destinationType == typeof(Lint) => new Lint(typed.Value),
                not null when destinationType == typeof(ULint) => new ULint(typed.Value),
                not null when destinationType == typeof(Real) => new Real(typed.Value),
                not null when destinationType == typeof(string) => typed.Format(Radix.Default(typed)),
                _ => base.ConvertTo(context, culture, value, destinationType!) ??
                     throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
}