using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters;

/// <summary>
/// A <see cref="TypeConverter"/> for the <see cref="INT"/> object.
/// </summary>
public class IntConverter : AtomicConverter
{
    /// <inheritdoc />
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
                SINT v => new INT((sbyte)v),
                USINT v => new INT(v),
                INT v => v,
                UINT v => new INT((short)(ushort)v),
                DINT v => new INT((short)v),
                UDINT v => new INT((short)(uint)v),
                LINT v => new INT((short)v),
                ULINT v => new INT((short)(ulong)v),
                REAL v => new INT((short)v),
                string v => short.TryParse(v, out var result)
                    ? new INT(result)
                    : (INT)ConvertFrom(Radix.Infer(v).Parse(v))!,
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
        if (value is not INT atomic)
            throw new InvalidOperationException($"Value must be of type {typeof(INT)}.");

        var type = (short)atomic;

        return base.ConvertTo(context, culture, type, destinationType) ??
               throw new NotSupportedException(
                   $"The provided value of type {value.GetType()} is not supported for conversion.");
    }
}