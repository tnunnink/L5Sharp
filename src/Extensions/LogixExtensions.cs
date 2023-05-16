using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Querying;
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

    /// <summary>
    /// Gets a lookup of all <see cref="ILogixTag"/> within the current <see cref="LogixContent"/> file.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <returns>A <see cref="ILookup{TKey,TValue}"/> of all tag names and their corresponding
    /// <see cref="ILogixTag"/> instance in the L5X file.</returns>
    /// <remarks>This is helper to get a tag lookup for fast access to finding tags within the L5X file. Note that some
    /// tags may have multiple <see cref="ILogixTag"/> instance if they are scoped (program) tags with the same tag name.</remarks>
    public static ILookup<TagName, ILogixTag> TagLookup(this LogixContent content) =>
        content.Find<Tag>().SelectMany(t => t.MembersAndSelf()).ToLookup(t => t.TagName, t => t);
}