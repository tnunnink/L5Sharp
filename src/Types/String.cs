using L5Sharp.Core;

namespace L5Sharp.Types
{
    public class String : Predefined
    {
        public String() : base(LoadElement(nameof(String).ToUpper()))
        {
                
        }

        public Dint Len => GetMember(nameof(Len)).DataType is Dint d ? d : default;
        public Dint Data => GetMember(nameof(Data)).DataType is Dint d ? d : default;
    }
}