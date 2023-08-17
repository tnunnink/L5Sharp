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
    /// Gets the name of the scoped container for the current logix element.
    /// </summary>
    /// <value> A <see cref="string"/> containing the name of the containing program or controller for which this tag
    /// belongs. If this tag has not container (i.e. <c>Null</c> scope), then returns <c>null</c>.</value>
    /// <remarks>
    /// This value is retrieved from the ancestors of the underlying element. If not ancestors exists, meaning this
    /// tag has no scope, then this property will be <c>null</c>.
    /// </remarks>
    /// <seealso cref="ScopeName"/>
    public static string? ContainerName(this Tag tag) =>
        tag.Serialize().Ancestors(tag.Scope.Name).FirstOrDefault()?.LogixName();

    /// <summary>
    /// Returns a collection of all descendent tag names of the current <c>Tag</c>, including the tag name of the
    /// this <c>Tag</c>. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> containing the this tag name and all child tag names.
    /// </returns>
    public static IEnumerable<TagName> TagNames(this Tag tag) => tag.Members().Select(t => t.TagName);

    /// <summary>
    /// Returns the <c>TagName</c> and the tag's program name prepended with the format <i>Program:{ProgramName}.{TagName}</i>
    /// if the tag is a program tag. If the tag is a controller tag, then returns the tag's <c>TagName</c>. This forms a
    /// globally unique tag identifier for the given tag object.
    /// </summary>
    /// <value>
    /// If this tag is a controller tag, then <see cref="TagName"/>. If this tag is a program tag,
    /// then <see cref="ContainerName"/> and <see cref="Common.TagName"/> in the format <i>Program:{ProgramName}.{TagName}</i>.
    /// </value>
    /// <remarks>
    /// This value is determined based on the <see cref="Scope"/> of the tag. This name provides a consistent
    /// global unique identified for a given tag object.
    /// </remarks>
    /// <seealso cref="ContainerName"/>
    public static TagName ScopeName(this Tag tag) =>
        tag.Scope == Scope.Program ? $"Program:{ContainerName(tag)}.{tag.TagName}" : tag.TagName;

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
    /// Returns all <see cref="Tag"/> objects in the entire L5X, including controller, program, AOI, and module tags.
    /// </summary>
    /// <param name="content">The <see cref="LogixContent"/> representing the L5X file to query.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all <see cref="Tag"/> objects in the L5X.</returns>
    /// <remarks>
    /// Tags are scattered throughout the L5X file. The API of this library sort of reflects that with all the
    /// different places to look for tag objects (controller, programs, modules).
    /// This extension is here to support a single interface through which to retrieve a flat list of all tags in the L5X
    /// regardless of container and element name. This method supports controller, program, AOI, and module config, input,
    /// and outputs tags.
    /// </remarks>
    public static TagDictionary Tags(this LogixContent content) => new(content);

    /// <summary>
    /// Returns a filtered collection of <see cref="Tag"/> that are contained in the specified program name.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects.</param>
    /// <param name="programName">The program name to filter the tags for.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing tags in the specified program.</returns>
    public static IEnumerable<Tag> In(this IEnumerable<Tag> tags, string programName) =>
        tags.Where(t => t.ContainerName() == programName);

    /// <summary>
    /// Returns a filtered collection of <see cref="Tag"/> that have the specified <see cref="Enums.Scope"/> value.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects.</param>
    /// <param name="scope">The <see cref="Enums.Scope"/> to filter the tags for.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing tags in the specified program.</returns>
    public static IEnumerable<Tag> In(this IEnumerable<Tag> tags, Scope scope) => tags.Where(t => t.Scope == scope);
}