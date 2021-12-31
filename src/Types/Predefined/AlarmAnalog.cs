using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types.Atomic;

// ReSharper disable InconsistentNaming Logix naming

namespace L5Sharp.Types.Predefined
{
    public class AlarmAnalog : ComplexType
    {
        public AlarmAnalog() : base("ALARM_ANALOG")
        {
        }
        
        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New()
        {
            return new AlarmAnalog();
        }

        public IMember<Bool> EnableIn = Member.Create<Bool>(nameof(EnableIn));
        public IMember<Real> In = Member.Create<Real>(nameof(In));
        public IMember<Bool> InFault = Member.Create<Bool>(nameof(InFault));
        public IMember<Bool> HHEnabled = Member.Create<Bool>(nameof(HHEnabled));
        public IMember<Bool> HEnabled = Member.Create<Bool>(nameof(HEnabled));
        public IMember<Bool> LEnabled = Member.Create<Bool>(nameof(LEnabled));
        public IMember<Bool> LLEnabled = Member.Create<Bool>(nameof(LLEnabled));
        public IMember<Bool> AckRequired = Member.Create<Bool>(nameof(AckRequired));
        public IMember<Bool> ProgAckAll = Member.Create<Bool>(nameof(ProgAckAll));
        public IMember<Bool> OperAckAll = Member.Create<Bool>(nameof(OperAckAll));
        public IMember<Bool> HHProgAck = Member.Create<Bool>(nameof(HHProgAck));
        public IMember<Bool> HHOperAck = Member.Create<Bool>(nameof(HHOperAck));
        public IMember<Bool> HProgAck = Member.Create<Bool>(nameof(HProgAck));
        public IMember<Bool> HOperAck = Member.Create<Bool>(nameof(HOperAck));
        public IMember<Bool> LProgAck = Member.Create<Bool>(nameof(LProgAck));
        public IMember<Bool> LOperAck = Member.Create<Bool>(nameof(LOperAck));
        public IMember<Bool> LLProgAck = Member.Create<Bool>(nameof(LLProgAck));
        public IMember<Bool> LLOperAck = Member.Create<Bool>(nameof(LLOperAck));
        public IMember<Bool> ROCPosProgAck = Member.Create<Bool>(nameof(ROCPosProgAck));
        public IMember<Bool> ROCPosOperAck = Member.Create<Bool>(nameof(ROCPosOperAck));
        public IMember<Bool> ROCNegProgAck = Member.Create<Bool>(nameof(ROCNegProgAck));
        public IMember<Bool> ROCNegOperAck = Member.Create<Bool>(nameof(ROCNegOperAck));
        public IMember<Bool> ProgSuppress = Member.Create<Bool>(nameof(ProgSuppress));
        public IMember<Bool> OperSuppress = Member.Create<Bool>(nameof(OperSuppress));
        public IMember<Bool> ProgUnsuppress = Member.Create<Bool>(nameof(ProgUnsuppress));
        public IMember<Bool> OperUnsuppress = Member.Create<Bool>(nameof(OperUnsuppress));
        public IMember<Bool> HHOperShelve = Member.Create<Bool>(nameof(HHOperShelve));
        public IMember<Bool> HOperShelve = Member.Create<Bool>(nameof(HOperShelve));
        public IMember<Bool> LOperShelve = Member.Create<Bool>(nameof(LOperShelve));
        public IMember<Bool> LLOperShelve = Member.Create<Bool>(nameof(LLOperShelve));
        public IMember<Bool> ROCPosOperShelve = Member.Create<Bool>(nameof(ROCPosOperShelve));
        public IMember<Bool> ROCNegOperShelve = Member.Create<Bool>(nameof(ROCNegOperShelve));
        public IMember<Bool> ProgUnshelveAll = Member.Create<Bool>(nameof(ProgUnshelveAll));
        public IMember<Bool> HHOperUnshelve = Member.Create<Bool>(nameof(HHOperUnshelve));
        public IMember<Bool> HOperUnshelve = Member.Create<Bool>(nameof(HOperUnshelve));
        public IMember<Bool> LOperUnshelve = Member.Create<Bool>(nameof(LOperUnshelve));
        public IMember<Bool> LLOperUnshelve = Member.Create<Bool>(nameof(LLOperUnshelve));
        public IMember<Bool> ROCPosOperUnshelve = Member.Create<Bool>(nameof(ROCPosOperUnshelve));
        public IMember<Bool> ROCNegOperUnshelve = Member.Create<Bool>(nameof(ROCNegOperUnshelve));
        public IMember<Bool> ProgDisable = Member.Create<Bool>(nameof(ProgDisable));
        public IMember<Bool> OperDisable = Member.Create<Bool>(nameof(OperDisable));
        public IMember<Bool> ProgEnable = Member.Create<Bool>(nameof(ProgEnable));
        public IMember<Bool> OperEnable = Member.Create<Bool>(nameof(OperEnable));
        public IMember<Bool> AlarmCountReset = Member.Create<Bool>(nameof(AlarmCountReset));
        public IMember<Bool> HHMinDurationEnable = Member.Create<Bool>(nameof(HHMinDurationEnable));
        public IMember<Bool> HMinDurationEnable = Member.Create<Bool>(nameof(HMinDurationEnable));
        public IMember<Bool> LMinDurationEnable = Member.Create<Bool>(nameof(LMinDurationEnable));
        public IMember<Bool> LLMinDurationEnable = Member.Create<Bool>(nameof(LLMinDurationEnable));
        public IMember<Real> HHLimit = Member.Create<Real>(nameof(HHLimit));
        public IMember<Dint> HHSeverity = Member.Create<Dint>(nameof(HHSeverity));
        public IMember<Real> HLimit = Member.Create<Real>(nameof(HLimit));
        public IMember<Dint> HSeverity = Member.Create<Dint>(nameof(HSeverity));
        public IMember<Real> LLimit = Member.Create<Real>(nameof(LLimit));
        public IMember<Dint> LSeverity = Member.Create<Dint>(nameof(LSeverity));
        public IMember<Real> LLLimit = Member.Create<Real>(nameof(LLLimit));
        public IMember<Dint> LLSeverity = Member.Create<Dint>(nameof(LLSeverity));
        public IMember<Dint> MinDurationPRE = Member.Create<Dint>(nameof(MinDurationPRE));
        public IMember<Dint> ShelveDuration = Member.Create<Dint>(nameof(ShelveDuration));
        public IMember<Dint> MaxShelveDuration = Member.Create<Dint>(nameof(MaxShelveDuration));
        public IMember<Real> Deadband = Member.Create<Real>(nameof(Deadband));
        public IMember<Real> ROCPosLimit = Member.Create<Real>(nameof(ROCPosLimit));
        public IMember<Dint> ROCPosSeverity = Member.Create<Dint>(nameof(ROCPosSeverity));
        public IMember<Real> ROCNegLimit = Member.Create<Real>(nameof(ROCNegLimit));
        public IMember<Dint> ROCNegSeverity = Member.Create<Dint>(nameof(ROCNegSeverity));
        public IMember<Real> ROCPeriod = Member.Create<Real>(nameof(ROCPeriod));
        public IMember<Bool> EnableOut = Member.Create<Bool>(nameof(EnableOut));
        public IMember<Bool> InAlarm = Member.Create<Bool>(nameof(InAlarm));
        public IMember<Bool> AnyInAlarmUnack = Member.Create<Bool>(nameof(AnyInAlarmUnack));
        public IMember<Bool> HHInAlarm = Member.Create<Bool>(nameof(HHInAlarm));
        public IMember<Bool> HInAlarm = Member.Create<Bool>(nameof(HInAlarm));
        public IMember<Bool> LInAlarm = Member.Create<Bool>(nameof(LInAlarm));
        public IMember<Bool> LLInAlarm = Member.Create<Bool>(nameof(LLInAlarm));
        public IMember<Bool> ROCPosInAlarm = Member.Create<Bool>(nameof(ROCPosInAlarm));
        public IMember<Bool> ROCNegInAlarm = Member.Create<Bool>(nameof(ROCNegInAlarm));
        public IMember<Real> ROC = Member.Create<Real>(nameof(ROC));
        public IMember<Bool> HHAcked = Member.Create<Bool>(nameof(HHAcked));
        public IMember<Bool> HAcked = Member.Create<Bool>(nameof(HAcked));
        public IMember<Bool> LAcked = Member.Create<Bool>(nameof(LAcked));
        public IMember<Bool> LLAcked = Member.Create<Bool>(nameof(LLAcked));
        public IMember<Bool> ROCPosAcked = Member.Create<Bool>(nameof(ROCPosAcked));
        public IMember<Bool> ROCNegAcked = Member.Create<Bool>(nameof(ROCNegAcked));
        public IMember<Bool> HHInAlarmUnack = Member.Create<Bool>(nameof(HHInAlarmUnack));
        public IMember<Bool> HInAlarmUnack = Member.Create<Bool>(nameof(HInAlarmUnack));
        public IMember<Bool> LInAlarmUnack = Member.Create<Bool>(nameof(LInAlarmUnack));
        public IMember<Bool> LLInAlarmUnack = Member.Create<Bool>(nameof(LLInAlarmUnack));
        public IMember<Bool> ROCPosInAlarmUnack = Member.Create<Bool>(nameof(ROCPosInAlarmUnack));
        public IMember<Bool> ROCNegInAlarmUnack = Member.Create<Bool>(nameof(ROCNegInAlarmUnack));
        public IMember<Bool> Suppressed = Member.Create<Bool>(nameof(Suppressed));
        public IMember<Bool> HHShelved = Member.Create<Bool>(nameof(HHShelved));
        public IMember<Bool> HShelved = Member.Create<Bool>(nameof(HShelved));
        public IMember<Bool> LShelved = Member.Create<Bool>(nameof(LShelved));
        public IMember<Bool> LLShelved = Member.Create<Bool>(nameof(LLShelved));
        public IMember<Bool> ROCPosShelved = Member.Create<Bool>(nameof(ROCPosShelved));
        public IMember<Bool> ROCNegShelved = Member.Create<Bool>(nameof(ROCNegShelved));
        public IMember<Bool> Disabled = Member.Create<Bool>(nameof(Disabled));
        public IMember<Bool> Commissioned = Member.Create<Bool>(nameof(Commissioned));
        public IMember<Dint> MinDurationACC = Member.Create<Dint>(nameof(MinDurationACC));
        public IMember<Lint> HHInAlarmTime = Member.Create<Lint>(nameof(HHInAlarmTime), radix: Radix.DateTime);
        public IMember<Dint> HHAlarmCount = Member.Create<Dint>(nameof(HHAlarmCount));
        public IMember<Lint> HInAlarmTime = Member.Create<Lint>(nameof(HInAlarmTime), radix: Radix.DateTime);
        public IMember<Dint> HAlarmCount = Member.Create<Dint>(nameof(HAlarmCount));
        public IMember<Lint> LInAlarmTime = Member.Create<Lint>(nameof(LInAlarmTime), radix: Radix.DateTime);
        public IMember<Dint> LAlarmCount = Member.Create<Dint>(nameof(LAlarmCount));
        public IMember<Lint> LLInAlarmTime = Member.Create<Lint>(nameof(LLInAlarmTime), radix: Radix.DateTime);
        public IMember<Dint> LLAlarmCount = Member.Create<Dint>(nameof(LLAlarmCount));
        public IMember<Lint> ROCPosInAlarmTime = Member.Create<Lint>(nameof(ROCPosInAlarmTime), radix: Radix.DateTime);
        public IMember<Dint> ROCPosAlarmCount = Member.Create<Dint>(nameof(ROCPosAlarmCount));
        public IMember<Lint> ROCNegInAlarmTime = Member.Create<Lint>(nameof(ROCNegInAlarmTime), radix: Radix.DateTime);
        public IMember<Dint> ROCNegAlarmCount = Member.Create<Dint>(nameof(ROCNegAlarmCount));
        public IMember<Lint> AckTime = Member.Create<Lint>(nameof(AckTime), radix: Radix.DateTime);
        public IMember<Lint> RetToNormalTime = Member.Create<Lint>(nameof(RetToNormalTime), radix: Radix.DateTime);
        public IMember<Lint> AlarmCountResetTime = Member.Create<Lint>(nameof(AlarmCountResetTime), radix: Radix.DateTime);
        public IMember<Lint> ShelveTime = Member.Create<Lint>(nameof(ShelveTime), radix: Radix.DateTime);
        public IMember<Lint> UnshelveTime = Member.Create<Lint>(nameof(UnshelveTime), radix: Radix.DateTime);
        public IMember<Dint> Status = Member.Create<Dint>(nameof(Status));
        public IMember<Bool> InstructFault = Member.Create<Bool>(nameof(InstructFault));
        public IMember<Bool> InFaulted = Member.Create<Bool>(nameof(InFaulted));
        public IMember<Bool> SeverityInv = Member.Create<Bool>(nameof(SeverityInv));
        public IMember<Bool> AlarmLimitsInv = Member.Create<Bool>(nameof(AlarmLimitsInv));
        public IMember<Bool> DeadbandInv = Member.Create<Bool>(nameof(DeadbandInv));
        public IMember<Bool> ROCPosLimitInv = Member.Create<Bool>(nameof(ROCPosLimitInv));
        public IMember<Bool> ROCNegLimitInv = Member.Create<Bool>(nameof(ROCNegLimitInv));
        public IMember<Bool> ROCPeriodInv = Member.Create<Bool>(nameof(ROCPeriodInv));
    }
}