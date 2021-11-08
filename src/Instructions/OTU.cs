using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class OTU : Instruction, IInstruction<ITagMember<Bool>>
    {
        public OTU() : base(nameof(OTU), "Output Latch", GetMembers())
        {
        }
        
        public NeutralText Of(ITagMember<Bool> dataBit)
        {
            return new NeutralText(this, dataBit.Name);
        }

        public IMember<Bool> DataBit => GetParameter<Bool>(nameof(DataBit));

        private static IEnumerable<IMember<IDataType>> GetMembers()
        {
            return new List<IMember<IDataType>>
            {
                Member.New(nameof(DataBit), new Bool()),
            };
        }
    }
}