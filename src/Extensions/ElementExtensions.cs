using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Common;

[assembly: InternalsVisibleTo("L5Sharp.Extensions.Tests")]

namespace L5Sharp.Extensions
{
    internal static class ElementExtensions
    {
        /// <summary>
        /// Helper for getting the current element's component name.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <returns>The string value of the element's attribute 'Name'.</returns>
        /// <exception cref="InvalidOperationException">When the element attribute is null (does not exist for the current element).</exception>
        public static string GetComponentName(this XElement element) =>
            element.Attribute(LogixNames.Name)?.Value
            ?? throw new InvalidOperationException("The current element does not have an attribute with name 'Name'");

        /// <summary>
        /// Helper for getting the current element's data type instance.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <returns>
        /// If the data type is known or defined in the current XDocument, then an instance of  <see cref="IDataType"/>
        /// representing that type; otherwise, and instance of <see cref="Types.Undefined"/>.
        /// </returns>
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

            return value is not null ? LogixParser.Parse<TProperty>(value) : default;
        }

        /// <summary>
        /// Helper for converting a <see cref="TComponent"/> property to an <c>XAttribute</c> for serialization.
        /// </summary>
        /// <param name="component">The current <c>ILogixComponent</c> instance.</param>
        /// <param name="expression">A member expression that points to the specified property to convert.</param>
        /// <param name="nameOverride">An optional parameter that overrides the name of the attribute being converted.</param>
        /// <typeparam name="TComponent"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static XAttribute ToAttribute<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> expression, XName? nameOverride = null)
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
            if (expression.Body is not MemberExpression memberExpression)
                throw new InvalidOperationException($"Expression must be {typeof(MemberExpression)}");

            var name = nameOverride ?? memberExpression.Member.GetXName();

            var value = expression.Compile().Invoke(component);

            return useCDataElement
                ? new XElement(name, new XCData(value.ToString()))
                : new XElement(name, value);
        }
    }
}