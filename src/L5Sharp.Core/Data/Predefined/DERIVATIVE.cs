using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DERIVATIVE</c> data type structure.
/// </summary>
[LogixData("DERIVATIVE")]
public sealed partial class DERIVATIVE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DERIVATIVE"/> instance initialized with default values.
    /// </summary>
    public DERIVATIVE() : base("DERIVATIVE")
    {
        EnableIn = new BOOL();
        In = new REAL();
        Gain = new REAL();
        ByPass = new BOOL();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        DeltaT = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="DERIVATIVE"/> instance initialized with the provided element.
    /// </summary>
    public DERIVATIVE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 88;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In.UpdateData(data, offset + 5);
        Gain.UpdateData(data, offset + 9);
        ByPass.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        TimingMode.UpdateData(data, offset + 13);
        OversampleDT.UpdateData(data, offset + 17);
        RTSTime.UpdateData(data, offset + 21);
        RTSTimeStamp.UpdateData(data, offset + 25);
        EnableOut.UpdateData((data[offset + 33] & (1 << 2)) != 0);
        Out.UpdateData(data, offset + 33);
        DeltaT.UpdateData(data, offset + 37);
        Status.UpdateData(data, offset + 41);
        InstructFault.UpdateData((data[offset + 45] & (1 << 3)) != 0);
        TimingModeInv.UpdateData((data[offset + 45] & (1 << 4)) != 0);
        RTSMissed.UpdateData((data[offset + 45] & (1 << 5)) != 0);
        RTSTimeInv.UpdateData((data[offset + 45] & (1 << 6)) != 0);
        RTSTimeStampInv.UpdateData((data[offset + 45] & (1 << 7)) != 0);
        DeltaTInv.UpdateData((data[offset + 46] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public REAL Gain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ByPass</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL ByPass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="DERIVATIVE"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}