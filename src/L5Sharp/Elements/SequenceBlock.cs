using System;
using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramBlock</c> type that defines the properties for nested function block elements in a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Block</c> represents a function block within the FBD. These are blocks that represent
/// specific logix built-in instructions as opposed to AOIs. A <c>Block</c> differs from a <c>Function</c>
/// in that it requires a backing tag to operate over, whereas a <c>Function</c> represents a simple logic gate or
/// operation that takes inputs and produces an output without the need for a backing tag.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class SequenceBlock : DiagramBlock
{
    /// <summary>
    /// Creates a new <see cref="SequenceBlock"/> with default values.
    /// </summary>
    protected SequenceBlock()
    {
    }

    /// <summary>
    /// Creates a new <see cref="FunctionBlock"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    protected SequenceBlock(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The parent <see cref="Elements.Sheet"/> element that this <c>FunctionBlock</c> is contained within.
    /// </summary>
    public Chart? Chart => Element.Parent is not null ? new Chart(Element.Parent) : default;
}