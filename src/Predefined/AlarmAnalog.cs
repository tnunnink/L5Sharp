using L5Sharp.Abstractions;
using L5Sharp.Atomics;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming Logix naming

namespace L5Sharp.Predefined
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class AlarmAnalog : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="AlarmAnalog"/> data type instance.
        /// </summary>
        public AlarmAnalog() : base("ALARM_ANALOG")
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new AlarmAnalog();

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> EnableIn = Member.Create<Bool>(nameof(EnableIn));


        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> In = Member.Create<Real>(nameof(In));

        /// <summary>
        /// Gets the <see cref="InFault"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> InFault = Member.Create<Bool>(nameof(InFault));

        /// <summary>
        /// Gets the <see cref="HHEnabled"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHEnabled = Member.Create<Bool>(nameof(HHEnabled));

        /// <summary>
        /// Gets the <see cref="HEnabled"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HEnabled = Member.Create<Bool>(nameof(HEnabled));

        /// <summary>
        /// Gets the <see cref="LEnabled"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LEnabled = Member.Create<Bool>(nameof(LEnabled));

        /// <summary>
        /// Gets the <see cref="LLEnabled"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLEnabled = Member.Create<Bool>(nameof(LLEnabled));

        /// <summary>
        /// Gets the <see cref="AckRequired"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> AckRequired = Member.Create<Bool>(nameof(AckRequired));

        /// <summary>
        /// Gets the <see cref="ProgAckAll"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ProgAckAll = Member.Create<Bool>(nameof(ProgAckAll));

        /// <summary>
        /// Gets the <see cref="OperAckAll"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> OperAckAll = Member.Create<Bool>(nameof(OperAckAll));

        /// <summary>
        /// Gets the <see cref="HHProgAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHProgAck = Member.Create<Bool>(nameof(HHProgAck));

        /// <summary>
        /// Gets the <see cref="HHOperAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHOperAck = Member.Create<Bool>(nameof(HHOperAck));

        /// <summary>
        /// Gets the <see cref="HProgAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HProgAck = Member.Create<Bool>(nameof(HProgAck));

        /// <summary>
        /// Gets the <see cref="HOperAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HOperAck = Member.Create<Bool>(nameof(HOperAck));

        /// <summary>
        /// Gets the <see cref="LProgAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LProgAck = Member.Create<Bool>(nameof(LProgAck));

        /// <summary>
        /// Gets the <see cref="LOperAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LOperAck = Member.Create<Bool>(nameof(LOperAck));

        /// <summary>
        /// Gets the <see cref="LLProgAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLProgAck = Member.Create<Bool>(nameof(LLProgAck));

        /// <summary>
        /// Gets the <see cref="LLOperAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLOperAck = Member.Create<Bool>(nameof(LLOperAck));

        /// <summary>
        /// Gets the <see cref="ROCPosProgAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosProgAck = Member.Create<Bool>(nameof(ROCPosProgAck));

        /// <summary>
        /// Gets the <see cref="ROCPosOperAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosOperAck = Member.Create<Bool>(nameof(ROCPosOperAck));

        /// <summary>
        /// Gets the <see cref="ROCNegProgAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegProgAck = Member.Create<Bool>(nameof(ROCNegProgAck));

        /// <summary>
        /// Gets the <see cref="ROCNegOperAck"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegOperAck = Member.Create<Bool>(nameof(ROCNegOperAck));

        /// <summary>
        /// Gets the <see cref="ProgSuppress"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ProgSuppress = Member.Create<Bool>(nameof(ProgSuppress));

        /// <summary>
        /// Gets the <see cref="OperSuppress"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> OperSuppress = Member.Create<Bool>(nameof(OperSuppress));

        /// <summary>
        /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ProgUnsuppress = Member.Create<Bool>(nameof(ProgUnsuppress));

        /// <summary>
        /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> OperUnsuppress = Member.Create<Bool>(nameof(OperUnsuppress));

        /// <summary>
        /// Gets the <see cref="HHOperShelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHOperShelve = Member.Create<Bool>(nameof(HHOperShelve));

        /// <summary>
        /// Gets the <see cref="HOperShelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HOperShelve = Member.Create<Bool>(nameof(HOperShelve));

        /// <summary>
        /// Gets the <see cref="LOperShelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LOperShelve = Member.Create<Bool>(nameof(LOperShelve));

        /// <summary>
        /// Gets the <see cref="LLOperShelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLOperShelve = Member.Create<Bool>(nameof(LLOperShelve));

        /// <summary>
        /// Gets the <see cref="ROCPosOperShelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosOperShelve = Member.Create<Bool>(nameof(ROCPosOperShelve));

        /// <summary>
        /// Gets the <see cref="ROCNegOperShelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegOperShelve = Member.Create<Bool>(nameof(ROCNegOperShelve));

        /// <summary>
        /// Gets the <see cref="ProgUnshelveAll"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ProgUnshelveAll = Member.Create<Bool>(nameof(ProgUnshelveAll));

        /// <summary>
        /// Gets the <see cref="HHOperUnshelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHOperUnshelve = Member.Create<Bool>(nameof(HHOperUnshelve));

        /// <summary>
        /// Gets the <see cref="HOperUnshelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HOperUnshelve = Member.Create<Bool>(nameof(HOperUnshelve));

        /// <summary>
        /// Gets the <see cref="LOperUnshelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LOperUnshelve = Member.Create<Bool>(nameof(LOperUnshelve));

        /// <summary>
        /// Gets the <see cref="LLOperUnshelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLOperUnshelve = Member.Create<Bool>(nameof(LLOperUnshelve));

        /// <summary>
        /// Gets the <see cref="ROCPosOperUnshelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosOperUnshelve = Member.Create<Bool>(nameof(ROCPosOperUnshelve));

        /// <summary>
        /// Gets the <see cref="ROCNegOperUnshelve"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegOperUnshelve = Member.Create<Bool>(nameof(ROCNegOperUnshelve));

        /// <summary>
        /// Gets the <see cref="ProgDisable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ProgDisable = Member.Create<Bool>(nameof(ProgDisable));

        /// <summary>
        /// Gets the <see cref="OperDisable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> OperDisable = Member.Create<Bool>(nameof(OperDisable));

        /// <summary>
        /// Gets the <see cref="ProgEnable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ProgEnable = Member.Create<Bool>(nameof(ProgEnable));

        /// <summary>
        /// Gets the <see cref="OperEnable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> OperEnable = Member.Create<Bool>(nameof(OperEnable));

        /// <summary>
        /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> AlarmCountReset = Member.Create<Bool>(nameof(AlarmCountReset));

        /// <summary>
        /// Gets the <see cref="HHMinDurationEnable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHMinDurationEnable = Member.Create<Bool>(nameof(HHMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="HMinDurationEnable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HMinDurationEnable = Member.Create<Bool>(nameof(HMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="LMinDurationEnable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LMinDurationEnable = Member.Create<Bool>(nameof(LMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="LLMinDurationEnable"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLMinDurationEnable = Member.Create<Bool>(nameof(LLMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="HHLimit"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> HHLimit = Member.Create<Real>(nameof(HHLimit));

        /// <summary>
        /// Gets the <see cref="HHSeverity"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> HHSeverity = Member.Create<Dint>(nameof(HHSeverity));

        /// <summary>
        /// Gets the <see cref="HLimit"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> HLimit = Member.Create<Real>(nameof(HLimit));

        /// <summary>
        /// Gets the <see cref="HSeverity"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> HSeverity = Member.Create<Dint>(nameof(HSeverity));

        /// <summary>
        /// Gets the <see cref="LLimit"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> LLimit = Member.Create<Real>(nameof(LLimit));

        /// <summary>
        /// Gets the <see cref="LSeverity"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> LSeverity = Member.Create<Dint>(nameof(LSeverity));

        /// <summary>
        /// Gets the <see cref="LLLimit"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> LLLimit = Member.Create<Real>(nameof(LLLimit));

        /// <summary>
        /// Gets the <see cref="LLSeverity"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> LLSeverity = Member.Create<Dint>(nameof(LLSeverity));

        /// <summary>
        /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> MinDurationPRE = Member.Create<Dint>(nameof(MinDurationPRE));

        /// <summary>
        /// Gets the <see cref="ShelveDuration"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> ShelveDuration = Member.Create<Dint>(nameof(ShelveDuration));

        /// <summary>
        /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> MaxShelveDuration = Member.Create<Dint>(nameof(MaxShelveDuration));

        /// <summary>
        /// Gets the <see cref="Deadband"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> Deadband = Member.Create<Real>(nameof(Deadband));

        /// <summary>
        /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> ROCPosLimit = Member.Create<Real>(nameof(ROCPosLimit));

        /// <summary>
        /// Gets the <see cref="ROCPosSeverity"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> ROCPosSeverity = Member.Create<Dint>(nameof(ROCPosSeverity));

        /// <summary>
        /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> ROCNegLimit = Member.Create<Real>(nameof(ROCNegLimit));

        /// <summary>
        /// Gets the <see cref="ROCNegSeverity"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> ROCNegSeverity = Member.Create<Dint>(nameof(ROCNegSeverity));

        /// <summary>
        /// Gets the <see cref="ROCPeriod"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> ROCPeriod = Member.Create<Real>(nameof(ROCPeriod));

        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> EnableOut = Member.Create<Bool>(nameof(EnableOut));

        /// <summary>
        /// Gets the <see cref="InAlarm"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> InAlarm = Member.Create<Bool>(nameof(InAlarm));

        /// <summary>
        /// Gets the <see cref="AnyInAlarmUnack"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> AnyInAlarmUnack = Member.Create<Bool>(nameof(AnyInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="HHInAlarm"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHInAlarm = Member.Create<Bool>(nameof(HHInAlarm));

        /// <summary>
        /// Gets the <see cref="HInAlarm"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HInAlarm = Member.Create<Bool>(nameof(HInAlarm));

        /// <summary>
        /// Gets the <see cref="LInAlarm"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LInAlarm = Member.Create<Bool>(nameof(LInAlarm));

        /// <summary>
        /// Gets the <see cref="LLInAlarm"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLInAlarm = Member.Create<Bool>(nameof(LLInAlarm));

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarm"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosInAlarm = Member.Create<Bool>(nameof(ROCPosInAlarm));

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarm"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegInAlarm = Member.Create<Bool>(nameof(ROCNegInAlarm));

        /// <summary>
        /// Gets the <see cref="ROC"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Real> ROC = Member.Create<Real>(nameof(ROC));

        /// <summary>
        /// Gets the <see cref="HHAcked"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHAcked = Member.Create<Bool>(nameof(HHAcked));

        /// <summary>
        /// Gets the <see cref="HAcked"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HAcked = Member.Create<Bool>(nameof(HAcked));

        /// <summary>
        /// Gets the <see cref="LAcked"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LAcked = Member.Create<Bool>(nameof(LAcked));

        /// <summary>
        /// Gets the <see cref="LLAcked"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLAcked = Member.Create<Bool>(nameof(LLAcked));

        /// <summary>
        /// Gets the <see cref="ROCPosAcked"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosAcked = Member.Create<Bool>(nameof(ROCPosAcked));

        /// <summary>
        /// Gets the <see cref="ROCNegAcked"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegAcked = Member.Create<Bool>(nameof(ROCNegAcked));

        /// <summary>
        /// Gets the <see cref="HHInAlarmUnack"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHInAlarmUnack = Member.Create<Bool>(nameof(HHInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="HInAlarmUnack"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HInAlarmUnack = Member.Create<Bool>(nameof(HInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="LInAlarmUnack"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LInAlarmUnack = Member.Create<Bool>(nameof(LInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="LLInAlarmUnack"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLInAlarmUnack = Member.Create<Bool>(nameof(LLInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarmUnack"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosInAlarmUnack = Member.Create<Bool>(nameof(ROCPosInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarmUnack"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegInAlarmUnack = Member.Create<Bool>(nameof(ROCNegInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="Suppressed"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> Suppressed = Member.Create<Bool>(nameof(Suppressed));

        /// <summary>
        /// Gets the <see cref="HHShelved"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HHShelved = Member.Create<Bool>(nameof(HHShelved));

        /// <summary>
        /// Gets the <see cref="HShelved"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> HShelved = Member.Create<Bool>(nameof(HShelved));

        /// <summary>
        /// Gets the <see cref="LShelved"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LShelved = Member.Create<Bool>(nameof(LShelved));

        /// <summary>
        /// Gets the <see cref="LLShelved"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> LLShelved = Member.Create<Bool>(nameof(LLShelved));

        /// <summary>
        /// Gets the <see cref="ROCPosShelved"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosShelved = Member.Create<Bool>(nameof(ROCPosShelved));

        /// <summary>
        /// Gets the <see cref="ROCNegShelved"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegShelved = Member.Create<Bool>(nameof(ROCNegShelved));

        /// <summary>
        /// Gets the <see cref="Disabled"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> Disabled = Member.Create<Bool>(nameof(Disabled));

        /// <summary>
        /// Gets the <see cref="Commissioned"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> Commissioned = Member.Create<Bool>(nameof(Commissioned));

        /// <summary>
        /// Gets the <see cref="MinDurationACC"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> MinDurationACC = Member.Create<Dint>(nameof(MinDurationACC));

        /// <summary>
        /// Gets the <see cref="HHInAlarmTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> HHInAlarmTime = Member.Create<Lint>(nameof(HHInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="HHAlarmCount"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> HHAlarmCount = Member.Create<Dint>(nameof(HHAlarmCount));

        /// <summary>
        /// Gets the <see cref="HInAlarmTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> HInAlarmTime = Member.Create<Lint>(nameof(HInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="HAlarmCount"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> HAlarmCount = Member.Create<Dint>(nameof(HAlarmCount));

        /// <summary>
        /// Gets the <see cref="LInAlarmTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> LInAlarmTime = Member.Create<Lint>(nameof(LInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="LAlarmCount"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> LAlarmCount = Member.Create<Dint>(nameof(LAlarmCount));

        /// <summary>
        /// Gets the <see cref="LLInAlarmTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> LLInAlarmTime = Member.Create<Lint>(nameof(LLInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="LLAlarmCount"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> LLAlarmCount = Member.Create<Dint>(nameof(LLAlarmCount));

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarmTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> ROCPosInAlarmTime = Member.Create<Lint>(nameof(ROCPosInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ROCPosAlarmCount"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> ROCPosAlarmCount = Member.Create<Dint>(nameof(ROCPosAlarmCount));

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarmTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> ROCNegInAlarmTime = Member.Create<Lint>(nameof(ROCNegInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ROCNegAlarmCount"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> ROCNegAlarmCount = Member.Create<Dint>(nameof(ROCNegAlarmCount));

        /// <summary>
        /// Gets the <see cref="AckTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> AckTime = Member.Create<Lint>(nameof(AckTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> RetToNormalTime = Member.Create<Lint>(nameof(RetToNormalTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> AlarmCountResetTime = Member.Create<Lint>(nameof(AlarmCountResetTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ShelveTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> ShelveTime = Member.Create<Lint>(nameof(ShelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="UnshelveTime"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Lint> UnshelveTime = Member.Create<Lint>(nameof(UnshelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Dint> Status = Member.Create<Dint>(nameof(Status));

        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> InstructFault = Member.Create<Bool>(nameof(InstructFault));

        /// <summary>
        /// Gets the <see cref="InFaulted"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> InFaulted = Member.Create<Bool>(nameof(InFaulted));

        /// <summary>
        /// Gets the <see cref="SeverityInv"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> SeverityInv = Member.Create<Bool>(nameof(SeverityInv));

        /// <summary>
        /// Gets the <see cref="AlarmLimitsInv"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> AlarmLimitsInv = Member.Create<Bool>(nameof(AlarmLimitsInv));

        /// <summary>
        /// Gets the <see cref="DeadbandInv"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> DeadbandInv = Member.Create<Bool>(nameof(DeadbandInv));

        /// <summary>
        /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPosLimitInv = Member.Create<Bool>(nameof(ROCPosLimitInv));

        /// <summary>
        /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCNegLimitInv = Member.Create<Bool>(nameof(ROCNegLimitInv));

        /// <summary>
        /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="AlarmAnalog"/> data type.
        /// </summary>
        public IMember<Bool> ROCPeriodInv = Member.Create<Bool>(nameof(ROCPeriodInv));
    }
}