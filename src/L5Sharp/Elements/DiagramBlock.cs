using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A base class for all FBD/SFC routine elements within a containing <c>Diagram</c>. This base class simply
/// contains some of the common properties and functions that all FBD/SFC elements share,
/// such as X and Y coordinates, and ID.
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
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
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
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
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
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
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

    /// <summary>
    /// Updates the X and Y coordinates of the <see cref="DiagramBlock"/> to the specified cell location.
    /// </summary>
    /// <param name="cell">The alpha-numeric cell location to move the block to.</param>
    /// <exception cref="ArgumentException"><c>cell</c> is null, empty, not two characters, does not start with a letter,
    /// or does not end with a digit.</exception>
    public void MoveTo(string cell)
    {
        if (string.IsNullOrEmpty(cell))
            throw new ArgumentException("Can not perform function with null or empty cell location.");
        
        if (cell.Length != 2)
            throw new ArgumentException(
                $"Cell {cell} is not a valid length argument. Must be 2 character cell location.");

        if (!char.IsLetter(cell[0]))
            throw new ArgumentException($"Cell {cell} must start with a valid letter character");
        
        if (!char.IsDigit(cell[1]))
            throw new ArgumentException($"Cell {cell} must end with a valid number character");
        
        X = (uint)(cell.ToUpper()[0] - 'A') * 200;
        Y = (uint)cell[1] * 200;
    }

    /// <inheritdoc />
    public virtual IEnumerable<CrossReference> References() => Enumerable.Empty<CrossReference>();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            ValueType value => Equals(ID, value),
            DiagramBlock block => Equals(ID, block.ID),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => ID.GetHashCode();
}