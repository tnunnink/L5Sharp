using System;
using System.Globalization;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of extension methods for <see cref="XElement"/> that assist with interacting with the L5X elements.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Gets the current <see cref="Use"/> option of the L5X element.
        /// </summary>
        /// <value>A <see cref="Enums.Use"/> enum value.</value>
        public static Use? Use(this XElement element) => element.Attribute(L5XName.Use)?.Value.Parse<Use>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string ComponentName(this XElement element) =>
            element.Attribute(L5XName.Name)?.Value
            ?? throw new InvalidOperationException($"Element {element.Name} does not have attribute 'Name'.");

        /// <summary>
        /// Gets the value of the specified attribute name from the current element parsed as the specified
        /// generic type parameter.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="name">The XName of the XAttribute to get the value of.</param>
        /// <typeparam name="TValue">The type of the value to be parsed.</typeparam>
        /// <returns>A <see cref="TValue"/> of the specified attribute.</returns>
        /// <exception cref="InvalidOperationException">Attribute with <c>name</c> does not exists on element.</exception>
        public static TValue Property<TValue>(this XElement element, XName name)
        {
            var attribute = element.Attribute(name);

            if (attribute is null)
                throw new InvalidOperationException(
                    $"The attribute {name} does not exist on the element {element.Name}");

            return attribute.Value.Parse<TValue>();
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
        public static TValue? PropertyOrDefault<TValue>(this XElement element, XName name)
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
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddProperty<TValue>(this XElement element, TValue value, XName? name = null)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var t = value.GetType().Name;

            element.Add(new XAttribute(name, value.ToString()));
        }

        /*public static void AddProperty<TType, TValue>(this XElement element,
            Expression<Func<TType, TValue>> propertySelector)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            if (propertySelector.Body is not MemberExpression memberExpression)
                throw new ArgumentException();

            var name = memberExpression.Member.Name;
            var value = 

            element.Add(new XAttribute(name, value.ToString()));
        }*/
    }
}