using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    public class TagQuery : LogixQuery<ITag<IDataType>>, ITagQuery
    {
        public TagQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <inheritdoc />
        public ITagQuery WithDataType(string typeName)
        {
            var tags = this.Where(t => string.Equals(t.Attribute(L5XAttribute.DataType.ToString())?.Value,
                typeName, StringComparison.OrdinalIgnoreCase));

            return new TagQuery(tags);
        }
    }
}