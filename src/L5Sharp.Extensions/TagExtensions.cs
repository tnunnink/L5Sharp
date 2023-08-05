using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Extensions;

/// <summary>
/// A class containing extensions for the <see cref="Tag"/> component.
/// </summary>
public static class TagExtensions
{
    /// <summary>
    /// Returns a collection of all descendent tag names of the current <c>Tag</c>, including the tag name of the
    /// this <c>Tag</c>. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> containing the this tag name and all child tag names.
    /// </returns>
    public static IEnumerable<TagName> TagNames(this Tag tag) => tag.Members().Select(t => t.TagName);

    /// <summary>
    /// Adds a new member to the tag's complex data structure.
    /// </summary>
    /// <param name="tag">The tag to add a member to.</param>
    /// <param name="name">The name of the member to add to the tag's data structure.</param>
    /// <param name="value">The <see cref="LogixType"/> of the member to add to the tag's data structure.</param>
    /// <exception cref="InvalidOperationException">The current tag does not contain a mutable complex logix type.</exception>
    /// <remarks>
    /// This will operate relative to the current tag member object, and is simply a call to the underlying
    /// <see cref="ComplexType"/> <c>Add</c> method. Therefore this is simply a helper to make mutating tag structures
    /// more concise.
    /// </remarks>
    public static void Add(this Tag tag, string name, LogixType value)
    {
        var member = new LogixMember(name, value);
        if (tag.Value is not ComplexType complexType)
            throw new InvalidOperationException("Can only mutate ComplexType tags.");
        complexType.Add(member);
    }

    /// <summary>
    /// Removes a member with the specified name from the tag's complex data structure.
    /// </summary>
    /// <param name="tag">The tag to remove a member from.</param>
    /// <param name="name">The name of the member to remove.</param>
    /// <exception cref="InvalidOperationException">The current tag does not contain a mutable complex logix type.</exception>
    /// <remarks>
    /// This will operate relative to the current tag member object, and is simply a call to the underlying
    /// <see cref="ComplexType"/> <c>Remove</c> method. Therefore this is simply a helper to make mutating tag structures
    /// more concise.
    /// </remarks>
    public static void Remove(this Tag tag, string name)
    {
        if (tag.Value is not ComplexType complexType)
            throw new InvalidOperationException("Can only mutate ComplexType tags.");
        complexType.Remove(name);
    }
    
    /// <summary>
    /// Finds all tags with the provided tag name using the specified tag name equality comparer. 
    /// </summary>
    /// <param name="tags">The collection of tags to search.</param>
    /// <param name="tagName">The <see cref="TagName"/> value to search for</param>
    /// <param name="comparer">The equality comparer to use for tag name comparison.
    /// Will default to <see cref="TagNameComparer.Qualified"/> or full tag name comparison.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects with the specified tag name value.</returns>
    /// <remarks>This is a helper to more concisely find nested tag objects for a given tag collection.</remarks>
    public static IEnumerable<Tag> Find(this IEnumerable<Tag> tags, TagName tagName,
        IEqualityComparer<TagName>? comparer = null)
    {
        comparer ??= TagNameComparer.Qualified;
        return tags.SelectMany(t => t.Members()).Where(t => comparer.Equals(t.TagName, tagName));
    }

    /// <summary>
    /// Returns all <see cref="Tag"/> objects in the entire L5X, including controller, program, AOI, and module tags.
    /// </summary>
    /// <param name="content">The <see cref="LogixContent"/> representing the L5X file to query.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all <see cref="Tag"/> objects in the L5X.</returns>
    /// <remarks>
    /// Tags are scattered throughout the L5X file. The API of this library sort of reflects that with all the
    /// different places to look for tag objects (controller, programs, AOIs, Modules).
    /// This extension is here to support a single interface through which to retrieve a flat list of all tags in the L5X
    /// regardless of container and element name. This method supports controller, program, AOI, and module config, input,
    /// and outputs tags.
    /// </remarks>
    public static IEnumerable<Tag> Tags(this LogixContent content)
    {
        var supportedNames = new List<string>
        {
            L5XName.Tag,
            L5XName.LocalTag,
            L5XName.ConfigTag,
            L5XName.InputTag,
            L5XName.OutputTag
        };

        return content.L5X.Descendants().Where(e => supportedNames.Any(n => n == e.Name)).Select(e => new Tag(e));
    }
    
    /// <summary>
    /// Returns a filtered collection of <see cref="Tag"/> that are contained in the specified program name.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects.</param>
    /// <param name="programName">The program name to filter the tags for.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing tags in the specified program.</returns>
    public static IEnumerable<Tag> In(this IEnumerable<Tag> tags, string programName)
    {
        return tags.Where(t => t.ScopeName() == programName);
    }

    /// <summary>
    /// Returns a filtered collection of <see cref="Tag"/> that have the specified <see cref="Enums.Scope"/> value.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects.</param>
    /// <param name="scope">The <see cref="Enums.Scope"/> to filter the tags for.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing tags in the specified program.</returns>
    public static IEnumerable<Tag> In(this IEnumerable<Tag> tags, Scope scope)
    {
        return tags.Where(t => t.Scope() == scope);
    }
}