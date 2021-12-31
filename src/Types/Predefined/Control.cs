using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types.Atomic;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types.Predefined
{
    public class Control : ComplexType
    {
        public Control() : base(nameof(Control).ToUpper())
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
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