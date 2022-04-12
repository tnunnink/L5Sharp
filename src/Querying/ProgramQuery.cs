using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IProgram"/> elements
    /// within the L5X context.  
    /// </summary>
    public class ProgramQuery : LogixQuery<IProgram>
    {
        /// <summary>
        /// Creates a new <see cref="ProgramQuery"/> with the provided source element to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public ProgramQuery(IEnumerable<XElement> source) : base(source)
        {
        }
    }
}