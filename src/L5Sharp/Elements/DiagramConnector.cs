using System;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramElement</c> type that defines the properties for pin connectors in a Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>DiagramConnector</c> is similar to a <c>DiagramWire</c> in that it maps the pins between diagram elements
/// in a FBD. The difference is that the connector uses the alias <c>Name</c> to map one pin to another without having
/// to draw the entire wire connection between blocks. A <see cref="Sheet"/> will contain both input and output connectors,
/// which map to input and from output pins of a given <c>DiagramElement</c>.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.ICon, L5XName.Sheet)]
[L5XType(L5XName.OCon, L5XName.Sheet)]
public class DiagramConnector : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramConnector"/> with default values.
    /// </summary>
    public DiagramConnector()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramConnector"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DiagramConnector(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    public override string Location => Sheet is not null ? $"Sheet {Sheet.Number} {Cell}" : $"{Cell}";
    
    /// <summary>
    /// The name identifying the connector element.
    /// </summary>
    public string? Name
    {
        get => GetValue<string>();
        set => SetValue(value); 
    }
    
    /// <summary>
    /// The <see cref="Sheet"/> this <c>DiagramFunction</c> belongs to.
    /// </summary>
    /// <value>A <see cref="Sheet"/> representing the containing code FBD sheet.</value>
    public Sheet? Sheet => Element.Parent is not null ? new Sheet(Element.Parent) : default;
}