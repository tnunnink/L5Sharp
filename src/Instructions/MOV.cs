using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Instructions
{
    public class MOV : Instruction
    {
        public MOV() : base(LoadElement(nameof(MOV)))
        {
        }

        public static INeutralText<MOV> Of(ITagMember source, ITagMember destination)
        {
            return NeutralText.Create<MOV>(source, destination);
        }
        
        public static INeutralText<MOV> Of(object value, ITagMember destination)
        {
            return NeutralText.Create<MOV>(value, destination);
        }

        public IMember Source => Operands.SingleOrDefault(o => o.Name == nameof(Source));
        public IMember Destination => Operands.SingleOrDefault(o => o.Name == nameof(Destination));
    }
}