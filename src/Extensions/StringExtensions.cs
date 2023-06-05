using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
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

        /*/// <summary>
        /// Returns the current string value as an array of <see cref="SINT"/> atomic type with <see cref="Enums.Radix.Ascii"/>
        /// format that represent the bytes of the string.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>An array of <see cref="SINT"/> atomic value types.</returns>
        public static ArrayType ToArrayType(this string value) =>
            

        /// <summary>
        /// Returns the collection of <see cref="SINT"/> atomic type values as a <see cref="string"/> value.
        /// </summary>
        /// <param name="array">The array or <see cref="IEnumerable{T}"/> of SINT to convert to string.</param>
        /// <returns>A <see cref="string"/> representing the ASCII character sequence of the SINT array.</returns>
        public static string AsString(this IEnumerable<SINT> array) =>
            Encoding.ASCII.GetString(array.Where(s => s > 0).Select(b => (byte)(sbyte)b).ToArray());*/

        
        internal static string ReplaceAll(this string value, IEnumerable<string> items, string replacement) =>
            items.Aggregate(value, (str, cItem) => str.Replace(cItem, replacement));


        internal static IEnumerable<string> Segment(this string input, int length)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0");

            for (var i = 0; i < input.Length; i += length)
                yield return input.Substring(i, i + length < input.Length ? length : input.Length - i);
        }
        
        internal static string TrimSingle(this string value, char character)
        {
            if (value.StartsWith(character) && value.EndsWith(character))
                return value.Substring(1, value.Length - 2);

            if (value.StartsWith(character))
                return value.Substring(1, value.Length - 1);

            return value.EndsWith(character) ? value[..^2] : value;
        }
    }
}