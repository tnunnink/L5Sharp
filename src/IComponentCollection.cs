using System;
using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// A collection of objets that implement <see cref="ILogixComponent"/>.
    /// </summary>
    /// <remarks>
    /// A <c>ComponentCollection</c> represents a collection of Logix components.
    /// Logix components are items of an L5X that can be identified by their name (i.e. Tag, DataType, etc.).
    /// <c>IComponentCollection</c> provides a reusable default implementation for maintaining components by key (name). 
    /// </remarks>
    /// <typeparam name="TComponent">The component type that this collection represents</typeparam>
    /// /// <footer>
    /// <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a>
    /// </footer>
    public interface IComponentCollection<out TComponent> : IEnumerable<TComponent> 
        where TComponent : ILogixComponent
    {
        /// <summary>
        /// Gets the number of components currently in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines if the provided name is contained in the current collection.
        /// </summary>
        /// <param name="name">The name of the component to search for.</param>
        /// <returns>true if the collection contains a component with the provided name; otherwise, false.</returns>
        bool Contains(string name);

        /// <summary>
        /// Gets a component instance with the provided name.
        /// </summary>
        /// <param name="name">The name of the component to get.</param>
        /// <returns>
        /// If a component with the specified name is found in the collection,
        /// the <see cref="TComponent"/> instance with the specified name; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentException">name is null or empty.</exception>
        TComponent? Get(string name);
    }
}