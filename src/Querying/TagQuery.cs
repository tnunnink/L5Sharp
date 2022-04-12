using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="ITag{TDataType}"/>
    /// elements within the L5X context.  
    /// </summary>
    public class TagQuery : LogixQuery<ITag<IDataType>>
    {
        /// <summary>
        /// Creates a new <see cref="TagQuery"/> with the provided source element to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public TagQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <summary>
        /// Filters the collection to include only tag components with the specified data type name.
        /// </summary>
        /// <param name="typeName">The name of the tag component data type.</param>
        /// <returns>A new <see cref="TaskQuery"/> with the filtered element collection.</returns>
        public TagQuery WithDataType(string typeName)
        {
            var tags = this.Where(t => string.Equals(t.Attribute(L5XAttribute.DataType.ToString())?.Value,
                typeName, StringComparison.OrdinalIgnoreCase));

            return new TagQuery(tags);
        }
    }
}