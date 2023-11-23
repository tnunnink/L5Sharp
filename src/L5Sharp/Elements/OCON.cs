using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramBlock</c> type that defines the properties for output reference blocks in a Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>OutReference</c> is used to map tags or immediate values to the pin or parameters or a function, block, or AOI
/// <c>DiagramBlock</c> within a FBD. A <see cref="Sheet"/> will contain both input and output
/// references which are different types with the same properties.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.OCon, L5XName.Sheet)]
public class OCON : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="OCON"/> with the required connector name value.
    /// </summary>
    /// <param name="name">The name of the connector.</param>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
    public OCON(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Creates a new <see cref="OCON"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public OCON(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name identifying the connector element.
    /// </summary>
    public string Name
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }
    
    /// <summary>
    /// The <see cref="ICON"/> pair block that represents the other end of this <see cref="OCON"/> connection.
    /// </summary>
    /// <value>A <see cref="ICON"/> block element with the same connector name.</value>
    public ICON? Pair => Sheet?.Blocks<ICON>().FirstOrDefault(b => b.Name == Name);

    /// <inheritdoc />
    public override Instruction ToInstruction()
    {
        return new Instruction(nameof(OCON), Name);
    }

    /// <inheritdoc />
    protected override IEnumerable<Argument> GetArguments(string? param = null)
    {
        return Pair is null ? Enumerable.Empty<Argument>() : Pair.Endpoints(param);
    }
}