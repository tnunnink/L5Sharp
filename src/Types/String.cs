using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Types
{
    public class String : Predefined
    {
        public String() : base(LoadElement(nameof(String).ToUpper()))
        {
                
        }

        public IMember Len => Members.SingleOrDefault(m => m.Name == nameof(Len).ToUpper());
        public IMember Data => Members.SingleOrDefault(m => m.Name == nameof(Data).ToUpper());
    }
}