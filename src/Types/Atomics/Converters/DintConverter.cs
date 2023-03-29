using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="DINT"/> object.
    /// </summary>
    public class DintConverter : AtomicConverter
    {
        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new DINT(v),
                    byte v => new DINT(v),
                    short v => new DINT(v),
                    ushort v => new DINT(v),
                    int v => new DINT(v),
                    uint v => new DINT((int)v),
                    long v => new DINT((int)v),
                    ulong v => new DINT((int)v),
                    float v => new DINT((int)v),
                    SINT v => new DINT((sbyte)v),
                    USINT v => new DINT(v),
                    INT v => new DINT((short)v),
                    UINT v => new DINT(v),
                    DINT v => v,
                    UDINT v => new DINT((int)(uint)v),
                    LINT v => new DINT((int)v),
                    ULINT v => new DINT((int)(ulong)v),
                    REAL v => new DINT((int)v),
                    string v => int.TryParse(v, out var result)
                        ? new DINT(result)
                        : (DINT)ConvertFrom(Radix.Infer(v).Parse(v))!,
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
            if (value is not DINT atomic)
                throw new InvalidOperationException($"Value must be of type {typeof(DINT)}.");

            var type = (int)atomic;

            return base.ConvertTo(context, culture, type, destinationType) ??
                   throw new NotSupportedException(
                       $"The provided value of type {value.GetType()} is not supported for conversion.");
        }
    }
}