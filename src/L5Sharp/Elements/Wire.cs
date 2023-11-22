using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>LogixElement</c> type that defines the properties for a wire connector within a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Connector</c> is not a <see cref="DiagramBlock"/> itself since is does not have the location and ID properties.
/// It simply maps the connections of pins within a diagram.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Wire, L5XName.Sheet)]
public class Wire : DiagramConnector
{
    /// <summary>
    /// Creates a new <see cref="Wire"/> with default values.
    /// </summary>
    public Wire()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Wire"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>block</c> is null.</exception>
    public Wire(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The parameter name of source <c>DiagramBlock</c> pin this wire is connected to.
    /// </summary>
    public string? FromParam
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The parameter name of destination <c>DiagramBlock</c> pin this wire is connected to.
    /// </summary>
    public string? ToParam
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The parent <see cref="Elements.Sheet"/> element that this <c>FunctionBlock</c> is contained within.
    /// </summary>
    public Sheet? Sheet => Element.Parent is not null ? new Sheet(Element.Parent) : default;

    /// <inheritdoc />
    /// <remarks>
    /// This makes it easier to find which block id and parameter the connector is attached to. This class is overriding
    /// the default implementation to return the local <see cref="FromParam"/> or <see cref="ToParam"/> depending on the
    /// associated to/from ID of the endpoint.
    /// </remarks>
    public override KeyValuePair<uint, string?> Endpoint(uint id)
    {
        return FromID == id ? new KeyValuePair<uint, string?>(ToID, ToParam) 
            : ToID == id ? new KeyValuePair<uint, string?>(FromID, FromParam) 
            : throw new ArgumentException($"The connector does not have a to/from id matching the id '{id}'");
    }
}