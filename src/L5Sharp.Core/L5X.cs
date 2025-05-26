using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// This is the primary entry point for interacting with the L5X file.
/// Provides access to query and manipulate logix components, elements, containers, and more. 
/// </summary>
/// <remarks>
/// </remarks>
[L5XType(L5XName.RSLogix5000Content)]
public sealed class L5X : LogixElement, ILogixLookup
{
    /// <summary>
    /// The <see cref="ILogixLookup"/> implementation used to quickly find objects in the file.
    /// The actual implementation that is instantiated is determined by the <see cref="L5XOptions"/> provided to the constructor.
    /// </summary>
    private readonly ILogixLookup _lookup;

    /// <summary>
    /// Creates a new <see cref="L5X"/> from the provided <see cref="LogixInfo"/> and optional <see cref="L5XOptions"/> argument.
    /// </summary>
    /// <param name="info">The <see cref="LogixInfo"/> object that represents the L5X content.</param>
    /// <param name="options">The <see cref="L5XOptions"/> that indicate how to load the content.</param>
    public L5X(LogixInfo info, L5XOptions options = L5XOptions.None) : base(info.Serialize())
    {
        Info = info;
        Controller = new Controller(Element.Element(L5XName.Controller)!);

        //This stores the L5X object as an in-memory object for the root XElement,
        //allowing child elements to retrieve the object locally without creating a new instance and potentially
        //reindexing of the XML content if requested.
        //This allows them to reference to root L5X for cross-referencing or other lookup-based operations.
        Element.AddAnnotation(this);

        //Depending on the options provided, either create a logix index or logix lookup for the ILogixLoop API usage.
        if (options == L5XOptions.Index)
        {
            //LogixIndex uses dictionaries and is super performant.
            _lookup = new LogixIndex(Element.Element(L5XName.Controller)!);
        }
        else
        {
            //LogixLookup uses XPath with is not terrible but will be slow for many lookups (mostly for tag elements).
            _lookup = new LogixLookup(Element.Element(L5XName.Controller)!);
        }
    }

    /// <summary>
    /// The <see cref="LogixInfo"/> instance representing the metadata and descriptive information of the L5X object.
    /// This property provides essential context and details about the RSLogix5000 content, such as target name, type,
    /// and other key attributes.
    /// </summary>
    public LogixInfo Info { get; }

    /// <summary>
    /// Represents the primary controller within a Logix project.
    /// This serves as the root element for components such as tags, programs, tasks, and other key project elements.
    /// </summary>
    public Controller Controller { get; }

    /// <summary>
    /// The container collection of <see cref="DataType"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="DataType"/> components.</value>
    public LogixContainer<DataType> DataTypes => Controller.DataTypes;

    /// <summary>
    /// Gets the collection of <see cref="AddOnInstruction"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="AddOnInstruction"/> components.</value>
    public LogixContainer<AddOnInstruction> Instructions => Controller.Instructions;

    /// <summary>
    /// Gets the collection of <see cref="Module"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Module"/> components.</value>
    public LogixContainer<Module> Modules => Controller.Modules;

    /// <summary>
    /// Gets the collection of Controller <see cref="Tags"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Tags"/> components.</value>
    /// <remarks>To access program-specific tag collection user the <see cref="Programs"/> collection.</remarks>
    public LogixContainer<Tag> Tags => Controller.Tags;

    /// <summary>
    /// Gets the collection of <see cref="Program"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Program"/> components.</value>
    public LogixContainer<Program> Programs => Controller.Programs;

    /// <summary>
    /// Gets the collection of <see cref="Task"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Task"/> components.</value>
    public LogixContainer<Task> Tasks => Controller.Tasks;

    /// <summary>
    /// The container collection of <see cref="ParameterConnection"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="ParameterConnection"/> components.</value>
    public LogixContainer<ParameterConnection> ParameterConnections => Controller.ParameterConnections;

    /// <summary>
    /// The container collection of <see cref="Trend"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="Trend"/> components.</value>
    public LogixContainer<Trend> Trends => Controller.Trends;

    /// <summary>
    /// The container collection of <see cref="WatchList"/> components found in the L5X file.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TComponent}"/> of <see cref="WatchList"/> components.</value>
    public LogixContainer<WatchList> WatchLists => Controller.WatchLists;

    /// <summary>
    /// Indicates whether the current L5X object is using an indexed structure for lookup operations.
    /// </summary>
    public bool IsIndexed => _lookup is LogixIndex;

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
        var element = XElement.Load(fileName);
        var info = new LogixInfo(element);
        return new L5X(info, options);
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
        var info = new LogixInfo(element);
        var file = new L5X(info, options);
        return file;
    }
#endif

    /// <summary>
    /// Creates a new <see cref="L5X"/> with the provided L5X string content.
    /// </summary>
    /// <param name="xml">The string that contains the L5X content to parse.</param>
    /// <param name="options">The <see cref="L5XOptions"/> that control how the L5X is initialized.
    /// The default is none, meaning no extra initialization steps are performed.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified string.</returns>
    /// <exception cref="ArgumentException">The string is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="XElement.Parse(string)"/> to load the contents of the XML file into
    /// memory. This means that this method is subject to the same exceptions that could be generated by parsing the
    /// XElement.
    /// </remarks>
    public static L5X Parse(string xml, L5XOptions options = L5XOptions.None)
    {
        var element = XElement.Parse(xml);
        var info = new LogixInfo(element);
        return new L5X(info, options);
    }

    /// <summary>
    /// Creates a new, empty <see cref="L5X"/> instance with a default <see cref="LogixInfo"/> configuration.
    /// </summary>
    /// <returns>An empty <see cref="L5X"/> instance with no additional content.</returns>
    public static L5X Empty() => new(LogixInfo.Empty());

    /// <summary>
    /// Finds an element across the entire L5X with the provided type as a flat collection of objects. 
    /// </summary>
    /// <param name="type">The type name or element name to retrieve.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found objects with the provided type name.</returns>
    /// <exception cref="ArgumentException"><c>type</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. This method allows
    /// specifying the type at runtime as opposed to the generic type but sacrifices the strong type querying of the
    /// generic counterpart. This method does not make use of any optimized searching. If you want efficient lookup,
    /// explore the methods defined by the <see cref="ILogixLookup"/> API such as <see cref="Find"/>.
    /// </para>
    /// <para>
    /// Also note that this will call <c>L5XType</c> extension internally which returns all configured
    /// element names for the provided type. This means the query will return all elements that the type supports.
    /// This is in contrast to something list <see cref="Tags"/>, which just returns controller scoped tags.
    /// </para>
    /// </remarks>
    public IEnumerable<LogixElement> Query(string type)
    {
        if (string.IsNullOrEmpty(type))
            throw new ArgumentNullException(nameof(type), "Type is required to retrieve elements from the L5X");

        return Element.Descendants(type).Select(e => e.Deserialize());
    }

    /// <summary>
    /// Finds an element across the entire L5X with the provided type as a flat collection of objects. 
    /// </summary>
    /// <param name="type">The type of the element type to retrieve.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found objects with the provided type name.</returns>
    /// <exception cref="ArgumentException"><c>type</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. This method allows
    /// specifying the type at runtime as opposed to the generic type but sacrifices the strong type querying of the
    /// generic counterpart. This method does not make use of any optimized searching. If you want efficient lookup,
    /// explore the methods defined by the <see cref="ILogixLookup"/> API such as <see cref="Find"/>.
    /// </para>
    /// <para>
    /// Also note that this will call <c>L5XType</c> extension internally which returns all configured
    /// element names for the provided type. This means the query will return all elements that the type supports.
    /// This is in contrast to something list <see cref="Tags"/>, which just returns controller scoped tags.
    /// </para>
    /// </remarks>
    public IEnumerable<LogixElement> Query(Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type), "Type is required to retrieve elements from the L5X");

        var types = new HashSet<string>(type.L5XTypes());

        foreach (var descendant in Element.Descendants())
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

        return Element.Descendants()
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

        return Element.Descendants()
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
    /// This provides a more dynamic way to add content to an L5X file, and since most components have a single top-level
    /// container, it will work for most types. However, note that this only adds to the first container found of the specific type.
    /// If you are adding scoped components such as, <see cref="Tag"/> or <see cref="Routine"/> you should be doing so in the context
    /// of a specific <see cref="Program"/> component, or use the overload accepting a specific container.
    /// </remarks>
    /// <seealso cref="Add(LogixComponent,string)"/>
    public void Add(LogixComponent component)
    {
        var container = component.GetType().L5XContainer();

        var target = Element.Descendants(container).FirstOrDefault();

        if (target is null)
            throw new InvalidOperationException($"Could not find container for type {component.GetType()}");

        target.Add(component.Serialize());
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
        var container = component.GetType().L5XContainer();

        var target = Element.Descendants(container).FirstOrDefault(e => ScopeLevel.Container(e) == program);

        if (target is null)
            throw new InvalidOperationException($"Could not find container '{program}' for type {component.GetType()}");

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
    /// Serialize this <see cref="L5X"/> to a file, overwriting an existing file, if it exists.
    /// </summary>
    /// <param name="fileName">A string that contains the name of the file.</param>
    public void Save(string fileName)
    {
        var declaration = new XDeclaration("1.0", "UTF-8", "yes");
        var document = new XDocument(declaration);
        document.Add(Element);
        document.Save(fileName);
    }

    /// <inheritdoc />
    public override string ToString() => Info.ToString();
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
    /// This means that the L5X file will be loaded or parsed with the default behavior.
    /// Any call to one of the <see cref="ILogixLookup"/> methods will use XPath lookup for elements.
    /// If you need fast lookups, consider selecting <see cref="Index"/> to have the content indexed upon a load.
    /// </remarks>
    None,

    /// <summary>
    /// This option enables indexing of the L5X file, allowing for fast lookups using the <see cref="ILogixLookup"/> API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This will slightly increase the load time, but it significantly speeds up the performance
    /// of lookup operations using any <see cref="ILogixLookup"/> API. 
    /// This option is useful when the user plans to execute many element lookups.
    /// </para>
    /// <para>
    /// Any mutation of scoped elements will not be reflected in the index as it does not track changes.
    /// The <c>Index</c> option is primarily intended for read-only interaction on the file. If you make changes and need
    /// to then perform fast lookups, you can load or parse a new instance of an L5X.
    /// </para> 
    /// </remarks>
    Index
}