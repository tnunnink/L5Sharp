// ReSharper disable RedundantUsingDirective

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace L5Sharp.Core;

/// <summary>
/// This is the primary entry point for interacting with the L5X file.
/// Provides access to query and manipulate logix components, elements, containers, and more. 
/// </summary>
/// <remarks>
/// </remarks>
[L5XType(L5XName.RSLogix5000Content)]
public sealed class L5X : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="L5X"/> from the provided <see cref="LogixInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="LogixInfo"/> object that represents the L5X content.</param>
    public L5X(LogixInfo info) : base(info.Serialize())
    {
        Info = info;
        Controller = new Controller(Element.Element(L5XName.Controller)!);

        //This stores the L5X object as an in-memory object for the root XElement,
        //allowing child elements to retrieve the object locally without creating a new instance and potentially
        //reindexing of the XML content if requested.
        //This allows them to reference to root L5X for cross-referencing or other lookup-based operations.
        Element.AddAnnotation(this);
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
    /// Creates a new <see cref="L5X"/> by loading the contents of the provide file name.
    /// </summary>
    /// <param name="fileName">A URI string referencing the file to load.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified file.</returns>
    /// <exception cref="ArgumentException">The string is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="XElement.Load(string)"/> to load the contents of the XML file into
    /// memory. This means that this method is subject to the same exceptions that could be generated by loading the
    /// XElement, such as XML format exceptions
    /// </remarks>
    public static L5X Load(string fileName)
    {
        var element = XElement.Load(fileName);
        var info = new LogixInfo(element);
        return new L5X(info);
    }

    /// <summary>
    /// Asynchronously loads an L5X file from the specified file name and returns an instance of <see cref="L5X"/>.
    /// </summary>
    /// <param name="fileName">The file name of the L5X file to load.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, with a result of type <see cref="L5X"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided file name is null or empty.</exception>
    public static async Task<L5X> LoadAsync(string fileName, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("File name can not be null or empty.", nameof(fileName));

        token.ThrowIfCancellationRequested();
        using var reader = new StreamReader(fileName);
        var xml = await reader.ReadToEndAsync();
        token.ThrowIfCancellationRequested();

        var element = XElement.Parse(xml);
        return new L5X(new LogixInfo(element));
    }

    /// <summary>
    /// Creates a new <see cref="L5X"/> with the provided L5X string content.
    /// </summary>
    /// <param name="xml">The string that contains the L5X content to parse.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified string.</returns>
    /// <exception cref="ArgumentException">The string is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="XElement.Parse(string)"/> to load the contents of the XML file into
    /// memory. This means that this method is subject to the same exceptions that could be generated by parsing the
    /// XElement.
    /// </remarks>
    public static L5X Parse(string xml)
    {
        var element = XElement.Parse(xml);
        var info = new LogixInfo(element);
        return new L5X(info);
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
    /// generic counterpart.
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
    /// generic counterpart.
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

    /// <summary>
    /// Determines whether the specified <see cref="Reference"/> exists in the current L5X content.
    /// </summary>
    /// <param name="reference">The <see cref="Reference"/> object representing the element to check for existence.</param>
    /// <returns><c>true</c> if the specified <see cref="Reference"/> exists; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="reference"/> is <c>null</c>.</exception>
    public bool Contains(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        var element = Element.XPathSelectElement(reference);
        return element is not null;
    }

    /// <summary>
    /// Determines whether the current <see cref="L5X"/> contains an element specified by the provided builder action.
    /// </summary>
    /// <param name="action">An <see cref="Action{T}"/> of <see cref="IReferenceTypeBuilder"/> that defines the element to locate.</param>
    /// <returns>True if the specified element is found within the current <see cref="L5X"/>; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="action"/> is null.</exception>
    public bool Contains(Action<IReferenceTypeBuilder> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);
        var element = Element.XPathSelectElement(reference);
        return element is not null;
    }

    /// <summary>
    /// Retrieves a <see cref="LogixEntity"/> from the L5X content using the specified <see cref="Reference"/>.
    /// </summary>
    /// <param name="reference">The <see cref="Reference"/> used to locate the specific entity within the L5X content.</param>
    /// <returns>The <see cref="LogixEntity"/> that corresponds to the specified <see cref="Reference"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="reference"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when no element matching the provided <paramref name="reference"/> is found.</exception>
    public LogixEntity Get(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        var element = Element.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {reference}");

        var result = element.Deserialize<LogixEntity>();
        return result is Tag tag ? tag[reference.Location.ToTagName().Path] : result;
    }

    /// <summary>
    /// Retrieves a <see cref="LogixComponent"/> of the specified type based on the provided name and optional program context.
    /// </summary>
    /// <typeparam name="TComponent">The type of the <see cref="LogixComponent"/> to retrieve.</typeparam>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="program">The optional name of the program that contains the component.</param>
    /// <returns>The retrieved <see cref="LogixComponent"/> of the specified type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no component matching the reference is found.</exception>
    public TComponent Get<TComponent>(string name, string? program = null) where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name, program);

        var element = Element.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = element.Deserialize<TComponent>();
        return result is Tag tag ? (TComponent)(LogixObject)tag[reference.Location.ToTagName().Path] : result;
    }

    /// <summary>
    /// Retrieves an instance of <typeparamref name="TCode"/> from the specified location within the project's structure.
    /// </summary>
    /// <param name="number">The unique numeric identifier associated with the target code element.</param>
    /// <param name="program">The name of the program containing the desired code element.</param>
    /// <param name="routine">The name of the routine within the program where the code element resides.</param>
    /// <typeparam name="TCode">The type of the code element to retrieve, which must inherit from <see cref="LogixCode"/>.</typeparam>
    /// <returns>An instance of <typeparamref name="TCode"/> corresponding to the specified parameters.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no element is found for the given reference parameters.</exception>
    public TCode Get<TCode>(int number, string program, string routine) where TCode : LogixCode
    {
        var reference = Reference.To<TCode>(number, program, routine);

        var element = Element.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        return element.Deserialize<TCode>();
    }

    /// <summary>
    /// Retrieves a <see cref="LogixEntity"/> based on the provided reference builder action.
    /// </summary>
    /// <param name="action">The action that configures the <see cref="IReferenceTypeBuilder"/> to specify the desired reference path.</param>
    /// <returns>The <see cref="LogixEntity"/> that corresponds to the specified reference.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="action"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no matching element is found for the specified reference.</exception>
    public LogixEntity Get(Action<IReferenceTypeBuilder> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);
        var element = Element.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = element.Deserialize<LogixEntity>();
        return result is Tag tag ? tag[reference.Location.ToTagName().Path] : result;
    }

    /// <summary>
    /// Retrieves an instance of the specified <typeparamref name="TEntity"/> type using the provided reference location builder action.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to retrieve, which must inherit from <see cref="LogixEntity"/>.</typeparam>
    /// <param name="action">An <see cref="Action{T}"/> that sets up the reference location for the desired entity via the <see cref="IReferenceLocationBuilder"/>.</param>
    /// <returns>An instance of <typeparamref name="TEntity"/> representing the retrieved entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="action"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when no element matching the specified reference is found.</exception>
    public TEntity Get<TEntity>(Action<IReferenceLocationBuilder> action) where TEntity : LogixEntity
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var builder = new ReferenceBuilder();
        builder.Type(ReferenceType.Parse(typeof(TEntity).Name));
        action.Invoke(builder);
        var reference = builder.Build();

        var element = Element.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = element.Deserialize<TEntity>();
        return result is Tag tag ? (TEntity)(LogixObject)tag[reference.Location.ToTagName().Path] : result;
    }

    /// <summary>
    /// Attempts to retrieve a <see cref="LogixEntity"/> based on the given <see cref="Reference"/>.
    /// </summary>
    /// <param name="reference">The <see cref="Reference"/> that specifies the location of the entity within the L5X structure.</param>
    /// <param name="entity">When this method returns, contains the <see cref="LogixEntity"/> if found; otherwise, null.</param>
    /// <returns>True if the entity is successfully retrieved; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="reference"/> is null.</exception>
    public bool TryGet(Reference reference, out LogixEntity entity)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        var result = Element.XPathSelectElement(reference)?.Deserialize<LogixEntity>();

        if (result is null)
        {
            entity = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path) : result;
        return target.IsNull(out entity);
    }

    /// <summary>
    /// Attempts to retrieve a component of the specified type with the given name.
    /// </summary>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="component">
    /// When this method returns, contains the component of the specified type if the retrieval was successful; otherwise,
    /// contains the default value for the component type.
    /// </param>
    /// <typeparam name="TComponent">The type of the component to retrieve, inheriting from <see cref="LogixComponent"/>.</typeparam>
    /// <returns>
    /// <c>true</c> if the component was found and successfully retrieved; otherwise, <c>false</c>.
    /// </returns>
    public bool TryGet<TComponent>(string name, out TComponent component) where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name);

        var result = Element.XPathSelectElement(reference)?.Deserialize<TComponent>();

        if (result is null)
        {
            component = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixEntity>() : result;
        return (target as TComponent).IsNull(out component);
    }

    /// <summary>
    /// Attempts to retrieve a <typeparamref name="TComponent"/> by the given name and optional program context.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="LogixComponent"/> to retrieve.</typeparam>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="program">The optional program context in which the component resides.</param>
    /// <param name="component">The output parameter that will contain the retrieved component if the operation succeeds.</param>
    /// <returns>True if the component is successfully retrieved; otherwise, false.</returns>
    public bool TryGet<TComponent>(string name, string program, out TComponent component)
        where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name, program);

        var result = Element.XPathSelectElement(reference)?.Deserialize<TComponent>();

        if (result is null)
        {
            component = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixEntity>() : result;
        return (target as TComponent).IsNull(out component);
    }

    /// <summary>
    /// Attempts to retrieve a <typeparamref name="TCode"/> from the L5X content using the specified parameters.
    /// </summary>
    /// <typeparam name="TCode">The type of <see cref="LogixCode"/> to retrieve.</typeparam>
    /// <param name="number">The identifying number related to the code reference.</param>
    /// <param name="program">The name of the program containing the desired code.</param>
    /// <param name="routine">The name of the routine containing the desired code.</param>
    /// <param name="code">When this method returns, contains the retrieved <typeparamref name="TCode"/> if the operation is successful, or null if unsuccessful.</param>
    /// <returns><c>true</c> if the <typeparamref name="TCode"/> is retrieved; otherwise, <c>false</c>.</returns>
    public bool TryGet<TCode>(int number, string program, string routine, out TCode code) where TCode : LogixCode
    {
        var reference = Reference.To<TCode>(number, program, routine);

        var result = Element.XPathSelectElement(reference)?.Deserialize<TCode>();

        if (result is null)
        {
            code = null!;
            return false;
        }

        return result.IsNull(out code);
    }

    /// <summary>
    /// Attempts to retrieve a <see cref="LogixEntity"/> based on the provided reference action.
    /// </summary>
    /// <param name="action">An <see cref="Action{T}"/> that defines the reference to locate the entity.</param>
    /// <param name="entity">When this method returns, contains the <see cref="LogixEntity"/> if found; otherwise, null.</param>
    /// <returns><c>true</c> if the entity was found; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="action"/> is null.</exception>
    public bool TryGet(Action<IReferenceTypeBuilder> action, out LogixEntity entity)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);
        var element = Element.XPathSelectElement(reference);
        var result = element?.Deserialize<LogixEntity>();

        if (result is null)
        {
            entity = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path) : result;
        return target.IsNull(out entity);
    }

    /// <summary>
    /// Attempts to retrieve an element of the specified type <typeparamref name="TEntity"/> from the L5X structure based on the provided reference location builder action.
    /// </summary>
    /// <typeparam name="TEntity">The type of element to retrieve, which must derive from <see cref="LogixEntity"/>.</typeparam>
    /// <param name="action">An action that defines the reference location using the <see cref="IReferenceLocationBuilder"/>.</param>
    /// <param name="entity">When this method returns, contains the retrieved entity if successful; otherwise, null if no matching entity is found.</param>
    /// <returns>True if the entity was successfully retrieved; otherwise, false if no matching entity exists.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="action"/> parameter is null.</exception>
    public bool TryGet<TEntity>(Action<IReferenceLocationBuilder> action, out TEntity entity)
        where TEntity : LogixEntity
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var builder = new ReferenceBuilder();
        builder.Type(ReferenceType.Parse(typeof(TEntity).Name));
        action.Invoke(builder);
        var reference = builder.Build();

        var element = Element.XPathSelectElement(reference);
        var result = element?.Deserialize<LogixEntity>();

        if (result is null)
        {
            entity = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixEntity>() : result;
        return (target as TEntity).IsNull(out entity);
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

        var element = Get(action);

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
        var element = Get(reference);
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

    /// <inheritdoc />
    public override string ToString() => Info.ToString();
}