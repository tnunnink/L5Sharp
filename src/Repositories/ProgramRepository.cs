using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    internal class ProgramRepository : ComponentRepository<IProgram>
    {
        public ProgramRepository(IEnumerable<XElement> elements, IL5XSerializer<IProgram> serializer) 
            : base(elements, serializer)
        {
        }
    }
}