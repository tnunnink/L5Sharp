using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming Logix naming
// ReSharper disable InconsistentNaming

namespace L5Sharp.Core;

/// <summary>
/// A predefined or built-in data type in Logix that is a part of the alarm instruction set.
/// </summary>
[LogixElement(L5XName.AlarmAnalogParameters)]
[LogixData(nameof(ALARM_ANALOG))]
public sealed class ALARM_ANALOG : StructureData
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
        HSeverity = 0;
        LLimit = 0;
        LSeverity = 0;
        LLLimit = 0;
        LLSeverity = 0;
        MinDurationPRE = 0;
        ShelveDuration = 0;
        MaxShelveDuration = 0;
        Deadband = 0;
        ROCPosLimit = 0;
        ROCPosSeverity = 0;
        ROCNegLimit = 0;
        ROCNegSeverity = 0;
        ROCPeriod = 0;
    }

    /// <inheritdoc />
    public ALARM_ANALOG(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override string Name => nameof(ALARM_ANALOG);

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => GenerateVirtualMembers();

    /// <summary>
    /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="In"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="InFault"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHEnabled
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HEnabled
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LEnabled
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLEnabled"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLEnabled
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AckRequired"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AckRequired
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgAckAll
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperAckAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperAckAll
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHProgAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HProgAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LProgAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLProgAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosProgAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegProgAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegProgAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperAck"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperAck
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgSuppress
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperSuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperSuppress
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgUnsuppress
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperUnsuppress"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperUnsuppress
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperShelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperShelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperShelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperShelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperShelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperShelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperShelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgUnshelveAll"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgUnshelveAll
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHOperUnshelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HOperUnshelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LOperUnshelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLOperUnshelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCPosOperUnshelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegOperUnshelve"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ROCNegOperUnshelve
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgDisable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperDisable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperDisable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProgEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL ProgEnable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="OperEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL OperEnable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AlarmCountReset"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL AlarmCountReset
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HHMinDurationEnable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL HMinDurationEnable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LMinDurationEnable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLMinDurationEnable"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public BOOL LLMinDurationEnable
    {
        get => GetRequiredValue(BOOL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL HHLimit
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HHSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT HHSeverity
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL HLimit
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="HSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT HSeverity
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL LLimit
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT LSeverity
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL LLLimit
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LLSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT LLSeverity
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MinDurationPRE"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT MinDurationPRE
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ShelveDuration
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="MaxShelveDuration"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT MaxShelveDuration
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL Deadband
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCPosLimit
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ROCPosSeverity
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCNegLimit
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegSeverity"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public DINT ROCNegSeverity
    {
        get => GetRequiredValue(DINT.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM_ANALOG"/> data type.
    /// </summary>
    public REAL ROCPeriod
    {
        get => GetRequiredValue(REAL.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Alarm data is formatted as attribute name/value pairs instead of nested element data structure.
    /// To make this compatible with our implementation of LogixData, we need to return "virtual" data value member elements
    /// that match the typical decorated format. We can derive the virtual element using the attribute name, value,
    /// and the defined properties of this class. The one issue is setting the underlying attribute. To support that,
    /// the attribute will be stored as an annotation on the virtual member element. <see cref="AtomicData"/> will inspect
    /// the element and if the attribute exists, will apply the update accordingly.
    /// </summary>
    private IEnumerable<LogixMember> GenerateVirtualMembers()
    {
        var propertyTypes = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => typeof(LogixData).IsAssignableFrom(p.PropertyType) && p.DeclaringType == GetType())
            .ToDictionary(p => p.Name, p => LogixType.NameFor(p.PropertyType));

        foreach (var attribute in Element.Attributes())
        {
            var member = new XElement(L5XName.DataValueMember);
            member.SetAttributeValue(L5XName.Name, attribute.Name.LocalName);
            member.SetAttributeValue(L5XName.DataType, propertyTypes[attribute.Name.LocalName]);
            member.SetAttributeValue(L5XName.Radix, Radix.Infer(attribute.Value));
            member.SetAttributeValue(L5XName.Value, attribute.Value);

            //This embeds the backing attribute on the in memory element so we can handle setting the value from atomic data.
            member.AddAnnotation(attribute);

            yield return new LogixMember(member);
        }
    }
}