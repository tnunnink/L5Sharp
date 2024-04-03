using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Extensions methods that assist with the base functionality of the library.
/// </summary>
public static class L5XExtensions
{
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

        var constructor = type.GetConstructor([typeof(XElement)]);

        if (constructor is null || !constructor.IsPublic)
            throw new ArgumentException(
                $"Can not build expression for type '{type.Name}' without public constructor accepting a XElement parameter.");

        var parameter = Expression.Parameter(typeof(XElement), "element");
        var expression = Expression.New(constructor, parameter);
        return Expression.Lambda<Func<XElement, TReturn>>(expression, parameter).Compile();
    }

    /// <summary>
    /// Determines if the current string is a value <see cref="TagName"/> string.
    /// </summary>
    /// <param name="input">The string input to analyze.</param>
    /// <returns><c>true</c> if the string is a valid tag name string; otherwise, <c>false</c>.</returns>
    public static bool IsTag(this string input) => TagName.IsTag(input);

    /// <summary>
    /// Determines if the current string is equal to string.Empty.
    /// </summary>
    /// <param name="value">The string input to analyze.</param>
    /// <returns>true if the string is empty. Otherwise false.</returns>
    public static bool IsEmpty(this string value) => value.Equals(string.Empty);

    /// <summary>
    /// Determines if this string is the same as, meaning equal to regardless of case, another string.
    /// </summary>
    /// <param name="value">The string value to compare.</param>
    /// <param name="other">The other string to compare.</param>
    /// <returns><c>true</c> if the strings are equal using the <see cref="StringComparer.OrdinalIgnoreCase"/>
    /// equality comparer,. Otherwise <c>false</c>.</returns>
    /// <remarks>This is a simplified way of calling the string comparer equals method since it is a little verbose.
    /// This could be used a lot since Logix naming is case agnostic.</remarks>
    public static bool IsEquivalent(this string value, string other) =>
        StringComparer.OrdinalIgnoreCase.Equals(value, other);

    /// <summary>
    /// Determines if the provided object is structurally or deeply equal to another object by performing a compare
    /// of the underlying XML data for the objects. 
    /// </summary>
    /// <param name="serializable">This <see cref="ILogixSerializable"/> object to compare.</param>
    /// <param name="other">The other <see cref="ILogixSerializable"/> to compare.</param>
    /// <returns><c>ture</c> if the objects are equivalent; Otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This internally compares the underlying XML nodes of the two elements to determine if all their
    /// elements, attributes, and values are equal. This includes all nested or descendant elements (i.e. it
    /// compares the entire XML structure).
    /// </remarks>
    public static bool IsEquivalent(this ILogixSerializable serializable, ILogixSerializable other)
    {
        return XNode.DeepEquals(serializable.Serialize(), other.Serialize());
    }

    /// <summary>
    /// Gets the L5X element name of the type's containing element. 
    /// </summary>
    /// <param name="type">The type to get the L5X element name for.</param>
    /// <returns>
    /// A <see cref="string"/> representing the name of the parent element that corresponds to the type's container.
    /// </returns>
    /// <remarks>
    /// All this does is look for the first <see cref="L5XTypeAttribute"/> to use as the explicitly configured container
    /// name, and if not found, returns the <see cref="Type"/> name with an 's' appended as the default element container
    /// name, as most type's element container is just the plural type name. This is unsophisticated pluralization,
    /// but it works for all cases in the L5X.
    /// </remarks>
    public static string L5XContainer(this Type type)
    {
        var attribute = type.GetCustomAttributes<L5XTypeAttribute>().FirstOrDefault();
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
        var line = ((IXmlLineInfo)element).HasLineInfo() ? ((IXmlLineInfo)element).LineNumber : -1;
        var exception = new InvalidOperationException(message);
        exception.Data.Add("target", name);
        exception.Data.Add("line", line);
        exception.Data.Add("element", element.ToString());
        return exception; 
    }

    /// <summary>
    /// Gets the L5X element local name for the current element, which represents the "L5XType". 
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to get the type of.</param>
    /// <returns>A <see cref="string"/> representing the type name for the element.</returns>
    /// <remarks>
    /// The "L5XType" is simply the name of the element. Since elements map to classes, we can know which
    /// type to deserialize or instantiate given the name of the element.
    /// </remarks>
    public static string L5XType(this XElement element) => element.Name.LocalName;

    /// <summary>
    /// Gets the first configured L5XType name or element name for the specified type. 
    /// </summary>
    /// <param name="type">The type to get the L5XType (element name) for.</param>
    /// <returns>A <see cref="string"/> representing the name of the element that corresponds to the type.</returns>
    /// <remarks>
    /// <para>
    /// All this does look for the first <see cref="L5XTypeAttribute"/> configured to use as the explicitly configured
    /// name, and if not found, returns the <see cref="Type"/> name as the default element name,
    /// as most types are the name of the element anyway.
    /// </para>
    /// <para>
    /// The "L5XType" is simply the name of the element. Since elements map the classes, we can know which
    /// type to deserialize or instantiate given the name of the element.
    /// </para>
    /// </remarks>
    public static string L5XType(this Type type)
    {
        var attribute = type.GetCustomAttributes<L5XTypeAttribute>().FirstOrDefault();
        return attribute is not null ? attribute.TypeName : type.Name;
    }

    /// <summary>
    /// Gets all the configured L5XType names fo the current type.
    /// </summary>
    /// <param name="type">The type to get the L5XTypes (element names) name for.</param>
    /// <returns>A collection of <see cref="string"/> values representing the element names the type supports.</returns>
    /// <remarks>
    /// This attempts to find all configured class <see cref="L5XTypeAttribute"/> to use as the explicitly
    /// configured name(s) for the type, and if not found, returns just the <see cref="Type"/> name as the
    /// default element name, as most types are the name of the element anyway.
    /// </remarks>
    public static IEnumerable<string> L5XTypes(this Type type)
    {
        var attributes = type.GetCustomAttributes<L5XTypeAttribute>().ToList();
        return attributes.Any() ? attributes.Select(attribute => attribute.TypeName) : new[] { type.Name };
    }

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
    /// Gets the name value of the current member <see cref="XElement"/> object. This can be either the <c>Name</c> or
    /// <c>Index</c> attribute depending on what member type it is.
    /// </summary>
    /// <param name="element">The element object for which to get the name.</param>
    /// <returns>A <see cref="string"/> containing the value of the name or index attribute. If neither attribute is
    /// found then this returns an empty string.</returns>
    public static string MemberName(this XElement element)
    {
        var name = element.Attribute(L5XName.Name)?.Value;
        if (name is not null) return name;

        var index = element.Attribute(L5XName.Index)?.Value;
        return index ?? string.Empty;
    }

    /// <summary>
    /// Gets the <c>DataType</c> attribute value for the provided element or it's parent element, which ever value is
    /// found first.
    /// </summary>
    /// <param name="element">The element to retrieve the data type for.</param>
    /// <returns>A <see cref="string"/> indicating the value of the data type property.</returns>
    /// <remarks>
    /// This is a helper for deserializing data structures. Most data elements have the data type we need
    /// in order to determine which object to construct, but some don't and we need to look at it's parent element
    /// to find out. Obviously, if we can't find the <c>DataType</c> value then we can't deserialize the type.
    /// </remarks>
    public static string? DataType(this XElement element)
    {
        //first check the provided element and return if found.
        var local = element.Attribute(L5XName.DataType)?.Value;
        if (local is not null) return local;

        //then check the parent element and return if found.
        var parent = element.Parent?.Attribute(L5XName.DataType)?.Value;
        return parent ?? null;
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
    /// All this info along with the corresponding tag suffix make up the tag name for a module tag,
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
    /// Returns the string value as a <see cref="XName"/> value object.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A <see cref="XName"/> object containing the string value.</returns>
    /// <remarks>This is to make converting from string to XName concise.</remarks>
    public static XName XName(this string value) => System.Xml.Linq.XName.Get(value);

    /// <summary>
    /// Combines the collection of string values into a single string separated by the provided character.
    /// </summary>
    /// <param name="enumerable">The collection to combine.</param>
    /// <param name="separator">The character to separate the items of the collection.</param>
    /// <returns>A <see cref="string"/> containing all the items of the collection separated by the provided character.</returns>
    /// <remarks>This was added to assign with supporting net standard and LINQ syntax.</remarks>
    public static string Combine(this IEnumerable<object>? enumerable, char separator)
    {
        return enumerable is not null ? string.Join(separator.ToString(), enumerable) : string.Empty;
    }
    
    /*private static string TrimSingle(this string value, char character)
    {
        if (value.StartsWith(character) && value.EndsWith(character))
            return value.Substring(1, value.Length - 2);

        if (value.StartsWith(character))
            return value.Substring(1, value.Length - 1);

        return value.EndsWith(character) ? value.Substring(0, value.Length - 1) : value;
    }*/

    //Extensions for .NET Standard 2.0 to allow me not to have to rewrite the code in certain places. 
#if NETSTANDARD2_0
    /// <summary>
    /// Tries to add a key-value pair to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key of the pair to add.</param>
    /// <param name="value">The value of the pair to add.</param>
    /// <returns>
    /// <c>true</c> if the pair was added successfully; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>This is added to support this method for net standard 2.0.</remarks>
    public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key)) return false;
        dictionary.Add(key, value);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsGenericTypeParameter(this Type type)
    {
        //todo not sure this is correct but we need to figure it out.
        return type is { IsGenericType: true, IsGenericParameter: true };
    }

    /// <summary>
    /// Determines whether the specified string contains a specific character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to search for.</param>
    /// <returns>
    /// <c>true</c> if the specified string contains the character; otherwise, <c>false</c>.
    /// </returns>
    public static bool Contains(this string value, char character)
    {
        return value.IndexOf(character) != -1;
    }

    /// <summary>
    /// Determines whether the string starts with the specified character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to compare.</param>
    /// <returns>
    /// true if the string starts with the specified character; otherwise, false.
    /// </returns>
    public static bool StartsWith(this string value, char character)
    {
        return value.Length != 0 && value[0] == character;
    }

    /// <summary>
    /// Determines whether a string ends with a specified character.
    /// </summary>
    /// <param name="value">The string to search.</param>
    /// <param name="character">The character to compare.</param>
    /// <returns>true if <paramref name="value"/> ends with <paramref name="character"/>; otherwise, false.</returns>
    public static bool EndsWith(this string value, char character)
    {
        return value.Length != 0 && value[value.Length - 1] == character;
    }

    /// <summary>
    /// Splits a string into substrings based on a specified separator and removal options.
    /// </summary>
    /// <param name="value">The string to split.</param>
    /// <param name="separator">A character used to specify the delimiter for splitting the string.
    /// The separator itself is not included in the resulting substrings.</param>
    /// <param name="options">Specifies whether empty substrings should be removed from the resulting array.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of strings that contains the substrings in the input string that are delimited by the separator.</returns>
    public static IEnumerable<string> Split(this string value, char separator, StringSplitOptions options)
    {
        return value.Split([separator], options);
    }

#endif
}