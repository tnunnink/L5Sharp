using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    internal class AddOnInstructionRepository : ComponentRepository<IAddOnInstruction>
    {
        public AddOnInstructionRepository(IEnumerable<XElement> elements, IL5XSerializer<IAddOnInstruction> serializer) 
            : base(elements, serializer)
        {
        }
    }
}