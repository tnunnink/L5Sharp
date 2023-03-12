using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Types;
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
        public static string LogixName(this XElement element) => element.Attribute(L5XName.Name)?.Value ?? string.Empty;

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
        /// <returns>A value of the specified type if the attribute exists; otherwise, default.</returns>
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
        /// <returns>A value of the specified type if the attribute exists; otherwise, default.</returns>
        public static TValue? TryGetValue<TValue>(this XElement element, XName name)
        {
            var value = element.Attribute(name)?.Value;
            return value is not null ? value.Parse<TValue>() : default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime TryGetDateTime(this XElement element, XName name, string format)
        {
            //const string format = "ddd MMM d HH:mm:ss yyyy";

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
        /// <param name="addCondition">An option condition that specifies when the value should be added.</param>
        public static void AddValue<T>(this XElement element, T? value, string name, Predicate<T>? addCondition = null)
        {
            if (value is null || value.ToString().IsEmpty() || addCondition?.Invoke(value) == false)
                return;

            element.Add(new XAttribute(name, value.ToString()));
        }

        /// <summary>
        /// Adds the provided object value to the current <see cref="XElement"/> as a attribute or element.
        /// </summary>
        /// <param name="element">The current <see cref="XElement"/> object.</param>
        /// <param name="obj">The object for which add the specified property value.</param>
        /// <param name="selector">The expression selecting the property value to add.</param>
        /// <param name="addCondition"></param>
        /// <exception cref="ArgumentNullException"><c>obj</c> or <c>selector</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>selector</c> is not a <see cref="MemberExpression"/>.</exception>
        public static void AddValue<TObj, TValue>(this XElement element, TObj obj,
            Expression<Func<TObj, TValue>> selector,
            Predicate<TValue>? addCondition = null)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            if (selector.Body is not MemberExpression memberExpression)
                throw new ArgumentException($"The provided selector expression must be a {typeof(MemberExpression)}");

            var name = memberExpression.Member.Name;
            var value = selector.Compile().Invoke(obj);

            element.AddValue(value, name, addCondition);
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