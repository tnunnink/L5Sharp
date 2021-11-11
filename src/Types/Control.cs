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

        public IMember<Dint> LEN = Member.Create<Dint>(nameof(LEN));
        public IMember<Dint> POS = Member.Create<Dint>(nameof(POS));
        public IMember<Bool> EN = Member.Create<Bool>(nameof(EN));
        public IMember<Bool> EU = Member.Create<Bool>(nameof(EU));
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
        public IMember<Bool> EM = Member.Create<Bool>(nameof(EM));
        public IMember<Bool> ER = Member.Create<Bool>(nameof(ER));
        public IMember<Bool> UL = Member.Create<Bool>(nameof(UL));
        public IMember<Bool> IN = Member.Create<Bool>(nameof(IN));
        public IMember<Bool> FD = Member.Create<Bool>(nameof(FD));
    }
}