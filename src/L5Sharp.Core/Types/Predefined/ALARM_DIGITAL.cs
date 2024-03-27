using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Core;

/// <summary>
/// A predefined or built in data type in Logix that is a part of the alarm instruction set.
/// </summary>
[L5XType(L5XName.AlarmDigitalParameters)]
public sealed class ALARM_DIGITAL : StructureType
{
    /// <summary>
    /// Creates a new <see cref="ALARM_DIGITAL"/> data type instance.
    /// </summary>
    public ALARM_DIGITAL() : base(new XElement(L5XName.AlarmDigitalParameters))
    {
        Severity = new DINT();
        MinDurationPRE = new DINT();
        ShelveDuration = new DINT();
        MaxShelveDuration = new DINT();
        ProgTime = new LINT(Radix.DateTime);
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
    }

    /// <inheritdoc />
    public ALARM_DIGITAL(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<Member> Members =>
        Element.Attributes().Select(a => new Member(a.Name.LocalName, AtomicType.Parse(a.Value)));

    /// <summary>
    /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="In"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL In
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Condition"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Condition
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL AckRequired
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Latched"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL Latched
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperAck"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgReset
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperReset
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgSuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperSuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgUnsuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperUnsuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperShelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperShelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperUnshelve"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgDisable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperDisable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL ProgEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL OperEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL AlarmCountReset
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="UseProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public BOOL UseProgTime
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgTime"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public LINT ProgTime
    {
        get => GetRequiredValue<LINT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Severity"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT Severity
    {
        get => GetRequiredValue<DINT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT MinDurationPRE
    {
        get => GetRequiredValue<DINT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT ShelveDuration
    {
        get => GetRequiredValue<DINT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_DIGITAL"/> data type.
    /// </summary>
    public DINT MaxShelveDuration
    {
        get => GetRequiredValue<DINT>();
        set => SetRequiredValue(value);
    }
}