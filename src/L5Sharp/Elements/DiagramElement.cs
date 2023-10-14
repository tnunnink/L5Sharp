using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Elements;

/// <summary>
/// A base class for all FBD routine elements within a <c>Sheet</c>. This base class simply contains some
/// of the common properties that all FBD elements share, such as X and Y coordinates, and ID.
/// </summary>
public abstract class DiagramElement : LogixElement, ILogixReferencable
{
    /// <summary>
    /// Creates a new <see cref="DiagramElement"/> with default values.
    /// </summary>
    protected DiagramElement()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramElement"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    protected DiagramElement(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// Gets the cell of the diagram where the <see cref="DiagramElement"/> is located. 
    /// </summary>
    /// <value>A <see cref="string"/> containing the cell coordinates (e.g. A1, B2, etc.)</value>
    /// <remarks>This is determines from the <c>X</c> and <c>Y</c> coordinates of the element.</remarks>
    public string Cell => $"{(char) (X / 200 + 'A')}{Y / 200 + 1}";
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string Location => $"{Cell} ({X}, {Y})";

    /// <summary>
    /// The unique identifier of the <see cref="DiagramElement"/> within the containing <c>Sheet</c>.
    /// </summary>
    public uint ID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    /// <summary>
    /// The X coordinate of the <see cref="DiagramElement"/> within the containing <c>Sheet</c>.
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
    /// The Y coordinate of the <see cref="DiagramElement"/> within the containing <c>Sheet</c>.
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

    /// <inheritdoc />
    public virtual IEnumerable<LogixReference> References() => Enumerable.Empty<LogixReference>();
}