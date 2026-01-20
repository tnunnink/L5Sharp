using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a strongly typed collection of <typeparamref name="TElement"/>
/// where <typeparamref name="TElement"/> is a type derived from <see cref="LogixElement"/>.
/// This class provides functionality for managing, querying, and modifying a collection of Logix elements.
/// </summary>
/// <typeparam name="TElement">The type of elements in the container, constrained to <see cref="LogixElement"/>.</typeparam>
public sealed class LogixContainer<TElement> : LogixElement, IList<TElement>, ICollection where TElement : LogixElement
{
    /// <summary>
    /// Creates a new container initialized with the provided <see cref="XElement"/>. 
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> containing a collection of child elements.</param>
    /// <exception cref="ArgumentNullException"><c>container</c> is null.</exception>
    public LogixContainer(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates an empty container. 
    /// </summary>
    /// <remarks>
    /// The underlying XElement name will be determined by pluralizing the registered name for the generic type parameter.
    /// If you need to override this functionality, use the XElement constructor and provide a custom XElement instance.
    /// </remarks>
    public LogixContainer() : base($"{LogixSerializer.NamesFor(typeof(TElement)).First()}s")
    {
    }

    /// <summary>
    /// Creates a new container initialized with the provided collection.
    /// </summary>
    /// <param name="elements">The collection of elements to initialize.</param>
    public LogixContainer(IEnumerable<TElement> elements) : this()
    {
        if (elements is null)
            throw new ArgumentNullException(nameof(elements));

        Element.Add(elements.Select(e => e.Serialize()));
    }

    /// <summary>
    /// Gets the number of elements contained in the collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of elements in the collection.</returns>
    public int Count => Element.Elements().Count();

    /// <summary>
    /// Accesses a single element at the specified index of the container collection.
    /// </summary>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <returns>The element at the specified position in the source container.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than
    /// or equal to the number of elements in the collection.
    /// </exception>
    public TElement this[int index]
    {
        get => Element.Elements().ElementAt(index).Deserialize<TElement>();
        set => Element.Elements().ElementAt(index).ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Adds the provided element to the logix container at the end of the collection.
    /// </summary>
    /// <param name="element">The element to add.</param>
    /// <remarks>If the provided element is null, nothing is added to the container.</remarks>
    public void Add(TElement? element)
    {
        Element.Add(element?.Serialize());
    }

    /// <summary>
    /// Adds the provided elements to the logix container at the end of the collection.
    /// </summary>
    /// <param name="elements">The collection of elements to add.</param>
    /// <exception cref="ArgumentNullException"><paramref name="elements"/> is null.</exception>
    public void AddRange(IEnumerable<TElement?> elements)
    {
        Element.Add(elements.Select(e => e?.Serialize()));
    }

    /// <summary>
    /// Clears all elements in the container collection.
    /// </summary>
    public void Clear()
    {
        Element.RemoveNodes();
    }

    /// <summary>
    /// Determines whether a sequence contains a specified element by using the default equality comparer.
    /// </summary>
    /// <param name="element">The element to locate in the sequence.</param>
    /// <returns><c>true</c> if the source sequence contains an element that has the specified value; otherwise,<c>false</c></returns>
    public bool Contains(TElement element)
    {
        return Element.Elements().Contains(element.Serialize());
    }

    /// <summary>
    /// Copies the entire <see cref="LogixContainer{TElement}"/> to a compatible one-dimensional array,
    /// starting at the specified index of the target array.
    /// </summary>
    /// <inheritdoc />
    public void CopyTo(TElement[] array, int arrayIndex)
    {
        var list = Element.Elements().Select(e => e.Deserialize<TElement>()).ToList();
        list.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Determines the index of a specific element in the container.
    /// </summary>
    /// <param name="element">The element to locate in the container.</param>
    /// <returns>The zero-based index of the element in the container if found; otherwise, -1.</returns>
    public int IndexOf(TElement element)
    {
        var index = 0;

        foreach (var child in Element.Elements())
        {
            if (child.Equals(element.Serialize()))
                return index;

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
    public void Insert(int index, TElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        Element.Elements().ElementAt(index).AddBeforeSelf(element.Serialize());
    }

    /// <summary>
    /// Removes the first occurrence of a specific element from the container.
    /// </summary>
    /// <param name="element">The element to remove from the container.</param>
    /// <returns>
    /// <c>true</c> if the element was successfully removed from the container; otherwise, <c>false</c>.
    /// </returns>
    public bool Remove(TElement element)
    {
        var xml = element.Serialize();

        foreach (var child in Element.Elements())
        {
            if (!child.Equals(xml)) continue;
            xml.Remove();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes an element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than
    /// or equal to the number of elements in the collection.
    /// </exception>
    public void RemoveAt(int index)
    {
        Element.Elements().ElementAt(index).Remove();
    }

    /// <summary>
    /// Removes all elements that satisfy the provided condition predicate.
    /// </summary>
    /// <param name="condition">The condition for which to remove elements.</param>
    /// <exception cref="ArgumentNullException"><paramref name="condition"/> is null.</exception>
    public void RemoveIf(Func<TElement, bool> condition)
    {
        if (condition is null)
            throw new ArgumentNullException(nameof(condition));

        Element.Elements().Where(e => condition.Invoke(e.Deserialize<TElement>())).Remove();
    }

    /// <summary>
    /// Updates all elements in the container by applying the provided update action delegate.
    /// </summary>
    /// <param name="update">An update to apply to each element.</param>
    /// <exception cref="ArgumentNullException"><paramref name="update"/> is null.</exception>
    public void Update(Action<TElement> update)
    {
        if (update is null)
            throw new ArgumentNullException(nameof(update));

        foreach (var child in Element.Elements())
        {
            var element = child.Deserialize<TElement>();
            update.Invoke(element);
        }
    }

    /// <summary>
    /// Updates all elements in the container that satisfy the provided condition predicate by applying the provided
    /// update action delegate.
    /// </summary>
    /// <param name="update">An update action to apply to each element.</param>
    /// <param name="condition">The condition predicate for which elements must satisfy to apply the provided update.</param>
    /// <exception cref="ArgumentNullException"><paramref name="update"/> or <paramref name="condition"/> is null.</exception>
    public void Update(Action<TElement> update, Func<TElement, bool> condition)
    {
        if (update is null) throw new ArgumentNullException(nameof(update));
        if (condition is null) throw new ArgumentNullException(nameof(condition));

        foreach (var child in Element.Elements())
        {
            var element = child.Deserialize<TElement>();

            if (condition.Invoke(element))
            {
                update.Invoke(element);
            }
        }
    }

    /// <inheritdoc />
    public IEnumerator<TElement> GetEnumerator()
    {
        return Element.Elements().Select(e => e.Deserialize<TElement>()).GetEnumerator();
    }

    void ICollection.CopyTo(Array array, int index)
    {
        var source = Element.Elements().Select(e => e.Deserialize<TElement>()).ToArray();
        Array.Copy(source, 0, array, index, source.Length);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    bool ICollection<TElement>.IsReadOnly => false;
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
        where TComponent : LogixComponent<TComponent>
    {
        return container.Serialize().Elements().Any(e => e.LogixName() == name);
    }

    /// <summary>
    /// Returns a component with the specified name if it exists in the container, otherwise returns <c>null</c>.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent{TComponent}"/> of the specified type if found; Otherwise, <c>null</c>.</returns>
    public static TComponent? Find<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        return container.Serialize().Elements().FirstOrDefault(e => e.LogixName() == name)?.Deserialize<TComponent>();
    }

    /// <summary>
    /// Returns a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent{TComponent}"/> of the specified type.</returns>
    /// <exception cref="InvalidOperationException">No component having <c>name</c> exists in the container.</exception>
    public static TComponent Get<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        var component = container.Serialize().Elements().FirstOrDefault(e => e.LogixName() == name);

        if (component is null)
            throw new InvalidOperationException($"No component with name {name} was found in container.");

        return component.Deserialize<TComponent>();
    }

    /// <summary>
    /// Attempts to retrieve a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The <see cref="LogixContainer{TComponent}"/> to search within.</param>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="result">When this method returns, contains the retrieved component if found; otherwise, the default value of <typeparamref name="TComponent"/>.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>True if a component with the specified name is found; otherwise, false.</returns>
    public static bool TryGet<TComponent>(this LogixContainer<TComponent> container, string name, out TComponent result)
        where TComponent : LogixComponent<TComponent>
    {
        var component = container.Serialize().Elements().FirstOrDefault(e => e.LogixName() == name);

        if (component is null)
        {
            result = null!;
            return false;
        }

        result = component.Deserialize<TComponent>();
        return true;
    }

    /// <summary>
    /// Removes a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to remove.</param>
    public static bool Remove<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        var component = container.Serialize().Elements().FirstOrDefault(c => c.LogixName().IsEquivalent(name));

        if (component is null)
        {
            return false;
        }

        component.Remove();
        return true;
    }
}