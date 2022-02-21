using System.Xml.Linq;
using L5Sharp.Helpers;

namespace L5Sharp.Extensions
{
    internal static class L5XExtensions
    {
        /// <summary>
        /// Converts the <see cref="L5XElement"/> enum value to an <see cref="XName"/> value.
        /// </summary>
        /// <param name="name">The L5XElement name to convert</param>
        /// <returns>A new <see cref="XName"/> value representing the name of the enumeration value.</returns>
        public static XName ToXName(this L5XElement name) => XName.Get(name.ToString());
        
        /// <summary>
        /// Converts the <see cref="L5XAttribute"/> enum value to an <see cref="XName"/> value.
        /// </summary>
        /// <param name="name">The L5XAttribute name to convert</param>
        /// <returns>A new <see cref="XName"/> value representing the name of the enumeration value.</returns>
        public static XName ToXName(this L5XAttribute name) => XName.Get(name.ToString());
    }
}