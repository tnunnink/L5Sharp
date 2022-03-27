using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <inheritdoc cref="L5Sharp.Querying.IProgramQuery" />
    internal class ProgramQuery : ComponentQuery<IProgram>, IProgramQuery
    {
        public ProgramQuery(IEnumerable<XElement> elements, IL5XSerializer<IProgram> serializer) 
            : base(elements, serializer)
        {
        }
    }
}