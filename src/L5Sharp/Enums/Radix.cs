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
    /// Represents a Null radix, or absence of a Radix value.
    /// </summary>
    /// <remarks>
    /// Only <see cref="AtomicType"/> types have non-null Radix. <see cref="StructureType"/> types all have null Radix.
    /// </remarks>
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

    /// <inheritdoc />
    public override string ToString() => Value;

    /// <summary>
    /// Gets the default <see cref="Radix"/> value for the provided logix type.
    /// </summary>
    /// <param name="type">The logix type to evaluate.</param>
    /// <returns>
    /// <see cref="Null"/> for all non atomic types.
    /// <see cref="Float"/> for <see cref="REAL"/> types.
    /// <see cref="Decimal"/> for all other atomic types.
    /// </returns>
    public static Radix Default(LogixType type)
    {
        if (type is not AtomicType atomicType)
            return Null;

        return atomicType is REAL or LREAL ? Float : Decimal;
    }

    /// <summary>
    /// Determines if the current <see cref="Radix"/> supports the provided data type instance.
    /// </summary>
    /// <param name="type">The logix type instance to evaluate.</param>
    /// <returns>true if the current radix value is valid for the given data type instance; otherwise, false.</returns>
    public bool SupportsType(LogixType type)
    {
        if (type is not AtomicType atomicType)
            return Equals(Null);

        return atomicType switch
        {
            BOOL => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex),
            LINT => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii) ||
                    Equals(DateTime) || Equals(DateTimeNs),
            REAL => Equals(Float) || Equals(Exponential),
            LREAL => Equals(Float) || Equals(Exponential),
            _ => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii)
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
                @$"Could not determine Radix from input '{input}'. 
                        Verify that the input string is an accepted Radix format.");

        return FromName(name);
    }

    /// <summary>
    /// Tries to infer the radix format from a string representing a formatted atomic value.
    /// </summary>
    /// <param name="input">The string input for which to infer the radix format.</param>
    /// <param name="radix">If parsed successfully, then the <see cref="Radix"/> representing the format of the input;
    /// Otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if a <c>Radix</c> format was inferred from the string input; Otherwise, <c>false</c>.</returns>
    public static bool TryInfer(string input, out Radix? radix)
    {
        var name = Identifiers.FirstOrDefault(i => i.Value.Invoke(input)).Key;
        radix = name is not null ? FromName(name) : null;
        return radix is not null;
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
    /// Returns an indication as to whether the current string input value has the format of the current Radix type.
    /// </summary>
    /// <param name="input">The input text value to examine.</param>
    /// <returns><c>true</c> if <c>input</c> qualifies as a valid format for the Radix type; otherwise, <c>false</c>.</returns>
    protected virtual bool HasFormat(string input) => !input.IsEmpty() && input.StartsWith(Specifier);

    /// <summary>
    /// Converts an atomic value to the current radix base format. 
    /// </summary>
    /// <param name="atomic">The current atomic type to convert.</param>
    /// <returns>
    /// A string that represents the value of the atomic type in the current radix base number style.
    /// </returns>
    public abstract string Format(AtomicType atomic);

    /// <summary>
    /// Parses a string input of a given Radix formatted value into an atomic value type. 
    /// </summary>
    /// <param name="input">The string value to parse.</param>
    /// <returns>An <see cref="AtomicType"/> representing the value of the formatted string.</returns>
    public abstract AtomicType Parse(string input);

    /// <summary>
    /// Converts the provided <see cref="AtomicType"/> to the specified base number.
    /// </summary>
    /// <param name="type">The atomic type to convert.</param>
    /// <param name="baseNumber">The base of the return value, which must be 2, 8, 10, or 16.</param>
    /// <returns>A <see cref="string"/> value representing the value in the specified base.</returns>
    /// <exception cref="ArgumentException">baseNumber is not 2, 8, 10, or 16.</exception>
    private static string ToBase(AtomicType type, int baseNumber)
    {
        var bitsPerByte = baseNumber switch
        {
            2 => 8,
            8 => 3,
            _ => 2
        };

        var bytes = type.GetBytes();
        var builder = new StringBuilder();

        for (var ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
        {
            var byteString = Convert.ToString(bytes[ctr], baseNumber).PadLeft(bitsPerByte, '0');
            builder.Append(byteString);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts the provided <see cref="string"/> to a <see cref="AtomicType"/> given the provided bitsPerByte and baseNumber.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <param name="charsPerByte">The number of chars in <c>value</c> that represented a single byte of data.</param>
    /// <param name="baseNumber">The base number of the return value, which must be 2, 8, 10, or 16.</param>
    /// <returns>A <see cref="string"/> value representing the value in the specified base.</returns>
    /// <exception cref="ArgumentException">baseNumber is not 2, 8, 10, or 16.</exception>
    private static AtomicType ToAtomic(string value, int charsPerByte, int baseNumber)
    {
        if (value.IsEmpty())
            throw new ArgumentException("Value can not be empty.");

        var byteLength = value.Length / charsPerByte;

        return byteLength switch
        {
            0 => new BOOL(value == "1"),
            > 0 and <= 1 => new SINT(Convert.ToSByte(value, baseNumber)),
            > 1 and <= 2 => new INT(Convert.ToInt16(value, baseNumber)),
            > 2 and <= 4 => new DINT(Convert.ToInt32(value, baseNumber)),
            > 4 and <= 8 => new LINT(Convert.ToInt64(value, baseNumber)),
            _ => throw new ArgumentOutOfRangeException(nameof(byteLength),
                $"The provided value '{value}' is out of range for atomic conversion. Must be between 0 and 8 bytes.")
        };
    }

    private void ValidateFormat(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input can not be null or empty.", nameof(input));

        if (!Identifiers[Name].Invoke(input))
            throw new FormatException($"Input '{input}' does not have expected {Name} format.");
    }

    private void ValidateType(AtomicType atomic)
    {
        if (atomic is null)
            throw new ArgumentNullException(nameof(atomic));

        if (!SupportsType(atomic))
            throw new NotSupportedException($"{atomic.GetType()} is not supported by {Name} Radix.");
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

    private class NullRadix : Radix
    {
        public NullRadix() : base("NullType", "NullType")
        {
        }

        protected override string Specifier => string.Empty;

        protected override bool HasFormat(string input) =>
            throw new NotSupportedException($"{Name} Radix does not support formatting atomic values");

        public override string Format(AtomicType atomic) =>
            throw new NotSupportedException($"{Name} Radix does not support formatting atomic values");

        public override AtomicType Parse(string input) =>
            throw new NotSupportedException($"{Name} Radix does not support parsing atomic values");
    }

    private class BinaryRadix : Radix
    {
        private const int BaseNumber = 2;
        private const int CharsPerByte = 8;
        private const string ByteSeparator = "_";
        private const string Pattern = @"(?<=\d)(?=(\d\d\d\d)+(?!\d))";

        public BinaryRadix() : base(nameof(Binary), nameof(Binary))
        {
        }

        protected override string Specifier => "2#";

        protected override bool HasFormat(string input) => !input.IsEmpty() && input.StartsWith(Specifier);

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            var converted = atomic is not BOOL b ? ToBase(atomic, BaseNumber) : b ? "1" : "0";

            var formatted = Regex.Replace(converted, Pattern, ByteSeparator);

            return $"{Specifier}{formatted}";
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

            return ToAtomic(value, CharsPerByte, BaseNumber);
        }
    }

    private class OctalRadix : Radix
    {
        private const int BaseNumber = 8;
        private const int CharsPerByte = 3;
        private const string ByteSeparator = "_";
        private const string Pattern = @"(?<=\d)(?=(\d\d\d)+(?!\d))";

        public OctalRadix() : base(nameof(Octal), nameof(Octal))
        {
        }

        protected override string Specifier => "8#";

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            var converted = atomic is not BOOL b ? ToBase(atomic, BaseNumber) : b ? "1" : "0";

            var formatted = Regex.Replace(converted, Pattern, ByteSeparator);

            return $"{Specifier}{formatted}";
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

            return ToAtomic(value, CharsPerByte, BaseNumber);
        }
    }

    private class DecimalRadix : Radix
    {
        public DecimalRadix() : base(nameof(Decimal), nameof(Decimal))
        {
        }

        protected override string Specifier => string.Empty;

        protected override bool HasFormat(string input)
        {
            if (input.StartsWith("+") || input.StartsWith("-"))
            {
                input = input.Remove(0, 1);
            }

            return !input.IsEmpty() && input.All(char.IsDigit);
        }

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            return atomic switch
            {
                BOOL v => v ? "1" : "0",
                SINT v => ((sbyte)v).ToString(),
                INT v => ((short)v).ToString(),
                DINT v => ((int)v).ToString(),
                LINT v => ((int)v).ToString(),
                USINT v => ((byte)v).ToString(),
                UINT v => ((ushort)v).ToString(),
                UDINT v => ((uint)v).ToString(),
                ULINT v => ((ulong)v).ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(atomic), atomic, null)
            };
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            if (sbyte.TryParse(input, out var sbyteValue))
                return new SINT(sbyteValue, this);

            if (byte.TryParse(input, out var byteValue))
                return new USINT(byteValue, this);

            if (short.TryParse(input, out var shortValue))
                return new INT(shortValue, this);

            if (ushort.TryParse(input, out var ushortValue))
                return new UINT(ushortValue, this);

            if (int.TryParse(input, out var intValue))
                return new DINT(intValue, this);

            if (uint.TryParse(input, out var uintValue))
                return new UDINT(uintValue, this);

            if (long.TryParse(input, out var longValue))
                return new LINT(longValue, this);

            if (ulong.TryParse(input, out var ulongValue))
                return new ULINT(ulongValue, this);

            throw new ArgumentOutOfRangeException(nameof(input),
                $"Input '{input}' is out of range for the {Name} Radix.");
        }
    }

    private class HexRadix : Radix
    {
        private const int BaseNumber = 16;
        private const int BitsPerByte = 2;
        private const string ByteSeparator = "_";
        private const string Pattern = @"(?<=\w)(?=(\w\w\w\w)+(?!\w))";

        public HexRadix() : base(nameof(Hex), nameof(Hex))
        {
        }

        protected override string Specifier => "16#";

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            var converted = atomic is not BOOL b ? ToBase(atomic, BaseNumber) : b ? "1" : "0";

            var formatted = Regex.Replace(converted, Pattern, ByteSeparator);

            return $"{Specifier}{formatted}";
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

            return ToAtomic(value, BitsPerByte, BaseNumber);
        }
    }

    private class FloatRadix : Radix
    {
        public FloatRadix() : base(nameof(Float), nameof(Float))
        {
        }

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

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            if (atomic is LREAL lreal)
            {
                return ((double)lreal).ToString("0.0##############", CultureInfo.InvariantCulture);
            }

            return ((float)(REAL)atomic).ToString("0.0######", CultureInfo.InvariantCulture);
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            return new REAL(float.Parse(input));
        }
    }

    private class ExponentialRadix : Radix
    {
        public ExponentialRadix() : base(nameof(Exponential), nameof(Exponential))
        {
        }

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

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            if (atomic is LREAL lreal)
            {
                return ((double)lreal).ToString("e16", CultureInfo.InvariantCulture);
            }

            return ((float)(REAL)atomic).ToString("e8", CultureInfo.InvariantCulture);
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            return new REAL(float.Parse(input));
        }

        private static string ReplaceAll(string value, IEnumerable<string> items, string replacement) =>
            items.Aggregate(value, (str, cItem) => str.Replace(cItem, replacement));
    }

    private class AsciiRadix : Radix
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

        public AsciiRadix() : base(nameof(Ascii), nameof(Ascii).ToUpper())
        {
        }

        protected override string Specifier => "'";

        protected override bool HasFormat(string input) =>
            input.StartsWith(Specifier) && input.EndsWith(Specifier) && Regex.IsMatch(input, Pattern);

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            var converted = ToBase(atomic, BaseNumber);

            var formatted = GenerateAscii(converted);

            return $"{Specifier}{formatted}{Specifier}";
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            var value = GenerateHex(TrimSingle(input, SpecifierChar));

            return ToAtomic(value, BitsPerByte, BaseNumber);
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

    private class DateTimeRadix : Radix
    {
        private const string Separator = "_";
        private const string Suffix = "Z";
        private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";
        private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;

        public DateTimeRadix() : base(nameof(DateTime), "Date/Time")
        {
        }

        protected override string Specifier => "DT#";

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            var timestamp = (long)(LINT)atomic;

            var milliseconds = timestamp / 1000;
            var microseconds = timestamp % 1000;
            var ticks = microseconds * TicksPerMicrosecond;

            var time = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks);

            var formatted = time.ToString("yyyy-MM-dd-HH:mm:ss.ffffff");

            var str = Regex.Replace(formatted, InsertPattern, Separator);

            return $"{Specifier}{str}{Suffix}";
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty)
                .Replace(Separator, string.Empty)
                .Replace(Suffix, string.Empty);

            var time = System.DateTime.ParseExact(value, "yyyy-MM-dd-HH:mm:ss.ffffff",
                CultureInfo.InvariantCulture);

            var timestamp = (time.Ticks - System.DateTime.UnixEpoch.Ticks) / TicksPerMicrosecond;

            return new LINT(timestamp);
        }
    }

    private class DateTimeNsRadix : Radix
    {
        private const string Separator = "_";
        private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";

        public DateTimeNsRadix() : base(nameof(DateTimeNs), "Date/Time (ns)")
        {
        }

        protected override string Specifier => "LDT#";

        public override string Format(AtomicType atomic)
        {
            ValidateType(atomic);

            var timestamp = (long)(LINT)atomic;

            var milliseconds = timestamp / 1000000;
            var microseconds = timestamp % 1000000;
            var ticks = microseconds / 100;

            var localTime = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks).ToLocalTime();

            var formatted = localTime.ToString("yyyy-MM-dd-HH:mm:ss.fffffff00(UTCzzz)");

            var str = Regex.Replace(formatted, InsertPattern, Separator);

            return $"{Specifier}{str}";
        }

        public override AtomicType Parse(string input)
        {
            ValidateFormat(input);

            var value = input.Replace(Specifier, string.Empty).Replace(Separator, string.Empty);

            var time = System.DateTime.ParseExact(value, "yyyy-MM-dd-HH:mm:ss.fffffff00(UTCzzz)",
                CultureInfo.InvariantCulture).ToUniversalTime();

            var timestamp = (time.Ticks - System.DateTime.UnixEpoch.Ticks) * 100;

            return new LINT(timestamp);
        }
    }
}