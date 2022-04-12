using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <summary>
    /// An abstract base class for all <see cref="ILogixQuery{TResult}"/> object to inherit. This abstract class
    /// provided the implementation of the <see cref="Execute"/> method that is used by the <see cref="ILogixContext"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of Logix element being queried and materialized.</typeparam>
    public abstract class LogixQuery<TResult> : ILogixQuery<TResult>
    {
        private readonly IEnumerable<XElement> _source;

        /// <summary>
        /// Creates a new <see cref="LogixQuery{TResult}"/> instance with the provided source elements.
        /// </summary>
        /// <param name="source">the collection of elements that represent the source of the query object.</param>
        protected LogixQuery(IEnumerable<XElement> source)
        {
            _source = source;
        }

        /// <inheritdoc />
        public IEnumerable<TResult> Execute(IL5XSerializer<TResult> serializer) => 
            _source.Select(serializer.Deserialize);

        /// <inheritdoc />
        public IEnumerator<XElement> GetEnumerator() => _source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}