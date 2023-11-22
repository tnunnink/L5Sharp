using System;
using System.Collections.Generic;
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
[L5XType(L5XName.ORef, L5XName.Sheet)]
public class OREF : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="OREF"/> with default values.
    /// </summary>
    public OREF()
    {
        HideDesc = false;
    }

    /// <summary>
    /// Creates a new <see cref="OREF"/> with the provided <c>operand</c> value.
    /// </summary>
    public OREF(Argument operand)
    {
        Operand = operand;
        HideDesc = false;
    }

    /// <summary>
    /// Creates a new <see cref="OREF"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public OREF(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The tag name or immediate operand value for the reference element.
    /// </summary>
    /// <value>A <see cref="Argument"/> containing the reference if it exists; Otherwise, <c>null</c>.</value>
    public Argument? Operand
    {
        get => GetValue<Argument>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the reference element.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool? HideDesc
    {
        get => GetValue<bool?>();
        set => SetValue(value);
    }

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        if (Operand is not null && Operand.IsTag)
        {
            yield return new CrossReference(Element, L5XName.Tag, Operand.ToString(), new Instruction(nameof(IREF), Operand));
        }
    }
    
    /// <inheritdoc />
    protected override IEnumerable<Argument> GetArguments(KeyValuePair<uint, string?> endpoint)
    {
        yield return Operand ?? Argument.Unknown;
    }
}
