using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A Logix <c>Sheet</c> block containing the properties for a L5X Sheet or Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Sheet</c> implements <see cref="LogixCode"/> and is the type of content that FBD routines contain.
/// <para>
/// Observe these guidelines when defining a sheet:<br/>
///     • The sheets in the routine appear in order in the export file.
///         Each sheet section contains all the drawing elements and wires for that sheet. <br/>
///     • On import, sheet numbers are assigned based on order in the file, not on the number attribute on the sheet.<br/>
///     • The sheet name is stored as description on the sheet.<br/>
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
public class Sheet : Diagram
{
    private static readonly HashSet<string> BlockTypes = [..typeof(Block).L5XTypes()];

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
        set => SetDescription(value);
    }

    /// <summary>
    /// Adds a block to the sheet, assigns a new block id if already used, and sorts the blocks to maintain
    /// a valid diagram order. 
    /// </summary>
    /// <param name="block">The <see cref="Core.Block"/> element to add.</param>
    /// <exception cref="ArgumentNullException"><paramref name="block"/> is null.</exception>
    /// <returns>Ths id of the block that was added to the sheet.</returns>
    /// <remarks>
    /// This will update the block id to the next available id if the id is already used.
    /// Doing this will preserve the uniqueness of the id's and prevent import errors. This will also perform a sort of the
    /// current underlying diagram elements to ensure the order of the element is maintained, which is also required to
    /// prevent import errors.
    /// </remarks>
    public uint Add(Block block)
    {
        if (block is null)
            throw new ArgumentNullException(nameof(block));

        var element = block.Serialize();

        AssignId(element);
        Element.Add(element);
        EnsureOrder();
        return block.ID;
    }

    /// <summary>
    /// Adds a block to the sheet at the specified coordinates, assigns a new block id if already used, and sorts the
    /// blocks to maintain a valid diagram order. 
    /// </summary>
    /// <param name="x">The X coordinate at which to place the block being added.</param>
    /// <param name="y">The Y coordinate at which to place the block being added.</param>
    /// <param name="block">The <see cref="Core.Block"/> element to add.</param>
    /// <exception cref="ArgumentNullException"><paramref name="block"/> is null.</exception>
    /// <returns>Ths id of the block that was added to the sheet.</returns>
    /// <remarks>
    /// This will update the block id to the next available id if the id is already used.
    /// Doing this will preserve the uniqueness of the id's and prevent import errors. This will also perform a sort of the
    /// current underlying diagram elements to ensure the order of the element is maintained, which is also required to
    /// prevent import errors.
    /// </remarks>
    public uint AddAt(uint x, uint y, Block block)
    {
        if (block is null)
            throw new ArgumentNullException(nameof(block));

        block.X = x;
        block.Y = y;

        var element = block.Serialize();

        AssignId(element);
        Element.Add(element);
        EnsureOrder();
        return block.ID;
    }

    /// <summary>
    /// Adds the provided wire element to the sheet diagram and sorts the sheet elements to maintain a valid element order.
    /// </summary>
    /// <param name="wire">The <see cref="Wire"/> to add to the sheet.</param>
    /// <exception cref="ArgumentNullException"><paramref name="wire"/> is null.</exception>
    public void Add(Wire wire)
    {
        if (wire is null)
            throw new ArgumentNullException(nameof(wire));

        var element = wire.Serialize();

        Element.Add(element);
        EnsureOrder();
    }

    /// <summary>
    /// Finds a block with the specified block id. If not found, returns <c>null</c>.
    /// </summary>
    /// <param name="id">The zero based id of the block to find.</param>
    /// <returns>A <see cref="Core.Block"/> with the specified id if found; Otherwise null</returns>
    /// <remarks>
    /// 
    /// </remarks>
    public Block? Block(uint id)
    {
        return Element.Elements()
            .SingleOrDefault(e => e.Attribute(L5XName.ID)?.Value.Parse<uint>() == id)
            ?.Deserialize<Block>();
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
            .FirstOrDefault(e => e.Attribute(L5XName.Operand)?.Value == operand)
            ?.Deserialize<Block>();
    }

    /// <summary>
    /// Retrieves all <see cref="Core.Block"/> elements found in the sheet diagram.
    /// </summary>
    /// <returns>A collection of <see cref="Core.Block"/> elements found in the diagram.</returns>
    /// <remarks>
    /// </remarks>
    public IEnumerable<Block> Blocks()
    {
        return Element.Elements().Where(e => BlockTypes.Contains(e.L5XType())).Select(e => e.Deserialize<Block>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="from">The <see cref="TagName"/> operand and pin to wire from.</param>
    /// <param name="to">The <see cref="TagName"/> operand and pin to wire to.</param>
    public void Wire(TagName from, TagName to)
    {
        if (from is null || from.IsEmpty)
            throw new ArgumentException("Can not create wire for null or empty from tag.");
        if (to is null || to.IsEmpty)
            throw new ArgumentException("Can not create wire for null or empty to tag.");

        var source = Block(from.Root) ??
                     throw new InvalidOperationException($"Could not find source block from operand '{from}'");
        var destination = Block(to.Root) ??
                          throw new InvalidOperationException(
                              $"Could not find destination block from operand '{from}'");

        var wire = new Wire
        {
            FromID = source.ID,
            FromParam = !string.IsNullOrEmpty(from.Path) ? from.Path : default,
            ToID = destination.ID,
            ToParam = !string.IsNullOrEmpty(to.Path) ? to.Path : default
        };

        Add(wire);
    }

    /// <summary>
    /// Retrieves all wires that are connected to the specified block ID.
    /// </summary>
    /// <param name="id">The id to filter the wires by.</param>
    /// <returns>An collection of <see cref="Wire"/> elements that are connected to or from the specified id.</returns>
    public IEnumerable<Wire> Wires(uint id)
    {
        return Wires().Where(w => w.IsTo(id)).Concat(Wires().Where(w => w.IsFrom(id)));
    }

    /// <summary>
    /// Retrieves all wires found in the <see cref="Sheet"/>.
    /// </summary>
    /// <returns>A collection of <see cref="Wire"/> elements found in the sheet diagram.</returns>
    public IEnumerable<Wire> Wires()
    {
        return Element.Elements(L5XName.Wire).Select(e => e.Deserialize<Wire>());
    }
}