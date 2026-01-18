using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>LEAD_LAG_SEC_ORDER</c> data type structure.
/// </summary>
[LogixData("LEAD_LAG_SEC_ORDER")]
public sealed partial class LEAD_LAG_SEC_ORDER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="LEAD_LAG_SEC_ORDER"/> instance initialized with default values.
    /// </summary>
    public LEAD_LAG_SEC_ORDER() : base("LEAD_LAG_SEC_ORDER")
    {
        EnableIn = new BOOL();
        In = new REAL();
        Initialize = new BOOL();
        WLead = new REAL();
        WLag = new REAL();
        ZetaLead = new REAL();
        ZetaLag = new REAL();
        Order = new DINT();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        DeltaT = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        WLeadInv = new BOOL();
        WLagInv = new BOOL();
        ZetaLeadInv = new BOOL();
        ZetaLagInv = new BOOL();
        OrderInv = new BOOL();
        WLagRatioInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="LEAD_LAG_SEC_ORDER"/> instance initialized with the provided element.
    /// </summary>
    public LEAD_LAG_SEC_ORDER(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLead</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL WLead
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLag</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL WLag
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZetaLead</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL ZetaLead
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZetaLag</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL ZetaLag
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Order</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public DINT Order
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLeadInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL WLeadInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLagInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL WLagInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZetaLeadInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL ZetaLeadInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZetaLagInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL ZetaLagInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OrderInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL OrderInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WLagRatioInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL WLagRatioInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="LEAD_LAG_SEC_ORDER"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}