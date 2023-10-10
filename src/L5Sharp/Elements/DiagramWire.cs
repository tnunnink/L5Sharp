using System;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>LogixElement</c> type that defines the properties for a wire connector within a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>DiagramWire</c> is not a <see cref="DiagramElement"/> itself since is does not have the location and ID properties.
/// It simply maps the connections of pins within a diagram.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Wire, L5XName.Sheet)]
public class DiagramWire : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramWire"/> with default values.
    /// </summary>
    public DiagramWire()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramWire"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DiagramWire(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The ID of the source <c>DiagramElement</c> this wire is connected to.
    /// </summary>
    public uint FromID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// The parameter name of source <c>DiagramElement</c> pin this wire is connected to.
    /// </summary>
    public string? FromParam
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// The ID of the destination <c>DiagramElement</c> this wire is connected to.
    /// </summary>
    public uint ToID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// The parameter name of destination <c>DiagramElement</c> pin this wire is connected to.
    /// </summary>
    public string? ToParam
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}