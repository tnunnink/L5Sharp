using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A generic collection that provides operations over an underlying <see cref="XElement"/> container
/// of <see cref="LogixObject"/> types.
/// </summary>
/// <typeparam name="TObject">The type inheriting <see cref="LogixObject"/>.</typeparam>
/// <remarks>
/// <para>
/// This class represents a wrapper around a L5X element that contains a sequence of child elements.
/// This class exposes collection methods for querying and modifying child elements of the container.
/// Note that a container could potentially contain more than one type of child element, but this container class will
/// only operate over the specified element type which is configured via the type's <see cref="L5XTypeAttribute"/>.
/// </para>
/// <para>
/// The class is designed to only offer very basic operations, allowing it to be applicable to all container type elements;
/// However, the user can extend the API for any container type using extension methods.
/// See <see cref="ContainerExtensions"/> for examples demonstrating extensions for containers of type <see cref="LogixComponent"/>.
/// </para>
/// </remarks>
public sealed class LogixContainer<TObject> : LogixElement, IList<TObject>, ICollection where TObject : LogixObject
{
    /// <summary>
    /// Creates an empty <see cref="LogixContainer{TObject}"/> with the default type name.
    /// </summary>
    public LogixContainer() : base(typeof(TObject).L5XContainer())
    {
        L5XType = typeof(TObject).L5XType();
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TObject}"/> initialized with the provided <see cref="XElement"/>. 
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> containing a collection of child elements.</param>
    /// <exception cref="ArgumentNullException"><c>container</c> is null.</exception>
    public LogixContainer(XElement element) : base(element)
    {
        L5XType = typeof(TObject).L5XType();
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TComponent}"/> initialized with the provided collection.
    /// </summary>
    /// <param name="elements">The collection of elements to initialize.</param>
    public LogixContainer(IEnumerable<TObject> elements) : this()
    {
        if (elements is null)
            throw new ArgumentNullException(nameof(elements));

        foreach (var element in elements)
        {
            if (element is null)
                throw new ArgumentException("Can not initialize LogixContainer with null elements.");

            var xml = element.L5XType == L5XType
                ? element.Serialize()
                : element.Convert<TObject>(L5XType).Serialize();

            Element.Add(xml);
        }
    }

    /// <summary>
    /// The type name of the child elements contained within this <see cref="LogixContainer{TObject}"/> collection. 
    /// </summary>
    public override string L5XType { get; }

    /// <summary>
    /// Gets the number of elements contained in the collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of elements in the collection.</returns>
    public int Count => Element.Elements(L5XType).Count();

    /// <summary>
    /// Accesses a single element at the specified index of the container collection.
    /// </summary>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <returns>The element at the specified position in the source container.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    /// <exception cref="ArgumentNullException"><c>value</c> is null when setting index.</exception>
    public TObject this[int index]
    {
        get => Element.Elements(L5XType).ElementAt(index).Deserialize<TObject>();
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value),
                    $"Can not set container element of type {typeof(TObject)} null instance.");

            var xml = value.L5XType == L5XType
                ? value.Serialize()
                : value.Convert<TObject>(L5XType).Serialize();

            Element.Elements(L5XType).ElementAt(index).ReplaceWith(xml);
        }
    }

    /// <summary>
    /// Adds the provided element to the logix container at the end of the collection.
    /// </summary>
    /// <param name="element">The element to add.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public void Add(TObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        var xml = element.L5XType == L5XType
            ? element.Serialize()
            : element.Convert<TObject>(L5XType).Serialize();

        var last = Element.Elements(L5XType).LastOrDefault();

        if (last is null)
        {
            Element.Add(xml);
            return;
        }

        last.AddAfterSelf(xml);
    }

    /// <summary>
    /// Clears all elements in the container collection.
    /// </summary>
    public void Clear()
    {
        Element.Elements(L5XType).Remove();
    }

    /// <summary>
    /// Determines whether a sequence contains a specified element by using the default equality comparer.
    /// </summary>
    /// <param name="element">The element to locate in the sequence.</param>
    /// <returns><c>true</c> if the source sequence contains an element that has the specified value; otherwise,<c>false</c></returns>
    public bool Contains(TObject element)
    {
        return Element.Elements(L5XType).Select(e => e.Deserialize<TObject>()).Contains(element);
    }

    /// <summary>
    /// Copies the entire <see cref="LogixContainer{TObject}"/> to a compatible one-dimensional array,
    /// starting at the specified index of the target array.
    /// </summary>
    /// <inheritdoc />
    public void CopyTo(TObject[] array, int arrayIndex)
    {
        var list = Element.Elements(L5XType).Select(e => e.Deserialize<TObject>()).ToList();
        list.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Adds the provided elements to the logix container at the end of the collection.
    /// </summary>
    /// <param name="elements">The collection of elements to add.</param>
    /// <exception cref="ArgumentNullException"><c>elements</c> or any element in <c>elements</c> is null.</exception>
    public void AddRange(IEnumerable<TObject> elements)
    {
        if (elements is null)
            throw new ArgumentNullException(nameof(elements));

        foreach (var element in elements)
        {
            if (element is null)
                throw new ArgumentException("Can not add null elements to a LogixContainer.");

            var xml = element.L5XType == L5XType
                ? element.Serialize()
                : element.Convert<TObject>(L5XType).Serialize();

            var last = Element.Elements(L5XType).LastOrDefault();

            if (last is null)
            {
                Element.Add(xml);
                continue;
            }

            last.AddAfterSelf(xml);
        }
    }

    /// <summary>
    /// Determines the index of a specific item in the container collection.
    /// </summary>
    /// <param name="element">The <see cref="LogixObject"/> to locate the index of.</param>
    /// <inheritdoc />
    public int IndexOf(TObject element)
    {
        var index = 0;
        foreach (var item in this)
        {
            if (item.Equals(element))
            {
                return index;
            }

            index++;
        }

        return -1;
    }

    /// <summary>
    /// Inserts the provided element at the specified index of the container collection.
    /// </summary>
    /// <param name="index">The zero-based index at which to insert the element.</param>
    /// <param name="element">The element to insert.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    public void Insert(int index, TObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        var xml = element.L5XType == L5XType
            ? element.Serialize()
            : element.Convert<TObject>(L5XType).Serialize();

        Element.Elements(L5XType).ElementAt(index).AddBeforeSelf(xml);
    }

    /// <summary>
    /// Removes all elements that satisfy the provided condition predicate.
    /// </summary>
    /// <param name="condition">The condition for which to remove elements.</param>
    /// <exception cref="ArgumentNullException"><c>condition</c> is null.</exception>
    public void RemoveAll(Func<TObject, bool> condition)
    {
        if (condition is null) throw new ArgumentNullException(nameof(condition));
        Element.Elements(L5XType).Where(e => condition.Invoke(e.Deserialize<TObject>())).Remove();
    }

    /// <summary>
    /// Removes an element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    public void RemoveAt(int index)
    {
        Element.Elements(L5XType).ElementAt(index).Remove();
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="LogixContainer{TObject}"/>.
    /// </summary>
    /// <param name="element">The <see cref="LogixObject"/> to remove from the collection.</param>
    /// <returns>
    /// <c>true</c> if item was successfully removed from the collection; otherwise, <c>false</c>.
    /// This method also returns <c>false</c> if item is not found in the original collection.
    /// </returns>
    public bool Remove(TObject element)
    {
        foreach (var item in this)
        {
            if (!item.Equals(element)) continue;
            item.Remove();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Updates all elements in the container by applying the provided update action delegate.
    /// </summary>
    /// <param name="update">An update to apply to each element.</param>
    /// <exception cref="ArgumentNullException"><c>update</c> is null.</exception>
    public void Update(Action<TObject> update)
    {
        if (update is null) throw new ArgumentNullException(nameof(update));

        foreach (var child in Element.Elements(L5XType))
        {
            var element = child.Deserialize<TObject>();
            update.Invoke(element);
        }
    }

    /// <summary>
    /// Updates all elements in the container that satisfy the provided condition predicate by applying the provided
    /// update action delegate.
    /// </summary>
    /// <param name="update">An update to apply to each element.</param>
    /// <param name="condition">The condition for which to update elements.</param>
    /// <exception cref="ArgumentNullException"><c>update</c> or <c>condition</c> is null.</exception>
    public void Update(Action<TObject> update, Func<TObject, bool> condition)
    {
        if (update is null) throw new ArgumentNullException(nameof(update));
        if (condition is null) throw new ArgumentNullException(nameof(condition));

        foreach (var child in Element.Elements(L5XType))
        {
            var element = child.Deserialize<TObject>();
            if (condition.Invoke(element))
                update.Invoke(element);
        }
    }

    /// <inheritdoc />
    public IEnumerator<TObject> GetEnumerator() =>
        Element.Elements(L5XType).Select(e => e.Deserialize<TObject>()).GetEnumerator();
    
    void ICollection.CopyTo(Array array, int index)
    {
        var source = Element.Elements(L5XType).Select(e => e.Deserialize<TObject>()).ToArray();
        Array.Copy(source, 0, array, index, source.Length);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    bool ICollection<TObject>.IsReadOnly => false;
    bool ICollection.IsSynchronized => true;
    object ICollection.SyncRoot => Element;
}

/// <summary>
/// Extensions methods to the <see cref="LogixContainer{TElement}"/> class.
/// </summary>
public static class ContainerExtensions
{
    /// <summary>
    /// Determines if a component with the specified name exists in the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <returns><c>true</c> if a component with the specified name exists; otherwise, <c>false</c>.</returns>
    public static bool Contains<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent
    {
        return container.Serialize().Elements().Any(e => e.LogixName() == name);
    }

    /// <summary>
    /// Returns a component with the specified name if it exists in the container, otherwise returns <c>null</c>.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent"/> of the specified type if found; Otherwise, <c>null</c>.</returns>
    public static TComponent? Find<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent
    {
        return container.Serialize().Elements().FirstOrDefault(e => e.LogixName() == name)?.Deserialize<TComponent>();
    }

    /// <summary>
    /// Returns a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent"/> of the specified type.</returns>
    /// <exception cref="InvalidOperationException">No component having <c>name</c> exists in the container.</exception>
    public static TComponent Get<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent
    {
        var element = container.Serialize();
        var component = element.Elements().SingleOrDefault(e => e.LogixName() == name);
        return component is not null
            ? component.Deserialize<TComponent>()
            : throw new InvalidOperationException($"No component with name {name} was found in container.");
    }

    /// <summary>
    /// Removes a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to remove.</param>
    public static void Remove<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent
    {
        container.Serialize().Elements().SingleOrDefault(c => string.Equals(c.LogixName(), name))?.Remove();
    }
}