using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class String : Predefined
    {
        public String() : base(LoadElement(nameof(String).ToUpper()))
        {
                
        }

        public IMember Len => Members.SingleOrDefault(m => m.Name == nameof(Len).ToUpper());
        public IMember Data => Members.SingleOrDefault(m => m.Name == nameof(Data).ToUpper());
        public override object Default => string.Empty;

        public override object ParseValue(string value)
        {
            return value;
        }

        public override bool IsValidValue(object value)
        {
            return value is string;
        }
    }
}