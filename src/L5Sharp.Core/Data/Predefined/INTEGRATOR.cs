using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>INTEGRATOR</c> data type structure.
/// </summary>
[LogixData("INTEGRATOR")]
public sealed partial class INTEGRATOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="INTEGRATOR"/> instance initialized with default values.
    /// </summary>
    public INTEGRATOR() : base("INTEGRATOR")
    {
        EnableIn = new BOOL();
        In = new REAL();
        Initialize = new BOOL();
        InitialValue = new REAL();
        IGain = new REAL();
        HighLimit = new REAL();
        LowLimit = new REAL();
        HoldHigh = new BOOL();
        HoldLow = new BOOL();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        HighAlarm = new BOOL();
        LowAlarm = new BOOL();
        DeltaT = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        IGainInv = new BOOL();
        HighLowLimsInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="INTEGRATOR"/> instance initialized with the provided element.
    /// </summary>
    public INTEGRATOR(XElement element) : base(element)
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
        Initialize.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        InitialValue.UpdateData(data, offset + 9);
        IGain.UpdateData(data, offset + 13);
        HighLimit.UpdateData(data, offset + 17);
        LowLimit.UpdateData(data, offset + 21);
        HoldHigh.UpdateData((data[offset + 25] & (1 << 2)) != 0);
        HoldLow.UpdateData((data[offset + 25] & (1 << 3)) != 0);
        TimingMode.UpdateData(data, offset + 25);
        OversampleDT.UpdateData(data, offset + 29);
        RTSTime.UpdateData(data, offset + 33);
        RTSTimeStamp.UpdateData(data, offset + 37);
        EnableOut.UpdateData((data[offset + 45] & (1 << 4)) != 0);
        Out.UpdateData(data, offset + 45);
        HighAlarm.UpdateData((data[offset + 49] & (1 << 5)) != 0);
        LowAlarm.UpdateData((data[offset + 49] & (1 << 6)) != 0);
        DeltaT.UpdateData(data, offset + 49);
        Status.UpdateData(data, offset + 53);
        InstructFault.UpdateData((data[offset + 57] & (1 << 7)) != 0);
        IGainInv.UpdateData((data[offset + 58] & (1 << 0)) != 0);
        HighLowLimsInv.UpdateData((data[offset + 58] & (1 << 1)) != 0);
        TimingModeInv.UpdateData((data[offset + 58] & (1 << 2)) != 0);
        RTSMissed.UpdateData((data[offset + 58] & (1 << 3)) != 0);
        RTSTimeInv.UpdateData((data[offset + 58] & (1 << 4)) != 0);
        RTSTimeStampInv.UpdateData((data[offset + 58] & (1 << 5)) != 0);
        DeltaTInv.UpdateData((data[offset + 58] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL InitialValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IGain</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL IGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLimit</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL HighLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowLimit</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL LowLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldHigh</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL HoldHigh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldLow</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL HoldLow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighAlarm</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL HighAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowAlarm</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL LowAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IGainInv</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL IGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLowLimsInv</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL HighLowLimsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="INTEGRATOR"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}