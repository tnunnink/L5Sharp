using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("L5Sharp.Extensions.Tests")]

namespace L5Sharp.Extensions
{
    internal static class ElementExtensions
    {
        /// <summary>
        /// Helper for getting the component name attribute for the current element.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <returns>The string name for attribute value for the component.</returns>
        public static string GetName(this XElement element) => element.Attribute("Name")?.Value;

        /// <summary>
        /// Helper for getting the component data type name attribute.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <returns>The string data type name attribute value for the component.</returns>
        public static string GetDataTypeName(this XElement element) => element.Attribute("DataType")?.Value;

        /// <summary>
        /// Generic helper for getting element values from a given L5X element.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="propertyExpression">Expression for the component indicating which property to get.</param>
        /// <typeparam name="TComponent">The type of the component that this element represents.</typeparam>
        /// <typeparam name="TProperty">The type of the property of the component to retrieve from the element.</typeparam>
        /// <returns>A strongly typed property value that is the value of the XAttribute or XElement data.</returns>
        public static TProperty GetValue<TComponent, TProperty>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression)
        {
            var getter = propertyExpression.ToXExpression();
            return getter.Invoke(element);
        }
    }
}