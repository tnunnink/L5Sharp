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
    internal class UIntConverter : AtomicConverter<UINT>
    {
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
                    SINT v => new UINT((ushort)v.Value),
                    USINT v => new UINT(v.Value),
                    INT v => new UINT((ushort)v.Value),
                    UINT v => v,
                    DINT v => new UINT((ushort)v.Value),
                    UDINT v => new UINT((ushort)v.Value),
                    LINT v => new UINT((ushort)v.Value),
                    ULINT v => new UINT((ushort)v.Value),
                    REAL v => new UINT((ushort)v.Value),
                    string v => ushort.TryParse(v, out var result) ? new UINT(result) : Radix.ParseValue<UINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}