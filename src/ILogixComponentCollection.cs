using System;
using System.Collections.Generic;

namespace L5Sharp;

/// <summary>
/// An interface defining the primary API for working with a collection of components within an L5X file.
/// </summary>
/// <typeparam name="TComponent"></typeparam>
public interface ILogixComponentCollection<TComponent> : IEnumerable<TComponent> where TComponent : ILogixComponent
{
    /// <summary>
    /// Adds the provided component to the current collection.
    /// </summary>
    /// <param name="component">The <see cref="ILogixComponent"/> to add to the collection.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>component</c> has an invalid logix name.</exception>
    /// <exception cref="InvalidOperationException">A component with the same name as <c>component</c> already exists in the collection.</exception>
    /// <remarks>
    /// <para>
    /// This method will validate the name and uniqueness of the component within the scope of the collection.
    /// This will prevent and invalid components from being added to the L5X which could cause import errors. Outside
    /// of name validation, nothing else is checked upon adding a component.
    /// </para>
    /// <para>
    /// Further, not all exported L5X files may contain proper "containing" elements. It is dependent on what component
    /// type was exported. Therefore, when adding a component, if the containing element does not exists, this method
    /// will "normalize" the L5X structure by injecting a controller element and child component container elements
    /// into the current structure. It will also preserve any current content by moving it under the controller context
    /// element that is created.
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
    /// This method will validate the name and uniqueness of each component within the scope of the collection.
    /// This will prevent and invalid components from being added to the L5X which could cause import errors. Outside
    /// of name validation, nothing else is checked upon adding a component.
    /// </para>
    /// <para>
    /// Further, not all exported L5X files may contain proper "containing" elements. It is dependent on what component
    /// type was exported. Therefore, when adding a component, if the containing element does not exists, this method
    /// will "normalize" the L5X structure by injecting a controller element and child component container elements
    /// into the current structure. It will also preserve any current content by moving it under the controller context
    /// element that is created.
    /// </para>
    /// </remarks>
    void Add(IEnumerable<TComponent> components);

    /// <summary>
    /// Removes all components from the current collections.
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
    /// Imports a collection of component into the current collection by add or overwriting existing components if
    /// specified.
    /// </summary>
    /// <param name="components">The collection of components to import.</param>
    /// <param name="overwrite">Whether to overwrite any existing components if found.</param>
    void Import(IEnumerable<TComponent> components, bool overwrite);

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
    void Rename(string current, string name);

    /// <summary>
    /// Replaces a component in the collection with the provided component if found.
    /// </summary>
    /// <param name="component">The component to replace.</param>
    /// <returns><c>true</c> if the component was found and replaced; otherwise, <c>false</c>.</returns>
    bool Replace(TComponent component);
        
    /// <summary>
    /// Replaces all components in the collection with the provided collection of component where found.
    /// </summary>
    /// <param name="components">The collection of components to replace.</param>
    /// <returns>An <see cref="int"/> representing the number of components replaced.</returns>
    int Replace(IEnumerable<TComponent> components);

    /// <summary>
    /// Adds or replaces if found the the provided component in the collection.
    /// </summary>
    /// <param name="component">The component to add or replace.</param>
    void Update(TComponent component);
        
    /// <summary>
    /// Adds or replaces if found the the provided components in the collection.
    /// </summary>
    /// <param name="components">The components to add or replace.</param>
    void Update(IEnumerable<TComponent> components);
}