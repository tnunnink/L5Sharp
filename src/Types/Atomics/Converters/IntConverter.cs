using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="INT"/> object.
    /// </summary>
    internal class IntConverter : AtomicConverter<INT>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new INT(v),
                    byte v => new INT(v),
                    short v => new INT(v),
                    ushort v => new INT((short)v),
                    int v => new INT((short)v),
                    uint v => new INT((short)v),
                    long v => new INT((short)v),
                    ulong v => new INT((short)v),
                    float v => new INT((short)v),
                    SINT v => new INT(v),
                    USINT v => new INT(v),
                    INT v => v,
                    UINT v => new INT((short)(ushort)v),
                    DINT v => new INT((short)v),
                    UDINT v => new INT((short)(uint)v),
                    LINT v => new INT((short)v),
                    ULINT v => new INT((short)(ulong)v),
                    REAL v => new INT((short)v),
                    string v => short.TryParse(v, out var result) ? new INT(result) : Radix.ParseValue<INT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}