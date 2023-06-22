using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Elements;
using L5Sharp.Extensions;

namespace L5Sharp;

/// <summary>
/// Container for all public extensions methods that add functionality to the base components of the library.
/// </summary>
public static class LogixExtensions
{
    /// <summary>
    /// Determines if a component with the specified name exists in the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <returns><c>true</c> if a component with the specified name exists; otherwise, <c>false</c>.</returns>
    public static bool Contains<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        return container.Serialize().Elements().Any(e => e.LogixName() == name);
    }
    
    /// <summary>
    /// Returns a component with the specified name if it exists in the container, otherwise returns <c>null</c>.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent{TComponent}"/> of the specified type if found; Otherwise, <c>null</c>.</returns>
    public static TComponent? Find<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        var element = container.Serialize();
        var component = element.Elements().SingleOrDefault(e => e.LogixName() == name);
        return component is not null ? LogixSerializer.Deserialize<TComponent>(component) : default;
    }

    /// <summary>
    /// Returns a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent{TComponent}"/> of the specified type.</returns>
    /// <exception cref="InvalidOperationException">No component having <c>name</c> exists in the container.</exception>
    public static TComponent Get<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        var element = container.Serialize();
        var component = element.Elements().SingleOrDefault(e => e.LogixName() == name);
        return component is not null
            ? LogixSerializer.Deserialize<TComponent>(component)
            : throw new InvalidOperationException($"No component with name {name} was found in container.");
    }

    /// <summary>
    /// Removes a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to remove.</param>
    public static void Remove<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        container.Serialize().Elements().SingleOrDefault(c => c.LogixName() == name)?.Remove();
    }

    /// <summary>
    /// Returns all <see cref="DataType"/> instances that are dependent on the specified data type name.
    /// </summary>
    /// <param name="dataTypes">The logix collection of data types.</param>
    /// <param name="name">The name of the data type for which to find dependencies.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="DataType"/> that are dependent on the specified data type name.</returns>
    /// <remarks>
    /// This extension serves as an example of how one could extend the API of specific component collections to
    /// add custom XML queries against the source L5X and return materialized components.
    /// </remarks>
    public static IEnumerable<DataType> DependentsOf(this LogixContainer<DataType> dataTypes, string name)
    {
        return dataTypes.Serialize().Descendants(L5XName.DataType)
            .Where(e => e.Descendants(L5XName.Member).Any(m => m.Attribute(L5XName.DataType)?.Value == name))
            .Select(e => new DataType(e));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static LogixTextQuery Text(this LogixContent content) => new(content);

    /// <summary>
    /// Creates a tag name lookup for the current collection of <c>Rung</c> logic.
    /// </summary>
    /// <param name="rungs">A collection of <see cref="Rung"/> logic.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> where each tag name withing the rungs is a key and it's
    /// corresponding value is a <see cref="List{T}"/> containing all the <see cref="Rung"/> referencing
    /// found in the collection.</returns>
    /// <remarks>
    /// This is useful for performing quick lookup of logic references by tag name.
    /// </remarks>
    public static Dictionary<TagName, List<Rung>> ToTagLookup(this IEnumerable<Rung> rungs)
    {
        var results = new Dictionary<TagName, List<Rung>>();

        foreach (var rung in rungs)
        {
            var tags = rung.Text.Tags();

            foreach (var tag in tags)
            {
                if (!results.ContainsKey(tag))
                {
                    results.Add(tag, new List<Rung> { rung });
                    continue;
                }

                results[tag].Add(rung);
            }
        }

        return results;
    }

    /// <summary>
    /// Returns all referenced tag names and their corresponding list of <see cref="NeutralText"/> logic references in
    /// the current collection of <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="logic">A collection of <see cref="NeutralText"/> rung logic.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> where each tag name is a key and it's corresponding value is
    /// a <see cref="List{T}"/> containing all the logic referencing the tag found in the file.</returns>
    /// <remarks>
    /// This is useful for performing quick lookup of logic references by tag name.
    /// </remarks>
    public static Dictionary<TagName, List<NeutralText>> ToTagLookup(this IEnumerable<NeutralText> logic)
    {
        var results = new Dictionary<TagName, List<NeutralText>>();

        foreach (var text in logic)
        {
            var tags = text.Tags();

            foreach (var tag in tags)
            {
                if (!results.ContainsKey(tag))
                {
                    results.Add(tag, new List<NeutralText> { text });
                    continue;
                }

                results[tag].Add(text);
            }
        }

        return results;
    }

    #region ElementExtensions

    /// <summary>
    /// Gets the L5X element name for the specified type. 
    /// </summary>
    /// <param name="type">The type to get the L5X element name for.</param>
    /// <returns>A <see cref="XName"/> representing the name of the element that corresponds to the type.</returns>
    /// <remarks>
    /// All this does is first look for the class attribute <see cref="XmlTypeAttribute"/> to use as the explicitly
    /// configured name, and if not found, returns the type name as the default element name.
    /// </remarks>
    public static XName LogixTypeName(this Type type)
    {
        var attribute = type.GetCustomAttribute<XmlTypeAttribute>();
        return attribute is not null ? attribute.TypeName : type.Name;
    }

    /// <summary>
    /// Gets the <c>Name</c> attribute value for the current <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> instance.</param>
    /// <returns>A <see cref="string"/> representing the name value.</returns>
    /// <remarks>
    /// This is a helper since we access and use the name attribute so often I just wanted to make
    /// the code more concise.
    /// </remarks>
    public static string LogixName(this XElement element) => element.Attribute(L5XName.Name)?.Value ?? string.Empty;

    #endregion

    #region StringExtensions

    /// <summary>
    /// Determines if the current string is equal to string.Empty.
    /// </summary>
    /// <param name="value">The string input to analyze.</param>
    /// <returns>true if the string is empty. Otherwise false.</returns>
    public static bool IsEmpty(this string value) => value.Equals(string.Empty);

    /// <summary>
    /// Tests the current string to indicate whether it is a valid Logix component name value. 
    /// </summary>
    /// <param name="name">The string name to test.</param>
    /// <returns><c>true</c> if <c>name</c> passes the Logix component name requirements; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Valid name must contain only alphanumeric or underscores, start with a letter or underscore,
    /// and be between 1 and 40 characters.
    /// </remarks>
    public static bool IsComponentName(this string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        var characters = name.ToCharArray();
        if (name.Length > 40) return false;
        if (!(char.IsLetter(characters[0]) || characters[0] == '_')) return false;
        return characters.All(c => char.IsLetter(c) || char.IsDigit(c) || c == '_');
    }

    /// <summary>
    /// Determines if the current string is a value <see cref="TagName"/> string.
    /// </summary>
    /// <param name="input">The string input to analyze.</param>
    /// <returns><c>true</c> if the string is a valid tag name string; otherwise, <c>false</c>.</returns>
    public static bool IsTagName(this string input) => Regex.IsMatch(input,
        @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?$");

    #endregion
}