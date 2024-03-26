using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A base class for all FBD/SFC routine elements within a containing <c>Diagram</c>. This base class simply
/// contains some of the common properties and functions that all FBD/SFC elements share,
/// such as X and Y coordinates, and ID.
/// </summary>
public abstract class DiagramElement : LogixObject, ILogixReferencable
{
    /// <summary>
    /// Creates a new <see cref="DiagramElement"/> with default values and initializes the required \
    /// diagram element properties ID, X, and Y to 0.
    /// </summary>
    protected DiagramElement()
    {
        Element.SetAttributeValue(L5XName.ID, 0);
        Element.SetAttributeValue(L5XName.X, 0);
        Element.SetAttributeValue(L5XName.Y, 0);
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
    /// The unique identifier of the <see cref="DiagramElement"/> within it's containing diagram.
    /// </summary>
    /// <value>A zero based <see cref="uint"/> representing the block id.</value>
    /// <remarks>
    /// This doesn't need to be set explicitly by the use as it will be assigned when adding an element to
    /// a <see cref="Diagram"/>.
    /// </remarks>
    public uint ID
    {
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The X coordinate of the <see cref="DiagramElement"/> within it's containing diagram.
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
    /// The Y coordinate of the <see cref="DiagramElement"/> within it's containing diagram.
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
    /// Gets the cell of the diagram where the <see cref="DiagramElement"/> is located. 
    /// </summary>
    /// <value>A <see cref="string"/> containing the cell coordinates (e.g. A1, B2, etc.)</value>
    /// <remarks>This is determines from the <c>X</c> and <c>Y</c> coordinates of the element. Each cell is 200x200
    /// pixels which can be used to calculate the cell location.</remarks>
    public string Cell => $"{(char)(X / 200 + 'A')}{Y / 200 + 1}";

    /// <summary>
    /// The descriptive indication of the location of this diagram element within the containing <see cref="Diagram"/>. 
    /// </summary>
    /// <value>A <see cref="string"/> indicating the cell and optional sheet or chart number where the block is located.</value>
    /// <remarks>
    /// This is an internally defined value so that we can identify instructions when referencing components.
    /// </remarks>
    public virtual string Location => Cell;
    
    /// <summary>
    /// Moves this diagram element to the specified X and Y coordinates.
    /// </summary>
    /// <param name="x">The X coordinate to move this block to.</param>
    /// <param name="y">The Y coordinate to move this block to.</param>
    /// <returns>This</returns>
    public void Move(uint x, uint y)
    {
        Element.SetAttributeValue(L5XName.X, x);
        Element.SetAttributeValue(L5XName.Y, y);
    }

    /// <summary>
    /// Moves this diagram element to the specified alpha-numeric cell location.
    /// </summary>
    /// <param name="cell">The alpha-numeric cell location to move this block to.</param>
    /// <exception cref="ArgumentException"><paramref name="cell"/> is null, empty, not two characters,
    /// does not start with a letter, or does not end with a digit.</exception>
    public void Move(string cell)
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

        var x = (uint)(cell.ToUpper()[0] - 'A') * 200;
        var y = (uint)cell[1] * 200;

        Element.SetAttributeValue(L5XName.X, x);
        Element.SetAttributeValue(L5XName.Y, y);
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
            DiagramElement block => Equals(ID, block.ID),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => ID.GetHashCode();
    
    /// <summary>
    /// Gets a collection of values for the specified attribute name parsed as the specified generic type parameter if it exists.
    /// If the element does not exist, returns an empty collection of the generic type parameter.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="separator">The value separator character. Default is ' '.</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>
    /// If found, all values of the attribute split on the specified separator and parsed as the generic type parameter.
    /// If not found, returns an empty collection.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting attributes with collection of values as a single string with a certain separator
    /// character as concise as possible for derived classes. This method is added here since only types like <see cref="Block"/>
    /// are using this method overload.
    /// </remarks>
    protected IEnumerable<T> GetValues<T>(string name, char separator = ' ')
    {
        var value = Element.Attribute(name)?.Value;
        
        return value is not null
            ? value.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Parse<T>())
            : Enumerable.Empty<T>();
    }
}