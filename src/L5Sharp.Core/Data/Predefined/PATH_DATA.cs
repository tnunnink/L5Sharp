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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 68;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        InterpolationType.UpdateData(data, offset + 4);
        Position.UpdateData(data, offset + 8);
        RobotConfiguration.UpdateData(data, offset + 44);
        TurnsCounters.UpdateData(data, offset + 48);
        MoveType.UpdateData(data, offset + 56);
        TerminationType.UpdateData(data, offset + 60);
        CommandToleranceLinear.UpdateData(data, offset + 64);
        
        return offset + GetSize();
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