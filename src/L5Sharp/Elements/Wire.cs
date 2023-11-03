using System;
using System.Collections.Generic;
using System.Linq;
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
public class Wire : LogixElement
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
    /// The ID of the source <c>DiagramBlock</c> this wire is connected to.
    /// </summary>
    public uint FromID
    {
        get => GetValue<uint>();
        set => SetValue(value);
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
    /// The ID of the destination <c>DiagramBlock</c> this wire is connected to.
    /// </summary>
    public uint ToID
    {
        get => GetValue<uint>();
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
    /// <param name="block">The block to determine the connection to/from.</param>
    /// <returns><c>true</c> if this wire has a ToId or FromId connecting the provided block; Otherwise, <c>false</c>.</returns>
    public bool Connects(DiagramBlock block) => ToID == block.ID || FromID == block.ID;
    
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
}