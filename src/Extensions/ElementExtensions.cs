using System;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Some common helpers for getting logix element/attribute values from and <see cref="XElement"/>
    /// </summary>
    internal static class ElementExtensions
    {
        /// <summary>
        /// Gets the component name value from the current <see cref="XElement"/> instance.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <returns>The string value of the 'Name' attribute.</returns>
        /// <exception cref="InvalidOperationException">element does not have a Name attribute.</exception>
        public static string ComponentName(this XElement element) =>
            element.Attribute(L5XAttribute.Name.ToString())?.Value
            ?? throw new InvalidOperationException("The current element does not have an attribute 'Name'");

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
        /// <exception cref="InvalidOperationException">element does not have a DataType attribute.</exception>
        public static string DataTypeName(this XElement element) =>
            element.Attribute(L5XAttribute.DataType.ToString())?.Value ??
            throw new InvalidOperationException("The current element does not have an attribute 'DataType'");

        /// <summary>
        /// Adds the provided string name as a attribute to the current element.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <param name="name">The value of the component name.</param>
        public static void AddComponentName(this XElement element, string name) =>
            element.Add(new XAttribute(L5XAttribute.Name.ToString(), name));

        /// <summary>
        /// Adds the provided string description as a child element to the current element if the value is not null or empty.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <param name="description">The value of the component description.</param>
        public static void AddComponentDescription(this XElement element, string description)
        {
            if (string.IsNullOrEmpty(description))
                return;

            element.Add(new XElement(L5XElement.Description.ToString(), new XCData(description)));
        }
    }
}