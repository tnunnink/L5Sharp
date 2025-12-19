using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable StringLiteralTypo

namespace L5Sharp.Core;

/// <summary>
/// Represents a fundamental enumeration of various radix types, used for defining number formats for atomic data values.
/// </summary>
/// <remarks>
/// Radix is a key part of this library in that it is how we format and parse tag data values in the L5X.
/// Each radix type has specific formatting and parsing logic to handle conversions. You can also use the static methods
/// on this class to infer the radix format from a given string value, since in some cases that information is not otherwise defined. 
/// </remarks>
public abstract class Radix : LogixEnum<Radix, string>
{
    private Radix(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Null radix, or absence of a Radix value.
    /// </summary>
    /// <remarks>
    /// All non value/atomic type data elements will have a null radix format.
    /// </remarks>
    public static readonly Radix Null = new NullRadix();

    /// <summary>
    /// Represents a Binary number base format.
    /// </summary>
    public static readonly Radix Binary = new BinaryRadix();

    /// <summary>
    /// Represents an Octal number base format. 
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
    /// Represents an Exponential number base format.
    /// </summary>
    public static readonly Radix Exponential = new ExponentialRadix();

    /// <summary>
    /// Represents a Float number base format.
    /// </summary>
    public static readonly Radix Float = new FloatRadix();

    /// <summary>
    /// Represents a radix format for ASCII encided character values.
    /// This format supports all integer-based atomic data values.
    /// </summary>
    public static readonly Radix Ascii = new AsciiRadix();

    /// <summary>
    /// Represents a radix format for Date/Time values having microsecond precision.
    /// This format only supports 64-bit value types such as <c>LINT</c>, <c>DT</c>, or <c>LDT</c>.
    /// </summary>
    public static readonly Radix DateTime = new DateTimeRadix();

    /// <summary>
    /// Represents a radix format for Date/Time values having nanosecond precision.
    /// This format only supports 64-bit value types such as <c>LINT</c>, <c>DT</c>, or <c>LDT</c>.
    /// </summary>
    public static readonly Radix DateTimeNs = new DateTimeNsRadix();

    /// <summary>
    /// Represents a radix format for 32-bit time-based values measured in microseconds.
    /// This radix format is specific to the <c>TIME32</c> data type in newer versions of Logix.
    /// </summary>
    public static readonly Radix Time32 = new Time32Radix();

    /// <summary>
    /// Represents a radix format for 64-bit time-based values measured in microseconds.
    /// This radix format is specific to the <c>TIME</c> data type in newer versions of Logix.
    /// </summary>
    public static readonly Radix Time = new TimeRadix();

    /// <summary>
    /// Represents a radix format for 64-bit time-based values measured in nanoseconds.
    /// This radix format is specific to the <c>LTIME</c> data type in newer versions of Logix.
    /// </summary>
    public static readonly Radix TimeNs = new TimeNsRadix();

    /// <summary>
    /// Determines the default <see cref="Radix"/> type for the specified data type.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> for which to determine the default <see cref="Radix"/>.</param>
    /// <returns>
    /// The default <see cref="Radix"/> instance corresponding to the provided data type.
    /// </returns>
    public static Radix Default(Type type)
    {
        return type switch
        {
            _ when type == typeof(REAL) || type == typeof(LREAL) => Float,
            _ when type == typeof(DT) => DateTime,
            _ when type == typeof(LDT) => DateTimeNs,
            _ when type == typeof(TIME32) => Time32,
            _ when type == typeof(TIME) => Time,
            _ when type == typeof(LTIME) => TimeNs,
            _ when typeof(AtomicData).IsAssignableFrom(type) => Decimal,
            _ => Null
        };
    }

    /// <summary>
    /// Infers the appropriate <see cref="Radix"/> type from the specified string value.
    /// </summary>
    /// <param name="value">The string value from which to determine the <see cref="Radix"/> type.</param>
    /// <returns>
    /// The <see cref="Radix"/> instance corresponding to the input value.
    /// </returns>
    /// <exception cref="FormatException">
    /// Thrown when the input value does not match any known <see cref="Radix"/> format.
    /// </exception>
    public static Radix Infer(string value)
    {
        var radix = All().FirstOrDefault(r => r.IsValidFormat(value));

        if (radix is null)
        {
            throw new FormatException($"Could not determine radix from value: {value}");
        }

        return radix;
    }

    /// <summary>
    /// Attempts to determine the correct <see cref="Radix"/> for the provided value.
    /// </summary>
    /// <param name="value">The string value to analyze for possible radix inference.</param>
    /// <param name="radix">
    /// When the method returns, contains the inferred <see cref="Radix"/> if the operation succeeds,
    /// or <see cref="Radix.Null"/> if no matching radix is found.
    /// </param>
    /// <returns>
    /// <c>true</c> if a matching <see cref="Radix"/> is successfully inferred from the value; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryInfer(string? value, out Radix radix)
    {
        radix = All().FirstOrDefault(r => r.IsValidFormat(value)) ?? Null;
        return radix != Null;
    }

    /// <summary>
    /// Formats the provided value type using the current radix.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>The formatted string representation of the value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    /// <exception cref="NotSupportedException">Thrown when the current radix does not support formatting the given value type.</exception>
    public virtual string Format<TValue>(TValue value) where TValue : struct
    {
        throw new NotSupportedException($"{Name} does not support formatting {typeof(TValue).Name}.");
    }

    /// <summary>
    /// Parses the specified string value into an instance of the defined value type.
    /// </summary>
    /// <typeparam name="TValue">The type of value to which the string will be parsed.</typeparam>
    /// <param name="value">The string value to parse into the specified type.</param>
    /// <returns>A value of type <typeparamref name="TValue"/> parsed from the provided string.</returns>
    /// <exception cref="NotSupportedException">Thrown when parsing for the specific <see cref="Radix"/> and <typeparamref name="TValue"/> is not supported.</exception>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="value"/> provided is null or empty.</exception>
    /// <exception cref="FormatException">Thrown when the <paramref name="value"/> does not match the expected format for the current <see cref="Radix"/>.</exception>
    public virtual TValue Parse<TValue>(string value) where TValue : struct
    {
        throw new NotSupportedException($"{Name} does not support parsing to {typeof(TValue).Name}.");
    }

    /// <summary>
    /// Determines whether the specified string value matches the expected format for the <see cref="Radix"/>.
    /// </summary>
    /// <param name="value">
    /// The string value to validate against the specific format of the <see cref="Radix"/>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the specified value matches the expected format of the <see cref="Radix"/>; otherwise, <c>false</c>.
    /// </returns>
    protected virtual bool IsValidFormat(string? value)
    {
        return false;
    }

    /// <summary>
    /// Determines whether the specified type is supported by the current <see cref="Radix"/>.
    /// </summary>
    /// <param name="type">The type to evaluate for compatibility with the <see cref="Radix"/>.</param>
    /// <returns>
    /// true if the specified type is supported; otherwise, false.
    /// </returns>
    protected virtual bool IsSupportedType(Type type)
    {
        return type == typeof(bool) ||
               type == typeof(sbyte) || type == typeof(byte) ||
               type == typeof(short) || type == typeof(ushort) ||
               type == typeof(int) || type == typeof(uint) ||
               type == typeof(long) || type == typeof(ulong);
    }

    #region RadixTypes

    /// <summary>
    /// Represents a null or uninitialized radix type within the LogixEnum structure.
    /// </summary>
    private class NullRadix() : Radix("NullType", "NullType");

    /// <summary>
    /// Represents a binary number base, typically used for formatting and parsing binary values.
    /// </summary>
    /// <remarks>
    /// This class extends the <see cref="Radix"/> abstraction to provide specific handling
    /// for binary-based numeric representations, including operations such as formatting
    /// atomic data into binary strings and parsing binary string values into their corresponding data types.
    /// </remarks>
    private class BinaryRadix() : Radix(nameof(Binary), nameof(Binary))
    {
        private const string Specifier = "2#";
        private const int BaseNumber = 2;
        private const int SegmentSize = 4;
        private const string Separator = "_";

        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Format(value);

            var formatted = FormatValue(value, BaseNumber).SeparateWith(Separator, SegmentSize);
            return $"{Specifier}{formatted}";
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            var data = value.Remove(0, Specifier.Length).Replace(Separator, string.Empty);

            if (Converters.TryGetValue(typeof(TValue), out var converter))
                return ((Func<string, int, TValue>)converter).Invoke(data, BaseNumber);

            return base.Parse<TValue>(value);
        }
    }

    /// <summary>
    /// Represents the octal number base for formatting and parsing Logix atomic data types.
    /// </summary>
    /// <remarks>
    /// Octal is a base-8 numeral system that uses the digits 0 through 7. This class provides functionality
    /// to format Logix atomic data types in octal representation and parse octal strings back into data types.
    /// </remarks>
    private class OctalRadix() : Radix(nameof(Octal), nameof(Octal))
    {
        private const string Specifier = "8#";
        private const int BaseNumber = 8;
        private const int SegmentSize = 3;
        private const string Separator = "_";

        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Format(value);

            var formatted = FormatValue(value, BaseNumber).SeparateWith(Separator, SegmentSize);
            return $"{Specifier}{formatted}";
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            var data = value.Remove(0, Specifier.Length).Replace(Separator, string.Empty);

            if (Converters.TryGetValue(typeof(TValue), out var converter))
                return ((Func<string, int, TValue>)converter).Invoke(data, BaseNumber);

            return base.Parse<TValue>(value);
        }
    }

    /// <summary>
    /// Represents the decimal radix used to specify base-10 numeric formatting and parsing for atomic data types.
    /// </summary>
    private class DecimalRadix() : Radix(nameof(Decimal), nameof(Decimal))
    {
        private const int BaseNumber = 10;

        protected override bool IsValidFormat(string? value)
        {
            if (value is null)
                return false;

            return value.ToLower() switch
            {
                "true" => true,
                "false" => true,
                _ => value.All(c => char.IsDigit(c) || c == '+' || c == '-')
            };
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Format(value);

            return FormatValue(value, BaseNumber);
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            if (Converters.TryGetValue(typeof(TValue), out var converter))
                return ((Func<string, int, TValue>)converter).Invoke(value, BaseNumber);

            return base.Parse<TValue>(value);
        }
    }

    /// <summary>
    /// Represents the hexadecimal radix system for formatting and parsing data values.
    /// Provides functionality to format atomic data types into hexadecimal representation
    /// and parse hexadecimal strings back to their original data types.
    /// </summary>
    private class HexRadix() : Radix(nameof(Hex), nameof(Hex))
    {
        private const string Specifier = "16#";
        private const int BaseNumber = 16;
        private const int SegmentSize = 4;
        private const string Separator = "_";

        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Format(value);

            var formatted = FormatValue(value, BaseNumber).SeparateWith(Separator, SegmentSize);
            return $"{Specifier}{formatted}";
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            var data = value.Remove(0, Specifier.Length).Replace(Separator, string.Empty);

            if (Converters.TryGetValue(typeof(TValue), out var converter))
                return ((Func<string, int, TValue>)converter).Invoke(data, BaseNumber);

            return base.Parse<TValue>(value);
        }
    }

    /// <summary>
    /// Represents a radix type used for floating-point values in Logix systems.
    /// </summary>
    private class FloatRadix() : Radix(nameof(Float), nameof(Float))
    {
        private const string QNAN = "1.#QNAN";
        private const string DoubleFormat = "0.0##############";
        private const string SingleFormat = "0.0######";

        protected override bool IsValidFormat(string? value)
        {
            return value switch
            {
                null => false,
                QNAN => true,
                _ => value.All(c => char.IsDigit(c) || c == '.' || c == '+' || c == '-')
            };
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(float) || type == typeof(double);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Format(value);

            return value switch
            {
                float.NaN => QNAN,
                float a => a.ToString(SingleFormat, CultureInfo.InvariantCulture),
                double.NaN => QNAN,
                double a => a.ToString(DoubleFormat, CultureInfo.InvariantCulture),
                _ => base.Format(value)
            };
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            if (Converters.TryGetValue(typeof(TValue), out var converter))
                return ((Func<string, TValue>)converter).Invoke(value);

            return base.Parse<TValue>(value);
        }
    }

    /// <summary>
    /// Represents an exponential base radix for use in formatting and parsing numeric data types.
    /// </summary>
    private class ExponentialRadix() : Radix(nameof(Exponential), nameof(Exponential))
    {
        private const string QNAN = "1.#QNAN";
        private const string DoubleExponent = "e16";
        private const string SingleExponent = "e8";
        private static readonly HashSet<char> ValidCharacters = ['.', '+', '-', 'e', 'E'];

        protected override bool IsValidFormat(string? value)
        {
            return value switch
            {
                null => false,
                QNAN => true,
                _ => value.All(c => char.IsDigit(c) || ValidCharacters.Contains(c))
            };
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(float) || type == typeof(double);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Format(value);

            return value switch
            {
                float a => a.ToString(SingleExponent, CultureInfo.InvariantCulture),
                double a => a.ToString(DoubleExponent, CultureInfo.InvariantCulture),
                _ => base.Format(value)
            };
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            if (Converters.TryGetValue(typeof(TValue), out var converter))
                return ((Func<string, TValue>)converter).Invoke(value);

            return base.Parse<TValue>(value);
        }
    }

    private class AsciiRadix() : Radix(nameof(Ascii), nameof(Ascii).ToUpper())
    {
        private const string Specifier = "'";
        private const int BaseNumber = 16;
        private const int ByteWidth = 2;
        private const char SpecifierChar = '\'';
        private const char Separator = '$';

        private static readonly Regex AsciiPattern =
            new(@"\$[A-Fa-f0-9]{2}|\$[tlpr'$]{1}|[\x00-\x7F]", RegexOptions.Compiled);

        private static readonly Dictionary<string, string> SpecialCharacters = new()
        {
            { "$t", "09" },
            { "$l", "0A" },
            { "$p", "0C" },
            { "$r", "0D" },
            { "$$", "24" },
            { "$'", "27" }
        };

        protected override bool IsValidFormat(string? value)
        {
            return value is not null &&
                   value.StartsWith(Specifier) &&
                   value.EndsWith(Specifier) &&
                   AsciiPattern.IsMatch(value);
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(sbyte) || type == typeof(byte) ||
                   type == typeof(short) || type == typeof(ushort) ||
                   type == typeof(int) || type == typeof(uint) ||
                   type == typeof(long) || type == typeof(ulong);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Format(value);

            var formatted = FormatValue(value, BaseNumber);
            return $"{Specifier}{GenerateAscii(formatted)}{Specifier}";
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            var data = GenerateHex(value.TrimSingle(SpecifierChar));

            if (Converters.TryGetValue(typeof(TValue), out var converter))
                return ((Func<string, int, TValue>)converter).Invoke(data, BaseNumber);

            return base.Parse<TValue>(value);
        }

        private static string GenerateAscii(string str)
        {
            var builder = new StringBuilder();

            var segments = Segment(str, ByteWidth);

            foreach (var segment in segments)
            {
                //If this is a special logix character, we need to add the escape '$' and then that char...
                var special = SpecialCharacters.FirstOrDefault(v => v.Value.IsEquivalent(segment));
                if (special.Key is not null)
                {
                    builder.Append(special.Key);
                    continue;
                }

                //Chars between 31 and 127 are printable characters so append.
                var character = Convert.ToChar(Convert.ToUInt16(segment, BaseNumber));
                if (character > 31 && character < 127)
                {
                    builder.Append(character);
                    continue;
                }

                //Everything else is represented as Hex with the '$' escape character.
                builder.Append(Separator);
                builder.Append(segment.ToUpper());
            }

            return builder.ToString();
        }

        private static string GenerateHex(string input)
        {
            var matches = AsciiPattern.Matches(input);

            var builder = new StringBuilder();

            foreach (Match match in matches)
            {
                var value = match.Value.Length switch
                {
                    3 => match.Value.TrimStart(Separator),
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
        private const string Specifier = "DT#";
        private const string Separator = "_";
        private const string Suffix = "Z";
        private const string DateTimeFormat = "yyyy-MM-dd-HH:mm:ss.ffffff";
        private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private static readonly Func<long, long> ValueConverter = l => l;


        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(long);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (value is not long timestamp)
                return base.Format(value);

            var milliseconds = timestamp / 1000;
            var microseconds = timestamp % 1000;
            var ticks = microseconds * TicksPerMicrosecond;

            var time = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks);

            var formatted = time.ToString(DateTimeFormat);
            //var str = Regex.Replace(formatted, InsertPattern, Separator);
            return $"{Specifier}{formatted}{Suffix}";
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            //Trim any formatting characters we don't need.
            var formatted = value
                .Remove(0, Specifier.Length)
                .Replace(Separator, string.Empty)
                .Replace(Suffix, string.Empty);

            var dateTime = System.DateTime.ParseExact(
                formatted,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal
            );

            var timestamp = (dateTime.Ticks - UnixEpoch.Ticks) / TicksPerMicrosecond;
            return ((Func<long, TValue>)(object)ValueConverter).Invoke(timestamp);
        }
    }

    private class DateTimeNsRadix() : Radix(nameof(DateTimeNs), "Date/Time (ns)")
    {
        private const string Specifier = "LDT#";
        private const string Separator = "_";
        private const string Suffix = "00Z";
        private const string DateTimeFormat = "yyyy-MM-dd-HH:mm:ss.fffffff";
        private const long NanosecondsPerTick = 100;
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private static readonly Func<long, long> ValueConverter = l => l;


        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(long);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (value is not long timestamp)
                return base.Format(value);

            var milliseconds = timestamp / 1000000;
            var microseconds = timestamp % 1000000;
            var ticks = microseconds / 100;

            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks);

            return $"{Specifier}{dateTime.ToString(DateTimeFormat)}{Suffix}";
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            //Trim any formatting characters we don't need.
            var formatted = value
                .Remove(0, Specifier.Length)
                .Replace(Separator, string.Empty)
                .Replace(Suffix, string.Empty);

            var dateTime = System.DateTime.ParseExact(
                formatted,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal
            );

            var timestamp = (dateTime.Ticks - UnixEpoch.Ticks) * NanosecondsPerTick;
            return ((Func<long, TValue>)(object)ValueConverter).Invoke(timestamp);
        }
    }

    private class Time32Radix() : Radix(nameof(Time32), "Time32 (us)")
    {
        private const string Specifier = "T32#";
        private const string Separator = "_";
        private static readonly Func<int, int> ValueConverter = l => l;

        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(int);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (value is not int timestamp)
                return base.Format(value);

            // Handle the zero case immediately
            if (timestamp == 0) return $"{Specifier}0us";

            var signed = timestamp < 0;
            var milliseconds = Math.Abs(timestamp / 1000);
            var microseconds = Math.Abs(timestamp % 1000);
            var time = TimeSpan.FromMilliseconds(milliseconds);

            var builder = new StringBuilder();
            builder.Append(Specifier);
            if (signed) builder.Append('-');

            //Logix only shows the time value of parts that are not zero.
            if (time.Minutes != 0) builder.Append(time.Minutes).Append('m').Append(Separator);
            if (time.Seconds != 0) builder.Append(time.Seconds).Append('s').Append(Separator);
            if (time.Milliseconds != 0) builder.Append(time.Milliseconds).Append("ms").Append(Separator);
            if (microseconds != 0) builder.Append(microseconds).Append("us").Append(Separator);

            // Remove the trailing separator, where ever it may be.
            if (builder.Length > Specifier.Length)
                builder.Length--;

            return builder.ToString();
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            var stripped = value.Substring(Specifier.Length);
            var signed = stripped.StartsWith('-');

            var parts = stripped
                .TrimStart('-')
                .Split([Separator], StringSplitOptions.RemoveEmptyEntries);

            var timestamp = 0;

            foreach (var part in parts)
            {
                if (part.EndsWith("us"))
                    timestamp += int.Parse(part.Substring(0, part.Length - 2));
                else if (part.EndsWith("ms"))
                    timestamp += int.Parse(part.Substring(0, part.Length - 2)) * 1000;
                else if (part.EndsWith("s"))
                    timestamp += int.Parse(part.Substring(0, part.Length - 1)) * 1000000;
                else if (part.EndsWith("m"))
                    timestamp += int.Parse(part.Substring(0, part.Length - 1)) * 60000000;
            }

            var result = signed ? -timestamp : timestamp;
            return ((Func<int, TValue>)(object)ValueConverter).Invoke(result);
        }
    }

    private class TimeRadix() : Radix(nameof(Time), "Time (us)")
    {
        private const string Specifier = "T#";
        private const string Separator = "_";
        private static readonly Func<long, long> ValueConverter = l => l;

        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(long);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (value is not long timestamp)
                return base.Format(value);

            // Handle the zero case immediately
            if (timestamp == 0) return $"{Specifier}0us";

            var signed = timestamp < 0;
            var milliseconds = Math.Abs(timestamp / 1000);
            var microseconds = Math.Abs(timestamp % 1000);
            var time = TimeSpan.FromMilliseconds(milliseconds);

            var builder = new StringBuilder();
            builder.Append(Specifier);
            if (signed) builder.Append('-');

            //Logix only shows the time value of parts that are not zero.
            if (time.Days != 0) builder.Append(time.Days).Append('d').Append(Separator);
            if (time.Hours != 0) builder.Append(time.Hours).Append('h').Append(Separator);
            if (time.Minutes != 0) builder.Append(time.Minutes).Append('m').Append(Separator);
            if (time.Seconds != 0) builder.Append(time.Seconds).Append('s').Append(Separator);
            if (time.Milliseconds != 0) builder.Append(time.Milliseconds).Append("ms").Append(Separator);
            if (microseconds != 0) builder.Append(microseconds).Append("us").Append(Separator);

            // Remove the trailing separator, where ever it may be.
            if (builder.Length > Specifier.Length)
                builder.Length--;

            return builder.ToString();
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            var stripped = value.Substring(Specifier.Length);
            var signed = stripped.StartsWith('-');

            var parts = stripped
                .TrimStart('-')
                .Split([Separator], StringSplitOptions.RemoveEmptyEntries);

            long timestamp = 0;

            foreach (var part in parts)
            {
                if (part.EndsWith("us"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 2));
                else if (part.EndsWith("ms"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 2)) * 1000;
                else if (part.EndsWith("s"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 1000000;
                else if (part.EndsWith("m"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 60000000;
                else if (part.EndsWith("h"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 3600000000;
                else if (part.EndsWith("d"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 86400000000;
            }

            var result = signed ? -timestamp : timestamp;
            return ((Func<long, TValue>)(object)ValueConverter).Invoke(result);
        }
    }

    private class TimeNsRadix() : Radix(nameof(TimeNs), "LTime (ns)")
    {
        private const string Specifier = "LT#";
        private const string Separator = "_";
        private static readonly Func<long, long> ValueConverter = l => l;

        protected override bool IsValidFormat(string? value)
        {
            return value is not null && value.StartsWith(Specifier);
        }

        protected override bool IsSupportedType(Type type)
        {
            return type == typeof(long);
        }

        public override string Format<TValue>(TValue value) where TValue : struct
        {
            if (value is not long timestamp)
                return base.Format(value);

            // Handle the zero case immediately
            if (timestamp == 0) return $"{Specifier}0ns";

            var signed = timestamp < 0;
            var milliseconds = Math.Abs(timestamp / 1000000);
            var microseconds = Math.Abs(timestamp % 1000000) / 1000;
            var nanoseconds = Math.Abs(timestamp % 1000);
            var time = TimeSpan.FromMilliseconds(milliseconds);

            var builder = new StringBuilder();
            builder.Append(Specifier);
            if (signed) builder.Append('-');

            //Logix only shows the time value of parts that are not zero.
            if (time.Days != 0) builder.Append(time.Days).Append('d').Append(Separator);
            if (time.Hours != 0) builder.Append(time.Hours).Append('h').Append(Separator);
            if (time.Minutes != 0) builder.Append(time.Minutes).Append('m').Append(Separator);
            if (time.Seconds != 0) builder.Append(time.Seconds).Append('s').Append(Separator);
            if (time.Milliseconds != 0) builder.Append(time.Milliseconds).Append("ms").Append(Separator);
            if (microseconds != 0) builder.Append(microseconds).Append("us").Append(Separator);
            if (nanoseconds != 0) builder.Append(nanoseconds).Append("ns").Append(Separator);

            // Remove the trailing separator, where ever it may be.
            if (builder.Length > Specifier.Length)
                builder.Length--;

            return builder.ToString();
        }

        public override TValue Parse<TValue>(string value)
        {
            if (!IsSupportedType(typeof(TValue)))
                return base.Parse<TValue>(value);

            ValidateFormat(value);

            var stripped = value.Substring(Specifier.Length);
            var signed = stripped.StartsWith('-');

            var parts = stripped
                .TrimStart('-')
                .Split([Separator], StringSplitOptions.RemoveEmptyEntries);

            long timestamp = 0;

            foreach (var part in parts)
            {
                if (part.EndsWith("ns"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 2));
                else if (part.EndsWith("us"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 2)) * 1000;
                else if (part.EndsWith("ms"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 2)) * 1000000;
                else if (part.EndsWith("s"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 1000000000;
                else if (part.EndsWith("m"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 60000000000;
                else if (part.EndsWith("h"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 3600000000000;
                else if (part.EndsWith("d"))
                    timestamp += long.Parse(part.Substring(0, part.Length - 1)) * 86400000000000;
            }

            var result = signed ? -timestamp : timestamp;

            return ((Func<long, TValue>)(object)ValueConverter).Invoke(result);
        }
    }

    #endregion

    /// <summary>
    /// Formats the specified value according to the provided base number.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to format, restricted to struct types.</typeparam>
    /// <param name="value">The value to be formatted.</param>
    /// <param name="baseNumber">The base number used for formatting the value (e.g., 2 for binary, 8 for octal).</param>
    /// <returns>
    /// A string representation of the value formatted in the specified base number.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Thrown when the type of <paramref name="value"/> is not supported for formatting.
    /// </exception>
    private string FormatValue<TValue>(TValue value, int baseNumber) where TValue : struct
    {
        return value switch
        {
            bool x => FormatValue(x),
            sbyte x => FormatValue(x, baseNumber),
            byte x => FormatValue(x, baseNumber),
            short x => FormatValue(x, baseNumber),
            ushort x => FormatValue(x, baseNumber),
            int x => FormatValue(x, baseNumber),
            uint x => FormatValue(x, baseNumber),
            long x => FormatValue(x, baseNumber),
            ulong x => FormatValue(x, baseNumber),
            _ => throw new NotSupportedException($"{Name} does not support formatting {value.GetType().Name}.")
        };
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the given number base.
    /// </summary>
    /// <param name="value">The value to be formatted.</param>
    /// <returns>
    /// A string representation of the value formatted according to the specified base.
    /// </returns>
    private static string FormatValue(bool value)
    {
        return value ? "1" : "0";
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(sbyte value, int baseNumber)
    {
        var width = ComputeWidth(8, baseNumber);
        var converted = Convert.ToString((byte)value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(byte value, int baseNumber)
    {
        var width = ComputeWidth(8, baseNumber);
        var converted = Convert.ToString(value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(short value, int baseNumber)
    {
        var width = ComputeWidth(16, baseNumber);
        var converted = Convert.ToString(value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(ushort value, int baseNumber)
    {
        var width = ComputeWidth(16, baseNumber);
        var converted = Convert.ToString((short)value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(int value, int baseNumber)
    {
        var width = ComputeWidth(32, baseNumber);
        var converted = Convert.ToString(value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(uint value, int baseNumber)
    {
        var width = ComputeWidth(32, baseNumber);
        var converted = Convert.ToString((int)value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(long value, int baseNumber)
    {
        var width = ComputeWidth(64, baseNumber);
        var converted = Convert.ToString(value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// Formats the specified value into a string representation based on the provided base number.
    /// </summary>
    /// <param name="value">The atomic data instance to format.</param>
    /// <param name="baseNumber">The base (radix) to be used for formatting the value.</param>
    /// <returns>
    /// A string representation of the formatted value based on the specified base.
    /// </returns>
    private static string FormatValue(ulong value, int baseNumber)
    {
        var width = ComputeWidth(64, baseNumber);
        var converted = Convert.ToString((long)value, baseNumber);
        return converted.PadLeft(width, '0');
    }

    /// <summary>
    /// These let us convert a provided string to a value type without boxing it as an object.
    /// </summary>
    private static readonly Dictionary<Type, Delegate> Converters = new()
    {
        { typeof(bool), new Func<string, int, bool>((s, _) => ConvertToBool(s)) },
        { typeof(sbyte), new Func<string, int, sbyte>(ConvertToSByte) },
        { typeof(byte), new Func<string, int, byte>(ConvertToByte) },
        { typeof(short), new Func<string, int, short>(ConvertToInt16) },
        { typeof(ushort), new Func<string, int, ushort>(ConvertToUInt16) },
        { typeof(int), new Func<string, int, int>(ConvertToInt32) },
        { typeof(uint), new Func<string, int, uint>(ConvertToUInt32) },
        { typeof(long), new Func<string, int, long>(ConvertToInt64) },
        { typeof(ulong), new Func<string, int, ulong>(ConvertToUInt64) },
        { typeof(float), new Func<string, float>(ConvertToSingle) },
        { typeof(double), new Func<string, double>(ConvertToDouble) }
    };

    /// <summary>
    /// Converts the specified string value to its boolean equivalent.
    /// </summary>
    /// <param name="value">The string value to convert to a boolean.</param>
    /// <returns>
    /// A boolean value derived from the string input. Returns <c>true</c> for "1",
    /// <c>false</c> for "0", and uses <see cref="Convert.ToBoolean(string)"/> for all other cases.
    /// </returns>
    private static bool ConvertToBool(string value)
    {
        return value switch
        {
            "1" => true,
            "0" => false,
            _ => Convert.ToBoolean(value)
        };
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="sbyte"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="sbyte"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static sbyte ConvertToSByte(string value, int baseNumber)
    {
        return Convert.ToSByte(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="byte"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="byte"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static byte ConvertToByte(string value, int baseNumber)
    {
        return Convert.ToByte(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="short"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="short"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static short ConvertToInt16(string value, int baseNumber)
    {
        return Convert.ToInt16(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="int"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="int"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static int ConvertToInt32(string value, int baseNumber)
    {
        return Convert.ToInt32(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="long"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="long"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static long ConvertToInt64(string value, int baseNumber)
    {
        return Convert.ToInt64(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="ushort"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="ushort"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static ushort ConvertToUInt16(string value, int baseNumber)
    {
        return Convert.ToUInt16(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="uint"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="uint"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static uint ConvertToUInt32(string value, int baseNumber)
    {
        return Convert.ToUInt32(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string representation of a number in a specified base to its <see cref="ulong"/> equivalent.
    /// </summary>
    /// <param name="value">The string representation of the number to convert.</param>
    /// <param name="baseNumber">The base of the number in the specified string.</param>
    /// <returns>The <see cref="ulong"/> equivalent of the number contained in <paramref name="value"/>.</returns>
    private static ulong ConvertToUInt64(string value, int baseNumber)
    {
        return Convert.ToUInt64(value, baseNumber);
    }

    /// <summary>
    /// Converts the specified string value to a single-precision floating-point number.
    /// </summary>
    /// <param name="value">The string representation of a number to be converted.</param>
    /// <returns>
    /// The <see cref="float"/> representation of the specified string, or <see cref="float.NaN"/> if the value
    /// is "1.#QNAN", which is the Rockwell representation of "not a number".
    /// </returns>
    private static float ConvertToSingle(string value)
    {
        return value == "1.#QNAN" ? float.NaN : Convert.ToSingle(value, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts the specified string value to a double-precision floating-point number.
    /// </summary>
    /// <param name="value">The string representation of a number to be converted.</param>
    /// <returns>
    /// The <see cref="double"/> representation of the specified string, or <see cref="double.NaN"/> if the value
    /// is "1.#QNAN", which is the Rockwell representation of "not a number".
    /// </returns>
    private static double ConvertToDouble(string value)
    {
        return value == "1.#QNAN" ? double.NaN : Convert.ToDouble(value, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Computes the width required to represent a value of a given size using the specified base number.
    /// </summary>
    /// <param name="size">The size of the value in bits.</param>
    /// <param name="baseNumber">The base number to be used for representation (e.g., 2 for binary, 10 for decimal).</param>
    /// <returns>The computed width required to represent the value in the given base.</returns>
    private static int ComputeWidth(int size, int baseNumber)
    {
        if (baseNumber == 10)
            return 0;

        return (int)Math.Ceiling(size * Math.Log(2) / Math.Log(baseNumber));
    }

    /// <summary>
    /// Validates whether the specified value conforms to the expected format for the current <see cref="Radix"/>.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <exception cref="ArgumentException">Thrown when the specified value is null or empty.</exception>
    /// <exception cref="FormatException">Thrown when the specified value does not match the expected format for the current <see cref="Radix"/>.</exception>
    private void ValidateFormat(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Value can not be null or empty.", nameof(value));

        if (!IsValidFormat(value))
            throw new FormatException($"Invalid {Name} format: '{value}' ");
    }
}