using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>POSITION_PROP</c> data type structure.
/// </summary>
[LogixData("POSITION_PROP")]
public sealed partial class POSITION_PROP : StructureData
{
    /// <summary>
    /// Creates a new <see cref="POSITION_PROP"/> instance initialized with default values.
    /// </summary>
    public POSITION_PROP() : base("POSITION_PROP")
    {
        EnableIn = new BOOL();
        SP = new REAL();
        Position = new REAL();
        OpenedFB = new BOOL();
        ClosedFB = new BOOL();
        PositionEUMax = new REAL();
        PositionEUMin = new REAL();
        CycleTime = new REAL();
        OpenRate = new REAL();
        CloseRate = new REAL();
        MaxOnTime = new REAL();
        MinOnTime = new REAL();
        Deadtime = new REAL();
        EnableOut = new BOOL();
        OpenOut = new BOOL();
        CloseOut = new BOOL();
        PositionPercent = new REAL();
        SPPercent = new REAL();
        OpenTime = new REAL();
        CloseTime = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        CycleTimeInv = new BOOL();
        OpenRateInv = new BOOL();
        CloseRateInv = new BOOL();
        MaxOnTimeInv = new BOOL();
        MinOnTimeInv = new BOOL();
        DeadtimeInv = new BOOL();
        PositionPctInv = new BOOL();
        SPPercentInv = new BOOL();
        PositionSpanInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="POSITION_PROP"/> instance initialized with the provided element.
    /// </summary>
    public POSITION_PROP(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Position</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL Position
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenedFB</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL OpenedFB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ClosedFB</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL ClosedFB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionEUMax</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL PositionEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionEUMin</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL PositionEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CycleTime</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL CycleTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenRate</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL OpenRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CloseRate</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL CloseRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxOnTime</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL MaxOnTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinOnTime</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL MinOnTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Deadtime</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL Deadtime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenOut</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL OpenOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CloseOut</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL CloseOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionPercent</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL PositionPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPPercent</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL SPPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenTime</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL OpenTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CloseTime</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public REAL CloseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CycleTimeInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL CycleTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenRateInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL OpenRateInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CloseRateInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL CloseRateInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxOnTimeInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL MaxOnTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinOnTimeInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL MinOnTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeadtimeInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL DeadtimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionPctInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL PositionPctInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPPercentInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL SPPercentInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionSpanInv</c> member of the <see cref="POSITION_PROP"/> data type.
    /// </summary>
    public BOOL PositionSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}