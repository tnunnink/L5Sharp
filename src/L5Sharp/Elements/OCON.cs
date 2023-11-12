using System;
using System.Xml.Linq;
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
    /// Creates a new <see cref="OCON"/> with default values.
    /// </summary>
    public OCON()
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="OCON"/> with the provided <c>operand</c> value.
    /// </summary>
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
    public string? Name
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}