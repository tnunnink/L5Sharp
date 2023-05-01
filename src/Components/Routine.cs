using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Routine</c> component. Contains the properties for a generic Routine element. This type does not
/// include content property. More specific routine types are derived from this base class.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixSerializer(typeof(RoutineSerializer))]
public class Routine : ILogixComponent, ILogixScoped
{
    /// <inheritdoc />
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Description { get; set; } = string.Empty;

    /// <inheritdoc />
    public Scope Scope { get; set; } = Scope.Null;

    /// <inheritdoc />
    public string Container { get; set; } = string.Empty;

    /// <summary>
    /// The type of the <see cref="Routine"/> component.
    /// </summary>
    /// <value>A <see cref="Enums.RoutineType"/> enum specifying the type content the routine contains.</value>
    public RoutineType Type { get; set; } = RoutineType.Typeless;

    /// <summary>
    /// The collection of <see cref="ILogixCode"/> objects that represent the content of the <see cref="Routine"/>.
    /// </summary>
    public List<ILogixCode> Content { get; set; } = new();
}