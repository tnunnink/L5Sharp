using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PATH_DATA</c> data type structure.
/// </summary>
[LogixData("PATH_DATA")]
public sealed partial class PATH_DATA : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PATH_DATA"/> instance initialized with default values.
    /// </summary>
    public PATH_DATA() : base("PATH_DATA")
    {
        InterpolationType = new DINT();
        Position = new ArrayData<REAL>(9);
        RobotConfiguration = new DINT();
        TurnsCounters = new ArrayData<INT>(4);
        MoveType = new DINT();
        TerminationType = new DINT();
        CommandToleranceLinear = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="PATH_DATA"/> instance initialized with the provided element.
    /// </summary>
    public PATH_DATA(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>InterpolationType</c> member of the <see cref="PATH_DATA"/> data type.
    /// </summary>
    public DINT InterpolationType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Position</c> member of the <see cref="PATH_DATA"/> data type.
    /// </summary>
    public ArrayData<REAL> Position
    {
        get => GetArray<REAL>();
        set => SetArray(value);
    }

    /// <summary>
    /// The <c>RobotConfiguration</c> member of the <see cref="PATH_DATA"/> data type.
    /// </summary>
    public DINT RobotConfiguration
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TurnsCounters</c> member of the <see cref="PATH_DATA"/> data type.
    /// </summary>
    public ArrayData<INT> TurnsCounters
    {
        get => GetArray<INT>();
        set => SetArray(value);
    }

    /// <summary>
    /// The <c>MoveType</c> member of the <see cref="PATH_DATA"/> data type.
    /// </summary>
    public DINT MoveType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TerminationType</c> member of the <see cref="PATH_DATA"/> data type.
    /// </summary>
    public DINT TerminationType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CommandToleranceLinear</c> member of the <see cref="PATH_DATA"/> data type.
    /// </summary>
    public REAL CommandToleranceLinear
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}