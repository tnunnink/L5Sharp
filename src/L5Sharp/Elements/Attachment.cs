using System;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// The attachment blocks identify the attachments from text boxes to other function block elements.
/// </summary>
/// /// <summary>
/// A <c>LogixElement</c> type that defines the properties for a wire connector within a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Connector</c> is not a <see cref="DiagramBlock"/> itself since is does not have the location and ID properties.
/// It simply maps the connections of pins within a diagram.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Attachment : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Attachment"/> with default values.
    /// </summary>
    public Attachment()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Attachment"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Attachment(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The ID of the source <c>DiagramBlock</c> this attachment is connected to.
    /// </summary>
    public uint FromID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// The ID of the destination <c>DiagramBlock</c> this attachment is connected to.
    /// </summary>
    public uint ToID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }
}