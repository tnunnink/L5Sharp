using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="DINT"/> object.
    /// </summary>
    internal class DintConverter : AtomicConverter<DINT>
    {
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
                    SINT v => new DINT(v.Value),
                    USINT v => new DINT(v.Value),
                    INT v => new DINT(v.Value),
                    UINT v => new DINT(v.Value),
                    DINT v => v,
                    UDINT v => new DINT((int)v.Value),
                    LINT v => new DINT((int)v.Value),
                    ULINT v => new DINT((int)v.Value),
                    REAL v => new DINT((int)v.Value),
                    string v => int.TryParse(v, out var result) ? new DINT(result) : Radix.ParseValue<DINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}