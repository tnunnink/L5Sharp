using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

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
    /// Retrieves a collection of <see cref="ModuleDefinition"/> objects that match the specified catalog number.
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the modules to be retrieved. If null, all modules are returned.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of type <see cref="ModuleDefinition"/> containing the modules
    /// that match the provided catalog number.
    /// </returns>
    IEnumerable<ModuleDefinition> FindAll(string? catalogNumber = null);

    /// <summary>
    /// Retrieves a <see cref="ModuleDefinition"/> object that matches the specified catalog number and revision.
    /// </summary>
    /// <param name="catalogNumber">The catalog number identifying the specific module to find.</param>
    /// <param name="revision">The revision of the module to retrieve.</param>
    /// <returns>
    /// A <see cref="ModuleDefinition"/> that matches the provided catalog number and revision.
    /// If no matching module is found, an exception may be thrown depending on the implementation.
    /// </returns>
    ModuleDefinition Find(string catalogNumber, Revision revision);

    /// <summary>
    /// Attempts to find a <see cref="ModuleDefinition"/> in the catalog matching the specified catalog number and revision.
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the module to locate.</param>
    /// <param name="revision">The revision of the module to locate.</param>
    /// <param name="definition">When this method returns, contains the <see cref="ModuleDefinition"/> if found; otherwise, null.</param>
    /// <returns>True if a matching <see cref="ModuleDefinition"/> is found; otherwise, false.</returns>
    bool TryFind(string catalogNumber, Revision revision, out ModuleDefinition definition);

    /// <summary>
    /// Retrieves the latest <see cref="ModuleDefinition"/> for the specified catalog number.
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the module for which to find the latest definition.</param>
    /// <returns>
    /// A <see cref="ModuleDefinition"/> representing the latest revision of the module for the specified catalog number.
    /// </returns>
    ModuleDefinition FindLatest(string catalogNumber);

    /// <summary>
    /// Attempts to retrieve the latest <see cref="ModuleDefinition"/> based on the specified catalog number.
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the module to search for.</param>
    /// <param name="definition">When this method returns, contains the latest <see cref="ModuleDefinition"/> if found;
    /// otherwise, null.
    /// </param>
    /// <returns>true if the latest <see cref="ModuleDefinition"/> is found; otherwise, false.</returns>
    bool TryFindLatest(string catalogNumber, out ModuleDefinition definition);

    /// <summary>
    /// Retrieves a collection of <see cref="ModuleDefinition"/> objects that belong to the
    /// specified <see cref="ModuleCategory"/>.
    /// </summary>
    /// <param name="categories">The <see cref="ModuleCategory"/> used to filter the module definitions.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of type <see cref="ModuleDefinition"/> containing all module definitions
    /// that match the specified category.
    /// </returns>
    IEnumerable<ModuleDefinition> FindBy(params ModuleCategory[] categories);

    /// <summary>
    /// Finds and returns module definitions that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">A function that defines the criteria to filter <see cref="ModuleDefinition"/> objects.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of type <see cref="ModuleDefinition"/> containing the module definitions
    /// that match the given predicate.
    /// </returns>
    IEnumerable<ModuleDefinition> FindWhere(Func<ModuleDefinition, bool> predicate);
}