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
/// <typeparam name="TElement">The type inheriting <see cref="LogixElement{TElement}"/>.</typeparam>
public class LogixContainer<TElement> : IEnumerable<TElement>, ILogixSerializable where TElement : LogixElement<TElement>
{
    /// <summary>
    /// Creates a empty <see cref="LogixContainer{TComponent}"/>.
    /// </summary>
    public LogixContainer()
    {
        _element = new XElement($"{typeof(TElement).LogixTypeName()}s");
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TComponent}"/> initialized with the provided <see cref="XContainer"/>. 
    /// </summary>
    /// <param name="container">The <see cref="XContainer"/> containing a collection of components.</param>
    /// <exception cref="ArgumentNullException"><c>container</c> is null.</exception>
    public LogixContainer(XElement container)
    {
        _element = container ?? throw new ArgumentNullException(nameof(container));
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TComponent}"/> initialized with the provided collection.
    /// </summary>
    /// <param name="components">The collection of components to initialize.</param>
    public LogixContainer(IEnumerable<TElement> components) : this()
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        foreach (var component in components)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            _element.Add(component.Serialize());
        }
    }
    
    /// <summary>
    /// The underlying <see cref="XElement"/> representing the backing data for the container. Use this object to store
    /// and retrieve data for the the collection.
    /// </summary>
    private readonly XElement _element;

    /// <summary>
    /// Accesses a single component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the component to retrieve.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    public TElement this[int index]
    {
        get => LogixSerializer.Deserialize<TElement>(_element.Elements().ElementAt(index));
        set => _element.Elements().ElementAt(index).ReplaceWith(value.Serialize());
    }
    
    /// <summary>
    /// Accesses a single component with the specified name.
    /// </summary>
    /// <param name="name">The name of the component to retrieve.</param>
    public TElement this[string name]
    {
        get => LogixSerializer.Deserialize<TElement>(_element.Elements().Single(e => e.LogixName() == name));
        set => _element.Elements().Single(e => e.LogixName() == name).ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Adds the provided component to the collection.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    public void Add(TElement component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        _element.Add(component.Serialize());
    }

    /// <summary>
    /// Adds the provided components to the collection.
    /// </summary>
    /// <param name="components">The collection of components to add.</param>
    public void AddRange(IEnumerable<TElement> components)
    {
        if (components is null)
            throw new ArgumentNullException(nameof(components));

        foreach (var component in components)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            _element.Add(component);
        }
    }

    /// <summary>
    /// Removes all components in the collection.
    /// </summary>
    public void Clear() => _element.RemoveNodes();

    /// <summary>
    /// Gets the number of components in the collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of components in the collection.</returns>
    public int Count() => _element.Elements().Count();

    /// <summary>
    /// Inserts the provided component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index at which to insert the component.</param>
    /// <param name="component">The component to insert.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    public void Insert(int index, TElement component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var count = _element.Elements().Count();

        if (index == count)
        {
            _element.Add(component.Serialize());
            return;
        }

        _element.Elements().ElementAt(index).AddBeforeSelf(component.Serialize());
    }

    /// <summary>
    /// Removes a element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    public void Remove(int index)
    {
        _element.Elements().ElementAt(index).Remove();
    }

    /// <summary>
    /// Removes all elements that satisfy the provided condition predicate.
    /// </summary>
    /// <param name="condition">The condition for which to remove components.</param>
    /// <exception cref="ArgumentNullException"><c>condition</c> is null.</exception>
    public void Remove(Func<TElement, bool> condition)
    {
        _element.Elements().Where(e => condition.Invoke(LogixSerializer.Deserialize<TElement>(e))).Remove();
    }

    /// <inheritdoc />
    public XElement Serialize() => _element;

    /// <inheritdoc />
    public IEnumerator<TElement> GetEnumerator() =>
        _element.Elements().Select(LogixSerializer.Deserialize<TElement>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}