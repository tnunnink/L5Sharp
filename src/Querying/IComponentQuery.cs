using System;
using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp.Querying
{
    /// <summary>
    /// Provides the ability to query <see cref="ILogixComponent"/> objects in the current L5X context.
    /// </summary>
    /// <typeparam name="TComponent">The type of component the query returns.</typeparam>
    public interface IComponentQuery<TComponent> : ILogixQuery<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// Determines if the specified component name exists in the current queried collection.
        /// </summary>
        /// <param name="name">The name of the component to search for.</param>
        /// <returns>true if the queried collection contains the specified name; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        bool Any(ComponentName name);

        /// <summary>
        /// Returns the first component of the queried collection with the provided name, if one exists.
        /// This method throws an exception when more than one component with the specified name exists.
        /// This should typically no be possible, except for querying the entire context scope (i.e. tags in all programs).
        /// </summary>
        /// <param name="name">The name of the component to get.</param>
        /// <returns>The single component with the specified name if the collection is not empty and one exists;
        /// otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        TComponent? Named(ComponentName name);

        /// <summary>
        /// Returns all components in the queried collection that have one of the provided component names.
        /// </summary>
        /// <param name="names">A collection of names for which to filter the source components.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of components that have a name equal to one of the provided names.</returns>
        /// <exception cref="ArgumentNullException">names is null.</exception>
        IEnumerable<TComponent> Named(ICollection<ComponentName> names);

        /// <summary>
        /// Returns the names of all components in the queried collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing the name for each component in the queried collection.
        /// </returns>
        IEnumerable<ComponentName> Names();
    }
}