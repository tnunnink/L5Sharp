using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramBlock</c> type that defines the properties a section of text within a
/// Function Block Diagram (FBD).
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.TextBox)]
public class TextBox : DiagramBlock
{
    /// <summary>
    /// Creates a new <see cref="TextBox"/> with default values.
    /// </summary>
    public TextBox()
    {
    }

    /// <summary>
    /// Creates a new <see cref="TextBox"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public TextBox(XElement element) : base(element)
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
    /// The text information contained in the <c>TextBox</c> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text contents.</value>
    public string Text
    {
        get => GetProperty<string>() ?? string.Empty;
        set => SetProperty(value);
    }
}