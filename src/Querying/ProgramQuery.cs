using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    public class ProgramQuery : ComponentQuery<IProgram>, IProgramQuery
    {
        protected ProgramQuery(IEnumerable<XElement> elements, IL5XSerializer<IProgram> serializer) 
            : base(elements, serializer)
        {
        }
    }
}