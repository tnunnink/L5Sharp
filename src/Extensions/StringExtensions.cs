using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of string extensions methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if the current string is equal to string.Empty.
        /// </summary>
        /// <param name="value">The string input to analyze.</param>
        /// <returns>true if the string is empty. Otherwise false.</returns>
        public static bool IsEmpty(this string value) => value.Equals(string.Empty);

        /// <summary>
        /// Tests the current string to indicate whether it is a valid Logix component name value. 
        /// </summary>
        /// <param name="name">The string name to test.</param>
        /// <returns><c>true</c> if <c>name</c> passes the Logix component name requirements; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// Valid name must contain only alphanumeric or underscores, start with a letter or underscore,
        /// and be between 1 and 40 characters.
        /// </remarks>
        public static bool IsComponentName(this string name)
        {
            //Can not be null or empty.
            if (string.IsNullOrEmpty(name))
                return false;

            var characters = name.ToCharArray();

            //First character has to be a letter or underscore.
            if (!(char.IsLetter(characters[0]) || characters[0] == '_'))
                return false;

            //All characters must be alphanumeric or underscore.
            if (!characters.All(c => char.IsLetter(c) || char.IsDigit(c) || c == '_'))
                return false;

            //Can not have name longer than 40 characters.
            return name.Length <= 40;
        }

        /// <summary>
        /// Determines if the current string is a value <see cref="TagName"/> string.
        /// </summary>
        /// <param name="input">The string input to analyze.</param>
        /// <returns><c>true</c> if the string is a valid tag name string; otherwise, <c>false</c>.</returns>
        public static bool IsTagName(this string input) => Regex.IsMatch(input,
            @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?$");

        /// <summary>
        /// Returns the current string value as an array of <see cref="SINT"/> atomic type with <see cref="Enums.Radix.Ascii"/>
        /// format that represent the bytes of the string.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>An array of <see cref="SINT"/> atomic value types.</returns>
        public static SINT[] ToSintArray(this string value) =>
            Encoding.ASCII.GetBytes(value).Select(b => new SINT((sbyte)b, Radix.Ascii)).ToArray();

        /// <summary>
        /// Returns the collection of <see cref="SINT"/> atomic type values as a <see cref="string"/> value.
        /// </summary>
        /// <param name="array">The array or <see cref="IEnumerable{T}"/> of SINT to convert to string.</param>
        /// <returns>A <see cref="string"/> representing the ASCII character sequence of the SINT array.</returns>
        public static string AsString(this IEnumerable<SINT> array) =>
            Encoding.ASCII.GetString(array.Where(s => s > 0).Select(b => (byte)(sbyte)b).ToArray());

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


        internal static IEnumerable<string> Segment(this string input, int length)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0");

            for (var i = 0; i < input.Length; i += length)
                yield return input.Substring(i, i + length < input.Length ? length : input.Length - i);
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

        /// <summary>
        /// Gets words from the specified string input text.
        /// </summary>
        /// <param name="input">A string input to analyze.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of words.</returns>
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