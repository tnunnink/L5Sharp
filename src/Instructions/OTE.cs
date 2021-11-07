using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    /// <summary>
    /// Output Energize: Instruction sets of clears the data bit
    /// When the OTE instruction is enabled, the controller sets the data bit. When
    /// the OTE instruction is disabled, the controller clears the data bit.
    /// </summary>
    public class OTE : Instruction
    {
        public OTE() : base(nameof(OTE), "Output Energize", GetOperands())
        {
        }
        
        public static NeutralText Of(ITagMember<Bool> dataBit)
        {
            return new NeutralText(new OTE(), dataBit.Name);    
        }

        public IMember<IDataType> DataBit => Operands.SingleOrDefault(p => p.Name == nameof(DataBit));

        private static IEnumerable<IMember<IDataType>> GetOperands()
        {
            return new List<IMember<IDataType>>
            {
                new Member<IDataType>(nameof(DataBit), new Bool())
            };
        }
    }
}