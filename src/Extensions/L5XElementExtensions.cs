using System.Xml.Linq;
using L5Sharp.Helpers;

namespace L5Sharp.Extensions
{
    internal static class L5XElementExtensions
    {
        /// <summary>
        /// Converts the <see cref="L5XElement"/> enum value to an <see cref="XName"/> value.
        /// </summary>
        /// <param name="name">The L5XElement name to convert</param>
        /// <returns></returns>
        public static XName ToXName(this L5XElement name) => XName.Get(name.ToString());
    }
}