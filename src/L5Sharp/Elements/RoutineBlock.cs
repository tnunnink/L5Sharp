using System;
using System.Collections.Generic;
using System.Linq;
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
public abstract class RoutineBlock : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="RoutineBlock"/> with default values.
    /// </summary>
    protected RoutineBlock()
    {
    }

    /// <summary>
    /// Creates a new <see cref="JsrBlock"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    protected RoutineBlock(XElement element) : base(element)
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
    /// Gets a collection of <see cref="TagName"/> representing the parameters of the <see cref="RoutineBlock"/>.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing <see cref="TagName"/> values for both <c>In</c> and <c>Ret</c>
    /// routine parameters. If none found, an empty collection.</value>
    public IEnumerable<TagName> Parameters
    {
        get
        {
            var ins = Element.Attribute(L5XName.In)?.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries) ??
                      Enumerable.Empty<string>();
            var rets = Element.Attribute(L5XName.Ret)?.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries) ??
                      Enumerable.Empty<string>();
            return ins.Union(rets).Select(t => new TagName(t));
        }
    }
}