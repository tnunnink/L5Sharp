using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using L5Sharp.Atomics;

namespace L5Sharp.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Determines if the current string is equal to string.Empty.
        /// </summary>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>true if the string is empty. Otherwise false.</returns>
        public static bool IsEmpty(this string value) => value.Equals(string.Empty);

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.Binary"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>
        /// true if the string starts with the binary specifier '2#'. Otherwise, false.
        /// </returns>
        public static bool HasBinaryFormat(this string value)
        {
            return !value.IsEmpty() && value.StartsWith("2#");
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.Octal"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>
        /// true if the string starts with the binary specifier '8#'. Otherwise, false.
        /// </returns>
        public static bool HasOctalFormat(this string value)
        {
            return !value.IsEmpty() && value.StartsWith("8#");
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.Decimal"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>true if the string is all numbers. Leading '+' and '-' are removed. otherwise, false.</returns>
        public static bool HasDecimalFormat(this string value)
        {
            if (value.StartsWith("+") || value.StartsWith("-"))
            {
                value = value.Remove(0, 1);
            }

            return !value.IsEmpty() && value.All(char.IsDigit);
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.Hex"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>
        /// true if the string starts with the binary specifier '8#'. Otherwise, false.
        /// </returns>
        public static bool HasHexFormat(this string value)
        {
            return !value.IsEmpty() && value.StartsWith("16#");
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.Float"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>true if the string has expected format. Leading '+' and '-' are removed. otherwise, false.</returns>
        public static bool HasFloatFormat(this string value)
        {
            //we don't care if it is positive or negative
            if (value.StartsWith("+") || value.StartsWith("-"))
            {
                value = value.Remove(0, 1);
            }

            return !value.IsEmpty() && value.Contains('.') && value.Replace(".", string.Empty).All(char.IsDigit);
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.Exponential"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>true if the string has expected format. Leading '+' and '-' are removed. otherwise, false.</returns>
        public static bool HasExponentialFormat(this string value)
        {
            //we don't care if it is positive or negative, so remove it.
            if (value.StartsWith("+") || value.StartsWith("-"))
            {
                value = value.Remove(0, 1);
            }

            return !value.IsEmpty() && value.Contains(".")
                                    && value.Contains("e", StringComparison.OrdinalIgnoreCase)
                                    && value.ReplaceAll(new[] { ".", "e", "E", "+", "-" }, string.Empty)
                                        .All(char.IsDigit);
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.Ascii"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>
        /// true if the string starts with the binary specifier '8#'. Otherwise, false.
        /// </returns>
        public static bool HasAsciiFormat(this string value)
        {
            return !value.IsEmpty() && value.StartsWith("'") && value.EndsWith("'");
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.DateTime"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>
        /// true if the string starts with the binary specifier '8#'. Otherwise, false.
        /// </returns>
        public static bool HasDateTimeFormat(this string value)
        {
            return !value.IsEmpty() && value.StartsWith("DT#");
        }

        /// <summary>
        /// Determines if the current string has a <see cref="Enums.Radix.DateTimeNs"/> format.
        /// </summary>
        /// <remarks>
        /// This is primarily a helper for determining Logix Radix number formats.
        /// This method is not intended to parse strings to actual .NET types.
        /// </remarks>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>
        /// true if the string starts with the binary specifier '8#'. Otherwise, false.
        /// </returns>
        public static bool HasDateTimeNsFormat(this string value)
        {
            return !value.IsEmpty() && value.StartsWith("LDT#");
        }

        /// <summary>
        /// Determines if the provided input string is a valid slot number.
        /// </summary>
        /// <param name="slotString">The slot number string to analyze.</param>
        /// <returns>true if the provided string is a valid slot number, meaning that it is parseable to a byte;
        /// otherwise, false.</returns>
        public static bool IsByte(this string slotString)
        {
            return byte.TryParse(slotString, out _);
        }

        /// <summary>
        /// Determines if the provided input string is a IPv4 address.
        /// </summary>
        /// <param name="ipString">The string IP address to analyze.</param>
        /// <returns>true if the provided string is a valid IPv4 address, meaning that it has 4 '.' characters and is
        /// is parseable to a <see cref="IsIPv4"/>;
        /// otherwise, false.</returns>
        public static bool IsIPv4(this string ipString)
        {
            return ipString.Count(c => c == '.') == 3 && IPAddress.TryParse(ipString, out _);
        }

        /// <summary>
        /// Replaces all specified string values with a single replacement string value in the current string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="items"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceAll(this string value, IEnumerable<string> items, string replacement)
        {
            return items.Aggregate(value, (str, cItem) => str.Replace(cItem, replacement));
        }
        
        public static IEnumerable<string> Segment(this string input, int length)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0");

            for (var i = 0; i < input.Length; i += length)
                yield return input.Substring(i, i + length < input.Length ? length : input.Length - i);
        }

        public static bool IsBalanced(this string value, char opening, char closing)
        {
            var characters = new Stack<char>();

            foreach (var c in value)
            {
                if (Equals(c, opening))
                    characters.Push(c);

                if (!Equals(c, closing)) continue;

                if (!characters.TryPop(out _))
                    return false;
            }

            return characters.Count == 0;
        }

        /// <summary>
        /// Removes the first and last occurrence of the specified string if they exist.
        /// </summary>
        /// <param name="value">The string value to trim.</param>
        /// <param name="character">the character to remove.</param>
        /// <returns>A new string value with the specified character removed from the beginning and end.</returns>
        public static string TrimSingle(this string value, char character)
        {
            if (value.StartsWith(character) && value.EndsWith(character))
                return value.Substring(1, value.Length - 2);

            if (value.StartsWith(character))
                return value.Substring(1, value.Length - 1);
            
            return value.EndsWith(character) ? value[..^2] : value;
        }

        public static IEnumerable<string> GetWords(this string input)
        {
            var matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = matches.Where(m => !string.IsNullOrEmpty(m.Value)).Select(m => TrimSuffix(m.Value));

            return words;
        }
        
        private static string TrimSuffix(this string word)
        {
            var apostropheLocation = word.IndexOf('\'');
            
            if (apostropheLocation != -1)
                word = word[..apostropheLocation];

            return word;
        }
    }
}