// ReSharper disable RedundantUsingDirective

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace L5Sharp.Core;

/// <summary>
/// This is the primary entry point for interacting with the L5X file.
/// Provides access to query and manipulate logix components, elements, containers, and more. 
/// </summary>
public sealed class L5X
{
    /// <summary>
    /// The root <see cref="XElement"/> instance representing the serialized structure of the L5X object.
    /// This property provides access to the underlying XML content of the RSLogix5000 project,
    /// enabling operations such as querying, serialization, and other element-based manipulations.
    /// </summary>
    private XElement Element => Content.Serialize();

    /// <summary>
    /// Internal index of L5X elements and references to components within the document for fast lookups.
    /// This class
    /// </summary>
    private readonly LogixIndex _index;

    /// <summary>
    /// Creates a new <see cref="L5X"/> from the provided <see cref="LogixContent"/>.
    /// </summary>
    /// <param name="content">The <see cref="LogixContent"/> object that represents the L5X content.</param>
    public L5X(LogixContent content)
    {
        Content = content;
        Controller = new Controller(Element.Element(L5XName.Controller)!);
        _index = new LogixIndex(Element);

        //This stores the L5X object as an in-memory object for the root XElement,
        //allowing child elements to retrieve the object locally without creating a new instance.
        //This allows them to reference to root L5X for cross-referencing or other lookup-based operations.
        Element.AddAnnotation(this);
    }

    /// <summary>
    /// The <see cref="LogixContent"/> instance representing the metadata and descriptive information of the L5X file.
    /// This property provides context and details about the RSLogix5000 content, such as target name, type,
    /// and other key attributes.
    /// </summary>
    public LogixContent Content { get; }

    /// <summary>
    /// Represents the controller instance within a Logix project.
    /// This serves as the root element for components such as tags, programs, tasks, and other key project elements.
    /// </summary>
    public Controller Controller { get; }

    /// <summary>
    /// A collection of <see cref="DataType"/> elements associated with the <see cref="Controller"/> of the L5X object.
    /// This property provides access to user-defined data types within the RSLogix5000 project, allowing for enumeration,
    /// manipulation, and querying of these custom-defined structures in a logix container.
    /// </summary>
    public LogixContainer<DataType> DataTypes => Controller.DataTypes;

    /// <summary>
    /// A collection of <see cref="AddOnInstruction"/> objects representing the user-defined Add-On Instructions (AOIs)
    /// within the RSLogix5000 project. This property provides access to the AOIs defined in the project's
    /// controller, enabling tasks such as enumeration, retrieval, and manipulation of Add-On Instruction definitions.
    /// </summary>
    public LogixContainer<AddOnInstruction> AddOnInstructions => Controller.AddOnInstructions;

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
    /// Creates a new <see cref="L5X"/> by loading the contents of the provided file name.
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
        var content = new LogixContent(element);
        return new L5X(content);
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
        var content = new LogixContent(element);
        return new L5X(content);
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
        var content = new LogixContent(element);
        return new L5X(content);
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
        var content = LogixContent.Create(name, processor, revision);
        return new L5X(content);
    }

    /// <summary>
    /// Creates a new, empty <see cref="L5X"/> instance with a default <see cref="LogixContent"/> configuration.
    /// </summary>
    /// <returns>An empty <see cref="L5X"/> instance with no additional content.</returns>
    public static L5X Empty() => new(LogixContent.Empty());

    /// <summary>
    /// Retrieves a collection of <see cref="ILogixComponent"/> objects from the L5X content, optionally filtered by a specified predicate.
    /// </summary>
    /// <param name="predicate">
    /// A function to test each <see cref="ILogixComponent"/> for a condition.
    /// If null, all components are returned.
    /// </param>
    /// <returns>
    /// A collection of <see cref="ILogixComponent"/> objects from the L5X content that match the specified predicate, if provided.
    /// </returns>
    public IEnumerable<ILogixComponent> Components(Func<ILogixComponent, bool>? predicate = null)
    {
        var results = Element.Descendants()
            .Where(x => x.IsComponentElement() || x.IsModuleTagElement())
            .Select(e => e.Deserialize<ILogixComponent>());

        return predicate is not null ? results.Where(predicate) : results;
    }

    /// <summary>
    /// Retrieves a collection of <see cref="ILogixCode"/> elements from the content, optionally filtered by a specified predicate.
    /// </summary>
    /// <param name="predicate">An optional function to filter the elements. The function takes an <see cref="ILogixCode"/> as input and returns a boolean indicating whether the item should be included.</param>
    /// <returns>A filtered collection of <see cref="ILogixCode"/> elements satisfying the given predicate, or the entire collection if no predicate is provided.</returns>
    public IEnumerable<ILogixCode> Code(Func<ILogixCode, bool>? predicate = null)
    {
        var results = Element.Descendants()
            .Where(x => x.IsCodeElement())
            .Select(e => e.Deserialize<ILogixCode>());

        return predicate is not null ? results.Where(predicate) : results;
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
    public IEnumerable<ILogixElement> Query(string type)
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
    public IEnumerable<ILogixElement> Query(Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type), "Type is required to retrieve elements from the L5X");

        var types = LogixSerializer.NamesFor(type);

        return Element.Descendants()
            .Where(e => types.Contains(e.Name.LocalName))
            .Select(e => e.Deserialize());
    }

    /// <summary>
    /// Finds elements of the specified type across the entire L5X as a flat collection of objects.
    /// </summary>
    /// <typeparam name="TEntity">The element type to find.</typeparam>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all found objects of the specified type.</returns>
    /// <remarks>
    /// <para>
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. Since
    /// it returns <see cref="IEnumerable{T}"/>, you can make use of LINQ and the strongly typed objects to build
    /// more complex queries.
    ///</para>
    /// </remarks>
    public IEnumerable<TEntity> Query<TEntity>() where TEntity : LogixEntity<TEntity>
    {
        var types = LogixSerializer.NamesFor(typeof(TEntity));

        return Element.Descendants()
            .Where(e => types.Contains(e.Name.LocalName))
            .Select(e => e.Deserialize<TEntity>());
    }

    /// <summary>
    /// Executes a query on the content of the L5X file, filtering elements based on the provided predicate.
    /// </summary>
    /// <param name="predicate">A function that defines the criteria for the elements to be included in the result.</param>
    /// <typeparam name="TEntity">The type of LogixElement to query for.</typeparam>
    /// <returns>An enumerable collection of elements of type TElement that satisfy the predicate.</returns>
    /// <remarks>
    /// <para>
    /// This method provides a flexible and simple way to query the entire L5X for a specific type. Since
    /// it returns <see cref="IEnumerable{T}"/>, you can make use of LINQ and the strongly typed objects to build
    /// more complex queries.
    ///</para>
    /// </remarks>
    public IEnumerable<TEntity> Query<TEntity>(Func<TEntity, bool> predicate) where TEntity : LogixEntity<TEntity>
    {
        var types = LogixSerializer.NamesFor(typeof(TEntity));

        return Element.Descendants()
            .Where(e => types.Contains(e.Name.LocalName))
            .Select(e => e.Deserialize<TEntity>())
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
        return _index.ContainsElement(reference);
    }

    /// <summary>
    /// Finds and returns a <see cref="ILogixEntity"/> object from the L5X content that matches
    /// the provided <see cref="Reference"/>.
    /// </summary>
    /// <param name="reference">A <see cref="Reference"/> object that identifies the entity to retrieve from the L5X content.</param>
    /// <returns>An <see cref="ILogixEntity"/> instance matching the specified <see cref="Reference"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reference"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no element with the specified <paramref name="reference"/> exists in the L5X content.</exception>
    public ILogixEntity Get(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        if (!reference.Type.IsTag)
            return _index.GetElement<ILogixEntity>(reference);

        // It's a tag, so handle potential member paths (e.g. "MyTag.Member[0]")
        var tagName = reference.Id.ToTagName();

        // Always search the index using the base name and the reference's explicit scope.
        var baseReference = Reference.To<Tag>(tagName.Base, reference.Scope);
        var tag = _index.GetElement<Tag>(baseReference);

        // Navigate to the specified member (this will return the base tag if no member is specified).
        return tag[tagName.Member];
    }

    /// <summary>
    /// Retrieves a component from the L5X content using the specified name.
    /// </summary>
    /// <typeparam name="TComponent">The type of the component to retrieve, which must inherit from <see cref="LogixComponent{TComponent}"/>.</typeparam>
    /// <param name="name">
    /// The name of the component to retrieve. For tags, supports dot-notation paths (e.g., "TagName.Member")
    /// and optional program scope prefix (e.g., "ProgramName.TagName").
    /// </param>
    /// <returns>The retrieved component instance of the specified type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no element with the specified name is found in the L5X content.</exception>
    /// <remarks>
    /// <para>
    /// This method uses the internal component index to efficiently locate components by their name.
    /// For tag components, the name parameter can include member paths and program scope information,
    /// which will be parsed automatically using <see cref="TagName"/> parsing.
    /// </para>
    /// <para>
    /// If the component is scoped to a program, include the program name as a prefix (e.g., "MyProgram.MyTag").
    /// Controller-scoped components can be retrieved using just the component name.
    /// </para>
    /// </remarks>
    public TComponent Get<TComponent>(string name) where TComponent : LogixComponent<TComponent>
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var tagName = name.ToTagName();
        var reference = Reference.To<TComponent>(tagName.Base, tagName.Scope);
        var element = _index.GetElement<TComponent>(reference);
        return element is Tag tag ? tag[tagName.Member].As<TComponent>() : element;
    }

    /// <summary>
    /// Retrieves an instance of the specified Logix code type from the element with the provided reference information.
    /// </summary>
    /// <typeparam name="TCode">The type of the Logix code object to retrieve.</typeparam>
    /// <param name="number">The numeric identifier of the Logix code object.</param>
    /// <param name="program">The name of the program containing the Logix code object.</param>
    /// <param name="routine">The name of the routine within the program where the Logix code object resides.</param>
    /// <returns>The deserialized instance of the specified <typeparamref name="TCode"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no element with the given reference is found.</exception>
    public TCode Get<TCode>(uint number, string program, string routine) where TCode : LogixCode<TCode>
    {
        var reference = Reference.To<TCode>(number, program, routine);
        return _index.GetElement<TCode>(reference);
    }

    /// <summary>
    /// Attempts to retrieve an <see cref="ILogixEntity"/> from the current <see cref="L5X"/> based on the specified <see cref="Reference"/>.
    /// </summary>
    /// <param name="reference">The <see cref="Reference"/> object that identifies the location of the desired entity.</param>
    /// <param name="entity">When this method returns, contains the retrieved <see cref="ILogixEntity"/> if the operation is successful; otherwise, contains null.</param>
    /// <returns><c>true</c> if the entity was successfully retrieved; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="reference"/> parameter is null.</exception>
    public bool TryGet(Reference reference, out ILogixEntity entity)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        if (!reference.Type.IsTag)
            return _index.TryGetElement(reference, out entity);

        // It's a tag, so handle potential member paths (e.g. "MyTag.Member[0]")
        var tagName = reference.Id.ToTagName();

        // Always search the index using the base name and the reference's explicit scope.
        if (_index.TryGetElement<Tag>(Reference.To<Tag>(tagName.Base, reference.Scope), out var tag))
        {
            var target = tag.Member(tagName.Member);
            return target.IsNull(out entity);
        }

        entity = null!;
        return false;
    }

    /// <summary>
    /// Attempts to retrieve a component of the specified type from the L5X instance by its name.
    /// </summary>
    /// <typeparam name="TComponent">The type of the component to retrieve.</typeparam>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="component">When this method returns, contains the component of the specified type if found,
    /// or null if the component does not exist.</param>
    /// <returns>True if the component is found; otherwise, false.</returns>
    public bool TryGet<TComponent>(string name, out TComponent component) where TComponent : LogixComponent<TComponent>
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var tagName = name.ToTagName();
        var reference = Reference.To<TComponent>(tagName.Base, tagName.Scope);

        if (_index.TryGetElement<TComponent>(reference, out var element))
        {
            var target = element is Tag tag ? tag.Member(tagName.Member)?.As<TComponent>() : element;
            return target.IsNull(out component);
        }

        component = null!;
        return false;
    }

    /// <summary>
    /// Attempts to retrieve a code element from the L5X content using the specified parameters.
    /// </summary>
    /// <typeparam name="TCode">The type of <see cref="ILogixCode"/> to retrieve.</typeparam>
    /// <param name="number">The identifying number related to the code reference.</param>
    /// <param name="program">The name of the program containing the desired code.</param>
    /// <param name="routine">The name of the routine containing the desired code.</param>
    /// <param name="code">When this method returns, contains the retrieved <typeparamref name="TCode"/> if the operation is successful, or null if unsuccessful.</param>
    /// <returns><c>true</c> if the <typeparamref name="TCode"/> is retrieved; otherwise, <c>false</c>.</returns>
    public bool TryGet<TCode>(uint number, string program, string routine, out TCode code)
        where TCode : LogixCode<TCode>
    {
        var reference = Reference.To<TCode>(number, program, routine);

        if (_index.TryGetElement(reference, out code))
        {
            return true;
        }

        code = null!;
        return false;
    }

    /// <summary>
    /// Retrieves a collection of <see cref="Reference"/> representing elements in the project that reference the
    /// provided string name.
    /// </summary>
    /// <param name="name">The name ot find references for in the project.</param>
    /// <returns>A collection of <see cref="Reference"/> objects that represent references to the name.</returns>
    /// <remarks>
    /// The name typically represents a logix component such as a <c>Tag</c>, <c>DataType</c>, <c>AOI</c>, etc.
    /// This method relies on internal indexing of the L5X project file to function. While this method is optimized for
    /// repeated lookups in read-only scenarios, frequent modifications to the underlying XML will trigger reindexing,
    /// which may impact performance.
    /// </remarks>
    public IEnumerable<Reference> References(string name)
    {
        return _index.FindReferences(name);
    }

    /// <summary>
    /// Adds the specified <see cref="ILogixComponent"/> to the current L5X content.
    /// </summary>
    /// <param name="component">The <see cref="ILogixComponent"/> to be added to the L5X content.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="component"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the L5X container corresponding to the <paramref name="component"/> could not be found for adding.
    /// </exception>
    /// <remarks>
    /// This provides a more dynamic way to add content to an L5X file, and since most components have a single top-level
    /// container, it will work most of the time. If the provided component has a defined scope, then this method will
    /// attempt to find the corresponding container to add this component to the correct scope. If this component is not
    /// scoped, then we will build a simple global relative scope using the type and name.
    /// </remarks>
    public void Add(ILogixComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var typeName = LogixSerializer.NamesFor(component.GetType()).First();
        var container = Element.Descendants($"{typeName}s").FirstOrDefault();

        if (container is null)
            throw new InvalidOperationException($"Could not find container for type: '{component.GetType()}'");

        container.Add(component.Serialize());
    }

    /// <summary>
    /// Adds a specified <see cref="ILogixComponent"/> to the container associated with the specified program name.
    /// </summary>
    /// <param name="component">The <see cref="ILogixComponent"/> to be added.</param>
    /// <param name="programName">The name of the program that contains the target container.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="component"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a container for the <paramref name="component"/> type cannot be found
    /// within the specified program scope.
    /// </exception>
    public void Add(ILogixComponent component, string programName)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var typeName = LogixSerializer.NamesFor(component.GetType()).First();
        var container = Element.Descendants($"{typeName}s").FirstOrDefault(e => Scope.Of(e).IsIn(programName));

        if (container is null)
            throw new InvalidOperationException(
                $"Could not find container for type {component.GetType()} in scope: '{programName}'");

        container.Add(component.Serialize());
    }

    /// <summary>
    /// Removes the specified <see cref="Reference"/> from the current L5X structure, if it exists.
    /// </summary>
    /// <param name="reference">The <see cref="Reference"/> object representing the element to remove.</param>
    /// <returns>True if the element was successfully removed; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="reference"/> is null.</exception>
    public bool Remove(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        if (_index.TryGetElement<ILogixEntity>(reference, out var entity))
        {
            entity.Serialize().Remove();
            return true;
        }

        return false;
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
    public bool Remove<TComponent>(string name) where TComponent : LogixComponent<TComponent>
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        var reference = Reference.To<TComponent>(name);

        if (_index.TryGetElement<TComponent>(reference, out var component))
        {
            component.Remove();
            return true;
        }

        return false;
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
    public override string ToString() => Element.ToString();
}