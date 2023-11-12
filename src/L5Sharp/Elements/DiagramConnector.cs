using System;
using System.Xml.Linq;

namespace L5Sharp.Elements;

/// <summary>
/// An abstraction for the different diagram connector elements such as <c>Wire</c> and <c>DirectLink</c> which have
/// shared to/from id properties. <see cref="DiagramConnector"/> elements will define how <see cref="DiagramBlock"/>
/// elements are connected within a containing <see cref="Diagram{TBlock,TConnector}"/>.
/// </summary>
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

    /// <summary>
    /// Determines if this connector connects either to or from the provided <c>DiagramBlock</c>.
    /// </summary>
    /// <param name="block">The block to determine the connection to/from.</param>
    /// <returns><c>true</c> if this wire has a ToId or FromId connecting the provided block; Otherwise, <c>false</c>.</returns>
    public bool IsConnected(DiagramBlock block) => ToID == block.ID || FromID == block.ID;

    /// <summary>
    /// Determines if this wire connects to the provided <c>DiagramBlock</c>.
    /// </summary>
    /// <param name="block">The block to determine the connection to.</param>
    /// <returns><c>ture</c> if this wire has a ToID connecting the provided block; Otherwise, <c>false</c>.</returns>
    public bool ConnectsTo(DiagramBlock block) => ToID == block.ID;

    /// <summary>
    /// Determines if this wire has a connection from the provided <c>DiagramBlock</c>.
    /// </summary>
    /// <param name="block">The block to determine the connection to.</param>
    /// <returns><c>ture</c> if this wire has a ToID connecting the provided block; Otherwise, <c>false</c>.</returns>
    public bool ConnectsFrom(DiagramBlock block) => FromID == block.ID;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    public uint? Connected(DiagramBlock block) => ToID == block.ID ? FromID : FromID == block.ID ? ToID : default;
}