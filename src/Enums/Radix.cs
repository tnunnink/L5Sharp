using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
        /// Valid Types: <see cref="Bool"/>, <see cref="Sint"/>, <see cref="Int"/>, <see cref="Dint"/>, <see cref="Lint"/>.
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
        /// <see cref="Float"/> for <see cref="Real"/> types.
        /// <see cref="Decimal"/> for all other atomic types.
        /// </returns>
        public static Radix Default(IDataType dataType)
        {
            if (dataType is IArrayType<IDataType> arrayType)
                dataType = arrayType.First().DataType;

            if (dataType is not IAtomicType atomicType)
                return Null;

            return atomicType is Real ? Float : Decimal;
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
                Bool => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex),
                Lint => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii) ||
                        Equals(DateTime) || Equals(DateTimeNs),
                Real => Equals(Float) || Equals(Exponential),
                _ => Equals(Binary) || Equals(Octal) || Equals(Decimal) || Equals(Hex) || Equals(Ascii)
            };
        }

        /// <summary>
        /// Parses a string input to an object value based on the format of the input value.
        /// </summary>
        /// <remarks>
        /// This method determines the radix based on patterns in the input string. For example, if the string input
        /// starts with the specifier '2#', this method will forward the call to <see cref="Parse"/> for the Binary Radix
        /// and return the result. If no radix can be determined from the input string, the call is forwarded to the
        /// <see cref="Null"/> radix, which simply returns the input string.
        /// </remarks>
        /// <param name="input">The string value to parse.</param>
        /// <returns>
        /// An object representing the value of the parsed string input.
        /// If no radix format can be determined from the input, returns the input string.
        /// </returns>
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
                    $"Could not determine Radix from input '{input}'. " +
                    "Verify that the input string is an accepted Radix format");

            var value = parser(input);

            var converter = TypeDescriptor.GetConverter(typeof(TAtomic));

            return (TAtomic)converter.ConvertFrom(value)!;
        }

        /// <summary>
        /// Attempts to parse the provided string input into an <see cref="IAtomicType"/> value.
        /// </summary>
        /// <param name="input">The string input to parse.</param>
        /// <param name="result">When the method returns, contains the value of the parse string if the parse was
        /// successful; otherwise, null.</param>
        /// <returns>true if the string input was successfully parsed; otherwise, false.</returns>
        public static bool TryParseValue(string input, out IAtomicType? result)
        {
            var parser = DetermineParser(input);

            if (parser is null)
            {
                result = null;
                return false;
            }

            try
            {
                var value = parser(input);
                result = value;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Converts an atomic value to the current radix base value. 
        /// </summary>
        /// <param name="atomic">The current atomic type to convert.</param>
        /// <returns>
        /// A string that represents the value of the atomic type in the current radix base number style.
        /// </returns>
        public abstract string Format(IAtomicType atomic);

        /// <summary>
        /// Parses a string input of a given Radix formatted value into an object value. 
        /// </summary>
        /// <param name="input">The string value to parse.</param>
        /// <returns>An object representing the value of the formatted string.</returns>
        public abstract IAtomicType Parse(string input);

        /// <summary>
        /// Converts the atomic value into the specified base number type.
        /// </summary>
        /// <param name="atomic">The atomic type to convert.</param>
        /// <param name="baseNumber">The base number to convert to.</param>
        /// <returns>
        /// A string representing the value of the atomic in the specified base number.
        /// If not convertable, returns an empty string.
        /// </returns>
        private static string ChangeBase(IAtomicType atomic, int baseNumber)
        {
            return atomic switch
            {
                Bool b => b ? "1" : "0",
                Sint s => Convert.ToString(s.Value, baseNumber),
                Int i => Convert.ToString(i.Value, baseNumber),
                Dint d => Convert.ToString(d.Value, baseNumber),
                Lint l => Convert.ToString(l.Value, baseNumber),
                _ => throw new ArgumentOutOfRangeException(nameof(atomic), atomic, null)
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

        private void ValidateType(IAtomicType atomic)
        {
            if (atomic == null)
                throw new ArgumentNullException(nameof(atomic));

            if (!SupportsType(atomic))
                throw new NotSupportedException($"{atomic.GetType()} is not supported by {Name} Radix.");
        }

        private void ValidateFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            if (!Identifiers[Name].Invoke(input))
                throw new FormatException($"Input '{input}' does not have expected {Name} format.");
        }

        private class NullRadix : Radix
        {
            public NullRadix() : base("NullType", "NullType")
            {
            }

            public override string Format(IAtomicType atomic) =>
                throw new NotSupportedException($"{Name} does not support formatting atomic values");

            public override IAtomicType Parse(string input) =>
                throw new NotSupportedException($"{Name} does not support parsing atomic values");
        }

        private class BinaryRadix : Radix
        {
            private const int BaseNumber = 2;
            private const string ByteSeparator = "_";
            private const string Specifier = "2#";
            private const string Pattern = @"(?<=\d)(?=(\d\d\d\d)+(?!\d))";

            public BinaryRadix() : base(nameof(Binary), nameof(Binary))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Bool => str.PadLeft(0, '0'),
                    Sint => str.PadLeft(8, '0'),
                    Int => str.PadLeft(16, '0'),
                    Dint => str.PadLeft(32, '0'),
                    Lint => str.PadLeft(64, '0'),
                    _ => throw new NotSupportedException($"{atomic.GetType()} not supported for {Name} Radix.")
                };

                str = Regex.Replace(str, Pattern, ByteSeparator, RegexOptions.Compiled);

                return $"{Specifier}{str}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return value.Length switch
                {
                    1 => new Bool(value == "1"),
                    > 1 and <= 8 => new Sint(Convert.ToByte(value, BaseNumber)),
                    > 8 and <= 16 => new Int(Convert.ToInt16(value, BaseNumber)),
                    > 16 and <= 32 => new Dint(Convert.ToInt32(value, BaseNumber)),
                    > 32 and <= 64 => new Lint(Convert.ToInt64(value, BaseNumber)),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Binary} Radix.")
                };
            }
        }

        private class OctalRadix : Radix
        {
            private const int BaseNumber = 8;
            private const string ByteSeparator = "_";
            private const string Specifier = "8#";
            private const string Pattern = @"(?<=\d)(?=(\d\d\d)+(?!\d))";

            public OctalRadix() : base(nameof(Octal), nameof(Octal))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Bool => str.PadLeft(0, '0'),
                    Sint => str.PadLeft(3, '0'),
                    Int => str.PadLeft(6, '0'),
                    Dint => str.PadLeft(11, '0'),
                    Lint => str.PadLeft(22, '0'),
                    _ => throw new NotSupportedException($"{atomic.GetType()} not supported for {Name} Radix.")
                };

                str = Regex.Replace(str, Pattern, ByteSeparator, RegexOptions.Compiled);

                return $"{Specifier}{str}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return value.Length switch
                {
                    1 => new Bool(value == "1"),
                    > 1 and <= 3 => new Sint(Convert.ToByte(value, BaseNumber)),
                    > 3 and <= 6 => new Int(Convert.ToInt16(value, BaseNumber)),
                    > 6 and <= 11 => new Dint(Convert.ToInt32(value, BaseNumber)),
                    > 11 and <= 22 => new Lint(Convert.ToInt64(value, BaseNumber)),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Octal} Radix.")
                };
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

                return ChangeBase(atomic, 10);
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                if (byte.TryParse(input, out var byteValue))
                    return new Sint(byteValue);

                if (short.TryParse(input, out var shortValue))
                    return new Int(shortValue);

                if (int.TryParse(input, out var intValue))
                    return new Dint(intValue);

                if (long.TryParse(input, out var longValue))
                    return new Lint(longValue);

                throw new ArgumentException($"Input '{input}' not valid for {Decimal} Radix.");
            }
        }

        private class HexRadix : Radix
        {
            private const int BaseNumber = 16;
            private const string ByteSeparator = "_";
            private const string Specifier = "16#";
            private const string Pattern = @"(?<=\w)(?=(\w\w\w\w)+(?!\w))";

            public HexRadix() : base(nameof(Hex), nameof(Hex))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Bool => str.PadLeft(0, '0'),
                    Sint => str.PadLeft(2, '0'),
                    Int => str.PadLeft(4, '0'),
                    Dint => str.PadLeft(8, '0'),
                    Lint => str.PadLeft(16, '0'),
                    _ => throw new NotSupportedException($"{atomic.GetType()} not supported for {Name} Radix.")
                };

                str = Regex.Replace(str, Pattern, ByteSeparator, RegexOptions.Compiled);

                return $"{Specifier}{str}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return value.Length switch
                {
                    1 => new Bool(value == "1"),
                    > 1 and <= 2 => new Sint(Convert.ToByte(value, BaseNumber)),
                    > 2 and <= 4 => new Int(Convert.ToInt16(value, BaseNumber)),
                    > 4 and <= 8 => new Dint(Convert.ToInt32(value, BaseNumber)),
                    > 8 and <= 16 => new Lint(Convert.ToInt64(value, BaseNumber)),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Name} Radix.")
                };
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

                if (!float.TryParse(input, out var result))
                    throw new ArgumentException($"Input '{input}' not valid for {Name} Radix.");

                return new Real(result);
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

                if (!float.TryParse(input, out var result))
                    throw new ArgumentException($"Input '{input}' not valid for {Name} Radix.");

                return new Real(result);
            }
        }

        private class AsciiRadix : Radix
        {
            private const int BaseNumber = 16;
            private const string ByteSeparator = "$";

            public AsciiRadix() : base(nameof(Ascii), nameof(Ascii).ToUpper())
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Sint _ => str.PadLeft(2, '0'),
                    Int _ => str.PadLeft(4, '0'),
                    Dint _ => str.PadLeft(8, '0'),
                    Lint _ => str.PadLeft(16, '0'),
                    _ => throw new NotSupportedException($"{atomic.GetType()} not supported for {Ascii} Radix.")
                };

                return Regex.Replace(str, @"(?=(\w\w)+(?!\w))", ByteSeparator);
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(ByteSeparator, string.Empty);

                return value.Length switch
                {
                    2 => new Sint(Convert.ToByte(value, BaseNumber)),
                    4 => new Int(Convert.ToInt16(value, BaseNumber)),
                    8 => new Dint(Convert.ToInt32(value, BaseNumber)),
                    16 => new Lint(Convert.ToInt64(value, BaseNumber)),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Name} Radix.")
                };
            }
        }

        private class DateTimeRadix : Radix
        {
            private const string Specifier = "DT#";
            private const string Separator = "_";
            private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";
            private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;

            public DateTimeRadix() : base(nameof(DateTime), nameof(DateTime))
            {
            }

            public override string Format(IAtomicType atomic)
            {
                ValidateType(atomic);

                var timestamp = (long)atomic.Value;

                var milliseconds = timestamp / 1000;
                var microseconds = timestamp % 1000;
                var ticks = microseconds * TicksPerMicrosecond;

                var localTime = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).AddTicks(ticks).ToLocalTime();

                var formatted = localTime.ToString("yyyy-MM-dd-HH:mm:ss.ffffff(UTCzzz)");

                var str = Regex.Replace(formatted, InsertPattern, Separator, RegexOptions.Compiled);

                return $"{Specifier}{str}";
            }

            public override IAtomicType Parse(string input)
            {
                ValidateFormat(input);

                var value = input.Replace(Specifier, string.Empty).Replace(Separator, string.Empty);

                var time = System.DateTime.ParseExact(value, "yyyy-MM-dd-HH:mm:ss.ffffff(UTCzzz)",
                    CultureInfo.InvariantCulture).ToUniversalTime();

                var timestamp = (time.Ticks - System.DateTime.UnixEpoch.Ticks) / TicksPerMicrosecond;

                return new Lint(timestamp);
            }
        }


        private class DateTimeNsRadix : Radix
        {
            private const string Specifier = "LDT#";
            private const string Separator = "_";
            private const string InsertPattern = @"(?<=\d\d\d)(?=(\d\d\d)+(?!\d))";

            public DateTimeNsRadix() : base(nameof(DateTimeNs), nameof(DateTimeNs))
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

                return new Lint(timestamp);
            }
        }
    }
}