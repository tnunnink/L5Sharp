using System;
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
    /// Determines if this connector connects either to or from the provided <c>DiagramBlock</c>.
    /// </summary>
    /// <param name="id">The id of the source block to find the connection to.</param>
    /// <param name="param">The parameter name of the source block to find the connection to.</param>
    /// <returns><c>true</c> if this wire has a ToId or FromId connecting the provided block; Otherwise, <c>false</c>.</returns>
    public Tuple<uint, string?>? Connection(uint id, string param)
    {
        return ToID == id && ToParam?.IsEquivalent(param) == true ? new Tuple<uint, string?>(FromID, FromParam)
            : FromID == id && FromParam?.IsEquivalent(param) == true ? new Tuple<uint, string?>(ToID, ToParam)
            : default;
    }
}