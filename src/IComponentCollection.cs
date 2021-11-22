using System;
using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// A collection of objets that implement <see cref="ILogixComponent"/>
    /// </summary>
    /// <remarks>
    /// A <c>ComponentCollection</c> represents a collection of Logix components.
    /// Logix components are items of an L5X that can be identified by their name (i.e. Tag, DataType, etc.).
    /// <c>ComponentCollection</c> provides a reusable default implementation for maintaining components by key (name). 
    /// </remarks>
    /// <typeparam name="TComponent">The component type that this collection represents</typeparam>
    /// /// <footer>
    /// <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a>
    /// </footer>
    public interface IComponentCollection<TComponent> : IEnumerable<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// Gets the number of components currently in the collection.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Determines if the provided name is contained in the current collection.
        /// </summary>
        bool Contains(string name);
        
        /// <summary>
        /// Determines if the collection contains an item that satisfies the provided delegate predicate.
        /// </summary>
        bool Contains(Func<TComponent, bool> predicate);
        
        /// <summary>
        /// Gets a component instance with the provided name.
        /// </summary>
        TComponent Get(string name);
        
        /// <summary>
        /// Gets the first component instance that satisfies the provided predicate delegate. 
        /// </summary>
        TComponent Get(Func<TComponent, bool> predicate);
        
        /// <summary>
        /// Gets all component instances that satisfy the provided predicate delegate. 
        /// </summary>
        IEnumerable<TComponent> Find(Func<TComponent, bool> predicate);
        
        /// <summary>
        /// Returns the component collection in the preserved order that it is configured. 
        /// </summary>
        IEnumerable<TComponent> Ordered();
        
        /// <summary>
        /// Gets the integer index of the given component.
        /// </summary>
        /// <param name="component">The component for which to find the index of.</param>
        /// <returns>
        /// The integer number representing the index of the specified component.
        /// -1 if the component does not exist in the collection
        /// </returns>
        int IndexOf(TComponent component);
        
        /// <summary>
        /// Adds the provided component to the collection.
        /// </summary>
        /// <param name="component">The component to add to the collection.</param>
        void Add(TComponent component);
        
        /// <summary>
        /// Adds the provided collection of components to the current collection.
        /// </summary>
        /// <param name="components">The collection of components to add to the collection.</param>
        void AddRange(IEnumerable<TComponent> components);
        
        /// <summary>
        /// Inserts the provided component into the collection at the specified index.
        /// </summary>
        /// <param name="index">The integer index to add the provided component at.</param>
        /// <param name="component">The component to add to the collection.</param>
        void Insert(int index, TComponent component);
        
        /// <summary>
        /// Updates the specified component if it exists, adds the component to the collection if is does not.
        /// </summary>
        /// <param name="component">The component to update on the collection.</param>
        void Update(TComponent component);
        
        /// <summary>
        /// Removes the component with the provided name from the collection.
        /// </summary>
        /// <param name="name">The name of the component to remove from the collection.</param>
        void Remove(string name);
    }
}