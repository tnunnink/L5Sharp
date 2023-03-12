using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions;

/// <summary>
/// A class of helper methods for extracting just the Rll text from the L5X file.
/// </summary>
public static class ContentExtensions
{
    /// <summary>
    /// Gets all <see cref="NeutralText"/> instances in the current L5X file.
    /// </summary>
    /// <param name="content">The current <see cref="LogixContent"/> instance.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> instance in the L5X file.</returns>
    public static IEnumerable<NeutralText> Logic(this LogixContent content) =>
        content.L5X.Descendants(L5XName.Rung).Select(r =>
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
    public static IEnumerable<NeutralText> Logic(this LogixContent content, Scope scope, string scopeName)
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
    public static IEnumerable<NeutralText> Logic(this LogixContent content, string program, string routine) =>
        content.L5X.Descendants(L5XName.Rung)
            .Where(t => t.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() == program
                        && t.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() == routine)
            .Select(r =>
            {
                var text = r.Element(L5XName.Text)?.Value;
                return text is not null ? new NeutralText(text) : NeutralText.Empty;
            });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <param name="tagName"></param>
    /// <returns></returns>
    public static IEnumerable<NeutralText> Logic(this LogixContent content, TagName tagName) =>
        content.L5X.Descendants(L5XName.Rung).Select(r => r.Element(L5XName.Text) is not null
                ? new NeutralText(r.Element(L5XName.Text)!.Value)
                : NeutralText.Empty)
            .Where(t => t.ContainsKey(tagName));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Dictionary<TagName, List<NeutralText>> LookupTags(this LogixContent content)
    {
        var references = content.L5X.Descendants(L5XName.Rung)
            .Select(r => r.Element(L5XName.Text) is not null
                ? new NeutralText(r.Element(L5XName.Text)!.Value)
                : NeutralText.Empty)
            .SelectMany(t => t.References());

        var results = new Dictionary<TagName, List<NeutralText>>();

        foreach (var reference in references)
        {
            if (!results.ContainsKey(reference.Key))
                results.Add(reference.Key, new List<NeutralText> { reference.Value });
            
            results[reference.Key].Add(reference.Value);
        }

        return results;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Dictionary<string, List<NeutralText>> LookupInstructions(this LogixContent content)
    {
        var references = content.L5X.Descendants(L5XName.Rung)
            .Select(r => r.Element(L5XName.Text) is not null
                ? new NeutralText(r.Element(L5XName.Text)!.Value)
                : NeutralText.Empty)
            .SelectMany(t => t.Split());

        var results = new Dictionary<string, List<NeutralText>>();

        foreach (var reference in references)
        {
            var key = reference.Keys().FirstOrDefault();
            if (key is null) continue;

            if (!results.ContainsKey(key))
                results.Add(key, new List<NeutralText> { reference });
            
            results[key].Add(reference);
        }

        return results;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Dictionary<string, List<TagName>> LookupDataTypes(this LogixContent content)
    {
        var tags = content.Query<Tag>().SelectMany(t => t.Members());
        
        var results = new Dictionary<string, List<TagName>>();

        foreach (var tag in tags)
        {
            if (!results.ContainsKey(tag.DataType))
                results.Add(tag.DataType, new List<TagName> { tag.TagName });
            
            results[tag.DataType].Add(tag.TagName);
        }

        return results;
    }
}