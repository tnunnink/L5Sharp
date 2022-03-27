using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    internal class InstructionQuery : ComponentQuery<IAddOnInstruction>, IInstructionQuery
    {
        public InstructionQuery(IEnumerable<XElement> elements, IL5XSerializer<IAddOnInstruction> serializer) 
            : base(elements, serializer)
        {
        }
    }
}