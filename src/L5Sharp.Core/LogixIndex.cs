using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A class that encapsulates the code we need to index the L5X for values that we can look up and find references to.
/// </summary>
public class LogixIndex
{
    /// <summary>
    /// The L5X file to index for fast lookups of references and usages.
    /// </summary>
    private readonly L5X _file;

    /// <summary>
    /// Internal cache of all indexed entity references (components and code elements). This lets us find/lookup components
    /// in constant time and not have to traverse the document. Although more of a micro optimization compared to usages,
    /// as long as we are indexing the document, it seems that we might as well index these references.
    /// </summary>
    private readonly ConcurrentDictionary<Reference, XElement> _references = [];

    /// <summary>
    /// Internal cache of all indexed usages. These are values/names found in properties in the L5X project that represent
    /// a usage of another component element. Each value will have an associated list of references that identify
    /// where in the project the usage was found. The reference object will contain addition information,
    /// including logic for tag/component usages, so we can further inspect the context of the reference.
    /// That context will allow components to evaluate which references are applicable
    /// (since the string key of this dictionary does not make it clear which component type it refers to).
    /// </summary>
    private readonly ConcurrentDictionary<string, List<Reference>> _usages = [];

    /// <summary>
    /// Creates a new <see cref="LogixIndex"/> with the provided root content element.
    /// </summary>
    /// <param name="content">The element to index for values and used for lookup operations.</param>
    /// <exception cref="ArgumentNullException">element is null</exception>
    public LogixIndex(L5X content)
    {
        _file = content ?? throw new ArgumentNullException(nameof(content));

        //Wire up a changed event handler to clear our internal cache when changes are detected to ensure the
        //validity of the dictionaries.
        _file.Info.Serialize().Changed += OnContentChanged;
    }

    /// <summary>
    /// Retrieves an entity of the specified type that corresponds to the provided reference.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to retrieve. Must implement <see cref="ILogixEntity"/>.</typeparam>
    /// <param name="reference">The reference associated with the entity to retrieve.</param>
    /// <returns>The entity of type <typeparamref name="TEntity"/> associated with the given reference.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="reference"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the <paramref name="reference"/> cannot be found in the index.</exception>
    public TEntity GetEntity<TEntity>(Reference reference) where TEntity : ILogixEntity
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        IndexIfRequired();

        if (!_references.TryGetValue(reference, out var element))
            throw new KeyNotFoundException($"Reference was not found in project: '{reference}'");

        return element.Deserialize<TEntity>();
    }

    /// <summary>
    /// Attempts to retrieve an entity of the specified type from the index using the given reference.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to retrieve. Must implement <see cref="ILogixEntity"/>.</typeparam>
    /// <param name="reference">The reference used to identify the entity in the index.</param>
    /// <param name="result">
    /// When the method returns, contains the entity of type <typeparamref name="TEntity"/> if found;
    /// otherwise, the default value for the type of the <paramref name="result"/> parameter.
    /// </param>
    /// <returns>
    /// <c>true</c> if an entity matching the provided reference was found in the index; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="reference"/> is null.</exception>
    public bool TryGetEntity<TEntity>(Reference reference, out TEntity result) where TEntity : ILogixEntity
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        IndexIfRequired();

        if (_references.TryGetValue(reference, out var element))
        {
            result = element.Deserialize<TEntity>();
            return true;
        }

        result = default!;
        return false;
    }

    /// <summary>
    /// Finds and returns all references (usages) to the specified name within the index.
    /// </summary>
    /// <param name="name">The name of the element for which to find usages.</param>
    /// <returns>
    /// A collection of <see cref="Reference"/> objects representing the usages of the specified name.
    /// Returns an empty collection if no usages are found.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the provided name is null or an empty string.</exception>
    public IEnumerable<Reference> FindUsages(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("");

        IndexIfRequired();

        return _usages.TryGetValue(name, out var references) ? references : [];
    }

    /// <summary>
    /// Ensures that the index is initialized by rebuilding it if required. We clear both dictionaries when we detect
    /// changes to ensure validity. As long as we are only reading the file, we should not have to reindex each call.
    /// </summary>
    private void IndexIfRequired()
    {
        if (_references.Count + _usages.Count > 0)
            return;

        IndexContent(_file.Info.Serialize());
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

        AddOrUpdateReference(reference, element);

        var members = element.Elements(L5XName.Member);

        foreach (var member in members)
        {
            if (!member.TryGetAttribute(L5XName.DataType, out var dataType)) continue;
            AddOrUpdateUsages(dataType, reference);
        }
    }

    /// <summary>
    /// Index the provided module element reference.
    /// Modules also contain tag data that we want to index. Each
    /// </summary>
    private void IndexModuleElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.Module) return;

        var reference = Reference.To(element);
        AddOrUpdateReference(reference, element);

        element.Descendants().Where(e => e.IsTagElement()).ToList().ForEach(IndexTagElement);
    }

    /// <summary>
    /// 
    /// </summary>
    private void IndexAddOnInstructionElement(XElement element)
    {
        if (element.Name.LocalName is not L5XName.AddOnInstructionDefinition) return;

        var reference = Reference.To(element);
        AddOrUpdateReference(reference, element);

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
        AddOrUpdateReference(reference, element);

        if (element.TryGetAttribute(L5XName.MainRoutineName, out var mainRoutine))
            AddOrUpdateUsages(mainRoutine, reference);

        if (element.TryGetAttribute(L5XName.FaultRoutineName, out var faultRoutine))
            AddOrUpdateUsages(faultRoutine, reference);

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
        if (!element.IsTagElement() || element.Name.LocalName != L5XName.Parameter) return;

        var reference = Reference.To(element);
        AddOrUpdateReference(reference, element);

        if (element.TryGetAttribute(L5XName.DataType, out var dataType))
            AddOrUpdateUsages(dataType, reference);

        if (element.TryGetAttribute(L5XName.AliasFor, out var alias))
            AddOrUpdateUsages(alias, reference);

        //todo is this right? Is the reference to the tag or the instruction where it is configured. This is so confusing by Rockwell.
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
        AddOrUpdateReference(reference, element);

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
        AddOrUpdateReference(code.Reference, element);

        foreach (var instruction in code.Instructions())
        {
            var reference = code.Reference.ToLogic(instruction);

            AddOrUpdateUsages(reference.Logic.Key, reference);

            foreach (var tag in reference.Logic.Tags)
                AddOrUpdateUsages(tag, reference);
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
        AddOrUpdateReference(reference, element);

        element.Element(L5XName.ScheduledPrograms)?.Elements(L5XName.ScheduledProgram).ToList().ForEach(e =>
        {
            if (!e.TryGetAttribute(L5XName.Name, out var scheduled)) return;
            AddOrUpdateUsages(scheduled, reference);
        });
    }

    /// <summary>
    /// Adds a new reference and its associated element to the collection, or updates the element if the reference already exists.
    /// </summary>
    /// <param name="reference">The reference key used for indexing the element.</param>
    /// <param name="element">The XML element associated with the reference to be added or updated.</param>
    private void AddOrUpdateReference(Reference reference, XElement element)
    {
        _references.AddOrUpdate(reference, _ => element, (_, _) => element);
    }

    /// <summary>
    /// Adds a new reference to the internal usage dictionary if the specified key does not exist,
    /// or updates the existing collection of references associated with the key by appending the new reference.
    /// </summary>
    /// <param name="key">The key associated with the references in the usage dictionary.</param>
    /// <param name="reference">The reference to add or update in the collection associated with the specified key.</param>
    private void AddOrUpdateUsages(string key, Reference reference)
    {
        _usages.AddOrUpdate(key, _ => [reference], (_, r) =>
        {
            r.Add(reference);
            return r;
        });
    }

    /// <summary>
    /// Handles the event when the content of the underlying L5X file changes.
    /// Clears all cached references and usages to ensure the index remains accurate.
    /// </summary>
    /// <param name="sender">The source of the event, typically the L5X file content object.</param>
    /// <param name="e">Provides data about the change event of an <see cref="XObject"/>.</param>
    private void OnContentChanged(object sender, XObjectChangeEventArgs e)
    {
        _references.Clear();
        _usages.Clear();
    }
}