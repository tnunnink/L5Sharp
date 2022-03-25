using L5Sharp.Abstractions;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class ALARM_DIGITAL : ComplexType
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
        protected override IDataType New() => new ALARM_DIGITAL();

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> EnableIn = Member.Create<BOOL>(nameof(EnableIn));

        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> In = Member.Create<BOOL>(nameof(In));

        /// <summary>
        /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> InFault = Member.Create<BOOL>(nameof(InFault));

        /// <summary>
        /// Gets the <see cref="Condition"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> Condition = Member.Create<BOOL>(nameof(Condition));

        /// <summary>
        /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> AckRequired = Member.Create<BOOL>(nameof(AckRequired));

        /// <summary>
        /// Gets the <see cref="Latched"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> Latched = Member.Create<BOOL>(nameof(Latched));

        /// <summary>
        /// Gets the <see cref="ProgAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> ProgAck = Member.Create<BOOL>(nameof(ProgAck));

        /// <summary>
        /// Gets the <see cref="OperAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperAck = Member.Create<BOOL>(nameof(OperAck));

        /// <summary>
        /// Gets the <see cref="ProgReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> ProgReset = Member.Create<BOOL>(nameof(ProgReset));

        /// <summary>
        /// Gets the <see cref="OperReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperReset = Member.Create<BOOL>(nameof(OperReset));

        /// <summary>
        /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> ProgSuppress = Member.Create<BOOL>(nameof(ProgSuppress));

        /// <summary>
        /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperSuppress = Member.Create<BOOL>(nameof(OperSuppress));

        /// <summary>
        /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> ProgUnsuppress = Member.Create<BOOL>(nameof(ProgUnsuppress));

        /// <summary>
        /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperUnsuppress = Member.Create<BOOL>(nameof(OperUnsuppress));

        /// <summary>
        /// Gets the <see cref="OperShelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperShelve = Member.Create<BOOL>(nameof(OperShelve));

        /// <summary>
        /// Gets the <see cref="ProgUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> ProgUnshelve = Member.Create<BOOL>(nameof(ProgUnshelve));

        /// <summary>
        /// Gets the <see cref="OperUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperUnshelve = Member.Create<BOOL>(nameof(OperUnshelve));

        /// <summary>
        /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> ProgDisable = Member.Create<BOOL>(nameof(ProgDisable));

        /// <summary>
        /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperDisable = Member.Create<BOOL>(nameof(OperDisable));

        /// <summary>
        /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> ProgEnable = Member.Create<BOOL>(nameof(ProgEnable));

        /// <summary>
        /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> OperEnable = Member.Create<BOOL>(nameof(OperEnable));

        /// <summary>
        /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> AlarmCountReset = Member.Create<BOOL>(nameof(AlarmCountReset));

        /// <summary>
        /// Gets the <see cref="UseProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> UseProgTime = Member.Create<BOOL>(nameof(UseProgTime));

        /// <summary>
        /// Gets the <see cref="ProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<LINT> ProgTime = Member.Create<LINT>(nameof(ProgTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="Severity"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<DINT> Severity = Member.Create<DINT>(nameof(Severity));

        /// <summary>
        /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<DINT> MinDurationPRE = Member.Create<DINT>(nameof(MinDurationPRE));

        /// <summary>
        /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<DINT> ShelveDuration = Member.Create<DINT>(nameof(ShelveDuration));

        /// <summary>
        /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<DINT> MaxShelveDuration = Member.Create<DINT>(nameof(MaxShelveDuration));

        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> EnableOut = Member.Create<BOOL>(nameof(EnableOut));

        /// <summary>
        /// Gets the <see cref="InAlarm"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> InAlarm = Member.Create<BOOL>(nameof(InAlarm));

        /// <summary>
        /// Gets the <see cref="Acked"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> Acked = Member.Create<BOOL>(nameof(Acked));

        /// <summary>
        /// Gets the <see cref="InAlarmUnack"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> InAlarmUnack = Member.Create<BOOL>(nameof(InAlarmUnack));

        /// <summary>
        /// Gets the <see cref="Suppressed"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> Suppressed = Member.Create<BOOL>(nameof(Suppressed));

        /// <summary>
        /// Gets the <see cref="Shelved"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> Shelved = Member.Create<BOOL>(nameof(Shelved));

        /// <summary>
        /// Gets the <see cref="Disabled"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> Disabled = Member.Create<BOOL>(nameof(Disabled));

        /// <summary>
        /// Gets the <see cref="Commissioned"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> Commissioned = Member.Create<BOOL>(nameof(Commissioned));

        /// <summary>
        /// Gets the <see cref="MinDurationACC"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<DINT> MinDurationACC = Member.Create<DINT>(nameof(MinDurationACC));

        /// <summary>
        /// Gets the <see cref="AlarmCount"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<DINT> AlarmCount = Member.Create<DINT>(nameof(AlarmCount));

        /// <summary>
        /// Gets the <see cref="InAlarmTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<LINT> InAlarmTime = Member.Create<LINT>(nameof(InAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="AckTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<LINT> AckTime = Member.Create<LINT>(nameof(AckTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<LINT> RetToNormalTime = Member.Create<LINT>(nameof(RetToNormalTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<LINT> AlarmCountResetTime = Member.Create<LINT>(nameof(AlarmCountResetTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ShelveTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<LINT> ShelveTime = Member.Create<LINT>(nameof(ShelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="UnshelveTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<LINT> UnshelveTime = Member.Create<LINT>(nameof(UnshelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<DINT> Status = Member.Create<DINT>(nameof(Status));

        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> InstructFault = Member.Create<BOOL>(nameof(InstructFault));

        /// <summary>
        /// Gets the <see cref="InFaulted"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> InFaulted = Member.Create<BOOL>(nameof(InFaulted));

        /// <summary>
        /// Gets the <see cref="SeverityInv"/> member of the <see cref="ALARM_DIGITAL"/> data type.
        /// </summary>
        public IMember<BOOL> SeverityInv = Member.Create<BOOL>(nameof(SeverityInv));
    }
}