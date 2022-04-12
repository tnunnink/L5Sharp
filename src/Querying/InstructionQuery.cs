using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    internal class InstructionQuery : LogixQuery<IAddOnInstruction>, IInstructionQuery
    {
        public InstructionQuery(IEnumerable<XElement> source) : base(source)
        {
        }
    }
}