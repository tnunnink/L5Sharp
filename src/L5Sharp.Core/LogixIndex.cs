using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A class that encapsulates the code required to index the L5X for values that we can look up and find all existing
/// references to. This class is internal and is exposed through the API of the <see cref="L5X"/>
/// and <see cref="ILogixComponent"/> types.
/// </summary>
internal class LogixIndex
{
    /// <summary>
    /// Indicates whether the L5X content has been successfully indexed for element lookups and reference searches.
    /// </summary>
    private bool _isIndexed;

    /// <summary>
    /// The L5X content to index for lookup of elements and references.
    /// </summary>
    private readonly XElement _content;

    /// <summary>
    /// Internal cache of all referencable elements (components and code elements). This lets us find/lookup elements
    /// in constant time and not have to traverse the document. This is somewhat of a micro optimization, but since we are
    /// traversing the document to index references, we might as well index these elements.
    /// </summary>
    private readonly ConcurrentDictionary<Reference, XElement> _elements = [];

    /// <summary>
    /// Internal cache of all entity references. These are values/names found in the L5X project that refer to some
    /// component element. Each value maps to a list of associated references that identify where in the project
    /// the value is found/referenced. The reference object will contain addition information, including logic for
    /// tag/component references, so we can further inspect the context of the reference.
    /// </summary>
    private readonly ConcurrentDictionary<string, List<Reference>> _references = [];

    /// <summary>
    /// Creates a new <see cref="LogixIndex"/> with the provided root content element.
    /// </summary>
    /// <param name="content">The element to index for values and used for lookup operations.</param>
    /// <exception cref="ArgumentNullException">element is null</exception>
    public LogixIndex(XElement content)
    {
        _content = content ?? throw new ArgumentNullException(nameof(content));

        //Wire up a changed event handler to reset the local cache when changes are detected.
        _content.Changed += OnContentChanged;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Reference"/> exists within the indexed elements.
    /// </summary>
    /// <param name="reference">The <see cref="Reference"/> to locate in the indexed elements.</param>
    /// <returns>True if the specified <see cref="Reference"/> exists in the indexed elements; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="reference"/> parameter is null.</exception>
    public bool ContainsElement(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        IndexIfRequired();

        return _elements.ContainsKey(reference);
    }

    /// <summary>
    /// Retrieves an entity of the specified type that corresponds to the provided reference.
    /// </summary>
    /// <typeparam name="TElement">The type of the element to retrieve. Must implement <see cref="ILogixElement"/>.</typeparam>
    /// <param name="reference">The reference associated with the entity to retrieve.</param>
    /// <returns>The entity of type <typeparamref name="TElement"/> associated with the given reference.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="reference"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the <paramref name="reference"/> cannot be found in the index.</exception>
    public TElement GetElement<TElement>(Reference reference) where TElement : ILogixElement
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        IndexIfRequired();

        if (!_elements.TryGetValue(reference, out var element))
            throw new KeyNotFoundException($"Reference was not found in project: '{reference}'");

        return element.Deserialize<TElement>();
    }

    /// <summary>
    /// Attempts to retrieve an entity of the specified type from the index using the given reference.
    /// </summary>
    /// <typeparam name="TElement">The type of the entity to retrieve. Must implement <see cref="ILogixElement"/>.</typeparam>
    /// <param name="reference">The reference used to identify the entity in the index.</param>
    /// <param name="result">
    /// When the method returns, contains the entity of type <typeparamref name="TElement"/> if found;
    /// otherwise, the default value for the type of the <paramref name="result"/> parameter.
    /// </param>
    /// <returns>
    /// <c>true</c> if an entity matching the provided reference was found in the index; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="reference"/> is null.</exception>
    public bool TryGetElement<TElement>(Reference reference, out TElement result) where TElement : ILogixElement
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        IndexIfRequired();

        if (_elements.TryGetValue(reference, out var element))
        {
            result = element.Deserialize<TElement>();
            return true;
        }

        result = default!;
        return false;
    }

    /// <summary>
    /// Retrieves all references associated with the specified name.
    /// </summary>
    /// <param name="name">The name for which references are to be found. Must not be null or empty.</param>
    /// <returns>
    /// A collection of <see cref="Reference"/> objects representing the references associated with the given name.
    /// If no references are found, an empty collection is returned.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the provided name is null or empty.</exception>
    public IEnumerable<Reference> FindReferences(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        IndexIfRequired();

        return _references.TryGetValue(name, out var references) ? references : [];
    }

    /// <summary>
    /// Ensures that the index is initialized by rebuilding it if required. We clear both dictionaries when we detect
    /// changes to ensure validity. As long as we are only reading the file, we should not have to reindex each call.
    /// </summary>
    private void IndexIfRequired()
    {
        if (_isIndexed)
            return;

        IndexContent(_content);
        _isIndexed = true;
    }

    /// <summary>
    /// Traverse only the elements we need to extract the reference and usage information and store in
    /// the local cache. To make indexing as fast as possible, don't iterate every element in the document, just
    /// get the child elements directly.
    /// </summary>
    private void IndexContent(XElement content)
    {
        var controller = content.Element(L5XName.Controller) ?? throw content.L5XError(L5XName.Controller);

        controller.Element(L5XName.DataTypes)?.Elements(L5XName.DataType)
            .ToList().ForEach(IndexDataTypeElement);

        controller.Element(L5XName.Modules)?.Elements(L5XName.Module)
            .ToList().ForEach(IndexModuleElement);

        controller.Element(L5XName.AddOnInstructionDefinitions)?.Elements(L5XName.AddOnInstructionDefinition)
            .ToList().ForEach(IndexAddOnInstructionElement);

        controller.Element(L5XName.Tags)?.Elements(L5XName.Tag)
            .ToList().ForEach(IndexTagElement);

        controller.Element(L5XName.Programs)?.Elements(L5XName.Program)
            .ToList().ForEach(IndexProgramElement);

        controller.Element(L5XName.Tasks)?.Elements(L5XName.Task)
            .ToList().ForEach(IndexTaskElement);
    }

    /// <summary>
    /// Index the provided data type element reference.
    /// Data types also potentially contain usages of other data types.
    /// </summary>
    private void IndexDataTypeElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.DataType) return;

        var reference = Reference.To(element);
        AddOrUpdateElement(reference, element);

        var members = element.Elements(L5XName.Member);

        foreach (var member in members)
        {
            if (!member.TryGetAttribute(L5XName.DataType, out var dataType)) continue;
            AddOrUpdateReference(dataType, reference);
        }
    }

    /// <summary>
    /// Index the provided module element reference.
    /// Modules also contain tag data that we want to index.
    /// </summary>
    private void IndexModuleElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.Module) return;

        var reference = Reference.To(element);
        AddOrUpdateElement(reference, element);

        element.Descendants().Where(e => e.IsTagElement()).ToList().ForEach(IndexTagElement);
    }

    /// <summary>
    /// 
    /// </summary>
    private void IndexAddOnInstructionElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.AddOnInstructionDefinition) return;

        var reference = Reference.To(element);
        AddOrUpdateElement(reference, element);

        element.Element(L5XName.LocalTags)?.Elements(L5XName.LocalTag).ToList().ForEach(IndexTagElement);
        element.Element(L5XName.Parameters)?.Elements(L5XName.Parameter).ToList().ForEach(IndexTagElement);
        element.Element(L5XName.Routines)?.Elements(L5XName.Routine).ToList().ForEach(IndexRoutineElement);
    }

    /// <summary>
    /// 
    /// </summary>
    private void IndexProgramElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.Program) return;

        var reference = Reference.To(element);
        AddOrUpdateElement(reference, element);

        if (element.TryGetAttribute(L5XName.MainRoutineName, out var mainRoutine))
            AddOrUpdateReference(mainRoutine, reference);

        if (element.TryGetAttribute(L5XName.FaultRoutineName, out var faultRoutine))
            AddOrUpdateReference(faultRoutine, reference);

        element.Element(L5XName.Tags)?.Elements(L5XName.Tag).ToList().ForEach(IndexTagElement);
        element.Element(L5XName.Routines)?.Elements(L5XName.Routine).ToList().ForEach(IndexRoutineElement);
    }

    /// <summary>
    /// Index the provided tag element reference.
    /// Tags will also contain usages to a DataType, other Tags via Alias config, and various other data references.
    /// Note that we are only indexing the root tag data type and not nested members for efficiency, but we can use
    /// data type usages/dependencies to determine if a tag uses a nested complex type via its root data type.
    /// </summary>
    private void IndexTagElement(XElement element)
    {
        if (!element.IsTagElement() && element.Name.LocalName != L5XName.Parameter) return;

        var reference = Reference.To(element);
        AddOrUpdateElement(reference, element);

        if (element.TryGetAttribute(L5XName.DataType, out var dataType))
            AddOrUpdateReference(dataType, reference);

        if (element.TryGetAttribute(L5XName.AliasFor, out var alias))
            AddOrUpdateReference(alias, reference);

        //todo is this right? Is the reference to the tag or the instruction where it is configured. This is so confusing by Rockwell.
        //todo this was slowing down index quite a bit. I will need to probably figure out to be more explicit/use XElement only.
        //  I do think this might be the only reference we are missing from the current implementation...
        /*if (element.TryGetFormattedData(out var data) && data is MESSAGE { DestinationTag: not null } message)
            AddOrUpdateUsages(message.DestinationTag, reference);*/
    }

    /// <summary>
    /// Index the provided routine element reference.
    /// Each routine can contain code which we need to iterate and index.
    /// </summary>
    private void IndexRoutineElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.Routine) return;

        var reference = Reference.To(element);
        AddOrUpdateElement(reference, element);

        element.Descendants().Where(e => e.IsCodeElement()).ToList().ForEach(IndexCodeElement);
    }

    /// <summary>
    /// Index the provided code element reference.
    /// Code elements will contain instruction logic that we want to parse for tag/instruction/component usages.
    /// </summary>
    private void IndexCodeElement(XElement element)
    {
        if (!element.IsCodeElement()) return;

        var code = element.Deserialize<ILogixCode>();
        AddOrUpdateElement(code.Reference, element);

        foreach (var instruction in code.Instructions())
        {
            var reference = code.Reference.At(instruction);

            AddOrUpdateReference(instruction.Key, reference);

            foreach (var tag in instruction.Arguments.Where(a => a.Type.IsTag))
                AddOrUpdateReference(tag, reference);
            
            
        }
    }

    /// <summary>
    /// Index the provided task component reference.
    /// Tasks also contain schedule programs which we will consider usages of that component.
    /// </summary>
    private void IndexTaskElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.Task) return;

        var reference = Reference.To(element);
        AddOrUpdateElement(reference, element);

        element.Element(L5XName.ScheduledPrograms)?.Elements(L5XName.ScheduledProgram).ToList().ForEach(e =>
        {
            if (!e.TryGetAttribute(L5XName.Name, out var scheduled)) return;
            AddOrUpdateReference(scheduled, reference);
        });
    }

    /// <summary>
    /// Adds a new reference and its associated element to the collection,
    /// or updates the element if the reference already exists.
    /// </summary>
    private void AddOrUpdateElement(Reference reference, XElement element)
    {
        _elements.AddOrUpdate(reference, _ => element, (_, _) => element);
    }

    /// <summary>
    /// Adds a new reference to the internal usage dictionary if the specified key does not exist,
    /// or updates the existing collection of references associated with the key by appending the new reference.
    /// </summary>
    private void AddOrUpdateReference(string key, Reference reference)
    {
        _references.AddOrUpdate(key, _ => [reference], (_, r) =>
        {
            r.Add(reference);
            return r;
        });
    }

    /// <summary>
    /// Handles the event when the content of the underlying L5X file changes.
    /// Clears all cached elements and references to ensure the index remains accurate.
    /// </summary>
    private void OnContentChanged(object sender, XObjectChangeEventArgs e)
    {
        _elements.Clear();
        _references.Clear();
        _isIndexed = false;
    }
}