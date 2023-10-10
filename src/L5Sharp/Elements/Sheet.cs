using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Sheet</c> element containing the properties for a L5X Sheet element.
/// </summary>
/// <remarks>
/// A <c>Sheet</c> implements <see cref="LogixCode"/> and is the type of content that FBD routines contain.
/// <para>
/// Observe these guidelines when defining a controller:<br/>
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
///         have a unique ID number within that sheet.<br/>
/// </para>
/// </remarks>
public class Sheet : LogixCode
{
    /// <summary>
    /// Creates a new <see cref="Sheet"/> with default values.
    /// </summary>
    public Sheet()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Sheet"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Sheet(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The description of the <see cref="Sheet"/> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the description if it exists; Otherwise, <c>null</c></value>
    public string? Description
    {
        get => GetProperty<string>();
        set => SetDescription(value);
    }

    /// <summary>
    /// The collection of <c>IRef</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramReference> InputReferences => new(Element, L5XName.IRef);

    /// <summary>
    /// The collection of <c>ORef</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramReference> OutputReferences => new(Element, L5XName.ORef);

    /// <summary>
    /// The collection of <c>ICon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramConnector> InputConnectors => new(Element, L5XName.ICon);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramConnector> OutputConnectors => new(Element, L5XName.OCon);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramBlock> Blocks => new(Element, L5XName.Block);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramFunction> Functions => new(Element, L5XName.Function);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramInstruction> AddOnInstructions => new(Element, L5XName.AddOnInstruction);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramRoutine> JumpRoutines => new(Element, L5XName.JSR);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramRoutine> SubRoutines => new(Element, L5XName.SBR);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramRoutine> Returns => new(Element, L5XName.RET);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramWire> Wires => new(Element, L5XName.Wire);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramText> TextBoxes => new(Element, L5XName.TextBox);

    /// <summary>
    /// The collection of <c>OCon</c> elements within the <see cref="Sheet"/>.
    /// </summary>
    public LogixContainer<DiagramAttachment> Attachments => new(Element, L5XName.Attachment);

    /// <inheritdoc />
    public override IEnumerable<TagName> TagNames()
    {
        var list = new List<TagName>();

        list.AddRange(from reference in InputReferences
            where reference.Operand is not null && reference.Operand.IsTagName()
            select new TagName(reference.Operand));

        list.AddRange(from reference in OutputReferences
            where reference.Operand is not null && reference.Operand.IsTagName()
            select new TagName(reference.Operand));

        list.AddRange(from block in Blocks where block.Operand is not null select new TagName(block.Operand));

        foreach (var instruction in AddOnInstructions)
        {
            if (instruction.Operand is not null)
                list.Add(new TagName(instruction.Operand));

            list.AddRange(from parameter in instruction.Parameters
                where parameter.Value is not null && parameter.Value.IsTagName()
                select new TagName(parameter.Value));
        }

        return list;
    }

    /// <inheritdoc />
    public override IEnumerable<Instruction> Instructions()
    {
        var list = new List<Instruction>();

        list.AddRange(from block in Blocks where block.Type is not null select new Instruction(block.Type));
        list.AddRange(from function in Functions where function.Type is not null select new Instruction(function.Type));
        list.AddRange(from instruction in AddOnInstructions
            where instruction.Name is not null
            select new Instruction(instruction.Name));
        
        return list;
    }
}