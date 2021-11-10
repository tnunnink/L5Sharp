using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming I want to keep the naming consistent with Logix (for now).

namespace L5Sharp.Types
{
    public class Alarm : Predefined
    {
        public Alarm() : base(nameof(Alarm).ToUpper())
        {
            RegisterMemberProperties();
        }
        
        public override string Description => $"RSLogix {Name} DataType";
        public override TagDataFormat DataFormat => TagDataFormat.Alarm;
        
        protected override IDataType New()
        {
            return new Alarm();
        }

        public IMember<Bool> EnableIn => Member.OfType<Bool>(nameof(EnableIn));
        public IMember<Real> In => Member.OfType<Real>(nameof(In));
        public IMember<Real> HHLimit => Member.OfType<Real>(nameof(HHLimit));
        public IMember<Real> HLimit => Member.OfType<Real>(nameof(HLimit));
        public IMember<Real> LLimit => Member.OfType<Real>(nameof(LLimit));
        public IMember<Real> LLLimit => Member.OfType<Real>(nameof(LLLimit));
        public IMember<Real> Deadband => Member.OfType<Real>(nameof(Deadband));
        public IMember<Real> ROCPosLimit => Member.OfType<Real>(nameof(ROCPosLimit));
        public IMember<Real> ROCNegLimit => Member.OfType<Real>(nameof(ROCNegLimit));
        public IMember<Real> ROCPeriod => Member.OfType<Real>(nameof(ROCPeriod));
        public IMember<Bool> EnableOut => Member.OfType<Bool>(nameof(EnableOut));
        public IMember<Bool> HHAlarm => Member.OfType<Bool>(nameof(HHAlarm));
        public IMember<Bool> HAlarm => Member.OfType<Bool>(nameof(HAlarm));
        public IMember<Bool> LAlarm => Member.OfType<Bool>(nameof(LAlarm));
        public IMember<Bool> LLAlarm => Member.OfType<Bool>(nameof(LLAlarm));
        public IMember<Bool> ROCPosAlarm => Member.OfType<Bool>(nameof(ROCPosAlarm));
        public IMember<Bool> ROCNegAlarm => Member.OfType<Bool>(nameof(ROCNegAlarm));
        public IMember<Real> ROC => Member.OfType<Real>(nameof(ROC));
        public IMember<Dint> Status => Member.OfType<Dint>(nameof(Status));
        public IMember<Bool> InstructFault => Member.OfType<Bool>(nameof(InstructFault));
        public IMember<Bool> DeadbandInv => Member.OfType<Bool>(nameof(DeadbandInv));
        public IMember<Bool> ROCPosLimitInv => Member.OfType<Bool>(nameof(ROCPosLimitInv));
        public IMember<Bool> ROCNegLimitInv => Member.OfType<Bool>(nameof(ROCNegLimitInv));
        public IMember<Bool> ROCPeriodInv => Member.OfType<Bool>(nameof(ROCPeriodInv));
    }
}