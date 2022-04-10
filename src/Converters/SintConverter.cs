using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="SINT"/> object.
    /// </summary>
    internal class SintConverter : AtomicConverter<SINT>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new SINT(v),
                    byte v => new SINT((sbyte)v),
                    short v => new SINT((sbyte)v),
                    ushort v => new SINT((sbyte)v),
                    int v => new SINT((sbyte)v),
                    uint v => new SINT((sbyte)v),
                    long v => new SINT((sbyte)v),
                    ulong v => new SINT((sbyte)v),
                    float v => new SINT((sbyte)v),
                    SINT v => v,
                    USINT v => new SINT((sbyte)v.Value),
                    INT v => new SINT((sbyte)v.Value),
                    UINT v => new SINT((sbyte)v.Value),
                    DINT v => new SINT((sbyte)v.Value),
                    UDINT v => new SINT((sbyte)v.Value),
                    LINT v => new SINT((sbyte)v.Value),
                    ULINT v => new SINT((sbyte)v.Value),
                    REAL v => new SINT((sbyte)v.Value),
                    string v => sbyte.TryParse(v, out var result) ? new SINT(result) : Radix.ParseValue<SINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                }; 
            }
        }
    }
}