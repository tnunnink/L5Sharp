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
[L5XType(L5XName.RET, L5XName.Sheet)]
public class RET : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="RET"/> with default values.
    /// </summary>
    public RET()
    {
    }

    /// <summary>
    /// Creates a new <see cref="RET"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public RET(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name of the routine to call for the <see cref="RET"/> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name of the routine if found; Otherwise, <c>null</c>.</value>
    public string? Routine
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The collection of input parameters to the routine being called by the <see cref="RET"/> <c>FunctionBlock</c> element.
    /// </summary>
    /// <value>A <see cref="Params"/> object wrapping the underlying attribute containing the <c>In</c> parameters
    /// if exists; Otherwise, <c>null</c>.</value>
    public Params? Ret
    {
        get => Element.Attribute(L5XName.In) is not null ? new Params(Element.Attribute(L5XName.In)!) : default;
        set => SetValue(value is not null ? string.Join(" ", value) : null);
    }

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        if (Routine is not null)
            yield return new CrossReference(Element, L5XName.Routine, Routine);

        if (Ret is null) yield break;

        foreach (var parameter in Ret)
            yield return new CrossReference(Element, L5XName.Tag, parameter);
    }
    
    /// <inheritdoc />
    protected override IEnumerable<Argument> GetArguments(KeyValuePair<uint, string?> endpoint)
    {
        yield return endpoint.Value is not null ? new TagName(endpoint.Value) : Argument.Empty;
    }
}