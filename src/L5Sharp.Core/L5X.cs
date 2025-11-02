// ReSharper disable RedundantUsingDirective

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
            _lookup = new LogixIndex(Element);
        }
        else
        {
            //LogixLookup uses XPath with is not terrible but will be slow for many lookups (mostly for tag elements).
            _lookup = new LogixLookup(Element);
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
    /// Creates a new <see cref="L5X"/> file with a seeded controller and processor module element using the provided
    /// controller name, processor type, and revision. 
    /// </summary>
    /// <param name="name">The name of the controller.</param>
    /// <param name="processor">The processor type of the controller.</param>
    /// <param name="revision">The optional software revision of the processor.
    /// If not provided, then the maximum installed revision is used.</param>
    /// <returns>A new <see cref="L5X"/> instance with the specified controller properties.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="processor"/> is null or empty.</exception>
    /// <exception cref="InvalidOperationException">No module info is defined for the provided processor type.</exception>
    public static L5X New(string name, string processor, Revision? revision = null)
    {
        var info = LogixInfo.Create(name, processor, revision);
        return new L5X(info);
    }

    /// <summary>
    /// Creates a new, empty <see cref="L5X"/> instance with a default <see cref="LogixInfo"/> configuration.
    /// </summary>
    /// <returns>An empty <see cref="L5X"/> instance with no additional content.</returns>
    public static L5X Empty() => new(LogixInfo.Empty());

    /// <summary>
    /// Retrieves a collection of <see cref="LogixComponent"/> objects based on the provided filter predicate.
    /// </summary>
    /// <param name="predicate">A function to filter the <see cref="LogixComponent"/> objects.
    /// If null, all components are returned.</param>
    /// <returns>An enumerable collection of <see cref="LogixComponent"/> objects that satisfy the specified predicate.</returns>
    public IEnumerable<LogixComponent> Components(Func<LogixComponent, bool>? predicate = null)
    {
        return Element.Descendants()
            .Where(x => x.IsComponentElement() || x.IsModuleTagElement())
            .Select(e => e.Deserialize<LogixComponent>())
            .Where(c => predicate is null || predicate.Invoke(c));
    }

    /// <summary>
    /// Retrieves a collection of <see cref="LogixCode"/> elements that match the provided predicate.
    /// </summary>
    /// <param name="predicate">A function to filter the <see cref="LogixCode"/> elements.
    /// If null, all <see cref="LogixCode"/> elements are returned.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the <see cref="LogixCode"/> elements that satisfy the predicate condition.</returns>
    public IEnumerable<LogixCode> Code(Func<LogixCode, bool>? predicate = null)
    {
        return Element.Descendants()
            .Where(x => x.IsCodeElement())
            .Select(e => e.Deserialize<LogixCode>())
            .Where(c => predicate is null || predicate.Invoke(c));
    }

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
    /// explore the methods defined by the <see cref="ILogixLookup"/> API.
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
    /// explore the methods defined by the <see cref="ILogixLookup"/> API.
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

        var types = new HashSet<string>(type.GetLogixTypeNames());

        return Element.Descendants()
            .Where(e => types.Contains(e.Name.LocalName))
            .Select(e => e.Deserialize());
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
    /// </remarks>
    public IEnumerable<TObject> Query<TObject>() where TObject : LogixObject
    {
        var types = new HashSet<string>(typeof(TObject).GetLogixTypeNames());

        return Element.Descendants()
            .Where(e => types.Contains(e.Name.LocalName))
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
    /// </remarks>
    public IEnumerable<TObject> Query<TObject>(Func<TObject, bool> predicate) where TObject : LogixObject
    {
        var types = new HashSet<string>(typeof(TObject).GetLogixTypeNames());

        return Element.Descendants()
            .Where(e => types.Contains(e.Name.LocalName))
            .Select(e => e.Deserialize<TObject>())
            .Where(predicate);
    }

    /// <inheritdoc />
    public bool Contains(Reference reference)
    {
        return _lookup.Contains(reference);
    }

    /// <inheritdoc />
    public bool Contains(Action<IReferenceTypeBuilder> action)
    {
        return _lookup.Contains(action);
    }

    /// <inheritdoc />
    public LogixEntity Get(Reference reference)
    {
        return _lookup.Get(reference);
    }

    /// <inheritdoc />
    public TComponent Get<TComponent>(string name, string? program = null) where TComponent : LogixComponent
    {
        return _lookup.Get<TComponent>(name, program);
    }

    /// <inheritdoc />
    public TCode Get<TCode>(int number, string program, string routine) where TCode : LogixCode
    {
        return _lookup.Get<TCode>(number, program, routine);
    }

    /// <inheritdoc />
    public LogixEntity Get(Action<IReferenceTypeBuilder> action)
    {
        return _lookup.Get(action);
    }

    /// <inheritdoc />
    public TEntity Get<TEntity>(Action<IReferenceLocationBuilder> action) where TEntity : LogixEntity
    {
        return _lookup.Get<TEntity>(action);
    }

    /// <inheritdoc />
    public bool TryGet(Reference reference, out LogixEntity entity)
    {
        return _lookup.TryGet(reference, out entity);
    }

    /// <inheritdoc />
    public bool TryGet<TComponent>(string name, out TComponent component) where TComponent : LogixComponent
    {
        return _lookup.TryGet(name, out component);
    }

    /// <inheritdoc />
    public bool TryGet<TComponent>(string name, string program, out TComponent component)
        where TComponent : LogixComponent
    {
        return _lookup.TryGet(name, out component);
    }

    /// <inheritdoc />
    public bool TryGet<TCode>(int number, string program, string routine, out TCode code) where TCode : LogixCode
    {
        return _lookup.TryGet(number, program, routine, out code);
    }

    /// <inheritdoc />
    public bool TryGet(Action<IReferenceTypeBuilder> action, out LogixEntity entity)
    {
        return _lookup.TryGet(action, out entity);
    }

    /// <inheritdoc />
    public bool TryGet<TEntity>(Action<IReferenceLocationBuilder> action, out TEntity element)
        where TEntity : LogixEntity
    {
        return _lookup.TryGet(action, out element);
    }

    /// <summary>
    /// Adds the provided <see cref="LogixComponent"/> to the first found container within the L5X file. 
    /// </summary>
    /// <param name="component">The component to add to the L5X.</param>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file, and since most components have a single top-level
    /// container, it will work most of the time. If the provided component has a defined scope, then this method will
    /// attempt to find the corresponding container to add this component to the correct scope. If this component is not
    /// scoped, then we will build a simple global relative scope using the type and name. If you want to be explicit about
    /// where this component should be added, then use the <seealso cref="Add(L5Sharp.Core.LogixComponent, string)"/> overload.
    /// </remarks>
    public void Add(LogixComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var container = Element
            .Descendants(component.GetType().GetLogixContainerName())
            .FirstOrDefault(e => Scope.Of(e).IsLocalTo(component.Reference));

        if (container is null)
            throw new InvalidOperationException($"Could not find container type '{component.GetElementType()}'");

        container.Add(component.Serialize());
    }

    /// <summary>
    /// Adds the provided <see cref="LogixComponent"/> to the specified container within the L5X file. 
    /// </summary>
    /// <param name="component">The component to add to the L5X.</param>
    /// <param name="programName">The program name in which to add the component.</param>
    /// <exception cref="InvalidOperationException">No container was found in the L5X for the specified type and container name.</exception>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file. This method will look for a container element
    /// within the specified container/scope name.
    /// </remarks>
    public void Add(LogixComponent component, string programName)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var container = Element
            .Descendants(component.GetType().GetLogixContainerName())
            .FirstOrDefault(e => Scope.Of(e).Container == programName);

        if (container is null)
            throw new InvalidOperationException(
                $"Could not find container type '{component.GetElementType()}' in scope: '{programName}'");

        container.Add(component.Serialize());
    }

    /// <summary>
    /// Removes a specific element identified by the provided <see cref="IReferenceTypeBuilder"/> action.
    /// </summary>
    /// <param name="action">An action to configure the <see cref="IReferenceTypeBuilder"/> for identifying the element to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="action"/> is null.</exception>
    public void Remove(Action<IReferenceTypeBuilder> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var element = _lookup.Get(action);

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

        var reference = Reference.To<TComponent>(name);
        var element = _lookup.Get(reference);
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

    /// <summary>
    /// Imports components into the current <see cref="L5X"/> instance based on the provided configuration action.
    /// </summary>
    /// <param name="action">An <see cref="Action{T}"/> delegate that configures the import using import builder API.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> parameter is null.</exception>
    public void Import(Action<IImportSourceBuilder> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));
        var builder = new ImportBuilder();
        action.Invoke(builder);
        var import = builder.Build();
        import.Execute(this);
    }

    /// <summary>
    /// Creates a new <see cref="ILogixLookup"/> instance for indexing the L5X content.
    /// </summary>
    /// <returns>An <see cref="ILogixLookup"/> instance representing the indexed L5X content.</returns>
    public ILogixLookup Index()
    {
        return new LogixIndex(Element);
    }

    /// <summary>
    /// Creates a new <see cref="ILogixLookup"/> for the specified program within the L5X content.
    /// </summary>
    /// <param name="program">The name of the program to create a lookup for.</param>
    /// <returns>An instance of <see cref="ILogixLookup"/> representing the specified program.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no program with the specified name exists.</exception>
    public ILogixLookup Index(string program)
    {
        var element = Element.Descendants(L5XName.Program).FirstOrDefault(p => p.LogixName() == program);

        if (element is null)
            throw new InvalidOperationException($"No program with name '{program}' found.");

        return new LogixIndex(element);
    }

    /// <inheritdoc />
    public override string ToString() => Info.ToString();
}