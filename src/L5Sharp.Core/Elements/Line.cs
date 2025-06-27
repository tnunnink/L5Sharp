﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A Logix <c>Line</c> element containing the properties for a L5X Line component.
/// </summary>
public sealed class Line : LogixCode
{
    private NeutralText Text => new(Element.Value);

    /// <summary>
    /// Creates a new <see cref="Line"/> with default values.
    /// </summary>
    public Line() : base(L5XName.Line)
    {
        Element.ReplaceNodes(new XCData(NeutralText.Empty));
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
    /// Creates a new <see cref="Line"/> initialized with the provided <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="text">The <see cref="NeutralText"/> representing the line of structured text logic.</param>
    /// <remarks>This will initialize ...
    /// When importing, Logix ignores the rung number and imports Rung's in order of the container sequence,
    /// meaning, its really only necessary to specify valid text, which is why this constructor is available,
    /// allowing concise construction of a <c>Rung</c> object.</remarks>
    public Line(NeutralText text) : base(L5XName.Line)
    {
        Element.ReplaceNodes(new XCData(text));
    }

    /// <inheritdoc />
    public override IEnumerable<LogixComponent> Dependencies()
    {
        if (L5X is null) return [];

        var dependencies = new List<LogixComponent>();

        foreach (var tagName in Text.Tags())
        {
            if (!L5X.TryGet<Tag>(tagName, out var tag)) continue;
            dependencies.Add(tag);
            dependencies.AddRange(tag.Dependencies());
        }

        foreach (var instruction in Text.Instructions())
        {
            if (!L5X.TryGet<AddOnInstruction>(instruction.Key, out var aoi)) continue;
            dependencies.Add(aoi);
            dependencies.AddRange(aoi.Dependencies());
        }

        return dependencies.Distinct(c => c.Name);
    }

    /// <inheritdoc />
    public override string ToString() => Text;

    /// <summary>
    /// Implicitly converts the <see cref="Rung"/> object to a <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="rung">The <c>Rung</c> to convert.</param>
    /// <returns>A <see cref="NeutralText"/> instance representing the contents of the <c>Rung</c>.</returns>
    public static implicit operator NeutralText(Line rung) => new(rung.Text);

    /// <summary>
    /// Implicitly converts the <see cref="NeutralText"/> object to a <see cref="Rung"/>.
    /// </summary>
    /// <param name="text">The <c>NeutralText</c> to convert.</param>
    /// <returns>A <see cref="Rung"/> instance representing the contents of the <c>NeutralText</c>.</returns>
    public static implicit operator Line(NeutralText text) => new(text);
}