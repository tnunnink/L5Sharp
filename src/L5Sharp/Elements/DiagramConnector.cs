using System;
using System.Xml.Linq;

namespace L5Sharp.Elements;

public abstract class DiagramConnector : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramConnector"/> with default values.
    /// </summary>
    protected DiagramConnector()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramConnector"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    protected DiagramConnector(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// The ID of the source <c>DiagramBlock</c> this <see cref="DiagramConnector"/> is connected to.
    /// </summary>
    public uint FromID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// The ID of the destination <c>DiagramBlock</c> this <see cref="DiagramConnector"/> is connected to.
    /// </summary>
    public uint ToID
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }
}