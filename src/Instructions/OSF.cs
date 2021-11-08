using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class OSF : Instruction, IInstruction<ITagMember<Bool>, ITagMember<Bool>>
    {
        public OSF() : base(nameof(OSF), "One Shot Falling", GetMembers())
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