using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// The internal implementation that indexes components and references and stores them in the local dictionaries.
/// This class is then used by <see cref="L5X"/> to find components, tags, and references quickly.
/// </summary>
internal class LogixIndex
{
    /// <summary>
    /// The root controller element of the project.
    /// </summary>
    private readonly XElement _content;

    /// <summary>
    /// An index of all logix components in the L5X file for fast lookups.
    /// </summary>
    public readonly Dictionary<ComponentKey, Dictionary<string, XElement>> Components = new();

    /// <summary>
    /// An index of all references to a logix component in the L5X file for fast lookups.
    /// </summary>
    public readonly Dictionary<ComponentKey, List<CrossReference>> References = new();

    public LogixIndex(XElement content)
    {
        _content = content;

        //Initialize the dictionaries.
        IndexComponents();
        IndexReferences();

        //Detect changes to keep index up to date.
        _content.Changing += OnContentChanging;
        _content.Changed += OnContentChanged;
    }

    /// <summary>
    /// Handles adding a component to the index. This requires the element to be attached to the L5X tree
    /// for it to determine the scope of the element.
    /// </summary>
    private void AddComponent(XElement element)
    {
        var key = new ComponentKey(element.L5XType(), element.LogixName());
        var container = ScopeLevel.Container(element);

        if (Components.TryAdd(key, new Dictionary<string, XElement> { { container, element } })) return;
        Components[key].TryAdd(container, element);
    }

    /// <summary>
    /// Handles adding the provided reference to the index. If no reference exists for the provided key, a new
    /// entry is created, otherwise the reference is added to the existing collection.
    /// </summary>
    private void AddReference(CrossReference reference)
    {
        if (!References.TryAdd(reference.Key, [reference]))
            References[reference.Key].Add(reference);
    }

    /// <summary>
    /// Adds the provided collection of references. This is a convenience method to add multiple references.
    /// </summary>
    private void AddReferences(IEnumerable<CrossReference> references)
    {
        foreach (var reference in references)
        {
            AddReference(reference);
        }
    }

    /// <summary>
    /// Handles adding all references to the index that are associated with the provided element.
    /// This is a convenience method since we need to parse the element as an <see cref="ILogixReferencable"/> to
    /// obtain the references to add.
    /// </summary>
    private void AddReferences(XElement element)
    {
        if (element.Deserialize() is not ILogixReferencable referencable) return;
        var references = referencable.References().ToList();
        AddReferences(references);
    }

    /// <summary>
    /// Triggered when any content of the L5X is about to change. We need to know if the object changing is an element
    /// we are maintaining state for in the component or reference index. If so, we need to perform the necessary actions.
    /// Prior to the object changing and if the change action is a remove or value change, we need to remove the applicable
    /// components or references from the index. This is because after the object has changed we no longer have access
    /// to the previous state.
    /// </summary>
    private void OnContentChanging(object? sender, XObjectChangeEventArgs e)
    {
        if (sender is null) return;
        
        if (e.ObjectChange is XObjectChange.Remove)
        {
            if (IsComponentElement(sender)) RemoveComponent((XElement)sender);
            if (IsCodeElement(sender)) RemoveReferences((XElement)sender);
        }

        if (e.ObjectChange is not XObjectChange.Value) return;

        if (IsNameProperty(sender)) RemoveComponent(((XAttribute)sender).Parent!);
        if (IsDataTypeProperty(sender))
        {
            var attribute = (XAttribute)sender;
            var reference = new CrossReference(attribute.Parent!, L5XName.DataType, attribute.Value);
            RemoveReference(reference);
        }

        if (IsCodeProperty(sender)) RemoveReferences(((XAttribute)sender).Parent!);
    }

    /// <summary>
    /// Triggered when any content of the L5X has changed.  We need to know if the object that changed is an element
    /// we are maintaining state for in the component or reference index. If so, we need to perform the necessary actions.
    /// Once the object has changed, the sender will hold the new state. If the change action is an add or value change,
    /// and the element or property value is one that would refer to an indexed object, we will update the state of the index
    /// to ensure consistency.
    /// </summary>
    private void OnContentChanged(object? sender, XObjectChangeEventArgs e)
    {
        if (sender is null) return;
        
        if (e.ObjectChange is XObjectChange.Add)
        {
            if (IsComponentElement(sender)) AddComponent((XElement)sender);
            if (IsCodeElement(sender)) AddReferences((XElement)sender);
        }

        if (e.ObjectChange is not XObjectChange.Value) return;

        if (IsNameProperty(sender)) AddComponent(((XAttribute)sender).Parent!);
        if (IsDataTypeProperty(sender))
        {
            var attribute = (XAttribute)sender;
            var reference = new CrossReference(attribute.Parent!, L5XName.DataType, attribute.Value);
            AddReference(reference);
        }

        if (IsCodeProperty(sender)) AddReferences(((XAttribute)sender).Parent!);
    }

    /// <summary>
    /// Finds all logix component elements and indexes them into a local dictionary for fast lookups.
    /// </summary>
    private void IndexComponents()
    {
        IndexControllerScopedComponents();
        IndexProgramScopedComponents();
        IndexModuleDefinedTagComponents();
    }

    /// <summary>
    /// Finds all controller scoped top level components to index. This includes all components except for program tags,
    /// routines, and module defined IO tags, which are handled separately in the following methods.
    /// </summary>
    private void IndexControllerScopedComponents()
    {
        //The scope for all controller scoped components will be the name of the controller.
        var scope = _content.LogixName();

        //Only consider component elements with a valid name attribute. Some components don't have and name and we
        //can't possibly index them.
        var components = _content.Elements()
            .SelectMany(c => c.Elements().Where(e => e.Attribute(L5XName.Name) is not null));

        foreach (var component in components)
        {
            var key = new ComponentKey(component.L5XType(), component.LogixName());
            if (!Components.TryAdd(key, new Dictionary<string, XElement> { { scope, component } }))
                Components[key].TryAdd(scope, component);
        }
    }

    /// <summary>
    /// Handles iterating each program component element in the L5X and indexes each tag and routine
    /// with the corresponding program scope.
    /// </summary>
    private void IndexProgramScopedComponents()
    {
        var programs = _content.Descendants(L5XName.Programs).Elements();

        foreach (var program in programs)
        {
            var scope = program.LogixName();

            foreach (var component in program.Descendants()
                         .Where(d => d.Name.LocalName is L5XName.Tag or L5XName.Routine))
            {
                var key = new ComponentKey(component.L5XType(), component.LogixName());
                if (!Components.TryAdd(key, new Dictionary<string, XElement> { { scope, component } }))
                    Components[key].TryAdd(scope, component);
            }
        }
    }

    /// <summary>
    /// Handles iterating each module defined tag component element in the L5X and indexes each tag.
    /// </summary>
    private void IndexModuleDefinedTagComponents()
    {
        var scope = _content.LogixName();

        foreach (var component in _content.Descendants(L5XName.Modules).Descendants().Where(e =>
                     e.L5XType() is L5XName.ConfigTag or L5XName.InputTag or L5XName.OutputTag))
        {
            var key = new ComponentKey(L5XName.Tag, component.ModuleTagName());
            if (!Components.TryAdd(key, new Dictionary<string, XElement> { { scope, component } }))
                Components[key].TryAdd(scope, component);
        }
    }

    /// <summary>
    /// Finds all logix reference elements and indexes them into a local dictionary for fast lookups.
    /// </summary>
    private void IndexReferences()
    {
        IndexDataTypeReferences();
        IndexCodeReferences();
    }

    /// <summary>
    /// Finds all elements with a data type attribute and indexes them into a local reference index for fast lookup.
    /// This will include technically any type, predefined, atomic, user defined, or add on instruction.
    /// </summary>
    private void IndexDataTypeReferences()
    {
        var targets = _content.Descendants().Where(d => d.Attribute(L5XName.DataType) is not null);
        foreach (var target in targets)
        {
            var componentName = target.Attribute(L5XName.DataType)!.Value;
            var reference = new CrossReference(target, L5XName.DataType, componentName);
            if (!References.TryAdd(reference.Key, [reference]))
                References[reference.Key].Add(reference);
        }
    }

    /// <summary>
    /// Finds all routine content elements, iterates each "code" element, and delegates the retrieval or references to the
    /// materialized logix element object. Then adds each set of references to the reference index for fast lookup.
    /// </summary>
    private void IndexCodeReferences()
    {
        var routines = _content.Descendants(L5XName.Program).SelectMany(p => p.Descendants(L5XName.Routine));
        
        foreach (var routine in routines)
        {
            var type = routine.Attribute(L5XName.Type)?.Value.Parse<RoutineType>().ContentName;
            
            switch (type)
            {
                case L5XName.RLLContent:
                    AddReferences(routine.Descendants(L5XName.Rung).SelectMany(x => new Rung(x).References()));
                    break;
                case L5XName.STContent:
                    AddReferences(routine.Descendants(L5XName.Line).SelectMany(x => new Line(x).References()));
                    break;
                case L5XName.FBDContent:
                    AddReferences(routine.Descendants(L5XName.Sheet).SelectMany(x => new Sheet(x).References()));
                    break;
                case L5XName.SFCContent:
                    AddReferences(new Chart(routine).References());
                    break;
            }
        }
    }

    /// <summary>
    /// Determines if the provided object is a component element, for which we need to reindex the component.
    /// </summary>
    private static bool IsComponentElement(object sender)
    {
        if (sender is not XElement element) return false;
        var componentTypes = ComponentType.All().Select(c => c.Value).ToList();
        return componentTypes.Contains(element.Name.LocalName);
    }

    /// <summary>
    /// Determines if the provided object is a attribute or property is a component name, for which we need to
    /// reindex the component.
    /// </summary>
    private static bool IsNameProperty(object sender)
    {
        if (sender is not XAttribute attribute) return false;
        if (attribute.Name.LocalName is not L5XName.Name) return false;
        if (attribute.Parent is null) return false;
        var componentTypes = ComponentType.All().Select(c => c.Value).ToList();
        return componentTypes.Contains(attribute.Parent.Name.LocalName);
    }

    /// <summary>
    /// Determines if the provided object is a attribute or property is a data type reference, for which we need to
    /// reindex references.
    /// </summary>
    private static bool IsDataTypeProperty(object sender)
    {
        if (sender is not XAttribute attribute) return false;
        if (attribute.Name.LocalName is not L5XName.DataType) return false;
        if (attribute.Parent is null) return false;
        var componentTypes = ComponentType.All().Select(c => c.Value).ToList();
        return componentTypes.Contains(attribute.Parent.Name.LocalName);
    }

    /// <summary>
    /// Determines if the provided object is a a logix code element, for which we need to reindex references.
    /// </summary>
    private static bool IsCodeElement(object sender)
    {
        if (sender is not XElement element) return false;
        var contentTypes = RoutineType.All().Select(r => r.ContentName).ToList();
        return element.Ancestors().Any(a => contentTypes.Contains(a.Name.LocalName));
    }

    /// <summary>
    /// Determines if the provided object is a attribute or property of a logix code element, for which we need to
    /// reindex references.
    /// </summary>
    private static bool IsCodeProperty(object sender)
    {
        if (sender is not XAttribute attribute) return false;
        if (attribute.Name.LocalName is not
            (L5XName.Operand or L5XName.Argument or L5XName.Routine or L5XName.Type or L5XName.Name)) return false;
        return attribute.Parent is not null && IsCodeElement(attribute.Parent);
    }

    /// <summary>
    /// Handles removing a component matching the current attached element from the index. This requires the element
    /// to be attached to the L5X tree for it to determine the scope of the element. 
    /// </summary>
    private void RemoveComponent(XElement element)
    {
        var key = new ComponentKey(element.Name.LocalName, element.LogixName());
        var scope = ScopeLevel.Container(element);

        if (!Components.TryGetValue(key, out var components)) return;

        if (components.Count == 1)
        {
            Components.Remove(key);
            return;
        }

        components.Remove(scope);
    }

    /// <summary>
    /// Handles removing the provided reference from the index. If only a single reference exists for the provided key,
    /// the entire reference is removed, otherwise we iterate the references and remove all that match the provided
    /// reference's location and type.
    /// </summary>
    private void RemoveReference(CrossReference reference)
    {
        if (!References.TryGetValue(reference.Key, out var results)) return;
        if (results.Count == 1)
        {
            References.Remove(reference.Key);
            return;
        }

        results.RemoveAll(r => r.Equals(reference));
    }

    /// <summary>
    /// Removes the provided collection of references. This is a convenience method to remove multiple references.
    /// </summary>
    private void RemoveReferences(IEnumerable<CrossReference> references)
    {
        foreach (var reference in references)
        {
            RemoveReference(reference);
        }
    }

    /// <summary>
    /// Handles removing all references from the index that are associated with the provided element.
    /// This is a convenience method since we need to parse the element as an <see cref="ILogixReferencable"/> to
    /// obtain the references to remove.
    /// </summary>
    private void RemoveReferences(XElement element)
    {
        if (element.Deserialize() is not ILogixReferencable referencable) return;
        var references = referencable.References().ToList();
        RemoveReferences(references);
    }
}