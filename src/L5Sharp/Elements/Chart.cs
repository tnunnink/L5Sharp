using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

[L5XType(L5XName.SFCContent)]
public class Chart : LogixCode
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
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Chart(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override int Number
    {
        get => 0;
        set => throw new NotSupportedException("Setting the number of a SFC is not supported.");
    }

    /// <inheritdoc />
    public override IEnumerable<TagName> TagNames()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override IEnumerable<Instruction> Instructions()
    {
        throw new NotImplementedException();
    }
}