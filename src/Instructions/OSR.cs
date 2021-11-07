using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class OSR : Instruction
    {
        public OSR() : base(nameof(OSR), "One Shot Rising", GetOperands())
        {
        }
        
        public static NeutralText Of(ITagMember<Bool> storage, ITagMember<Bool> output)
        {
            return new NeutralText(new OSR(), storage.Name, output.Name);    
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