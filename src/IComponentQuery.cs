using System;
using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Provides the ability to manipulate <see cref="ILogixComponent"/> objects in the current L5X context.
    /// </summary>
    /// <typeparam name="TComponent">The type of component the query returns.</typeparam>
    public interface IComponentQuery<out TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// Returns all components from the context collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all components found in the context.</returns>
        IEnumerable<TComponent> All();

        /// <summary>
        /// Determines if the context collection contains any components.
        /// </summary>
        /// <returns>true if the context contains at least one component; otherwise, false.</returns>
        bool Any();

        /// <summary>
        /// Determines if the context collection contains any components with the provided component name.
        /// </summary>
        /// <param name="name">The name of the component to search for.</param>
        /// <returns>true if the context contains the specified name; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        bool Any(string name);

        /// <summary>
        /// Returns the number of components in the context collection.
        /// </summary>
        /// <returns>An integer number representing the number of components.</returns>
        int Count();

        /// <summary>
        /// Returns the first component in the context collection with the provided name, if one exists.
        /// </summary>
        /// <param name="name">The name of the component to find.</param>
        /// <returns>The first component with the specified name if the context is not empty and one exists;
        /// otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        TComponent? Find(string name);

        /// <summary>
        /// Returns all components in the context collection that have one of the provided component names.
        /// </summary>
        /// <param name="names">A collection of names for which to filter the source components.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of components that have a name equal to one of the provided names.</returns>
        /// <exception cref="ArgumentNullException">names is null.</exception>
        IEnumerable<TComponent> Find(IEnumerable<string> names);

        /// <summary>
        /// Returns the first component from the context collection.
        /// </summary>
        /// <returns>The first component of the context.</returns>
        /// <exception cref="InvalidOperationException">The queried collection is empty.</exception>
        TComponent First();

        /// <summary>
        /// Returns the first component from the context collection,
        /// or a default value of the specified type if none are found or the context is empty. 
        /// </summary>
        /// <returns>The first component of the context collection if one exists; otherwise, null.</returns>
        TComponent? FirstOrDefault();

        /// <summary>
        /// Returns the single component in the context collection with the provided name.
        /// This call will throw an exception if the provided name is not found.
        /// </summary>
        /// <param name="name">The name of the component to get.</param>
        /// <returns>The single component of the context collection with the provided name.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        /// <exception cref="InvalidOperationException">A component with the specified name does not exits.</exception>
        /// <remarks>
        /// This call gets the single component with the provided name, and therefore will fail if there are more than
        /// one component with the provided name. However, since component names should be unique,
        /// this should not happen. If the component name does not exist, this call will throw an exception. To get
        /// components where you are unsure if the name exists, use <see cref="Find(string)"/>. 
        /// </remarks>
        TComponent Get(string name);

        /// <summary>
        /// Returns the names of all components in the context collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing the name for each component in the context collection.
        /// </returns>
        IEnumerable<ComponentName> Names();

        /// <summary>
        /// Returns a specified number of contiguous components from the start of the context collection.  
        /// </summary>
        /// <param name="count">The number of components to return.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing the specified number or components from the start of
        /// the context collection.
        /// </returns>
        IEnumerable<TComponent> Take(int count);
    }
}