using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramElement</c> type that defines the properties for a call to a <c>Routine</c>.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.JSR)]
[L5XType(L5XName.SBR)]
[L5XType(L5XName.RET)]
[L5XType(L5XName.SbrRet)]
public class DiagramRoutine : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramRoutine"/> with default values.
    /// </summary>
    public DiagramRoutine()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramRoutine"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DiagramRoutine(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// The name of the routine to call for the <c>DiagramRoutine</c> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name of the routine if found; Otherwise, <c>null</c>.</value>
    public string? Routine
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    /// <summary>
    /// A collection of input parameter names for the <c>DiagramRoutine</c>.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing the names of the parameters if found. If not found then an
    /// empty collection.</value>
    /// <remarks>To update the property, you must assign a new collection of <see cref="string"/> names.</remarks>
    public IEnumerable<string> In
    {
        get => GetValue<string>()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList() ??
               Enumerable.Empty<string>();
        set => SetValue(string.Join(' ', value));
    }

    /// <summary>
    /// A collection of return parameter names for the <c>DiagramRoutine</c>.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing the names of the parameters if found. If not found then an
    /// empty collection.</value>
    /// <remarks>To update the property, you must assign a new collection of <see cref="string"/> names.</remarks>
    public IEnumerable<string> Ret
    {
        get => GetValue<string>()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList() ??
               Enumerable.Empty<string>();
        set => SetValue(string.Join(' ', value));
    }
    
    /// <inheritdoc />
    public override IEnumerable<LogixReference> References()
    {
        if (Routine is not null)
            yield return new LogixReference(Element, Routine, L5XName.Routine);
        
        foreach (var parameter in In)
            if (parameter.IsTagName())
                yield return new LogixReference(Element, parameter, L5XName.Tag);
        
        foreach (var parameter in Ret)
            if (parameter.IsTagName())
                yield return new LogixReference(Element, parameter, L5XName.Tag);
    }
}