using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents an abstract base class for entities in the Logix platform.
/// </summary>
/// <remarks>
/// A LogixEntity is the foundational construct used to define core objects
/// within the Logix ecosystem. It inherits from <see cref="LogixObject"/>
/// and provides functionality for managing references, scope, and dependencies.
/// </remarks>
public abstract class LogixEntity : LogixObject
{
    /// <inheritdoc />
    protected LogixEntity(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixEntity(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets a <see cref="Reference"/> object representing the current element's path within the L5X file structure.
    /// </summary>
    /// <value>A <see cref="Reference"/> created from the underlying <see cref="XElement"/> of the entity.</value>
    /// <remarks>
    /// <para>
    /// The <see cref="Reference"/> provides context and identification for an element in the L5X file. It is commonly
    /// used for locating and interacting with elements within the file, ensuring precise references are maintained across
    /// operations. This property is particularly useful when dealing with constructs like components, containers, or other
    /// hierarchical elements.
    /// </para>
    /// <para>
    /// The reference encapsulates the necessary information to trace an entity to its relevant scope or location,
    /// which is critical for ensuring the integrity of further element operations within an L5X document.
    /// </para>
    /// </remarks>
    public virtual Reference Reference => Reference.To(Element);

    /// <summary>
    /// Gets a <see cref="Scope"/> object representing the hierarchical or functional context of the current entity in the Logix project.
    /// </summary>
    /// <value>A <see cref="Scope"/> created from the underlying <see cref="XElement"/> of the entity.</value>
    /// <remarks>
    /// The <see cref="Scope"/> defines the logical container or boundary in which the entity resides, such as a program,
    /// routine, or global context. It provides critical functionality to assess the visibility, accessibility, and
    /// hierarchical relationships of the current entity within the L5X file.
    /// This property is essential for determining the scope's level, whether global or local, and provides insights
    /// into its container details. The <see cref="Scope"/> also facilitates operations such as peer or visibility checks,
    /// enabling robust scope management within the Logix environment.
    /// </remarks>
    public Scope Scope => Scope.Of(Element);

    /// <summary>
    /// Gets a collection of <see cref="Reference"/> objects that indicate the <see cref="LogixEntity"/> instances that
    /// use or reference this entity within the current L5X project.
    /// </summary>
    /// <returns>A collection of <see cref="Reference"/> objects representing the usages of the entity.</returns>
    public virtual IEnumerable<Reference> Usages() => [];

    /// <summary>
    /// Retrieves a collection of <see cref="LogixEntity"/> objects that the current entity depends on.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="LogixEntity"/> representing the entities on which the current entity relies.
    /// If no dependencies exist, an empty collection is returned.
    /// </returns>
    /// <remarks>
    /// Dependencies are references to components that this component would rely on to import correctly. This
    /// is the primary use of this implementation; so that we can export/import components to maintain a valid L5X project.
    /// This method can also be viewed as returning outbound references to this object.
    /// </remarks>
    public virtual IEnumerable<LogixComponent> Dependencies() => [];

    /// <summary>
    /// Attempts to resolve a component of the specified type and location within the context of the current entity instance.
    /// </summary>
    /// <param name="name">The string representing the location of the component to resolve.</param>
    /// <param name="component">The output parameter that will contain the resolved component of type <typeparamref name="TComponent"/> if successful.</param>
    /// <typeparam name="TComponent">The type of the component to resolve, which must inherit from <see cref="LogixComponent"/>.</typeparam>
    /// <returns>True if the component was successfully resolved; otherwise, false.</returns>
    /// <remarks>
    /// This method helps find the actual defined component in the L5X context when we only have a weak reference (type/name) to the object.
    /// For instance, if we have a tag name, we can resolve it to the exact tag instance defined (either local or global) in the L5X based
    /// on the scope of this entity. This should work for any component/reference type but requires this entity is attached to an L5X document.
    /// </remarks>
    protected bool TryResolve<TComponent>(string name, out TComponent component) where TComponent : LogixComponent
    {
        component = null!;
        if (Document is null) return false;
        if (string.IsNullOrEmpty(name)) return false;

        var reference = Reference.To<TComponent>(name);

        if (reference.Type.SupportsScope && Document.TryGet<TComponent>(reference.ToScope(Scope), out var local))
        {
            component = local;
            return true;
        }

        if (Document.TryGet<TComponent>(reference, out var global))
        {
            component = global;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Attempts to resolve the specified type name to a corresponding <see cref="LogixComponent"/>
    /// using the available document data.
    /// </summary>
    /// <param name="typeName">The name of the type to be resolved.</param>
    /// <param name="dataType">
    /// When the method returns, contains the resolved <see cref="LogixComponent"/> if the type name is successfully resolved,
    /// or null if the resolution fails. This instance will either be a <see cref="DataType"/> or <see cref="AddOnInstruction"/>
    /// depending on where it was found.
    /// </param>
    /// <returns>true if the type name is successfully resolved to a <see cref="LogixComponent"/>; otherwise, false.</returns>
    protected bool TryResolveType(string typeName, out LogixComponent dataType)
    {
        dataType = null!;
        if (Document is null) return false;
        if (string.IsNullOrEmpty(typeName)) return false;

        if (Document.TryGet<DataType>(typeName, out var udt))
        {
            dataType = udt;
            return true;
        }

        if (Document.TryGet<AddOnInstruction>(typeName, out var aoi))
        {
            dataType = aoi;
            return true;
        }

        return false;
    }
}