using System;
using System.Collections.Generic;

namespace L5Sharp;

/// <summary>
/// An interface defining the primary API for working with a collection of components within an L5X file.
/// </summary>
/// <typeparam name="TComponent">The <see cref="ILogixComponent"/> type the collection represents (e.g. DataType, Tag, etc.).</typeparam>
public interface ILogixComponentCollection<TComponent> : IEnumerable<TComponent> where TComponent : ILogixComponent
{
    /// <summary>
    /// Adds the provided component to the current collection.
    /// </summary>
    /// <param name="component">The <see cref="ILogixComponent"/> to add to the collection.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>component</c> has an invalid logix name.</exception>
    /// <exception cref="InvalidOperationException">A component with the same name already exists in the collection.</exception>
    /// <remarks>
    /// <para>
    /// This method will validate the name (value and uniqueness) of the component within the scope of the collection.
    /// This will prevent and invalid components from being added to the L5X which could cause import errors. Outside
    /// of name validation, nothing else is checked upon adding a component.
    /// </para>
    /// </remarks>
    void Add(TComponent component);
        
    /// <summary>
    /// Adds provided component collection to the current collection.
    /// </summary>
    /// <param name="components">A collection of <see cref="ILogixComponent"/> to add to the collection.</param>
    /// <exception cref="ArgumentNullException"><c>components</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>components</c> has a component with an invalid logix name.</exception>
    /// <exception cref="InvalidOperationException">A component name collision -where the collection already
    /// contains a component with a name from the provided <c>components</c> collection.</exception>
    /// <remarks>
    /// <para>
    /// This method will validate the name (value and uniqueness) of the component within the scope of the collection.
    /// This will prevent and invalid components from being added to the L5X which could cause import errors. Outside
    /// of name validation, nothing else is checked upon adding a component.
    /// </para>
    /// </remarks>
    void Add(IEnumerable<TComponent> components);

    /// <summary>
    /// Removes all components from the current collection.
    /// </summary>
    void Clear();

    /// <summary>
    /// Determines whether a component with the specified name exists in the current collection.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <returns><c>true</c> if the component exists; otherwise, <c>false</c>.</returns>
    bool Contains(string name);

    /// <summary>
    /// Returns the number of components in the current collection.
    /// </summary>
    /// <returns>A <see cref="int"/> representing the number of components found.</returns>
    int Count();

    /// <summary>
    /// Returns the component with the specified name if found. If not found, returns null. 
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <returns>A <see cref="ILogixComponent"/> of the specified type if found; otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// This is similar to <see cref="Get"/>, except that it does not throw an exception if a component
    /// of the specified name does not exist in the collection.
    /// </remarks>
    /// <seealso cref="Get"/>
    TComponent? Find(string name);

    /// <summary>
    /// Returns the component with the specified name.
    /// </summary>
    /// <param name="name">The name of the component to get.</param>
    /// <returns>A <see cref="ILogixComponent"/> of the specified type.</returns>
    /// <exception cref="InvalidOperationException">When component is not found or doesn't exist in the collection.</exception>
    /// <remarks>
    /// This method will throw an exception if the component with the specified name is not found.
    /// To find a single element that you are not sure exists, use <see cref="Find"/> and check that the result is not null.
    /// </remarks>
    /// <seealso cref="Find"/>
    TComponent Get(string name);

    /// <summary>
    /// Gets a collection of components that have a name in the provided list of component names.
    /// </summary>
    /// <param name="names">A collection of names for which to find components.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing components objects with the specified names.</returns>
    IEnumerable<TComponent> In(IEnumerable<string> names);

    /// <summary>
    /// Removes the component with the specified name from the collection.
    /// </summary>
    /// <param name="name">The name of the component to remove.</param>
    /// <returns><c>true</c> if the component was found and removed; otherwise, <c>false</c>.</returns>
    bool Remove(string name);

    /// <summary>
    /// Removes all components found from the provided list of names.
    /// </summary>
    /// <param name="names">The names of the components to remove.</param>
    /// <returns>An <see cref="int"/> representing the number of components that were removed.</returns>
    int Remove(IEnumerable<string> names);

    /// <summary>
    /// Renames a component of a specified name to a newly provided name.
    /// </summary>
    /// <param name="current">The name of the component to rename.</param>
    /// <param name="name">The new name of the component.</param>
    /// <exception cref="ArgumentException">The provided name is not a valid logix component name.</exception>
    void Rename(string current, string name);

    /// <summary>
    /// Adds or replaces the the provided component in the collection.
    /// </summary>
    /// <param name="component">The component to add or replace.</param>
    void Update(TComponent component);

    /// <summary>
    /// Adds or replaces the the provided components in the collection.
    /// </summary>
    /// <param name="components">The components to add or replace.</param>
    void Update(IEnumerable<TComponent> components);

    /// <summary>
    /// Configures a component by applying the provided action delegate to the specified component.
    /// </summary>
    /// <param name="name">The name of the component to apply <c>config</c> to.</param>
    /// <param name="config">The action delegate to apply to the component.</param>
    void Update(string name, Action<TComponent> config);

    /// <summary>
    /// Configures all components by applying the provided action delegate.
    /// </summary>
    /// <param name="config">The action delegate to apply to the components.</param>
    void Update(Action<TComponent> config);

    /// <summary>
    /// Configures all components by applying the provided action delegate to components that pass the provided condition
    /// delegate.
    /// </summary>
    /// <param name="condition">The filter for which to determine which components to apply <c>config</c> to.</param>
    /// <param name="config">The action delegate to apply to the components.</param>
    void Update(Func<TComponent, bool> condition, Action<TComponent> config);
}