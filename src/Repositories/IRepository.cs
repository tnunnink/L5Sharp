namespace L5Sharp.Repositories
{
    /// <summary>
    /// A base for implementing write operations over the L5X.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IReadOnlyRepository<T> where T : ILogixComponent
    {
        /// <summary>
        /// Add the provided component to the current context.
        /// </summary>
        /// <param name="component">The component to add to the context.</param>
        void Add(T component);
        
        /// <summary>
        /// Removes the specified component from the current context.
        /// </summary>
        /// <param name="component">The component to remove from the context.</param>
        void Remove(T component);
        
        /// <summary>
        /// Updates a component with the provided component on the context.
        /// </summary>
        /// <param name="component">The component to update on the context.</param>
        void Update(T component);
    }
}