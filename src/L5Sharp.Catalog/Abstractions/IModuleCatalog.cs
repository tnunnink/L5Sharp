using L5Sharp.Core;

namespace L5Sharp.Catalog.Abstractions;

/// <summary>
/// Defines an interface for querying and retrieving module definitions based on catalog numbers.
/// </summary>
/// <remarks>
/// Implementations of this interface provide mechanisms for accessing <see cref="ModuleDefinition"/> objects
/// by catalog number.
/// </remarks>
public interface IModuleCatalog
{
    /// <summary>
    /// Creates a new <see cref="Module"/> instance using the specified catalog number and name.
    /// </summary>
    /// <param name="name">The name to assign to the new module.</param>
    /// <param name="catalogNumber">The catalog number of the module to create.</param>
    /// <param name="config">An optional configuration delegate to apply to the created module.</param>
    /// <returns>A new <see cref="Module"/> instance initialized with values from the latest revision of the catalog definition.</returns>
    /// <remarks>This overload will use the latest (highest revision) module definition found in the catalog.</remarks>
    Module Create(string name, string catalogNumber, Action<Module>? config = null);

    /// <summary>
    /// Creates a new <see cref="Module"/> instance using the specified catalog number, name, and revision.
    /// </summary>
    /// <param name="name">The name to assign to the new module.</param>
    /// <param name="catalogNumber">The catalog number of the module to create.</param>
    /// <param name="revision">The specific <see cref="Revision"/> of the module to create.</param>
    /// <param name="config">An optional configuration delegate to apply to the created module.</param>
    /// <returns>A new <see cref="Module"/> instance initialized with values from the specified revision of the catalog definition.</returns>
    Module Create(string name, string catalogNumber, Revision revision, Action<Module>? config = null);

    /// <summary>
    /// Attempts to create a new <see cref="Module"/> instance using the specified catalog number and name.
    /// </summary>
    /// <param name="name">The name to assign to the new module.</param>
    /// <param name="catalogNumber">The catalog number of the module to create.</param>
    /// <param name="module">When this method returns, contains the created <see cref="Module"/> instance if successful; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the module was successfully created; otherwise, <c>false</c>.</returns>
    /// <remarks>This overload will use the latest (highest revision) module definition found in the catalog.</remarks>
    bool TryCreate(string name, string catalogNumber, out Module module);

    /// <summary>
    /// Attempts to create a new instance of the <see cref="Module"/> class based on the specified parameters.
    /// The operation will return a value indicating whether the creation was successful and, if so, an instance
    /// of the created module.
    /// </summary>
    /// <param name="name">The name to assign to the module.</param>
    /// <param name="catalogNumber">The catalog number of the module to create. This value specifies the type of module.</param>
    /// <param name="revision">The revision of the module to create, detailing the major and minor version information.</param>
    /// <param name="module">
    /// When this method returns, contains the created <see cref="Module"/> instance if the creation was successful;
    /// otherwise, contains <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the module was successfully created; otherwise, <c>false</c>.
    /// </returns>
    bool TryCreate(string name, string catalogNumber, Revision revision, out Module module);

    /// <summary>
    /// Retrieves a collection of <see cref="ModuleDefinition"/> objects that match the specified catalog number.
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the modules to be retrieved. If null, all modules are returned.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of type <see cref="ModuleDefinition"/> containing the modules
    /// that match the provided catalog number.
    /// </returns>
    IEnumerable<ModuleDefinition> Definitions(string? catalogNumber = null);

    /// <summary>
    /// Retrieves a <see cref="ModuleDefinition"/> object that matches the specified catalog number and revision.
    /// </summary>
    /// <param name="catalogNumber">The catalog number identifying the specific module to find.</param>
    /// <param name="revision">The revision of the module to retrieve. If null, this will return the
    /// latest (highest revision) definition found.</param>
    /// <returns>
    /// A <see cref="ModuleDefinition"/> that matches the provided catalog number and revision.
    /// If no matching module is found, an exception may be thrown depending on the implementation.
    /// </returns>
    ModuleDefinition GetDefinition(string catalogNumber, Revision? revision = null);

    /// <summary>
    /// Attempts to retrieve the latest <see cref="ModuleDefinition"/> for the specified catalog number.
    /// </summary>
    /// <param name="catalogNumber">The catalog number identifying the specific module definition to find.</param>
    /// <param name="definition">When this method returns, contains the <see cref="ModuleDefinition"/> found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if a definition was found; otherwise, <c>false</c>.</returns>
    bool TryGetDefinition(string catalogNumber, out ModuleDefinition definition);

    /// <summary>
    /// Attempts to retrieve a <see cref="ModuleDefinition"/> for the specified catalog number and revision.
    /// </summary>
    /// <param name="catalogNumber">The catalog number identifying the specific module definition to find.</param>
    /// <param name="revision">The revision of the module to retrieve.</param>
    /// <param name="definition">When this method returns, contains the <see cref="ModuleDefinition"/> found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if a definition was found; otherwise, <c>false</c>.</returns>
    bool TryGetDefinition(string catalogNumber, Revision revision, out ModuleDefinition definition);
}