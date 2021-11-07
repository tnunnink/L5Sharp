using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class ONS : Instruction
    {
        public ONS() : base(nameof(ONS), "One Shot", GetOperands())
        {
        }
        
        public static NeutralText Of(ITagMember<Bool> storage)
        {
            return new NeutralText(new ONS(), storage.Name);    
        }

        public IMember<IDataType> StorageBit => Operands.SingleOrDefault(p => p.Name == nameof(StorageBit));

        private static IEnumerable<IMember<IDataType>> GetOperands()
        {
            return new List<IMember<IDataType>>
            {
                new Member<IDataType>(nameof(StorageBit), new Bool()),
            };
        }
    }
}