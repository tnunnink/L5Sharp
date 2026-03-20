using L5Sharp.Core;

namespace L5Sharp.Catalog.Abstractions;

/// <summary>
/// Provides a fluent interface for constructing an <see cref="IModuleCatalog"/> instance.
/// This builder separates the configuration phase (seeding module definitions from various sources)
/// from the consumption phase (querying the catalog), enabling atomic construction of an immutable
/// catalog suitable for dependency injection and thread-safe usage.
/// </summary>
public interface IModuleCatalogBuilder
{
    /// <summary>
    /// Adds the built-in default module definitions to the catalog.
    /// </summary>
    /// <returns>The current builder instance for method chaining.</returns>
    /// <remarks>
    /// This method loads a set of common Rockwell Automation module definitions that are embedded in the library.
    /// These definitions are useful for testing and providing out-of-the-box support for frequently used hardware.
    /// </remarks>
    IModuleCatalogBuilder WithDefaultModules();

    /// <summary>
    /// Adds module definitions extracted from the specified L5X project file.
    /// </summary>
    /// <param name="filePath">The path to the L5X file containing module definitions.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    /// <exception cref="ArgumentException"><paramref name="filePath"/> is null or empty.</exception>
    /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
    IModuleCatalogBuilder WithModulesFromL5X(string filePath);

    /// <summary>
    /// Adds module definitions from a Rockwell Automation Database (RAD) XML file.
    /// </summary>
    /// <param name="filePath">
    /// The path to the Rockwell Automation Database XML file.
    /// If null, the method will look in the default local machine directory for the database file.
    /// </param>
    /// <returns>The current builder instance for method chaining.</returns>
    /// <exception cref="FileNotFoundException">The specified file (or the default file if <paramref name="filePath"/> is null) does not exist.</exception>
    IModuleCatalogBuilder WithModulesFromRAD(string? filePath = null);

    /// <summary>
    /// Adds a module definition generated from the provided <see cref="Module"/> instance.
    /// </summary>
    /// <param name="module">The module from which to extract and add a definition.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="module"/> is null.</exception>
    IModuleCatalogBuilder AddDefinitionFor(Module module);

    /// <summary>
    /// Constructs and returns the final <see cref="IModuleCatalog"/> instance with all configured definitions.
    /// After calling this method, the catalog is ready for querying and should be treated as immutable.
    /// </summary>
    /// <returns>A fully constructed <see cref="IModuleCatalog"/> instance.</returns>
    IModuleCatalog Build();
}