using L5Sharp.Abstractions;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming Logix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class ALARM_ANALOG : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="ALARM_ANALOG"/> data type instance.
        /// </summary>
        public ALARM_ANALOG() : base(nameof(ALARM_ANALOG))
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new ALARM_ANALOG();

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> EnableIn = Member.Create<BOOL>(nameof(EnableIn));


        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> In = Member.Create<REAL>(nameof(In));

        /// <summary>
        /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> InFault = Member.Create<BOOL>(nameof(InFault));

        /// <summary>
        /// Gets the <see cref="HHEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHEnabled = Member.Create<BOOL>(nameof(HHEnabled));

        /// <summary>
        /// Gets the <see cref="HEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HEnabled = Member.Create<BOOL>(nameof(HEnabled));

        /// <summary>
        /// Gets the <see cref="LEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LEnabled = Member.Create<BOOL>(nameof(LEnabled));

        /// <summary>
        /// Gets the <see cref="LLEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLEnabled = Member.Create<BOOL>(nameof(LLEnabled));

        /// <summary>
        /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> AckRequired = Member.Create<BOOL>(nameof(AckRequired));

        /// <summary>
        /// Gets the <see cref="ProgAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ProgAckAll = Member.Create<BOOL>(nameof(ProgAckAll));

        /// <summary>
        /// Gets the <see cref="OperAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> OperAckAll = Member.Create<BOOL>(nameof(OperAckAll));

        /// <summary>
        /// Gets the <see cref="HHProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHProgAck = Member.Create<BOOL>(nameof(HHProgAck));

        /// <summary>
        /// Gets the <see cref="HHOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHOperAck = Member.Create<BOOL>(nameof(HHOperAck));

        /// <summary>
        /// Gets the <see cref="HProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HProgAck = Member.Create<BOOL>(nameof(HProgAck));

        /// <summary>
        /// Gets the <see cref="HOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HOperAck = Member.Create<BOOL>(nameof(HOperAck));

        /// <summary>
        /// Gets the <see cref="LProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LProgAck = Member.Create<BOOL>(nameof(LProgAck));

        /// <summary>
        /// Gets the <see cref="LOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LOperAck = Member.Create<BOOL>(nameof(LOperAck));

        /// <summary>
        /// Gets the <see cref="LLProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLProgAck = Member.Create<BOOL>(nameof(LLProgAck));

        /// <summary>
        /// Gets the <see cref="LLOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLOperAck = Member.Create<BOOL>(nameof(LLOperAck));

        /// <summary>
        /// Gets the <see cref="ROCPosProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosProgAck = Member.Create<BOOL>(nameof(ROCPosProgAck));

        /// <summary>
        /// Gets the <see cref="ROCPosOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosOperAck = Member.Create<BOOL>(nameof(ROCPosOperAck));

        /// <summary>
        /// Gets the <see cref="ROCNegProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegProgAck = Member.Create<BOOL>(nameof(ROCNegProgAck));

        /// <summary>
        /// Gets the <see cref="ROCNegOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegOperAck = Member.Create<BOOL>(nameof(ROCNegOperAck));

        /// <summary>
        /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ProgSuppress = Member.Create<BOOL>(nameof(ProgSuppress));

        /// <summary>
        /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> OperSuppress = Member.Create<BOOL>(nameof(OperSuppress));

        /// <summary>
        /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ProgUnsuppress = Member.Create<BOOL>(nameof(ProgUnsuppress));

        /// <summary>
        /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> OperUnsuppress = Member.Create<BOOL>(nameof(OperUnsuppress));

        /// <summary>
        /// Gets the <see cref="HHOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHOperShelve = Member.Create<BOOL>(nameof(HHOperShelve));

        /// <summary>
        /// Gets the <see cref="HOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HOperShelve = Member.Create<BOOL>(nameof(HOperShelve));

        /// <summary>
        /// Gets the <see cref="LOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LOperShelve = Member.Create<BOOL>(nameof(LOperShelve));

        /// <summary>
        /// Gets the <see cref="LLOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLOperShelve = Member.Create<BOOL>(nameof(LLOperShelve));

        /// <summary>
        /// Gets the <see cref="ROCPosOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosOperShelve = Member.Create<BOOL>(nameof(ROCPosOperShelve));

        /// <summary>
        /// Gets the <see cref="ROCNegOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegOperShelve = Member.Create<BOOL>(nameof(ROCNegOperShelve));

        /// <summary>
        /// Gets the <see cref="ProgUnshelveAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ProgUnshelveAll = Member.Create<BOOL>(nameof(ProgUnshelveAll));

        /// <summary>
        /// Gets the <see cref="HHOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHOperUnshelve = Member.Create<BOOL>(nameof(HHOperUnshelve));

        /// <summary>
        /// Gets the <see cref="HOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HOperUnshelve = Member.Create<BOOL>(nameof(HOperUnshelve));

        /// <summary>
        /// Gets the <see cref="LOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LOperUnshelve = Member.Create<BOOL>(nameof(LOperUnshelve));

        /// <summary>
        /// Gets the <see cref="LLOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLOperUnshelve = Member.Create<BOOL>(nameof(LLOperUnshelve));

        /// <summary>
        /// Gets the <see cref="ROCPosOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosOperUnshelve = Member.Create<BOOL>(nameof(ROCPosOperUnshelve));

        /// <summary>
        /// Gets the <see cref="ROCNegOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegOperUnshelve = Member.Create<BOOL>(nameof(ROCNegOperUnshelve));

        /// <summary>
        /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ProgDisable = Member.Create<BOOL>(nameof(ProgDisable));

        /// <summary>
        /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> OperDisable = Member.Create<BOOL>(nameof(OperDisable));

        /// <summary>
        /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ProgEnable = Member.Create<BOOL>(nameof(ProgEnable));

        /// <summary>
        /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> OperEnable = Member.Create<BOOL>(nameof(OperEnable));

        /// <summary>
        /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> AlarmCountReset = Member.Create<BOOL>(nameof(AlarmCountReset));

        /// <summary>
        /// Gets the <see cref="HHMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHMinDurationEnable = Member.Create<BOOL>(nameof(HHMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="HMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HMinDurationEnable = Member.Create<BOOL>(nameof(HMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="LMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LMinDurationEnable = Member.Create<BOOL>(nameof(LMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="LLMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLMinDurationEnable = Member.Create<BOOL>(nameof(LLMinDurationEnable));

        /// <summary>
        /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> HHLimit = Member.Create<REAL>(nameof(HHLimit));

        /// <summary>
        /// Gets the <see cref="HHSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> HHSeverity = Member.Create<DINT>(nameof(HHSeverity));

        /// <summary>
        /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> HLimit = Member.Create<REAL>(nameof(HLimit));

        /// <summary>
        /// Gets the <see cref="HSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> HSeverity = Member.Create<DINT>(nameof(HSeverity));

        /// <summary>
        /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> LLimit = Member.Create<REAL>(nameof(LLimit));

        /// <summary>
        /// Gets the <see cref="LSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> LSeverity = Member.Create<DINT>(nameof(LSeverity));

        /// <summary>
        /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> LLLimit = Member.Create<REAL>(nameof(LLLimit));

        /// <summary>
        /// Gets the <see cref="LLSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> LLSeverity = Member.Create<DINT>(nameof(LLSeverity));

        /// <summary>
        /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> MinDurationPRE = Member.Create<DINT>(nameof(MinDurationPRE));

        /// <summary>
        /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> ShelveDuration = Member.Create<DINT>(nameof(ShelveDuration));

        /// <summary>
        /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> MaxShelveDuration = Member.Create<DINT>(nameof(MaxShelveDuration));

        /// <summary>
        /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> Deadband = Member.Create<REAL>(nameof(Deadband));

        /// <summary>
        /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> ROCPosLimit = Member.Create<REAL>(nameof(ROCPosLimit));

        /// <summary>
        /// Gets the <see cref="ROCPosSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> ROCPosSeverity = Member.Create<DINT>(nameof(ROCPosSeverity));

        /// <summary>
        /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> ROCNegLimit = Member.Create<REAL>(nameof(ROCNegLimit));

        /// <summary>
        /// Gets the <see cref="ROCNegSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> ROCNegSeverity = Member.Create<DINT>(nameof(ROCNegSeverity));

        /// <summary>
        /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> ROCPeriod = Member.Create<REAL>(nameof(ROCPeriod));

        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> EnableOut = Member.Create<BOOL>(nameof(EnableOut));

        /// <summary>
        /// Gets the <see cref="InAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> InAlarm = Member.Create<BOOL>(nameof(InAlarm));

        /// <summary>
        /// Gets the <see cref="AnyInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> AnyInAlarmUnack = Member.Create<BOOL>(nameof(AnyInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="HHInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHInAlarm = Member.Create<BOOL>(nameof(HHInAlarm));

        /// <summary>
        /// Gets the <see cref="HInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HInAlarm = Member.Create<BOOL>(nameof(HInAlarm));

        /// <summary>
        /// Gets the <see cref="LInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LInAlarm = Member.Create<BOOL>(nameof(LInAlarm));

        /// <summary>
        /// Gets the <see cref="LLInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLInAlarm = Member.Create<BOOL>(nameof(LLInAlarm));

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosInAlarm = Member.Create<BOOL>(nameof(ROCPosInAlarm));

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegInAlarm = Member.Create<BOOL>(nameof(ROCNegInAlarm));

        /// <summary>
        /// Gets the <see cref="ROC"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<REAL> ROC = Member.Create<REAL>(nameof(ROC));

        /// <summary>
        /// Gets the <see cref="HHAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHAcked = Member.Create<BOOL>(nameof(HHAcked));

        /// <summary>
        /// Gets the <see cref="HAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HAcked = Member.Create<BOOL>(nameof(HAcked));

        /// <summary>
        /// Gets the <see cref="LAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LAcked = Member.Create<BOOL>(nameof(LAcked));

        /// <summary>
        /// Gets the <see cref="LLAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLAcked = Member.Create<BOOL>(nameof(LLAcked));

        /// <summary>
        /// Gets the <see cref="ROCPosAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosAcked = Member.Create<BOOL>(nameof(ROCPosAcked));

        /// <summary>
        /// Gets the <see cref="ROCNegAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegAcked = Member.Create<BOOL>(nameof(ROCNegAcked));

        /// <summary>
        /// Gets the <see cref="HHInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHInAlarmUnack = Member.Create<BOOL>(nameof(HHInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="HInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HInAlarmUnack = Member.Create<BOOL>(nameof(HInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="LInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LInAlarmUnack = Member.Create<BOOL>(nameof(LInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="LLInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLInAlarmUnack = Member.Create<BOOL>(nameof(LLInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosInAlarmUnack = Member.Create<BOOL>(nameof(ROCPosInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegInAlarmUnack = Member.Create<BOOL>(nameof(ROCNegInAlarmUnack));

        /// <summary>
        /// Gets the <see cref="Suppressed"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> Suppressed = Member.Create<BOOL>(nameof(Suppressed));

        /// <summary>
        /// Gets the <see cref="HHShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HHShelved = Member.Create<BOOL>(nameof(HHShelved));

        /// <summary>
        /// Gets the <see cref="HShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> HShelved = Member.Create<BOOL>(nameof(HShelved));

        /// <summary>
        /// Gets the <see cref="LShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LShelved = Member.Create<BOOL>(nameof(LShelved));

        /// <summary>
        /// Gets the <see cref="LLShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> LLShelved = Member.Create<BOOL>(nameof(LLShelved));

        /// <summary>
        /// Gets the <see cref="ROCPosShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosShelved = Member.Create<BOOL>(nameof(ROCPosShelved));

        /// <summary>
        /// Gets the <see cref="ROCNegShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegShelved = Member.Create<BOOL>(nameof(ROCNegShelved));

        /// <summary>
        /// Gets the <see cref="Disabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> Disabled = Member.Create<BOOL>(nameof(Disabled));

        /// <summary>
        /// Gets the <see cref="Commissioned"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> Commissioned = Member.Create<BOOL>(nameof(Commissioned));

        /// <summary>
        /// Gets the <see cref="MinDurationACC"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> MinDurationACC = Member.Create<DINT>(nameof(MinDurationACC));

        /// <summary>
        /// Gets the <see cref="HHInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> HHInAlarmTime = Member.Create<LINT>(nameof(HHInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="HHAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> HHAlarmCount = Member.Create<DINT>(nameof(HHAlarmCount));

        /// <summary>
        /// Gets the <see cref="HInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> HInAlarmTime = Member.Create<LINT>(nameof(HInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="HAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> HAlarmCount = Member.Create<DINT>(nameof(HAlarmCount));

        /// <summary>
        /// Gets the <see cref="LInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> LInAlarmTime = Member.Create<LINT>(nameof(LInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="LAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> LAlarmCount = Member.Create<DINT>(nameof(LAlarmCount));

        /// <summary>
        /// Gets the <see cref="LLInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> LLInAlarmTime = Member.Create<LINT>(nameof(LLInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="LLAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> LLAlarmCount = Member.Create<DINT>(nameof(LLAlarmCount));

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> ROCPosInAlarmTime = Member.Create<LINT>(nameof(ROCPosInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ROCPosAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> ROCPosAlarmCount = Member.Create<DINT>(nameof(ROCPosAlarmCount));

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> ROCNegInAlarmTime = Member.Create<LINT>(nameof(ROCNegInAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ROCNegAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> ROCNegAlarmCount = Member.Create<DINT>(nameof(ROCNegAlarmCount));

        /// <summary>
        /// Gets the <see cref="AckTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> AckTime = Member.Create<LINT>(nameof(AckTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> RetToNormalTime = Member.Create<LINT>(nameof(RetToNormalTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> AlarmCountResetTime = Member.Create<LINT>(nameof(AlarmCountResetTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ShelveTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> ShelveTime = Member.Create<LINT>(nameof(ShelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="UnshelveTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<LINT> UnshelveTime = Member.Create<LINT>(nameof(UnshelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<DINT> Status = Member.Create<DINT>(nameof(Status));

        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> InstructFault = Member.Create<BOOL>(nameof(InstructFault));

        /// <summary>
        /// Gets the <see cref="InFaulted"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> InFaulted = Member.Create<BOOL>(nameof(InFaulted));

        /// <summary>
        /// Gets the <see cref="SeverityInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> SeverityInv = Member.Create<BOOL>(nameof(SeverityInv));

        /// <summary>
        /// Gets the <see cref="AlarmLimitsInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> AlarmLimitsInv = Member.Create<BOOL>(nameof(AlarmLimitsInv));

        /// <summary>
        /// Gets the <see cref="DeadbandInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> DeadbandInv = Member.Create<BOOL>(nameof(DeadbandInv));

        /// <summary>
        /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPosLimitInv = Member.Create<BOOL>(nameof(ROCPosLimitInv));

        /// <summary>
        /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCNegLimitInv = Member.Create<BOOL>(nameof(ROCNegLimitInv));

        /// <summary>
        /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public IMember<BOOL> ROCPeriodInv = Member.Create<BOOL>(nameof(ROCPeriodInv));
    }
}