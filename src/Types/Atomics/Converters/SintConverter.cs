using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="SINT"/> object.
    /// </summary>
    public class SintConverter : AtomicConverter<SINT>
    {
        /// <inheritdoc />
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
                    USINT v => new SINT((sbyte)(byte)v),
                    INT v => new SINT((sbyte)v),
                    UINT v => new SINT((sbyte)(ushort)v),
                    DINT v => new SINT((sbyte)v),
                    UDINT v => new SINT((sbyte)(uint)v),
                    LINT v => new SINT((sbyte)v),
                    ULINT v => new SINT((sbyte)(ulong)v),
                    REAL v => new SINT((sbyte)v),
                    string v => sbyte.TryParse(v, out var result) ? new SINT(result) : Radix.Infer(v).Parse(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                }; 
            }
        }
    }
}