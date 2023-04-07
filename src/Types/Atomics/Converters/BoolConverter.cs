using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics.Converters;

/// <summary>
/// A <see cref="TypeConverter"/> for the <see cref="BOOL"/> object.
/// </summary>
public class BoolConverter : TypeConverter
{
    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(bool) ||
               sourceType == typeof(byte) ||
               sourceType == typeof(short) ||
               sourceType == typeof(int) ||
               sourceType == typeof(long) ||
               sourceType == typeof(BOOL) ||
               sourceType == typeof(SINT) ||
               sourceType == typeof(INT) ||
               sourceType == typeof(DINT) ||
               sourceType == typeof(LINT) ||
               sourceType == typeof(string) ||
               base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc />
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        return value switch
        {
            bool v => new BOOL(v),
            byte v => new BOOL(v != 0),
            short v => new BOOL(v != 0),
            int v => new BOOL(v != 0),
            long v => new BOOL(v != 0),
            BOOL v => v,
            SINT v => new BOOL(v != 0),
            INT v => new BOOL(v != 0),
            DINT v => new BOOL(v != 0),
            LINT v => new BOOL(v != 0),
            string v => bool.TryParse(v, out var result)
                ? new BOOL(result)
                : (BOOL)ConvertFrom(Radix.Infer(v).Parse(v))!,
            _ => base.ConvertFrom(context, culture, value)
                 ?? throw new NotSupportedException(
                     $"The provided value of type {value.GetType()} is not supported for conversion.")
        };
    }

    /// <inheritdoc />
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(bool) ||
               destinationType == typeof(BOOL) ||
               destinationType == typeof(string) ||
               base.CanConvertFrom(context, destinationType);
    }

    /// <inheritdoc />
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
        Type destinationType)
    {
        if (value is not BOOL typed)
            throw new InvalidOperationException($"Value must be of type {typeof(BOOL)}.");

        return destinationType switch
        {
            not null when destinationType == typeof(bool) => (bool)typed,
            not null when destinationType == typeof(BOOL) => typed,
            not null when destinationType == typeof(string) => typed.ToString(),
            _ => base.ConvertTo(context, culture, value, destinationType!) ??
                 throw new NotSupportedException(
                     $"The provided value of type {value.GetType()} is not supported for conversion.")
        };
    }
}