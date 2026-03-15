using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Provides an in-memory implementation of <see cref="IModuleCatalog"/> for testing purposes.
/// This catalog stores module definitions in memory, allowing tests to run without depending on
/// the Rockwell Automation catalog database.
/// </summary>
public class InMemoryModuleCatalog : IModuleCatalog
{
    private readonly Dictionary<string, List<ModuleDefinition>> _definitions = new();

    /// <inheritdoc />
    public IEnumerable<ModuleDefinition> FindAll(string? catalogNumber = null)
    {
        if (catalogNumber == null)
            return _definitions.Values.SelectMany(v => v);

        return _definitions.TryGetValue(catalogNumber, out var definitions) ? definitions : [];
    }

    /// <inheritdoc />
    public ModuleDefinition Find(string catalogNumber, Revision revision)
    {
        if (_definitions.TryGetValue(catalogNumber, out var definitions))
        {
            var definition = definitions.FirstOrDefault(d => d.Revision == revision);
            if (definition is not null) return definition;
        }

        throw new KeyNotFoundException(
            $"Module definition for '{catalogNumber}' and revision '{revision}' not found.");
    }

    /// <inheritdoc />
    public bool TryFind(string catalogNumber, Revision revision, out ModuleDefinition definition)
    {
        if (!_definitions.TryGetValue(catalogNumber, out var definitions) ||
            definitions.All(d => d.Revision != revision))
        {
            definition = null!;
            return false;
        }

        definition = definitions.First(d => d.Revision == revision);
        return true;
    }

    /// <inheritdoc />
    public ModuleDefinition FindLatest(string catalogNumber)
    {
        if (_definitions.TryGetValue(catalogNumber, out var definitions) && definitions.Any())
        {
            return definitions.OrderByDescending(d => d.Revision).First();
        }

        throw new KeyNotFoundException($"No module definitions found for catalog number '{catalogNumber}'.");
    }

    /// <inheritdoc />
    public bool TryFindLatest(string catalogNumber, out ModuleDefinition definition)
    {
        if (_definitions.TryGetValue(catalogNumber, out var definitions) && definitions.Any())
        {
            definition = definitions.OrderByDescending(d => d.Revision).First();
            return true;
        }

        definition = null!;
        return false;
    }

    /// <inheritdoc />
    public IEnumerable<ModuleDefinition> FindWhere(Func<ModuleDefinition, bool> predicate)
    {
        return _definitions.Values.SelectMany(v => v).Where(predicate);
    }

    /// <summary>
    /// Adds a module definition to the catalog.
    /// </summary>
    /// <param name="definition">The module definition to add.</param>
    public void Add(ModuleDefinition definition)
    {
        AddDefinition(definition);
    }

    /// <summary>
    /// Adds multiple module definitions to the catalog.
    /// </summary>
    /// <param name="definitions">The collection of module definitions to add.</param>
    public void AddRange(IEnumerable<ModuleDefinition> definitions)
    {
        foreach (var definition in definitions)
        {
            AddDefinition(definition);
        }
    }

    /// <summary>
    /// Adds the specified module definition to the in-memory catalog.
    /// </summary>
    /// <param name="definition">The module definition to be added to the catalog.</param>
    private void AddDefinition(ModuleDefinition definition)
    {
        if (definition is null)
            throw new ArgumentNullException(nameof(definition));
        
        if (!_definitions.TryGetValue(definition.CatalogNumber, out var definitions))
        {
            definitions = [];
            _definitions.Add(definition.CatalogNumber, definitions);
        }

        definitions.Add(definition);
    }
}