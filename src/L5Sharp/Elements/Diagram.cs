using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// An abstract class for FBD and SFC routine code elements. Both routine types can be viewed as a container with a
/// collection of child block elements of different types. This abstraction defines the common interface for adding,
/// removing, and accessing child block types from the <c>Diagram</c>.
/// </summary>
/// <typeparam name="TBlock">The base <see cref="DiagramBlock"/> type this <c>Diagram</c> contains.</typeparam>
/// <typeparam name="TConnector">The <see cref="DiagramConnector"/> type the <c>Diagram</c> supports.</typeparam>
[PublicAPI]
public abstract class Diagram<TBlock, TConnector> : LogixCode
    where TBlock : DiagramBlock
    where TConnector : DiagramConnector, new()
{
    /// <summary>
    /// The defined order of all child diagram elements. This is required so we can add elements in the correct position.
    /// </summary>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> of <see cref="string"/> element name.</returns>
    protected abstract IEnumerable<string> Ordering();

    /// <summary>
    /// Creates a new <see cref="Diagram{TBlock,TConnector}"/> with default values.
    /// </summary>
    protected Diagram()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Diagram{TBlock,TConnector}"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    protected Diagram(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets or sets a single <see cref="DiagramBlock"/> with the specified block id.
    /// </summary>
    /// <param name="id">The Id of the block to get or set.</param>
    /// <exception cref="InvalidOperationException">No element has the specified <c>id</c>.
    /// -or- More than one element has the specified <c>id</c>.
    /// -or- The source sequence is empty.</exception>
    /// <remarks>This will find the block in the <c>Diagram</c> with the specified ID attribute value. Internally this is
    /// calling <c>Single</c> so if the element does not exists or there are more than one of the same ID then this will
    /// throw an exception.</remarks>
    public TBlock this[uint id]
    {
        get => Element.Elements()
            .Single(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id)
            .Deserialize<TBlock>();
        set => Element.Elements()
            .Single(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id)
            .ReplaceWith(value);
    }

    /// <summary>
    /// Adds the provided <c>DiagramBlock</c> to this <c>Diagram</c>.
    /// </summary>
    /// <param name="block">The <see cref="DiagramBlock"/> to add to the diagram.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    /// <remarks>
    /// This will update the block ID to the next available ID if the ID is already used.
    /// This will preserve the uniqueness of the ID's and prevent import errors. This will also perform a sort of the
    /// current underlying diagram elements to ensure the order of the element is maintained, which is also required to
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
    /// Adds the provided <c>DiagramConnector</c> to this <c>Diagram</c> element.
    /// </summary>
    /// <param name="connector">The <see cref="DiagramConnector"/> to add to the diagram.</param>
    /// <exception cref="ArgumentNullException"><c>connector</c> is null.</exception>
    public void Add(TConnector connector)
    {
        if (connector is null)
            throw new ArgumentNullException(nameof(connector));

        Element.Add(connector.Serialize());
        SortBlocks();
    }

    /// <summary>
    /// Adds the provided <c>TextBox</c> to this <c>Diagram</c>.
    /// </summary>
    /// <param name="textBox">The <see cref="TextBox"/> to add to the diagram.</param>
    /// <exception cref="ArgumentNullException"><c>textBox</c> is null.</exception>
    /// <remarks>
    /// This will update the block ID to the next available ID if the ID is already used.
    /// This will preserve the uniqueness of the ID's and prevent import errors. This will also perform a sort of the
    /// current underlying diagram elements to ensure the order of the element is maintained. This also is required to
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
    /// Adds the provided <c>attachment</c> to this <c>Diagram</c>.
    /// </summary>
    /// <param name="attachment">The <see cref="Attachment"/> to add to the diagram.</param>
    /// <exception cref="ArgumentNullException"><c>textBox</c> is null.</exception>
    /// <remarks>
    /// This will perform a sort of the current underlying diagram elements to ensure the order of the element is maintained.
    /// This is required to prevent import errors.
    /// </remarks>
    public void Add(Attachment attachment)
    {
        if (attachment is null)
            throw new ArgumentNullException(nameof(attachment));

        Element.Add(attachment.Serialize());
        SortBlocks();
    }

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
    /// Gets all <see cref="DiagramBlock"/> element contained in the <see cref="Diagram{TBlock,TConnector}"/>.
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
    /// Gets all <see cref="DiagramBlock"/> elements of the specified block type contained in the <see cref="Diagram{TBlock,TConnector}"/>.
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
    /// Adds a connector between the specified block IDs.
    /// </summary>
    /// <param name="from">The ID of the block from which the connector starts.</param>
    /// <param name="to">The ID of the block to which the connector ends.</param>
    public void Connect(uint from, uint to)
    {
        var connector = new TConnector
        {
            FromID = from,
            ToID = to
        };

        Element.Add(connector.Serialize());
        SortBlocks();
    }

    /// <summary>
    /// Retrieves all connector elements in the diagram. 
    /// </summary>
    /// <returns>A collection of all connectors of type <see cref="TConnector"/> found in the diagram.</returns>
    public IEnumerable<TConnector> Connectors()
    {
        return Elements().Where(e => e is TConnector).Cast<TConnector>();
    }
    
    /// <summary>
    /// Retrieves all connector elements in the diagram that have a connection to the specified block id.
    /// </summary>
    /// <param name="id">The <see cref="uint"/> id of the block to find connectors for.</param>
    /// <returns>A collection of connectors of type <see cref="TConnector"/> with an to/from id equal to the
    /// specified block id.</returns>
    public IEnumerable<TConnector> Connectors(uint id)
    {
        var inputs = Connectors().Where(c => c.IsTo(id));
        var outputs = Connectors().Where(c => c.IsTo(id));
        return inputs.Concat(outputs);
    }

    /// <summary>
    /// Gets all <see cref="DiagramConnector"/> elements in the <see cref="Diagram{TBlock,TConnector}"/>. 
    /// </summary>
    /// <returns>A collection of connectors of type <see cref="TConnector"/> with that are connected to and from the
    /// specified block id.</returns>
    public IEnumerable<TConnector> Connectors(TBlock block)
    {
        var inputs = Connectors().Where(c => c.IsTo(block));
        var outputs = Connectors().Where(c => c.IsFrom(block));
        return inputs.Concat(outputs);
    }

    /// <summary>
    /// Finds all other <see cref="DiagramBlock"/> elements that are connected via a <see cref="DiagramConnector"/> to
    /// the provided <c>block</c>.
    /// </summary>
    /// <param name="block">The <see cref="DiagramBlock"/> to find connections to/from.</param>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="DiagramBlock"/> elements connected to the provided <c>block</c>.
    /// </returns>
    /// <remarks>
    /// This helps find all the connecting blocks within a sheet, allowing potential traversal of the diagram
    /// block elements.
    /// </remarks>
    public IEnumerable<TBlock> Connections(TBlock block)
    {
        var endpoints = Connectors(block).Select(c => c.Endpoint(block).Key).ToHashSet();
        return Blocks().Where(b => endpoints.Contains(b.ID));
    }

    /// <summary>
    /// Completely removes the block and all associated connectors from the <c>Diagram</c>.
    /// </summary>
    /// <param name="block">The <see cref="DiagramBlock"/> to delete.</param>
    /// <remarks>This is effectively a combination of <see cref="Disconnect(TBlock)"/> and <see cref="Remove(TBlock)"/>
    /// for a more concise way of scrubbing a block from the diagram.</remarks>
    public void Delete(TBlock block)
    {
        Element.Elements()
            .Where(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == block.ID ||
                        e.Attribute(L5XName.FromID)?.Value.Parse<uint>() == block.ID ||
                        e.Attribute(L5XName.ToID)?.Value.Parse<uint>() == block.ID)
            .Remove();
    }

    /// <summary>
    /// Removes all <see cref="DiagramConnector"/> elements from the diagram with connections to the specified block ID.
    /// </summary>
    /// <param name="block">The block to disconnect from the diagram.</param>
    public void Disconnect(TBlock block)
    {
        Element.Elements()
            .Where(e => e.Attribute(L5XName.FromID)?.Value.Parse<uint>() == block.ID ||
                        e.Attribute(L5XName.ToID)?.Value.Parse<uint>() == block.ID)
            .Remove();
    }

    /// <summary>
    /// Removes all <see cref="DiagramConnector"/> elements from the diagram with connections to the specified block ID.
    /// </summary>
    /// <param name="id"></param>
    public void Disconnect(uint id)
    {
        Element.Elements()
            .Where(e => e.Attribute(L5XName.FromID)?.Value.Parse<uint>() == id ||
                        e.Attribute(L5XName.ToID)?.Value.Parse<uint>() == id)
            .Remove();
    }

    /// <summary>
    /// Removes all <see cref="DiagramConnector"/> elements from the diagram with the from/to ID pair.
    /// </summary>
    /// <param name="from">The ID of the source block for the connector.</param>
    /// <param name="to">The ID of the destination block for the connector.</param>
    /// <remarks>
    /// This will remove any connector with the from/to ID pair, which could be multiple connectors. This is
    /// because there is no ID to uniquely identify a generic <see cref="DiagramConnector"/>. If no connectors are
    /// found with provided ID pair, then will return.
    /// </remarks>
    public void Disconnect(uint from, uint to)
    {
        Element.Elements(typeof(TConnector).L5XType())
            .Where(e => e.Attribute(L5XName.FromID)?.Value.Parse<uint>() == from ||
                        e.Attribute(L5XName.FromID)?.Value.Parse<uint>() == to)
            .Remove();
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
    /// Gets all child <see cref="DiagramBlock"/> elements in the <see cref="Diagram{TBlock,TConnector}"/>
    /// </summary>
    /// <typeparam name="TBlockType"></typeparam>
    /// <returns></returns>
    public IEnumerable<TBlockType> Elements<TBlockType>() where TBlockType : DiagramBlock =>
        Elements().Where(e => e is TBlockType).Cast<TBlockType>();

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        return Blocks().Where(b => b is ILogixReferencable).Cast<ILogixReferencable>().SelectMany(r => r.References());
    }

    /// <summary>
    /// Removes the provided <see cref="DiagramBlock"/> from the the <c>Diagram</c> collection.
    /// </summary>
    /// <param name="block">The <see cref="DiagramBlock"/> to remove.</param>
    /// <remarks>
    /// This will remove single block with the Id of the provided block if it is found.
    /// If not found, the will return without removing anything.
    /// </remarks>
    public void Remove(TBlock block)
    {
        Element.Elements().SingleOrDefault(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == block.ID)?.Remove();
    }

    /// <summary>
    /// Removes the provided <see cref="DiagramBlock"/> from the the <c>Diagram</c> collection.
    /// </summary>
    /// <param name="connector">The <see cref="DiagramConnector"/> to remove.</param>
    /// <remarks>
    /// This will remove any connector having the same from/to ID pair as the provided connector IDs if found.
    /// If not found, the will return without removing anything.
    /// </remarks>
    public void Remove(TConnector connector)
    {
        Element.Elements(typeof(TConnector).L5XType()).SingleOrDefault(e =>
                e.Attribute(L5XName.FromID)?.Value.Parse<uint>() == connector.FromID &&
                e.Attribute(L5XName.ToID)?.Value.Parse<uint>() == connector.ToID)
            ?.Remove();
    }

    #region Internal

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

    #endregion
}