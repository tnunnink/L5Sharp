using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class OSR : Instruction, IInstruction<ITagMember<Bool>, ITagMember<Bool>>
    {
        public OSR() : base(nameof(OSR), "One Shot Rising", GetMembers())
        {
        }
        
        public NeutralText Of(ITagMember<Bool> storage, ITagMember<Bool> output)
        {
            return new NeutralText(this, storage.Name, output.Name);
        }

        public IMember<Bool> StorageBit => GetParameter<Bool>(nameof(StorageBit));

        public IMember<Bool> OutputBit => GetParameter<Bool>(nameof(OutputBit));

        private static IEnumerable<IMember<IDataType>> GetMembers()
        {
            return new List<IMember<IDataType>>
            {
                Member.New(nameof(StorageBit), new Bool()),
                Member.New(nameof(OutputBit), new Bool()),
            };
        }
    }
}