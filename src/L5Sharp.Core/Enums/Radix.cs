using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable StringLiteralTypo

namespace L5Sharp.Core;

/// <summary>
/// Represents a number base for a given value type or atomic type.
/// </summary>
public abstract class Radix : LogixEnum<Radix, string>
{
    private Radix(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// The specifier prefix of the Radix string format.
    /// </summary>
    /// <value>A <see cref="string"/> representing the text that identifies the format of the Radix.</value>
    /// <remarks>
    /// Most <see cref="Radix"/> will have a specifier prefix, such as '#2' for Binary, '#16' for Hex, and so on.
    /// By default this property is used by <see cref="HasFormat"/> to determine if an input string has the specified
    /// Radix format, and further by <see cref="Infer"/> to determine a Radix from a string value.
    /// However, some Radix options are overriden as they do not have specifiers (e.g. Decimal, Float).
    /// </remarks>
    protected abstract string Specifier { get; }

    /// <summary>
    /// Represents a Null radix, or absence of a Radix value.
    /// </summary>
    public static readonly Radix Null = new NullRadix();

    /// <summary>
    /// Represents a Binary number base format.
    /// </summary>
    public static readonly Radix Binary = new BinaryRadix();

    /// <summary>
    /// Represents a Octal number base format. 
    /// </summary>
    public static readonly Radix Octal = new OctalRadix();

    /// <summary>
    /// Represents a Decimal number base format.
    /// </summary>
    public static readonly Radix Decimal = new DecimalRadix();

    /// <summary>
    /// Represents a Hexadecimal number base format.
    /// </summary>
    public static readonly Radix Hex = new HexRadix();

    /// <summary>
    /// Represents a Exponential number base format.
    /// </summary>
    public static readonly Radix Exponential = new ExponentialRadix();

    /// <summary>
    /// Represents a Float number base format.
    /// </summary>
    public static readonly Radix Float = new FloatRadix();

    /// <summary>
    /// Represents a Ascii number base format.
    /// </summary>
    public static readonly Radix Ascii = new AsciiRadix();

    /// <summary>
    /// Represents a DateTime number base format.
    /// </summary>
    public static readonly Radix DateTime = new DateTimeRadix();

    /// <summary>
    /// Represents a DateTimeNs number base format.
    /// </summary>
    public static readonly Radix DateTimeNs = new DateTimeNsRadix();

    /// <summary>
    /// Gets the default <see cref="Radix"/> value for the provided logix type.
    /// </summary>
    /// <param name="type">The logix type to evaluate.</param>
    /// <returns>
    /// <see cref="Null"/> for all non atomic types.
    /// <see cref="Float"/> for <see cref="REAL"/> types.
    /// <see cref="Decimal"/> for all other atomic types.
    /// </returns>
    public static Radix Default(object? type)
    {
        return type switch
        {
            AtomicType atomic => atomic is REAL or LREAL ? Float : Decimal,
            ValueType value => value is float or double ? Float : Decimal,
            _ => Null
        };
    }

    /// <summary>
    /// Determines the radix format from a string representing a formatted atomic value.
    /// </summary>
    /// <param name="input">The string for which to infer the format.</param>
    /// <returns>A <see cref="Radix"/> format enum value.</returns>
    /// <exception cref="FormatException">A radix can not be determined from the format of <c>value</c>.</exception>
    public static Radix Infer(string input)
    {
        var name = Identifiers.FirstOrDefault(i => i.Value.Invoke(input)).Key;

        if (name is null)
            throw new FormatException(
                $"Could not determine Radix from input '{input}'. Verify that the input string is an accepted Radix format.");

        return Parse(name);
    }

    /// <summary>
    /// Tries to infer the radix format from a string representing a formatted atomic value.
    /// </summary>
    /// <param name="input">The string input for which to infer the radix format.</param>
    /// <param name="radix">If parsed successfully, then the <see cref="Radix"/> representing the format of the input;
    /// Otherwise, a <see cref="Null"/> radix format.</param>
    /// <returns><c>true</c> if a <c>Radix</c> format was inferred from the string input; Otherwise, <c>false</c>.</returns>
    public static bool TryInfer(string input, out Radix radix)
    {
        var name = Identifiers.FirstOrDefault(i => i.Value.Invoke(input)).Key;
        radix = name is not null ? Parse(name) : Null;
        return radix != Null;
    }

    /// <summary>
    /// Formats the provided <see cref="ValueType"/> in the current radix base format. 
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string that represents the value in the current radix base number style.</returns>
    public abstract string FormatValue(ValueType value);

    /// <summary>
    /// Parses a string input of a given Radix formatted value into an atomic value type. 
    /// </summary>
    /// <param name="input">The string value to parse.</param>
    /// <returns>An <see cref="ValueType"/> representing the value of the formatted string.</returns>
    public abstract ValueType ParseValue(string input);

    /// <summary>
    /// Returns an indication as to whether the current string input value has the format of the current Radix type.
    /// </summary>
    /// <param name="input">The input text value to examine.</param>
    /// <returns><c>true</c> if <c>input</c> qualifies as a valid format for the Radix type; otherwise, <c>false</c>.</returns>
    protected virtual bool HasFormat(string input) => !input.IsEmpty() && input.StartsWith(Specifier);

    /// <summary>
    /// Converts the provided <see cref="ValueType"/> to the specified base number.
    /// </summary>
    /// <param name="type">The atomic type to convert.</param>
    /// <param name="baseNumber">The base of the return value, which must be 2, 8, 10, or 16.</param>
    /// <returns>A <see cref="string"/> value representing the value in the specified base.</returns>
    /// <exception cref="ArgumentException">baseNumber is not 2, 8, 10, or 16.</exception>
    private static string ToBase(ValueType type, int baseNumber)
    {
        var bitsPerByte = baseNumber switch
        {
            2 => 8,
            8 => 3,
            _ => 2
        };

        var bytes = GetBytes(type);
        var builder = new StringBuilder();

        for (var ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
        {
            var byteString = Convert.ToString(bytes[ctr], baseNumber).PadLeft(bitsPerByte, '0');
            builder.Append(byteString);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the byte array for a provided <see cref="ValueType"/> object. This will allow us to format a string
    /// representation of the byte/bit values.
    /// </summary>
    private static byte[] GetBytes(ValueType type)
    {
        return type switch
        {
            bool v => BitConverter.GetBytes(v),
            sbyte v => [(byte)v],
            byte v => [v],
            short v => BitConverter.GetBytes(v),
            ushort v => BitConverter.GetBytes(v),
            int v => BitConverter.GetBytes(v),
            uint v => BitConverter.GetBytes(v),
            long v => BitConverter.GetBytes(v),
            ulong v => BitConverter.GetBytes(v),
            float v => BitConverter.GetBytes(v),
            double v => BitConverter.GetBytes(v),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type,
                "The provided value is out of range for the conversion to a byte array.")
        };
    }

    /// <summary>
    /// Converts the provided <see cref="string"/> to a <see cref="ValueType"/> given the provided bitsPerByte and baseNumber.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <param name="charsPerByte">The number of chars in <c>value</c> that represented a single byte of data.</param>
    /// <param name="baseNumber">The base number of the return value, which must be 2, 8, 10, or 16.</param>
    /// <returns>A <see cref="string"/> value representing the value in the specified base.</returns>
    /// <exception cref="ArgumentException">baseNumber is not 2, 8, 10, or 16.</exception>
    private static ValueType ToValueType(string value, int charsPerByte, int baseNumber)
    {
        var byteLength = value.Length / charsPerByte;

        return byteLength switch
        {
            0 => value == "1",
            > 0 and <= 1 => Convert.ToSByte(value, baseNumber),
            > 1 and <= 2 => Convert.ToInt16(value, baseNumber),
            > 2 and <= 4 => Convert.ToInt32(value, baseNumber),
            > 4 and <= 8 => Convert.ToInt64(value, baseNumber),
            _ => throw new ArgumentOutOfRangeException(nameof(value), value,
                $"The provided value '{value}' is out of range for conversion to a value type. Value type must be between 0 and 8 bytes.")
        };
    }

    /// <summary>
    /// Validates the string input as text that can be parsed by this <see cref="Radix"/> type.
    /// </summary>
    private void ValidateFormat(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input can not be null or empty.", nameof(input));

        if (!Identifiers[Name].Invoke(input))
            throw new FormatException($"Input '{input}' does not have expected {Name} format.");
    }

    /// <summary>
    /// Validates the input value type object as one that a <see cref="Radix"/> can format.
    /// </summary>
    private void ValidateType(ValueType value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (!SupportsType(value))
            throw new NotSupportedException($"Radix {Name} does not support type {value.GetType()}.");
    }

    /// <summary>
    /// Determines if the current <see cref="Radix"/> supports the provided value type object.
    /// </summary>
    private bool SupportsType(ValueType type)
    {
        return type switch
        {
            bool => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex),
            long => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii) ||
                    Equals(DateTime) || Equals(DateTimeNs),
            float => Equals(Float) || Equals(Exponential),
            double => Equals(Float) || Equals(Exponential),
            _ => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii)
        };
    }

    private static readonly Dictionary<string, Func<string, bool>> Identifiers = new()
    {
        { nameof(Binary), s => Binary.HasFormat(s) },
        { nameof(Octal), s => Octal.HasFormat(s) },
        { nameof(Decimal), s => Decimal.HasFormat(s) },
        { nameof(Hex), s => Hex.HasFormat(s) },
        { nameof(Float), s => Float.HasFormat(s) },
        { nameof(Exponential), s => Exponential.HasFormat(s) },
        { nameof(Ascii), s => Ascii.HasFormat(s) },
        { nameof(DateTime), s => DateTime.HasFormat(s) },
        { nameof(DateTimeNs), s => DateTimeNs.HasFormat(s) }
    };

    private class NullRadix() : Radix("NullType", "NullType")
    {
        protected override string Specifier => string.Empty;

        protected override bool HasFormat(string input) =>
            throw new NotSupportedException($"{Name} Radix does not support formatting atomic values");

        public override string FormatValue(ValueType value) =>
            throw new NotSupportedException($"{Name} Radix does not support formatting value types.");

        public override ValueType ParseValue(string input) =>
            throw new NotSupportedException($"{Name} Radix does not support parsing value types.");
    }

    private class BinaryRadix() : Radix(nameof(Binary), nameof(Binary))
    {
        private const int BaseNumber = 2;
        private const int CharsPerByte = 8;
        private const string ByteSeparator = "_";
        private const string Pattern = @"(?<=\d)(?=(\d\d\d\d)+(?!\d))";

        protected override string Specifier => "2#";

        protected override bool HasFormat(string input) => !input.IsEmpty() && input.StartsWith(Specifier);

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            var converted = value is not bool b ? ToBase(value, BaseNumber) : b ? "1" : "0";

            var formatted = Regex.Replace(converted, Pattern, ByteSeparator);

            return $"{Specifier}{formatted}";
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

            return ToValueType(value, CharsPerByte, BaseNumber);
        }
    }

    private class OctalRadix() : Radix(nameof(Octal), nameof(Octal))
    {
        private const int BaseNumber = 8;
        private const int CharsPerByte = 3;
        private const string ByteSeparator = "_";
        private const string Pattern = @"(?<=\d)(?=(\d\d\d)+(?!\d))";

        protected override string Specifier => "8#";

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            var converted = value is not bool b ? ToBase(value, BaseNumber) : b ? "1" : "0";

            var formatted = Regex.Replace(converted, Pattern, ByteSeparator);

            return $"{Specifier}{formatted}";
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

            return ToValueType(value, CharsPerByte, BaseNumber);
        }
    }

    private class DecimalRadix() : Radix(nameof(Decimal), nameof(Decimal))
    {
        protected override string Specifier => string.Empty;

        protected override bool HasFormat(string input)
        {
            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                input = input.Remove(0, 1);
            }

            return !input.IsEmpty() && input.All(char.IsDigit);
        }

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            return value switch
            {
                bool v => v ? "1" : "0",
                _ => value.ToString()!
            };
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);

            if (sbyte.TryParse(input, out var sbyteValue)) return sbyteValue;
            if (byte.TryParse(input, out var byteValue)) return byteValue;
            if (short.TryParse(input, out var shortValue)) return shortValue;
            if (ushort.TryParse(input, out var ushortValue)) return ushortValue;
            if (int.TryParse(input, out var intValue)) return intValue;
            if (uint.TryParse(input, out var uintValue)) return uintValue;
            if (long.TryParse(input, out var longValue)) return longValue;
            if (ulong.TryParse(input, out var ulongValue)) return ulongValue;

            throw new ArgumentOutOfRangeException(nameof(input),
                $"Input '{input}' is out of range for the {Name} Radix.");
        }
    }

    private class HexRadix() : Radix(nameof(Hex), nameof(Hex))
    {
        private const int BaseNumber = 16;
        private const int BitsPerByte = 2;
        private const string ByteSeparator = "_";
        private const string Pattern = @"(?<=\w)(?=(\w\w\w\w)+(?!\w))";

        protected override string Specifier => "16#";

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            var converted = value is not bool b ? ToBase(value, BaseNumber) : b ? "1" : "0";

            var formatted = Regex.Replace(converted, Pattern, ByteSeparator);

            return $"{Specifier}{formatted}";
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

            return ToValueType(value, BitsPerByte, BaseNumber);
        }
    }

    private class FloatRadix() : Radix(nameof(Float), nameof(Float))
    {
        protected override string Specifier => string.Empty;

        protected override bool HasFormat(string input)
        {
            //we don't care if it is positive or negative
            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                input = input.Remove(0, 1);
            }

            return input.Contains('.') && input.Replace(".", string.Empty).All(char.IsDigit);
        }

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            if (value is double d)
            {
                return d.ToString("0.0##############", CultureInfo.InvariantCulture);
            }

            return ((float)value).ToString("0.0######", CultureInfo.InvariantCulture);
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);
            return float.Parse(input); //todo will this be out of range if we get and LREAL/double?
        }
    }

    private class ExponentialRadix() : Radix(nameof(Exponential), nameof(Exponential))
    {
        protected override string Specifier => "";

        protected override bool HasFormat(string input)
        {
            //we don't care if it is positive or negative, so remove it.
            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                input = input.Remove(0, 1);
            }

            return !input.IsEmpty() && input.Contains(".")
                                    && input.Contains("e", StringComparison.OrdinalIgnoreCase)
                                    && ReplaceAll(input, new[] { ".", "e", "E", "+", "-" }, string.Empty)
                                        .All(char.IsDigit);
        }

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            if (value is double d)
            {
                return d.ToString("e16", CultureInfo.InvariantCulture);
            }

            return ((float)value).ToString("e8", CultureInfo.InvariantCulture);
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);
            return float.Parse(input);
        }

        private static string ReplaceAll(string value, IEnumerable<string> items, string replacement) =>
            items.Aggregate(value, (str, cItem) => str.Replace(cItem, replacement));
    }

    private class AsciiRadix() : Radix(nameof(Ascii), nameof(Ascii).ToUpper())
    {
        private const int BaseNumber = 16;
        private const int BitsPerByte = 2;
        private const char SpecifierChar = '\'';
        private const char ByteSeparator = '$';
        private const string Pattern = @"\$[A-Fa-f0-9]{2}|\$[tlpr'$]{1}|[\x00-\x7F]";

        private static readonly Dictionary<string, string> SpecialCharacters = new()
        {
            { "$t", "09" },
            { "$l", "0A" },
            { "$p", "0C" },
            { "$r", "0D" },
            { "$$", "24" },
            { "$'", "27" }
        };

        protected override string Specifier => "'";

        protected override bool HasFormat(string input) =>
            input.StartsWith(Specifier) && input.EndsWith(Specifier) && Regex.IsMatch(input, Pattern);

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            var converted = ToBase(value, BaseNumber);

            var formatted = GenerateAscii(converted);

            return $"{Specifier}{formatted}{Specifier}";
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);

            var value = GenerateHex(TrimSingle(input, SpecifierChar));

            return ToValueType(value, BitsPerByte, BaseNumber);
        }

        private static string TrimSingle(string value, char character)
        {
            if (value.StartsWith(character) && value.EndsWith(character))
                return value.Substring(1, value.Length - 2);

            if (value.StartsWith(character))
                return value.Substring(1, value.Length - 1);

            return value.EndsWith(character) ? value[..^2] : value;
        }

        private static string GenerateAscii(string str)
        {
            var builder = new StringBuilder();

            var segments = Segment(str, BitsPerByte);

            foreach (var segment in segments)
            {
                var character = Convert.ToChar(Convert.ToUInt16(segment, BaseNumber));

                if (character == 9 || character == 10 || character == 12 || character == 13 ||
                    character > 31 && character < 127)
                {
                    builder.Append(character);
                    continue;
                }

                builder.Append(ByteSeparator);
                builder.Append(segment.ToUpper());
            }

            return builder.ToString();
        }

        private static string GenerateHex(string input)
        {
            var matches = Regex.Matches(input, Pattern);

            var builder = new StringBuilder();

            foreach (Match match in matches)
            {
                var value = match.Value.Length switch
                {
                    3 => match.Value.TrimStart(ByteSeparator),
                    2 => SpecialCharacters[match.Value],
                    1 => $"{Convert.ToInt32(char.Parse(match.Value)):X}",
                    _ => string.Empty
                };

                builder.Append(value);
            }

            return builder.ToString();
        }

        private static IEnumerable<string> Segment(string input, int length)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0");

            for (var i = 0; i < input.Length; i += length)
                yield return input.Substring(i, i + length < input.Length ? length : input.Length - i);
        }
    }

    private class DateTimeRadix() : Radix(nameof(DateTime), "Date/Time")
    {
        private const string Separator = "_";
        private const string Suffix = "Z";
        private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";
        private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;

        protected override string Specifier => "DT#";

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            var timestamp = (long)value;

            var milliseconds = timestamp / 1000;
            var microseconds = timestamp % 1000;
            var ticks = microseconds * TicksPerMicrosecond;

            var time = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks);

            var formatted = time.ToString("yyyy-MM-dd-HH:mm:ss.ffffff");

            var str = Regex.Replace(formatted, InsertPattern, Separator);

            return $"{Specifier}{str}{Suffix}";
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty)
                .Replace(Separator, string.Empty)
                .Replace(Suffix, string.Empty);

            var time = System.DateTime.ParseExact(value, "yyyy-MM-dd-HH:mm:ss.ffffff",
                CultureInfo.InvariantCulture);

            return (time.Ticks - System.DateTime.UnixEpoch.Ticks) / TicksPerMicrosecond;
        }
    }

    private class DateTimeNsRadix() : Radix(nameof(DateTimeNs), "Date/Time (ns)")
    {
        private const string Separator = "_";
        private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";

        protected override string Specifier => "LDT#";

        public override string FormatValue(ValueType value)
        {
            ValidateType(value);

            var timestamp = (long)value;

            var milliseconds = timestamp / 1000000;
            var microseconds = timestamp % 1000000;
            var ticks = microseconds / 100;

            var localTime = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks).ToLocalTime();

            var formatted = localTime.ToString("yyyy-MM-dd-HH:mm:ss.fffffff00(UTCzzz)");

            var str = Regex.Replace(formatted, InsertPattern, Separator);

            return $"{Specifier}{str}";
        }

        public override ValueType ParseValue(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(Separator, string.Empty);

            var time = System.DateTime.ParseExact(value, "yyyy-MM-dd-HH:mm:ss.fffffff00(UTCzzz)",
                CultureInfo.InvariantCulture).ToUniversalTime();

            return (time.Ticks - System.DateTime.UnixEpoch.Ticks) * 100;
        }
    }
}