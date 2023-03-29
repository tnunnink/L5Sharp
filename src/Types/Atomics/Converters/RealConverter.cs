using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="REAL"/> object.
    /// </summary>
    public class RealConverter : AtomicConverter
    {
        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new REAL(v),
                    byte v => new REAL(v),
                    short v => new REAL(v),
                    ushort v => new REAL(v),
                    int v => new REAL(v),
                    uint v => new REAL(v),
                    long v => new REAL(v),
                    ulong v => new REAL(v),
                    float v => new REAL(v),
                    SINT v => new REAL((sbyte)v),
                    USINT v => new REAL(v),
                    INT v => new REAL((short)v),
                    UINT v => new REAL(v),
                    DINT v => new REAL((int)v),
                    UDINT v => new REAL(v),
                    LINT v => new REAL((long)v),
                    ULINT v => new REAL(v),
                    REAL v => v,
                    string v => float.TryParse(v, out var result) ? new REAL(result) : Radix.Infer(v).Parse(v),
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
            if (value is not REAL atomic)
                throw new InvalidOperationException($"Value must be of type {typeof(REAL)}.");

            var type = (float)atomic;

            return base.ConvertTo(context, culture, type, destinationType) ??
                   throw new NotSupportedException(
                       $"The provided value of type {value.GetType()} is not supported for conversion.");
        }
    }
}