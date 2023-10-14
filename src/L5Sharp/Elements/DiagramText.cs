using System;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramElement</c> type that defines the properties a section of text within a
/// Function Block Diagram (FBD).
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.TextBox)]
public class DiagramText : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramReference"/> with default values.
    /// </summary>
    public DiagramText()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramReference"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DiagramText(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The width of the text box element.
    /// </summary>
    /// <value>A <see cref="uint"/> specifying the width of the text box.</value>
    /// <remarks>According to the documentation this is not used.</remarks>
    public uint Width
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// The text information contained in the <c>DiagramText</c> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text contents.</value>
    public string Text
    {
        get => GetProperty<string>() ?? string.Empty;
        set => SetProperty(value);
    }
    
    /// <summary>
    /// The <see cref="Sheet"/> this <c>DiagramFunction</c> belongs to.
    /// </summary>
    /// <value>A <see cref="Sheet"/> representing the containing code FBD sheet.</value>
    public Sheet? Sheet => Element.Parent is not null ? new Sheet(Element.Parent) : default;
}