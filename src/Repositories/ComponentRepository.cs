using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories;

/// <summary>
/// A generic component collection implementation that makes use of a component container and provided L5X to
/// perform methods over XML content.
/// </summary>
/// <typeparam name="TComponent">The logix component type for which the collection represents.</typeparam>
internal class ComponentRepository<TComponent> : ILogixComponentRepository<TComponent>
    where TComponent : ILogixComponent
{
    private readonly XName _name;
    private readonly ILogixSerializer<TComponent> _serializer;
    private readonly XContainer _container;
    private readonly HashSet<string> _names;
    
    internal ComponentRepository(XContainer container)
    {
        _name = typeof(TComponent).GetLogixName();
        _serializer = LogixSerializer.GetSerializer<TComponent>();
        _container = container ?? throw new ArgumentNullException(nameof(container));
        _names = _container.Descendants(_name).Select(e => e.LogixName()).ToHashSet();
    }

    internal ComponentRepository(L5X l5X)
    {
        _name = typeof(TComponent).GetLogixName();
        _serializer = LogixSerializer.GetSerializer<TComponent>();
        _container = l5X.GetContainer(_name);
        _names = _container.Descendants(_name).Select(e => e.LogixName()).ToHashSet();
    }

    public int Count => _container.Elements().Count();

    public void Add(TComponent component)
    {
        ValidateComponent(component);
        ValidateUniqueness(component);

        var element = _serializer.Serialize(component);

        _container.Add(element);
        _names.Add(component.Name);
    }

    public void Add(IEnumerable<TComponent> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        var names = new HashSet<string>();
        var elements = new List<XElement>();

        foreach (var component in components)
        {
            ValidateComponent(component);
            ValidateUniqueness(component);

            if (names.Contains(component.Name))
                throw new ArgumentException($"The provided collection has duplicate name '{component.Name}'.");

            var element = _serializer.Serialize(component);

            names.Add(component.Name);
            elements.Add(element);
        }

        _container.Add(elements);
        _names.UnionWith(names);
    }

    public bool Contains(string name) => _container.Descendants(_name).Any(e => e.LogixName() == name);

    public TComponent? Find(string name)
    {
        var component = _container.Descendants(_name).SingleOrDefault(e => e.LogixName() == name);
        return component is not null ? _serializer.Deserialize(component) : default;
    }

    public void Remove() => _container.RemoveNodes();

    public void Remove(TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        _container.Descendants(_name).SingleOrDefault(c => c.LogixName() == component.Name)?.Remove();
    }

    public void Remove(IEnumerable<TComponent> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        foreach (var component in components)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            _container.Descendants(_name).SingleOrDefault(c => c.LogixName() == component.Name)?.Remove();
        }
    }

    public void Remove(string name) =>
        _container.Descendants(_name).SingleOrDefault(c => c.LogixName() == name)?.Remove();

    public void Remove(IEnumerable<string> names) =>
        _container.Descendants(_name).Where(c => names.Any(n => n == c.LogixName())).Remove();

    public void Remove(Func<TComponent, bool> predicate) => Remove(_container.Descendants(_name)
        .Select(e => _serializer.Deserialize(e)).Where(predicate.Invoke));

    public void Update(TComponent component)
    {
        ValidateComponent(component);

        var element = _serializer.Serialize(component);

        var target = _container.Descendants(_name).SingleOrDefault(c => c.LogixName() == component.Name);

        if (target is not null)
        {
            target.ReplaceWith(element);
            return;
        }

        _container.Add(element);
    }

    public void Update(IEnumerable<TComponent> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        var dictionary = components.ToDictionary(c => c.Name);

        foreach (var (key, component) in dictionary)
        {
            var element = _serializer.Serialize(component);

            var target = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == key);

            if (target is not null)
            {
                target.ReplaceWith(element);
                continue;
            }

            ValidateComponent(component);

            _container?.Add(element);
            _names.Add(component.Name);
        }
    }

    public void Update(string name, Action<TComponent> config)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var target = _container.Descendants(_name).SingleOrDefault(c => c.LogixName() == name);

        if (target is null)
            throw new InvalidOperationException($"No component with name {name} exists in the current collection.");

        var component = _serializer.Deserialize(target);

        config.Invoke(component);
        
        ValidateComponent(component);

        target.ReplaceWith(_serializer.Serialize(component));
    }

    public void Update(Action<TComponent> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var elements = _container.Descendants(_name);

        foreach (var element in elements)
        {
            var component = _serializer.Deserialize(element);
            config.Invoke(component);
            ValidateComponent(component);
            element.ReplaceWith(_serializer.Serialize(component));
        }
    }

    public void Update(Func<TComponent, bool> condition, Action<TComponent> config)
    {
        if (condition is null)
            throw new ArgumentNullException(nameof(condition));

        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var elements = _container.Descendants(_name);

        foreach (var element in elements)
        {
            var component = _serializer.Deserialize(element);
            if (!condition.Invoke(component)) continue;
            config.Invoke(component);
            ValidateComponent(component);
            element.ReplaceWith(_serializer.Serialize(component));
        }
    }

    public IEnumerator<TComponent> GetEnumerator() =>
        _container.Descendants(_name).Select(e => _serializer.Deserialize(e)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static void ValidateComponent(TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        if (!component.Name.IsComponentName())
            throw new ArgumentException($"The provided component name '{component.Name}' is not valid.");
    }

    private void ValidateUniqueness(TComponent component)
    {
        if (_names.Contains(component.Name))
            throw new InvalidOperationException($"A component with the name '{component.Name}' already exists.");
    }
}