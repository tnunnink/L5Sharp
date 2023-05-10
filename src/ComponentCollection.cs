using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// A generic component collection implementation that makes use of a component container and provided L5X to
/// perform methods over XML content.
/// </summary>
/// <typeparam name="TComponent">The logix component type for which the collection represents.</typeparam>
internal class ComponentCollection<TComponent> : ILogixComponentCollection<TComponent>
    where TComponent : class, ILogixComponent
{
    private readonly L5X _l5X;
    private readonly XName _name;
    private readonly ILogixSerializer<TComponent> _serializer;
    private XContainer? _container;
    
    internal ComponentCollection(L5X l5X, XContainer? container)
    {
        _l5X = l5X ?? throw new ArgumentNullException(nameof(l5X));
        _name = typeof(TComponent).GetLogixName();
        _serializer = LogixSerializer.GetSerializer<TComponent>();
        _container = container;
    }
    
    public void Add(TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        if (!component.Name.IsComponentName())
            throw new ArgumentException($"The provided component name '{component.Name}' is not valid.");

        if (_container is not null && _container.Elements().Any(e => e.LogixName() == component.Name))
            throw new InvalidOperationException(
                $"A component with the name '{component.Name}' already exists in the collection.");

        var element = _serializer.Serialize(component);

        if (_container is null)
        {
            _l5X.Normalize();
            _container = _l5X.GetContainer(_name);
        }

        _container?.Add(element);
    }
    
    public void Add(IEnumerable<TComponent> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        var dictionary = components.ToDictionary(c => c.Name);
        if (dictionary.Count == 0) return;

        var elements = new List<XElement>();

        foreach (var (key, component) in dictionary)
        {
            if (!key.IsComponentName())
                throw new ArgumentException($"The provided component name '{key}' is not a valid logix name.");

            if (_container is not null && _container.Elements().Any(e => e.LogixName() == key))
                throw new InvalidOperationException(
                    $"A component with the name '{key}' already exists in the collection.");

            var element = _serializer.Serialize(component);
            elements.Add(element);
        }

        //Only if we make it here should we modify the current L5X content.
        if (_container is null)
        {
            _l5X.Normalize();
            _container = _l5X.GetContainer(_name);
        }

        _container?.Add(elements);
    }
    
    public void Clear() => _container?.RemoveNodes();

    public bool Contains(string name) =>
        _container is not null && _container.Descendants(_name).Any(e => e.LogixName() == name);

    public int Count() => _container?.Elements().Count() ?? 0;

    public TComponent? Find(string name)
    {
        var component = _container?.Descendants(_name).SingleOrDefault(e => e.LogixName() == name);
        return component != null ? _serializer.Deserialize(component) : default;
    }

    public TComponent Get(string name)
    {
        if (_container is null)
            throw new InvalidOperationException(
                $"No container exists for the current component type {typeof(TComponent)}");

        var result = _container.Descendants(_name).SingleOrDefault(e => e.LogixName() == name);

        if (result is null)
            throw new InvalidOperationException($"No component with name '{name}' was found in the collection.");

        return _serializer.Deserialize(result);
    }

    public IEnumerable<TComponent> In(IEnumerable<string> names)
    {
        var components = _container?.Descendants(_name).Where(e => names.Any(n => n == e.LogixName()));
            
        return components is not null
            ? components.Select(c => _serializer.Deserialize(c))
            : Enumerable.Empty<TComponent>();
    }


    public bool Remove(string name)
    {
        var component = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == name);
        if (component is null) return false;
        component.Remove();
        return true;
    }


    public int Remove(IEnumerable<string> names)
    {
        var components = _container?.Descendants(_name).Where(c => names.Any(n => n == c.LogixName())).ToList();
        if (components is null) return 0;
        components.Remove();
        return components.Count;
    }


    public void Rename(string current, string name)
    {
        var component = _container?.Descendants(_name).SingleOrDefault(e => e.LogixName() == current);

        if (component is null)
            throw new InvalidOperationException($"No component with name {current} exists in the collection.");

        //We know if we get here component has a name since it would be null otherwise. 
        component.Attribute(L5XName.Name)!.Value = name;
    }

    public void Update(TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        if (!component.Name.IsComponentName())
            throw new ArgumentException($"The provided component name '{component.Name}' is not valid.");

        if (_container is null)
        {
            _l5X.Normalize();
            _container = _l5X.GetContainer(_name);
        }

        var element = _serializer.Serialize(component);

        var target = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == component.Name);

        if (target is not null)
        {
            target.ReplaceWith(element);
            return;
        }

        _container?.Add(element);
    }

    public void Update(IEnumerable<TComponent> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        var dictionary = components.ToDictionary(c => c.Name);
        if (dictionary.Count == 0) return;

        //We only need to validate the incoming names, since existing will be replaced.
        if (dictionary.Keys.Any(k => !k.IsComponentName()))
            throw new ArgumentException("The provided collection contains invalid component names.");

        if (_container is null)
        {
            _l5X.Normalize();
            _container = _l5X.GetContainer(_name);
        }

        foreach (var (key, component) in dictionary)
        {
            var element = _serializer.Serialize(component);

            var target = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == key);

            if (target is not null)
            {
                target.ReplaceWith(element);
                continue;
            }

            _container?.Add(element);
        }
    }

    public void Update(string name, Action<TComponent> config)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.", nameof(name));

        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var target = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == name);

        if (target is null)
            throw new InvalidOperationException($"No component with name {name} exists in the current collection.");

        var component = _serializer.Deserialize(target);
        
        config.Invoke(component);
        
        target.ReplaceWith(_serializer.Serialize(component));
    }

    public void Update(Action<TComponent> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var elements = _container?.Descendants(_name);

        if ( elements is null) return;

        foreach (var element in elements)
        {
            var component = _serializer.Deserialize(element);
            config.Invoke(component);
            element.ReplaceWith(_serializer.Serialize(component));
        }
    }

    public void Update(Func<TComponent, bool> condition, Action<TComponent> config)
    {
        if (condition is null)
            throw new ArgumentNullException(nameof(condition));
        
        if (config is null)
            throw new ArgumentNullException(nameof(config));
        
        var elements = _container?.Descendants(_name);
        
        if ( elements is null) return;
        
        foreach (var element in elements)
        {
            var component = _serializer.Deserialize(element);
            if (!condition.Invoke(component)) continue;
            config.Invoke(component);
            element.ReplaceWith(_serializer.Serialize(component));
        }
    }

    public IEnumerator<TComponent> GetEnumerator() =>
        _container?.Descendants(_name).Select(e => _serializer.Deserialize(e)).GetEnumerator()
        ?? Enumerable.Empty<TComponent>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}