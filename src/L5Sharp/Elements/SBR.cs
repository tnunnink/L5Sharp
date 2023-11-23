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
public class SBR : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="SBR"/> with default values.
    /// </summary>
    public SBR()
    {
    }

    /// <summary>
    /// Creates a new <see cref="SBR"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public SBR(XElement element) : base(element)
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
    /// The collection of input parameters to the routine being called by the <see cref="SBR"/> <c>FunctionBlock</c> element.
    /// </summary>
    /// <value>A <see cref="Params"/> object wrapping the underlying attribute containing the <c>In</c> parameters
    /// if exists; Otherwise, <c>null</c>.</value>
    public Params? In
    {
        get => Element.Attribute(L5XName.In) is not null ? new Params(Element.Attribute(L5XName.In)!) : default;
        set => SetValue(value is not null ? string.Join(" ", value) : null);
    }

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        if (Routine is not null)
            yield return new CrossReference(Element, L5XName.Routine, Routine);

        if (In is null) yield break;
        
        foreach (var parameter in In)
            yield return new CrossReference(Element, L5XName.Tag, parameter);
    }

    /// <inheritdoc />
    public override Instruction ToInstruction()
    {
        return Instruction.JSR.Of(); //todo get args from params
    }

    /// <inheritdoc />
    protected override IEnumerable<Argument> GetArguments(string? param = null)
    {
        yield return param is not null ? new TagName(param) : Argument.Empty;
    }
}