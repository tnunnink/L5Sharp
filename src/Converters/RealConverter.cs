using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="REAL"/> object.
    /// </summary>
    internal class RealConverter : AtomicConverter<REAL>
    {
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
                    SINT v => new REAL(v.Value),
                    USINT v => new REAL(v.Value),
                    INT v => new REAL(v.Value),
                    UINT v => new REAL(v.Value),
                    DINT v => new REAL(v.Value),
                    UDINT v => new REAL(v.Value),
                    LINT v => new REAL(v.Value),
                    ULINT v => new REAL(v.Value),
                    REAL v => v,
                    string v => float.TryParse(v, out var result) ? new REAL(result) : Radix.ParseValue<REAL>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}