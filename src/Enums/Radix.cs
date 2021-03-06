using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ardalis.SmartEnum;
using L5Sharp.Extensions;
using L5Sharp.Types;

// ReSharper disable StringLiteralTypo

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents a number base for a given value type or atomic type.
    /// </summary>
    public abstract class Radix : SmartEnum<Radix, string>
    {
        private static readonly Dictionary<string, Func<string, bool>> Identifiers = new()
        {
            { nameof(Binary), s => s.HasBinaryFormat() },
            { nameof(Octal), s => s.HasOctalFormat() },
            { nameof(Decimal), s => s.HasDecimalFormat() },
            { nameof(Hex), s => s.HasHexFormat() },
            { nameof(Float), s => s.HasFloatFormat() },
            { nameof(Exponential), s => s.HasExponentialFormat() },
            { nameof(Ascii), s => s.HasAsciiFormat() },
            { nameof(DateTime), s => s.HasDateTimeFormat() },
            { nameof(DateTimeNs), s => s.HasDateTimeNsFormat() }
        };

        private Radix(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a Null radix, or absence of a Radix value.
        /// </summary>
        /// <remarks>
        /// Only <see cref="IAtomicType"/> types have non-null Radix. <see cref="IComplexType"/> types all have null Radix.
        /// </remarks>
        public static readonly Radix Null = new NullRadix();

        /// <summary>
        /// Represents a Binary number base format.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Binary Radix format starts with the specifier string '2#'.
        /// Each byte is separated by a '_' character.
        /// The string value is padded based on the size of the data type.
        /// </para>
        /// <para>
        /// Valid Types: <see cref="BOOL"/>, <see cref="SINT"/>, <see cref="INT"/>, <see cref="DINT"/>, <see cref="LINT"/>.
        /// </para> 
        /// </remarks>
        /// <example>
        /// Int with value 5 would be '2#0000_0101'.
        /// </example>
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
        /// Gets the default <see cref="Radix"/> type for the provided data type instance.
        /// </summary>
        /// <param name="dataType">The data type instance to evaluate.</param>
        /// <returns>
        /// <see cref="Null"/> for all non atomic types.
        /// <see cref="Float"/> for <see cref="REAL"/> types.
        /// <see cref="Decimal"/> for all other atomic types.
        /// </returns>
        public static Radix Default(IDataType dataType)
        {
            if (dataType is IArrayType<IDataType> arrayType)
                dataType = arrayType.First().DataType;

            if (dataType is not IAtomicType atomicType)
                return Null;

            return atomicType is REAL ? Float : Decimal;
        }

        /// <summary>
        /// Determines if the current <see cref="Radix"/> supports the provided data type instance.
        /// </summary>
        /// <param name="dataType">The data type instance to evaluate.</param>
        /// <returns>true if the current radix value is valid for the given data type instance; otherwise, false.</returns>
        public bool SupportsType(IDataType dataType)
        {
            if (dataType is IArrayType<IDataType> arrayType)
                dataType = arrayType.First().DataType;

            if (dataType is not IAtomicType atomicType)
                return Equals(Null);

            return atomicType switch
            {
                BOOL => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex),
                LINT => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii) ||
                        Equals(DateTime) || Equals(DateTimeNs),
                REAL => Equals(Float) || Equals(Exponential),
                _ => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii)
            };
        }

        /// <summary>
        /// Parses a string input to an object value based on the format of the input value.
        /// </summary>
        /// <param name="input">The string value to parse.</param>
        /// <returns>
        /// An object representing the value of the parsed string input.
        /// If no radix format can be determined from the input, returns the input string.
        /// </returns>
        /// <remarks>
        /// This method determines the radix based on patterns in the input string. For example, if the string input
        /// starts with the specifier '2#', this method will forward the call to <see cref="Parse"/> for the Binary Radix
        /// and return the result. If no radix can be determined from the input string, the call is forwarded to the
        /// <see cref="Null"/> radix, which which will throw a <see cref="NotSupportedException"/>.
        /// </remarks>
        public static IAtomicType ParseValue(string input)
        {
            var parser = DetermineParser(input);

            if (parser is null)
                throw new FormatException(
                    $"Could not determine Radix from input '{input}'. " +
                    "Verify that the input string is an accepted Radix format");

            return parser(input);
        }

        /// <summary>
        /// Parsed a string input and returns the value as an <see cref="IAtomicType"/> value type.
        /// </summary>
        /// <remarks>
        /// This method is similar to <see cref="ParseValue"/>, except it will return the parsed input as the
        /// atomic value type that is specified by the generic parameter.
        /// </remarks>
        /// <param name="input">The string value to parse.</param>
        /// <typeparam name="TAtomic">The <see cref="IAtomicType"/> type to return.</typeparam>
        /// <returns>
        /// An IAtomic value type instance representing the value of the parsed string input.
        /// </returns>
        public static TAtomic ParseValue<TAtomic>(string input) where TAtomic : IAtomicType
        {
            var parser = DetermineParser(input);

            if (parser is null)
                throw new ArgumentException(
                    $"Could not determine Radix from input '{input}'. Verify that the input string is an accepted Radix format");

            var value = parser(input);

            var converter = TypeDescriptor.GetConverter(typeof(TAtomic));

            return (TAtomic)converter.ConvertFrom(value)!;
        }

        /// <summary>
        /// Attempts to parse the provided input string into a <see cref="IAtomicType{T}"/> value representation.
        /// </summary>
        /// <param name="input">The string input to parse.</param>
        /// <returns>If the parse was successful, then a <see cref="IAtomicType{T}"/> with the value of the parsed input;
        /// otherwise, null.</returns>
        public static IAtomicType? TryParseValue(string input)
        {
            var parser = DetermineParser(input);
            return parser?.Invoke(input);
        }

        /// <summary>
        /// Converts an atomic value to the current radix base value. 
        /// </summary>
        /// <param name="atomic">The current atomic type to convert.</param>
        /// <returns>
        /// A string that represents the value of the atomic type in the current radix base number style.
        /// </returns>
        public abstract string Format(IAtomicType atomic);
        
        
        /*public virtual string Format(string format, object arg, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// Parses a string input of a given Radix formatted value into an object value. 
        /// </summary>
        /// <param name="input">The string value to parse.</param>
        /// <returns>An object representing the value of the formatted string.</returns>
        public abstract IAtomicType Parse(string input);

        private static IAtomicType ConvertToAtomic(string value, int bitsPerByte, int baseNumber)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("The proided value can not be null or empty");

            var byteLength = value.Length / bitsPerByte;

            return byteLength switch
            {
                0 => new BOOL(value == "1"),
                > 0 and <= 1 => new SINT(Convert.ToSByte(value, baseNumber)),
                > 1 and <= 2 => new INT(Convert.ToInt16(value, baseNumber)),
                > 2 and <= 4 => new DINT(Convert.ToInt32(value, baseNumber)),
                > 4 and <= 8 => new LINT(Convert.ToInt64(value, baseNumber)),
                _ => throw new ArgumentOutOfRangeException(nameof(byteLength),
                    $"The provided value byte length '{byteLength}' is out of range for atomic conversion. " +
                    "Must be between 0 and 8 bytes.")
            };
        }

        /// <summary>
        /// Gets a parse function based on the provided string input.
        /// </summary>
        /// <param name="value">The string input value to determine a parse function for.</param>
        /// <returns>
        /// A func delegate that represents the parse function for the given string input.
        /// </returns>
        private static Func<string, IAtomicType>? DetermineParser(string value)
        {
            var name = Identifiers.FirstOrDefault(i => i.Value.Invoke(value)).Key;

            return name is not null ? input => FromName(name).Parse(input) : null;
        }

        private void ValidateFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            if (!Identifiers[Name].Invoke(input))
                throw new FormatException($"Input '{input}' does not have expected {Name} format.");
        }

        private void ValidateType(IAtomicType atomic)
        {
            if (atomic is null)
                throw new ArgumentNullException(nameof(atomic));

            if (!SupportsType(atomic))
                throw new NotSupportedException($"{atomic.GetType()} is not supported by {Name} Radix.");
        }

        private class NullRadix : Radix
        {
            public NullRadix() : base("NullType", "NullType")
            {
            }

            public override string Format(IAtomicType atomic) =>
                throw new NotSupportedException($"{Name} Radix does not support formatting atomic values");

            public override IAtomicType Parse(string input) =>
                throw new NotSupportedException($"{Name} Radix does not support parsing atomic values");
        }

        private class BinaryRadix : Radix
        {
            private const int BaseNumber = 2;
            private const int CharsPerByte = 8;
            private const string Specifier = "2#";
            private const string ByteSeparator = "_";
            private const string Pattern = @"(?<=\d)(?=(\d\d\d\d)+(?!\d))";

            public BinaryRadix() : base(nameof(Binary), nameof(Binary))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var value = (ValueType)atomic.Value;

                var converted = value is not bool b ? value.ToBase(BaseNumber) : b ? "1" : "0";

                var formatted = Regex.Replace(converted, Pattern, ByteSeparator, RegexOptions.Compiled);

                return $"{Specifier}{formatted}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return ConvertToAtomic(value, CharsPerByte, BaseNumber);
            }
        }

        private class OctalRadix : Radix
        {
            private const int BaseNumber = 8;
            private const int CharsPerByte = 3;
            private const string Specifier = "8#";
            private const string ByteSeparator = "_";
            private const string Pattern = @"(?<=\d)(?=(\d\d\d)+(?!\d))";

            public OctalRadix() : base(nameof(Octal), nameof(Octal))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var value = (ValueType)atomic.Value;

                var converted = value is not bool b ? value.ToBase(BaseNumber) : b ? "1" : "0";

                var formatted = Regex.Replace(converted, Pattern, ByteSeparator, RegexOptions.Compiled);

                return $"{Specifier}{formatted}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return ConvertToAtomic(value, CharsPerByte, BaseNumber);
            }
        }

        private class DecimalRadix : Radix
        {
            public DecimalRadix() : base(nameof(Decimal), nameof(Decimal))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                if (atomic is BOOL b)
                    return b ? "1" : "0";

                return atomic.Value.ToString();
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                if (sbyte.TryParse(input, out var sbyteValue))
                    return new SINT(sbyteValue);

                if (byte.TryParse(input, out var byteValue))
                    return new USINT(byteValue);

                if (short.TryParse(input, out var shortValue))
                    return new INT(shortValue);

                if (ushort.TryParse(input, out var ushortValue))
                    return new UINT(ushortValue);

                if (int.TryParse(input, out var intValue))
                    return new DINT(intValue);

                if (uint.TryParse(input, out var uintValue))
                    return new UDINT(uintValue);

                if (long.TryParse(input, out var longValue))
                    return new LINT(longValue);

                if (ulong.TryParse(input, out var ulongValue))
                    return new ULINT(ulongValue);

                throw new ArgumentOutOfRangeException(nameof(input),
                    $"Input '{input}' is out of range for the {Name} Radix.");
            }
        }

        private class HexRadix : Radix
        {
            private const int BaseNumber = 16;
            private const int BitsPerByte = 2;
            private const string Specifier = "16#";
            private const string ByteSeparator = "_";
            private const string Pattern = @"(?<=\w)(?=(\w\w\w\w)+(?!\w))";

            public HexRadix() : base(nameof(Hex), nameof(Hex))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var value = (ValueType)atomic.Value;

                var converted = value is not bool b ? value.ToBase(BaseNumber) : b ? "1" : "0";

                var formatted = Regex.Replace(converted, Pattern, ByteSeparator, RegexOptions.Compiled);

                return $"{Specifier}{formatted}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return ConvertToAtomic(value, BitsPerByte, BaseNumber);
            }
        }

        private class FloatRadix : Radix
        {
            public FloatRadix() : base(nameof(Float), nameof(Float))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var value = (float)atomic.Value;

                return value.ToString("0.0######", CultureInfo.InvariantCulture);
            }

            public override IAtomicType Parse(string input)
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

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var value = (float)atomic.Value;

                return value.ToString("e8", CultureInfo.InvariantCulture);
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                return new REAL(float.Parse(input));
            }
        }

        private class AsciiRadix : Radix
        {
            private const int BaseNumber = 16;
            private const int BitsPerByte = 2;
            private const char Specifier = '\'';
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

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var value = (ValueType)atomic.Value;

                var converted = value.ToBase(BaseNumber);

                var formatted = GenerateAscii(converted);

                return $"{Specifier}{formatted}{Specifier}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = GenerateHex(input.TrimSingle(Specifier));

                return ConvertToAtomic(value, BitsPerByte, BaseNumber);
            }

            private static string GenerateAscii(string str)
            {
                var builder = new StringBuilder();

                var segments = str.Segment(BitsPerByte);

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
        }

        private class DateTimeRadix : Radix
        {
            private const string Specifier = "DT#";
            private const string Separator = "_";
            private const string Suffix = "Z";
            private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";
            private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;

            public DateTimeRadix() : base(nameof(DateTime), "Date/Time")
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var timestamp = (long)atomic.Value;

                var milliseconds = timestamp / 1000;
                var microseconds = timestamp % 1000;
                var ticks = microseconds * TicksPerMicrosecond;

                var time = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks);

                var formatted = time.ToString("yyyy-MM-dd-HH:mm:ss.ffffff");

                var str = Regex.Replace(formatted, InsertPattern, Separator, RegexOptions.Compiled);

                return $"{Specifier}{str}{Suffix}";
            }

            public override IAtomicType Parse(string input)
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
            private const string Specifier = "LDT#";
            private const string Separator = "_";
            private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";

            public DateTimeNsRadix() : base(nameof(DateTimeNs), "Date/Time (ns)")
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var timestamp = (long)atomic.Value;

                var milliseconds = timestamp / 1000000;
                var microseconds = timestamp % 1000000;
                var ticks = microseconds / 100;

                var localTime = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks).ToLocalTime();

                var formatted = localTime.ToString("yyyy-MM-dd-HH:mm:ss.fffffff00(UTCzzz)");

                var str = Regex.Replace(formatted, InsertPattern, Separator, RegexOptions.Compiled);

                return $"{Specifier}{str}";
            }

            public override IAtomicType Parse(string input)
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
}