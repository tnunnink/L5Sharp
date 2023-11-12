using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// Represents the containing element for Sequential Function Chart (SFC) block elements. This class inherits
/// the common <see cref="Diagram{TBlock,TConnector}"/> logic and allows easier manipulation for SFC elements
/// within a SFC type routine.
/// </summary>
public class Chart : Diagram<SequenceBlock, DirectedLink>
{
    /// <summary>
    /// Creates a new <see cref="Chart"/> with default values.
    /// </summary>
    public Chart()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Chart"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    public Chart(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override int Number => 0;

    /// <inheritdoc />
    protected override IEnumerable<string> Ordering()
    {
        return new List<string>
        {
            L5XName.Step,
            L5XName.Transition,
            L5XName.Condition,
            L5XName.SbrRet,
            L5XName.Stop,
            L5XName.Branch,
            L5XName.DirectedLink,
            L5XName.TextBox,
            L5XName.Attachment
        };
    }
}