using System;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramElement</c> type that defines the properties for a input or output reference within a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>DiagramReference</c> can be a tag name or immediate value, which is contained in the <see cref="Operand"/>
/// property of the type. A <see cref="Sheet"/> will contain both input and output references.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.IRef, L5XName.Sheet)]
[L5XType(L5XName.ORef, L5XName.Sheet)]
public class DiagramReference : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramReference"/> with default values.
    /// </summary>
    public DiagramReference()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramReference"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DiagramReference(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// The tag name or immediate value for the <c>DiagramReference</c> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the reference name if it exists; Otherwise, <c>null</c>.</value>
    public string? Operand
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramReference</c> element.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool HideDesc
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
}