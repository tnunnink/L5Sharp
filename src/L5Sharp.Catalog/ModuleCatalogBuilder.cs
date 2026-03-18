using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Catalog.Abstractions;
using L5Sharp.Catalog.Internal;
using L5Sharp.Core;
using Module = L5Sharp.Core.Module;

namespace L5Sharp.Catalog;

/// <inheritdoc />
public class ModuleCatalogBuilder : IModuleCatalogBuilder
{
    /// <summary>
    /// The default file path to the Rockwell Automation Catalog Services database file.
    /// </summary>
    /// <remarks>
    /// This constant represents the default location of the Catalog Services database file, which is used to load
    /// module definitions into the catalog. It is provided as a convenience for scenarios where no custom file
    /// path is specified. It is expected that this file exists and is accessible for proper functionality.
    /// </remarks>
    private const string DefaultRaDatabaseFile =
        @"C:\ProgramData\Rockwell Automation\Catalog Services\CatalogSvcsDatabaseV2.xml";

    /// <summary>
    /// The default embedded XML resource containing module definitions for the module catalog.
    /// </summary>
    /// <remarks>
    /// This constant specifies the name of the embedded resource file that stores predefined module definitions
    /// in XML format. It is used as the default data source for populating the module catalog when no other
    /// external source is provided. The resource is expected to be embedded in the assembly and accessible
    /// at runtime for proper functionality.
    /// </remarks>
    private const string DefaultModuleDatabaseResource = "L5Sharp.Catalog.Internal.ModuleDefinitions.xml";

    /// <summary>
    /// A private dictionary that stores module definitions, categorized by their catalog number and further grouped by their revision.
    /// </summary>
    /// <remarks>
    /// The outer dictionary uses the catalog number of the module as the key, and the value is another dictionary.
    /// The inner dictionary uses the module's <see cref="Revision"/> as the key and a corresponding <see cref="ModuleDefinition"/> as the value.
    /// This structure allows efficient organization and retrieval of module definitions by catalog number and revision.
    /// </remarks>
    private readonly Dictionary<string, Dictionary<Revision, ModuleDefinition>> _definitions = [];

    /// <inheritdoc />
    public IModuleCatalogBuilder WithDefaultModules()
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(DefaultModuleDatabaseResource);

        if (stream is null)
            throw new InvalidOperationException($"Embedded resource '{DefaultModuleDatabaseResource}' not found.");

        var document = XDocument.Load(stream);

        var definitions = document.Descendants(L5XName.Module)
            .Select(e => new Module(e))
            .Select(ModuleDefinition.Generate);

        AddModuleDefinitions(definitions);
        return this;
    }

    /// <inheritdoc />
    public IModuleCatalogBuilder WithModulesFromL5X(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The specified L5X file was not found at path: {filePath}", filePath);

        var content = L5X.Load(filePath);
        var definitions = content.Modules.Select(ModuleDefinition.Generate);
        AddModuleDefinitions(definitions);
        return this;
    }

    /// <inheritdoc />
    public IModuleCatalogBuilder WithModulesFromRAD(string? filePath = null)
    {
        filePath ??= DefaultRaDatabaseFile;

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The specified RAD file was not found at path: {filePath}", filePath);

        var definitions = RockwellDatabaseReader.ReadDefinitions(filePath);
        AddModuleDefinitions(definitions);
        return this;
    }

    /// <inheritdoc />
    public IModuleCatalogBuilder AddDefinitionFor(Module module)
    {
        if (module is null)
            throw new ArgumentNullException(nameof(module));

        var definition = ModuleDefinition.Generate(module);
        AddModuleDefinitions([definition]);
        return this;
    }

    /// <inheritdoc />
    public IModuleCatalog Build()
    {
        var catalog = _definitions.ToDictionary(d => d.Key, d => d.Value.Select(r => r.Value).ToList());
        return new ModuleCatalog(catalog);
    }

    /// <summary>
    /// Adds a set of module definitions to the catalog.
    /// </summary>
    /// <param name="definitions">The collection of module definitions to be added.</param>
    /// <param name="overwrite">
    /// A boolean value indicating whether existing definitions with the same catalog number and revision
    /// should be overwritten. If false, existing definitions are preserved.
    /// </param>
    private void AddModuleDefinitions(IEnumerable<ModuleDefinition> definitions, bool overwrite = false)
    {
        foreach (var definition in definitions)
        {
            if (!_definitions.TryGetValue(definition.CatalogNumber, out var revisions))
            {
                var collection = new Dictionary<Revision, ModuleDefinition> { { definition.Revision, definition } };
                _definitions[definition.CatalogNumber] = collection;
                continue;
            }

            if (!revisions.ContainsKey(definition.Revision) || overwrite)
            {
                revisions[definition.Revision] = definition;
            }
        }
    }
}