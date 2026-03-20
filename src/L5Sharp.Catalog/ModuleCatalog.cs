using L5Sharp.Catalog.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Catalog;

/// <summary>
/// Provides an in-memory implementation of <see cref="IModuleCatalog"/> for testing purposes.
/// This catalog stores module definitions in memory, allowing tests to run without depending on
/// the Rockwell Automation catalog database.
/// </summary>
public class ModuleCatalog : IModuleCatalog
{
    /// <summary>
    /// A data structure containing a collection of module definitions indexed by catalog number.
    /// Each entry in the dictionary holds a key representing the catalog number and a value containing
    /// a list of <see cref="ModuleDefinition"/> objects associated with that catalog number. Each catalog
    /// number can contain multiple revisions of the same device, which is why this structure is used.
    /// </summary>
    private readonly Dictionary<string, List<ModuleDefinition>> _definitions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleCatalog"/> class with the specified
    /// collection of module definitions.
    /// </summary>
    /// <param name="definitions">
    /// A dictionary containing module definitions indexed by catalog number. Each catalog number maps
    /// to a list of <see cref="ModuleDefinition"/> objects representing different revisions of the module.
    /// </param>
    internal ModuleCatalog(Dictionary<string, List<ModuleDefinition>> definitions)
    {
        _definitions = definitions;
    }

    /// <inheritdoc />
    public Module Create(string name, string catalogNumber, Action<Module>? config = null)
    {
        ThrowIfEmptyCatalogNumber(catalogNumber);
        var definition = GetDefinition(catalogNumber);
        var module = definition.Create(name, config);
        return module;
    }

    /// <inheritdoc />
    public Module Create(string name, string catalogNumber, Revision revision, Action<Module>? config = null)
    {
        ThrowIfEmptyCatalogNumber(catalogNumber);
        var definition = GetDefinition(catalogNumber, revision);
        var module = definition.Create(name, config);
        return module;
    }

    /// <inheritdoc />
    public bool TryCreate(string name, string catalogNumber, out Module module)
    {
        ThrowIfEmptyCatalogNumber(catalogNumber);

        if (TryGetDefinition(catalogNumber, out var definition))
        {
            module = definition.Create(name);
            return true;
        }

        module = null!;
        return false;
    }

    /// <inheritdoc />
    public bool TryCreate(string name, string catalogNumber, Revision revision, out Module module)
    {
        ThrowIfEmptyCatalogNumber(catalogNumber);

        if (TryGetDefinition(catalogNumber, revision, out var definition))
        {
            module = definition.Create(name);
            return true;
        }

        module = null!;
        return false;
    }

    /// <inheritdoc />
    public IEnumerable<ModuleDefinition> Definitions(string? catalogNumber = null)
    {
        if (catalogNumber == null)
            return _definitions.Values.SelectMany(v => v);

        return _definitions.TryGetValue(catalogNumber, out var definitions) ? definitions : [];
    }

    /// <inheritdoc />
    public ModuleDefinition GetDefinition(string catalogNumber, Revision? revision = null)
    {
        ThrowIfEmptyCatalogNumber(catalogNumber);

        if (!_definitions.TryGetValue(catalogNumber, out var revisions) || revisions.Count == 0)
            throw new KeyNotFoundException($"Module definition for '{catalogNumber}' not found.");

        var definition = revisions
            .OrderByDescending(d => d.Revision)
            .FirstOrDefault(d => revision is null || d.Revision == revision);

        if (definition is null)
        {
            throw new KeyNotFoundException(
                $"Module definition for '{catalogNumber}'  and revision '{revision}' not found.");
        }

        return definition;
    }

    /// <inheritdoc />
    public bool TryGetDefinition(string catalogNumber, out ModuleDefinition definition)
    {
        ThrowIfEmptyCatalogNumber(catalogNumber);

        if (!_definitions.TryGetValue(catalogNumber, out var revisions) || revisions.Count == 0)
        {
            definition = null!;
            return false;
        }

        var latest = revisions.OrderByDescending(d => d.Revision).FirstOrDefault();

        if (latest is null)
        {
            definition = null!;
            return false;
        }

        definition = latest;
        return true;
    }

    /// <inheritdoc />
    public bool TryGetDefinition(string catalogNumber, Revision revision, out ModuleDefinition definition)
    {
        ThrowIfEmptyCatalogNumber(catalogNumber);

        if (revision is null)
            throw new ArgumentNullException(nameof(revision));

        if (!_definitions.TryGetValue(catalogNumber, out var revisions) || revisions.Count == 0)
        {
            definition = null!;
            return false;
        }

        var target = revisions.FirstOrDefault(d => d.Revision == revision);

        if (target is null)
        {
            definition = null!;
            return false;
        }

        definition = target;
        return true;
    }

    /// <summary>
    /// Validates the specified catalog number, throwing an exception if it is null or empty.
    /// </summary>
    /// <param name="catalogNumber">
    /// The catalog number to validate. It must not be null or an empty string.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the catalog number is null or an empty string.
    /// </exception>
    private static void ThrowIfEmptyCatalogNumber(string catalogNumber)
    {
        if (string.IsNullOrEmpty(catalogNumber))
            throw new ArgumentException("Catalog number cannot be null or empty.", nameof(catalogNumber));
    }
}