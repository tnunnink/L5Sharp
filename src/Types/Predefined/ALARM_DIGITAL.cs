using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class ALARM_DIGITAL : StructureType
    {
        /// <summary>
        /// Creates a new <see cref="ALARM_DIGITAL"/> data type instance.
        /// </summary>
        public ALARM_DIGITAL() : base(nameof(ALARM_DIGITAL))
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        public override IEnumerable<Member> Members()
        {
            var members = base.Members().ToList();
            
            members.Single(m => m.Name == nameof(ProgTime)).Radix = Radix.DateTime;
            members.Single(m => m.Name == nameof(InAlarmTime)).Radix = Radix.DateTime;
            members.Single(m => m.Name == nameof(AckTime)).Radix = Radix.DateTime;
            members.Single(m => m.Name == nameof(RetToNormalTime)).Radix = Radix.DateTime;
            members.Single(m => m.Name == nameof(AlarmCountResetTime)).Radix = Radix.DateTime;
            members.Single(m => m.Name == nameof(ShelveTime)).Radix = Radix.DateTime;
            members.Single(m => m.Name == nameof(UnshelveTime)).Radix = Radix.DateTime;

            return members;
        }

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL EnableIn { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL In { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL InFault { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Condition"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL Condition { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL AckRequired { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Latched"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL Latched { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL ProgAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperAck { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL ProgReset { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperReset { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL ProgSuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperSuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL ProgUnsuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperUnsuppress { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperShelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperShelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL ProgUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperUnshelve { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL ProgDisable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperDisable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL ProgEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL OperEnable { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL AlarmCountReset { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="UseProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL UseProgTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public LINT ProgTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Severity"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public DINT Severity { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public DINT MinDurationPRE { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public DINT ShelveDuration { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public DINT MaxShelveDuration { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL EnableOut { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InAlarm"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL InAlarm { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Acked"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL Acked { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InAlarmUnack"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL InAlarmUnack { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Suppressed"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL Suppressed { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Shelved"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL Shelved { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Disabled"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL Disabled { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Commissioned"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL Commissioned { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="MinDurationACC"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public DINT MinDurationACC { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AlarmCount"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public DINT AlarmCount { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InAlarmTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public LINT InAlarmTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AckTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public LINT AckTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public LINT RetToNormalTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public LINT AlarmCountResetTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ShelveTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public LINT ShelveTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="UnshelveTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public LINT UnshelveTime { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public DINT Status { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL InstructFault { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="InFaulted"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL InFaulted { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="SeverityInv"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public BOOL SeverityInv { get; set; } = new();
    }
}