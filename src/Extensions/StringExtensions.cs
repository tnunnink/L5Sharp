using System;
using System.Linq;

namespace L5Sharp.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsEmpty(this string value) => value.Equals(string.Empty);

        public static bool IsNumeric(this string value) => double.TryParse(value, out _);

        public static bool IsWholeNumber(this string value) => !value.IsEmpty() && value.All(char.IsNumber);

        public static bool IsExponential(this string value)
        {
            return double.TryParse(value, out _) && value.Contains("e", StringComparison.OrdinalIgnoreCase);
        }
    }
}