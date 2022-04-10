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
    internal class USintConverter : AtomicConverter<USINT>
    {
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
                    SINT v => new USINT((byte)v.Value),
                    USINT v => v,
                    INT v => new USINT((byte)v.Value),
                    UINT v => new USINT((byte)v.Value),
                    DINT v => new USINT((byte)v.Value),
                    UDINT v => new USINT((byte)v.Value),
                    LINT v => new USINT((byte)v.Value),
                    ULINT v => new USINT((byte)v.Value),
                    REAL v => new USINT((byte)v.Value),
                    string v => byte.TryParse(v, out var result) ? new USINT(result) : Radix.ParseValue<USINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}