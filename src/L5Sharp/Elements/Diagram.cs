using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// An abstract class for FBD and SFC routine code elements. Both routine types can be viewed as a container with a
/// collection of child elements of different types. This abstraction defines the common interface for adding,
/// removing, and accessing child block types from the <c>Diagram</c>.
/// </summary>
[PublicAPI]
public abstract class Diagram : LogixCode
{
    /// <summary>
    /// The defined order of all child diagram elements. This is required so we can add elements in the correct position.
    /// </summary>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> of <see cref="string"/> element name.</returns>
    protected abstract IEnumerable<string> Ordering();

    /// <summary>
    /// Creates a new <see cref="Diagram"/> with default values.
    /// </summary>
    protected Diagram()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Diagram"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    protected Diagram(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Add a <see cref="TextBox"/> element with the given text to the diagram.
    /// </summary>
    /// <param name="text">The text to be added to the TextBox element.</param>
    /// <returns>The ID of the added TextBox element.</returns>
    public uint Add(string text)
    {
        var id = NextAvailableId();
        
        var element = new XElement(L5XName.TextBox);
        element.Add(new XAttribute(L5XName.ID, id));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Width, 0));
        
        if (!string.IsNullOrEmpty(text))
        {
            element.Add(new XElement(L5XName.Text), new XCData(text));    
        }
        
        Element.Add(element);
        SortBlocks();
        return id;
    }

    /// <summary>
    /// Add a <see cref="TextBox"/> element at the specified position (x, y) with the given text to the diagram.
    /// </summary>
    /// <param name="text">The text to be added to the TextBox element.</param>
    /// <param name="x">The x-coordinate of the TextBox element.</param>
    /// <param name="y">The y-coordinate of the TextBox element.</param>
    /// <returns>The ID of the added TextBox element.</returns>
    public uint AddAt(string text, uint x, uint y)
    {
        var id = NextAvailableId();

        var element = new XElement(L5XName.TextBox);
        element.Add(new XAttribute(L5XName.ID, id));
        element.Add(new XAttribute(L5XName.X, x));
        element.Add(new XAttribute(L5XName.Y, y));
        element.Add(new XAttribute(L5XName.Width, 0));
        
        if (!string.IsNullOrEmpty(text))
        {
            element.Add(new XElement(L5XName.Text), new XCData(text));    
        }
        
        Element.Add(element);
        SortBlocks();
        return id;
    }

    /// <summary>
    /// Retrieves all child <see cref="LogixElement"/> of the current diagram.
    /// </summary>
    /// <returns>A collection of <see cref="LogixElement"/> in the diagram.</returns>
    /// <remarks>
    /// This is a generic method for retrieving whatever child elements exist on the diagram. Derived classes
    /// will implement more specific methods for retrieving more concrete elements.
    /// </remarks>
    public IEnumerable<LogixElement> Elements()
    {
        return Element.Elements().Select(e => e.Deserialize());
    }
    
    /// <summary>
    /// Joins the defined ordered set of element names with the child elements of the diagram, and replaces all current
    /// nodes with the same set of order nodes. This maintains the order of the diagram elements base on the derived
    /// classes order requirements.
    /// </summary>
    protected void SortBlocks()
    {
        var ordered = Ordering().Join(Element.Elements(), s => s, e => e.Name.LocalName, (_, e) => e).ToList();
        Element.ReplaceNodes(ordered);
    }

    /// <summary>
    /// Given a new diagram block, assign the Id to the next available Id only if it is not already used.
    /// </summary>
    protected void AssignId(XElement element)
    {
        var id = element.Attribute(L5XName.ID)?.Value.Parse<uint>() ?? 0;
        
        if (IsUsed(id))
        {
            element.SetAttributeValue(L5XName.ID, NextAvailableId());
        }
    }

    /// <summary>
    /// Gets the next highest ID value with the given set of diagram elements.
    /// </summary>
    private uint NextAvailableId()
    {
        return Element.Elements().Select(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>()).Max() + 1 ?? 0;
    }

    /// <summary>
    /// Determines if the provided Id is already used by another diagram element.
    /// </summary>
    private bool IsUsed(uint id)
    {
        return Element.Elements().Any(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id);
    }
}