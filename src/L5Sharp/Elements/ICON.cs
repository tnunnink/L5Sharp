using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramBlock</c> type that defines the properties for pin connectors in a Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>ConnectorBlock</c> is similar to a <c>Wire</c> in that it maps the pins between diagram elements
/// in a FBD. The difference is that the connector uses the alias <c>Name</c> to map one pin to another without having
/// to draw the entire <c>Wire</c> connection between blocks. A <see cref="Sheet"/> will contain both input and output connectors,
/// which map to input and from output pins of a given <c>DiagramBlock</c>. To specify whether a new element represents
/// an input or output connector block, use the <see cref="ParameterType"/> overload constructor.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.ICon, L5XName.Sheet)]
public class ICON : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="ICON"/> with default values.
    /// </summary>
    public ICON()
    {
        Name = string.Empty;
    }
    
    /// <summary>
    /// Creates a new <see cref="IREF"/> with the provided <c>operand</c> value.
    /// </summary>
    public ICON(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Creates a new <see cref="ICON"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public ICON(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name identifying the connector element.
    /// </summary>
    public string? Name
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}