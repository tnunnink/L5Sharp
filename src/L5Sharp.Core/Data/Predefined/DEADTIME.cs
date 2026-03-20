using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DEADTIME</c> data type structure.
/// </summary>
[LogixData("DEADTIME")]
public sealed partial class DEADTIME : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DEADTIME"/> instance initialized with default values.
    /// </summary>
    public DEADTIME() : base("DEADTIME")
    {
        EnableIn = new BOOL();
        In = new REAL();
        InFault = new BOOL();
        Deadtime = new REAL();
        Gain = new REAL();
        Bias = new REAL();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        DeltaT = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        InFaulted = new BOOL();
        DeadtimeInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="DEADTIME"/> instance initialized with the provided element.
    /// </summary>
    public DEADTIME(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 112;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In.UpdateData(data, offset + 5);
        InFault.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        Deadtime.UpdateData(data, offset + 9);
        Gain.UpdateData(data, offset + 13);
        Bias.UpdateData(data, offset + 17);
        TimingMode.UpdateData(data, offset + 21);
        OversampleDT.UpdateData(data, offset + 25);
        RTSTime.UpdateData(data, offset + 29);
        RTSTimeStamp.UpdateData(data, offset + 33);
        EnableOut.UpdateData((data[offset + 41] & (1 << 2)) != 0);
        Out.UpdateData(data, offset + 41);
        DeltaT.UpdateData(data, offset + 45);
        Status.UpdateData(data, offset + 49);
        InstructFault.UpdateData((data[offset + 53] & (1 << 3)) != 0);
        InFaulted.UpdateData((data[offset + 53] & (1 << 4)) != 0);
        DeadtimeInv.UpdateData((data[offset + 53] & (1 << 5)) != 0);
        TimingModeInv.UpdateData((data[offset + 53] & (1 << 6)) != 0);
        RTSMissed.UpdateData((data[offset + 53] & (1 << 7)) != 0);
        RTSTimeInv.UpdateData((data[offset + 54] & (1 << 0)) != 0);
        RTSTimeStampInv.UpdateData((data[offset + 54] & (1 << 1)) != 0);
        DeltaTInv.UpdateData((data[offset + 54] & (1 << 2)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFault</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Deadtime</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public REAL Deadtime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public REAL Gain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Bias</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public REAL Bias
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFaulted</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL InFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeadtimeInv</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL DeadtimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="DEADTIME"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}