using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Common;

namespace L5Sharp.Elements;

public class Chart : Diagram<SequenceBlock>
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
    public override IEnumerable<CrossReference> References()
    {
        throw new NotImplementedException();
    }

    protected override IEnumerable<string> Ordering()
    {
        throw new NotImplementedException();
    }

    public override void Connect(uint fromId, uint toId)
    {
        throw new NotImplementedException();
    }
}