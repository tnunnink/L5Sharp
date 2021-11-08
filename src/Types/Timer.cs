using L5Sharp.Core;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    public class Timer : Predefined
    {
        public Timer() : base(LoadElement(nameof(Timer).ToUpper()))
        {
        }

        public IMember<Dint> PRE => GetMember<Dint>(nameof(PRE));
        public IMember<Dint> ACC => GetMember<Dint>(nameof(ACC));
        public IMember<Bool> EN => GetMember<Bool>(nameof(EN));
        public IMember<Bool> TT => GetMember<Bool>(nameof(TT));
        public IMember<Bool> DN => GetMember<Bool>(nameof(DN));
    }
}