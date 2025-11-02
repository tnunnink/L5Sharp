using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A Logix <c>Rung</c> element containing the properties for a L5X Rung component.
/// </summary>
public class Rung : LogixCode
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Comment,
        L5XName.Text
    ];

    /// <summary>
    /// Creates a new <see cref="Rung"/> with default values.
    /// </summary>
    public Rung() : base(L5XName.Rung)
    {
        Element.Add(new XAttribute(L5XName.Number, 0));
        Type = RungType.Normal;
        Text = string.Empty;
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
    /// Creates a new <see cref="Rung"/> initialized with the provided neutral text code and optional comment.
    /// </summary>
    /// <param name="text">The neutral text that represents the rung code/content.</param>
    /// <param name="comment">The optional command to configure the rung with.</param>
    public Rung(string text, string? comment = null) : this()
    {
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
    /// Gets or sets the text content of the Rung element.
    /// Represents the primary logic or code defined within the Rung component in a Logix project.
    /// </summary>
    /// <remarks>
    /// The <c>Text</c> property is used to store and manipulate the textual representation of the Rung logic.
    /// This property will return an empty string if no value has been explicitly set.
    /// </remarks>
    public string Text
    {
        get => GetProperty<string>() ?? string.Empty;
        set => SetProperty(value);
    }

    /// <summary>
    /// Gets or sets the comment associated with the rung. This property represents
    /// an optional textual description or note that provides additional context
    /// or clarification for the rung within the Logix program.
    /// </summary>
    public string? Comment
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    /// <inheritdoc />
    public override IEnumerable<Instruction> Instructions()
    {
        return Instruction.Split(Text);
    }

    /// <inheritdoc />
    public override IEnumerable<TagName> Tags()
    {
        return Instruction.Split(Text).SelectMany(x => x.Tags);
    }

    /// <summary>
    /// Returns a flat list of <see cref="Rung"/> representing all base and nested AOI logic in the
    /// current rung instance.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all the <see cref="Rung"/>, including nested instruction
    /// text, found in the rung collection.
    /// </returns>
    /// <remarks>
    /// This extension was specifically created to assist in getting a flat list of logic, including
    /// nested AOI logic, for specialized querying purposes, such as finding tag references within nested logic.
    /// This method will replace the instruction logic parameters with the neutral text operands of the instruction signature,
    /// so to get the effective flattened list of executing <see cref="Rung"/> code.
    /// </remarks>
    public IEnumerable<Rung> Flatten()
    {
        if (Document is null)
            throw new InvalidOperationException("Can not flatten rungs that are not attached to a L5X content file.");

        var code = new List<Rung>();

        foreach (var instruction in Instructions())
        {
            if (instruction.IsNative) continue;
            if (!TryResolve<AddOnInstruction>(instruction.Key, out var aoi)) continue;
            var logic = aoi.LogicFor(instruction);
            code.AddRange(logic);
        }

        return code;
    }

    /// <summary>
    /// Allows implicit conversion of a <see cref="Rung"/> object to a string representation of its <see cref="Text"/> property.
    /// </summary>
    /// <param name="rung">The <see cref="Rung"/> instance to convert.</param>
    /// <returns>A string containing the value of the <see cref="Rung.Text"/> property.</returns>
    public static implicit operator string(Rung rung) => rung.Text;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static implicit operator Rung(string text) => new(text);
}

/// <summary>
/// Extension methods for a <see cref="Rung"/> element or collections or elements.
/// </summary>
public static class RungExtensions
{
    /*/// <summary>
    /// Returns a flat list of <see cref="NeutralText"/> representing all base and nested AoiBlock logic in the
    /// collection of <see cref="Rung"/> objects.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all the <see cref="NeutralText"/>, including nested instruction
    /// text, found in the rung collection.
    /// </returns>
    /// <remarks>
    /// This extension was specifically created to assist in getting a flat list of logic, including
    /// nested AoiBlock logic, for specialized querying purposes, such as finding tag references within nested logic.
    /// This method will replace the instruction logic parameters with the neutral text operands of the instruction signature,
    /// so to get the effective flattened list of executing <see cref="NeutralText"/> code.
    /// </remarks>
    public static IEnumerable<NeutralText> Flatten(this IEnumerable<Rung> rungs)
    {
        var code = new List<NeutralText>();
        var collection = rungs.ToList();

        var content = collection.FirstOrDefault()?.Document;
        if (content is null)
            throw new InvalidOperationException("Can not flatten rungs that are not attached to a L5X content file.");

        var lookup = content.Instructions.ToDictionary(k => k.Name, v => v);
        var text = collection.Select(r => r.Text);

        foreach (var line in text)
        {
            var instructions = lookup.SelectMany(l => line.Instructions(l.Key)).ToList();

            if (instructions.Count == 0)
            {
                code.Add(line);
                continue;
            }

            foreach (var logic in from instruction in instructions
                     let definition = lookup[instruction.Key]
                     select definition.LogicFor(instruction))
            {
                code.AddRange(logic);
            }
        }

        return code;
    }*/
}