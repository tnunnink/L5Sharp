using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <inheritdoc cref="L5Sharp.Querying.IInstructionQuery" />
    public class InstructionQuery : ComponentQuery<IAddOnInstruction>, IInstructionQuery
    {
        /// <inheritdoc />
        protected InstructionQuery(IEnumerable<XElement> elements, IL5XSerializer<IAddOnInstruction> serializer) 
            : base(elements, serializer)
        {
        }
    }
}