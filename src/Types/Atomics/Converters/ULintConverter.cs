using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters
{
    /// <summary>
    /// A <see cref="TypeConverter"/> for the <see cref="UDINT"/> object.
    /// </summary>
    public class ULintConverter : AtomicConverter<ULINT>
    {
        /// <inheritdoc />
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
                    SINT v => new ULINT((ulong)(sbyte)v),
                    USINT v => new ULINT(v),
                    INT v => new ULINT((ulong)(short)v),
                    UINT v => new ULINT(v),
                    DINT v => new ULINT((ulong)(int)v),
                    UDINT v => new ULINT(v),
                    LINT v => new ULINT((ulong)(long)v),
                    ULINT v => v,
                    REAL v => new ULINT((ulong)v),
                    string v => ulong.TryParse(v, out var result) ? new ULINT(result) : Atomic.Parse<ULINT>(v),
                    _ => base.ConvertFrom(context, culture, value)
                         ?? throw new NotSupportedException(
                             $"The provided value of type {value.GetType()} is not supported for conversion.")
                };
            }
        }
    }
}