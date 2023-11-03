using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramBlock</c> type that defines the properties for a call to a <c>Routine</c>.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.SBR, L5XName.Sheet)]
public class SbrBlock : DiagramBlock
{
    /// <summary>
    /// Creates a new <see cref="SbrBlock"/> with default values.
    /// </summary>
    public SbrBlock()
    {
    }

    /// <summary>
    /// Creates a new <see cref="SbrBlock"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public SbrBlock(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name of the routine to call for the <c>JSR</c> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name of the routine if found; Otherwise, <c>null</c>.</value>
    public string? Routine
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// A collection of input parameter names for the <c>JSR</c>.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing the names of the parameters if found. If not found then an
    /// empty collection.</value>
    /// <remarks>To update the property, you must assign a new collection of <see cref="string"/> names.</remarks>
    public IEnumerable<TagName> In => throw new NotImplementedException();

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        if (Routine is not null)
            yield return new CrossReference(Element, Routine, L5XName.Routine);

        foreach (var parameter in In)
            yield return new CrossReference(Element, parameter, L5XName.Tag);
    }
}