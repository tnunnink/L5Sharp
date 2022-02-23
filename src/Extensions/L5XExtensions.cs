using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Helpers;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Extensions
{
    internal static class L5XExtensions
    {
        private static readonly Dictionary<L5XElement, IL5XSerializer> Serializers = new()
        {
            { L5XElement.DataValueMember, new DataValueMemberSerializer() },
            { L5XElement.ArrayMember, new ArrayMemberSerializer() },
            { L5XElement.StructureMember, new StructureMemberSerializer() },
        };

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

        public static IL5XSerializer GetSerializer(this L5XElement element)
        {
            return Serializers.TryGetValue(element, out var serializer)
                ? serializer
                : throw new ArgumentException($"No serializer defined for element name {element}");
        }
    }
}