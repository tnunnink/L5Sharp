using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Types
{
    public sealed class Counter : Predefined
    {
        public Counter() : base(LoadElement(nameof(Counter).ToUpper()))
        {
                
        }

        public IMember Pre => Members.SingleOrDefault(m => m.Name == nameof(Pre).ToUpper());
        public IMember Acc => Members.SingleOrDefault(m => m.Name == nameof(Acc).ToUpper());
        public IMember Cu => Members.SingleOrDefault(m => m.Name == nameof(Cu).ToUpper());
        public IMember Cd => Members.SingleOrDefault(m => m.Name == nameof(Cd).ToUpper());
        public IMember Dn => Members.SingleOrDefault(m => m.Name == nameof(Dn).ToUpper());
        public IMember Ov => Members.SingleOrDefault(m => m.Name == nameof(Ov).ToUpper());
        public IMember Un => Members.SingleOrDefault(m => m.Name == nameof(Un).ToUpper());
    }
}