using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Ardalis.SmartEnum;
using L5Sharp.Types;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents a number base for a given value type or atomic type.
    /// </summary>
    public abstract class Radix : SmartEnum<Radix, string>
    {
        //todo ideally make these all regex match expressions?
        private static readonly Dictionary<string, Func<string, bool>> Identifiers = new()
        {
            { nameof(Binary), s => s.StartsWith(BinaryRadix.Specifier) },
            { nameof(Octal), s => s.StartsWith(OctalRadix.Specifier) },
            { nameof(Decimal), s => Regex.IsMatch(s, @"^\d+$") },
            { nameof(Hex), s => s.StartsWith(HexRadix.Specifier) },
            { nameof(Float), s => s.Contains(".") },
            { nameof(Exponential), s => s.Contains("e+") },
            { nameof(Ascii), s => s.Contains("$") },
            { nameof(DateTime), s => s.StartsWith("DT#") },
            { nameof(DateTimeNs), s => s.StartsWith("LDT#") }
        };

        private Radix(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a Null radix, or absence of a Radix value.
        /// </summary>
        /// <remarks>
        /// Only <see cref="IAtomic"/> types have non-null Radix. <see cref="IComplexType"/> types all have null Radix.
        /// </remarks>
        public static readonly Radix Null = new NullRadix();

        /// <summary>
        /// Represents a Binary number base format.
        /// </summary>
        /// <remarks>
        /// Valid for types <see cref="Bool"/>, <see cref="Sint"/>, <see cref="Int"/>, <see cref="Dint"/>, <see cref="Lint"/>.
        /// </remarks>
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
        /// Represents a Hex number base format.
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
        /// Gets the default Radix for the provided data type.
        /// </summary>
        /// <param name="dataType">The data type to determine the default radix for.</param>
        /// <returns>
        /// <see cref="Null"/> for all non atomic types.
        /// <see cref="Float"/> for <see cref="Real"/> types.
        /// <see cref="Decimal"/> for all other atomic types.
        /// </returns>
        public static Radix Default(IDataType dataType)
        {
            if (dataType is not IAtomic atomic)
                return Null;

            return atomic is Real ? Float : Decimal;
        }

        /// <summary>
        /// Determines a Radix type based on the provided string formatted value.
        /// </summary>
        /// <param name="value">The string input to infer the Radix type for.</param>
        /// <returns>A Radix representing the type that </returns>
        public static Radix Infer(string value)
        {
            var radix = Identifiers.FirstOrDefault(i => i.Value.Invoke(value)).Key;

            return radix is not null ? FromName(radix) : Null;
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
        public static object ParseValue(string input)
        {
            var parser = DetermineParser(input);
            return parser(input);
        }

        /// <summary>
        /// Parsed a string input and returns the value as an <see cref="IAtomic"/> value type.
        /// </summary>
        /// <remarks>
        /// This method is similar to <see cref="ParseValue"/>, except it will return the parsed input value as the
        /// atomic value type that is specified by the generic parameter.
        /// </remarks>
        /// <param name="input">The string value to parse.</param>
        /// <typeparam name="TAtomic">The <see cref="IAtomic"/> type to return.</typeparam>
        /// <returns>
        /// An IAtomic value type instance representing the value of the parsed string input.
        /// </returns>
        public static TAtomic ParseValue<TAtomic>(string input) where TAtomic : IAtomic, new()
        {
            var parser = DetermineParser(input);

            var value = parser(input);

            var atomic = new TAtomic();

            return (TAtomic)atomic.Update(value);
        }

        /// <summary>
        /// Attempts to call the <see cref="Parse"/> method and return a result if the parse was successful.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <param name="result">The resulting parse object value.</param>
        /// <returns>
        /// true if the parse was successful and didn't throw any exceptions. false if not.
        /// </returns>
        public static bool TryParseValue(string input, out object? result)
        {
            try
            {
                result = ParseValue(input);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Attempts to call the <see cref="Parse"/> method and return a result if the parse was successful.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <param name="result">The resulting parse object value.</param>
        /// <returns>
        /// true if the parse was successful and didn't throw any exceptions. false if not.
        /// </returns>
        public static bool TryParseValue<TAtomic>(string input, out object? result) where TAtomic : IAtomic, new()
        {
            try
            {
                result = ParseValue<TAtomic>(input);
                return true;
            }
            catch (Exception)
            {
                result = default;
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
        public abstract string Convert(IAtomic atomic);

        /// <summary>
        /// Parses a string input of a given Radix formatted value into an object value. 
        /// </summary>
        /// <param name="input">The string value to parse.</param>
        /// <returns>An object representing the value of the formatted string.</returns>
        public abstract object Parse(string input);

        /// <summary>
        /// Converts the atomic value into the specified base number type.
        /// </summary>
        /// <param name="atomic">The atomic type to convert.</param>
        /// <param name="baseNumber">The base number to convert to.</param>
        /// <returns>A string representing the value of the atomic in the specified base number.</returns>
        /// <exception cref="NotSupportedException">Thrown if the atomic type is not supported by the given function.</exception>
        private static string ChangeBase(IAtomic atomic, int baseNumber)
        {
            return atomic switch
            {
                Bool b => b ? "1" : "0",
                Sint s => System.Convert.ToString(s.Value, baseNumber),
                Int i => System.Convert.ToString(i.Value, baseNumber),
                Dint d => System.Convert.ToString(d.Value, baseNumber),
                Lint l => System.Convert.ToString(l.Value, baseNumber),
                _ => throw new NotSupportedException(
                    $"{atomic.GetType()} is not supported by the function {nameof(ChangeBase)}")
            };
        }

        /// <summary>
        /// Gets a parse function based on the provided string input.
        /// </summary>
        /// <param name="value">The string input value to determine a parse function for.</param>
        /// <returns>
        /// A func delegate that represents the parse function for the given string input.
        /// </returns>
        private static Func<string, object> DetermineParser(string value)
        {
            var radix = Identifiers.FirstOrDefault(i => i.Value.Invoke(value)).Key;

            return radix is not null ? FromName(radix).Parse : Null.Parse;
        }

        private class NullRadix : Radix
        {
            public NullRadix() : base("NullType", "NullType")
            {
            }

            public override string Convert(IAtomic atomic)
            {
                return atomic.Value.ToString();
            }

            public override object Parse(string input)
            {
                return input;
            }
        }

        private class BinaryRadix : Radix
        {
            private const int BaseNumber = 2;
            private const string ByteSeparator = "_";
            public const string Specifier = "2#";

            public BinaryRadix() : base(nameof(Binary), nameof(Binary))
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Bool _ => str.PadLeft(0, '0'),
                    Sint _ => str.PadLeft(8, '0'),
                    Int _ => str.PadLeft(16, '0'),
                    Dint _ => str.PadLeft(32, '0'),
                    Lint _ => str.PadLeft(64, '0'),
                    _ => throw new NotSupportedException(
                        $"{atomic.GetType()} not supported by {Binary} Radix.")
                };

                str = Regex.Replace(str, @"(?<=\d)(?=(\d\d\d\d)+(?!\d))", ByteSeparator);

                return $"{Specifier}{str}";
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (!input.StartsWith(Specifier))
                    throw new ArgumentException($"Input must start with {Binary} specifier '{Specifier}'.");

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return value.Length switch
                {
                    1 => value == "1",
                    > 1 and <= 8 => System.Convert.ToByte(value, BaseNumber),
                    > 8 and <= 16 => System.Convert.ToInt16(value, BaseNumber),
                    > 16 and <= 32 => System.Convert.ToInt32(value, BaseNumber),
                    > 32 and <= 64 => System.Convert.ToInt64(value, BaseNumber),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Binary} Radix.")
                };
            }
        }

        private class OctalRadix : Radix
        {
            private const int BaseNumber = 8;
            private const string ByteSeparator = "_";
            public const string Specifier = "8#";

            public OctalRadix() : base(nameof(Octal), nameof(Octal))
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Bool _ => str.PadLeft(0, '0'),
                    Sint _ => str.PadLeft(3, '0'),
                    Int _ => str.PadLeft(6, '0'),
                    Dint _ => str.PadLeft(11, '0'),
                    Lint _ => str.PadLeft(22, '0'),
                    _ => throw new NotSupportedException($"{atomic.GetType()} not supported for {Octal} Radix.")
                };

                str = Regex.Replace(str, @"(?<=\d)(?=(\d\d\d)+(?!\d))", ByteSeparator);

                return $"{Specifier}{str}";
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (!input.StartsWith(Specifier))
                    throw new ArgumentException($"Input must start with {Octal} specifier '{Specifier}'.");

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return value.Length switch
                {
                    1 => value == "1",
                    > 1 and <= 3 => System.Convert.ToByte(value, BaseNumber),
                    > 3 and <= 6 => System.Convert.ToInt16(value, BaseNumber),
                    > 6 and <= 11 => System.Convert.ToInt32(value, BaseNumber),
                    > 11 and <= 22 => System.Convert.ToInt64(value, BaseNumber),
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

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                return ChangeBase(atomic, 10);
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (byte.TryParse(input, out var byteValue))
                    return byteValue;

                if (short.TryParse(input, out var shortValue))
                    return shortValue;

                if (int.TryParse(input, out var intValue))
                    return intValue;

                if (long.TryParse(input, out var longValue))
                    return longValue;

                throw new ArgumentException($"Input value '{input}' not valid for {Decimal} Radix.");
            }
        }

        private class HexRadix : Radix
        {
            private const int BaseNumber = 16;
            private const string ByteSeparator = "_";
            public const string Specifier = "16#";

            public HexRadix() : base(nameof(Hex), nameof(Hex))
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Bool _ => str.PadLeft(0, '0'),
                    Sint _ => str.PadLeft(2, '0'),
                    Int _ => str.PadLeft(4, '0'),
                    Dint _ => str.PadLeft(8, '0'),
                    Lint _ => str.PadLeft(16, '0'),
                    _ => throw new NotSupportedException($"{atomic.GetType()} not supported for {Hex} Radix.")
                };

                str = Regex.Replace(str, @"(?<=\d)(?=(\d\d\d\d)+(?!\d))", ByteSeparator);

                return $"{Specifier}{str}";
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (!input.StartsWith(Specifier))
                    throw new ArgumentException($"Input must start with {Hex} specifier '{Specifier}'.");

                var value = input.Replace(Specifier, string.Empty).Replace(ByteSeparator, string.Empty);

                return value.Length switch
                {
                    1 => value == "1",
                    2 => System.Convert.ToByte(value, BaseNumber),
                    4 => System.Convert.ToInt16(value, BaseNumber),
                    8 => System.Convert.ToInt32(value, BaseNumber),
                    16 => System.Convert.ToInt64(value, BaseNumber),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Hex} Radix.")
                };
            }
        }

        private class ExponentialRadix : Radix
        {
            public ExponentialRadix() : base(nameof(Exponential), nameof(Exponential))
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                var value = (float)atomic.Value;
                return value.ToString("e8", CultureInfo.InvariantCulture);
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                return float.Parse(input);
            }
        }

        private class FloatRadix : Radix
        {
            public FloatRadix() : base(nameof(Float), nameof(Float))
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                var value = (float)atomic.Value;
                return value.ToString("0.0######", CultureInfo.InvariantCulture);
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                return float.Parse(input);
            }
        }

        private class AsciiRadix : Radix
        {
            private const string Separator = "$";
            private const int BaseNumber = 16;

            public AsciiRadix() : base(nameof(Ascii).ToUpper(), nameof(Ascii).ToUpper())
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                var str = ChangeBase(atomic, BaseNumber);

                str = atomic switch
                {
                    Sint _ => str.PadLeft(2, '0'),
                    Int _ => str.PadLeft(4, '0'),
                    Dint _ => str.PadLeft(8, '0'),
                    Lint _ => str.PadLeft(16, '0'),
                    _ => throw new NotSupportedException($"{atomic.GetType()} not supported for {Ascii} Radix.")
                };

                return Regex.Replace(str, @"(?=(\d\d)+(?!\d))", Separator);
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                var value = input.Replace(Separator, string.Empty);

                return value.Length switch
                {
                    2 => System.Convert.ToByte(value, BaseNumber),
                    4 => System.Convert.ToInt16(value, BaseNumber),
                    8 => System.Convert.ToInt32(value, BaseNumber),
                    16 => System.Convert.ToInt64(value, BaseNumber),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Hex} Radix.")
                };
            }
        }

        private class DateTimeRadix : Radix
        {
            private const string Specifier = "DT#";

            public DateTimeRadix() : base(nameof(DateTime), nameof(DateTime))
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                //Calculate local time from provided long value.
                var seconds = (long)atomic.Value / 1000000;
                var dateTime = System.DateTime.UnixEpoch.AddSeconds(seconds).ToLocalTime();
                var offset = System.DateTime.SpecifyKind(dateTime, DateTimeKind.Local);

                // ReSharper disable once StringLiteralTypo
                var formatted = offset.ToString("yyyy-MM-dd-HH:mm:ss.ffffff(UTCzzz)");

                return $"{Specifier}{formatted}";
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (!input.StartsWith(Specifier))
                    throw new ArgumentException($"Input must start with {Hex} specifier '{Specifier}'.");

                var value = input.Replace(Specifier, string.Empty);

                return System.DateTime.ParseExact(value, "yyyy-MM-dd-HH:mm:ss.ffffff(UTCzzz)",
                    CultureInfo.InvariantCulture);
            }
        }

        private class DateTimeNsRadix : Radix
        {
            public DateTimeNsRadix() : base(nameof(DateTimeNs), nameof(DateTimeNs))
            {
            }

            public override string Convert(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                //Calculate local time from provided long value.
                var seconds = (long)atomic.Value / 100;
                var dateTime = System.DateTime.UnixEpoch.AddTicks(seconds).ToLocalTime();
                var offset = System.DateTime.SpecifyKind(dateTime, DateTimeKind.Local);

                // ReSharper disable once StringLiteralTypo (this is on UTC zzz)
                var formatted = offset.ToString("yyyy-MM-dd-HH:mm:ss.fffffff(UTCzzz)");

                return $"LDT#{formatted}";
            }

            public override object Parse(string input)
            {
                throw new NotImplementedException();
            }
        }
    }
}