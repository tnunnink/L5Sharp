using System.Linq;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Instructions
{
    public class XIC : Instruction
    {
        public XIC() : base(LoadElement(nameof(XIC)))
        {
        }

        public static INeutralText<XIC> Of(ITagMember<Bool> dataBit)
        {
            return NeutralText.Create<XIC>(dataBit);
        }

        public IMember Bit => Operands.SingleOrDefault(p => p.Name == nameof(Bit));
    }
}