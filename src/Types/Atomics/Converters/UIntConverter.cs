using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="UINT"/> object.
    /// </summary>
    public class UIntConverter : AtomicConverter
    {
        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new UINT((ushort)v),
                    byte v => new UINT(v),
                    short v => new UINT((ushort)v),
                    ushort v => new UINT(v),
                    int v => new UINT((ushort)v),
                    uint v => new UINT((ushort)v),
                    long v => new UINT((ushort)v),
                    ulong v => new UINT((ushort)v),
                    float v => new UINT((ushort)v),
                    SINT v => new UINT((ushort)(sbyte)v),
                    USINT v => new UINT(v),
                    INT v => new UINT((ushort)(short)v),
                    UINT v => v,
                    DINT v => new UINT((ushort)v),
                    UDINT v => new UINT((ushort)v),
                    LINT v => new UINT((ushort)v),
                    ULINT v => new UINT((ushort)v),
                    REAL v => new UINT((ushort)v),
                    string v => ushort.TryParse(v, out var result)
                        ? new UINT(result)
                        : (UINT)ConvertFrom(Radix.Infer(v).Parse(v))!,
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
        
        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (value is not UINT atomic)
                throw new InvalidOperationException($"Value must be of type {typeof(UINT)}.");

            var type = (ushort)atomic;

            return base.ConvertTo(context, culture, type, destinationType) ??
                   throw new NotSupportedException(
                       $"The provided value of type {value.GetType()} is not supported for conversion.");
        }
    }
}