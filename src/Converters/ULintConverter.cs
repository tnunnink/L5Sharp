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
    internal class ULintConverter : AtomicConverter<ULINT>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new ULINT((ulong)v),
                    byte v => new ULINT(v),
                    short v => new ULINT((ulong)v),
                    ushort v => new ULINT(v),
                    int v => new ULINT((ulong)v),
                    uint v => new ULINT(v),
                    long v => new ULINT((ulong)v),
                    ulong v => new ULINT(v),
                    float v => new ULINT((ulong)v),
                    SINT v => new ULINT((ulong)v.Value),
                    USINT v => new ULINT(v.Value),
                    INT v => new ULINT((ulong)v.Value),
                    UINT v => new ULINT(v.Value),
                    DINT v => new ULINT((ulong)v.Value),
                    UDINT v => new ULINT(v.Value),
                    LINT v => new ULINT((ulong)v.Value),
                    ULINT v => v,
                    REAL v => new ULINT((ulong)v.Value),
                    string v => ulong.TryParse(v, out var result) ? new ULINT(result) : Radix.ParseValue<ULINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}