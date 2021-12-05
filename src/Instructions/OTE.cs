using System.Collections.Generic;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    /// <summary>
    /// Output Energize: Instruction sets of clears the data bit
    /// When the OTE instruction is enabled, the controller sets the data bit. When
    /// the OTE instruction is disabled, the controller clears the data bit.
    /// </summary>
    public class OTE : Instruction, IInstruction<ITagMember<Bool>>
    {
        public OTE() : base(nameof(OTE), "Output Energize", GetMembers())
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