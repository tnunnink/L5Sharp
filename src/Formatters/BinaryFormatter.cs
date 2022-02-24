using System;
using System.Collections.Generic;
using System.Globalization;
using L5Sharp.Extensions;

namespace L5Sharp.Formatters
{
    /// <summary>
    /// 
    /// </summary>
    public class BinaryFormatter : IFormatProvider, ICustomFormatter
    {
        private static readonly Dictionary<string, int> Formats = new()
        {
            { "B", 2 },
            { "O", 8 },
            { "H", 16 },
            { "A", 16 }
        };

        /// <inheritdoc />
        public object? GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        /// <inheritdoc />
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            Formats.TryGetValue(format, out var baseNumber);

            if (baseNumber == 0)
            {
                try {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e) {
                    throw new FormatException($"The format of '{format}' is invalid.", e);
                }
            }

            if (arg is not ValueType valueType)
                throw new ArgumentException();

            var bytes = valueType.GetBytes();

            var numericString = string.Empty;

            for (var ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
            {
                var byteString = Convert.ToString(bytes[ctr], baseNumber);
                
                if (baseNumber == 2)
                    byteString = new string('0', 8 - byteString.Length) + byteString;
                else if (baseNumber == 8)
                    byteString = new String('0', 4 - byteString.Length) + byteString;
                // Base is 16.
                else
                    byteString = new String('0', 2 - byteString.Length) + byteString;

                numericString += byteString + " ";
            }

            return numericString.Trim();
        }

        private static string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable formatter)
                return formatter.ToString(format, CultureInfo.CurrentCulture);

            return arg.ToString();
        }
    }
}