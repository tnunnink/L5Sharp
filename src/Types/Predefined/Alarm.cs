using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types.Atomic;

// ReSharper disable InconsistentNaming I want to keep the naming consistent with Logix (for now).

namespace L5Sharp.Types.Predefined
{
    public class Alarm : ComplexType
    {
        public Alarm() : base(nameof(Alarm).ToUpper())
        {
        }
        
        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New()
        {
            return new Alarm();
        }

        public IMember<Bool> EnableIn = Member.Create<Bool>(nameof(EnableIn));
        public IMember<Real> In = Member.Create<Real>(nameof(In));
        public IMember<Real> HHLimit = Member.Create<Real>(nameof(HHLimit));
        public IMember<Real> HLimit = Member.Create<Real>(nameof(HLimit));
        public IMember<Real> LLimit = Member.Create<Real>(nameof(LLimit));
        public IMember<Real> LLLimit = Member.Create<Real>(nameof(LLLimit));
        public IMember<Real> Deadband = Member.Create<Real>(nameof(Deadband));
        public IMember<Real> ROCPosLimit = Member.Create<Real>(nameof(ROCPosLimit));
        public IMember<Real> ROCNegLimit = Member.Create<Real>(nameof(ROCNegLimit));
        public IMember<Real> ROCPeriod = Member.Create<Real>(nameof(ROCPeriod));
        public IMember<Bool> EnableOut = Member.Create<Bool>(nameof(EnableOut));
        public IMember<Bool> HHAlarm = Member.Create<Bool>(nameof(HHAlarm));
        public IMember<Bool> HAlarm = Member.Create<Bool>(nameof(HAlarm));
        public IMember<Bool> LAlarm = Member.Create<Bool>(nameof(LAlarm));
        public IMember<Bool> LLAlarm = Member.Create<Bool>(nameof(LLAlarm));
        public IMember<Bool> ROCPosAlarm = Member.Create<Bool>(nameof(ROCPosAlarm));
        public IMember<Bool> ROCNegAlarm = Member.Create<Bool>(nameof(ROCNegAlarm));
        public IMember<Real> ROC = Member.Create<Real>(nameof(ROC));
        public IMember<Dint> Status = Member.Create<Dint>(nameof(Status));
        public IMember<Bool> InstructFault = Member.Create<Bool>(nameof(InstructFault));
        public IMember<Bool> DeadbandInv = Member.Create<Bool>(nameof(DeadbandInv));
        public IMember<Bool> ROCPosLimitInv = Member.Create<Bool>(nameof(ROCPosLimitInv));
        public IMember<Bool> ROCNegLimitInv = Member.Create<Bool>(nameof(ROCNegLimitInv));
        public IMember<Bool> ROCPeriodInv = Member.Create<Bool>(nameof(ROCPeriodInv));
    }
}