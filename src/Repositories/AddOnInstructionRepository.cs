using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Querying;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    internal class AddOnInstructionRepository : AddOnInstructionQuery, IComponentRepository<IAddOnInstruction>
    {
        public AddOnInstructionRepository(IEnumerable<XElement> elements, IL5XSerializer<IAddOnInstruction> serializer) 
            : base(elements, serializer)
        {
        }

        public void Add(IAddOnInstruction component)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ComponentName name)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IAddOnInstruction component)
        {
            throw new System.NotImplementedException();
        }
    }
}