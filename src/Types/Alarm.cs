using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming I want to keep the naming consistent with Logix (for now).

namespace L5Sharp.Types
{
    public class Alarm : Predefined
    {
        public Alarm() : base(LoadElement(nameof(Alarm).ToUpper()))
        {
        }
        
        public override TagDataFormat DataFormat => TagDataFormat.Alarm;

        public IMember EnableIn => Members.SingleOrDefault(m => m.Name == nameof(EnableIn));
        public IMember In => Members.SingleOrDefault(m => m.Name == nameof(In));
        public IMember HHLimit => Members.SingleOrDefault(m => m.Name == nameof(HHLimit));
        public IMember HLimit => Members.SingleOrDefault(m => m.Name == nameof(HLimit));
        public IMember LLimit => Members.SingleOrDefault(m => m.Name == nameof(LLimit));
        public IMember LLLimit => Members.SingleOrDefault(m => m.Name == nameof(LLLimit));
        public IMember Deadband => Members.SingleOrDefault(m => m.Name == nameof(Deadband));
        public IMember ROCPosLimit => Members.SingleOrDefault(m => m.Name == nameof(ROCPosLimit));
        public IMember ROCNegLimit => Members.SingleOrDefault(m => m.Name == nameof(ROCNegLimit));
        public IMember ROCPeriod => Members.SingleOrDefault(m => m.Name == nameof(ROCPeriod));
        public IMember EnableOut => Members.SingleOrDefault(m => m.Name == nameof(EnableOut));
        public IMember HHAlarm => Members.SingleOrDefault(m => m.Name == nameof(HHAlarm));
        public IMember HAlarm => Members.SingleOrDefault(m => m.Name == nameof(HAlarm));
        public IMember LAlarm => Members.SingleOrDefault(m => m.Name == nameof(LAlarm));
        public IMember LLAlarm => Members.SingleOrDefault(m => m.Name == nameof(LLAlarm));
        public IMember ROCPosAlarm => Members.SingleOrDefault(m => m.Name == nameof(ROCPosAlarm));
        public IMember ROCNegAlarm => Members.SingleOrDefault(m => m.Name == nameof(ROCNegAlarm));
        public IMember ROC => Members.SingleOrDefault(m => m.Name == nameof(ROC));
        public IMember Status => Members.SingleOrDefault(m => m.Name == nameof(Status));
        public IMember InstructFault => Members.SingleOrDefault(m => m.Name == nameof(InstructFault));
        public IMember DeadbandInv => Members.SingleOrDefault(m => m.Name == nameof(DeadbandInv));
        public IMember ROCPosLimitInv => Members.SingleOrDefault(m => m.Name == nameof(ROCPosLimitInv));
        public IMember ROCNegLimitInv => Members.SingleOrDefault(m => m.Name == nameof(ROCNegLimitInv));
        public IMember ROCPeriodInv => Members.SingleOrDefault(m => m.Name == nameof(ROCPeriodInv));
        
    }
}