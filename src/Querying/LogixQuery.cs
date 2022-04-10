using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <summary>
    /// An abstract implementation of the <see cref="ILogixQuery{TResult}"/> interface, provided the essential default
    /// api for querying a set of L5X elements. 
    /// </summary>
    /// <typeparam name="TResult">The result type of the queries.</typeparam>
    public abstract class LogixQuery<TResult> : ILogixQuery<TResult>
    {
        /// <summary>
        /// Gets the current element source collection instance. 
        /// </summary>
        protected readonly IEnumerable<XElement> Elements;
        
        /// <summary>
        /// Gets the current serializer instance. 
        /// </summary>
        protected readonly IL5XSerializer<TResult> Serializer;

        /// <summary>
        /// Creates a new generic <see cref="LogixQuery{TResult}"/> instance with the provided elements and serializer.
        /// </summary>
        /// <param name="elements">The source collection of elements for which to execute queries over.</param>
        /// <param name="serializer">The serializer instance that can deserialize elements to the specified result type.</param>
        protected LogixQuery(IEnumerable<XElement> elements, IL5XSerializer<TResult> serializer)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <inheritdoc />
        public IEnumerable<TResult> Select() => 
            Elements.Select(e => Serializer.Deserialize(e));

        /// <inheritdoc />
        public bool Any() => Elements.Any();

        /// <inheritdoc />
        public bool Any(Expression<Func<TResult, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            
            var filter = predicate.ToXExpression();
            
            return Elements.Any(filter);
        }

        /// <inheritdoc />
        public TResult First() => Serializer.Deserialize(Elements.First());

        /// <inheritdoc />
        public TResult First(Expression<Func<TResult, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            
            var filter = predicate.ToXExpression();
            
            return Serializer.Deserialize(Elements.First(filter));
        }

        /// <inheritdoc />
        public TResult? FirstOrDefault()
        {
            var result = Elements.FirstOrDefault();
            return result is not null ? Serializer.Deserialize(result) : default;
        }

        /// <inheritdoc />
        public TResult? FirstOrDefault(Expression<Func<TResult, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            
            var filter = predicate.ToXExpression();
            
            var result = Elements.FirstOrDefault(filter);
            
            return result is not null ? Serializer.Deserialize(result) : default;
        }

        /// <inheritdoc />
        public IEnumerable<TResult> Take(int count) => 
            Elements.Take(count).Select(e => Serializer.Deserialize(e));

        /// <inheritdoc />
        public IEnumerable<TResult> Where(Expression<Func<TResult, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            
            var filter = predicate.ToXExpression();
            
            return Elements.Where(filter).Select(e => Serializer.Deserialize(e));
        }
    }
}