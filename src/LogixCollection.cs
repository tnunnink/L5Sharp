using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TComponent"></typeparam>
public class LogixCollection<TComponent> : ILogixCollection<TComponent>
    where TComponent : ILogixComponent, ILogixSerializable
{
    private readonly HashSet<string> _names;

    /// <summary>
    /// Creates a empty <see cref="LogixCollection{TEntity}"/>.
    /// </summary>
    public LogixCollection()
    {
        Container = new XElement($"{typeof(TComponent).LogixTypeName()}s");
        _names = new HashSet<string>();
    }

    /// <summary>
    /// Creates a new <see cref="LogixCollection{TEntity}"/> initialized with the provided <see cref="XContainer"/>. 
    /// </summary>
    /// <param name="container">The <see cref="XContainer"/> containing a collection of components.</param>
    /// <exception cref="ArgumentNullException"><c>container</c> is null.</exception>
    public LogixCollection(XContainer container)
    {
        Container = container ?? throw new ArgumentNullException(nameof(container));
        _names = Container.Elements().Select(e => e.LogixName()).ToHashSet();
    }

    /// <summary>
    /// Creates a new <see cref="LogixCollection{TEntity}"/> initialized with the provided collection.
    /// </summary>
    /// <param name="components">The collection of components to initialize.</param>
    public LogixCollection(IEnumerable<TComponent> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        Container = new XElement($"{typeof(TComponent).LogixTypeName()}s");
        Container.Add(components.Select(e => e.Serialize()));
        _names = Container.Elements().Select(e => e.LogixName()).ToHashSet();
    }

    /// <inheritdoc />
    public XContainer Container { get; }

    /// <inheritdoc />
    public TComponent this[int index]
    {
        get => LogixSerializer.Deserialize<TComponent>(Container.Elements().ElementAt(index));
        set => Container.Elements().ElementAt(index).ReplaceWith(value.Serialize()); //todo validation
    }

    /// <inheritdoc />
    public TComponent this[string name]
    {
        get => LogixSerializer.Deserialize<TComponent>(Container.Elements().Single(e => e.LogixName() == name));
        set => Container.Elements().Single(e => e.LogixName() == name)
            .ReplaceWith(value.Serialize()); //todo validation
    }

    /// <inheritdoc />
    public void Add(TComponent component)
    {
        ValidateComponent(component);
        ValidateUniqueness(component);

        Container.Add(component.Serialize());
        _names.Add(component.Name);
    }

    /// <inheritdoc />
    public void AddMany(IEnumerable<TComponent> components)
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

            names.Add(component.Name);
            elements.Add(component.Serialize());
        }

        Container.Add(elements);
        _names.UnionWith(names);
    }

    /// <inheritdoc />
    public void Clear() => Container.RemoveNodes();

    /// <inheritdoc />
    public bool Contains(string name) => Container.Elements().Any(e => e.LogixName() == name);

    /// <inheritdoc />
    public int Count() => Container.Elements().Count();

    /// <inheritdoc />
    public TComponent? Find(string name)
    {
        var component = Container.Elements().SingleOrDefault(e => e.LogixName() == name);
        return component is not null ? LogixSerializer.Deserialize<TComponent>(component) : default;
    }

    /// <inheritdoc />
    public void Insert(int index, TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var count = Container.Elements().Count();

        if (index < 0 || index > count)
            throw new IndexOutOfRangeException();

        if (index == count)
        {
            Container.Add(component.Serialize());
            return;
        }

        Container.Elements().ElementAt(index).AddBeforeSelf(component.Serialize());
    }

    /// <inheritdoc />
    public void Remove(int index)
    {
        Container.Elements().ElementAt(index).Remove();
    }

    /// <inheritdoc />
    public void Remove(string name)
    {
        Container.Elements().SingleOrDefault(c => c.LogixName() == name)?.Remove();
    }

    /// <inheritdoc />
    public void Remove(Func<TComponent, bool> condition)
    {
        Container.Elements().Where(e => condition.Invoke(LogixSerializer.Deserialize<TComponent>(e))).Remove();
    }

    /// <inheritdoc />
    public void Update(Action<TComponent> update)
    {
        if (update is null)
            throw new ArgumentNullException(nameof(update));

        var elements = Container.Elements();

        foreach (var element in elements)
        {
            var component = LogixSerializer.Deserialize<TComponent>(element);

            update.Invoke(component);
            ValidateComponent(component);
            ValidateUniqueness(component);

            element.ReplaceWith(component.Serialize());
        }
    }

    /// <inheritdoc />
    public void Update(Func<TComponent, bool> condition, Action<TComponent> update)
    {
        foreach (var element in Container.Elements())
        {
            var entity = LogixSerializer.Deserialize<TComponent>(element);
            if (!condition.Invoke(entity)) continue;
            update.Invoke(entity);
            element.ReplaceWith(entity.Serialize());
        }
    }

    /// <inheritdoc />
    public IEnumerator<TComponent> GetEnumerator() =>
        Container.Elements().Select(LogixSerializer.Deserialize<TComponent>).GetEnumerator();

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