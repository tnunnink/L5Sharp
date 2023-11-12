using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Elements;

/// <summary>
/// A abstract derivative of a <c>DiagramBlock</c> type that represent blocks contained within a Function Block Diagram (FBD).
/// This type is primarily to constrain the type of blocks that the caller can add to a <c>Sheet</c> diagram type.
/// It also adds some common properties that we want all <c>FunctionBlock</c> derivatives to contain.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class FunctionBlock : DiagramBlock
{
    /// <summary>
    /// Creates a new <see cref="FunctionBlock"/> with default values.
    /// </summary>
    protected FunctionBlock()
    {
    }

    /// <summary>
    /// Creates a new <see cref="FunctionBlock"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    protected FunctionBlock(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override string Location =>
        Sheet is not null ? $"Sheet {Sheet.Number} {Cell} ({X}, {Y})" : $"{Cell} ({X}, {Y})";

    /// <summary>
    /// The parent <see cref="Elements.Sheet"/> element that this <c>FunctionBlock</c> is contained within.
    /// </summary>
    public Sheet? Sheet => Element.Parent is not null ? new Sheet(Element.Parent) : default;

    /// <summary>
    /// Finds all other <see cref="FunctionBlock"/> elements that have connections to this block element.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing connected <see cref="FunctionBlock"/> element objects.</returns>
    /// <remarks>This relies on the parent <see cref="Sheet"/> diagram element to find other connecting blocks.
    /// If this block element is not attached to a <c>Sheet</c> then it will return and empty collection.</remarks>
    public IEnumerable<FunctionBlock> Connections() => Sheet?.Connections(this) ?? Enumerable.Empty<FunctionBlock>();
}