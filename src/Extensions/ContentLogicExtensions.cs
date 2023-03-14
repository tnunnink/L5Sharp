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
public static class ContentLogicExtensions
{
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

        var text = content.Logic().ToList();

        for (var i = 0; i < text.Count; i++)
        {
            var index = i;

            var references = aoiLookup.SelectMany(l => text[index].SplitByKey(l.Key)).ToList();

            if (references.Count == 0)
                continue;

            text.RemoveAt(i);

            foreach (var reference in references)
            {
                var key = reference.Keys().FirstOrDefault();
                var instruction = aoiLookup[key];
                var logic = instruction.GetLogic(reference);
                text.InsertRange(i, logic);
            }
        }

        return text;
    }
}