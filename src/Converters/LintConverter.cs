using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="LINT"/> object.
    /// </summary>
    internal class LintConverter : AtomicConverter<LINT>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            checked
            {
                return value switch
                {
                    sbyte v => new LINT(v),
                    byte v => new LINT(v),
                    short v => new LINT(v),
                    ushort v => new LINT(v),
                    int v => new LINT(v),
                    uint v => new LINT(v),
                    long v => new LINT(v),
                    ulong v => new LINT((long)v),
                    float v => new LINT((long)v),
                    SINT v => new LINT(v.Value),
                    USINT v => new LINT(v.Value),
                    INT v => new LINT(v.Value),
                    UINT v => new LINT(v.Value),
                    DINT v => new LINT(v.Value),
                    UDINT v => new LINT(v.Value),
                    LINT v => v,
                    ULINT v => new LINT((long)v.Value),
                    REAL v => new LINT((long)v.Value),
                    string v => long.TryParse(v, out var result) ? new LINT(result) : Radix.ParseValue<LINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}