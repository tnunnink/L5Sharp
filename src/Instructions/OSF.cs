using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class OSF : Instruction
    {
        public OSF() : base(nameof(OSF), "One Shot Falling", GetOperands())
        {
        }
        
        public static NeutralText Of(ITagMember<Bool> storage, ITagMember<Bool> output)
        {
            return new NeutralText(new OSF(), storage.Name, output.Name);    
        }

        public IMember<IDataType> StorageBit => Operands.SingleOrDefault(p => p.Name == nameof(StorageBit));
        public IMember<IDataType> OutputBit => Operands.SingleOrDefault(p => p.Name == nameof(OutputBit));

        private static IEnumerable<IMember<IDataType>> GetOperands()
        {
            return new List<IMember<IDataType>>
            {
                new Member<IDataType>(nameof(StorageBit), new Bool()),
                new Member<IDataType>(nameof(OutputBit), new Bool())
            };
        }
    }
}