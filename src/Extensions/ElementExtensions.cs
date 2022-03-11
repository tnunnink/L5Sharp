using System;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Extensions
{
    internal static class ElementExtensions
    {
        /// <summary>
        /// Gets the component name value from the current <see cref="XElement"/> instance.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <returns>The string value of the 'Name' attribute.</returns>
        /// <exception cref="InvalidOperationException">When the current element does not have a name value.</exception>
        public static string ComponentName(this XElement element) =>
            element.Attribute(L5XAttribute.Name.ToString())?.Value
            ?? throw new InvalidOperationException("The current element does not have an attribute with name 'Name'");

        /// <summary>
        /// Gets the component description value from the current <see cref="XElement"/> instance.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <returns>The string value of the 'Description' element if it exists; otherwise, empty string.</returns>
        public static string ComponentDescription(this XElement element) =>
            element.Element(L5XElement.Description.ToString())?.Value
            ?? string.Empty;

        /// <summary>
        /// Helper for getting the current element's data type name.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <returns>
        /// 
        /// </returns>
        public static string DataTypeName(this XElement element) =>
            element.Attribute(L5XAttribute.DataType.ToString())?.Value ??
            throw new InvalidOperationException(
                "The current element does not have an attribute with name 'DataType'");
    }
}