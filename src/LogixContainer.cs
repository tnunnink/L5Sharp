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
/// <typeparam name="TEntity"></typeparam>
public class LogixContainer<TEntity> : ILogixCollection<TEntity> where TEntity : ILogixComponent, ILogixSerializable
{
    protected readonly XElement Container;

    /// <summary>
    /// Creates a empty <see cref="LogixContainer{TComponent}"/>.
    /// </summary>
    public LogixContainer()
    {
        Container = new XElement($"{typeof(TEntity).LogixTypeName()}s");
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TComponent}"/> initialized with the provided <see cref="XContainer"/>. 
    /// </summary>
    /// <param name="container">The <see cref="XContainer"/> containing a collection of components.</param>
    /// <exception cref="ArgumentNullException"><c>container</c> is null.</exception>
    public LogixContainer(XElement container)
    {
        Container = container ?? throw new ArgumentNullException(nameof(container));
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TComponent}"/> initialized with the provided collection.
    /// </summary>
    /// <param name="components">The collection of components to initialize.</param>
    public LogixContainer(IEnumerable<TEntity> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        Container = new XElement($"{typeof(TEntity).LogixTypeName()}s");

        foreach (var component in components)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            Container.Add(component.Serialize());    
        }
        
    }

    /// <summary>
    /// Accesses a single component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the component to retrieve.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    public virtual TEntity this[int index]
    {
        get => LogixSerializer.Deserialize<TEntity>(Container.Elements().ElementAt(index));
        set => Container.Elements().ElementAt(index).ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Accesses a single component with the specified name.
    /// </summary>
    /// <param name="name">The name of the component to retrieve.</param>
    public virtual TEntity this[string name]
    {
        get => LogixSerializer.Deserialize<TEntity>(Container.Elements().Single(e => e.LogixName() == name));
        set => Container.Elements().Single(e => e.LogixName() == name).ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Adds the provided component to the collection.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    public virtual void Add(TEntity component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        Container.Add(component.Serialize());
    }

    /// <summary>
    /// Adds the provided components to the collection.
    /// </summary>
    /// <param name="components">The collection of components to add.</param>
    public virtual void AddMany(IEnumerable<TEntity> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        foreach (var component in components)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            Container.Add(component);
        }
    }

    /// <summary>
    /// Removes all components in the collection.
    /// </summary>
    public virtual void Clear() => Container.RemoveNodes();

    /// <summary>
    /// Determines if a component with the specified name exists in the collection.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    /// <returns><c>true</c> if a component with the specified name exists; otherwise, <c>false</c>.</returns>
    public virtual bool Contains(string name) => Container.Elements().Any(e => e.LogixName() == name);

    /// <summary>
    /// Gets the number of components in the collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of components in the collection.</returns>
    public virtual int Count() => Container.Elements().Count();

    /// <summary>
    /// Returns a component with the specified name if found in the collection. If not found, returns <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <returns>If found, the component instance with the specified name; otherwise, <c>null</c>.</returns>
    public virtual TEntity? Find(string name)
    {
        var component = Container.Elements().SingleOrDefault(e => e.LogixName() == name);
        return component is not null ? LogixSerializer.Deserialize<TEntity>(component) : default;
    }

    /// <summary>
    /// Inserts the provided component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index at which to insert the component.</param>
    /// <param name="component">The component to insert.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    public virtual void Insert(int index, TEntity component)
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

    /// <summary>
    /// Removes a component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the component to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    public virtual void Remove(int index)
    {
        Container.Elements().ElementAt(index).Remove();
    }

    /// <summary>
    /// Removes a component with the specified name from the collection.
    /// </summary>
    /// <param name="name">The name of the component to remove.</param>
    public virtual void Remove(string name)
    {
        Container.Elements().SingleOrDefault(c => c.LogixName() == name)?.Remove();
    }

    /// <summary>
    /// Removes all components that satisfy the provided condition predicate.
    /// </summary>
    /// <param name="condition">The condition for which to remove components.</param>
    /// <exception cref="ArgumentNullException"><c>condition</c> is null.</exception>
    public virtual void Remove(Func<TEntity, bool> condition)
    {
        Container.Elements().Where(e => condition.Invoke(LogixSerializer.Deserialize<TEntity>(e))).Remove();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="update"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void Update(Action<TEntity> update)
    {
        if (update is null)
            throw new ArgumentNullException(nameof(update));

        var elements = Container.Elements();

        foreach (var element in elements)
        {
            var component = LogixSerializer.Deserialize<TEntity>(element);
            update.Invoke(component);
            element.ReplaceWith(component.Serialize());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="update"></param>
    public virtual void Update(Func<TEntity, bool> condition, Action<TEntity> update)
    {
        foreach (var element in Container.Elements())
        {
            var entity = LogixSerializer.Deserialize<TEntity>(element);
            if (!condition.Invoke(entity)) continue;
            update.Invoke(entity);
            element.ReplaceWith(entity.Serialize());
        }
    }

    /// <inheritdoc />
    public IEnumerator<TEntity> GetEnumerator() =>
        Container.Elements().Select(LogixSerializer.Deserialize<TEntity>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public XElement Serialize() => Container;
}