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
    }

    /// <inheritdoc />
    public ALARM_ANALOG(XElement element) : base(element)
    {
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
}