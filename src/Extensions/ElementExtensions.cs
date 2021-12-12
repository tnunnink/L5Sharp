using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Utilities;

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
        public static string? GetName(this XElement element) => element.Attribute("Name")?.Value;

        /// <summary>
        /// Helper for getting the component data type name attribute.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <returns>The string data type name attribute value for the component.</returns>
        public static string? GetDataTypeName(this XElement element) => element.Attribute("DataType")?.Value;

        public static IDataType GetDataType(this XElement element)
        {
            var provider = new LogixTypeProvider(element);
            return provider.FindType();
        }

        /// <summary>
        /// Generic helper for getting element values from a given L5X element.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="expression">Expression for the component indicating which property to get.</param>
        /// <typeparam name="TComponent">The type of the component that this element represents.</typeparam>
        /// <typeparam name="TProperty">The type of the property of the component to retrieve from the element.</typeparam>
        /// <returns>A strongly typed property value that is the value of the XAttribute or XElement data.</returns>
        public static TProperty? GetValue<TComponent, TProperty>(this XElement element,
            Expression<Func<TComponent, TProperty>> expression)
        {
            if (expression.Body is not MemberExpression memberExpression)
                throw new InvalidOperationException($"Expression must be {typeof(MemberExpression)}");

            var name = memberExpression.Member.GetXName();

            var value = element.Attribute(name)?.Value;

            if (value == null) return default;

            var converter = LogixParser.Get(typeof(TProperty));

            return (TProperty)converter.Invoke(value);
        }

        public static XAttribute ToAttribute<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> expression, string? nameOverride = null)
        {
            if (expression.Body is not MemberExpression memberExpression)
                throw new InvalidOperationException($"Expression must be {typeof(MemberExpression)}");

            var name = nameOverride ?? memberExpression.Member.GetXName();

            var value = expression.Compile().Invoke(component);

            return new XAttribute(name, value);
        }

        public static XElement ToElement<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> expression, bool useCDataElement = true,
            string? nameOverride = null)
        {
            if (!(expression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException($"Expression must be {typeof(MemberExpression)}");

            var name = nameOverride ?? memberExpression.Member.GetXName();

            var value = expression.Compile().Invoke(component);

            return useCDataElement
                ? new XElement(name, new XCData(value.ToString()))
                : new XElement(name, value);
        }
    }
}