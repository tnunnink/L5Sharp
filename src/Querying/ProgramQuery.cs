using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <inheritdoc cref="L5Sharp.Querying.IProgramQuery" />
    public class ProgramQuery : ComponentQuery<IProgram>, IProgramQuery
    {
        /// <inheritdoc />
        protected ProgramQuery(IEnumerable<XElement> elements, IL5XSerializer<IProgram> serializer) 
            : base(elements, serializer)
        {
        }
    }
}