using System.Collections.Generic;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class OTL : Instruction
    {
        public OTL() : base(nameof(OTL), "Output Latch", GetMembers())
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
                Member.Create(nameof(DataBit), new Bool()),
            };
        }
    }
}