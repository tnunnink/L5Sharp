using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters;

/// <summary>
/// A <see cref="TypeConverter"/> for the <see cref="LINT"/> object.
/// </summary>
public class LintConverter : AtomicConverter
{
    /// <inheritdoc />
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
                SINT v => new LINT((sbyte)v),
                USINT v => new LINT(v),
                INT v => new LINT((short)v),
                UINT v => new LINT(v),
                DINT v => new LINT((int)v),
                UDINT v => new LINT(v),
                LINT v => v,
                ULINT v => new LINT((long)(ulong)v),
                REAL v => new LINT((long)v),
                string v => long.TryParse(v, out var result)
                    ? new LINT(result)
                    : (LINT)ConvertFrom(Radix.Infer(v).Parse(v))!,
                _ => base.ConvertFrom(context, culture, value)
                     ?? throw new NotSupportedException(
                         $"The provided value of type {value.GetType()} is not supported for conversion.")
            };
        }
    }
        
    /// <inheritdoc />
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
        Type destinationType)
    {
        if (value is not LINT atomic)
            throw new InvalidOperationException($"Value must be of type {typeof(LINT)}.");

        var type = (long)atomic;

        return base.ConvertTo(context, culture, type, destinationType) ??
               throw new NotSupportedException(
                   $"The provided value of type {value.GetType()} is not supported for conversion.");
    }
}