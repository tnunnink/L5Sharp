using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A base class for all FBD/SFC routine elements within a containing <c>Diagram</c>. This base class simply
/// contains some of the common properties and functions that all FBD/SFC elements share,
/// such as X and Y coordinates, and ID. We have also added a <see cref="ParameterType"/> and <see cref="Cell"/>
/// property for determining the type and location (e.g. A1) of a given block.
/// </summary>
public abstract class DiagramBlock : LogixElement, ILogixReferencable
{
    /// <summary>
    /// Creates a new <see cref="DiagramBlock"/> with default values.
    /// </summary>
    protected DiagramBlock()
    {
        Element.SetAttributeValue(L5XName.ID, 0);
        Element.SetAttributeValue(L5XName.X, 0);
        Element.SetAttributeValue(L5XName.Y, 0);
    }

    /// <summary>
    /// Creates a new <see cref="DiagramBlock"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    protected DiagramBlock(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The unique identifier of the <see cref="DiagramBlock"/> within the containing <c>Diagram</c>.
    /// </summary>
    /// <value>A zero based <see cref="uint"/> representing the block id.</value>
    public uint ID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    /// <summary>
    /// The X coordinate of the <see cref="DiagramBlock"/> within the containing <c>Diagram</c>.
    /// </summary>
    /// <remarks>
    /// The <c>X</c> and <c>Y</c> grid locations are a relative position from the upper-left corner of the sheet.
    /// X is the horizontal position; Y is the vertical position.
    /// </remarks>
    public uint X
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    /// <summary>
    /// The Y coordinate of the <see cref="DiagramBlock"/> within the containing <c>Diagram</c>.
    /// </summary>
    /// <remarks>
    /// The <c>X</c> and <c>Y</c> grid locations are a relative position from the upper-left corner of the sheet.
    /// X is the horizontal position; Y is the vertical position.
    /// </remarks>
    public uint Y
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the cell of the diagram where the <see cref="DiagramBlock"/> is located. 
    /// </summary>
    /// <value>A <see cref="string"/> containing the cell coordinates (e.g. A1, B2, etc.)</value>
    /// <remarks>This is determines from the <c>X</c> and <c>Y</c> coordinates of the element. Each cell is 200x200
    /// pixels which can be used to calculate the cell location.</remarks>
    public string Cell => $"{(char)(X / 200 + 'A')}{Y / 200 + 1}";

    /// <summary>
    /// The descriptive indication of the location of this <see cref="DiagramBlock"/> within the containing <c>Diagram</c>. 
    /// </summary>
    /// <value>A <see cref="string"/> indicating the cell and optional sheet or chart number where the block is located.</value>
    /// <remarks>
    /// This is an internally defined value so that we can identify instructions when referencing components.
    /// </remarks>
    public virtual string Location => Cell;

    /// <inheritdoc />
    public virtual IEnumerable<CrossReference> References() => Enumerable.Empty<CrossReference>();
}