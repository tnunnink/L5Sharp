using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// An abstract class for FBD and SFC routine code elements. Both routine types can be viewed as a container with a
/// collection of child block elements of different types. This abstraction defines the common interface for adding,
/// removing, and accessing child block types from the <c>Diagram</c>.
/// </summary>
/// <typeparam name="TBlock">The base <see cref="DiagramBlock"/> type this <c>Diagram</c> contains.</typeparam>
[PublicAPI]
public abstract class Diagram<TBlock> : LogixCode where TBlock : DiagramBlock
{
    /// <summary>
    /// The defined order of all child diagram elements. This is required so we can add elements in the correct position.
    /// </summary>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> of <see cref="string"/> element name.</returns>
    protected abstract IEnumerable<string> Ordering();

    /// <summary>
    /// Creates a new <see cref="Diagram{TBlock}"/> with default values.
    /// </summary>
    protected Diagram()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Diagram{TBlock}"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    protected Diagram(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets a <see cref="DiagramBlock"/> with the specified block id.
    /// </summary>
    /// <param name="id">The Id of the block to get or set.</param>
    /// 
    /// <remarks>This will </remarks>
    public TBlock this[uint id]
    {
        get => Element.Elements()
            .Single(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id)
            .Deserialize<TBlock>();
        set => Element.Elements().Single(e => e.Get(L5XName.ID).Parse<uint>() == id).ReplaceWith(value);
    }

    /// <summary>
    /// Adds the provided <c>DiagramBlock</c> to this <c>Diagram</c>.
    /// </summary>
    /// <param name="block">The <see cref="DiagramBlock"/> to add to the diagram.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    /// <remarks>
    /// This will update the block ID to the next available ID if the ID is already used.
    /// This will preserve the uniqueness of the ID's and prevent import errors. This will also perform a sort of the
    /// current underlying diagram elements to ensure the order of the element is maintained. This also si required to
    /// prevent import errors.
    /// </remarks>
    public uint Add(TBlock block)
    {
        if (block is null)
            throw new ArgumentNullException(nameof(block));

        AssignId(block);
        Element.Add(block.Serialize());
        SortBlocks();
        return block.ID;
    }

    /// <summary>
    /// Adds the provided <c>TextBox</c> to this <c>Diagram</c>.
    /// </summary>
    /// <param name="textBox">The <see cref="TextBox"/> to add to the diagram.</param>
    /// <exception cref="ArgumentNullException"><c>textBox</c> is null.</exception>
    /// <remarks>
    /// This will update the block ID to the next available ID if the ID is already used.
    /// This will preserve the uniqueness of the ID's and prevent import errors. This will also perform a sort of the
    /// current underlying diagram elements to ensure the order of the element is maintained. This also si required to
    /// prevent import errors.
    /// </remarks>
    public uint Add(TextBox textBox)
    {
        if (textBox is null)
            throw new ArgumentNullException(nameof(textBox));

        AssignId(textBox);
        Element.Add(textBox.Serialize());
        SortBlocks();
        return textBox.ID;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fromId"></param>
    /// <param name="toId"></param>
    public abstract void Connect(uint fromId, uint toId);

    /// <summary>
    /// Finds a block with the specified block id. If not found, returns <c>null</c>.
    /// </summary>
    /// <param name="id">The zero based id of the block to find.</param>
    /// <returns>A <see cref="DiagramBlock"/> with the specified id if found; Otherwise; <c>null</c></returns>
    /// <remarks>
    /// This uses <c>SingleOrDefault</c> internally so will fail if the the id is not unique which is should be.
    /// </remarks>
    public TBlock? Block(uint id)
    {
        return Element.Elements().SingleOrDefault(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id)
            ?.Deserialize<TBlock>();
    }

    /// <summary>
    /// Finds a block with the specified block id and type. If not found, returns <c>null</c>.
    /// </summary>
    /// <param name="id">The zero based id of the block to find.</param>
    /// <typeparam name="TBlockType">The block type to return.</typeparam>
    /// <returns>A <see cref="DiagramBlock"/> of the generic type parameter with the specified id if found; Otherwise, <c>null</c>.</returns>
    public TBlockType? Block<TBlockType>(uint id) where TBlockType : TBlock
    {
        return Element.Elements(typeof(TBlockType).L5XType())
            .SingleOrDefault(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id)
            ?.Deserialize<TBlockType>();
    }

    /// <summary>
    /// Gets all <see cref="DiagramBlock"/> element contained in the <see cref="Diagram{TBlock}"/>.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="DiagramBlock"/> elements.</returns>
    /// <remarks>
    /// To avoid adding container collections for each block type, we are simplifying the interface using a single
    /// method with different overloads for retrieving child diagram blocks. This simple returns an enumerable collection
    /// of elements. To add or remove blocks use the corresponding <see cref="Add(TBlock)"/> methods of the diagram.
    /// </remarks>
    public IEnumerable<TBlock> Blocks()
    {
        return Elements().Where(e => e is TBlock).Cast<TBlock>();
    }

    /// <summary>
    /// Gets all <see cref="DiagramBlock"/> elements of the specified block type contained in the <see cref="Diagram{TBlock}"/>.
    /// </summary>
    /// <typeparam name="TBlockType">The diagram block type to return.</typeparam>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found <see cref="DiagramBlock"/> elements.</returns>
    /// <remarks>
    /// To avoid adding container collections for each block type, we are simplifying the interface using a single
    /// method with different overloads for retrieving child diagram blocks. This simple returns an enumerable collection
    /// of elements. To add or remove blocks use the corresponding <see cref="Add(TBlock)"/> methods of the diagram.
    /// </remarks>
    public IEnumerable<TBlockType> Blocks<TBlockType>() where TBlockType : TBlock
    {
        return Elements().Where(e => e is TBlockType).Cast<TBlockType>();
    }

    /// <summary>
    /// Gets all child <see cref="LogixElement"/> of the current diagram.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="LogixElement"/> objects.</returns>
    public IEnumerable<LogixElement> Elements()
    {
        return Element.Elements().Select(e => e.Deserialize());
    }

    /// <summary>
    /// Joins the defined ordered set of element names with the child elements of the diagram, and replaces all current
    /// nodes with the same set of order nodes. This maintains the order of the diagram elements base on the derived
    /// classes order requirements.
    /// </summary>
    private void SortBlocks()
    {
        var ordered = Ordering().Join(Element.Elements(), s => s, e => e.Name.LocalName, (_, e) => e).ToList();
        Element.ReplaceNodes(ordered);
    }

    /// <summary>
    /// Given a new diagram block, assign the Id to the next available Id only if it is not already used.
    /// </summary>
    private void AssignId(DiagramBlock block)
    {
        if (IsUsed(block.ID))
        {
            block.ID = NextAvailableId();
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