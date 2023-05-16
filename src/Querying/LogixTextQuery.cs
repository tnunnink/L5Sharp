using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Querying;

/// <summary>
/// An extension interface for simplifying queries of <see cref="NeutralText"/> within a L5X file.
/// </summary>
public class LogixTextQuery : IEnumerable<NeutralText>
{
    private readonly LogixContent _content;
    private IEnumerable<XElement> _elements;

    internal LogixTextQuery(LogixContent content)
    {
        _content = content;
        _elements = _content.L5X.Descendants(L5XName.Text).Where(e => e.Ancestors(L5XName.Program).Any());
    }

    /// <summary>
    /// Returns a flat list of <see cref="NeutralText"/> with all AOI references replaced with underlying logic with
    /// the text arguments in place of the parameter names of the AOI logic. 
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all the <see cref="NeutralText"/>, including nested instruction
    /// text, found in the text collection.
    /// </returns>
    /// <remarks>
    /// This extension was specifically created to assist in getting a flat list of logic, including
    /// nested AOI logic, for specialized querying purposes, such as finding tag references within nested logic.
    /// This method will replace the instruction logic parameters with the neutral text operands of the instruction signature.
    /// </remarks>
    public IEnumerable<NeutralText> Flatten()
    {
        var aoiLookup = _content.Instructions.ToDictionary(k => k.Name, v => v);

        var code = new List<NeutralText>();

        foreach (var text in this)
        {
            var references = aoiLookup.SelectMany(l => text.SplitByKey(l.Key)).ToList();

            if (references.Count == 0)
            {
                code.Add(text);
                continue;
            }

            foreach (var reference in references)
            {
                var key = reference.Keys().FirstOrDefault();
                var instruction = aoiLookup[key];
                var logic = instruction.Logic(reference);
                code.AddRange(logic);
            }
        }

        return code;
    }

    /// <summary>
    /// Filters the current collection to text contained in the specified container.
    /// </summary>
    /// <param name="container">The container name (program/routine/instruction) in which to filter the text collection.</param>
    /// <returns>The <see cref="LogixTextQuery"/> filtered to the specific container.</returns>
    public LogixTextQuery In(string container)
    {
        _elements = _elements.Where(e => e.Ancestors().Any(a => a.LogixName() == container));
        return this;
    }

    /// <summary>
    /// Filters the current collection to text contained in the specified program and routine.
    /// </summary>
    /// <param name="program">The program name in which to filter the text collection.</param>
    /// <param name="routine">The routine name in which to filter the text collection.</param>
    /// <returns>The <see cref="LogixTextQuery"/> filtered to the specific program and routine.</returns>
    public LogixTextQuery In(string program, string routine)
    {
        _elements = _elements
            .Where(e => e.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() == routine
                        && e.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() == program);
        return this;
    }

    /// <summary>
    /// Filters the current collection to text contained in the specified routine type.
    /// </summary>
    /// <param name="routineType">The <see cref="RoutineType"/> (RLL/ST) in which to filter the text collection.</param>
    /// <returns>The <see cref="LogixTextQuery"/> filtered to the specific routine type.</returns>
    public LogixTextQuery In(RoutineType routineType)
    {
        _elements = _elements.Where(e =>
            e.Ancestors(L5XName.Routine).FirstOrDefault()?.GetValue<RoutineType>(L5XName.Type)! == routineType);
        return this;
    }

    /// <summary>
    /// Filters the current collection to text containing the provided <see cref="TagName"/>.
    /// </summary>
    /// <param name="tagName">The tag name to search for.</param>
    /// <returns>The <see cref="LogixTextQuery"/> filtered to the specific tag name.</returns>
    public LogixTextQuery With(TagName tagName)
    {
        _elements = _elements.Where(e => e.Value.Contains(tagName));
        return this;
    }
    
    /// <summary>
    /// Filters the current collection to text containing the provided <see cref="TagName"/>.
    /// </summary>
    /// <param name="instruction">The tag name to search for.</param>
    /// <returns>The <see cref="LogixTextQuery"/> filtered to the specific tag name.</returns>
    public LogixTextQuery With(Instruction instruction)
    {
        _elements = _elements.Where(e => Regex.IsMatch(instruction.Signature, e.Value));
        return this;
    }

    /// <inheritdoc />
    public IEnumerator<NeutralText> GetEnumerator() =>
        _elements.Select(e => new NeutralText(e.Value)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}