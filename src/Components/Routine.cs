using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Routine</c> component. Contains the properties for a generic Routine element. This type does not
/// include content property. More specific routine types are derived from this base class.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Routine : LogixComponent<Routine>, ILogixScoped
{
    /// <inheritdoc />
    public Routine()
    {
    }

    /// <inheritdoc />
    public Routine(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public Scope Scope => Scope.FromElement(Element);

    /// <inheritdoc />
    public string Container => Element.Ancestors(Scope.XName).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <summary>
    /// The type of the <see cref="Routine"/> component.
    /// </summary>
    /// <value>A <see cref="Enums.RoutineType"/> enum specifying the type content the routine contains.</value>
    public RoutineType Type
    {
        get => GetValue<RoutineType>() ?? throw new XmlException();
        set => SetValue(value);
    }

    /// <summary>
    /// The collection of <see cref="ILogixCode"/> objects that represent the content of the <see cref="Routine"/>.
    /// </summary>
    public List<ILogixCode> Content { get; set; } = new();
}