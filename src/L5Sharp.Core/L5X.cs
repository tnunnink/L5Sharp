using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TTask = System.Threading.Tasks.Task;


namespace L5Sharp.Core;

/// <summary>
/// This is the primary entry point for interacting with the L5X file.
/// Provides access to query and manipulate logix components, elements, containers, and more. 
/// </summary>
/// <remarks>
/// </remarks>
public class L5X : ILogixSerializable
{
    /// <summary>
    /// The date/time format for the L5X content.
    /// </summary>
    private const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

    /// <summary>
    /// The underlying root RSLogix5000Content element of the L5X file.
    /// </summary>
    private readonly XElement _content;

    /// <summary>
    /// An index of all logix components in the L5X file for fast lookups. This field is not loaded or constructed until
    /// requested by an associated method call.
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
    private static readonly List<string> Containers =
    [
        L5XName.DataTypes,
        L5XName.Modules,
        L5XName.AddOnInstructionDefinitions,
        L5XName.Tags,
        L5XName.Programs,
        L5XName.Tasks,
        L5XName.ParameterConnections,
        L5XName.Trends,
        L5XName.QuickWatchLists
    ];

    /// <summary>
    /// Creates a new <see cref="L5X"/> instance wrapping the provided <see cref="XElement"/> content object.
    /// </summary>
    /// <param name="content">The root <see cref="XElement"/> object representing the RSLogix5000Content element of the
    /// L5X file.</param>
    /// <exception cref="ArgumentNullException"><c>content</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>content</c> name is not expected <c>RSLogix5000Content</c>.</exception>
    /// <remarks>
    /// Although you create the L5X instance by providing a valid XML element, it is or more typical to use the
    /// static factory methods <see cref="Load"/> to load a file from disc, <see cref="Parse"/> to parse the XML from a string,
    /// or <see cref="New(string,string,Revision?)"/> to generate a new default instance initialized with
    /// the root RSLogix5000Content element and attributes. Also note that each individual component can be exported to
    /// generate a new L5X content file.
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
        //allowing child elements to retrieve the object locally without creating a new instance and potentially
        //reindexing of the XML content. This allows them to reference to root L5X for cross referencing or other operations.
        _content.AddAnnotation(this);
    }

    /// <summary>
    /// The <see cref="LogixInfo"/> representing the L5X content export information.
    /// </summary>
    public LogixInfo Info => new(_content);

    /// <summary>
    /// The root <see cref="Core.Controller"/> component of the L5X file.
    /// </summary>
    /// <value>A <see cref="Core.Controller"/> component object.</value>
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

    //Async overload not supported in .NET Standard 2.0
#if NET7_0_OR_GREATER
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
        var l5X = await TTask.Run(() => new L5X(element), token);
        return l5X;
    }
#endif

    /// <summary>
    /// Creates a new blank <see cref="L5X"/> file with the standard root content and controller elements, and configures them
    /// with the provided controller name, processor, and revision. 
    /// </summary>
    /// <param name="name">The name of the controller.</param>
    /// <param name="processor">The processor type of the controller.</param>
    /// <param name="revision">The optional software revision of the processor.</param>
    /// <returns>A new <see cref="L5X"/> instance with the specified controller properties.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="processor"/> is null or empty.</exception>
    public static L5X New(string name, string processor, Revision? revision = null)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name can not be null or empty.");
        if (string.IsNullOrEmpty(processor)) throw new ArgumentException("Name can not be null or empty.");

        var content = NewContent(name, nameof(Controller), revision);

        var controller = new XElement(L5XName.Controller,
            new XAttribute(L5XName.Name, name),
            new XAttribute(L5XName.ProcessorType, processor)
        );

        content.Add(controller);
        return new L5X(content);
    }

    /// <summary>
    /// Creates a new <see cref="L5X"/> file with the provided <see cref="LogixComponent"/> object instance as the export
    /// target of the file.
    /// </summary>
    /// <param name="component">The component that will serve as the target for the new L5X.</param>
    /// <param name="revision">The software revision of the L5X file.</param>
    /// <returns>A new <see cref="L5X"/> with the default root and target component as the content of the file.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="component"/> is null.</exception>
    public static L5X New(LogixComponent component, Revision? revision = null)
    {
        if (component is null) throw new ArgumentNullException(nameof(component));
        var content = NewContent(component.Name, component.L5XType, revision);
        var file = new L5X(content);
        file.Add(component);
        return file;
    }

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
    /// Creates a new instance of <see cref="L5X"/> with an empty content.
    /// </summary>
    /// <param name="targetName">The optional target name for the new L5X content. Will default to empty string.</param>
    /// <param name="targetType">The optional target type for the new L5X content. Will default to Controller.</param>
    /// <returns>A new <see cref="L5X"/> instance with an empty content.</returns>
    public static L5X Empty(string? targetName = default, string? targetType = default)
    {
        return new L5X(NewContent(targetName ?? string.Empty, targetType ?? L5XName.Controller, new Revision()));
    }

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
    /// Adds the provided <see cref="LogixComponent"/> to the first found container within the L5X file. 
    /// </summary>
    /// <param name="component">The component to add to the L5X.</param>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file, and since most components have a single top level
    /// container, it will work for most types. However, note that this only adds to the first container found of the specific type.
    /// If you are adding scoped components such as <see cref="Tag"/> or <see cref="Routine"/> you should be doing so in the context
    /// of a specific <see cref="Program"/> component, or use the overload accepting a specific container.
    /// </remarks>
    /// <seealso cref="Add(LogixComponent,string)"/>
    public void Add(LogixComponent component)
    {
        var containerType = component.GetType().L5XContainer();
        var container = GetContainer(containerType);
        container.Add(component.Serialize());
    }

    /// <summary>
    /// Adds the provided <see cref="LogixComponent"/> to the specified container within the L5X file. 
    /// </summary>
    /// <param name="component">The component to add to the L5X.</param>
    /// <param name="container">The container name (controller, program, or instruction) in which to add the component.</param>
    /// <exception cref="InvalidOperationException">No container was found in the L5X for the specified type and container name.</exception>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file. This method will look for a container element
    /// within the specified container/scope name. Therefore it is important to be sure you specify the correct type and
    /// container name. For example, if you try to add a Module to a Program, this will fail even if a program with the
    /// container name exists, because there is no Modules container in a Program component.
    /// </remarks>
    public void Add(LogixComponent component, string container)
    {
        var type = component.GetType().L5XContainer();
        var target = _content.Descendants(type).FirstOrDefault(e => Scope.Container(e) == container);
        if (target is null)
            throw new InvalidOperationException($"Not container with name '{container}' was found in the L5X.");
        target.Add(component.Serialize());
    }

    /// <summary>
    /// Retrieves all components with the specified key.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to search for.</param>
    /// <returns>A collection of <see cref="LogixComponent"/> having the specified type/name composite key.</returns>
    /// <remarks>
    /// <para>
    /// Note that this method returns all components with the type/name pair. This is typically a single component,
    /// but types like Tag can have same name across different scopes, so it may be multiple different objects.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(ComponentKey)"/>
    /// <seealso cref="Get(ComponentKey)"/>
    public IEnumerable<LogixComponent> All(ComponentKey key)
    {
        return Index.Components.TryGetValue(key, out var components)
            ? components.Select(c => c.Value.Deserialize<LogixComponent>())
            : Enumerable.Empty<LogixComponent>();
    }

    /// <summary>
    /// Retrieves all components with the specified name nad type.
    /// </summary>
    /// <param name="name">The name of the components to retrieve.</param>
    /// <typeparam name="TComponent">The type of components to retrieve.</typeparam>
    /// <returns>A collection of <see cref="LogixComponent"/> having the specified type/name composite key.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// Note that this method returns all components with the type/name pair. This is typically a single component,
    /// but types like <c>Tag</c> or <c>Routine</c> can have same name across different scopes,
    /// so it may be multiple different objects.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find{TComponent}(string)"/>
    /// <seealso cref="Get{TComponent}(string)"/>
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
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(TagName,string)"/>
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
    /// This method makes use of the internal component index to find a component efficiently.
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
    /// This method makes use of the internal component index to find a component efficiently.
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
    /// found object. For types like <c>Tag</c>, this will typically be the controller scoped instance (as it is indexed first).
    /// For non-scoped components, there should be only one component anyway. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(ComponentKey,string)"/>
    public LogixComponent? Find(ComponentKey key)
    {
        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.First().Deserialize<LogixComponent>()
            : default;
    }

    /// <summary>
    /// Finds a component in the L5X file having the provided composite type/name key and container name.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> to search for.</param>
    /// <param name="container">The name of the container (controller, program, instruction) in which the
    /// component should be found.</param>
    /// <returns>The found <see cref="LogixComponent"/> if it exists, otherwise returns null.</returns>
    /// <exception cref="ArgumentException"><paramref name="container"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
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
    /// <exception cref="ArgumentException"><paramref name="name"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// If there are multiple different scoped components with the same type and name, this method will return the first
    /// found object. For types like <c>Tag</c>, this will be the controller scoped instance (as it is indexed first).
    /// For non-scoped components, there should be only one component anyway. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
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
            ? components.Values.First().Deserialize<TComponent>()
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
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="container"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
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
    /// <returns>The first found <see cref="Tag"/> if it exists, otherwise returns null.</returns>
    /// <exception cref="ArgumentException"><c>tagName</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method only differs from the more generic Find methods in that it also returns a nested tag member if
    /// specified by the <paramref name="tagName"/> path. This means the caller can directly and efficiently get either
    /// a root tag component, or one of it's nested child members. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(TagName,string)"/>
    /// <seealso cref="All(TagName)"/>
    /// <seealso cref="Get(TagName)"/>
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
    /// <exception cref="ArgumentException"><c>tagName</c> is null -or- <paramref name="container"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// This method only differs from the more generic Find methods in that it also returns a nested tag member if
    /// specified by the <paramref name="tagName"/> path. This means the caller can directly and efficiently get either
    /// a root tag component, or one of it's nested child members. 
    /// </para>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Find(TagName)"/>
    /// <seealso cref="All(TagName)"/>
    /// <seealso cref="Get(TagName)"/>
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
    /// <returns>The <see cref="LogixComponent"/> with the specified key. If no component is found, then an exception
    /// is thrown.</returns>
    /// <exception cref="KeyNotFoundException">No component is found with the provided composite key.</exception>
    /// <remarks>
    /// <para>
    /// If there are multiple different scoped components with the same component key, this method will return the first
    /// found object. For types like <c>Tag</c>, this will be the controller scoped instance (as it is indexed first).
    /// For non-scoped components, there should be only one component anyway. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Get(ComponentKey,string)"/>
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
    /// <returns>The <see cref="LogixComponent"/> with the specified key. If no component is found, then an exception
    /// is thrown.</returns>
    /// <exception cref="ArgumentException"><paramref name="container"/> is null or empty.</exception>
    /// <exception cref="KeyNotFoundException">No component is found with the provided composite key.</exception>
    /// <remarks>
    /// <para>
    /// This method allows the caller to further specify which scoped component they would like to retrieve, instead of
    /// just getting the first found component object. Container represents the name of the controller, program, or
    /// instruction the scoped component is/should be contained in. If no component is found in that scope,
    /// this method returns null.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
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
    /// Gets the first component in the L5X file having the provided type and name.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The type of component to find.</typeparam>
    /// <returns>The <see cref="LogixComponent"/> with the specified key. If no component is found, then an exception
    /// is thrown.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is null or empty.</exception>
    /// <exception cref="KeyNotFoundException">No component is found with the provided composite key.</exception>
    /// <remarks>
    /// <para>
    /// If there are multiple different scoped components with the same type and name, this method will return the first
    /// found object. For types like <c>Tag</c>, there may be multiple components with the same name across difference scopes.
    /// For non-scoped components, there should be only one "controller scoped" component. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
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
    /// Gets the first component in the L5X file having the provided type and name.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <param name="container">The name of the container (controller, program, instruction) in which the
    /// component should be found.</param>
    /// <typeparam name="TComponent">The type of component to find.</typeparam>
    /// <returns>The <see cref="LogixComponent"/> with the specified key. If no component is found, then an exception
    /// is thrown.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="container"/> is null or empty.</exception>
    /// <exception cref="KeyNotFoundException">No component is found with the provided composite key.</exception>
    /// <remarks>
    /// <para>
    /// If there are multiple different scoped components with the same type and name, this method will return the first
    /// found object. For types like <c>Tag</c>, there may be multiple components with the same name across difference scopes.
    /// For non-scoped components, there should be only one "controller scoped" component. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
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

    /// <summary>
    /// Gets the first <see cref="Tag"/> in the L5X file having the specified tag name.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> of the tag to get.</param>
    /// <returns>The <see cref="Tag"/> with the specified key. If a multiple scoped tags with the same name exist, this
    /// will return the first tag instance found. If not tag is found, then an exception is thrown.</returns>
    /// <exception cref="ArgumentException"><c>tagName</c> is null.</exception>
    /// <exception cref="KeyNotFoundException">No tag with the provided tag name was found.</exception>
    /// <remarks>
    /// <para>
    /// This method only differs from the more generic Get methods in that it also returns a nested tag member if
    /// specified by the <paramref name="tagName"/> path. This means the caller can directly and efficiently get either
    /// a root tag component, or one of it's nested child members. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Get(TagName,string)"/>
    /// <seealso cref="All(TagName)"/>
    /// <seealso cref="Find(TagName)"/>
    public Tag Get(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        var key = new ComponentKey(nameof(Tag), tagName.Root);

        return Index.Components.TryGetValue(key, out var components)
            ? components.Values.First().Deserialize<Tag>()[tagName.Path]
            : throw new KeyNotFoundException($"Tag not found in L5X: {tagName}");
    }

    /// <summary>
    /// Gets the first <see cref="Tag"/> in the L5X file having the specified tag name.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> of the tag to get.</param>
    /// <param name="container">The name of the container (controller, program, instruction) in which the
    /// tag should be found.</param>
    /// <returns>The <see cref="Tag"/> with the specified key. If a multiple scoped tags with the same name exist, this
    /// will return the first tag instance found. If no tag is found, then an exception is thrown.</returns>
    /// <exception cref="ArgumentException"><c>tagName</c> is null.</exception>
    /// <exception cref="KeyNotFoundException">No tag with the provided tag name was found.</exception>
    /// <remarks>
    /// <para>
    /// This method only differs from the more generic Get methods in that it also returns a nested tag member if
    /// specified by the <paramref name="tagName"/> path. This means the caller can directly and efficiently get either
    /// a root tag component, or one of it's nested child members. 
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    /// <seealso cref="Get(TagName)"/>
    /// <seealso cref="All(TagName)"/>
    /// <seealso cref="Find(TagName)"/>
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

    /// <summary>
    /// Removes the component(s) with the specified composite type/name key from the L5X file.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> of the component to remove.</param>
    /// <remarks>
    /// <para>
    /// Note that this will remove all components with the type/name pair. For scoped components types such as
    /// <c>Tag</c> and <c>Routine</c> this may be more than a single component.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public void Remove(ComponentKey key)
    {
        if (!Index.Components.TryGetValue(key, out var components)) return;
        foreach (var component in components)
            component.Value.Remove();
    }

    /// <summary>
    /// Removes a single component with the specified composite type/name key and container from the L5X file.
    /// </summary>
    /// <param name="key">The <see cref="ComponentKey"/> of the component to remove.</param>
    /// <param name="container">The container name of the component to remove.</param>
    /// <remarks>
    /// <para>
    /// This method should only remove a single component as it allows the caller to further define the container
    /// or scope the component should be found in.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
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

    /// <summary>
    /// Removes the component(s) with the specified type/name from the L5X file.
    /// </summary>
    /// <param name="name">The name of the component to remove.</param>
    /// <typeparam name="TComponent">The type of the component to remove.</typeparam>
    /// <exception cref="ArgumentException"><paramref name="name"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// Note that this will remove all components with the type/name pair. For scoped components types such as
    /// <c>Tag</c> and <c>Routine</c> this may be more than a single component.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public void Remove<TComponent>(string name) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);

        if (!Index.Components.TryGetValue(key, out var components)) return;
        foreach (var component in components)
            component.Value.Remove();
    }

    /// <summary>
    /// Removes a single component with the specified type/name pair and container from the L5X file.
    /// </summary>
    /// <param name="name">The name of the component to remove.</param>
    /// <param name="container">The container of the component to remove.</param>
    /// <typeparam name="TComponent">The type of the component to remove.</typeparam>
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="container"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// This method should only remove a single component as it allows the caller to further define the container
    /// or scope the component should be found in.
    /// </para>
    /// <para>
    /// This method makes use of the internal component index to find a component efficiently.
    /// Since components have (mostly) unique type/name pairs, we can index them and find them quickly.
    /// This is needed for operations that might be more computationally complex, such as iterating many components
    /// and finding references or dependents.
    /// </para>
    /// </remarks>
    public void Remove<TComponent>(string name, string container) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(container))
            throw new ArgumentException("Container can not be null or empty.", nameof(container));

        var key = new ComponentKey(typeof(TComponent).L5XType(), name);

        if (Index.Components.TryGetValue(key, out var components) &&
            components.TryGetValue(container, out var element))
        {
            element.Remove();
        }
    }

    /// <summary>
    /// Retrieves all <see cref="CrossReference"/> objects found in the L5X file.
    /// </summary>
    /// <returns>A collection of all <see cref="CrossReference"/> objects found in the L5X.</returns>
    /// <remarks>
    /// <para>
    /// A cross-reference object contains information about the element and location of the object that has a reference
    /// to a given component. This library has a built-in mechanism for parsing and indexing both tag and logic references
    /// to various components for efficient lookup. This can allow the caller to find references for many objects at a time
    /// without having to iterate the L5X multiple times.
    /// </para>
    /// </remarks>
    public IEnumerable<CrossReference> References() => Index.References.SelectMany(r => r.Value);

    /// <summary>
    /// Retrieves a collection of <see cref="CrossReference"/> objects that reference the specified <paramref name="component"/>.
    /// </summary>
    /// <param name="component">The <see cref="LogixComponent"/> to retrieve references for.</param>
    /// <returns>A collection of <see cref="CrossReference"/> objects that reference the specified <paramref name="component"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="component"/> is null.</exception>
    /// <remarks>
    /// <para>
    /// A cross-reference object contains information about the element and location of the object that has a reference
    /// to a given component. This library has a built-in mechanism for parsing and indexing both tag and logic references
    /// to various components for efficient lookup. This can allow the caller to find references for many objects at a time
    /// without having to iterate the L5X multiple times.
    /// </para>
    /// </remarks>
    public IEnumerable<CrossReference> References(LogixComponent component)
    {
        if (component is null) throw new ArgumentNullException(nameof(component));

        return Index.References.TryGetValue(component.Key, out var references)
            ? references
            : Enumerable.Empty<CrossReference>();
    }

    /// <summary>
    /// /// Retrieves a collection of <see cref="CrossReference"/> objects that reference the specified component
    /// type and name.
    /// </summary>
    /// <param name="name">The name of the component to retrieve references for.</param>
    /// <typeparam name="TComponent">The type of component to retrieve references for.</typeparam>
    /// <returns>A collection of <see cref="CrossReference"/> objects that reference the specified <paramref name="name"/>.</returns>
    /// <exception cref="ArgumentException">Throw if <paramref name="name"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// A cross-reference object contains information about the element and location of the object that has a reference
    /// to a given component. This library has a built-in mechanism for parsing and indexing both tag and logic references
    /// to various components for efficient lookup. This can allow the caller to find references for many objects at a time
    /// without having to iterate the L5X multiple times.
    /// </para>
    /// </remarks>
    public IEnumerable<CrossReference> References<TComponent>(string name) where TComponent : LogixComponent
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

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
    /// Gets the root controller element of the L5X file. We expect this to always exist if the L5X is constructed
    /// due to the normalization process. 
    /// </summary>
    private XElement GetController()
    {
        return _content.Element(L5XName.Controller) ?? throw _content.L5XError(L5XName.Controller);
    }

    /// <summary>
    /// Gets a top level container element from the root controller element of the L5X.
    /// </summary>
    /// <param name="name">The name of the container to retrieve.</param>
    /// <returns>A <see cref="XElement"/> representing the container with the provided name.</returns>
    /// <exception cref="InvalidOperationException">The element does not exist.</exception>
    private XElement GetContainer(string name)
    {
        return _content.Element(L5XName.Controller)?.Element(name) ?? throw _content.L5XError(name);
    }

    /// <summary>
    /// Gets all primary/top level L5X component containers in the current L5X file.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="XElement"/> representing the L5X component containers.</returns>
    private IEnumerable<XElement> GetContainers()
    {
        return Containers
            .Select(name => _content.Element(L5XName.Controller)?.Element(name))
            .Where(e => e is not null)
            .Cast<XElement>()
            .ToList();
    }

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

        foreach (var container in Containers)
        {
            var existing = controller.Element(container);
            if (existing is not null) continue;
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