using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using JetBrains.Annotations;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Utilities;
using Task = L5Sharp.Components.Task;
using task = System.Threading.Tasks.Task;


namespace L5Sharp;

/// <summary>
/// This is the primary entry point for interacting with the L5X file.
/// Provides access to query and manipulate logix components, elements, containers, and more. 
/// </summary>
/// <remarks>
/// </remarks>
[PublicAPI]
public class L5X : ILogixSerializable
{
    /// <summary>
    /// The date/time format for the L5X content.
    /// </summary>
    public const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

    /// <summary>
    /// The underlying root RSLogix5000Content element of the L5X file.
    /// </summary>
    private readonly XElement _content;

    /// <summary>
    /// An index of all logix components in the L5X file for fast lookups.
    /// </summary>
    private LogixIndex? _index;

    /// <summary>
    /// An index of all logix components in the L5X file for fast lookups.
    /// </summary>
    private LogixIndex Index => _index ??= new LogixIndex(GetController());

    /// <summary>
    /// The list of top level component containers for a L5X content or controller element in order of which
    /// they should appear in the L5X file.
    /// </summary>
    private static readonly List<string> Containers = new()
    {
        L5XName.DataTypes,
        L5XName.Modules,
        L5XName.AddOnInstructionDefinitions,
        L5XName.Tags,
        L5XName.Programs,
        L5XName.Tasks,
        L5XName.ParameterConnections,
        L5XName.Trends,
        L5XName.QuickWatchLists
    };

    /// <summary>
    /// Creates a new <see cref="L5X"/> instance wrapping the provided <see cref="XElement"/> content object.
    /// </summary>
    /// <param name="content">The root <see cref="XElement"/> object representing the RSLogix5000Content element of the
    /// L5X file.</param>
    /// <exception cref="ArgumentNullException"><c>content</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>content</c> name is not expected <c>RSLogix5000Content</c>.</exception>
    /// <remarks>
    /// Although you create the L5X instance by providing a valid XML element, it is typically easier or more
    /// typical to use the static factory methods <see cref="Load"/> to load a file from disc or <see cref="New"/>
    /// to generate a new default instance. Also note that each individual component can be exported to generate a new
    /// L5X content file.
    /// </remarks>
    public L5X(XElement content)
    {
        if (content is null)
            throw new ArgumentNullException(nameof(content));

        if (content.Name != L5XName.RSLogix5000Content)
            throw new ArgumentException(
                $"Expecting root element name of {L5XName.RSLogix5000Content} to initialize L5X.");

        _content = content;

        //We will "normalize" (ensure consistent root controller element and component containers) for all
        //files so that we won't have issues getting top level containers. When saving we can remove unused containers.
        NormalizeContent();

        //This stores L5X object as in-memory object for the root XElement,
        //allowing child elements to retrieve the object locally without creating a new instance (and reindexing of content).
        //This allows them to reference to root L5X for cross referencing or other operations.
        _content.AddAnnotation(this);
    }

    /// <summary>
    /// The <see cref="L5XInfo"/> representing the L5X content export information.
    /// </summary>
    public L5XInfo Info => new(_content);

    /// <summary>
    /// The root <see cref="Components.Controller"/> component of the L5X file.
    /// </summary>
    /// <value>A <see cref="Components.Controller"/> component object.</value>
    /// <remarks>If the L5X does not <c>ContainContext</c>, meaning it is a project export, this will contain all the
    /// relevant controller properties and configurations. Otherwise most data will be null as the controller serves as
    /// just a root container for other component objects.</remarks>
    public Controller Controller => new(GetController());

    /// <summary>
    /// The container collection of <see cref="DataType"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="DataType"/> components.</value>
    public LogixContainer<DataType> DataTypes => new(GetContainer(L5XName.DataTypes));

    /// <summary>
    /// Gets the collection of <see cref="AddOnInstruction"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="AddOnInstruction"/> components.</value>
    public LogixContainer<AddOnInstruction> Instructions => new(GetContainer(L5XName.AddOnInstructionDefinitions));

    /// <summary>
    /// Gets the collection of <see cref="Module"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Module"/> components.</value>
    public LogixContainer<Module> Modules => new(GetContainer(L5XName.Modules));

    /// <summary>
    /// Gets the collection of Controller <see cref="Tags"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Tags"/> components.</value>
    /// <remarks>To access program specific tag collection user the <see cref="Programs"/> collection.</remarks>
    public LogixContainer<Tag> Tags => new(GetContainer(L5XName.Tags));

    /// <summary>
    /// Gets the collection of <see cref="Program"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Program"/> components.</value>
    public LogixContainer<Program> Programs => new(GetContainer(L5XName.Programs));

    /// <summary>
    /// Gets the collection of <see cref="Task"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Task"/> components.</value>
    public LogixContainer<Task> Tasks => new(GetContainer(L5XName.Tasks));

    /// <summary>
    /// The container collection of <see cref="ParameterConnection"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="ParameterConnection"/> components.</value>
    public LogixContainer<ParameterConnection> ParameterConnections =>
        new(GetContainer(L5XName.ParameterConnections));

    /// <summary>
    /// The container collection of <see cref="Trend"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Trend"/> components.</value>
    public LogixContainer<Trend> Trends => new(GetContainer(L5XName.Trends));

    /// <summary>
    /// The container collection of <see cref="WatchList"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="WatchList"/> components.</value>
    public LogixContainer<WatchList> WatchLists => new(GetContainer(L5XName.QuickWatchLists));

    /// <summary>
    /// Creates a new <see cref="L5X"/> by loading the contents of the provide file name.
    /// </summary>
    /// <param name="fileName">The full path, including file name, to the L5X file to load.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified file.</returns>
    /// <exception cref="ArgumentException">The string is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="XElement.Load(string)"/> to load the contents of the XML file into
    /// memory. This means that this method is subject to the same exceptions that could be generated by loading the
    /// XElement.
    /// </remarks>
    public static L5X Load(string fileName) => new(XElement.Load(fileName));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<L5X> LoadAsync(string fileName, CancellationToken token)
    {
        await using var stream = new FileStream(fileName, FileMode.Open);
        var element = await XElement.LoadAsync(stream, LoadOptions.SetLineInfo, token);
        var l5X = await task.Run(() => new L5X(element), token);
        return l5X;
    }

    /// <summary>
    /// Creates a new <see cref="L5X"/> file with the standard root content and controller elements, and configures them
    /// with the provided controller name, processor, and revision. 
    /// </summary>
    /// <param name="name">The name of the controller.</param>
    /// <param name="processor">The processor catalog number.</param>
    /// <param name="revision">The optional software revision of the processor.</param>
    /// <returns>A new default <see cref="L5X"/> with the specified controller properties.</returns>
    public static L5X New(string name, string processor, Revision? revision) =>
        new(NewContent(name, nameof(Controller), revision));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="revision"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    public static L5X New<TComponent>(TComponent component, Revision? revision = null)
        where TComponent : LogixComponent => new(NewContent(component.Name, typeof(TComponent).L5XType(), revision));

    /// <summary>
    /// Creates a new <see cref="L5X"/> with the provided L5X string content.
    /// </summary>
    /// <param name="text">The string that contains the L5X content to parse.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified string.</returns>
    /// <exception cref="ArgumentException">The string is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="XElement.Parse(string)"/> to load the contents of the XML file into
    /// memory. This means that this method is subject to the same exceptions that could be generated by parsing the
    /// XElement.
    /// </remarks>
    public static L5X Parse(string text) => new(XElement.Parse(text));

    /// <summary>
    /// Finds element across the entire L5X with the provided type as a flat collection of object. 
    /// </summary>
    /// <param name="type">The type name or element name to retrieve.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found object with the provided type name.</returns>
    /// <exception cref="ArgumentException"><c>type</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This methods provides a flexible and simple way to query the entire L5X for a specific type. This method is allows
    /// specifying the type at runtime as opposed the generic type but sacrifices the strong type querying of the
    /// generic counterpart. This method does not make use of any optimized searching, so if you want to find items quickly,
    /// see &lt;c&gt;FindComponent&lt;/c&gt; or &lt;c&gt;FindTag&lt;/c&gt; method.
    /// </para>
    /// <para>
    /// Also note that this will call <c>L5XType</c> extension internally which returns all configured
    /// element name for the provided type. This means the query will return all elements that the type supports.
    /// If you want specific components look at the container properties.
    /// For example <see cref="Tags"/> for controller scoped tag components only.
    /// </para>
    /// </remarks>
    public IEnumerable<LogixElement> Query(string type)
    {
        if (string.IsNullOrEmpty(type))
            throw new ArgumentNullException(nameof(type), "Type is required to retrieve elements from the L5X");

        return _content.Descendants(type).Select(e => e.Deserialize());
    }

    /// <summary>
    /// Finds element across the entire L5X with the provided type as a flat collection of object. 
    /// </summary>
    /// <param name="type">The type of the element type to retrieve.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found object with the provided type name.</returns>
    /// <exception cref="ArgumentException"><c>type</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This methods provides a flexible and simple way to query the entire L5X for a specific type. This method is allows
    /// specifying the type at runtime as opposed the generic type but sacrifices the strong type querying of the
    /// generic counterpart. This method does not make use of any optimized searching, so if you want to find items quickly,
    /// see &lt;c&gt;FindComponent&lt;/c&gt; or &lt;c&gt;FindTag&lt;/c&gt; method.
    /// </para>
    /// <para>
    /// Also note that this will call <c>L5XType</c> extension internally which returns all configured
    /// element name for the provided type. This means the query will return all elements that the type supports.
    /// If you want specific components look at the container properties.
    /// For example <see cref="Tags"/> for controller scoped tag components only.
    /// </para>
    /// </remarks>
    public IEnumerable<LogixElement> Query(Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type), "Type is required to retrieve elements from the L5X");

        var typeNames = type.L5XTypes().ToList();

        return _content.Descendants()
            .Where(e => typeNames.Any(n => n.IsEquivalent(e.L5XType())))
            .Select(e => e.Deserialize());
    }

    /// <summary>
    /// Finds elements of the specified type across the entire L5X as a flat collection of objects.
    /// </summary>
    /// <typeparam name="TElement">The element type to find.</typeparam>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found objects of the specified type.</returns>
    /// <remarks>
    /// This methods provides a flexible and simple way to query the entire L5X for a specific type. Since
    /// it returns an <see cref="IEnumerable{T}"/>, you can make use of LINQ and the strongly typed objects to build
    /// more complex queries.
    /// </remarks>
    public IEnumerable<TElement> Query<TElement>() where TElement : LogixElement
    {
        var typeNames = typeof(TElement).L5XTypes().ToList();

        return _content.Descendants()
            .Where(e => typeNames.Any(n => n.IsEquivalent(e.L5XType())))
            .Select(e => e.Deserialize<TElement>());
    }

    /// <summary>
    /// Adds the given logix component to the first found container within the L5X tree. 
    /// </summary>
    /// <param name="component">The component to add to the L5X.</param>
    /// <typeparam name="TComponent">The type of component to add to the L5X.</typeparam>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file. Note that this only adds to the first
    /// container found of the specific type. If you are adding scoped components such as <c>Tag</c> or <c>Routine</c>
    /// you should be doing so in the context of a <c>Program</c> component.
    /// </remarks>
    public void Add<TComponent>(TComponent component) where TComponent : LogixComponent
    {
        var containerType = typeof(TComponent).L5XContainer();
        var container = GetContainer(containerType);
        container.Add(component.Serialize());
    }

    /// <summary>
    /// Adds the given logix component to the first found container within the L5X tree. 
    /// </summary>
    /// <param name="component">The component to add to the L5X.</param>
    /// <param name="container"></param>
    /// <typeparam name="TComponent">The type of component to add to the L5X.</typeparam>
    /// <exception cref="InvalidOperationException">No container was found in the L5X tree for the specified type.</exception>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file. Note that this only adds to the first
    /// container found of the specific type. If you are adding scoped components such as <c>Tag</c> or <c>Routine</c>
    /// you should be doing so in the context of a <c>Program</c> component.
    /// </remarks>
    public void AddTo<TComponent>(TComponent component, string container) where TComponent : LogixComponent
    {
        var type = typeof(TComponent).L5XContainer();
        var target = _content.Descendants(type).FirstOrDefault(e => Scope.Container(e) == container);
        if (target is null)
            throw new InvalidOperationException($"Not container with name '{container}' was found in the L5X.");
        target.Add(component.Serialize());
    }

    /// <summary>
    /// Retrieves all components with the specified key.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to search for.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="LogixComponent"/> having the specified type/name
    /// composite key.</returns>
    /// <remarks>
    /// <para>
    /// Note that this method returns all components with the type/name pair. This is typically a single component,
    /// but types like Tag can have same name across different scopes, so it may be multiple different objects.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public IEnumerable<LogixComponent> All(ComponentKey key)
    {
        return Index.Components.TryGetValue(key, out var components)
            ? components.Select(c => c.Value.Deserialize<LogixComponent>())
            : Enumerable.Empty<LogixComponent>();
    }

    /// <summary>
    /// Retrieves all components of type <see cref="TComponent"/> with the specified name.
    /// </summary>
    /// <param name="name">The name of the components to retrieve.</param>
    /// <typeparam name="TComponent">The type of components to retrieve.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="LogixComponent"/> having the specified type/name
    /// composite key.</returns>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// Note that this method returns all components with the type/name pair. This is typically a single component,
    /// but types like Tag or Routine can have same name across different scopes, so it may be multiple different objects.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find{TComponent}(string)"/>
    public IEnumerable<TComponent> All<TComponent>(string name) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);

        return Index.Components.TryGetValue(key, out var components)
            ? components.Select(c => c.Value.Deserialize<TComponent>())
            : Enumerable.Empty<TComponent>();
    }

    /// <summary>
    /// Retrieves all tags with the given tag name across the entire L5X file.
    /// </summary>
    /// <param name="tagName">The name of the tag to search for.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Tag"/> objects that match the given tag name.
    /// Note that this could be multiple tags from different containers or scopes.
    /// </returns>
    /// <exception cref="ArgumentNullException"><c>tagName</c> parameter is null.</exception>
    /// <remarks>
    /// <para>
    /// The provided tag name can be a top level tag name or a nested (dot-down) tag name path. This method will
    /// find the root tag and then reach down the tag hierarchy as specified. Note that this method can return
    /// multiple tags if the specified tag name exists in different scopes.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(L5Sharp.Common.TagName,string)"/>
    public IEnumerable<Tag> All(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        var key = new ComponentKey(nameof(Tag), tagName.Root);

        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.Select(t => new Tag(t).Member(tagName.Path)).Where(t => t is not null).Cast<Tag>()
            : Enumerable.Empty<Tag>();
    }

    /// <summary>
    /// Checks if a component object with the specified type and name composite key exists in the L5X file.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to check.</param>
    /// <returns><c>true</c> if the component key exists in the L5X; otherwise, <c>false</c>.</returns>
    /// <remarks>This </remarks>
    /// <remarks>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </remarks>
    public bool Contains(ComponentKey key) => Index.Components.ContainsKey(key);

    /// <summary>
    /// Checks if a component object with the specified type and name composite key exists in the L5X file.
    /// </summary>
    /// <param name="name">The name of the component to check for existence.</param>
    /// <typeparam name="TComponent">The type of component to check for existence.</typeparam>
    /// <returns><c>true</c> if the component key exists in the L5X; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </remarks>
    public bool Contains<TComponent>(string name) =>
        Index.Components.ContainsKey(new ComponentKey(typeof(TComponent).L5XType(), name));

    /// <summary>
    /// Returns the number of elements of the specified type in the L5X.
    /// </summary>
    /// <typeparam name="TElement">The logix element type to get the count for.</typeparam>
    /// <returns>A <see cref="int"/> representing the number of elements of the specified type.</returns>
    public int Count<TElement>() where TElement : LogixElement =>
        _content.Descendants(typeof(TElement).L5XType()).Count();

    /// <summary>
    /// Finds the first component in the L5X file having the provided composite type/name key.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to search for.</param>
    /// <returns>The found <see cref="LogixComponent"/> if it exists, otherwise returns null.</returns>
    /// <remarks>
    /// <para>
    /// If there are multiple different scoped components with the same component key, this method will return the first
    /// found object. For types like <c>Tag</c>, this will be the controller scoped instance (as it is indexed first).
    /// For non-scoped components, there should be only one component anyway. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(L5Sharp.Common.ComponentKey,string)"/>
    public LogixComponent? Find(ComponentKey key)
    {
        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.First()?.Deserialize<LogixComponent>()
            : default;
    }

    /// <summary>
    /// Finds a component in the L5X file having the provided composite type/name key and container name.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to search for.</param>
    /// <param name="container">The name of the container (controller, program, instruction) in which the
    /// component should be found.</param>
    /// <returns>The found <see cref="LogixComponent"/> if it exists, otherwise returns null.</returns>
    /// <exception cref="ArgumentException"><c>container</c> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public LogixComponent? Find(ComponentKey key, string container)
    {
        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));

        if (Index.Components.TryGetValue(key, out var components)
            && components.TryGetValue(container, out var component))
            return component.Deserialize<LogixComponent>();

        return default;
    }

    /// <summary>
    /// Finds the first component in the L5X file having the provided type and name.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The type of component to find.</typeparam>
    /// <returns>The found <see cref="LogixComponent"/> if it exists, otherwise returns null.</returns>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// If there are multiple different scoped components with the same type and name, this method will return the first
    /// found object. For types like <c>Tag</c>, this will be the controller scoped instance (as it is indexed first).
    /// For non-scoped components, there should be only one component anyway. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public TComponent? Find<TComponent>(string name) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);

        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.First()?.Deserialize<TComponent>()
            : default;
    }

    /// <summary>
    /// Finds a component in the L5X file having the provided composite type/name key and container name.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <param name="container">The name of the container (controller, program, instruction) in which the
    /// component should be found.</param>
    /// <typeparam name="TComponent">The type of component to find.</typeparam>
    /// <returns>The found <see cref="LogixComponent"/> if it exists, otherwise returns null.</returns>
    /// <exception cref="ArgumentException"><c>name</c> or <c>container</c> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public TComponent? Find<TComponent>(string name, string container) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);

        if (Index.Components.TryGetValue(key, out var components)
            && components.TryGetValue(container, out var component))
            return component.Deserialize<TComponent>();

        return default;
    }

    /// <summary>
    /// Finds the first <see cref="Tag"/> in the L5X file having the specified tag name.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> of the tag to find.</param>
    /// <returns>The found <see cref="Tag"/> if it exists, otherwise returns null.</returns>
    /// <exception cref="ArgumentException"><c>tagName</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method is similar to the other <c>Find</c> methods with the slight difference that is will also return the
    /// nested tag member based on the provided <c>tagName</c> parameter. The tag name can represent a nested or dot-down
    /// member path to a child tag of a given base tag. So ultimately this is a slightly more concise call for a <c>Tag</c>
    /// type component specifically. Note that his will return the first found tag.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(L5Sharp.Common.TagName,string)"/>
    /// <seealso cref="All(L5Sharp.Common.TagName)"/>
    public Tag? Find(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        var key = new ComponentKey(nameof(Tag), tagName.Root);

        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.First().Deserialize<Tag>().Member(tagName.Path)
            : default;
    }

    /// <summary>
    /// Finds a single <see cref="Tag"/> in the L5X file having the specified tag and container name.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> of the tag to find.</param>
    /// <param name="container">The name of the container (controller, program, instruction) in which the tag
    /// should be found.</param>
    /// <returns>The found <see cref="Tag"/> if it exists, otherwise returns null.</returns>
    /// <exception cref="ArgumentException"><c>tagName</c> is null -or- <c>container</c> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// This method is similar to the other <c>Find</c> methods with the slight difference that is will also return the
    /// nested tag member based on the provided <c>tagName</c> parameter. The tag name can represent a nested or dot-down
    /// member path to a child tag of a given base tag. So ultimately this is a slightly more concise call for a <c>Tag</c>
    /// type component specifically.
    /// </para>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(L5Sharp.Common.TagName)"/>
    /// <seealso cref="All(L5Sharp.Common.TagName)"/>
    public Tag? Find(TagName tagName, string container)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));

        var key = new ComponentKey(nameof(Tag), tagName.Root);

        return Index.Components.TryGetValue(key, out var components) && components.TryGetValue(container, out var tag)
            ? tag.Deserialize<Tag>().Member(tagName.Path)
            : default;
    }

    /// <summary>
    /// Gets the first component in the L5X file having the provided composite type/name key.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to search for.</param>
    /// <returns>The <see cref="LogixComponent"/> with the specified key.</returns>
    /// <exception cref="KeyNotFoundException">No component is found with the provided composite key.</exception>
    /// <remarks>
    /// <para>
    /// If there are multiple different scoped components with the same component key, this method will return the first
    /// found object. For types like <c>Tag</c>, this will be the controller scoped instance (as it is indexed first).
    /// For non-scoped components, there should be only one component anyway. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Get(L5Sharp.Common.ComponentKey,string)"/>
    public LogixComponent Get(ComponentKey key)
    {
        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.Single().Deserialize<LogixComponent>()
            : throw new KeyNotFoundException($"Component not found in L5X: {key}");
    }

    /// <summary>
    /// Gets a component in the L5X file having the provided composite type/name key and container name.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to search for.</param>
    /// <param name="container">The name of the container (controller, program, instruction) in which the
    /// component should be found.</param>
    /// <returns>The <see cref="LogixComponent"/> with the specified key.</returns>
    /// <exception cref="ArgumentException"><c>container</c> is null or empty.</exception>
    /// <exception cref="KeyNotFoundException">No component is found with the provided composite key.</exception>
    /// <remarks>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a components efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public LogixComponent Get(ComponentKey key, string container)
    {
        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));

        return Index.Components.TryGetValue(key, out var scoped) && scoped.TryGetValue(container, out var component)
            ? component.Deserialize<LogixComponent>()
            : throw new KeyNotFoundException($"Component not found in L5X: {key}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public TComponent Get<TComponent>(string name) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);

        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.First().Deserialize<TComponent>()
            : throw new KeyNotFoundException($"Component not found in L5X: {key}");
    }

    /// <summary>
    /// Gets the component of type TComponent from the specified container in the L
    /// 5X index.
    /// </summary>
    /// <typeparam name="TComponent">The type of the component to retrieve.</typeparam>
    /// <param name="name">The name of the component.</param>
    /// <param name="container">The container of the component.</param>
    /// <returns>The component of type TComponent.</returns>
    /// <exception cref="ArgumentException">Thrown when the name or container is null or empty.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the component is not found in the L5X index.</exception>
    public TComponent Get<TComponent>(string name, string container) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);

        return Index.Components.TryGetValue(key, out var scoped) && scoped.TryGetValue(container, out var component)
            ? component.Deserialize<TComponent>()
            : throw new KeyNotFoundException($"Component not found in L5X: {key}");
    }

    public Tag Get(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        var key = new ComponentKey(nameof(Tag), tagName.Root);

        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.First().Deserialize<Tag>()[tagName.Path]
            : throw new KeyNotFoundException($"Tag not found in L5X: {tagName}");
    }

    public Tag Get(TagName tagName, string container)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));

        var key = new ComponentKey(nameof(Tag), tagName.Root);

        return Index.Components.TryGetValue(key, out var components) && components.TryGetValue(container, out var tag)
            ? tag.Deserialize<Tag>()[tagName.Path]
            : throw new KeyNotFoundException($"Tag not found in L5X: {tagName}");
    }

    public void Remove(ComponentKey key)
    {
        if (!Index.Components.TryGetValue(key, out var components)) return;
        foreach (var component in components)
            component.Value.Remove();
    }

    public void Remove(ComponentKey key, string container)
    {
        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));
        
        if (Index.Components.TryGetValue(key, out var components) &&
            components.TryGetValue(container, out var element))
        {
            element.Remove();   
        }
    }

    public void Remove<TComponent>(string name) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);
        
        if (!Index.Components.TryGetValue(key, out var components)) return;
        foreach (var component in components)
            component.Value.Remove();
    }

    public IEnumerable<CrossReference> ReferencesTo(LogixComponent component)
    {
        if (component is null) throw new ArgumentNullException(nameof(component));

        return Index.References.TryGetValue(component.Key, out var references)
            ? references
            : Enumerable.Empty<CrossReference>();
    }

    public IEnumerable<CrossReference> ReferencesTo<TComponent>(string name) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name can not be null or empty.", nameof(name));
        var key = new ComponentKey(typeof(TComponent).L5XType(), name);
        return Index.References.TryGetValue(key, out var references) ? references : Enumerable.Empty<CrossReference>();
    }

    /// <summary>
    /// Merges the specified L5X file with the current <see cref="L5X"/> L5X by adding or overwriting logix components.
    /// </summary>
    /// <param name="fileName">The file name of L5X to merge.</param>
    /// <param name="overwrite">A bit indicating whether to overwrite incoming components of the same name.</param>
    /// <exception cref="ArgumentException"><c>fileName</c> is null or empty.</exception>
    public void Import(string fileName, bool overwrite = true)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("FileName can not be null or empty.", nameof(fileName));
        var content = Load(fileName);
        MergeContent(content, overwrite);
    }

    /// <summary>
    /// Merges another <see cref="L5X"/> file into the current L5X by adding or overwriting logix components.
    /// </summary>
    /// <param name="content">The <see cref="L5X"/> to merge.</param>
    /// <param name="overwrite">A bit indicating whether to overwrite incoming components of the same name.</param>
    /// <exception cref="ArgumentNullException"><c>content</c> is null.</exception>
    public void Import(L5X content, bool overwrite = true)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));
        MergeContent(content, overwrite);
    }

    /// <summary>
    /// Serialize this <see cref="L5X"/> to a file, overwriting an existing file, if it exists.
    /// </summary>
    /// <param name="fileName">A string that contains the name of the file.</param>
    public void Save(string fileName) => SaveContent(fileName);

    /// <inheritdoc />
    public XElement Serialize() => _content;

    /// <inheritdoc />
    public override string ToString() => _content.ToString();

    #region Internal

    /// <summary>
    /// Gets a top level container element from the root controller element of the L5X.
    /// </summary>
    /// <param name="name">The name of the container to retrieve.</param>
    /// <returns>A <see cref="XElement"/> representing the container with the provided name.</returns>
    /// <exception cref="InvalidOperationException">The element does not exist.</exception>
    private XElement GetContainer(string name) => GetController().Element(name) ?? throw _content.L5XError(name);

    /// <summary>
    /// Gets all primary/top level L5X component containers in the current L5X file.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="XElement"/> representing the L5X component containers.</returns>
    private IEnumerable<XElement> GetContainers() => Containers.Select(name => GetController().Element(name)).ToList();

    /// <summary>
    /// Gets the root controller element of the L5X file. We expect this to always exist if the L5X is constructed
    /// due to the normalization process. 
    /// </summary>
    private XElement GetController() =>
        _content.Element(L5XName.Controller) ?? throw _content.L5XError(L5XName.Controller);

    /// <summary>
    /// Gets the name of the controller element of the L5X file. Will default to empty string if not found. 
    /// </summary>
    private string GetControllerName() => GetController().LogixName();

    /// <summary>
    /// Merges all top level containers and their immediate child elements between the current L5X content and the
    /// provided L5X content. Will overwrite if specified.
    /// </summary>
    /// <param name="l5X">The L5X element to merge with the current target element.</param>
    /// <param name="overwrite">A flag to indicate whether to overwrite child elements of matching name.</param>
    private void MergeContent(L5X l5X, bool overwrite)
    {
        if (l5X is null) throw new ArgumentNullException(nameof(l5X));

        var containerPairs = GetContainers()
            .Join(l5X.GetContainers(), e => e.Name, e => e.Name, (a, b) => new { a, b })
            .ToList();

        foreach (var pair in containerPairs)
            MergeContainers(pair.a, pair.b, overwrite);
    }

    /// <summary>
    /// Given to top level containers, adds or replaces all child elements matching based on the logix name of the elements.
    /// </summary>
    private static void MergeContainers(XContainer target, XContainer source, bool overwrite)
    {
        foreach (var element in source.Elements())
        {
            var match = target.Elements().FirstOrDefault(e => e.LogixName() == element.LogixName());

            if (match is null)
            {
                target.Add(element);
                continue;
            }

            if (overwrite)
                match.ReplaceWith(element);
        }
    }

    /// <summary>
    /// Creates a new default content element for a new instance of an L5X file given the provided target name and type.
    /// </summary>
    private static XElement NewContent(string targetName, string targetType, Revision? softwareRevision)
    {
        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, new Revision()));
        if (softwareRevision is not null) content.Add(new XAttribute(L5XName.SoftwareRevision, softwareRevision));
        content.Add(new XAttribute(L5XName.TargetName, targetName));
        content.Add(new XAttribute(L5XName.TargetType, targetType));
        content.Add(new XAttribute(L5XName.ContainsContext, targetType != nameof(Controller)));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        return content;
    }

    /// <summary>
    /// If no root controller element exists, adds new context controller and moves all root elements into that controller
    /// element. Then adds missing top level containers to ensure consistent structure of the root L5X.
    /// </summary>
    private void NormalizeContent()
    {
        if (_content.Element(L5XName.Controller) is null)
        {
            var context = new XElement(L5XName.Controller, new XAttribute(L5XName.Use, Use.Context));
            context.Add(_content.Elements());
            _content.RemoveNodes();
            _content.Add(context);
        }

        var controller = _content.Element(L5XName.Controller)!;

        foreach (var container in from container in Containers
                 let existing = controller.Element(container)
                 where existing is null
                 select container)
        {
            controller.Add(new XElement(container));
        }
    }

    /// <summary>
    /// Create document, adds default declaration, and saves the current L5X content to the specified file name.
    /// </summary>
    /// <param name="fileName">A string that contains the name of the file.</param>
    private void SaveContent(string fileName)
    {
        //This will sanitize containers that were perhaps added when normalizing that went unused.
        foreach (var container in GetContainers().Where(c => !c.HasElements))
            container.Remove();

        var declaration = new XDeclaration("1.0", "UTF-8", "yes");
        var document = new XDocument(declaration);
        document.Add(_content);
        document.Save(fileName);
    }

    #endregion
}