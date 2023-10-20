using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramElement</c> type that defines the properties for nested function block elements in a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>DiagramBlock</c> or Block represents a function block within the FBD. These are blocks that represent
/// specific logix built-in instructions as opposed to AOIs. A <c>DiagramBlock</c> differs from a <c>DiagramFunction</c>
/// in that it requires a backing tag to operate over, whereas a <c>DiagramFunction</c> represents a simple logic gate or
/// operation that takes inputs and produces an output without the need for a backing tag.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Block, L5XName.Sheet)]
public class DiagramBlock : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramBlock"/> with default values.
    /// </summary>
    public DiagramBlock()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramBlock"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DiagramBlock(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The mnemonic name specifying the type of function for the <c>DiagramBlock</c> instance.
    /// </summary>
    /// <value>A <see cref="string"/> containing the type of the function if it exists; Otherwise, <c>null</c>.</value>
    public string? Type
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The backing tag name for the <c>DiagramBlock</c> instance.
    /// </summary>
    /// <value>A <see cref="string"/> containing the tag name if it exists; Otherwise, <c>null</c>.</value>
    public string? Operand
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// A collection of pin names that are visible for the <c>DiagramBlock</c>.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing the names of the pins if found. If not found then an
    /// empty collection.</value>
    /// <remarks>To update the property, you must assign a new collection of pin names.</remarks>
    public IEnumerable<string> VisiblePins
    {
        get => GetValue<string>()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList() ??
               Enumerable.Empty<string>();
        set => SetValue(string.Join(' ', value));
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramBlock</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool HideDesc
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// The <see cref="Sheet"/> this <c>DiagramFunction</c> belongs to.
    /// </summary>
    /// <value>A <see cref="Sheet"/> representing the containing code FBD sheet.</value>
    public Sheet? Sheet => Element.Parent is not null ? new Sheet(Element.Parent) : default;
    
    /// <inheritdoc />
    public override IEnumerable<LogixReference> References()
    {
        if (Operand is not null && Operand.IsTag())
            yield return new LogixReference(Element, Operand, L5XName.Tag);
        
        if (Type is not null)
            yield return new LogixReference(Element, Type, L5XName.AddOnInstructionDefinition);
    }
}