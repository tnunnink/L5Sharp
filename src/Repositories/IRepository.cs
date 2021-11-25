using L5Sharp.Core;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A base for implementing write operations over the L5X.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public interface IRepository<TComponent> : IReadOnlyRepository<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// Add the provided component to the current context.
        /// </summary>
        /// <param name="component">The component to add to the context.</param>
        void Add(TComponent component);

        /// <summary>
        /// Updates a component with the provided component on the context.
        /// </summary>
        /// <param name="component">The component to update on the context.</param>
        void Update(TComponent component);

        /// <summary>
        /// Removes the specified component from the current context.
        /// </summary>
        /// <param name="name">The name of the component to remove from the context.</param>
        void Remove(ComponentName name);
    }
}