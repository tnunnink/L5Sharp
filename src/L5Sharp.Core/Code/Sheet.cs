using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A Logix <c>Sheet</c> block containing the properties for a Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Sheet</c> implements <see cref="LogixCode"/> and is the type of content that FBD routines contain.
/// <para>
/// Observe these guidelines when defining a sheet:<br/>
///     • The sheets in the routine appear in order in the export file.
///         Each sheet section contains all the drawing elements and wires for that sheet. <br/>
///     • On import, sheet numbers are assigned based on order in the file, not on the number attribute on the sheet.<br/>
///     • The sheet name is stored as a description on the sheet.<br/>
///     • Input references, blocks, output references, special drawing elements,
///         and wires are contained within the sheet. On export, the elements
///         appear in the order shown. On import in the L5K format, elements can
///         be interspersed in the file. On import in the L5X format, the elements
///         must appear in the exported order.<br/>
///     • Wire and feedback wire statements must appear after all the other
///         components.<br/>
///     • Be careful when copying and pasting function block components
///         within an import/export file. Each component within a sheet must
///         have a unique id number within that sheet.<br/>
/// </para>
/// </remarks>
[L5XType(L5XName.Sheet)]
public class Sheet : LogixCode
{
    private static readonly HashSet<string> BlockTypes = [..typeof(Block).GetLogixTypeNames()];

    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.IRef,
        L5XName.ORef,
        L5XName.ICon,
        L5XName.OCon,
        L5XName.Block,
        L5XName.Function,
        L5XName.AddOnInstruction,
        L5XName.JSR,
        L5XName.SBR,
        L5XName.Ret,
        L5XName.Wire,
        L5XName.FeedbackWire,
        L5XName.TextBox,
        L5XName.Attachment
    ];

    /// <summary>
    /// Creates a new <see cref="Sheet"/> with default values.
    /// </summary>
    public Sheet() : base(L5XName.Sheet)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Sheet"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    public Sheet(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The description of the <see cref="Sheet"/> block.
    /// </summary>
    /// <value>A <see cref="string"/> containing the description if it exists; Otherwise, <c>null</c></value>
    public string? Description
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    /// <inheritdoc />
    public override IEnumerable<Instruction> Instructions()
    {
        return Blocks().Select(b => b.ToInstruction());
    }

    /// <inheritdoc />
    public override IEnumerable<TagName> Tags()
    {
        return Blocks().Select(b => b.ToInstruction()).SelectMany(i => i.Tags);
    }

    /// <summary>
    /// Retrieves all <see cref="Core.Block"/> elements found in the sheet diagram.
    /// </summary>
    /// <returns>A collection of <see cref="Core.Block"/> elements found in the diagram.</returns>
    /// <remarks>
    /// </remarks>
    public IEnumerable<Block> Blocks()
    {
        return Element.Elements()
            .Where(e => BlockTypes.Contains(e.Name.LocalName))
            .Select(e => e.Deserialize<Block>());
    }

    /// <summary>
    /// Retrieves a collection of <see cref="Core.Block"/> objects filtered by the specified type.
    /// </summary>
    /// <param name="type">The type of the blocks to retrieve.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the blocks that match the specified type.</returns>
    public IEnumerable<Block> Blocks(string type)
    {
        return Element.Elements()
            .Where(e => BlockTypes.Contains(e.Name.LocalName))
            .Select(e => e.Deserialize<Block>())
            .Where(b => b.Type == type);
    }

    /// <summary>
    /// Retrieves a <see cref="Core.Block"/> with the specified ID from the elements.
    /// </summary>
    /// <param name="id">The identifier of the block to retrieve.</param>
    /// <returns>The <see cref="Core.Block"/> that matches the specified ID.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no block with the specified ID is found.</exception>
    public Block Block(uint id)
    {
        var element = Element.Elements()
            .SingleOrDefault(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id);

        return element?.Deserialize<Block>() ?? throw new KeyNotFoundException("");
    }

    /// <summary>
    /// Finds a block with the specified operand value. If not found, returns <c>null</c>.
    /// </summary>
    /// <param name="operand">The operand of that </param>
    /// <returns>A <see cref="Core.Block"/> with the specified id if found; Otherwise null</returns>
    /// <remarks>
    /// 
    /// </remarks>
    public Block? Block(string operand)
    {
        return Element.Elements()
            .FirstOrDefault(e =>
                e.Attribute(L5XName.Operand)?.Value == operand || e.Attribute(L5XName.Name)?.Value == operand
            )
            ?.Deserialize<Block>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operand"></param>
    /// <returns></returns>
    public Sheet AddInput(TagName operand)
    {
        var block = Core.Block.IREF(operand);
        
        var element = block.Serialize();
        element.SetAttributeValue(L5XName.ID, NextAvailableId());

        Element.Add(element);
        EnsureOrder();

        return this;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="operand"></param>
    /// <returns></returns>
    public Sheet AddOutput(TagName operand)
    {
        var block = Core.Block.OREF(operand);
        
        var element = block.Serialize();
        element.SetAttributeValue(L5XName.ID, NextAvailableId());
        element.SetAttributeValue(L5XName.X, NextAvailableId());

        Element.Add(element);
        EnsureOrder();

        return this;
    }

    /// <summary>
    /// Adds a <see cref="Core.Block"/> to the current <see cref="Sheet"/>.
    /// </summary>
    /// <param name="block">The <see cref="Core.Block"/> to be added to the <see cref="Sheet"/>. Must not be null.</param>
    /// <returns>Returns the current <see cref="Sheet"/> instance after the block has been added.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="block"/> parameter is null.</exception>
    public Sheet AddBlock(Block block)
    {
        if (block is null)
            throw new ArgumentNullException(nameof(block));

        var element = block.Serialize();
        element.SetAttributeValue(L5XName.ID, NextAvailableId());

        Element.Add(element);
        EnsureOrder();

        return this;
    }

    /// <summary>
    /// Removes a block matching the specified operand from the sheet.
    /// </summary>
    /// <param name="operand">The <see cref="TagName"/> representing the block to remove.</param>
    /// <returns>The updated <see cref="Sheet"/> with the block removed.</returns>
    public Sheet RemoveBlock(TagName operand)
    {
        if (operand is null) throw new ArgumentNullException(nameof(operand));
        
        Element.Elements().SingleOrDefault(e => e.GetBlockOperand() == operand)?.Remove();
        
        return this;
    }

    /// <summary>
    /// Connects two blocks on the sheet and optionally specifies the source and target tag names for the connection.
    /// </summary>
    /// <param name="source">The source block to be connected.</param>
    /// <param name="target">The target block to be connected.</param>
    /// <param name="from">Optional tag name representing the originating connection point on the source block.</param>
    /// <param name="to">Optional tag name representing the receiving connection point on the target block.</param>
    /// <returns>The <see cref="Sheet"/> instance with the connection applied.</returns>
    public Sheet Connect(Block source, Block target, TagName? from = null, TagName? to = null)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (target is null) throw new ArgumentNullException(nameof(target));

        WireBlocks(source.ID, target.ID, from, to);

        return this;
    }

    /// <summary>
    /// Creates a wire connection between two tag elements in a <see cref="Sheet"/>.
    /// </summary>
    /// <param name="from">The source <see cref="TagName"/> representing the starting point of the connection.</param>
    /// <param name="to">The target <see cref="TagName"/> representing the endpoint of the connection.</param>
    /// <returns>The current <see cref="Sheet"/> instance with the wire connection established.</returns>
    /// <exception cref="ArgumentNullException">Thrown if either <paramref name="from"/> or <paramref name="to"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the source or target block cannot be resolved from the provided tag elements.
    /// </exception>
    public Sheet Connect(TagName from, TagName to)
    {
        if (from is null) throw new ArgumentNullException(nameof(from));
        if (to is null) throw new ArgumentNullException(nameof(to));
        
        var source = Element.Elements().SingleOrDefault(e => e.GetBlockOperand() == from.Root)?.Deserialize<Block>();
        var target = Element.Elements().SingleOrDefault(e => e.GetBlockOperand() == to.Root)?.Deserialize<Block>();

        if (source is null)
            throw new InvalidOperationException($"No source block with operand '{from.Root}' exists in the sheet.");
        
        if (target is null)
            throw new InvalidOperationException($"No target block with operand '{to.Root}' exists in the sheet.");

        WireBlocks(source.ID, target.ID, from.Path, to.Path);
        
        return this;
    }

    /// <summary>
    /// Gets the next highest ID value with the given set of diagram elements.
    /// </summary>
    private uint NextAvailableId()
    {
        return Element.Elements().Select(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>()).Max() + 1 ?? 0;
    }

    /// <summary>
    /// Adds a wire between two blocks, with optional parameters for specifying connection points.
    /// </summary>
    /// <param name="fromId">The ID of the source block to connect from.</param>
    /// <param name="toId">The ID of the target block to connect to.</param>
    /// <param name="fromParam">Optional parameter specifying the connection point on the source block.</param>
    /// <param name="toParam">Optional parameter specifying the connection point on the target block.</param>
    /// <exception cref="InvalidOperationException">Thrown if the block is not part of a sheet when attempting to add a wire.</exception>
    private void WireBlocks(uint fromId, uint toId, TagName? fromParam = null, TagName? toParam = null)
    {
        var existingWire = Element.Elements(L5XName.Wire).FirstOrDefault(w =>
            w.Attribute(L5XName.ToID)?.Value == toId.ToString() &&
            w.Attribute(L5XName.ToParam)?.Value == toParam
        );

        existingWire?.Remove();

        var wire = new XElement(L5XName.Wire);

        wire.SetAttributeValue(L5XName.FromID, fromId);
        wire.SetAttributeValue(L5XName.FromParam, fromParam);

        wire.SetAttributeValue(L5XName.ToID, toId);
        wire.SetAttributeValue(L5XName.ToParam, toParam);

        Element.Add(wire);
        EnsureOrder();
    }
}