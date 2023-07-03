using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming Logix naming

namespace L5Sharp.Types.Predefined;

/// <summary>
/// A predefined or built in data type in Logix that is a part of the alarm instruction set.
/// </summary>
public sealed class ALARM_ANALOG : StructureType
{
    /// <summary>
    /// Creates a new <see cref="ALARM_ANALOG"/> data type instance.
    /// </summary>
    public ALARM_ANALOG() : base(nameof(ALARM_ANALOG))
    {
        EnableIn = new BOOL();
        In = new REAL();
        InFault = new BOOL();
        HHEnabled = new BOOL();
        HEnabled = new BOOL();
        LEnabled = new BOOL();
        LLEnabled = new BOOL();
        AckRequired = new BOOL();
        ProgAckAll = new BOOL();
        OperAckAll = new BOOL();
        HHProgAck = new BOOL();
        HHOperAck = new BOOL();
        HProgAck = new BOOL();
        HOperAck = new BOOL();
        LProgAck = new BOOL();
        LOperAck = new BOOL();
        LLProgAck = new BOOL();
        LLOperAck = new BOOL();
        ROCPosProgAck = new BOOL();
        ROCPosOperAck = new BOOL();
        ROCNegProgAck = new BOOL();
        ROCNegOperAck = new BOOL();
        ProgSuppress = new BOOL();
        OperSuppress = new BOOL();
        ProgUnsuppress = new BOOL();
        OperUnsuppress = new BOOL();
        HHOperShelve = new BOOL();
        HOperShelve = new BOOL();
        LOperShelve = new BOOL();
        LLOperShelve = new BOOL();
        ROCPosOperShelve = new BOOL();
        ROCNegOperShelve = new BOOL();
        ProgUnshelveAll = new BOOL();
        HHOperUnshelve = new BOOL();
        HOperUnshelve = new BOOL();
        LOperUnshelve = new BOOL();
        LLOperUnshelve = new BOOL();
        ROCPosOperUnshelve = new BOOL();
        ROCNegOperUnshelve = new BOOL();
        ProgDisable = new BOOL();
        OperDisable = new BOOL();
        ProgEnable = new BOOL();
        OperEnable = new BOOL();
        AlarmCountReset = new BOOL();
        HHMinDurationEnable = new BOOL();
        HMinDurationEnable = new BOOL();
        LMinDurationEnable = new BOOL();
        LLMinDurationEnable = new BOOL();
        HHLimit = new REAL();
        HHSeverity = new DINT();
        HLimit = new REAL();
        HSeverity = new DINT();
        LLimit = new REAL();
        LSeverity = new DINT();
        LLLimit = new REAL();
        LLSeverity = new DINT();
        MinDurationPRE = new DINT();
        ShelveDuration = new DINT();
        MaxShelveDuration = new DINT();
        Deadband = new REAL();
        ROCPosLimit = new REAL();
        ROCPosSeverity = new DINT();
        ROCNegLimit = new REAL();
        ROCNegSeverity = new DINT();
        ROCPeriod = new REAL();
        EnableOut = new BOOL();
        InAlarm = new BOOL();
        AnyInAlarmUnack = new BOOL();
        HHInAlarm = new BOOL();
        HInAlarm = new BOOL();
        LInAlarm = new BOOL();
        LLInAlarm = new BOOL();
        ROCPosInAlarm = new BOOL();
        ROCNegInAlarm = new BOOL();
        ROC = new REAL();
        HHAcked = new BOOL();
        HAcked = new BOOL();
        LAcked = new BOOL();
        LLAcked = new BOOL();
        ROCPosAcked = new BOOL();
        ROCNegAcked = new BOOL();
        HHInAlarmUnack = new BOOL();
        HInAlarmUnack = new BOOL();
        LInAlarmUnack = new BOOL();
        LLInAlarmUnack = new BOOL();
        ROCPosInAlarmUnack = new BOOL();
        ROCNegInAlarmUnack = new BOOL();
        Suppressed = new BOOL();
        HHShelved = new BOOL();
        HShelved = new BOOL();
        LShelved = new BOOL();
        LLShelved = new BOOL();
        ROCPosShelved = new BOOL();
        ROCNegShelved = new BOOL();
        Disabled = new BOOL();
        Commissioned = new BOOL();
        MinDurationACC = new DINT();
        HHInAlarmTime = new LINT();
        HHAlarmCount = new DINT();
        HInAlarmTime = new LINT();
        HAlarmCount = new DINT();
        LInAlarmTime = new LINT();
        LAlarmCount = new DINT();
        LLInAlarmTime = new LINT();
        LLAlarmCount = new DINT();
        ROCPosInAlarmTime = new LINT();
        ROCPosAlarmCount = new DINT();
        ROCNegInAlarmTime = new LINT();
        ROCNegAlarmCount = new DINT();
        AckTime = new LINT();
        RetToNormalTime = new LINT();
        AlarmCountResetTime = new LINT();
        ShelveTime = new LINT();
        UnshelveTime = new LINT();
        Status = new DINT();
        InstructFault = new BOOL();
        InFaulted = new BOOL();
        SeverityInv = new BOOL();
        AlarmLimitsInv = new BOOL();
        DeadbandInv = new BOOL();
        ROCPosLimitInv = new BOOL();
        ROCNegLimitInv = new BOOL();
        ROCPeriodInv = new BOOL();
    }

    /// <inheritdoc />
    public ALARM_ANALOG(XElement element) : base(nameof(ALARM_ANALOG))
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
        var element = new XElement(L5XName.AlarmAnalogParameters);
        element.Add(Members.Select(m => new XAttribute(m.Name, m.DataType)));
        return element;
    }

    /// <summary>
    /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="In"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AckRequired
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgSuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperSuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgUnsuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperUnsuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnshelveAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgUnshelveAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AlarmCountReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHMinDurationEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HMinDurationEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LMinDurationEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLMinDurationEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL HHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT HHSeverity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT HSeverity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT LSeverity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL LLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT LLSeverity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT MinDurationPRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ShelveDuration
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT MaxShelveDuration
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL Deadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ROCPosSeverity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ROCNegSeverity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCPeriod
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL InAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AnyInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AnyInAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHInAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HInAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LInAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLInAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosInAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegInAlarm"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegInAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROC"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHAcked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HAcked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LAcked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLAcked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosAcked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegAcked"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegAcked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHInAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HInAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LInAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLInAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosInAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegInAlarmUnack"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegInAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Suppressed"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL Suppressed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHShelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HShelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LShelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLShelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosShelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegShelved"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegShelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Disabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL Disabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Commissioned"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL Commissioned
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationACC"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT MinDurationACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT HHInAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT HHAlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT HInAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT HAlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT LInAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT LAlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT LLInAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT LLAlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT ROCPosInAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ROCPosAlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegInAlarmTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT ROCNegInAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegAlarmCount"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ROCNegAlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AckTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT AckTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="RetToNormalTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT RetToNormalTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCountResetTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT AlarmCountResetTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT ShelveTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="UnshelveTime"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public LINT UnshelveTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Status"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InFaulted"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL InFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="SeverityInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL SeverityInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmLimitsInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AlarmLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="DeadbandInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL DeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPeriodInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}