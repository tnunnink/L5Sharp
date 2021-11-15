using L5Sharp.Core;
using L5Sharp.Enums;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    public class AlarmDigital : Predefined
    {
        public AlarmDigital() : base("ALARM_DIGITAL")
        {
        }

        protected override IDataType New()
        {
            return new AlarmDigital();
        }

        public IMember<Bool> EnableIn = Member.Create<Bool>(nameof(EnableIn));
        public IMember<Bool> In = Member.Create<Bool>(nameof(In));
        public IMember<Bool> InFault = Member.Create<Bool>(nameof(InFault));
        public IMember<Bool> Condition = Member.Create<Bool>(nameof(Condition));
        public IMember<Bool> AckRequired = Member.Create<Bool>(nameof(AckRequired));
        public IMember<Bool> Latched = Member.Create<Bool>(nameof(Latched));
        public IMember<Bool> ProgAck = Member.Create<Bool>(nameof(ProgAck));
        public IMember<Bool> OperAck = Member.Create<Bool>(nameof(OperAck));
        public IMember<Bool> ProgReset = Member.Create<Bool>(nameof(ProgReset));
        public IMember<Bool> OperReset = Member.Create<Bool>(nameof(OperReset));
        public IMember<Bool> ProgSuppress = Member.Create<Bool>(nameof(ProgSuppress));
        public IMember<Bool> OperSuppress = Member.Create<Bool>(nameof(OperSuppress));
        public IMember<Bool> ProgUnsuppress = Member.Create<Bool>(nameof(ProgUnsuppress));
        public IMember<Bool> OperUnsuppress = Member.Create<Bool>(nameof(OperUnsuppress));
        public IMember<Bool> OperShelve = Member.Create<Bool>(nameof(OperShelve));
        public IMember<Bool> ProgUnshelve = Member.Create<Bool>(nameof(ProgUnshelve));
        public IMember<Bool> OperUnshelve = Member.Create<Bool>(nameof(OperUnshelve));
        public IMember<Bool> ProgDisable = Member.Create<Bool>(nameof(ProgDisable));
        public IMember<Bool> OperDisable = Member.Create<Bool>(nameof(OperDisable));
        public IMember<Bool> ProgEnable = Member.Create<Bool>(nameof(ProgEnable));
        public IMember<Bool> OperEnable = Member.Create<Bool>(nameof(OperEnable));
        public IMember<Bool> AlarmCountReset = Member.Create<Bool>(nameof(AlarmCountReset));
        public IMember<Bool> UseProgTime = Member.Create<Bool>(nameof(UseProgTime));
        public IMember<Lint> ProgTime = Member.Create<Lint>(nameof(ProgTime), radix: Radix.DateTime);
        public IMember<Dint> Severity = Member.Create<Dint>(nameof(Severity));
        public IMember<Dint> MinDurationPRE = Member.Create<Dint>(nameof(MinDurationPRE));
        public IMember<Dint> ShelveDuration = Member.Create<Dint>(nameof(ShelveDuration));
        public IMember<Dint> MaxShelveDuration = Member.Create<Dint>(nameof(MaxShelveDuration));
        public IMember<Bool> EnableOut = Member.Create<Bool>(nameof(EnableOut));
        public IMember<Bool> InAlarm = Member.Create<Bool>(nameof(InAlarm));
        public IMember<Bool> Acked = Member.Create<Bool>(nameof(Acked));
        public IMember<Bool> InAlarmUnack = Member.Create<Bool>(nameof(InAlarmUnack));
        public IMember<Bool> Suppressed = Member.Create<Bool>(nameof(Suppressed));
        public IMember<Bool> Shelved = Member.Create<Bool>(nameof(Shelved));
        public IMember<Bool> Disabled = Member.Create<Bool>(nameof(Disabled));
        public IMember<Bool> Commissioned = Member.Create<Bool>(nameof(Commissioned));
        public IMember<Dint> MinDurationACC = Member.Create<Dint>(nameof(MinDurationACC));
        public IMember<Dint> AlarmCount = Member.Create<Dint>(nameof(AlarmCount));
        public IMember<Lint> InAlarmTime = Member.Create<Lint>(nameof(InAlarmTime), radix: Radix.DateTime);
        public IMember<Lint> AckTime = Member.Create<Lint>(nameof(AckTime), radix: Radix.DateTime);
        public IMember<Lint> RetToNormalTime = Member.Create<Lint>(nameof(RetToNormalTime), radix: Radix.DateTime);
        public IMember<Lint> AlarmCountResetTime = Member.Create<Lint>(nameof(AlarmCountResetTime), radix: Radix.DateTime);
        public IMember<Lint> ShelveTime = Member.Create<Lint>(nameof(ShelveTime), radix: Radix.DateTime);
        public IMember<Lint> UnshelveTime = Member.Create<Lint>(nameof(UnshelveTime), radix: Radix.DateTime);
        public IMember<Dint> Status = Member.Create<Dint>(nameof(Status));
        public IMember<Bool> InstructFault = Member.Create<Bool>(nameof(InstructFault));
        public IMember<Bool> InFaulted = Member.Create<Bool>(nameof(InFaulted));
        public IMember<Bool> SeverityInv = Member.Create<Bool>(nameof(SeverityInv));
    }
}