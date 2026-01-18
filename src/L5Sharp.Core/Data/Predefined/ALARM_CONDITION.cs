using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>ALARM_CONDITION</c> data type structure.
/// </summary>
[LogixData("ALARM_CONDITION")]
public sealed partial class ALARM_CONDITION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="ALARM_CONDITION"/> instance initialized with default values.
    /// </summary>
    public ALARM_CONDITION() : base("ALARM_CONDITION")
    {
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
        Limit = new REAL();
        Severity = new DINT();
        OnDelay = new DINT();
        OffDelay = new DINT();
        ShelveDuration = new DINT();
        MaxShelveDuration = new DINT();
        Deadband = new REAL();
        InAlarm = new BOOL();
        Acked = new BOOL();
        InAlarmUnack = new BOOL();
        Suppressed = new BOOL();
        Shelved = new BOOL();
        Disabled = new BOOL();
        Used = new BOOL();
        AlarmCount = new DINT();
        InAlarmTime = new LINT();
        AckTime = new LINT();
        RetToNormalTime = new LINT();
        AlarmCountResetTime = new LINT();
        ShelveTime = new LINT();
        UnshelveTime = new LINT();
        Status = new DINT();
        AlarmFault = new BOOL();
        InFaulted = new BOOL();
        SeverityInv = new BOOL();
        LimitInv = new BOOL();
        DeadbandInv = new BOOL();
        Overflow = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="ALARM_CONDITION"/> instance initialized with the provided element.
    /// </summary>
    public ALARM_CONDITION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>InFault</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Condition</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Condition
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AckRequired</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL AckRequired
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Latched</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Latched
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgAck</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL ProgAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperAck</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgReset</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL ProgReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperReset</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgSuppress</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL ProgSuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperSuppress</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperSuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgUnsuppress</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL ProgUnsuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperUnsuppress</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperUnsuppress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperShelve</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgUnshelve</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL ProgUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperUnshelve</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperUnshelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgDisable</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL ProgDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperDisable</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgEnable</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL ProgEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperEnable</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL OperEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmCountReset</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL AlarmCountReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Limit</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public REAL Limit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Severity</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public DINT Severity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OnDelay</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public DINT OnDelay
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OffDelay</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public DINT OffDelay
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShelveDuration</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public DINT ShelveDuration
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxShelveDuration</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public DINT MaxShelveDuration
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Deadband</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public REAL Deadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InAlarm</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL InAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Acked</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Acked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InAlarmUnack</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL InAlarmUnack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Suppressed</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Suppressed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Shelved</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Shelved
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Disabled</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Disabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Used</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Used
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmCount</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public DINT AlarmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InAlarmTime</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public LINT InAlarmTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AckTime</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public LINT AckTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RetToNormalTime</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public LINT RetToNormalTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmCountResetTime</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public LINT AlarmCountResetTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShelveTime</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public LINT ShelveTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UnshelveTime</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public LINT UnshelveTime
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmFault</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL AlarmFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFaulted</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL InFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SeverityInv</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL SeverityInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitInv</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL LimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeadbandInv</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL DeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Overflow</c> member of the <see cref="ALARM_CONDITION"/> data type.
    /// </summary>
    public BOOL Overflow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}