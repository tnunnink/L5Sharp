using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Types;
using System.Reflection;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of extension methods for <see cref="XElement"/> that assist with interacting with the L5X elements.
    /// </summary>
    public static class ElementExtensions
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
        public static XName LogixTypeName(this Type type)
        {
            var attribute = type.GetCustomAttribute<XmlTypeAttribute>();
            return attribute is not null ? attribute.TypeName : type.Name;
        }

        /// <summary>
        /// Gets the <c>Name</c> attribute value for the current <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> instance.</param>
        /// <returns>A <see cref="string"/> representing the name value.</returns>
        /// <remarks>
        /// This is a helper since we access and use the name attribute so often I just wanted to make
        /// the code more concise.
        /// </remarks>
        public static string LogixName(this XElement element) => 
            element.Attribute(L5XName.Name)?.Value ?? string.Empty;

        /// <summary>
        /// Gets the <c>DataType</c> attribute value for the current <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> instance.</param>
        /// <returns>The <see cref="string"/> value of the attribute if exists; otherwise, <c>null</c>.</returns>
        /// <remarks>
        /// This is a helper since we access and use the data type attribute often, I just wanted to make
        /// the code more concise.
        /// </remarks>
        public static string LogixType(this XElement element) =>
            element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);

        /// <summary>
        /// Gets the value of the name or index attribute of the <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> instance.</param>
        /// <returns>
        /// If name exists, the value of the name attribute.
        /// If index exists, the value of the index attribute.
        /// If neither exists, returns default.
        /// </returns>
        /// <remarks>
        /// This is a helper since we access and use the member name attribute so often I just wanted to make
        /// the code more concise.
        /// </remarks>
        public static string? MemberName(this XElement element) => 
            element.Attribute(L5XName.Name)?.Value ?? (element.Attribute(L5XName.Index)?.Value ?? default);

        /// <summary>
        /// A helper extension for determining a <see cref="Module"/> tag name for an input, output, or config tag.
        /// </summary>
        /// <param name="element">THe current module tag element.</param>
        /// <param name="suffix">The string suffix to append to the determines tag name. Default is 'C' for config tag.</param>
        /// <returns>A <see cref="string"/> representing the tag name of the module tag.</returns>
        public static string ModuleTagName(this XNode element, string suffix = "C")
        {
            var moduleName = element.Ancestors(L5XName.Module)
                .FirstOrDefault()?.Attribute(L5XName.Name)?.Value;

            var parentName = element.Ancestors(L5XName.Module)
                .FirstOrDefault()?.Attribute(L5XName.ParentModule)?.Value;

            var slot = element
                .Ancestors(L5XName.Module)
                .Descendants(L5XName.Port)
                .Where(p => bool.Parse(p.Attribute(L5XName.Upstream)?.Value!)
                            && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                            && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
                .Select(p => p.Attribute(L5XName.Address)?.Value)
                .FirstOrDefault();

            return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
        }

        /// <summary>
        /// Determines if the current element has a structure that indicates it is a <see cref="StringType"/> structure.
        /// </summary>
        /// <param name="element">The current element to examine.</param>
        /// <returns><c>true</c> if the structure checks out as a string structure.</returns>
        public static bool HasLogixStringStructure(this XElement element)
        {
            return element.Descendants(L5XName.DataValueMember).Any(e =>
                e?.Value is not null
                && e.Attribute(L5XName.Name)?.Value == "DATA"
                && e.Attribute(L5XName.DataType)?.Value == element.Attribute(L5XName.DataType)?.Value
                && e.Attribute(L5XName.Radix)?.Value == "ASCII");
        }
    }
}