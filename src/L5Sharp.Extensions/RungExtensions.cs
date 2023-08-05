using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;

namespace L5Sharp.Extensions;

/// <summary>
/// A class containing extensions for the <see cref="Rung"/> element.
/// </summary>
public static class RungExtensions
{
    /// <summary>
    /// Gets the parent <see cref="Routine"/> of this <see cref="Rung"/> instance if it is attached.
    /// </summary>
    /// <param name="rung">The current <see cref="Rung"/> object.</param>
    /// <returns>A <see cref="Routine"/> instance representing the containing routine of the rung if found; Otherwise, <c>null</c>.</returns>
    public static Routine? Routine(this Rung rung)
    {
        var routine = rung.Serialize().Ancestors(L5XName.Routine).FirstOrDefault();
        return routine is not null ? new Routine(routine) : default;
    }

    /// <summary>
    /// Returns a flat list of <see cref="NeutralText"/> representing all base and nested AOI logic in the
    /// collection of <see cref="Rung"/> objects.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all the <see cref="NeutralText"/>, including nested instruction
    /// text, found in the rung collection.
    /// </returns>
    /// <remarks>
    /// This extension was specifically created to assist in getting a flat list of logic, including
    /// nested AOI logic, for specialized querying purposes, such as finding tag references within nested logic.
    /// This method will replace the instruction logic parameters with the neutral text operands of the instruction signature,
    /// so to get the effective flattened list of executing <see cref="NeutralText"/> code.
    /// </remarks>
    public static IEnumerable<NeutralText> Flatten(this IEnumerable<Rung> rungs)
    {
        var code = new List<NeutralText>();
        var collection = rungs.ToList();

        var content = collection.FirstOrDefault()?.Content();
        if (content is null)
            throw new InvalidOperationException("Can not flatten rungs that are not attached to a L5X content file.");

        var aoiLookup = content.Instructions.ToDictionary(k => k.Name, v => v);
        var text = collection.Select(r => r.Text);

        foreach (var line in text)
        {
            var references = aoiLookup.SelectMany(l => line.SplitByKey(l.Key)).ToList();

            if (references.Count == 0)
            {
                code.Add(line);
                continue;
            }

            foreach (var logic in from reference in references
                     let key = reference.Keys().FirstOrDefault()
                     let instruction = aoiLookup[key]
                     select instruction.Logic(reference))
            {
                code.AddRange(logic);
            }
        }

        return code;
    }

    /// <summary>
    /// Filters the current collection to text contained in the specified container.
    /// </summary>
    /// <param name="rungs">The collection of <see cref="Rung"/> to filter.</param>
    /// <param name="container">The container name in which to filter the text collection.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Rung"/> filtered to the specific container..</returns>
    /// <remarks><c>container</c> can be either a program, routine, or instruction name. You can also chain calls in any order
    /// to scope the rung collection to a specific combination of program/routine or instruction/routine.</remarks>
    public static IEnumerable<Rung> In(this IEnumerable<Rung> rungs, string container)
    {
        return rungs.Where(r => r.ScopeName() == container);
    }
}