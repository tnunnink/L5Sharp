using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a type that is uniquely referencable by name (component elements) or number (code elements),
/// and that provides the ability to query for usages and dependencies of the entity in the current L5X context. 
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
    /// Gets a <see cref="Scope"/> object representing the hierarchical context of the current entity in the Logix project.
    /// </summary>
    /// <value>A <see cref="Scope"/> created from the underlying <see cref="XElement"/> of the entity.</value>
    /// <remarks>
    /// The <see cref="Scope"/> defines the logical container or boundary in which the entity resides, such as a program,
    /// routine, or global context. It provides functionality to assess the visibility, accessibility, and
    /// hierarchical relationships of the current entity within the L5X file.
    /// </remarks>
    Scope Scope { get; }

    /// <summary>
    /// Retrieves a collection of <see cref="Reference"/> that indicate where this entity is used within the current project.
    /// </summary>
    /// <returns>A collection of <see cref="Reference"/> representing the usages of this entity.</returns>
    /// <remarks>
    /// Usages represent references to other entities that use this entity. This is similar to the cross-referencing
    /// mechanism in Studio 5k and is meant to resemble it at some level. Each deriving type must implement
    /// logic as needed to find all usages of this entity withing an L5X document.
    /// </remarks>
    IEnumerable<Reference> Usages();

    /// <summary>
    /// Retrieves a collection of <see cref="ILogixEntity"/> that this entity depends on to be valid within the current project.
    /// </summary>
    /// <returns>A collection of <see cref="ILogixEntity"/> that the current entity depends on.</returns>
    /// <remarks>
    /// Dependencies are other entities that must exist for this entity to be resolved/function within a L5X project.
    /// Each implementation must provide the logic to resolve and return these dependencies as appropriate.
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
    /// <param name="component">The resolved component of type <typeparamref name="TComponent"/> if successful.</param>
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
    /// <param name="typeName">The name of the type to resolve. This is the name of a user-defined type (UDT) or Add-On Instruction (AOI).</param>
    /// <param name="type">The resolved <see cref="ILogixComponent"/> instance if the resolution succeeds.</param>
    /// <returns>
    /// <c>true</c> if the specified type name was successfully resolved to a corresponding <see cref="ILogixComponent"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method attempts to locate and resolve the specified type name by searching the relevant Logix document.
    /// It supports resolving both DataType and AddOnInstruction components.
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