using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    public class AddOnInstructionQuery : ComponentQuery<IAddOnInstruction>, IAddOnInstrcutionQuery
    {
        protected AddOnInstructionQuery(IEnumerable<XElement> elements, IL5XSerializer<IAddOnInstruction> serializer) 
            : base(elements, serializer)
        {
        }
    }
}