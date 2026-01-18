using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>S_CURVE</c> data type structure.
/// </summary>
[LogixData("S_CURVE")]
public sealed partial class S_CURVE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="S_CURVE"/> instance initialized with default values.
    /// </summary>
    public S_CURVE() : base("S_CURVE")
    {
        EnableIn = new BOOL();
        In = new REAL();
        Initialize = new BOOL();
        InitialValue = new REAL();
        AbsAlgRamp = new BOOL();
        AccelRate = new REAL();
        DecelRate = new REAL();
        JerkRate = new REAL();
        HoldMode = new BOOL();
        HoldEnable = new BOOL();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        EnableOut = new BOOL();
        S_Mode = new BOOL();
        Out = new REAL();
        Rate = new REAL();
        DeltaT = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        AccelRateInv = new BOOL();
        DecelRateInv = new BOOL();
        JerkRateInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="S_CURVE"/> instance initialized with the provided element.
    /// </summary>
    public S_CURVE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL InitialValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AbsAlgRamp</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL AbsAlgRamp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelRate</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL AccelRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelRate</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL DecelRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>JerkRate</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL JerkRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldMode</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL HoldMode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldEnable</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL HoldEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>S_Mode</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL S_Mode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Rate</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL Rate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelRateInv</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL AccelRateInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelRateInv</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL DecelRateInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>JerkRateInv</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL JerkRateInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="S_CURVE"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}