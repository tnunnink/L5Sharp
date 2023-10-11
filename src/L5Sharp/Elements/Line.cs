using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Line</c> element containing the properties for a L5X Line component.
/// </summary>
public class Line : LogixCode
{
    /// <summary>
    /// Creates a new <see cref="Line"/> with default values.
    /// </summary>
    public Line()
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
    public Line(NeutralText text)
    {
        Element.ReplaceNodes(new XCData(text));
    }
    
    /// <inheritdoc />
    public override IEnumerable<Instruction> Instructions() => ((NeutralText)this).Instructions();
    
    /// <inheritdoc />
    public override IEnumerable<TagName> TagNames() => ((NeutralText)this).Tags();

    /// <inheritdoc />
    public override string ToString() => Element.Value;
    
    /// <summary>
    /// Implicitly converts the <see cref="Rung"/> object to a <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="rung">The <c>Rung</c> to convert.</param>
    /// <returns>A <see cref="NeutralText"/> instance representing the contents of the <c>Rung</c>.</returns>
    public static implicit operator NeutralText(Line rung) => new(rung.ToString());
    
    /// <summary>
    /// Implicitly converts the <see cref="NeutralText"/> object to a <see cref="Rung"/>.
    /// </summary>
    /// <param name="text">The <c>NeutralText</c> to convert.</param>
    /// <returns>A <see cref="Rung"/> instance representing the contents of the <c>NeutralText</c>.</returns>
    public static implicit operator Line(NeutralText text) => new(text);
}