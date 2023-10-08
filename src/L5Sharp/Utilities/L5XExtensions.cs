using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using L5Sharp.Common;

namespace L5Sharp.Utilities;

/// <summary>
/// Extensions methods that assist with the base functionality of the library.
/// </summary>
public static class L5XExtensions
{
    /// <summary>
    /// Gets the <c>Name</c> attribute value for the current <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> instance.</param>
    /// <returns>A <see cref="string"/> representing the name value if found; Otherwise, <c>empty</c>.</returns>
    /// <remarks>
    /// This is a helper since we access and use the name attribute so often I just wanted to make
    /// the code more concise.
    /// </remarks>
    public static string LogixName(this XElement element) => element.Attribute(L5XName.Name)?.Value ?? string.Empty;

    /// <summary>
    /// Determines if the current string is a value <see cref="TagName"/> string.
    /// </summary>
    /// <param name="input">The string input to analyze.</param>
    /// <returns><c>true</c> if the string is a valid tag name string; otherwise, <c>false</c>.</returns>
    public static bool IsTagName(this string input) => Regex.IsMatch(input,
        @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?$");

    /// <summary>
    /// Determines if the current string is equal to string.Empty.
    /// </summary>
    /// <param name="value">The string input to analyze.</param>
    /// <returns>true if the string is empty. Otherwise false.</returns>
    public static bool IsEmpty(this string value) => value.Equals(string.Empty);

    /// <summary>
    /// Returns the string value as a <see cref="XName"/> value object.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A <see cref="XName"/> object containing the string value.</returns>
    /// <remarks>This is to make converting from string to XName concise.</remarks>
    public static XName XName(this string value) => System.Xml.Linq.XName.Get(value);

    /// <summary>
    /// A concise method for getting a required attribute value from a XElement object.
    /// </summary>
    /// <param name="element">The element containing the attribute to retrieve.</param>
    /// <param name="name">The name of the attribute value to get.</param>
    /// <returns>The <see cref="string"/> value of the element's specified attribute.</returns>
    /// <exception cref="InvalidOperationException">No attribute with <c>name</c> exists for the current element.</exception>
    public static string Get(this XElement element, XName name) =>
        element.Attribute(name)?.Value ?? throw element.L5XError(name);

    /// <summary>
    /// Gets the L5X element local name for the current element, which represents the L5X type. 
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to get the type of.</param>
    /// <returns>A <see cref="string"/> representing the type name for the element.</returns>
    public static string L5XType(this XElement element) => element.Name.LocalName;

    /// <summary>
    /// Gets the L5X element name for the specified type. 
    /// </summary>
    /// <param name="type">The type to get the L5X element name for.</param>
    /// <returns>A <see cref="string"/> representing the name of the element that corresponds to the type.</returns>
    /// <remarks>
    /// All this does is first look for a class <see cref="L5XTypeAttribute"/> with the <c>IsPrimaryAttribute</c> configured
    /// to use as the explicitly configured name, and if not found, returns the <see cref="Type"/> name as the
    /// default element name, as most types are the name of the element.
    /// </remarks>
    public static string L5XType(this Type type)
    {
        var attribute = type.GetCustomAttributes<L5XTypeAttribute>().FirstOrDefault(a => a.IsPrimaryType);
        return attribute is not null ? attribute.TypeName : type.Name;
    }

    /// <summary>
    /// Gets the L5X element name for the specified type. 
    /// </summary>
    /// <param name="type">The type to get the L5X element name for.</param>
    /// <returns>A <see cref="string"/> representing the name of the element that corresponds to the type.</returns>
    /// <remarks>
    /// This attempts to find all configured class <see cref="L5XTypeAttribute"/> to use as the explicitly
    /// configured name(s) for the type, and if not found, returns the <see cref="Type"/> name as the
    /// default element name, as most types are the name of the element.
    /// </remarks>
    public static IEnumerable<string> L5XTypes(this Type type)
    {
        var attributes = type.GetCustomAttributes<L5XTypeAttribute>().ToList();
        return attributes.Any() ? attributes.Select(attribute => attribute.TypeName) : new[] {type.Name};
    }

    /// <summary>
    /// Gets the L5X element name of the type's containing element. 
    /// </summary>
    /// <param name="type">The type to get the L5X element name for.</param>
    /// <returns>
    /// A <see cref="string"/> representing the name of the parent element that corresponds to the type's container.
    /// </returns>
    /// <remarks>
    /// All this does is first look for a class <see cref="L5XTypeAttribute"/> with the <c>IsPrimaryAttribute</c> configured
    /// to use as the explicitly configured container name, and if not found, returns the <see cref="Type"/> name
    /// with an 's' appended as the default element container name, as most type's element container is the type
    /// plural type name. This is unsophisticated pluralization, but it works for all cases in the L5X.
    /// </remarks>
    public static string L5XContainerType(this Type type)
    {
        var attribute = type.GetCustomAttributes<L5XTypeAttribute>().FirstOrDefault(a => a.IsPrimaryType);
        return attribute is not null ? attribute.ContainerName : $"{type.Name}s";
    }

    /// <summary>
    /// Creates and configures a <see cref="InvalidOperationException"/> to be thrown for required properties of complex
    /// types that do not exist for the current element object.
    /// </summary>
    /// <param name="element">The element for which the exception is occuring.</param>
    /// <param name="name">The name of the attribute or child element that is missing.</param>
    /// <returns>A <see cref="InvalidOperationException"/> object configured with standard message and exception
    /// data for troubleshooting purposes.</returns>
    /// <remarks>This is a helper so to avoid reconfiguring this exception every time it needed to be thrown.
    /// Aside from setting the standard error message, this will add the target attribute name, element line number,
    /// and element object as data to the exception.</remarks>
    public static InvalidOperationException L5XError(this XElement element, XName name)
    {
        var message = $"The required attribute or child element '{name}' does not exist for {element.Name}.";
        var line = ((IXmlLineInfo) element).HasLineInfo() ? ((IXmlLineInfo) element).LineNumber : -1;
        var exception = new InvalidOperationException(message);
        exception.Data.Add("target", name);
        exception.Data.Add("line", line);
        exception.Data.Add("element", element);
        return exception;
    }

    /// <summary>
    /// Builds a deserialization expression delegate which returns the specified type using the current type information.
    /// </summary>
    /// <param name="type">The current type for which to build the expression.</param>
    /// <typeparam name="TReturn">The return type of the expression delegate.</typeparam>
    /// <returns>A <see cref="Func{TResult}"/> which accepts a <see cref="XElement"/> and returns the specified
    /// return type.</returns>
    /// <remarks>
    /// This extension is the basis for how we build the deserialization functions using reflection and
    /// expression trees. Using compiled expression trees is much more efficient that calling the invoke method for a type's
    /// constructor info obtained via reflection. This method makes all the necessary checks on the current type, ensuring the
    /// returned deserializer delegate will execute without exception.
    /// </remarks>
    public static Func<XElement, TReturn> Deserializer<TReturn>(this Type type)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));

        if (type.IsAbstract)
            throw new ArgumentException($"Can not build deserializer expression for abstract type '{type.Name}'.");

        if (!typeof(TReturn).IsAssignableFrom(type))
            throw new ArgumentException(
                $"The type {type.Name} is not assignable (inherited) from '{typeof(TReturn).Name}'.");

        var constructor = type.GetConstructor(new[] {typeof(XElement)});

        if (constructor is null || !constructor.IsPublic)
            throw new ArgumentException(
                $"Can not build expression for type '{type.Name}' without public constructor accepting a XElement parameter.");

        var parameter = Expression.Parameter(typeof(XElement), "element");
        var expression = Expression.New(constructor, parameter);
        return Expression.Lambda<Func<XElement, TReturn>>(expression, parameter).Compile();
    }

    /// <summary>
    /// Returns just the immediate element's CDATA value as a string if found, otherwise returns an empty string.
    /// </summary>
    /// <param name="element">The element for which to retrieve the value of.</param>
    /// <returns>
    /// A <see cref="string"/> containing the text value of the element, and not any descendant element's value.
    /// </returns>
    /// <remarks>This is necessary since the <c>Value</c> of an <see cref="XElement"/> actually also returns all child
    /// element values for some reason.</remarks>
    public static string ShallowValue(this XElement element)
    {
        return element.Nodes().OfType<XCData>()
            .Aggregate(new StringBuilder(), (s, c) => s.Append(c), s => s.ToString());
    }

    /// <summary>
    /// Determines the tag name for a given <see cref="XElement"/> representing a module IO tag.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> representing the module defined IO tag
    /// (InputTag, OutputTag, or ConfigTag).</param>
    /// <returns>A <see cref="TagName"/> representing the name of the module IO tag.</returns>
    /// <remarks>
    /// This is a helper extension since the logic is somewhat complex and used in more than one class.
    /// We look up the L5X tree for module name and parent name, as well as back down to find the potential slot of the module.
    /// All this info, along with the corresponding tag suffix, make up the tag name for a module tag,
    /// which is not inherent in the L5X element itself, but one that is important to us as it allows us to
    /// find or reference these tags by name (just as you would find in Studio 5k).
    /// </remarks>
    public static TagName ModuleTagName(this XElement element)
    {
        var suffix = DetermineModuleSuffix(element);
        var module = element.Ancestors(L5XName.Module).FirstOrDefault();
        var moduleName = module?.Attribute(L5XName.Name)?.Value;
        var parentName = module?.Attribute(L5XName.ParentModule)?.Value;

        var slot = module?.Descendants(L5XName.Port)
            .Where(p => bool.TryParse(p.Attribute(L5XName.Upstream)?.Value, out var upstream) && upstream
                && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
            .Select(p => p.Attribute(L5XName.Address)?.Value)
            .FirstOrDefault();

        return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";

        string DetermineModuleSuffix(XElement el)
        {
            if (el.Name == L5XName.InputTag)
                return el.Parent?.Attribute(L5XName.InputTagSuffix)?.Value ?? "I";

            if (el.Name == L5XName.OutputTag)
                return el.Parent?.Attribute(L5XName.OutputTagSuffix)?.Value ?? "O";

            return "C";
        }
    }
}