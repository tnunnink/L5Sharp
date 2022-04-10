using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="UDINT"/> object.
    /// </summary>
    internal class UDintConverter : AtomicConverter<UDINT>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new UDINT((uint)v),
                    byte v => new UDINT(v),
                    short v => new UDINT((uint)v),
                    ushort v => new UDINT(v),
                    int v => new UDINT((uint)v),
                    uint v => new UDINT(v),
                    long v => new UDINT((uint)v),
                    ulong v => new UDINT((uint)v),
                    float v => new UDINT((uint)v),
                    SINT v => new UDINT((uint)v.Value),
                    USINT v => new UDINT(v.Value),
                    INT v => new UDINT((uint)v.Value),
                    UINT v => new UDINT(v.Value),
                    DINT v => new UDINT((uint)v.Value),
                    UDINT v => v,
                    LINT v => new UDINT((uint)v.Value),
                    ULINT v => new UDINT((uint)v.Value),
                    REAL v => new UDINT((uint)v.Value),
                    string v => uint.TryParse(v, out var result) ? new UDINT(result) : Radix.ParseValue<UDINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                }; 
            }
        }
    }
}