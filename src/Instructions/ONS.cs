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

        public IMember<Bool> StorageBit => GetParameter<Bool>(nameof(StorageBit));

        private static IEnumerable<IMember<IDataType>> GetOperands()
        {
            return new List<IMember<IDataType>>
            {
                Member.New(nameof(StorageBit), new Bool())
            };
        }
    }
}