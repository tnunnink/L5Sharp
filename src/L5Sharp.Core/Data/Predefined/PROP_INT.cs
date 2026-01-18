using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PROP_INT</c> data type structure.
/// </summary>
[LogixData("PROP_INT")]
public sealed partial class PROP_INT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PROP_INT"/> instance initialized with default values.
    /// </summary>
    public PROP_INT() : base("PROP_INT")
    {
        EnableIn = new BOOL();
        In = new REAL();
        Initialize = new BOOL();
        InitialValue = new REAL();
        Kp = new REAL();
        Wld = new REAL();
        HighLimit = new REAL();
        LowLimit = new REAL();
        HoldHigh = new BOOL();
        HoldLow = new BOOL();
        ShapeKpPlus = new REAL();
        ShapeKpMinus = new REAL();
        KpInRange = new REAL();
        ShapeWldPlus = new REAL();
        ShapeWldMinus = new REAL();
        WldInRange = new REAL();
        NonLinearMode = new BOOL();
        ParabolicLinear = new BOOL();
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
        KpInv = new BOOL();
        WldInv = new BOOL();
        HighLowLimsInv = new BOOL();
        ShapeKpPlusInv = new BOOL();
        ShapeKpMinusInv = new BOOL();
        KpInRangeInv = new BOOL();
        ShapeWldPlusInv = new BOOL();
        ShapeWldMinusInv = new BOOL();
        WldInRangeInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="PROP_INT"/> instance initialized with the provided element.
    /// </summary>
    public PROP_INT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL InitialValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Kp</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL Kp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Wld</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL Wld
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLimit</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL HighLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowLimit</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL LowLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldHigh</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL HoldHigh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldLow</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL HoldLow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeKpPlus</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL ShapeKpPlus
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeKpMinus</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL ShapeKpMinus
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>KpInRange</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL KpInRange
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeWldPlus</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL ShapeWldPlus
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeWldMinus</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL ShapeWldMinus
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WldInRange</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL WldInRange
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NonLinearMode</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL NonLinearMode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ParabolicLinear</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL ParabolicLinear
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighAlarm</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL HighAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowAlarm</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL LowAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>KpInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL KpInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WldInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL WldInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLowLimsInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL HighLowLimsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeKpPlusInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL ShapeKpPlusInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeKpMinusInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL ShapeKpMinusInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>KpInRangeInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL KpInRangeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeWldPlusInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL ShapeWldPlusInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShapeWldMinusInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL ShapeWldMinusInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WldInRangeInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL WldInRangeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="PROP_INT"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}