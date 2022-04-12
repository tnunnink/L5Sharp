using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp
{
    /// <summary>
    /// Provides the ability to specify and execute a query over a set of <see cref="XElement"/> objects in order to
    /// return a filtered deserialized set of Logix elements.
    /// </summary>
    /// <typeparam name="TResult">The type of Logix element being queried and materialized.</typeparam>
    public interface ILogixQuery<TResult> : IEnumerable<XElement>
    {
        /// <summary>
        /// Executes the <see cref="ILogixQuery{TResult}"/> and returns the filtered collection of resulting objects.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TResult> Execute(IL5XSerializer<TResult> serializer);
    }
}