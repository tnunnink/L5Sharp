using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="USINT"/> object.
    /// </summary>
    public class USintConverter : AtomicConverter<USINT>
    {
        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new USINT((byte)v),
                    byte v => new USINT(v),
                    short v => new USINT((byte)v),
                    ushort v => new USINT((byte)v),
                    int v => new USINT((byte)v),
                    uint v => new USINT((byte)v),
                    long v => new USINT((byte)v),
                    ulong v => new USINT((byte)v),
                    float v => new USINT((byte)v),
                    SINT v => new USINT((byte)(sbyte)v),
                    USINT v => v,
                    INT v => new USINT((byte)v),
                    UINT v => new USINT((byte)v),
                    DINT v => new USINT((byte)v),
                    UDINT v => new USINT((byte)v),
                    LINT v => new USINT((byte)v),
                    ULINT v => new USINT((byte)v),
                    REAL v => new USINT((byte)v),
                    string v => byte.TryParse(v, out var result) ? new USINT(result) : Atomic.Parse<USINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}