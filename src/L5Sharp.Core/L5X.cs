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
public sealed class L5X : ILogixSerializable, ILogixLookup
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
    /// The <see cref="ILogixLookup"/> implementation used to quickly find objects in the file.
    /// The actual implementation that is instantiated is determined by the <see cref="L5XOptions"/> provided to the constructor.
    /// </summary>
    private readonly ILogixLookup _lookup;

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
    /// <param name="options">The <see cref="L5XOptions"/> that control how the L5X is initialized.
    /// The default is none, meaning no extra initialization steps are performed.
    /// </param>
    /// <exception cref="ArgumentNullException"><c>content</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>content</c> name is not expected <c>RSLogix5000Content</c>.</exception>
    private L5X(XElement content, L5XOptions options = L5XOptions.None)
    {
        if (content is null)
            throw new ArgumentNullException(nameof(content));

        if (content.Name != L5XName.RSLogix5000Content)
            throw new ArgumentException(
                $"Expecting root element name of {L5XName.RSLogix5000Content} to initialize L5X.");

        _content = content;

        //We will "normalize" (ensure consistent root controller element and component containers) for all
        //files so that we won't have issues getting top level containers.
        //When saving, we can remove unused containers. 
        NormalizeContent();

        //This stores the L5X object as an in-memory object for the root XElement,
        //allowing child elements to retrieve the object locally without creating a new instance and potentially
        //reindexing of the XML content if requested.
        //This allows them to reference to root L5X for cross-referencing or other lookup-based operations.
        _content.AddAnnotation(this);

        //Depending on the options provided, either create a logix index or logix lookup for the ILogixLoop API usage.
        if (options == L5XOptions.Index)
        {
            //LogixIndex uses dictionaries and is super performant.
            _lookup = new LogixIndex(GetController());
        }
        else
        {
            //LogixLookup uses XPath with is not terrible but will be slow for many lookups (mostly for tag elements).
            _lookup = new LogixLookup(GetController());
        }
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
    /// relevant controller properties and configurations. Otherwise, most data will be null as the controller serves as
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
    /// <remarks>To access program-specific tag collection user the <see cref="Programs"/> collection.</remarks>
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
    /// <param name="fileName">A URI string referencing the file to load.</param>
    /// <param name="options">The <see cref="L5XOptions"/> that control how the L5X is initialized.
    /// The default is none, meaning no extra initialization steps are performed.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified file.</returns>
    /// <exception cref="ArgumentException">The string is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="XElement.Load(string)"/> to load the contents of the XML file into
    /// memory. This means that this method is subject to the same exceptions that could be generated by loading the
    /// XElement, such as XML format exceptions
    /// </remarks>
    public static L5X Load(string fileName, L5XOptions options = L5XOptions.None)
    {
        return new L5X(XElement.Load(fileName), options);
    }

    //Async overload isn't supported in .NET Standard 2.0
#if NET7_0_OR_GREATER
    /// <summary>
    /// Asynchronously loads te specified file path and returns the contents as a new <see cref="L5X"/> instance.
    /// </summary>
    /// <param name="fileName">A URI string referencing the file to load.</param>
    /// <param name="options">The <see cref="L5XOptions"/> that control how the L5X is initialized.
    /// The default is none, meaning no extra initialization steps are performed.</param>
    /// <param name="token">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified file.</returns>
    /// <remarks>This method can support opening either</remarks>
    public static async Task<L5X> LoadAsync(string fileName, L5XOptions options = L5XOptions.None,
        CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("File name can not be null or empty.", nameof(fileName));

        await using var stream = new FileStream(fileName, FileMode.Open);
        var element = await XElement.LoadAsync(stream, LoadOptions.SetLineInfo, token);
        var file = await TTask.Run(() => new L5X(element, options), token);
        return file;
    }
#endif

    /// <summary>
    /// Creates a new <see cref="L5X"/> with the provided L5X string content.
    /// </summary>
    /// <param name="text">The string that contains the L5X content to parse.</param>
    /// <param name="options">The <see cref="L5XOptions"/> that control how the L5X is initialized.
    /// The default is none, meaning no extra initialization steps are performed.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified string.</returns>
    /// <exception cref="ArgumentException">The string is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="XElement.Parse(string)"/> to load the contents of the XML file into
    /// memory. This means that this method is subject to the same exceptions that could be generated by parsing the
    /// XElement.
    /// </remarks>
    public static L5X Parse(string text, L5XOptions options = L5XOptions.None) => new(XElement.Parse(text), options);

    /// <summary>
    /// Creates a new instance of <see cref="L5X"/> with an empty content.
    /// </summary>
    /// <param name="targetName">The optional target name for the new L5X content. Will default to empty string.</param>
    /// <param name="targetType">The optional target type for the new L5X content. Will default to Controller.</param>
    /// <returns>A new <see cref="L5X"/> instance with an empty content.</returns>
    public static L5X Empty(string? targetName = null, string? targetType = null)
    {
        return new L5X(NewContent(targetName ?? string.Empty, targetType ?? L5XName.Controller, new Revision()));
    }

    /// <summary>
    /// Creates a new blank <see cref="L5X"/> file with the standard root content and controller elements,
    /// and configures them with the provided controller name, processor, and revision. Also adds the Local controller
    /// Module component as Studio does so that module scan be added in memory. 
    /// </summary>
    /// <param name="name">The name of the controller.</param>
    /// <param name="processor">The processor type of the controller.</param>
    /// <param name="revision">The optional software revision of the processor.</param>
    /// <returns>A new <see cref="L5X"/> instance with the specified controller properties.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="processor"/> is null or empty.</exception>
    public static L5X New(string name, string processor, Revision revision)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name can not be null or empty.");
        if (string.IsNullOrEmpty(processor)) throw new ArgumentException("Processor can not be null or empty.");
        if (revision is null) throw new ArgumentException("Revision can not be null.");

        var content = NewContent(name, nameof(Controller), revision);
        var controller = new Controller(name, processor, revision) { Use = Use.Target };
        content.Add(controller.Serialize());

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
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var content = NewContent(component.Name, component.L5XType, revision);

        var file = new L5X(content);
        file.Add(component);

        return file;
    }

    /// <summary>
    /// Finds element across the entire L5X with the provided type as a flat collection of object. 
    /// </summary>
    /// <param name="type">The type name or element name to retrieve.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found object with the provided type name.</returns>
    /// <exception cref="ArgumentException"><c>type</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. This method allows
    /// specifying the type at runtime as opposed the generic type but sacrifices the strong type querying of the
    /// generic counterpart. This method does not make use of any optimized searching. If you want efficient lookup,
    /// explore the methods defined by the <see cref="ILogixLookup"/> API such as <see cref="Find"/>.
    /// </para>
    /// <para>
    /// Also note that this will call <c>L5XType</c> extension internally which returns all configured
    /// element name for the provided type. This means the query will return all elements that the type supports.
    /// This is in contrast to something list <see cref="Tags"/>, which just returns controller scoped tags.
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
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. This method allows
    /// specifying the type at runtime as opposed the generic type but sacrifices the strong type querying of the
    /// generic counterpart. This method does not make use of any optimized searching. If you want efficient lookup,
    /// explore the methods defined by the <see cref="ILogixLookup"/> API such as <see cref="Find"/>.
    /// </para>
    /// <para>
    /// Also note that this will call <c>L5XType</c> extension internally which returns all configured
    /// element name for the provided type. This means the query will return all elements that the type supports.
    /// This is in contrast to something list <see cref="Tags"/>, which just returns controller scoped tags.
    /// </para>
    /// </remarks>
    public IEnumerable<LogixElement> Query(Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type), "Type is required to retrieve elements from the L5X");

        var types = new HashSet<string>(type.L5XTypes());

        foreach (var descendant in _content.Descendants())
        {
            if (!types.Contains(descendant.L5XType())) continue;
            yield return descendant.Deserialize();
        }
    }

    /// <summary>
    /// Finds elements of the specified type across the entire L5X as a flat collection of objects.
    /// </summary>
    /// <typeparam name="TObject">The element type to find.</typeparam>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found objects of the specified type.</returns>
    /// <remarks>
    /// <para>
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. Since
    /// it returns <see cref="IEnumerable{T}"/>, you can make use of LINQ and the strongly typed objects to build
    /// more complex queries.
    ///</para>
    /// <para>
    /// This method does not make use of any optimized searching. If you want efficient lookup,
    /// explore the methods defined by the <see cref="ILogixLookup"/> API such as <see cref="Find"/>.
    /// </para>
    /// </remarks>
    public IEnumerable<TObject> Query<TObject>() where TObject : LogixObject, new()
    {
        var typeNames = typeof(TObject).L5XTypes().ToList();

        return _content.Descendants()
            .Where(e => typeNames.Any(n => n.IsEquivalent(e.L5XType())))
            .Select(e => e.Deserialize<TObject>());
    }

    /// <summary>
    /// Executes a query on the content of the L5X file, filtering elements based on the provided predicate.
    /// </summary>
    /// <param name="predicate">A function that defines the criteria for the elements to be included in the result.</param>
    /// <typeparam name="TObject">The type of LogixElement to query for.</typeparam>
    /// <returns>An enumerable collection of elements of type TElement that satisfy the predicate.</returns>
    /// <remarks>
    /// <para>
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. Since
    /// it returns <see cref="IEnumerable{T}"/>, you can make use of LINQ and the strongly typed objects to build
    /// more complex queries.
    ///</para>
    /// <para>
    /// This method does not make use of any optimized searching. If you want efficient lookup,
    /// explore the methods defined by the <see cref="ILogixLookup"/> API such as <see cref="Find"/>.
    /// </para>
    /// </remarks>
    public IEnumerable<TObject> Query<TObject>(Func<TObject, bool> predicate) where TObject : LogixObject, new()
    {
        var typeNames = typeof(TObject).L5XTypes().ToList();

        return _content.Descendants()
            .Where(e => typeNames.Any(n => n.IsEquivalent(e.L5XType())))
            .Select(e => e.Deserialize<TObject>())
            .Where(predicate);
    }

    /// <inheritdoc />
    public bool Contains(Scope scope)
    {
        return _lookup.Contains(scope);
    }

    /// <inheritdoc />
    public bool Contains(Func<IScopeBuilder, Scope> builder)
    {
        return _lookup.Contains(builder);
    }

    /// <inheritdoc />
    public IEnumerable<LogixScoped> Find(Scope scope)
    {
        return _lookup.Find(scope);
    }

    /// <inheritdoc />
    public IEnumerable<TScoped> Find<TScoped>(Scope scope) where TScoped : LogixScoped
    {
        return _lookup.Find<TScoped>(scope);
    }

    /// <inheritdoc />
    public LogixScoped Get(Scope scope)
    {
        return _lookup.Get(scope);
    }

    /// <inheritdoc />
    public TScoped Get<TScoped>(Scope scope) where TScoped : LogixScoped
    {
        return _lookup.Get<TScoped>(scope);
    }

    /// <inheritdoc />
    public LogixScoped Get(Func<IScopeBuilder, Scope> builder)
    {
        return _lookup.Get(builder);
    }

    /// <inheritdoc />
    public TScoped Get<TScoped>(Func<IScopeBuilder, Scope> builder) where TScoped : LogixScoped
    {
        return _lookup.Get<TScoped>(builder);
    }

    /// <inheritdoc />
    public bool TryGet(Scope scope, out LogixScoped element)
    {
        return _lookup.TryGet(scope, out element);
    }

    /// <inheritdoc />
    public bool TryGet<TScoped>(Scope scope, out TScoped element) where TScoped : LogixScoped
    {
        return _lookup.TryGet(scope, out element);
    }

    /// <inheritdoc />
    public bool TryGet(Func<IScopeBuilder, Scope> builder, out LogixScoped element)
    {
        return _lookup.TryGet(builder, out element);
    }

    /// <inheritdoc />
    public bool TryGet<TScoped>(Func<IScopeBuilder, Scope> builder, out TScoped element) where TScoped : LogixScoped
    {
        return _lookup.TryGet(builder, out element);
    }

    /// <inheritdoc />
    public IEnumerable<CrossReference> References(LogixComponent component)
    {
        return _lookup.References(component);
    }

    /// <inheritdoc />
    public IEnumerable<Scope> Scopes()
    {
        return _lookup.Scopes();
    }

    /// <summary>
    /// Adds the provided <see cref="LogixComponent"/> to the first found container within the L5X file. 
    /// </summary>
    /// <param name="component">The component to add to the L5X.</param>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file, and since most components have a single top level
    /// container, it will work for most types. However, note that this only adds to the first container found of the specific type.
    /// If you are adding scoped components such as, <see cref="Tag"/> or <see cref="Routine"/> you should be doing so in the context
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
    /// <param name="program">The container name (controller, program, or instruction) in which to add the component.</param>
    /// <exception cref="InvalidOperationException">No container was found in the L5X for the specified type and container name.</exception>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file. This method will look for a container element
    /// within the specified container/scope name. Therefore, it is important to be sure you specify the correct type and
    /// container name. For example, if you try to add a Module to a Program, this will fail even if a program with the
    /// container name exists, because there is no Modules container in a Program component.
    /// </remarks>
    public void Add(LogixComponent component, string program)
    {
        var type = component.GetType().L5XContainer();

        var target = _content.Descendants(type).FirstOrDefault(e => ScopeLevel.Container(e) == program);
        if (target is null)
            throw new InvalidOperationException($"Not container with name '{program}' was found in the L5X.");

        target.Add(component.Serialize());
    }

    /// <summary>
    /// Removes the element returned by the provided <see cref="IScopeBuilder"/> function.
    /// </summary>
    /// <param name="builder">A function that takes an <see cref="IScopeBuilder"/> and returns an <see cref="Scope"/> element to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="builder"/> function is null.</exception>
    public void Remove(Func<IScopeBuilder, Scope> builder)
    {
        if (builder is null) throw new ArgumentNullException(nameof(builder));
        var element = _lookup.Get(builder);
        element.Remove();
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

        var scope = Scope.Build(Controller.Name).Type(typeof(TComponent).L5XType()).Named(name);
        var element = _lookup.Get(scope);
        element.Remove();
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
    /// Gets a top-level container element from the root controller element of the L5X.
    /// We expecte
    /// </summary>
    private XElement GetContainer(string name)
    {
        return _content.Element(L5XName.Controller)?.Element(name) ?? throw _content.L5XError(name);
    }

    /// <summary>
    /// Gets all primary top level L5X component containers in the current L5X file.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="XElement"/> representing the L5X component containers.</returns>
    private List<XElement> GetContainers()
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
    /// If no root controller element exists, adds a new context controller and moves all root elements into that controller
    /// element. Then adds missing top level containers to ensure a consistent structure of the root L5X.
    /// </summary>
    private void NormalizeContent()
    {
        //If no controller element exists, we will insert on with the existing content as the child.
        if (_content.Element(L5XName.Controller) is null)
        {
            var context = new XElement(L5XName.Controller, new XAttribute(L5XName.Use, Use.Context));
            context.Add(_content.Elements());
            _content.ReplaceAll(context);
        }

        //This should now always exist.
        var controller = _content.Element(L5XName.Controller)!;

        //Inject remaining container elements that do not exist to ensure our container collections return without error.
        foreach (var container in Containers)
        {
            var existing = controller.Element(container);
            if (existing is not null) continue;
            controller.Add(new XElement(container));
        }
    }

    /// <summary>
    /// Create a document, adds default declaration, and saves the current L5X content to the specified file name.
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

/// <summary>
/// Enum representing options when loading or parsing an <see cref="L5X"/> instnace.
/// </summary>
public enum L5XOptions
{
    /// <summary>
    /// No additional options for loading or parsing the L5X file are enabled.
    /// </summary>
    /// <remarks>
    /// This means that the L5X file will be loaded or parsed with default behaviour.
    /// Any call to one of the <see cref="ILogixLookup"/> methods will use XPath lookup for elements.
    /// If you need fast lookups, consider selecting <see cref="Index"/> to have the content indexed upon load.
    /// </remarks>
    None,

    /// <summary>
    /// This option enables indexing of the L5X file, allowing for fast lookups using the <see cref="ILogixLookup"/> API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This will slightly increase the load time, but it significantly accelerates the performance
    /// of lookup operations using any <see cref="ILogixLookup"/> API. 
    /// This option is advantageous when the user plans to execute numerous lookups of elements.
    /// </para>
    /// <para>
    /// Any mutation of scoped elements will not be reflected in the index as it does not track changes.
    /// The <c>Index</c> option is primarily intended for read-only interaction on the file. If you make changes and need
    /// to then perform fast lookups, you can load or parse a new instance of an L5X.
    /// </para> 
    /// </remarks>
    Index
}