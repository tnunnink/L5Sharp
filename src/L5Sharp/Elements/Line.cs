using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Line</c> element containing the properties for a L5X Line component.
/// </summary>
public sealed class Line : LogixCode
{
    private NeutralText Text => new(Element.Value);
    
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
    public override IEnumerable<CrossReference> References()
    {
        var references = new List<CrossReference>();

        references.AddRange(Text.Tags()
            .Select(name => new CrossReference(Element, name, L5XName.Tag)));

        references.AddRange(Text.Instructions()
            .Select(instruction => new CrossReference(Element, instruction.Key, L5XName.AddOnInstructionDefinition)));

        //todo routines? Have to look for JSR and SBR, RET
        //todo modules? Have to look for tag names with ':'
        
        return references;
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