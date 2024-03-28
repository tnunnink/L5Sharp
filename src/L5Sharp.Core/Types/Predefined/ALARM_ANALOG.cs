using System.Collections.Generic;
using System.Xml.Linq;

// ReSharper disable InconsistentNaming Logix naming

namespace L5Sharp.Core;

/// <summary>
/// A predefined or built in data type in Logix that is a part of the alarm instruction set.
/// </summary>
[L5XType(L5XName.AlarmAnalogParameters)]
public sealed class ALARM_ANALOG : StructureType
{
    /// <summary>
    /// Creates a new <see cref="ALARM_ANALOG"/> data type instance.
    /// </summary>
    public ALARM_ANALOG() : base(new XElement(L5XName.AlarmAnalogParameters))
    {
        EnableIn = false;
        In = 0;
        InFault = false;
        HHEnabled = false;
        HEnabled = false;
        LEnabled = false;
        LLEnabled = false;
        AckRequired = false;
        ProgAckAll = false;
        OperAckAll = false;
        HHProgAck = false;
        HHOperAck = false;
        HProgAck = false;
        HOperAck = false;
        LProgAck = false;
        LOperAck = false;
        LLProgAck = false;
        LLOperAck = false;
        ROCPosProgAck = false;
        ROCPosOperAck = false;
        ROCNegProgAck = false;
        ROCNegOperAck = false;
        ProgSuppress = false;
        OperSuppress = false;
        ProgUnsuppress = false;
        OperUnsuppress = false;
        HHOperShelve = false;
        HOperShelve = false;
        LOperShelve = false;
        LLOperShelve = false;
        ROCPosOperShelve = false;
        ROCNegOperShelve = false;
        ProgUnshelveAll = false;
        HHOperUnshelve = false;
        HOperUnshelve = false;
        LOperUnshelve = false;
        LLOperUnshelve = false;
        ROCPosOperUnshelve = false;
        ROCNegOperUnshelve = false;
        ProgDisable = false;
        OperDisable = false;
        ProgEnable = false;
        OperEnable = false;
        AlarmCountReset = false;
        HHMinDurationEnable = false;
        HMinDurationEnable = false;
        LMinDurationEnable = false;
        LLMinDurationEnable = false;
        HHLimit = 0;
        HHSeverity = new int();
        HLimit = 0;
        HSeverity = new int();
        LLimit = 0;
        LSeverity = new int();
        LLLimit = 0;
        LLSeverity = new int();
        MinDurationPRE = new int();
        ShelveDuration = new int();
        MaxShelveDuration = new int();
        Deadband = 0;
        ROCPosLimit = 0;
        ROCPosSeverity = new int();
        ROCNegLimit = 0;
        ROCNegSeverity = new int();
        ROCPeriod = 0;
    }

    /// <inheritdoc />
    public ALARM_ANALOG(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// A <see cref="ALARM_ANALOG"/> is a special Rockwell defined data type that does not contain decorated member elements.
    /// All values are stored as local attributes on the data element. When using this type with a tag, you must cast
    /// the tag <c>Value</c> as the message type and set these properties statically.
    /// </remarks>
    public override IEnumerable<Member> Members => GenerateVirtualMembers();

    private IEnumerable<Member> GenerateVirtualMembers()
    {
        yield return new Member(nameof(EnableIn), () => EnableIn, t => { EnableIn = t.As<BOOL>(); });
        yield return new Member(nameof(In), () => In, t => { In = t.As<REAL>(); });
        yield return new Member(nameof(InFault), () => InFault, t => { InFault = t.As<BOOL>(); });
    }

    /// <summary>
    /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="In"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHEnabled
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HEnabled
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LEnabled
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLEnabled
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AckRequired
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgAckAll
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperAckAll
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHProgAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HProgAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LProgAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLProgAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosProgAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegProgAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperAck
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgSuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperSuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgUnsuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperUnsuppress
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperShelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperShelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperShelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperShelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperShelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperShelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnshelveAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgUnshelveAll
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperUnshelve
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgDisable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperDisable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AlarmCountReset
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHMinDurationEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HMinDurationEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LMinDurationEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLMinDurationEnable
    {
        get => GetRequiredValue<BOOL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL HHLimit
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int HHSeverity
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL HLimit
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int HSeverity
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL LLimit
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int LSeverity
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL LLLimit
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int LLSeverity
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int MinDurationPRE
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int ShelveDuration
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int MaxShelveDuration
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL Deadband
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCPosLimit
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int ROCPosSeverity
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCNegLimit
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public int ROCNegSeverity
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCPeriod
    {
        get => GetRequiredValue<REAL>();
        set => SetRequiredValue(value);
    }
}