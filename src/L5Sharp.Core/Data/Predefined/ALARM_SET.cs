using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>ALARM_SET</c> data type structure.
/// </summary>
[LogixData("ALARM_SET")]
public sealed partial class ALARM_SET : StructureData
{
    /// <summary>
    /// Creates a new <see cref="ALARM_SET"/> instance initialized with default values.
    /// </summary>
    public ALARM_SET() : base("ALARM_SET")
    {
        InAlarmUnackedCount = new DINT();
        InAlarmAckedCount = new DINT();
        NormalUnackedCount = new DINT();
        SuppressedCount = new DINT();
        ShelvedCount = new DINT();
        DisabledCount = new DINT();
        HasUnackedAlarm = new BOOL();
        HighestSeverity = new DINT();
        HighestSeverityAlarmName = new STRING();
        HighestSeverityAlarmType = new STRING();
    }
    
    /// <summary>
    /// Creates a new <see cref="ALARM_SET"/> instance initialized with the provided element.
    /// </summary>
    public ALARM_SET(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>InAlarmUnackedCount</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public DINT InAlarmUnackedCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InAlarmAckedCount</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public DINT InAlarmAckedCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NormalUnackedCount</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public DINT NormalUnackedCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SuppressedCount</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public DINT SuppressedCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShelvedCount</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public DINT ShelvedCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DisabledCount</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public DINT DisabledCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HasUnackedAlarm</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public BOOL HasUnackedAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighestSeverity</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public DINT HighestSeverity
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighestSeverityAlarmName</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public STRING HighestSeverityAlarmName
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighestSeverityAlarmType</c> member of the <see cref="ALARM_SET"/> data type.
    /// </summary>
    public STRING HighestSeverityAlarmType
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }
}