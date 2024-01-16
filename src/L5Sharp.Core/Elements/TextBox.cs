using System;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A element of a diagram that contains a descriptive section of text. This element can be found on both FBD or SFC
/// diagram elements.
/// </summary>
/// <remarks>
/// This element contains methods for creating attachments to other elements on a diagram. Use the
/// <see cref="Attach"/> and <see cref="Detach"/> methods to create or remove the attachments within the parent diagram.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.TextBox)]
public class TextBox : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="TextBox"/> with the provided text.
    /// </summary>
    /// <param name="text">The text to initialize the text box with.</param>
    public TextBox(string text)
    {
        Element.SetAttributeValue(L5XName.Text, text);
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
    /// The text information contained in the <c>TextBox</c> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text contents.</value>
    public string? Text
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    /// <summary>
    /// Creates an attachment from this <see cref="TextBox"/> to the specified block id.
    /// </summary>
    /// <param name="to">The id of the block attach this text to.</param>
    /// <exception cref="InvalidOperationException">No parent element exists for this TextBox element.
    /// This means that the object is created but has not yet been added to a diagram.</exception>
    public void Attach(uint to)
    {
        var element = new XElement(L5XName.Attachment);
        element.Add(new XAttribute(L5XName.FromID, ID));
        element.Add(new XAttribute(L5XName.ToID, to));

        var diagram = Element.Parent ??
            throw new InvalidOperationException("Can not attach text to element that does not have a parent.");
        
        diagram.LastNode?.AddAfterSelf(element);
    }

    /// <summary>
    /// Detaches this <see cref="TextBox"/> from the block it is currently attached to.
    /// </summary>
    public void Detach()
    {
        Element.Elements(L5XName.Attachment)
            .FirstOrDefault(e => e.Attribute(L5XName.FromID)?.Value.Parse<uint>() == ID)
            ?.Remove();
    }
}