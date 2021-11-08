using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class XIC : Instruction, IInstruction<ITagMember<Bool>>
    {
        public XIC() : base(nameof(XIC), "Examine If Closed", GetMembers())
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