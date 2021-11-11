using L5Sharp.Core;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    public class Control : Predefined
    {
        public Control() : base(nameof(Control).ToUpper())
        {
            RegisterMemberFields();
        }

        protected override IDataType New()
        {
            return new Control();
        }

        public IMember<Dint> LEN = Member.OfType<Dint>(nameof(LEN));
        public IMember<Dint> POS = Member.OfType<Dint>(nameof(POS));
        public IMember<Bool> EN = Member.OfType<Bool>(nameof(EN));
        public IMember<Bool> EU = Member.OfType<Bool>(nameof(EU));
        public IMember<Bool> DN = Member.OfType<Bool>(nameof(DN));
        public IMember<Bool> EM = Member.OfType<Bool>(nameof(EM));
        public IMember<Bool> ER = Member.OfType<Bool>(nameof(ER));
        public IMember<Bool> UL = Member.OfType<Bool>(nameof(UL));
        public IMember<Bool> IN = Member.OfType<Bool>(nameof(IN));
        public IMember<Bool> FD = Member.OfType<Bool>(nameof(FD));
    }
}