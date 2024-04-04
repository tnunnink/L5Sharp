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
/// The class is designed to only offer very basic operations, allowing it to be applicable to all container type elements,
/// However, the user can extended the API for any container type using extension methods.
/// See <see cref="ContainerExtensions"/> for examples demonstrating extensions for containers of type <see cref="LogixComponent"/>.
/// </para>
/// </remarks>
public class LogixContainer<TObject> : LogixElement, IEnumerable<TObject> where TObject : LogixObject
{
    /// <summary>
    /// The type name of the child elements in the container. This is needed as we support containers with different
    /// element types, so we need to know the name of the correct type to return and deserialize.
    /// This also allows types with secondary element names to be synced/converted as they are added to the container. 
    /// </summary>
    private readonly string _type;

    /// <summary>
    /// Creates a empty <see cref="LogixContainer{TObject}"/> with the default type name.
    /// </summary>
    public LogixContainer() : base(typeof(TObject).L5XContainer())
    {
        _type = typeof(TObject).L5XType();
    }

    /// <summary>
    /// Creates a new <see cref="LogixContainer{TObject}"/> initialized with the provided <see cref="XElement"/>. 
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> containing a collection of child elements.</param>
    /// <exception cref="ArgumentNullException"><c>container</c> is null.</exception>
    public LogixContainer(XElement element) : base(element)
    {
        _type = typeof(TObject).L5XType();
    }
    
    /// <summary>
    /// This is exclusively used by the <c>AddOnInstruction</c> component. Would be nice to figure something else out but
    /// not sure what would be less of a workaround at this time.
    /// </summary>
    internal LogixContainer(string containerName) : base(containerName)
    {
        _type = containerName.TrimEnd('s');
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

            var xml = element.L5XType == _type
                ? element.Serialize()
                : element.Convert<TObject>(_type).Serialize();

            Element.Add(xml);
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
    public TObject this[int index]
    {
        get => Element.Elements(_type).ElementAt(index).Deserialize<TObject>();
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value),
                    $"Can not set container element of type {typeof(TObject)} null instance.");

            var xml = value.L5XType == _type
                ? value.Serialize()
                : value.Convert<TObject>(_type).Serialize();

            Element.Elements(_type).ElementAt(index).ReplaceWith(xml);
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

        var xml = element.L5XType == _type
            ? element.Serialize()
            : element.Convert<TObject>(_type).Serialize();

        var last = Element.Elements(_type).LastOrDefault();

        if (last is null)
        {
            Element.Add(xml);
            return;
        }

        last.AddAfterSelf(xml);
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

            var xml = element.L5XType == _type
                ? element.Serialize()
                : element.Convert<TObject>(_type).Serialize();

            var last = Element.Elements(_type).LastOrDefault();

            if (last is null)
            {
                Element.Add(xml);
                continue;
            }

            last.AddAfterSelf(xml);
        }
    }

    /// <summary>
    /// Gets the number of elements in the collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of elements in the collection.</returns>
    public int Count() => Element.Elements(_type).Count();

    /// <summary>
    /// Inserts the provided element at the specified index of the container collection.
    /// </summary>
    /// <param name="index">The zero based index at which to insert the element.</param>
    /// <param name="element">The element to insert.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    public void Insert(int index, TObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        var xml = element.L5XType == _type
            ? element.Serialize()
            : element.Convert<TObject>(_type).Serialize();

        Element.Elements(_type).ElementAt(index).AddBeforeSelf(xml);
    }

    /// <summary>
    /// Removes all elements in the container collection.
    /// </summary>
    public void RemoveAll() => Element.Elements(_type).Remove();

    /// <summary>
    /// Removes all elements that satisfy the provided condition predicate.
    /// </summary>
    /// <param name="condition">The condition for which to remove elements.</param>
    /// <exception cref="ArgumentNullException"><c>condition</c> is null.</exception>
    public void RemoveAll(Func<TObject, bool> condition)
    {
        if (condition is null) throw new ArgumentNullException(nameof(condition));
        Element.Elements(_type).Where(e => condition.Invoke(e.Deserialize<TObject>())).Remove();
    }

    /// <summary>
    /// Removes a element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of elements in the collection.</exception>
    public void RemoveAt(int index)
    {
        Element.Elements(_type).ElementAt(index).Remove();
    }

    /// <summary>
    /// Updates all elements in the container by applying the provided update action delegate.
    /// </summary>
    /// <param name="update">A update to apply to each element.</param>
    /// <exception cref="ArgumentNullException"><c>update</c> is null.</exception>
    public void Update(Action<TObject> update)
    {
        if (update is null) throw new ArgumentNullException(nameof(update));

        foreach (var child in Element.Elements(_type))
        {
            var element = child.Deserialize<TObject>();
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
    public void Update(Action<TObject> update, Func<TObject, bool> condition)
    {
        if (update is null) throw new ArgumentNullException(nameof(update));
        if (condition is null) throw new ArgumentNullException(nameof(condition));

        foreach (var child in Element.Elements(_type))
        {
            var element = child.Deserialize<TObject>();
            if (condition.Invoke(element))
                update.Invoke(element);
        }
    }

    /// <inheritdoc />
    public IEnumerator<TObject> GetEnumerator() =>
        Element.Elements(_type).Select(e => e.Deserialize<TObject>()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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