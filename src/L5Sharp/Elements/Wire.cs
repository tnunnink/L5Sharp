using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <c>LogixElement</c> type that defines the properties for a wire connector within a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Connector</c> is not a <see cref="DiagramElement"/> itself since is does not have the location and ID properties.
/// It simply maps the connections of pins within a diagram.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Wire, L5XName.Sheet)]
public class Wire : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Wire"/> with default values.
    /// </summary>
    public Wire()
    {
        Element.SetAttributeValue(L5XName.FromID, 0);
        Element.SetAttributeValue(L5XName.ToID, 0);
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
    /// The ID of the source block the wire connection is from. 
    /// </summary>
    /// <value>A <see cref="uint"/> indicating the source block ID.</value>
    public uint FromID
    {
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The parameter name of source block's pin this wire connection is from.
    /// </summary>
    /// <value>A <see cref="string"/> parameter name if found on the wire element. Otherwise, null.</value>
    /// <remarks>
    /// Some wires connect from blocks that don't have pins. For example, an IREF block contain a single
    /// reference and no pin parameter, and therefore will have a null FromParam for the wire element connected to it.
    /// </remarks>
    public string? FromParam
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The ID of the destination block the wire connection is to. 
    /// </summary>
    /// <value>A <see cref="uint"/> indicating the destination block ID.</value>
    public uint ToID
    {
        get => GetRequiredValue<uint>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The parameter name of destination block's pin this wire connection is to.
    /// </summary>
    /// <value>A <see cref="string"/> parameter name if found on the wire element; Otherwise, null.</value>
    /// <remarks>
    /// Some wires connect to blocks that don't have pins. For example, OREF blocks contain a single
    /// reference and no pin parameter, and therefore will have a null ToParam for the wire element connected to it.
    /// </remarks>
    public string? ToParam
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The parent <see cref="Core.Sheet"/> element that this <c>FunctionBlock</c> is contained within.
    /// </summary>
    public Sheet? Sheet => Element.Parent is not null ? new Sheet(Element.Parent) : default;

    /// <summary>
    /// Determines if this wire element is connected from the provided block ID and optional param name.
    /// </summary>
    /// <param name="id">The ID of the block to check connection from.</param>
    /// <param name="param">The parameter/pin of the block to check connection from.</param>
    /// <returns><c>true</c> if this wire is connected from the specified block ID and param name; Otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Note that param is optional. If not provided then the null comparison will be checked, and return true
    /// only if this wire has the specified FromID and a null FromParam value.
    /// </remarks>
    public bool IsFrom(uint id, string? param = null) => FromID == id && FromParam == param;

    /// <summary>
    /// Determines if this wire element is connected to the provided block ID and optional param name.
    /// </summary>
    /// <param name="id">The ID of the block to check connection to.</param>
    /// <param name="param">The parameter/pin of the block to check connection to.</param>
    /// <returns><c>true</c> if this wire is connected to the specified block ID and param name; Otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Note that param is optional. If not provided then the null comparison will be checked, and return true
    /// only if this wire has the specified ToID and a null ToParam value.
    /// </remarks>
    public bool IsTo(uint id, string? param = null) => ToID == id && ToParam == param;
}