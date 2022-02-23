using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using L5Sharp.Core;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A base for implementing read operations over the L5X.
    /// </summary>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    public interface IReadOnlyRepository<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// Determines if the current component with the provided name is contained within the current context.
        /// </summary>
        /// <param name="name">The name of the component to search.</param>
        /// <returns>true if the current context contains the component with the specified name. Otherwise, false.</returns>
        bool Contains(ComponentName name);

        /// <summary>
        /// Gets the first component that satisfies the provided expression delegate. 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TComponent? Find(Expression<Func<TComponent, bool>> predicate);

        /// <summary>
        /// Gets the first component that satisfies the provided expression delegate. 
        /// </summary>
        /// <param name="name">The name of the component to find.</param>
        /// <returns></returns>
        TComponent? Find(ComponentName name);

        /// <summary>
        /// Finds all components that satisfy the provided expression delegate. 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TComponent> FindAll(Expression<Func<TComponent, bool>> predicate);

        /// <summary>
        /// Gets a component instance with the provided name in the current context.
        /// </summary>
        /// <param name="name">The name of the component to get.</param>
        /// <returns>An instance of the component with the specified name if found. Otherwise, null.</returns>
        TComponent Get(ComponentName name);

        /// <summary>
        /// Gets all component instances for the given type in the current context.
        /// </summary>
        /// <returns>A collection of all components of the given type.</returns>
        IEnumerable<TComponent> GetAll();
    }
}