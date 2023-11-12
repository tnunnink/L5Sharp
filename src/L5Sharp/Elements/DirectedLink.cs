using System;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>LogixElement</c> type that defines the properties for a wire connector within a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Connector</c> is not a <see cref="DiagramBlock"/> itself since is does not have the location and ID properties.
/// It simply maps the connections of pins within a diagram.
/// Use these guidelines for directed links:
/// • All directed link blocks must come after all step, transition, stop, and branch blocks.
/// • A directed link links only one element to one other element.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.DirectedLink, L5XName.SFCContent)]
public class DirectedLink : DiagramConnector
{
    /// <summary>
    /// Creates a new <see cref="DirectedLink"/> with default values.
    /// </summary>
    public DirectedLink()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DirectedLink"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    public DirectedLink(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Indicates whether the link is shown on the SFC <c>Diagram</c>.
    /// </summary>
    public bool? Show
    {
        get => GetValue<bool?>();
        set => SetValue(value);
    }
}