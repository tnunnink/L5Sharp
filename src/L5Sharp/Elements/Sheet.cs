using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Sheet</c> block containing the properties for a L5X Sheet block.
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
[L5XType(L5XName.Sheet, L5XName.FBDContent)]
public class Sheet : Diagram<FunctionBlock>
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

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        var references = new List<CrossReference>();
        references.AddRange(Blocks<ReferenceBlock>().SelectMany(r => r.References()));
        references.AddRange(Blocks<Block>().SelectMany(r => r.References()));
        references.AddRange(Blocks<AddOnInstructionBlock>().SelectMany(r => r.References()));
        references.AddRange(Blocks<JsrBlock>().SelectMany(r => r.References()));
        return references;
    }

    public override void Connect(uint fromId, uint toId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    protected override IEnumerable<string> Ordering()
    {
        return new List<string>
        {
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
        };
    }
}