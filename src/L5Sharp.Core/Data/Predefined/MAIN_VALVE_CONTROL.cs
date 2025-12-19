using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MAIN_VALVE_CONTROL</c> data type structure.
/// </summary>
[LogixData("MAIN_VALVE_CONTROL")]
public sealed partial class MAIN_VALVE_CONTROL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MAIN_VALVE_CONTROL"/> instance initialized with default values.
    /// </summary>
    public MAIN_VALVE_CONTROL() : base("MAIN_VALVE_CONTROL")
    {
        EnableIn = new BOOL();
        Actuate = new BOOL();
        FeedbackType = new BOOL();
        Feedback1 = new BOOL();
        Feedback2 = new BOOL();
        InputStatus = new BOOL();
        OutputStatus = new BOOL();
        Reset = new BOOL();
        FeedbackReactionTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        O2 = new BOOL();
        FP = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="MAIN_VALVE_CONTROL"/> instance initialized with the provided element.
    /// </summary>
    public MAIN_VALVE_CONTROL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Actuate</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Actuate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackType</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL FeedbackType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Feedback1</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Feedback1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Feedback2</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Feedback2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutputStatus</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL OutputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackReactionTime</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT FeedbackReactionTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O2</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL O2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="MAIN_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
