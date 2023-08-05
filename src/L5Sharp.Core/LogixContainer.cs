using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A generic collection that provides operations over an underlying <see cref="XElement"/> container of <see cref="LogixElement"/> objects.
/// </summary>
/// <typeparam name="TElement">The type inheriting <see cref="LogixElement"/>.</typeparam>
/// <remarks>
/// <para>
/// This class represents a wrapper around a L5X element that contains a sequence of child elements of the same type.
/// This class exposes collection methods for querying and modifying child elements of the container.
/// Note that a container could potentially contain more than one type of child element, but this container class will
/// only operate over the specified element type.
/// </para>
/// <para>
/// The class is designed to only offer very basic operations, allowing it to be applicable to all container type elements,
/// However, the user can extended the API for any container type using extension methods and <see cref="Serialize"/>
/// to get the underlying <see cref="XElement"/> container object. See <see cref="LogixExtensions"/> for examples.
/// </para>
/// </remarks>
public class LogixContainer<TElement> : IEnumerable<TElement>, ILogixSerializable where TElement : LogixElement
{
    /// <summary>
    /// The underlying <see cref="XElement"/> representing the backing data for the container. Use this object to store
    /// and retrieve data for the the collection.
    /// </summary>
    private readonly XElement _element;

    /// <summary>
    /// The type name of the child elements in the container. This is needed as we support containers with different
    /// element types, so we need to know the name of the correct type to return and deserialize.
    /// </summary>
    private readonly XName _typeName = typeof(TElement).L5XType();

    /// <summary>
    /// Creates a empty <see cref="LogixContainer{TElement}"/> with the default type name.
    /// </summary>
    public LogixContainer()
    {
        _element = new XElement($"{typeof(TElement).L5XType()}s");
    }

    /// <summary>
    /// Creates a empty <see cref="LogixContainer{TElement}"/> with the specified type name.
    /// </summary>
    /// <param name="name">The name of the container element.</param>
    public LogixContainer(XName name)
    {
        _element = new XElement(name);
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TComponent}"/> initialized with the provided <see cref="XElement"/>. 
    /// </summary>
    /// <param name="container">The <see cref="XElement"/> containing a collection of elements.</param>
    /// <exception cref="ArgumentNullException"><c>container</c> is null.</exception>
    public LogixContainer(XElement container)
    {
        _element = container ?? throw new ArgumentNullException(nameof(container));
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TComponent}"/> initialized with the provided collection.
    /// </summary>
    /// <param name="components">The collection of elements to initialize.</param>
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
    /// Accesses a single element at the specified index of the container collection.
    /// </summary>
    /// <param name="index">The zero based index of the element to retrieve.</param>
    /// <returns>The element at the specified position in the source container.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    /// <exception cref="ArgumentNullException"><c>value</c> is null when setting index.</exception>
    public TElement this[int index]
    {
        get => LogixSerializer.Deserialize<TElement>(_element.Elements(_typeName).ElementAt(index));
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value),
                    $"Can not set container element of type {typeof(TElement)} null instance.");
            _element.Elements(_typeName).ElementAt(index).ReplaceWith(value.Serialize());
        }
    }

    /// <summary>
    /// Adds the provided element to the logix container at the end of the collection.
    /// </summary>
    /// <param name="element">The element to add.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public void Add(TElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        var last = _element.Elements(_typeName).LastOrDefault();

        if (last is null)
        {
            _element.Add(element.Serialize());
            return;
        }

        last.AddAfterSelf(element.Serialize());
    }

    /// <summary>
    /// Adds the provided elements to the logix container at the end of the collection.
    /// </summary>
    /// <param name="elements">The collection of elements to add.</param>
    /// <exception cref="ArgumentNullException"><c>elements</c> or any element in <c>elements</c> is null.</exception>
    public void AddRange(IEnumerable<TElement> elements)
    {
        if (elements is null)
            throw new ArgumentNullException(nameof(elements));

        foreach (var element in elements)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            var last = _element.Elements(_typeName).LastOrDefault();

            if (last is null)
            {
                _element.Add(element.Serialize());
                continue;
            }

            last.AddAfterSelf(element.Serialize());
        }
    }

    /// <summary>
    /// Gets the number of elements in the collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of elements in the collection.</returns>
    public int Count() => _element.Elements(_typeName).Count();

    /// <summary>
    /// Inserts the provided element at the specified index of the container collection.
    /// </summary>
    /// <param name="index">The zero based index at which to insert the element.</param>
    /// <param name="element">The element to insert.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    public void Insert(int index, TElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        _element.Elements(_typeName).ElementAt(index).AddBeforeSelf(element.Serialize());
    }

    /// <summary>
    /// Removes all elements in the container collection.
    /// </summary>
    public void RemoveAll() => _element.Elements(_typeName).Remove();

    /// <summary>
    /// Removes all elements that satisfy the provided condition predicate.
    /// </summary>
    /// <param name="condition">The condition for which to remove elements.</param>
    /// <exception cref="ArgumentNullException"><c>condition</c> is null.</exception>
    public void RemoveAll(Func<TElement, bool> condition)
    {
        if (condition is null) throw new ArgumentNullException(nameof(condition));
        _element.Elements(_typeName).Where(e => condition.Invoke(LogixSerializer.Deserialize<TElement>(e))).Remove();
    }

    /// <summary>
    /// Removes a element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    public void RemoveAt(int index)
    {
        _element.Elements(_typeName).ElementAt(index).Remove();
    }

    /// <summary>
    /// Updates all elements in the container by applying the provided update action delegate.
    /// </summary>
    /// <param name="update">A update to apply to each element.</param>
    /// <exception cref="ArgumentNullException"><c>update</c> is null.</exception>
    public void Update(Action<TElement> update)
    {
        if (update is null) throw new ArgumentNullException(nameof(update));

        foreach (var child in _element.Elements(_typeName))
        {
            var element = LogixSerializer.Deserialize<TElement>(child);
            update.Invoke(element);
        }
    }

    /// <summary>
    /// Updates all elements in the container that satisfy the provided condition predicate by applying the provided
    /// update action delegate.
    /// </summary>
    /// <param name="update">A update to apply to each element.</param>
    /// <param name="condition">The condition for which to update elements.</param>
    /// <exception cref="ArgumentNullException"><c>update</c> or <c>condition</c> is null.</exception>
    public void Update(Action<TElement> update, Func<TElement, bool> condition)
    {
        if (update is null) throw new ArgumentNullException(nameof(update));
        if (condition is null) throw new ArgumentNullException(nameof(condition));

        foreach (var child in _element.Elements(_typeName))
        {
            var element = LogixSerializer.Deserialize<TElement>(child);
            if (condition.Invoke(element))
                update.Invoke(element);
        }
    }

    /// <inheritdoc />
    public XElement Serialize() => _element;

    /// <inheritdoc />
    public IEnumerator<TElement> GetEnumerator() =>
        _element.Elements(_typeName).Select(LogixSerializer.Deserialize<TElement>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}