using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEC_ORDER_CONTROLLER</c> data type structure.
/// </summary>
[LogixData("SEC_ORDER_CONTROLLER")]
public sealed partial class SEC_ORDER_CONTROLLER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEC_ORDER_CONTROLLER"/> instance initialized with default values.
    /// </summary>
    public SEC_ORDER_CONTROLLER() : base("SEC_ORDER_CONTROLLER")
    {
        EnableIn = new BOOL();
        In = new REAL();
        Initialize = new BOOL();
        InitialValue = new REAL();
        Gain = new REAL();
        WLag = new REAL();
        WLead = new REAL();
        ZetaLead = new REAL();
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
        GainInv = new BOOL();
        WLagInv = new BOOL();
        WLeadInv = new BOOL();
        ZetaLeadInv = new BOOL();
        HighLowLimsInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEC_ORDER_CONTROLLER"/> instance initialized with the provided element.
    /// </summary>
    public SEC_ORDER_CONTROLLER(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL InitialValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL Gain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLag</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL WLag
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLead</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL WLead
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZetaLead</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL ZetaLead
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLimit</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL HighLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowLimit</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL LowLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldHigh</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL HoldHigh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldLow</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL HoldLow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighAlarm</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL HighAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowAlarm</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL LowAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GainInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL GainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLagInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL WLagInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLeadInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL WLeadInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZetaLeadInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL ZetaLeadInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLowLimsInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL HighLowLimsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="SEC_ORDER_CONTROLLER"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}