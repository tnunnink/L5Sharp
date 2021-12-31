using System;
using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a collection of <see cref="ILogixComponent"/> items.
    /// </summary>
    /// <typeparam name="TComponent">The <see cref="ILogixComponent"/> type that the collection represents.</typeparam>
    /// <remarks>
    /// The <see cref="IComponentCollection{TComponent}"/> maintains items by the string name of
    /// an <see cref="ILogixComponent"/>.
    /// </remarks>
    public interface IComponentCollection<TComponent> : ICollection<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// Gets the first occurrence in the <see cref="IComponentCollection{TComponent}"/> of a component that matches
        /// the provided predicate condition.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the objects to search for.</param>
        /// <returns>The first component that matches the condition of the specified predicate if found; otherwise; null</returns>
        /// <exception cref="ArgumentNullException">match is null.</exception>
        TComponent? Find(Predicate<TComponent> match);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the objects to search for.</param>
        /// <returns>
        /// All components that match the condition of the specified predicate if any are found;
        /// otherwise, an empty collection
        /// </returns>
        /// <exception cref="ArgumentNullException">match is null.</exception>
        IEnumerable<TComponent> FindAll(Predicate<TComponent> match);

        /// <summary>
        /// Gets a component with the specified name from the <see cref="IComponentCollection{TComponent}"/>
        /// </summary>
        /// <param name="name">The name of the component to get.</param>
        /// <returns>The <see cref="ILogixComponent"/> instance with the specified name if found; otherwise; null</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        TComponent? Get(ComponentName name);

        /// <summary>
        /// Removes a component with the specified name from the <see cref="IComponentCollection{TComponent}"/> if found.
        /// </summary>
        /// <param name="name">The name of the component to remove.</param>
        /// <returns>true if the component is successfully found and removed; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        bool Remove(ComponentName name);

        /// <summary>
        /// Updates or adds the provided component to the <see cref="IComponentCollection{TComponent}"/>
        /// based on its existence in the collection.
        /// </summary>
        /// <param name="component">The component to upsert.</param>
        /// <exception cref="ArgumentNullException">component is null.</exception>
        void Upsert(TComponent component);

        /// <summary>
        /// Updates or adds the provided component collection to the <see cref="IComponentCollection{TComponent}"/>
        /// based on the existence of each component.
        /// </summary>
        /// <param name="components">The collection of components to upsert.</param>
        /// <exception cref="ArgumentNullException">components is null.</exception>
        void Upsert(IEnumerable<TComponent> components);
    }
}