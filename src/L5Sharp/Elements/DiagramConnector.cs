using System;
using System.Collections.Generic;
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
        FromID = 0;
        ToID = 0;
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
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The ID of the destination <c>DiagramBlock</c> this <see cref="DiagramConnector"/> is connected to.
    /// </summary>
    public uint ToID
    {
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
    }
    
    /// <summary>
    /// Returns the connecting endpoint of this <c>Connector</c> element, which is a <see cref="KeyValuePair{TKey,TValue}"/>
    /// where the key/value is the ID and param of the block element opposite the provided block element.
    /// </summary>
    /// <param name="block">The block element for which to find the connected endpoint.</param>
    /// <returns>
    /// A <see cref="KeyValuePair{TKey,TValue}"/> containing the opposite block's ID and param the connector
    /// is connected to.
    /// </returns>
    /// <remarks>
    /// This makes it easier to find which block id and parameter the connector is attached to. Note that
    /// for a generic <see cref="DiagramConnector"/> the parameter will always be <c>null</c> since it does not
    /// define a To/From parameter. The <c>Wire</c> connector will override this implementation to return it's
    /// associated param name.
    /// </remarks>
    public KeyValuePair<uint, string?> Endpoint(DiagramBlock block) => Endpoint(block.ID);
    
    /// <summary>
    /// Returns the connecting endpoint of this <c>Connector</c> element, which is a <see cref="KeyValuePair{TKey,TValue}"/>
    /// where the key/value is the ID/Param of the block element opposite the provided block element.
    /// </summary>
    /// <param name="id">The ID of the block element for which to find the connected endpoint.</param>
    /// <returns>
    /// A <see cref="KeyValuePair{TKey,TValue}"/> containing the opposite block's ID and Param the connector
    /// is connected to.
    /// </returns>
    /// <remarks>
    /// This makes it easier to find which block id and parameter the connector is attached to. Note that
    /// for a generic <see cref="DiagramConnector"/> the parameter will always be <c>null</c> since it does not
    /// define a To/From parameter. The <c>Wire</c> connector will override this implementation to return it's
    /// associated param name.
    /// </remarks>
    public virtual KeyValuePair<uint, string?> Endpoint(uint id)
    {
        return FromID == id ? new KeyValuePair<uint, string?>(ToID, default) 
            : ToID == id ? new KeyValuePair<uint, string?>(FromID, default) 
            : throw new ArgumentException($"The connector does not have a to/from id matching the id '{id}'");
    }

    /// <summary>
    /// Determines if this <c>Connector</c> has a connection or endpoint to the provided <c>Block</c>.
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    public bool IsConnected(DiagramBlock block) => block.ID == FromID || block.ID == ToID;
    
    /// <summary>
    /// Determines if this <c>Connector</c> has a connection or endpoint to the provided <c>Block</c>.
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    public bool IsConnectedTo(DiagramBlock block) => block.ID == ToID;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    public bool IsConnectedFrom(DiagramBlock block) => block.ID == FromID;
}