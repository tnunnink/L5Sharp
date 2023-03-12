using System;
using System.Collections.Generic;

namespace L5Sharp
{
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
        /// <remarks>
        /// <para>
        /// This method will validate the name and uniqueness of the component within the scope of the collection.
        /// This will prevent and invalid components from being added to the L5X which could cause import errors. Outside
        /// of name validation, nothing else is checked upon adding a component.
        /// </para>
        /// <para>
        /// Further, not all exported L5X files may contains proper "containing" elements. It is dependent on what component
        /// type was exported. Therefore, when adding a component, if the containing element does not exists, this method
        /// will "normalize" the L5X structure by injecting a controller element and child component container elements
        /// into the current structure. It will also preserve any current content by moving it under the controller context
        /// element as well.
        /// </para>
        /// </remarks>
        void Add(TComponent component);

        /// <summary>
        /// Determines whether a component with the specified name exists in the current collection.
        /// </summary>
        /// <param name="name">The name of the component to find.</param>
        /// <returns><c>true</c> if the component exists; otherwise, <c>false</c>.</returns>
        bool Contains(string name);

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
        /// Removes the component with the specified name from the collection.
        /// </summary>
        /// <param name="name">The name of the component to remove.</param>
        /// <returns><c>true</c> if the component was found and removed; otherwise, <c>false</c>.</returns>
        bool Remove(string name);

        /// <summary>
        /// Replaces a component in the collection with the provided component.
        /// </summary>
        /// <param name="component"></param>
        /// <returns><c>true</c> if the component was found and replaced; otherwise, <c>false</c>.</returns>
        bool Replace(TComponent component);

        /// <summary>
        /// Adds, or replaces if found, the the provided component in the collection.
        /// </summary>
        /// <param name="component">The component to add or replace.</param>
        void Upsert(TComponent component);
    }
}