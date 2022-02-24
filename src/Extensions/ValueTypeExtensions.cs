using System;
using System.Globalization;
using System.Text;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of generic extension methods
    /// </summary>
    internal static class ValueTypeExtensions
    {
        /// <summary>
        /// Converts the current value type to an array of bytes.
        /// </summary>
        /// <param name="valueType">The current value type to convert.</param>
        /// <returns>An array of bytes representing the value.</returns>
        public static byte[] GetBytes(this ValueType valueType)
        {
            return valueType switch
            {
                bool value => BitConverter.GetBytes(value),
                sbyte value => new[] { byte.Parse(value.ToString("X2"), NumberStyles.HexNumber) },
                byte value => new[] { value },
                short value => BitConverter.GetBytes(value),
                int value => BitConverter.GetBytes(value),
                long value => BitConverter.GetBytes(value),
                ushort value => BitConverter.GetBytes(value),
                uint value => BitConverter.GetBytes(value),
                ulong value => BitConverter.GetBytes(value),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Converts the current <see cref="ValueType"/> to the specified base number.
        /// </summary>
        /// <param name="valueType">The value type to convert.</param>
        /// <param name="baseNumber">The base number to convert to.</param>
        /// <returns>A <see cref="string"/> value representing the value in the specified base.</returns>
        public static string ToBase(this ValueType valueType, int baseNumber)
        {
            var bitsPerByte = baseNumber switch
            {
                2 => 8,
                8 => 3,
                _ => 2
            };
            
            var bytes = valueType.GetBytes();

            var builder = new StringBuilder();
            
            for (var ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
            {
                var byteString = Convert.ToString(bytes[ctr], baseNumber).PadLeft(bitsPerByte, '0');
                builder.Append(byteString);
            }

            return builder.ToString();
        }
    }
}