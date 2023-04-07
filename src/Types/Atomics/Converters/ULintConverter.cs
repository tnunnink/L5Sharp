using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters;

/// <summary>
/// A <see cref="TypeConverter"/> for the <see cref="UDINT"/> object.
/// </summary>
public class ULintConverter : AtomicConverter
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
                string v => ulong.TryParse(v, out var result)
                    ? new ULINT(result)
                    : (ULINT)ConvertFrom(Radix.Infer(v).Parse(v))!,
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
        if (value is not ULINT atomic)
            throw new InvalidOperationException($"Value must be of type {typeof(ULINT)}.");

        var type = (ulong)atomic;

        return base.ConvertTo(context, culture, type, destinationType) ??
               throw new NotSupportedException(
                   $"The provided value of type {value.GetType()} is not supported for conversion.");
    }
}