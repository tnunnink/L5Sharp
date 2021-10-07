using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class Timer : Predefined
    {
        public Timer() : base(LoadElement(nameof(Timer).ToUpper()))
        {
                
        }

        public IMember Pre => Members.SingleOrDefault(m => m.Name == nameof(Pre).ToUpper());
        public IMember Acc => Members.SingleOrDefault(m => m.Name == nameof(Acc).ToUpper());
        public IMember En => Members.SingleOrDefault(m => m.Name == nameof(En).ToUpper());
        public IMember Tt => Members.SingleOrDefault(m => m.Name == nameof(Tt).ToUpper());
        public IMember Dn => Members.SingleOrDefault(m => m.Name == nameof(Dn).ToUpper());
    }
}