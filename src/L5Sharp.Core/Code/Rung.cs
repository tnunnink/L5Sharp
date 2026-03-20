using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A Logix <c>Rung</c> element containing the properties for a L5X Rung component.
/// </summary>
[LogixElement(L5XName.Rung)]
public class Rung : LogixCode<Rung>
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
        Element.Add(new XAttribute(L5XName.Type, RungType.Normal.Value));
        Text = ";";
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
    /// The <see cref="RungType"/>, indicating an edit state option of the rung.
    /// </summary>
    public RungType? Type => GetValue(RungType.Parse);

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
        get => GetProperty() ?? string.Empty;
        set => SetProperty(value);
    }

    /// <summary>
    /// Gets or sets the comment associated with the rung. This property represents
    /// an optional textual description or note that provides additional context
    /// or clarification for the rung within the Logix program.
    /// </summary>
    public string? Comment
    {
        get => GetProperty();
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

    /// <inheritdoc />
    public override IEnumerable<ILogixEntity> Dependencies()
    {
        var dependencies = new List<ILogixEntity>();

        foreach (var tagName in Tags())
        {
            if (!TryResolve<Tag>(tagName, out var tag)) continue;
            dependencies.Add(tag);
            dependencies.AddRange(tag.Dependencies());
        }

        foreach (var instruction in Instructions())
        {
            if (!TryResolve<AddOnInstruction>(instruction.Key, out var aoi)) continue;
            dependencies.Add(aoi);
            dependencies.AddRange(aoi.Dependencies());
        }

        return dependencies.Distinct(c => c.Reference);
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