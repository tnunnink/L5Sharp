using System.Collections.Generic;
using System.Linq;
using L5Sharp.Attributes;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming Logix naming

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    [LogixSerializer(typeof(AlarmAnalogSerializer))]
    public sealed class ALARM_ANALOG : StructureType
    {
        /// <summary>
        /// Creates a new <see cref="ALARM_ANALOG"/> data type instance.
        /// </summary>
        public ALARM_ANALOG() : base(nameof(ALARM_ANALOG))
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL EnableIn { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL In { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL InFault { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHEnabled { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HEnabled { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LEnabled { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLEnabled { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL AckRequired { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ProgAckAll { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL OperAckAll { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHProgAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHOperAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HProgAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HOperAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LProgAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LOperAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLProgAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLOperAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosProgAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosOperAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegProgAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegOperAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ProgSuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL OperSuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ProgUnsuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL OperUnsuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHOperShelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HOperShelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LOperShelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLOperShelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosOperShelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegOperShelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgUnshelveAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ProgUnshelveAll { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHOperUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HOperUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LOperUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLOperUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosOperUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegOperUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ProgDisable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL OperDisable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ProgEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL OperEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL AlarmCountReset { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHMinDurationEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HMinDurationEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LMinDurationEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLMinDurationEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL HHLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT HHSeverity { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL HLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT HSeverity { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL LLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT LSeverity { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL LLLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT LLSeverity { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT MinDurationPRE { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT ShelveDuration { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT MaxShelveDuration { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL Deadband { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL ROCPosLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT ROCPosSeverity { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL ROCNegLimit { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT ROCNegSeverity { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL ROCPeriod { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL EnableOut { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL InAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AnyInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL AnyInAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHInAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HInAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LInAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLInAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosInAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegInAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROC"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public REAL ROC { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHAcked { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HAcked { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LAcked { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLAcked { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosAcked { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegAcked { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHInAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HInAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LInAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLInAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosInAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegInAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Suppressed"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL Suppressed { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HHShelved { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL HShelved { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LShelved { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL LLShelved { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosShelved { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegShelved { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Disabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL Disabled { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Commissioned"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL Commissioned { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="MinDurationACC"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT MinDurationACC { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT HHInAlarmTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HHAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT HHAlarmCount { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT HInAlarmTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="HAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT HAlarmCount { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT LInAlarmTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT LAlarmCount { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT LLInAlarmTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LLAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT LLAlarmCount { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT ROCPosInAlarmTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT ROCPosAlarmCount { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT ROCNegInAlarmTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT ROCNegAlarmCount { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AckTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT AckTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT RetToNormalTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT AlarmCountResetTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ShelveTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT ShelveTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="UnshelveTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public LINT UnshelveTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public DINT Status { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL InstructFault { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InFaulted"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL InFaulted { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="SeverityInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL SeverityInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AlarmLimitsInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL AlarmLimitsInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="DeadbandInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL DeadbandInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPosLimitInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCNegLimitInv { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
        /// </summary>
        public BOOL ROCPeriodInv { get; set; } = new();
    }
}