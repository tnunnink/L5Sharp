using L5Sharp.Core;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    public class Timer : Predefined
    {
        public Timer() : base(nameof(Timer).ToUpper())
        {
            PRE = Member.OfType<Dint>(nameof(PRE));
            ACC = Member.OfType<Dint>(nameof(ACC));
            EN = Member.OfType<Bool>(nameof(EN));
            TT = Member.OfType<Bool>(nameof(TT));
            DN = Member.OfType<Bool>(nameof(DN));

            RegisterMemberProperties();
        }

        public IMember<Dint> PRE { get; }
        public IMember<Dint> ACC { get; }
        public IMember<Bool> EN { get; }
        public IMember<Bool> TT { get; }
        public IMember<Bool> DN { get; }
    }
}