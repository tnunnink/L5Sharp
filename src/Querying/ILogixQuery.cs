using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace L5Sharp.Querying
{
    /// <summary>
    /// Provides the ability to query content of an L5X and return deserialized objects representing
    /// a set of elements that satisfy the specified query. 
    /// </summary>
    /// <typeparam name="TResult">The type of object being queried.</typeparam>
    public interface ILogixQuery<TResult>
    {
        /// <summary>
        /// Returns all elements from the current queried collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing the entire queried collection.
        /// </returns>
        IEnumerable<TResult> Select();
        
        /// <summary>
        /// Determines if the current queried collection contains and elements.
        /// </summary>
        /// <returns>true if the queried collection contains at least on element.</returns>
        bool Any();
        
        /// <summary>
        /// Determines if the current queried collection contains and elements.
        /// </summary>
        /// <returns>true if the queried collection contains at least on element.</returns>
        /// <exception cref="ArgumentNullException">predicate is null.</exception>
        bool Any(Expression<Func<TResult, bool>> predicate);

        /// <summary>
        /// Returns the first element from the queried collection.
        /// </summary>
        /// <returns>The first element of the queried collection.</returns>
        /// <exception cref="InvalidOperationException">The queried collection is empty.</exception>
        TResult First();

        /// <summary>
        /// Returns the first element from the queried collection that satisfies the filter predicate. 
        /// </summary>
        /// <param name="predicate">An expression that defines the criteria for which to filter items.</param>
        /// <returns>The first element of the queried collection that matches the filter predicate.</returns>
        /// <exception cref="ArgumentNullException">predicate is null.</exception>
        /// <exception cref="InvalidOperationException">No element satisfies the condition in predicate.
        /// -or- The queried collection is empty.</exception>
        TResult First(Expression<Func<TResult, bool>> predicate);

        /// <summary>
        /// Returns the first element from the queried collection,
        /// or a default value of the specified type if none are found or the collection is empty. 
        /// </summary>
        /// <returns>The first element of the queried collection if one exists; otherwise, null.</returns>
        TResult? FirstOrDefault();

        /// <summary>
        /// Returns the first element from the queried collection that satisfies the filter predicate,
        /// or a default value of the specified type if none are found or the collection is empty. 
        /// </summary>
        /// <returns>
        /// The first element of the queried collection that matches the filter predicate if one exists; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentNullException">predicate is null.</exception>
        TResult? FirstOrDefault(Expression<Func<TResult, bool>> predicate);

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of the queried collection.  
        /// </summary>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing the specified number or elements from the start of
        /// the queried collection.
        /// </returns>
        IEnumerable<TResult> Take(int count);

        /// <summary>
        /// Returns a collection of elements from the queried collection that satisfy the provided filter predicate.
        /// </summary>
        /// <param name="predicate">The predicate that defines the criteria for which to match elements.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing the elements that match the specified filter predicate.
        /// </returns>
        /// <exception cref="ArgumentNullException">predicate is null.</exception>
        IEnumerable<TResult> Where(Expression<Func<TResult, bool>> predicate);
    }
}