using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Types.Predefined;

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
        EnableIn = new BOOL();
        In = new BOOL();
        InFault = new BOOL();
        Condition = new BOOL();
        AckRequired = new BOOL();
        Latched = new BOOL();
        ProgAck = new BOOL();
        OperAck = new BOOL();
        ProgReset = new BOOL();
        OperReset = new BOOL();
        ProgSuppress = new BOOL();
        OperSuppress = new BOOL();
        ProgUnsuppress = new BOOL();
        OperUnsuppress = new BOOL();
        OperShelve = new BOOL();
        ProgUnshelve = new BOOL();
        OperUnshelve = new BOOL();
        ProgDisable = new BOOL();
        OperDisable = new BOOL();
        ProgEnable = new BOOL();
        OperEnable = new BOOL();
        AlarmCountReset = new BOOL();
        UseProgTime = new BOOL();
        ProgTime = new LINT(Radix.DateTime);
        Severity = new DINT();
        MinDurationPRE = new DINT();
        ShelveDuration = new DINT();
        MaxShelveDuration = new DINT();
        EnableOut = new BOOL();
        InAlarm = new BOOL();
        Acked = new BOOL();
        InAlarmUnack = new BOOL();
        Suppressed = new BOOL();
        Shelved = new BOOL();
        Disabled = new BOOL();
        Commissioned = new BOOL();
        MinDurationACC = new DINT();
        AlarmCount = new DINT();
        InAlarmTime = new LINT(Radix.DateTime);
        AckTime = new LINT(Radix.DateTime);
        RetToNormalTime = new LINT(Radix.DateTime);
        AlarmCountResetTime = new LINT(Radix.DateTime);
        ShelveTime = new LINT(Radix.DateTime);
        UnshelveTime = new LINT(Radix.DateTime);
        Status = new DINT();
        InstructFault = new BOOL();
        InFaulted = new BOOL();
        SeverityInv = new BOOL();
    }

    /// <inheritdoc />
    public ALARM_DIGITAL(XElement element) : base(nameof(ALARM_DIGITAL))
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        var members = element.Attributes().Select(a => new Member(a.Name.ToString(), Atomic.Parse(a.Value)));
        AddMembers(members.ToList());
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;
    
    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.AlarmDigitalParameters);
        element.Add(Members.Select(m => new XAttribute(m.Name, m.DataType)));
        return element;
    }

    /// <summary>
    /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="In"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL In
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Condition"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Condition
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL AckRequired
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Latched"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Latched
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgSuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperSuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgUnsuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperUnsuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperShelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL AlarmCountReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="UseProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL UseProgTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT ProgTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Severity"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT Severity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT MinDurationPRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT ShelveDuration
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT MaxShelveDuration
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InAlarm"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL InAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Acked"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Acked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InAlarmUnack"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL InAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Suppressed"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Suppressed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Shelved"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Shelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Disabled"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Disabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Commissioned"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Commissioned
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationACC"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT MinDurationACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCount"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT AlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InAlarmTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT InAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AckTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT AckTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT RetToNormalTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT AlarmCountResetTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT ShelveTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="UnshelveTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT UnshelveTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Status"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InFaulted"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL InFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="SeverityInv"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL SeverityInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}