using L5Sharp.Abstractions;
using L5Sharp.Atomics;
using L5Sharp.Enums;
using L5Sharp.Factories;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Predefined
{
    /// <summary>
    /// A predefined or built in data type in Logix that is a part of the alarm instruction set.
    /// </summary>
    public sealed class AlarmDigital : ComplexTypeBase
    {
        /// <summary>
        /// Creates a new <see cref="AlarmDigital"/> data type instance.
        /// </summary>
        public AlarmDigital() : base("ALARM_DIGITAL")
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new AlarmDigital();

        /// <summary>
        /// Gets the <see cref="EnableIn"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> EnableIn = Member.Create<Bool>(nameof(EnableIn));

        /// <summary>
        /// Gets the <see cref="In"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> In = Member.Create<Bool>(nameof(In));

        /// <summary>
        /// Gets the <see cref="InFault"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> InFault = Member.Create<Bool>(nameof(InFault));

        /// <summary>
        /// Gets the <see cref="Condition"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> Condition = Member.Create<Bool>(nameof(Condition));

        /// <summary>
        /// Gets the <see cref="AckRequired"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> AckRequired = Member.Create<Bool>(nameof(AckRequired));

        /// <summary>
        /// Gets the <see cref="Latched"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> Latched = Member.Create<Bool>(nameof(Latched));

        /// <summary>
        /// Gets the <see cref="ProgAck"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> ProgAck = Member.Create<Bool>(nameof(ProgAck));

        /// <summary>
        /// Gets the <see cref="OperAck"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperAck = Member.Create<Bool>(nameof(OperAck));

        /// <summary>
        /// Gets the <see cref="ProgReset"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> ProgReset = Member.Create<Bool>(nameof(ProgReset));

        /// <summary>
        /// Gets the <see cref="OperReset"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperReset = Member.Create<Bool>(nameof(OperReset));

        /// <summary>
        /// Gets the <see cref="ProgSuppress"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> ProgSuppress = Member.Create<Bool>(nameof(ProgSuppress));

        /// <summary>
        /// Gets the <see cref="OperSuppress"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperSuppress = Member.Create<Bool>(nameof(OperSuppress));

        /// <summary>
        /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> ProgUnsuppress = Member.Create<Bool>(nameof(ProgUnsuppress));

        /// <summary>
        /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperUnsuppress = Member.Create<Bool>(nameof(OperUnsuppress));

        /// <summary>
        /// Gets the <see cref="OperShelve"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperShelve = Member.Create<Bool>(nameof(OperShelve));

        /// <summary>
        /// Gets the <see cref="ProgUnshelve"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> ProgUnshelve = Member.Create<Bool>(nameof(ProgUnshelve));

        /// <summary>
        /// Gets the <see cref="OperUnshelve"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperUnshelve = Member.Create<Bool>(nameof(OperUnshelve));

        /// <summary>
        /// Gets the <see cref="ProgDisable"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> ProgDisable = Member.Create<Bool>(nameof(ProgDisable));

        /// <summary>
        /// Gets the <see cref="OperDisable"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperDisable = Member.Create<Bool>(nameof(OperDisable));

        /// <summary>
        /// Gets the <see cref="ProgEnable"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> ProgEnable = Member.Create<Bool>(nameof(ProgEnable));

        /// <summary>
        /// Gets the <see cref="OperEnable"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> OperEnable = Member.Create<Bool>(nameof(OperEnable));

        /// <summary>
        /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> AlarmCountReset = Member.Create<Bool>(nameof(AlarmCountReset));

        /// <summary>
        /// Gets the <see cref="UseProgTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> UseProgTime = Member.Create<Bool>(nameof(UseProgTime));

        /// <summary>
        /// Gets the <see cref="ProgTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Lint> ProgTime = Member.Create<Lint>(nameof(ProgTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="Severity"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Dint> Severity = Member.Create<Dint>(nameof(Severity));

        /// <summary>
        /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Dint> MinDurationPRE = Member.Create<Dint>(nameof(MinDurationPRE));

        /// <summary>
        /// Gets the <see cref="ShelveDuration"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Dint> ShelveDuration = Member.Create<Dint>(nameof(ShelveDuration));

        /// <summary>
        /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Dint> MaxShelveDuration = Member.Create<Dint>(nameof(MaxShelveDuration));

        /// <summary>
        /// Gets the <see cref="EnableOut"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> EnableOut = Member.Create<Bool>(nameof(EnableOut));

        /// <summary>
        /// Gets the <see cref="InAlarm"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> InAlarm = Member.Create<Bool>(nameof(InAlarm));

        /// <summary>
        /// Gets the <see cref="Acked"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> Acked = Member.Create<Bool>(nameof(Acked));

        /// <summary>
        /// Gets the <see cref="InAlarmUnack"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> InAlarmUnack = Member.Create<Bool>(nameof(InAlarmUnack));

        /// <summary>
        /// Gets the <see cref="Suppressed"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> Suppressed = Member.Create<Bool>(nameof(Suppressed));

        /// <summary>
        /// Gets the <see cref="Shelved"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> Shelved = Member.Create<Bool>(nameof(Shelved));

        /// <summary>
        /// Gets the <see cref="Disabled"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> Disabled = Member.Create<Bool>(nameof(Disabled));

        /// <summary>
        /// Gets the <see cref="Commissioned"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> Commissioned = Member.Create<Bool>(nameof(Commissioned));

        /// <summary>
        /// Gets the <see cref="MinDurationACC"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Dint> MinDurationACC = Member.Create<Dint>(nameof(MinDurationACC));

        /// <summary>
        /// Gets the <see cref="AlarmCount"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Dint> AlarmCount = Member.Create<Dint>(nameof(AlarmCount));

        /// <summary>
        /// Gets the <see cref="InAlarmTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Lint> InAlarmTime = Member.Create<Lint>(nameof(InAlarmTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="AckTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Lint> AckTime = Member.Create<Lint>(nameof(AckTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Lint> RetToNormalTime = Member.Create<Lint>(nameof(RetToNormalTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Lint> AlarmCountResetTime = Member.Create<Lint>(nameof(AlarmCountResetTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="ShelveTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Lint> ShelveTime = Member.Create<Lint>(nameof(ShelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="UnshelveTime"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Lint> UnshelveTime = Member.Create<Lint>(nameof(UnshelveTime), Radix.DateTime);

        /// <summary>
        /// Gets the <see cref="Status"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Dint> Status = Member.Create<Dint>(nameof(Status));

        /// <summary>
        /// Gets the <see cref="InstructFault"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> InstructFault = Member.Create<Bool>(nameof(InstructFault));

        /// <summary>
        /// Gets the <see cref="InFaulted"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> InFaulted = Member.Create<Bool>(nameof(InFaulted));

        /// <summary>
        /// Gets the <see cref="SeverityInv"/> member of the <see cref="AlarmDigital"/> data type.
        /// </summary>
        public IMember<Bool> SeverityInv = Member.Create<Bool>(nameof(SeverityInv));
    }
}