using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the content for Sequential Function Chart (SFC) elements.
/// This class is currently not supported. It's mostly here to prevent deserialization issues with SFCContent.
/// </summary>
[LogixElement(L5XName.SFCContent)]
public class Chart : LogixCode<Chart>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Step,
        L5XName.Transition,
        L5XName.Condition,
        L5XName.SbrRet,
        L5XName.Stop,
        L5XName.Branch,
        L5XName.DirectedLink,
        L5XName.TextBox,
        L5XName.Attachment
    ];

    /// <summary>
    /// Creates a new <see cref="Chart"/> with default values.
    /// </summary>
    public Chart() : base(L5XName.SFCContent)
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
    public override IEnumerable<Instruction> Instructions()
    {
        throw new NotSupportedException("Chart code elements currently do not support parsing instructions.");
    }

    /// <inheritdoc />
    public override IEnumerable<TagName> Tags()
    {
        throw new NotSupportedException("Chart code elements currently do not support parsing tags.");
    }

    /// <inheritdoc />
    public override IEnumerable<ILogixEntity> Dependencies() => [];
}