using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 108;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In.UpdateData(data, offset + 5);
        CycleTime.UpdateData(data, offset + 9);
        MaxHeatIn.UpdateData(data, offset + 13);
        MinHeatIn.UpdateData(data, offset + 17);
        MaxCoolIn.UpdateData(data, offset + 21);
        MinCoolIn.UpdateData(data, offset + 25);
        MaxHeatTime.UpdateData(data, offset + 29);
        MinHeatTime.UpdateData(data, offset + 33);
        MaxCoolTime.UpdateData(data, offset + 37);
        MinCoolTime.UpdateData(data, offset + 41);
        EnableOut.UpdateData((data[offset + 49] & (1 << 1)) != 0);
        HeatOut.UpdateData((data[offset + 49] & (1 << 2)) != 0);
        CoolOut.UpdateData((data[offset + 49] & (1 << 3)) != 0);
        HeatTimePercent.UpdateData(data, offset + 49);
        CoolTimePercent.UpdateData(data, offset + 53);
        Status.UpdateData(data, offset + 57);
        InstructFault.UpdateData((data[offset + 61] & (1 << 4)) != 0);
        CycleTimeInv.UpdateData((data[offset + 61] & (1 << 5)) != 0);
        MaxHeatTimeInv.UpdateData((data[offset + 61] & (1 << 6)) != 0);
        MinHeatTimeInv.UpdateData((data[offset + 61] & (1 << 7)) != 0);
        MaxCoolTimeInv.UpdateData((data[offset + 62] & (1 << 0)) != 0);
        MinCoolTimeInv.UpdateData((data[offset + 62] & (1 << 1)) != 0);
        HeatSpanInv.UpdateData((data[offset + 62] & (1 << 2)) != 0);
        CoolSpanInv.UpdateData((data[offset + 62] & (1 << 3)) != 0);
        
        return offset + GetSize();
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