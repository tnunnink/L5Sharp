using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions;

/// <summary>
/// Container for all public extensions methods that add functionality to the base components of the library.
/// </summary>
public static class LogixExtensions
{
    /// <summary>
    /// Performs a deep clone of the current logix component and returns a new instance with same values.
    /// </summary>
    /// <param name="component">The component object to clone.</param>
    /// <typeparam name="TComponent">The logix component type.</typeparam>
    /// <returns>A new <see cref="ILogixComponent"/> of the specified type with same property values.</returns>
    /// <remarks>All this extension does is serialize the component and then deserializes it back as a new object.</remarks>
    public static TComponent Clone<TComponent>(this TComponent component) where TComponent : ILogixComponent
    {
        var element = LogixSerializer.Serialize(component);
        return LogixSerializer.Deserialize<TComponent>(element);
    }

    /// <summary>
    /// Performs a explicit cast of the current <see cref="ILogixType"/> to the type of the generic argument.
    /// </summary>
    /// <param name="logixType">The current logix type to cast</param>
    /// <typeparam name="TLogixType">The logix type to cast to.</typeparam>
    /// <returns>The instance casted as the specified generic type argument.</returns>
    /// <exception cref="InvalidCastException">The current type is not compatible with specified generic argument type.</exception>
    public static TLogixType To<TLogixType>(this ILogixType logixType) where TLogixType : ILogixType =>
        (TLogixType)logixType;

    /// <summary>
    /// Performs a safe cast of the current <see cref="ILogixType"/> to the type of the generic argument.
    /// </summary>
    /// <param name="logixType">The current logix type to cast</param>
    /// <typeparam name="TLogixType">The logix type to cast to.</typeparam>
    /// <returns>The instance casted as the specified generic type argument.</returns>
    public static TLogixType? As<TLogixType>(this ILogixType logixType) where TLogixType : class, ILogixType =>
        logixType as TLogixType;

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

    /// <summary>
    /// Gets a lookup of all <see cref="ILogixTag"/> within the current <see cref="LogixContent"/> file.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <returns>A <see cref="ILookup{TKey,TValue}"/> of all tag names and their corresponding
    /// <see cref="ILogixTag"/> instance in the L5X file.</returns>
    /// <remarks>This is helper to get a tag lookup for fast access to finding tags within the L5X file. Note that some
    /// tags may have multiple <see cref="ILogixTag"/> instance if they are scoped (program) tags with the same tag name.</remarks>
    public static ILookup<TagName, ILogixTag> TagLookup(this LogixContent content) =>
        content.Query<Tag>().SelectMany(t => t.MembersAndSelf()).ToLookup(t => t.TagName, t => t);

    /// <summary>
    /// Gets all <see cref="NeutralText"/> instances in the current L5X file.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> instance in the L5X file.</returns>
    public static IEnumerable<NeutralText> Logic(this LogixContent content) =>
        content.L5X.Descendants(L5XName.Rung)
            .Where(t => t.Ancestors(L5XName.Program).Any())
            .Select(r =>
            {
                var text = r.Element(L5XName.Text)?.Value;
                return text is not null ? new NeutralText(text) : NeutralText.Empty;
            });

    /// <summary>
    /// Gets all <see cref="NeutralText"/> instances in a specific scope of the L5X file.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <param name="scope">The scope for which to search (program, routine, controller).</param>
    /// <param name="scopeName">The name of the scope (program or routine).</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> instance in specified scope of
    /// the L5X file.</returns>
    /// <exception cref="ArgumentNullException"><c>scope</c> or <c>scopeName</c> is null.</exception>
    public static IEnumerable<NeutralText> LogicIn(this LogixContent content, Scope scope, string scopeName)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        if (scopeName is null)
            throw new ArgumentNullException(nameof(scopeName));

        if (scope == Scope.Program)
        {
            return content.L5X.Descendants(L5XName.Rung)
                .Where(t => t.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() == scopeName)
                .Select(r =>
                {
                    var text = r.Element(L5XName.Text)?.Value;
                    return text is not null ? new NeutralText(text) : NeutralText.Empty;
                });
        }

        if (scope == Scope.Routine)
        {
            return content.L5X.Descendants(L5XName.Rung)
                .Where(t => t.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() == scopeName)
                .Select(r =>
                {
                    var text = r.Element(L5XName.Text)?.Value;
                    return text is not null ? new NeutralText(text) : NeutralText.Empty;
                });
        }

        return content.Logic();
    }

    /// <summary>
    /// Gets all <see cref="NeutralText"/> instances in a specific program and routine of the L5X file.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <param name="program">The program to search in.</param>
    /// <param name="routine">The routine to search in.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> instance in specified scope of
    /// the L5X file.</returns>
    public static IEnumerable<NeutralText> LogicIn(this LogixContent content, string program, string routine) =>
        content.L5X.Descendants(L5XName.Rung)
            .Where(t => t.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() == program
                        && t.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() == routine)
            .Select(r =>
            {
                var text = r.Element(L5XName.Text)?.Value;
                return text is not null ? new NeutralText(text) : NeutralText.Empty;
            });

    /// <summary>
    /// Gets all <see cref="NeutralText"/> instance with a reference to the provided <see cref="TagName"/> value.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all the <see cref="NeutralText"/> references found.</returns>
    public static IEnumerable<NeutralText> LogicWith(this LogixContent content, TagName tagName) =>
        content.L5X.Descendants(L5XName.Rung).Select(r => r.Element(L5XName.Text) is not null
                ? new NeutralText(r.Element(L5XName.Text)!.Value)
                : NeutralText.Empty)
            .Where(t => t.ContainsKey(tagName));

    /// <summary>
    /// Gets all <see cref="NeutralText"/> found in the L5X with each AOI text reference replaced with it's underlying instruction logic.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all the <see cref="NeutralText"/>, including nested instruction
    /// text, found in the L5X file.
    /// </returns>
    /// <remarks>
    /// This extension was specifically created to assist in getting a flat list of logic, including
    /// nested AOI logic, for specialized querying purposes, such as finding tag references within nested logic.
    /// This method will replace the instruction logic parameters with the neutral text operands of the instruction signature.
    /// </remarks>
    public static IEnumerable<NeutralText> LogicFlatten(this LogixContent content)
    {
        var aoiLookup = content.Instructions().ToDictionary(k => k.Name, v => v);

        var text = content.Query<Rung>().Select(r => r.Text).ToList();

        for (var i = 0; i < text.Count; i++)
        {
            var index = i;

            var references = aoiLookup.SelectMany(l => text[index].SplitByKey(l.Key)).ToList();

            if (references.Count == 0)
                continue;

            //Bug This removes the entire line which is not necessarily correct? May be problematic
            text.RemoveAt(i);

            foreach (var reference in references)
            {
                var key = reference.Keys().FirstOrDefault();
                var instruction = aoiLookup[key];
                var logic = instruction.Logic(reference);
                text.InsertRange(i, logic);
            }
        }

        return text;
    }
}