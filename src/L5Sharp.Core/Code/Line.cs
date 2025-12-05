using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A Logix <c>Line</c> element containing the properties for a L5X Line component.
/// </summary>
[LogixElement(L5XName.Line)]
public sealed class Line : LogixCode<Line>
{
    /// <summary>
    /// Creates a new <see cref="Line"/> with default values.
    /// </summary>
    public Line() : base(L5XName.Line)
    {
        Element.ReplaceNodes(new XCData(string.Empty));
    }

    /// <summary>
    /// Creates a new <see cref="Line"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Line(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Line"/> initialized with the provided neutral text code.
    /// </summary>
    public Line(string text) : base(L5XName.Line)
    {
        Element.ReplaceNodes(new XCData(text));
    }

    /// <inheritdoc />
    public override IEnumerable<Instruction> Instructions()
    {
        return Instruction.Split(Element.Value);
    }

    /// <inheritdoc />
    public override IEnumerable<TagName> Tags()
    {
        return Instruction.Split(Element.Value).SelectMany(x => x.Tags);
    }

    /// <inheritdoc />
    public override IEnumerable<Reference> Usages()
    {
        return Instructions().Select(x => Reference.ToLogic(x));
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

    /// <inheritdoc />
    public override string ToString() => Element.Value;

    /// <summary>
    /// Defines an implicit conversion operator from a <see cref="Line"/> instance to its corresponding <see cref="string"/> representation.
    /// </summary>
    /// <param name="rung">The <see cref="Line"/> instance to be converted.</param>
    /// <returns>The string value of the <see cref="Line"/> element.</returns>
    public static implicit operator string(Line rung) => rung.Element.Value;

    /// <summary>
    /// Defines an implicit conversion from <see langword="string"/> to <see cref="Line"/>.
    /// </summary>
    /// <param name="text">The string value to be converted into a <see cref="Line"/> instance.</param>
    /// <returns>A new <see cref="Line"/> instance initialized with the provided string value.</returns>
    public static implicit operator Line(string text) => new(text);
}