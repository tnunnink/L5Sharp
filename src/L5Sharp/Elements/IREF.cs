using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramBlock</c> type that defines the properties for input reference blocks in a Function Block Diagram (FBD).
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
[L5XType(L5XName.IRef, L5XName.Sheet)]
public class IREF : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="IREF"/> with default values.
    /// </summary>
    public IREF()
    {
        Operand = string.Empty;
        HideDesc = false;
    }

    /// <summary>
    /// Creates a new <see cref="IREF"/> with the provided <c>operand</c> value.
    /// </summary>
    public IREF(Argument operand) : this()
    {
        Operand = operand;
    }

    /// <summary>
    /// Creates a new <see cref="IREF"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public IREF(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// The tag name or immediate value for the reference element.
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
            yield return new CrossReference(Element, L5XName.Tag, Operand.ToString());
        }
    }

    /// <inheritdoc />
    protected override IEnumerable<Argument> GetArguments(KeyValuePair<uint, string?> endpoint)
    {
        yield return Operand ?? Argument.Unknown;
    }
}