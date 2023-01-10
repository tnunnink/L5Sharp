using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
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
                    SINT v => new UDINT((uint)(sbyte)v),
                    USINT v => new UDINT(v),
                    INT v => new UDINT((uint)(short)v),
                    UINT v => new UDINT(v),
                    DINT v => new UDINT((uint)(int)v),
                    UDINT v => v,
                    LINT v => new UDINT((uint)v),
                    ULINT v => new UDINT((uint)v),
                    REAL v => new UDINT((uint)v),
                    string v => uint.TryParse(v, out var result) ? new UDINT(result) : Radix.ParseValue<UDINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                }; 
            }
        }
    }
}