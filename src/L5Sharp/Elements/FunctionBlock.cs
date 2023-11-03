using System;
using System.Xml.Linq;

namespace L5Sharp.Elements;

/// <summary>
/// A abstract derivative of a <c>DiagramBlock</c> type that represent blocks contained within a Function Block Diagram (FBD).
/// This type is primarily to constrain the type of blocks that the caller can add to a <c>Sheet</c> diagram type.
/// It also add some common properties that we want all <c>FunctionBlock</c> derivatives to contain.
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
}