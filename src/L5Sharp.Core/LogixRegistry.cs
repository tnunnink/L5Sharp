using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a registry for managing Logix template components for DataTypes, Modules, and Programs.
/// This class acts as a store and factory so that callers can easily create and configure new component instances
/// based on predefined or well-known templates that can be easily loaded from existing L5X files.
/// </summary>
public class LogixRegistry()
{
    /// <summary>
    /// The conflict resolution strategy used by the registry when handling duplicate component references.
    /// Determines how conflicts are resolved when multiple components with the same identifier are encountered.
    /// </summary>
    private readonly ConflictStrategy _strategy = ConflictStrategy.ThrowException;

    /// <summary>
    /// Collection of supported template elements that the registry will index when scanning L5X files.
    /// </summary>
    private static readonly Dictionary<Type, string> Supported = new()
    {
        { typeof(DataType), L5XName.DataType },
        { typeof(AddOnInstruction), L5XName.AddOnInstructionDefinition },
        { typeof(Module), L5XName.Module },
        { typeof(Program), L5XName.Program }
    };

    /// <summary>
    /// Creates a new <see cref="LogixRegistry"/> with the provided <see cref="ConflictStrategy"/> option.
    /// </summary>
    /// <param name="strategy">The <see cref="ConflictStrategy"/> option indicating how to handle duplicate component references.</param>
    public LogixRegistry(ConflictStrategy strategy) : this()
    {
        _strategy = strategy;
    }

    /// <summary>
    /// The local registry dictionary containing template components loaded from L5X files.
    /// </summary>
    private readonly Dictionary<string, XElement> _registry = [];

    /// <summary>
    /// Registers all supported elements defined in the file located at the specified file path into the Logix registry.
    /// </summary>
    /// <param name="filePath">The path to the file containing logix components to register.</param>
    /// <exception cref="FileNotFoundException">Thrown if the file specified by <paramref name="filePath"/> does not exist.</exception>
    /// <exception cref="XmlException">Thrown if there is an error loading or parsing the XML file.</exception>
    /// <remarks>
    /// This will register <see cref="DataType"/>, <see cref="AddOnInstruction"/>, <see cref="Module"/>, and
    /// <see cref="Program"/> elements as template to create new instances from in memory.
    /// </remarks>
    public int Scan(string filePath)
    {
        return RegisterFile(filePath);
    }


#if NETSTANDARD2_0
    /// <summary>
    /// Scans all files with a .L5X extension in the specified directory and registers their elements into the Logix registry.
    /// Elements are identified and filtered based on the supported types within the system.
    /// </summary>
    /// <param name="directoryPath">The path to the directory containing .L5X files to scan and register.</param>
    /// <param name="searchOption">Option that specifies whether the search operation should include only the
    /// current directory or should include all subdirectories. The default value is TopDirectoryOnly.</param>
    /// <returns>The total number of elements registered from the files in the specified directory.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory specified by <paramref name="directoryPath"/> does not exist.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if access to the directory or its files is denied.</exception>
    public int ScanDirectory(string directoryPath, SearchOption searchOption = default)
    {
        var registered = 0;

        foreach (var file in Directory.EnumerateFiles(directoryPath, "*.L5X", searchOption))
        {
            registered += Scan(file);
        }

        return registered;
    }
#endif


#if NET7_0_OR_GREATER
    /// <summary>
    /// Scans all files with a .L5X extension in the specified directory and registers their elements into the Logix registry.
    /// Elements are identified and filtered based on the supported types within the system.
    /// </summary>
    /// <param name="directoryPath">The path to the directory containing .L5X files to scan and register.</param>
    /// <param name="options">The optional <see cref="EnumerationOptions"/> to control how file enumeration executes.
    /// If not provided, the default options are configured to recurse subdirectories and ignore inaccessible folders/files.</param>
    /// <returns>The total number of elements registered from the files in the specified directory.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory specified by <paramref name="directoryPath"/> does not exist.</exception>
    public int ScanAll(string directoryPath, EnumerationOptions? options = null)
    {
        var registered = 0;

        options ??= new EnumerationOptions
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
        };

        foreach (var file in Directory.EnumerateFiles(directoryPath, "*.L5X", options))
        {
            registered += RegisterFile(file);
        }

        return registered;
    }
#endif

    /// <summary>
    /// Retrieves a component of the specified type that is registered to the provided identifier.
    /// If not found, then an exception is thrown. 
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <param name="identifier">The unique identifier of the component to retrieve.</param>
    /// <returns>The component of type <typeparamref name="T"/> associated with the specified identifier.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="identifier"/> is null or empty.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no component corresponding to the specified identifier is found.</exception>
    /// <seealso cref="TryGetTemplate{T}"/>
    public T GetTemplate<T>(string identifier) where T : LogixComponent
    {
        if (!TryGetElement<T>(identifier, out var element))
        {
            throw new KeyNotFoundException($"No component found with identifier: '{identifier}'");
        }

        return element.Deserialize<T>();
    }

    /// <summary>
    /// Attempts to retrieve a component of the specified type that is registered to the provided identifier.
    /// If found, returns the component and a value indicating success; otherwise, returns false.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <param name="identifier">The unique identifier of the component to retrieve.</param>
    /// <param name="template">
    /// When this method returns, contains the component of type <typeparamref name="T"/> associated with the specified identifier,
    /// if the component is found; otherwise, the default value for the type of the <paramref name="template"/> parameter.
    /// </param>
    /// <returns>
    /// A boolean value indicating whether the component was successfully retrieved.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="identifier"/> is null or empty.</exception>
    /// <seealso cref="GetTemplate{T}"/>
    public bool TryGetTemplate<T>(string identifier, out T template) where T : LogixComponent
    {
        if (!TryGetElement<T>(identifier, out var element))
        {
            template = null!;
            return false;
        }

        template = element.Deserialize<T>();
        return true;
    }

    /// <summary>
    /// Retrieves a collection of templates of the specified type from the registry.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="LogixComponent"/> templates to be retrieved.</typeparam>
    /// <returns>An enumerable collection of templates of the specified type.</returns>
    public IEnumerable<T> ListTemplates<T>() where T : LogixComponent
    {
        if (!Supported.TryGetValue(typeof(T), out var element))
        {
            throw new ArgumentException($"The component type {typeof(T).Name} is not supported by the registry.");
        }

        return _registry.Where(x => x.Key.Contains(element)).Select(x => x.Value.Deserialize<T>());
    }

    /// <summary>
    /// Creates and configures a <see cref="Module"/> using the specified catalog number and configuration delegate.
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the module to create.</param>
    /// <param name="config">An action to configure the created module.</param>
    /// <returns>The newly created and configured <see cref="Module"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="config"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no module is found for the specified <paramref name="catalogNumber"/>.</exception>
    public Module CreateModule(string catalogNumber, Action<Module> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        if (!TryGetElement<Module>(catalogNumber, out var element))
        {
            throw new KeyNotFoundException($"No module found with identifier: '{catalogNumber}'");
        }

        var module = element.Deserialize<Module>();
        config(module);
        return module;
    }

    /// <summary>
    /// Creates and configures a <see cref="Program"/> using the specified name and configuration delegate.
    /// </summary>
    /// <param name="programName">The name of the program to create.</param>
    /// <param name="config">A configuration action applied to the program after creation.</param>
    /// <returns>The newly created and configured <see cref="Program"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="config"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no program is found with the specified <paramref name="programName"/> identifier.</exception>
    public Program CreateProgram(string programName, Action<Program> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        if (!TryGetElement<Program>(programName, out var element))
        {
            throw new KeyNotFoundException($"No module found with identifier: '{programName}'");
        }

        var program = element.Deserialize<Program>();
        config(program);
        return program;
    }

    /// <summary>
    /// Creates a new tag with the specified name and data type, applying the provided configuration action to the created tag.
    /// </summary>
    /// <param name="dataType">The data type of the tag to be created.</param>
    /// <param name="config">An action to configure the created tag.</param>
    /// <returns>A newly created and configured <see cref="Tag"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="config"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no data type with the specified <paramref name="dataType"/> is found in the registry.</exception>
    public Tag CreateTag(string dataType, Action<Tag> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        if (!TryGetElement<DataType>(dataType, out var element))
        {
            throw new KeyNotFoundException($"No module found with identifier: '{dataType}'");
        }

        //todo but this should use the registry not the L5X parent because there is not parent. This is more complex though...
        var definition = element.Deserialize<DataType>();
        var tag = new Tag($"{definition.Name}Tag", definition.ToData());
        config(tag);
        return tag;
    }

    /// <summary>
    /// Clears all components from the local registry, effectively resetting it to an empty state.
    /// </summary>
    public void Clear()
    {
        _registry.Clear();
    }

    #region Internals

    /// <summary>
    /// Attempts to retrieve an <see cref="XElement"/> from the registry that matches the specified type and identifier.
    /// </summary>
    private bool TryGetElement<T>(string identifier, out XElement element)
    {
        if (string.IsNullOrEmpty(identifier))
            throw new ArgumentException("Can not retrieve component from null or empty identifier.");

        var key = BuildKey(typeof(T), identifier);

        if (!_registry.TryGetValue(key, out var template))
        {
            element = null!;
            return false;
        }

        element = new XElement(template);
        return true;
    }

    /// <summary>
    /// Registers modules, types, and programs from the specified L5X file.
    /// </summary>
    private int RegisterFile(string filePath)
    {
        var registered = 0;
        var file = L5X.Load(filePath);

        registered += RegisterModules(file);
        registered += RegisterTypes(file);
        registered += RegisterPrograms(file);

        return registered;
    }

    /// <summary>
    /// Registers the modules from the given L5X file into the registry.
    /// Only gets modules 
    /// </summary>
    private int RegisterModules(L5X file)
    {
        var registered = 0;
        var cataloged = new HashSet<string>(); //using hashset since .NET2 doesn't support DistinctBy().

        foreach (var module in file.Modules)
        {
            if (module.CatalogNumber is null || module.CatalogNumber.IsEmpty()) continue;
            if (!cataloged.Add(module.CatalogNumber)) continue;

            var key = BuildKey(typeof(Module), module.CatalogNumber);
            registered += RegisterElement(key, module);
        }

        return registered;
    }

    /// <summary>
    /// Registers all data types found in the provided <see cref="L5X"/> file into the registry.
    /// </summary>
    private int RegisterTypes(L5X file)
    {
        var registered = 0;

        foreach (var dataType in file.DataTypes)
        {
            if (dataType.Name.IsEmpty()) continue;
            var key = BuildKey(typeof(DataType), dataType.Name);
            registered += RegisterElement(key, dataType);
        }

        return registered;
    }

    /// <summary>
    /// Registers all programs from the provided <see cref="L5X"/> instance into the current registry.
    /// </summary>
    private int RegisterPrograms(L5X file)
    {
        var registered = 0;

        foreach (var program in file.Programs)
        {
            if (program.Name.IsEmpty()) continue;
            var key = BuildKey(typeof(Program), program.Name);
            registered += RegisterElement(key, program);
        }

        return registered;
    }

    /// <summary>
    /// Registers a <see cref="LogixElement"/> in the registry with the specified key.
    /// Handles conflicts according to the configured <see cref="ConflictStrategy"/>.
    /// </summary>
    private int RegisterElement(string key, LogixElement element)
    {
        var template = new XElement(element.Serialize());

        if (_registry.ContainsKey(key))
        {
            switch (_strategy)
            {
                case ConflictStrategy.OverwriteExisting:
                    _registry[key] = template;
                    return 1;
                case ConflictStrategy.ThrowException:
                    throw new InvalidOperationException($"Component already exists for identifier: '{key}'");
                case ConflictStrategy.KeepFirst:
                default:
                    return 0;
            }
        }

        _registry.Add(key, template);
        return 1;
    }

    /// <summary>
    /// Represents a unique identifier used to reference a specific build configuration or instance.
    /// </summary>
    private static string BuildKey(Type type, string identifier)
    {
        if (!Supported.TryGetValue(type, out var element))
        {
            throw new ArgumentException($"The component type {type.Name} is not supported by the registry.");
        }

        return $"/{element}/{identifier}";
    }

    #endregion
}

/// <summary>
/// Defines the strategy for handling conflicts when loading components
/// with the same identifier into the registry.
/// </summary>
public enum ConflictStrategy
{
    /// <summary>
    /// Throw an exception if a component with the same identifier is already registered.
    /// </summary>
    ThrowException,

    /// <summary>
    /// Keep the first component registered and ignore the following components with the same identifier.
    /// </summary>
    KeepFirst,

    /// <summary>
    /// Overwrite the existing component with the newly loaded component if identifiers match.
    /// </summary>
    OverwriteExisting

    // Potential future option: StoreAllVersions (would require API changes for retrieval)
}