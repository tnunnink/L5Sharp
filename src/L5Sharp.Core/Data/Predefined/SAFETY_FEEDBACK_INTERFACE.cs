using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFETY_FEEDBACK_INTERFACE</c> data type structure.
/// </summary>
[LogixData("SAFETY_FEEDBACK_INTERFACE")]
public sealed partial class SAFETY_FEEDBACK_INTERFACE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFETY_FEEDBACK_INTERFACE"/> instance initialized with default values.
    /// </summary>
    public SAFETY_FEEDBACK_INTERFACE() : base("SAFETY_FEEDBACK_INTERFACE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        TimeUnit = new BOOL();
        FeedbackValid = new BOOL();
        ConnectionFaulted = new BOOL();
        HomeTrigger = new BOOL();
        Reset = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        SFH = new BOOL();
        PositionScaling = new REAL();
        FeedbackResolution = new DINT();
        Unwind = new DINT();
        HomePosition = new REAL();
        FeedbackPosition = new DINT();
        FeedbackVelocity = new REAL();
        ActualPosition = new REAL();
        ActualCycles = new DINT();
        ActualSpeed = new REAL();
        FaultType = new SINT();
        DiagnosticCode = new SINT();
        PositionScalingOut = new REAL();
        UnwindOut = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SAFETY_FEEDBACK_INTERFACE"/> instance initialized with the provided element.
    /// </summary>
    public SAFETY_FEEDBACK_INTERFACE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimeUnit</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL TimeUnit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackValid</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL FeedbackValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ConnectionFaulted</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL ConnectionFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HomeTrigger</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL HomeTrigger
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SFH</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public BOOL SFH
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionScaling</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public REAL PositionScaling
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackResolution</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public DINT FeedbackResolution
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Unwind</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public DINT Unwind
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HomePosition</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public REAL HomePosition
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackPosition</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public DINT FeedbackPosition
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackVelocity</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public REAL FeedbackVelocity
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ActualPosition</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public REAL ActualPosition
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ActualCycles</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public DINT ActualCycles
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ActualSpeed</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public REAL ActualSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultType</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public SINT FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionScalingOut</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public REAL PositionScalingOut
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UnwindOut</c> member of the <see cref="SAFETY_FEEDBACK_INTERFACE"/> data type.
    /// </summary>
    public DINT UnwindOut
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}