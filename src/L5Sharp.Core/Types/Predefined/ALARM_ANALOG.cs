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
        HHSeverity = 500;
        HLimit = 0;
        HSeverity = new DINT();
        LLimit = 0;
        LSeverity = new DINT();
        LLLimit = 0;
        LLSeverity = new DINT();
        MinDurationPRE = new DINT();
        ShelveDuration = new DINT();
        MaxShelveDuration = new DINT();
        Deadband = 0;
        ROCPosLimit = 0;
        ROCPosSeverity = new DINT();
        ROCNegLimit = 0;
        ROCNegSeverity = new DINT();
        ROCPeriod = 0;
    }

    /// <inheritdoc />
    public ALARM_ANALOG(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override string Name => nameof(ALARM_ANALOG);

    /// <inheritdoc />
    /// <remarks>
    /// A <see cref="ALARM_ANALOG"/> is a special Rockwell defined data type that does not contain decorated member elements.
    /// All values are stored as local attributes on the data element. When using this type with a tag, you must cast
    /// the tag <c>Value</c> as the message type and set these properties statically.
    /// </remarks>
    public override IEnumerable<Member> Members => GenerateVirtualMembers();

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
    public DINT HHSeverity
    {
        get => GetRequiredValue<DINT>();
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
    public DINT HSeverity
    {
        get => GetRequiredValue<DINT>();
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
    public DINT LSeverity
    {
        get => GetRequiredValue<DINT>();
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
    public DINT LLSeverity
    {
        get => GetRequiredValue<DINT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT MinDurationPRE
    {
        get => GetRequiredValue<DINT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ShelveDuration
    {
        get => GetRequiredValue<DINT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT MaxShelveDuration
    {
        get => GetRequiredValue<DINT>();
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
    public DINT ROCPosSeverity
    {
        get => GetRequiredValue<DINT>();
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
    public DINT ROCNegSeverity
    {
        get => GetRequiredValue<DINT>();
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

    /// <summary>
    /// Creates the collection of virtual members for this custom predefined type.
    /// </summary>
    private IEnumerable<Member> GenerateVirtualMembers()
    {
        yield return new Member(nameof(EnableIn), () => EnableIn, t => { EnableIn = t.As<BOOL>(); });
        yield return new Member(nameof(In), () => In, t => { In = t.As<REAL>(); });
        yield return new Member(nameof(InFault), () => InFault, t => { InFault = t.As<BOOL>(); });
        yield return new Member(nameof(HHEnabled), () => HHEnabled, t => { HHEnabled = t.As<BOOL>(); });
        yield return new Member(nameof(HEnabled), () => HEnabled, t => { HEnabled = t.As<BOOL>(); });
        yield return new Member(nameof(LEnabled), () => LEnabled, t => { LEnabled = t.As<BOOL>(); });
        yield return new Member(nameof(LLEnabled), () => LLEnabled, t => { LLEnabled = t.As<BOOL>(); });
        yield return new Member(nameof(AckRequired), () => AckRequired, t => { AckRequired = t.As<BOOL>(); });
        yield return new Member(nameof(ProgAckAll), () => ProgAckAll, t => { ProgAckAll = t.As<BOOL>(); });
        yield return new Member(nameof(OperAckAll), () => OperAckAll, t => { OperAckAll = t.As<BOOL>(); });
        yield return new Member(nameof(HHProgAck), () => HHProgAck, t => { HHProgAck = t.As<BOOL>(); });
        yield return new Member(nameof(HHOperAck), () => HHOperAck, t => { HHOperAck = t.As<BOOL>(); });
        yield return new Member(nameof(HProgAck), () => HProgAck, t => { HProgAck = t.As<BOOL>(); });
        yield return new Member(nameof(HOperAck), () => HOperAck, t => { HOperAck = t.As<BOOL>(); });
        yield return new Member(nameof(LProgAck), () => LProgAck, t => { LProgAck = t.As<BOOL>(); });
        yield return new Member(nameof(LOperAck), () => LOperAck, t => { LOperAck = t.As<BOOL>(); });
        yield return new Member(nameof(LLProgAck), () => LLProgAck, t => { LLProgAck = t.As<BOOL>(); });
        yield return new Member(nameof(LLOperAck), () => LLOperAck, t => { LLOperAck = t.As<BOOL>(); });
        yield return new Member(nameof(ROCPosProgAck), () => ROCPosProgAck, t => { ROCPosProgAck = t.As<BOOL>(); });
        yield return new Member(nameof(ROCPosOperAck), () => ROCPosOperAck, t => { ROCPosOperAck = t.As<BOOL>(); });
        yield return new Member(nameof(ROCNegProgAck), () => ROCNegProgAck, t => { ROCNegProgAck = t.As<BOOL>(); });
        yield return new Member(nameof(ROCNegOperAck), () => ROCNegOperAck, t => { ROCNegOperAck = t.As<BOOL>(); });
        yield return new Member(nameof(ProgSuppress), () => ProgSuppress, t => { ProgSuppress = t.As<BOOL>(); });
        yield return new Member(nameof(OperSuppress), () => OperSuppress, t => { OperSuppress = t.As<BOOL>(); });
        yield return new Member(nameof(ProgUnsuppress), () => ProgUnsuppress, t => { ProgUnsuppress = t.As<BOOL>(); });
        yield return new Member(nameof(OperUnsuppress), () => OperUnsuppress, t => { OperUnsuppress = t.As<BOOL>(); });
        yield return new Member(nameof(HHOperShelve), () => HHOperShelve, t => { HHOperShelve = t.As<BOOL>(); });
        yield return new Member(nameof(HOperShelve), () => HOperShelve, t => { HOperShelve = t.As<BOOL>(); });
        yield return new Member(nameof(LOperShelve), () => LOperShelve, t => { LOperShelve = t.As<BOOL>(); });
        yield return new Member(nameof(LLOperShelve), () => LLOperShelve, t => { LLOperShelve = t.As<BOOL>(); });
        yield return new Member(nameof(ROCPosOperShelve), () => ROCPosOperShelve,
            t => { ROCPosOperShelve = t.As<BOOL>(); });
        yield return new Member(nameof(ROCNegOperShelve), () => ROCNegOperShelve,
            t => { ROCNegOperShelve = t.As<BOOL>(); });
        yield return new Member(nameof(ProgUnshelveAll), () => ProgUnshelveAll,
            t => { ProgUnshelveAll = t.As<BOOL>(); });
        yield return new Member(nameof(HHOperUnshelve), () => HHOperUnshelve, t => { HHOperUnshelve = t.As<BOOL>(); });
        yield return new Member(nameof(HOperUnshelve), () => HOperUnshelve, t => { HOperUnshelve = t.As<BOOL>(); });
        yield return new Member(nameof(LOperUnshelve), () => LOperUnshelve, t => { LOperUnshelve = t.As<BOOL>(); });
        yield return new Member(nameof(LLOperUnshelve), () => LLOperUnshelve, t => { LLOperUnshelve = t.As<BOOL>(); });
        yield return new Member(nameof(ROCPosOperUnshelve), () => ROCPosOperUnshelve,
            t => { ROCPosOperUnshelve = t.As<BOOL>(); });
        yield return new Member(nameof(ROCNegOperUnshelve), () => ROCNegOperUnshelve,
            t => { ROCNegOperUnshelve = t.As<BOOL>(); });
        yield return new Member(nameof(ProgDisable), () => ProgDisable, t => { ProgDisable = t.As<BOOL>(); });
        yield return new Member(nameof(OperDisable), () => OperDisable, t => { OperDisable = t.As<BOOL>(); });
        yield return new Member(nameof(ProgEnable), () => ProgEnable, t => { ProgEnable = t.As<BOOL>(); });
        yield return new Member(nameof(OperEnable), () => OperEnable, t => { OperEnable = t.As<BOOL>(); });
        yield return new Member(nameof(AlarmCountReset), () => AlarmCountReset,
            t => { AlarmCountReset = t.As<BOOL>(); });
        yield return new Member(nameof(HHMinDurationEnable), () => HHMinDurationEnable,
            t => { HHMinDurationEnable = t.As<BOOL>(); });
        yield return new Member(nameof(HMinDurationEnable), () => HMinDurationEnable,
            t => { HMinDurationEnable = t.As<BOOL>(); });
        yield return new Member(nameof(LMinDurationEnable), () => LMinDurationEnable,
            t => { LMinDurationEnable = t.As<BOOL>(); });
        yield return new Member(nameof(LLMinDurationEnable), () => LLMinDurationEnable,
            t => { LLMinDurationEnable = t.As<BOOL>(); });
        yield return new Member(nameof(HHLimit), () => HHLimit, t => { HHLimit = t.As<REAL>(); });
        yield return new Member(nameof(HHSeverity), () => HHSeverity, t => { HHSeverity = t.As<DINT>(); });
        yield return new Member(nameof(HLimit), () => HLimit, t => { HLimit = t.As<REAL>(); });
        yield return new Member(nameof(HSeverity), () => HSeverity, t => { HSeverity = t.As<DINT>(); });
        yield return new Member(nameof(LLimit), () => LLimit, t => { LLimit = t.As<REAL>(); });
        yield return new Member(nameof(LSeverity), () => LSeverity, t => { LSeverity = t.As<DINT>(); });
        yield return new Member(nameof(LLLimit), () => LLLimit, t => { LLLimit = t.As<REAL>(); });
        yield return new Member(nameof(LLSeverity), () => LLSeverity, t => { LLSeverity = t.As<DINT>(); });
        yield return new Member(nameof(MinDurationPRE), () => MinDurationPRE, t => { MinDurationPRE = t.As<DINT>(); });
        yield return new Member(nameof(ShelveDuration), () => ShelveDuration, t => { ShelveDuration = t.As<DINT>(); });
        yield return new Member(nameof(MaxShelveDuration), () => MaxShelveDuration,
            t => { MaxShelveDuration = t.As<DINT>(); });
        yield return new Member(nameof(Deadband), () => Deadband, t => { Deadband = t.As<REAL>(); });
        yield return new Member(nameof(ROCPosLimit), () => ROCPosLimit, t => { ROCPosLimit = t.As<REAL>(); });
        yield return new Member(nameof(ROCPosSeverity), () => ROCPosSeverity, t => { ROCPosSeverity = t.As<DINT>(); });
        yield return new Member(nameof(ROCNegLimit), () => ROCNegLimit, t => { ROCNegLimit = t.As<REAL>(); });
        yield return new Member(nameof(ROCNegSeverity), () => ROCNegSeverity, t => { ROCNegSeverity = t.As<DINT>(); });
        yield return new Member(nameof(ROCPeriod), () => ROCPeriod, t => { ROCPeriod = t.As<REAL>(); });
    }
}