using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A Logix <c>Line</c> element containing the properties for a L5X Line component.
/// </summary>
public sealed class Line : LogixCode
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
    public override string ToString() => Element.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rung"></param>
    /// <returns></returns>
    public static implicit operator string(Line rung) => rung.Element.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static implicit operator Line(string text) => new(text);
}