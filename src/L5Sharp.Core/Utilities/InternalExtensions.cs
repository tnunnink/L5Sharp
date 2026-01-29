using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Extensions methods that help with the base functionality of the library.
/// </summary>
internal static class InternalExtensions
{
    /// <summary>
    /// Determines if the current string is equal to string.Empty.
    /// </summary>
    /// <param name="value">The string input to analyze.</param>
    /// <returns>True if the string is empty. Otherwise, false.</returns>
    internal static bool IsEmpty(this string value)
    {
        return value.Equals(string.Empty);
    }

    /// <summary>
    /// Determines if this string is the same as, meaning equal to regardless of case, another string.
    /// </summary>
    /// <param name="value">The string value to compare.</param>
    /// <param name="other">The other string to compare.</param>
    /// <returns><c>true</c> if the strings are equal using the <see cref="StringComparer.OrdinalIgnoreCase"/>
    /// equality comparer, Otherwise <c>false</c>.</returns>
    /// <remarks>This is a simplified way of calling the string comparer equals method since it is a little verbose.
    /// This could be used a lot since Logix naming is case agnostic.</remarks>
    internal static bool IsEquivalent(this string value, string? other)
    {
        return StringComparer.OrdinalIgnoreCase.Equals(value, other);
    }

    /// <summary>
    /// Combines the collection of string values into a single string separated by the provided character.
    /// </summary>
    /// <param name="enumerable">The collection to combine.</param>
    /// <param name="separator">The character to separate the items of the collection.</param>
    /// <returns>A <see cref="string"/> containing all the items of the collection separated by the provided character.</returns>
    /// <remarks>This was added to assign with supporting .net standard and making the syntax nicer than using string.Join.</remarks>
    internal static string Combine(this IEnumerable<object>? enumerable, char separator)
    {
        return enumerable is not null ? string.Join(separator.ToString(), enumerable) : string.Empty;
    }

    /// <summary>
    /// Removes a single instance of the provided character from the start and end of the current string. 
    /// </summary>
    /// <param name="value">The string to update.</param>
    /// <param name="character">The character to remove.</param>
    /// <returns>A new <see cref="string"/> with the first and last instance of the provided character.</returns>
    internal static string TrimSingle(this string value, char character)
    {
        if (value.StartsWith(character) && value.EndsWith(character))
            return value.Substring(1, value.Length - 2);

        if (value.StartsWith(character))
            return value.Substring(1, value.Length - 1);

        // ReSharper disable once ReplaceSubstringWithRangeIndexer can't do this with .NET Standard 2.0
        return value.EndsWith(character) ? value.Substring(0, value.Length - 1) : value;
    }

    /// <summary>
    /// Finds and replaces occurrences of a specified string within the attributes, text, and CDATA sections
    /// of the given XML element and its descendants.
    /// </summary>
    /// <param name="element">The XML element where the search and replace operation is performed.</param>
    /// <param name="find">The string to search for within the XML element and its children.</param>
    /// <param name="replace">The string to replace the found occurrences with.</param>
    /// <param name="targets">
    /// An array of target element or attribute names where the replace operation should be restricted.
    /// If empty, all elements and attributes will be included in the operation.
    /// </param>
    internal static void FindAndReplace(this XElement element, string find, string replace, string[] targets)
    {
        foreach (var attribute in element.Attributes())
        {
            TryReplaceAttribute(attribute);
        }

        foreach (var node in element.Nodes().ToList())
        {
            switch (node)
            {
                case XCData data:
                    TryReplaceData(node, data);
                    break;
                case XText text:
                    TryReplaceText(node, text);
                    break;
                case XElement child:
                    child.FindAndReplace(find, replace, targets);
                    break;
            }
        }

        return;

        void TryReplaceAttribute(XAttribute attribute)
        {
            if (targets.Length == 0 || targets.Contains(attribute.Name.LocalName))
                attribute.Value = attribute.Value.Replace(find, replace);
        }

        void TryReplaceData(XNode node, XCData data)
        {
            if (targets.Length == 0 || targets.Contains(node.Parent?.Name.LocalName))
                data.ReplaceWith(new XCData(data.Value.Replace(find, replace)));
        }

        void TryReplaceText(XNode node, XText text)
        {
            if (targets.Length == 0 || targets.Contains(node.Parent?.Name.LocalName))
                text.Value = text.Value.Replace(find, replace);
        }
    }

    /// <summary>
    /// Filters a sequence of values by a specific key selector, ensuring that only distinct elements, based on the key, are returned.
    /// </summary>
    /// <param name="source">The source collection to process.</param>
    /// <param name="selector">A function that projects a key for each element in the source collection.</param>
    /// <typeparam name="TSource">The type of elements in the source collection.</typeparam>
    /// <typeparam name="TKey">The type of key used to distinguish elements in the source collection.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains distinct elements from the source collection based on the specified key.</returns>
    internal static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
    {
        var seen = new HashSet<TKey>();

        foreach (var element in source)
        {
            if (seen.Add(selector(element)))
            {
                yield return element;
            }
        }
    }

    /// <summary>
    /// Appends the string representation of a value to the current StringBuilder instance
    /// if a specified condition evaluates to true.
    /// </summary>
    /// <param name="builder">The StringBuilder instance to append to.</param>
    /// <param name="value">The value to be evaluated and potentially appended.</param>
    /// <param name="predicate">A function that determines whether the value should be appended.</param>
    /// <param name="selector">
    /// An optional function that specifies how to convert the value to its string representation.
    /// If not provided, the value's <c>ToString</c> method is used.
    /// </param>
    /// <typeparam name="T">The type of the value to be appended.</typeparam>
    /// <returns>The updated StringBuilder instance.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="builder"/> or <paramref name="predicate"/> is null.
    /// </exception>
    public static StringBuilder AppendIf<T>(this StringBuilder builder, T value,
        Func<T, bool> predicate,
        Func<T, string>? selector = null)
    {
        if (builder is null) throw new ArgumentNullException(nameof(builder));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));

        var text = selector?.Invoke(value) ?? value?.ToString() ?? null;

        if (predicate(value) && text is not null)
        {
            builder.Append(text);
        }

        return builder;
    }

    /// <summary>
    /// Determines whether the specified element is null.
    /// </summary>
    /// <typeparam name="T">The type of the element.</typeparam>
    /// <param name="element">The element to evaluate.</param>
    /// <param name="result">The output parameter that will contain the value of the element if it is not null.</param>
    /// <returns>True if the element is not null; otherwise, false.</returns>
    internal static bool IsNull<T>(this T? element, out T result) where T : class
    {
        if (element is null)
        {
            result = null!;
            return false;
        }

        result = element;
        return true;
    }

    /// <summary>
    /// Determines whether the specified string contains a specific character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to search for.</param>
    /// <returns>
    /// <c>true</c> if the specified string contains the character; otherwise, <c>false</c>.
    /// </returns>
    internal static bool Contains(this string value, char character)
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
    internal static bool StartsWith(this string value, char character)
    {
        return value.Length != 0 && value[0] == character;
    }

    /// <summary>
    /// Determines whether a string ends with a specified character.
    /// </summary>
    /// <param name="value">The string to search.</param>
    /// <param name="character">The character to compare.</param>
    /// <returns>true if <paramref name="value"/> ends with <paramref name="character"/>; otherwise, false.</returns>
    internal static bool EndsWith(this string value, char character)
    {
        return value.Length != 0 && value[value.Length - 1] == character;
    }

    /// <summary>
    /// Inserts a specified separator character in the string at every defined interval.
    /// </summary>
    /// <param name="value">The original string to be processed.</param>
    /// /// <param name="separator">The character to insert as a separator.</param>
    /// <param name="count">The number of characters between each separator.</param>
    /// <returns>A new string with the separator inserted at the specified intervals.</returns>
    public static string SeparateWith(this string value, string separator, int count)
    {
        if (count == 0 || value.Length < count)
            return value;

        var builder = new StringBuilder();

        for (var i = value.Length - 1; i >= 0; i--)
        {
            builder.Append(value[i]);

            if ((value.Length - i) % count == 0 && i > 0)
                builder.Append(separator);
        }

        return new string(builder.ToString().Reverse().ToArray());
    }

    /// <summary>
    /// Splits a string into substrings based on a specified separator and removal options.
    /// </summary>
    /// <param name="value">The string to split.</param>
    /// <param name="separator">A character used to specify the delimiter for splitting the string.
    /// The separator itself is not included in the resulting substrings.</param>
    /// <param name="options">Specifies whether empty substrings should be removed from the resulting array.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of strings that contains the substrings in the input string that are delimited by the separator.</returns>
    internal static string[] Split(this string value, char separator, StringSplitOptions options)
    {
        return value.Split([separator], options);
    }
}