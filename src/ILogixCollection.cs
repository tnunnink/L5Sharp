using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A simple enumerable interface for interacting with and updating <see cref="ILogixComponent"/> objects in a collection.
/// </summary>
/// <typeparam name="TComponent">The component type of the collection.</typeparam>
public interface ILogixCollection<TComponent> : IEnumerable<TComponent>
    where TComponent : ILogixComponent, ILogixSerializable
{
    /// <summary>
    /// 
    /// </summary>
    XContainer Container { get; }

    /// <summary>
    /// Accesses a single component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the component to retrieve.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    TComponent this[int index] { get; set; }

    /// <summary>
    /// Accesses a single component with the specified name.
    /// </summary>
    /// <param name="name">The name of the component to retrieve.</param>
    TComponent this[string name] { get; set; }

    /// <summary>
    /// Adds the provided component to the collection.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    void Add(TComponent component);

    /// <summary>
    /// Adds the provided components to the collection.
    /// </summary>
    /// <param name="components">The collection of components to add.</param>
    void AddMany(IEnumerable<TComponent> components);

    /// <summary>
    /// Removes all components in the collection.
    /// </summary>
    void Clear();

    /// <summary>
    /// Determines if a component with the specified name exists in the collection.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    /// <returns><c>true</c> if a component with the specified name exists; otherwise, <c>false</c>.</returns>
    bool Contains(string name);

    /// <summary>
    /// Gets the number of components in the collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of components in the collection.</returns>
    int Count();

    /// <summary>
    /// Returns a component with the specified name if found in the collection. If not found, returns <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <returns>If found, the component instance with the specified name; otherwise, <c>null</c>.</returns>
    TComponent? Find(string name);

    /// <summary>
    /// Inserts the provided component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index at which to insert the component.</param>
    /// <param name="component">The component to insert.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    void Insert(int index, TComponent component);

    /// <summary>
    /// Removes a component at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero based index of the component to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero or greater than or equal to the
    /// number of components in the collection.</exception>
    void Remove(int index);

    /// <summary>
    /// Removes a component with the specified name from the collection.
    /// </summary>
    /// <param name="name">The name of the component to remove.</param>
    void Remove(string name);

    /// <summary>
    /// Removes all components that satisfy the provided condition predicate.
    /// </summary>
    /// <param name="condition">The condition for which to remove components.</param>
    /// <exception cref="ArgumentNullException"><c>condition</c> is null.</exception>
    void Remove(Func<TComponent, bool> condition);

    void Update(Action<TComponent> update);

    void Update(Func<TComponent, bool> condition, Action<TComponent> update);
}