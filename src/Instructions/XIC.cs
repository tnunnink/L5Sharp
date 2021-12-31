using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class XIC : Instruction
    {
        public XIC() : base(nameof(XIC))
        {
        }

        /*public NeutralText Of(ITagMember<Bool> dataBit)
        {
            return new NeutralText(this, dataBit.Name);
        }*/

        //public IMember<Bool> DataBit => GetParameter<Bool>(nameof(DataBit));

        /*private static IEnumerable<IMember<IDataType>> GetMembers()
        {
            return new List<IMember<IDataType>>
            {
                Member.Create(nameof(DataBit), new Bool()),
            };
        }*/
    }
}