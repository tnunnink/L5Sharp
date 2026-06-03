using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a local tag within a routine scope in a Logix application.
/// </summary>
/// <remarks>
/// Local tags are scoped to the routine in which they are defined and are not accessible outside that routine.
/// Unlike controller or program-scoped tags, local tags have a <see cref="Usage"/> of <see cref="TagUsage.Local"/>.
/// Local tags inherit from <see cref="Parameter"/> and can only contain atomic data types.
/// </remarks>
[LogixElement(L5XName.LocalTag)]
public sealed class LocalTag : Parameter
{
    /// <summary>
    /// Creates a new <see cref="LocalTag"/> with default values.
    /// </summary>
    public LocalTag() : base(L5XName.LocalTag)
    {
    }

    /// <summary>
    /// Creates a new <see cref="LocalTag"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> containing the LocalTag data.</param>
    public LocalTag(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="LocalTag"/> initialized with the provided name and value.
    /// </summary>
    /// <param name="name">The name of the LocalTag.</param>
    /// <param name="value">The <see cref="LogixData"/> value of the LocalTag.</param>
    /// <param name="description">the optional description of the LocalTag.</param>
    public LocalTag(string name, AtomicData value, string? description = null) : base(L5XName.LocalTag)
    {
        Element.SetAttributeValue(L5XName.Name, name);
        Default = value;
        SetProperty(description, nameof(Description));
    }

    /// <summary>
    /// Gets the tag usage type for the current instance, defining how the tag is used.
    /// </summary>
    /// <remarks>
    /// For <see cref="LocalTag"/> the usages is overriden to be hard coded to "local". Studio does not serialize this
    /// value for local tags, but it is inferred by is placement within the context of the L5X hierarchy.
    /// </remarks>
    public override TagUsage Usage => TagUsage.Local;
}