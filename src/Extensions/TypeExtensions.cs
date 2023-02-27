using System;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Some basic <see cref="Type"/> extensions and helper methods.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the L5X element name for the specified type. 
        /// </summary>
        /// <param name="type">The type to get the L5X element name for.</param>
        /// <returns>A <see cref="XName"/> representing the name of the element that corresponds to the type.</returns>
        /// <remarks>
        /// All this does is first look for the class attribute <see cref="XmlTypeAttribute"/> to use as the explicitly
        /// configured name, and if not found, returns the type name as the default element name.
        /// </remarks>
        public static XName GetLogixName(this Type type)
        {
            var attribute = type.GetCustomAttribute<XmlTypeAttribute>();
            return attribute is not null ? attribute.TypeName : type.Name;
        }
    }
}