using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Ardalis.SmartEnum;
using L5Sharp.Exceptions;
using L5Sharp.Types;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents a number base for a given value type or atomic type.
    /// </summary>
    public abstract class Radix : SmartEnum<Radix, string>
    {
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
        /// Formats an atomic value to the string representation of the value. 
        /// </summary>
        /// <param name="atomic">The current atomic type to format.</param>
        /// <returns>A string that represents the value of the atomic formatted based on the rules of the current radix type.</returns>
        public abstract string Format(IAtomic atomic);

        /// <summary>
        /// Parses a string representation of a given Radix formatted value into an object. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract object Parse(string input);

        /// <summary>
        /// Attempts to call the <see cref="Parse"/> method and return a result if the parse was successful.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <param name="result">The resulting parse object value.</param>
        /// <returns>
        /// true if the parse was successful and didn't throw any exceptions. false if not.
        /// </returns>
        public bool TryParse(string input, out object result)
        {
            try
            {
                result = Parse(input);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        private static string ChangeBase(IAtomic atomic, int baseNumber)
        {
            return atomic switch
            {
                Bool b => b ? "1" : "0",
                Sint s => Convert.ToString(s.Value, baseNumber),
                Int i => Convert.ToString(i.Value, baseNumber),
                Dint d => Convert.ToString(d.Value, baseNumber),
                Lint l => Convert.ToString(l.Value, baseNumber),
                _ => throw new NotSupportedException(
                    $"{atomic.GetType()} is not supported by the function {nameof(ChangeBase)}")
            };
        }

        private class NullRadix : Radix
        {
            public NullRadix() : base("NullType", "NullType")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return null;
            }

            public override object Parse(string input)
            {
                return null;
            }
        }

        private class BinaryRadix : Radix
        {
            private const string Separator = "_";
            private const string Specifier = "2#";
            private const int BaseNumber = 2;

            public BinaryRadix() : base(nameof(Binary), nameof(Binary))
            {
            }

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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

                str = Regex.Replace(str, @"(?<=\d)(?=(\d\d\d\d)+(?!\d))", Separator);

                return $"{Specifier}{str}";
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (!input.StartsWith(Specifier))
                    throw new ArgumentException($"Input must start with {Binary} specifier '{Specifier}'.");

                var value = input.Replace(Specifier, string.Empty).Replace(Separator, string.Empty);

                return value.Length switch
                {
                    1 => value == "1",
                    var n when n > 1 && n <= 8 => Convert.ToByte(value, BaseNumber),
                    var n when n > 8 && n <= 16 => Convert.ToInt16(value, BaseNumber),
                    var n when n > 16 && n <= 32 => Convert.ToInt32(value, BaseNumber),
                    var n when n > 32 && n <= 64 => Convert.ToInt64(value, BaseNumber),
                    _ => throw new ArgumentOutOfRangeException(nameof(value.Length),
                        $"The value {value.Length} is out of range for {Binary} Radix.")
                };
            }
        }

        private class OctalRadix : Radix
        {
            private const string Separator = "_";
            private const string Specifier = "8#";
            private const int BaseNumber = 8;

            public OctalRadix() : base(nameof(Octal), nameof(Octal))
            {
            }

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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

                str = Regex.Replace(str, @"(?<=\d)(?=(\d\d\d)+(?!\d))", Separator);

                return $"{Specifier}{str}";
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (!input.StartsWith(Specifier))
                    throw new ArgumentException($"Input must start with {Octal} specifier '{Specifier}'.");

                var value = input.Replace(Specifier, string.Empty).Replace(Separator, string.Empty);

                return value.Length switch
                {
                    1 => value == "1",
                    var n when n > 1 && n <= 3 => Convert.ToByte(value, BaseNumber),
                    var n when n > 3 && n <= 6 => Convert.ToInt16(value, BaseNumber),
                    var n when n > 6 && n <= 11 => Convert.ToInt32(value, BaseNumber),
                    var n when n > 11 && n <= 22 => Convert.ToInt64(value, BaseNumber),
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

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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
            private const string Separator = "_";
            private const string Specifier = "16#";
            private const int BaseNumber = 16;

            public HexRadix() : base(nameof(Hex), nameof(Hex))
            {
            }

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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

                str = Regex.Replace(str, @"(?<=\d)(?=(\d\d\d\d)+(?!\d))", Separator);

                return $"{Specifier}{str}";
            }

            public override object Parse(string input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentNullException(nameof(input));

                if (!input.StartsWith(Specifier))
                    throw new ArgumentException($"Input must start with {Hex} specifier '{Specifier}'.");

                var value = input.Replace(Specifier, string.Empty).Replace(Separator, string.Empty);

                return value.Length switch
                {
                    1 => value == "1",
                    2 => Convert.ToByte(value, BaseNumber),
                    4 => Convert.ToInt16(value, BaseNumber),
                    8 => Convert.ToInt32(value, BaseNumber),
                    16 => Convert.ToInt64(value, BaseNumber),
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

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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
                    2 => Convert.ToByte(value, BaseNumber),
                    4 => Convert.ToInt16(value, BaseNumber),
                    8 => Convert.ToInt32(value, BaseNumber),
                    16 => Convert.ToInt64(value, BaseNumber),
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

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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

            public override string Format(IAtomic atomic)
            {
                if (atomic == null)
                    throw new ArgumentNullException(nameof(atomic));

                if (!atomic.SupportsRadix(this))
                    throw new RadixNotSupportedException(this, atomic);

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