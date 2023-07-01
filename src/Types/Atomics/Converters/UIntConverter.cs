using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters;

/// <summary>
/// A <see cref="TypeConverter"/> for the <see cref="UINT"/> object.
/// </summary>
public class UIntConverter : AtomicConverter
{
    /// <inheritdoc />
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        checked
        {
            return value switch
            {
                bool v => v ? new USINT(1) : new USINT(),
                sbyte v => new UINT((ushort)v),
                byte v => new UINT(v),
                short v => new UINT((ushort)v),
                ushort v => new UINT(v),
                int v => new UINT((ushort)v),
                uint v => new UINT((ushort)v),
                long v => new UINT((ushort)v),
                ulong v => new UINT((ushort)v),
                float v => new UINT((ushort)v),
                BOOL v => v,
                SINT v => (UINT)v,
                USINT v => v,
                INT v => (UINT)v,
                UINT v => v,
                DINT v => (UINT)v,
                UDINT v => (UINT)v,
                LINT v => (UINT)v,
                ULINT v => (UINT)v,
                REAL v => (UINT)v,
                string v => (UINT)v,
                _ => throw new NotSupportedException(
                    $"The provided value type {value.GetType()} is not supported for conversion to {typeof(UINT)}.")
            };
        }
    }

    /// <inheritdoc />
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
        Type destinationType)
    {
        if (value is not UINT atomic)
            throw new ArgumentException($"Value must be of type {typeof(UINT)}.");

        var type = (ushort)atomic;

        return base.ConvertTo(context, culture, type, destinationType) ??
               throw new NotSupportedException(
                   $"The provided value of type {value.GetType()} is not supported for conversion.");
    }
}