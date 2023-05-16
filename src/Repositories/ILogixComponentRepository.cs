using System;
using System.Collections.Generic;

namespace L5Sharp.Repositories;

/// <summary>
/// An interface defining the primary API for working with a collection of components within an L5X file.
/// </summary>
/// <typeparam name="TComponent">The <see cref="ILogixComponent"/> type the collection represents (e.g. DataType, Module, Program).</typeparam>
public interface ILogixComponentRepository<TComponent> : IEnumerable<TComponent> where TComponent : ILogixComponent
{
    /// <summary>
    /// Returns the number of components in the content collection.
    /// </summary>
    int Count { get; }
    
    /// <summary>
    /// Adds the provided component to the content collection.
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
    /// Adds the provided components to the content collection.
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
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    bool Contains(string name);

    /// <summary>
    /// Returns a component with the specified name if found. If not found, return <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the component to find.</param>
    /// <returns>A <see cref="TComponent"/> with the specified name.</returns>
    TComponent? Find(string name);

    /// <summary>
    /// Removes all components from the content collection.
    /// </summary>
    void Remove();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    void Remove(TComponent component);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="components"></param>
    /// <returns></returns>
    void Remove(IEnumerable<TComponent> components);

    /// <summary>
    /// Removes a component with the specified name.
    /// </summary>
    /// <param name="name">The name of the component to remove.</param>
    /// <returns><c>true</c> if the component was found and removed; otherwise, <c>false</c>.</returns>
    void Remove(string name);

    /// <summary>
    /// Removes all components found from the provided list of names.
    /// </summary>
    /// <param name="names">The names of the components to remove.</param>
    void Remove(IEnumerable<string> names);
    
    /// <summary>
    /// Removes all components that satisfy the provided predicate condition.
    /// </summary>
    /// <param name="predicate">The condition on which to remove a component.</param>
    void Remove(Func<TComponent, bool> predicate);

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
    /// <param name="predicate">The filter for which to determine which components to apply <c>config</c> to.</param>
    /// <param name="config">The action delegate to apply to the components.</param>
    void Update(Func<TComponent, bool> predicate, Action<TComponent> config);
}