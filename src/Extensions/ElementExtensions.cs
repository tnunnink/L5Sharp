using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of extension methods for <see cref="XElement"/> that assist with interacting with the L5X elements.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Gets the name attribute for the current L5X element.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> for which to get the value.</param>
        /// <returns>A <see cref="string"/> representing the name value.</returns>
        /// <exception cref="InvalidOperationException">The element does not have a name attribute.</exception>
        public static string LogixName(this XElement element) =>
            element.Attribute(L5XName.Name)?.Value
            ?? throw new InvalidOperationException($"Element {element.Name} does not have attribute 'Name'.");

        /// <summary>
        /// Gets the description element value for the current L5X element.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> for which to get the value.</param>
        /// <returns>A <see cref="string"/> representing the description value.</returns>
        public static string LogixDescription(this XElement element) =>
            element.Element(L5XName.Description)?.Value ?? string.Empty;

        /// <summary>
        /// Gets the value of the specified attribute name from the current element parsed as the specified
        /// generic type parameter.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="name">The XName of the XAttribute to get the value of.</param>
        /// <typeparam name="TValue">The type of the value to be parsed.</typeparam>
        /// <returns>A <see cref="TValue"/> of the specified attribute.</returns>
        /// <exception cref="InvalidOperationException">Attribute with <c>name</c> does not exists on element.</exception>
        public static TValue GetValue<TValue>(this XElement element, XName name)
        {
            var property = element.Attribute(name);

            if (property is null)
                throw new InvalidOperationException(
                    $"The attribute {name} does not exist on the element {element.Name}");

            return property.Value.Parse<TValue>();
        }

        /// <summary>
        /// Gets the value of the specified attribute name from the current element parsed as the specified
        /// generic type parameter is it exists. If the attribute does not exist, returns default of specified generic
        /// type parameter.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="name">The XName of the XAttribute to get the value of.</param>
        /// <typeparam name="TValue">The type of the value to be parsed.</typeparam>
        /// <returns>A <see cref="TValue"/> of the specified attribute if exists; otherwise, default.</returns>
        public static TValue? ValueOrDefault<TValue>(this XElement element, XName name)
        {
            var value = element.Attribute(name)?.Value;
            return value is not null ? value.Parse<TValue>() : default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DateTime LogixDateTimeOrDefault(this XElement element, XName name)
        {
            const string format = "ddd MMM d HH:mm:ss yyyy";

            var attribute = element.Attribute(name);

            return attribute is not null
                ? DateTime.ParseExact(attribute.Value, format, CultureInfo.CurrentCulture)
                : default;
        }

        /// <summary>
        /// Adds the provided object value to the current <see cref="XElement"/> as a attribute or element.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> object.</param>
        /// <param name="value">The object value to add to the element. Only added if not null or empty.</param>
        /// <param name="name">The name of the attribute or element to add.</param>
        public static void AddValue(this XElement element, object? value, string name)
        {
            if (value is null || value.ToString().IsEmpty()) 
                return;

            element.Add(new XAttribute(name, value.ToString()));
        }

        /// <summary>
        /// Adds the provided object value to the current <see cref="XElement"/> as a attribute or element.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> object.</param>
        /// <param name="obj">The object for which add the specified property value.</param>
        /// <param name="selector">The expression selecting the property value to add.</param>
        /// <exception cref="ArgumentNullException"><c>obj</c> or <c>selector</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>selector</c> is not a <see cref="MemberExpression"/>.</exception>
        public static void AddValue<T>(this XElement element, T obj, Expression<Func<T, object>> selector)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            if (selector.Body is not MemberExpression memberExpression)
                throw new ArgumentException($"The provided selector expression must be a {typeof(MemberExpression)}");

            var name = memberExpression.Member.Name;
            var value = selector.Compile().Invoke(obj);

            element.AddValue(value, name);
        }

        /// <summary>
        /// Adds the provided text to the current <see cref="XElement"/> as a child element with text wrapped in CData.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> object.</param>
        /// <param name="value">The object value to add to the element. Only added if not null or empty.</param>
        /// <param name="name">The name of the attribute or element to add.</param>
        public static void AddText(this XElement element, string? value, string name)
        {
            if (string.IsNullOrEmpty(value)) 
                return;

            element.Add(new XElement(name, new XCData(value)));
        }
        
        /// <summary>
        /// Adds the specified text property to the current <see cref="XElement"/> as a child element with text wrapped in CData.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> object.</param>
        /// <param name="obj">The object for which add the specified property value.</param>
        /// <param name="selector">The expression selecting the property value to add.</param>
        /// <exception cref="ArgumentNullException"><c>obj</c> or <c>selector</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>selector</c> is not a <see cref="MemberExpression"/>.</exception>
        public static void AddText<T>(this XElement element, T obj, Expression<Func<T, string>> selector)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            if (selector.Body is not MemberExpression memberExpression)
                throw new ArgumentException($"The provided selector expression must be a {typeof(MemberExpression)}");

            var name = memberExpression.Member.Name;
            var value = selector.Compile().Invoke(obj);

            element.AddText(value, name);
        }
    }
}