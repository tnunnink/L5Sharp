using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Rung</c> element containing the properties for a L5X Rung component.
/// </summary>
public class Rung : LogixCode
{
    /// <summary>
    /// Creates a new <see cref="Rung"/> with default values.
    /// </summary>
    public Rung()
    {
        Type = RungType.Normal;
        Text = NeutralText.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Rung"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Rung(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Rung"/> initialized with the provided <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="text">The <see cref="NeutralText"/> representing the rung logic.</param>
    /// <param name="comment">The optional string comment of the rung. Default is <c>null</c> (no comment).</param>
    /// <remarks>This will initialize ...
    /// When importing, Logix ignores the rung number and imports Rung's in order of the container sequence,
    /// meaning, its really only necessary to specify valid text, which is why this constructor is available,
    /// allowing concise construction of a <c>Rung</c> object.</remarks>
    public Rung(NeutralText text, string? comment = null)
    {
        Type = RungType.Normal;
        Text = text;
        Comment = comment;
    }

    /// <summary>
    /// The <see cref="RungType"/>, indicating edit state option of the rung.
    /// </summary>
    public RungType? Type
    {
        get => GetValue<RungType>();
        set => SetValue(value);
    }

    /// <summary>
    /// The logic of the <see cref="Rung"/> as a <see cref="NeutralText"/> value.
    /// </summary>
    /// <value>A <see cref="NeutralText"/> instance containing the textual format of the run logic.</value>
    public NeutralText Text
    {
        get => GetProperty<NeutralText>() ?? NeutralText.Empty;
        set => SetProperty(value);
    }

    /// <summary>
    /// The text comment of the <see cref="Rung"/>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text comment of the rung if it exists; Otherwise, <c>null</c>.</value>
    public string? Comment
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }
    
    /// <inheritdoc />
    public override IEnumerable<Instruction> Instructions() => Text.Instructions();

    /// <inheritdoc />
    public override IEnumerable<TagName> TagNames() => Text.Tags();

    /// <inheritdoc />
    public override string ToString() => Text;
    
    /// <summary>
    /// Implicitly converts the <see cref="Rung"/> object to a <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="rung">The <c>Rung</c> to convert.</param>
    /// <returns>A <see cref="NeutralText"/> instance representing the contents of the <c>Rung</c>.</returns>
    public static implicit operator NeutralText(Rung rung) => new(rung.Text);
    
    /// <summary>
    /// Implicitly converts the <see cref="NeutralText"/> object to a <see cref="Rung"/>.
    /// </summary>
    /// <param name="text">The <c>NeutralText</c> to convert.</param>
    /// <returns>A <see cref="Rung"/> instance representing the contents of the <c>NeutralText</c>.</returns>
    public static implicit operator Rung(NeutralText text) => new(text);

    #region Extensions

    /// <summary>
    /// Gets the parent <see cref="Routine"/> of this <see cref="Rung"/> instance if it is attached.
    /// </summary>
    /// <returns>A <see cref="Routine"/> instance representing the containing routine of the rung if found; Otherwise, <c>null</c>.</returns>
    public Routine? GetRoutine()
    {
        var routine = Element.Ancestors(L5XName.Routine).FirstOrDefault();
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
    public IEnumerable<NeutralText> Flatten()
    {
        if (L5X is null)
            throw new InvalidOperationException("Can not flatten rungs that are not attached to a L5X content file.");

        var code = new List<NeutralText>();

        var references = L5X.Instructions
            .Select(i => new {Instruction = i, Instances = Text.SplitByKey(i.Name)})
            .ToList();

        foreach (var reference in references.Where(r => r.Instances.Any()))
        {
            var logic = reference.Instances.SelectMany(i => reference.Instruction.Logic(i));
            code.AddRange(logic);
        }

        return code;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Dictionary<TagName, Tag> References()
    {
        if (L5X is null)
            throw new InvalidOperationException(
                "Can not get references for rungs that are not attached to a L5X content file.");

        var references = new Dictionary<TagName, Tag>();

        foreach (var tagName in Text.Tags())
        {
            var tag = L5X.FindTag(tagName, Program);
            if (tag is null) continue;
            references.Add(tagName, tag);
        }

        return references;
    }

    #endregion
}

/// <summary>
/// Extension methods for a <see cref="Rung"/> element or collections or elements.
/// </summary>
public static class RungExtensions
{
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

        var content = collection.FirstOrDefault()?.L5X;
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
}