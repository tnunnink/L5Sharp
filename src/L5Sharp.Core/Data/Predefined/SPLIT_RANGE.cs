using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SPLIT_RANGE</c> data type structure.
/// </summary>
[LogixData("SPLIT_RANGE")]
public sealed partial class SPLIT_RANGE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SPLIT_RANGE"/> instance initialized with default values.
    /// </summary>
    public SPLIT_RANGE() : base("SPLIT_RANGE")
    {
        EnableIn = new BOOL();
        In = new REAL();
        CycleTime = new REAL();
        MaxHeatIn = new REAL();
        MinHeatIn = new REAL();
        MaxCoolIn = new REAL();
        MinCoolIn = new REAL();
        MaxHeatTime = new REAL();
        MinHeatTime = new REAL();
        MaxCoolTime = new REAL();
        MinCoolTime = new REAL();
        EnableOut = new BOOL();
        HeatOut = new BOOL();
        CoolOut = new BOOL();
        HeatTimePercent = new REAL();
        CoolTimePercent = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        CycleTimeInv = new BOOL();
        MaxHeatTimeInv = new BOOL();
        MinHeatTimeInv = new BOOL();
        MaxCoolTimeInv = new BOOL();
        MinCoolTimeInv = new BOOL();
        HeatSpanInv = new BOOL();
        CoolSpanInv = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="SPLIT_RANGE"/> instance initialized with the provided element.
    /// </summary>
    public SPLIT_RANGE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CycleTime</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL CycleTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxHeatIn</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MaxHeatIn
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinHeatIn</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MinHeatIn
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxCoolIn</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MaxCoolIn
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinCoolIn</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MinCoolIn
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxHeatTime</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MaxHeatTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinHeatTime</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MinHeatTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxCoolTime</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MaxCoolTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinCoolTime</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL MinCoolTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HeatOut</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL HeatOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CoolOut</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL CoolOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HeatTimePercent</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL HeatTimePercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CoolTimePercent</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public REAL CoolTimePercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CycleTimeInv</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL CycleTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxHeatTimeInv</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL MaxHeatTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinHeatTimeInv</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL MinHeatTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxCoolTimeInv</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL MaxCoolTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinCoolTimeInv</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL MinCoolTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HeatSpanInv</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL HeatSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CoolSpanInv</c> member of the <see cref="SPLIT_RANGE"/> data type.
    /// </summary>
    public BOOL CoolSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
