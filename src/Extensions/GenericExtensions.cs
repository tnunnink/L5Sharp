using System;
using System.Globalization;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of generic extension methods
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
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
    }
}