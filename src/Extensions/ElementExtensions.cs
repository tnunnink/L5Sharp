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
        /// Gets the component name value from the current <see cref="XElement"/> instance.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <returns>The string value of the 'Name' attribute.</returns>
        /// <exception cref="InvalidOperationException">When the current element does not have a name value.</exception>
        public static string GetComponentName(this XElement element) =>
            element.Attribute(LogixNames.Name)?.Value
            ?? throw new InvalidOperationException("The current element does not have an attribute with name 'Name'");
        
        /// <summary>
        /// Gets the component description value from the current <see cref="XElement"/> instance.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <returns>The string value of the 'Description' element if it exists; otherwise, empty string.</returns>
        public static string GetComponentDescription(this XElement element) =>
            element.Element(LogixNames.Description)?.Value
            ?? string.Empty;

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
        /// Get a specified property value from current <see cref="XElement"/> instance.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> instance.</param>
        /// <param name="propertySelector">An expression that selects the property of the specified type.</param>
        /// <param name="isElement">
        /// A indication as to whether to read the selected property as an <see cref="XElement"/>,
        /// or an <see cref="XAttribute"/> (which is the default).
        /// </param>
        /// <param name="nameOverride">A string override to replace the default name (name of the property) being used.</param>
        /// <typeparam name="TComplex">The type of the object for which the property is selected.</typeparam>
        /// <typeparam name="TProperty">The type of the property being selected from <see cref="element"/>.</typeparam>
        /// <returns>
        /// A strongly typed property value that is the value of the <see cref="XAttribute"/> or <see cref="XElement"/>
        /// retrieved from the current <see cref="element"/> instance.
        /// </returns>
        /// <exception cref="ArgumentException">When propertySelector is not a <see cref="MemberExpression"/>.</exception>
        public static TProperty? GetValue<TComplex, TProperty>(this XElement element,
            Expression<Func<TComplex, TProperty>> propertySelector,
            bool isElement = false, string? nameOverride = null)
        {
            if (propertySelector.Body is not MemberExpression memberExpression)
                throw new ArgumentException($"PropertySelector expression must be {typeof(MemberExpression)}");

            var name = nameOverride ?? memberExpression.Member.Name;

            var value = isElement ? element.Element(name)?.Value : element.Attribute(name)?.Value;

            return value is not null ? LogixParser.Parse<TProperty>(value) : default;
        }

        /// <summary>
        /// Adds a specified property of a given complex instance to the current <see cref="XElement"/> instance. 
        /// </summary>
        /// <param name="element">The current <c>XElement</c> instance.</param>
        /// <param name="instance">The instance of the type from which the selected property will be added.</param>
        /// <param name="propertySelector">An expression that selects the property of the provided type to add to the <c>XElement</c>.</param>
        /// <param name="isElement">A indication as to write the selected property as an element instance of an attribute (which is the default).</param>
        /// <param name="nameOverride">A string override to replace the default name (name of the property) of the content being added to the <c>XElement</c>.</param>
        /// <typeparam name="TComplex">The type of the object for which a member property will be added.</typeparam>
        /// <typeparam name="TProperty">The type of the property being added to the <c>XElement</c>.</typeparam>
        /// <exception cref="ArgumentNullException">When instance is null.</exception>
        /// <exception cref="ArgumentException">When propertySelector is not a <see cref="MemberExpression"/>.</exception>
        /// <remarks>
        /// This is a helper method for quickly adding content to a given XElement.
        /// By default, this method will add properties as XAttributes with the name property specified by <see cref="propertySelector"/>.
        /// This functionality can be overriden with <see cref="isElement"/> and <see cref="nameOverride"/>.
        /// All content written as a child <c>XElement</c> will be wrapped in <see cref="XCData"/> element (since this is default for content in L5X).
        /// This method will also call <see cref="object.ToString()"/> to attempt to get the string value of the selected property.
        /// If the property is null or empty, then no content will be added.
        /// </remarks>
        public static void AddValue<TComplex, TProperty>(this XElement element, TComplex instance,
            Expression<Func<TComplex, TProperty>> propertySelector,
            bool isElement = false, string? nameOverride = null)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));

            if (propertySelector.Body is not MemberExpression memberExpression)
                throw new ArgumentException($"PropertySelector expression must be {typeof(MemberExpression)}");

            var name = nameOverride ?? memberExpression.Member.Name;

            var value = propertySelector.Compile().Invoke(instance)?.ToString() ?? string.Empty;

            if (value.IsEmpty()) return;

            object content = isElement ? new XElement(name, new XCData(value)) : new XAttribute(name, value);

            element.Add(content);
        }
    }
}