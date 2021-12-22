using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public interface IComponentsList<TComponent> : IComponentCollection<TComponent>
        where TComponent : ILogixComponent
    {
        /// <summary>
        /// Clears all <see cref="TComponent"/> objects from the current collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds the provided <see cref="TComponent"/> to the collection.
        /// </summary>
        /// <param name="component">The <see cref="TComponent"/> to add to the collection.</param>
        void Add(TComponent component);

        /// <summary>
        /// Adds the provided collection of <see cref="TComponent"/> objects to the current collection.
        /// </summary>
        /// <param name="components">The collection of <see cref="TComponent"/> objects to add to the collection.</param>
        void AddRange(IEnumerable<TComponent> components);

        /// <summary>
        /// Updates the provided <see cref="TComponent"/> on the current collection.
        /// </summary>
        /// <remarks>
        /// If the provided <see cref="TComponent"/> does not exist, then it will be added to the collection.
        /// If it does exist, then it will be replaced.
        /// </remarks>
        /// <param name="component">The <see cref="TComponent"/> to update on the collection.</param>
        void Update(TComponent component);

        /// <summary>
        /// Removes a <see cref="TComponent"/> with the provided name from the collection.
        /// </summary>
        /// <param name="name">The name of the component to remove from the collection.</param>
        void Remove(string name);
    }
}