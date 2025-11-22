using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the base interface for Logix entities in a Logix project. This interface defines the essential
/// properties and methods that are common across all Logix entities, enabling consistent handling and
/// interaction with entities within the L5X environment.
/// </summary>
public interface ILogixEntity : ILogixElement
{
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
    Reference Reference { get; }

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
    Scope Scope { get; }

    /// <summary>
    /// Retrieves a collection of <see cref="Reference"/> that identify other entities that use this <see cref="ILogixEntity"/>.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Reference"/> representing the usages of the entity.
    /// </returns>
    /// <remarks>
    /// Usages represent places where this entity is used. These are often references by name in some other element
    /// property. Each deriving type must implement logic as needed to find all usages of this entity withing an L5X document.
    /// </remarks>
    IEnumerable<Reference> Usages();

    /// <summary>
    /// Retrieves a collection of <see cref="ILogixComponent"/> representing the dependencies of this entity.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="ILogixComponent"/> that the current entity requires to function. These components may
    /// represent relationships, references, or other entities that this entity depends on.
    /// </returns>
    /// <remarks>
    /// Dependencies are other entities that must exist or be available for this entity to fully function or define its context.
    /// Each implementation must provide the logic to resolve and return these dependencies as appropriate within a Logix application.
    /// </remarks>
    IEnumerable<ILogixEntity> Dependencies();
}

/// <summary>
/// Represents a base class for all Logix entities in the L5X project environment. This class provides fundamental
/// functionality and shared behaviors for concrete Logix entity types, including operations to determine references,
/// scope, dependencies, and component resolution within the project structure.
/// </summary>
/// <typeparam name="TEntity">
/// The type of the derived entity, constrained to inherit from LogixEntity itself. This type parameter ensures
/// type safety and enforces the inheritance structure of Logix entities.
/// </typeparam>
public abstract class LogixEntity<TEntity> : LogixObject<TEntity>, ILogixEntity where TEntity : LogixEntity<TEntity>
{
    /// <inheritdoc />
    protected LogixEntity(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixEntity(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public virtual Reference Reference => Reference.To(Element);

    /// <inheritdoc />
    public Scope Scope => Scope.Of(Element);

    /// <inheritdoc />
    public abstract IEnumerable<Reference> Usages();

    /// <inheritdoc />
    public abstract IEnumerable<ILogixEntity> Dependencies();

    /// <summary>
    /// Attempts to resolve a component of the specified type and location within the context of the current entity instance.
    /// </summary>
    /// <param name="name">The string representing the location of the component to resolve.</param>
    /// <param name="component">The output parameter that will contain the resolved component of type <typeparamref name="TComponent"/> if successful.</param>
    /// <typeparam name="TComponent">The type of the component to resolve.</typeparam>
    /// <returns>True if the component was successfully resolved; otherwise, false.</returns>
    /// <remarks>
    /// This method helps find the actual defined component in the L5X context when we only have a weak reference (type/name) to the object.
    /// For instance, if we have a tag name, we can resolve it to the exact tag instance defined (either local or global) in the L5X based
    /// on the scope of this entity. This should work for any component/reference type but requires this entity is attached to an L5X document.
    /// </remarks>
    protected bool TryResolve<TComponent>(string name, out TComponent component)
        where TComponent : LogixComponent<TComponent>
    {
        component = null!;
        if (!TryGetDocument(out var doc)) return false;
        if (string.IsNullOrEmpty(name)) return false;

        var reference = Reference.To<TComponent>(name);

        if (reference.Type.SupportsScope && doc.TryGet<TComponent>(reference.ToScope(Scope), out var local))
        {
            component = local;
            return true;
        }

        if (doc.TryGet<TComponent>(reference, out var global))
        {
            component = global;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Attempts to resolve the specified type name to a corresponding <see cref="ILogixComponent"/> instance
    /// within the context of the provided <see cref="ILogixEntity"/>.
    /// </summary>
    /// <param name="typeName">
    /// The name of the type to resolve. This is typically the name of a user-defined type (UDT)
    /// or Add-On Instruction (AOI) within the Logix document.
    /// </param>
    /// <param name="type">
    /// An output parameter that will contain the resolved <see cref="ILogixComponent"/> instance if the resolution succeeds.
    /// The value will be <c>null</c> if the resolution fails.
    /// </param>
    /// <returns>
    /// <c>true</c> if the specified type name was successfully resolved to a corresponding <see cref="ILogixComponent"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method attempts to locate and resolve the specified type name by searching the relevant Logix document.
    /// It supports resolving both user-defined data types (UDTs) and Add-On Instructions (AOIs).
    /// </remarks>
    protected bool TryResolveType(string typeName, out ILogixComponent type)
    {
        type = null!;
        if (!TryGetDocument(out var doc)) return false;
        if (string.IsNullOrEmpty(typeName)) return false;

        if (doc.TryGet<DataType>(typeName, out var udt))
        {
            type = udt;
            return true;
        }

        if (doc.TryGet<AddOnInstruction>(typeName, out var aoi))
        {
            type = aoi;
            return true;
        }

        return false;
    }
}