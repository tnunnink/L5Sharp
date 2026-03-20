using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>AUX_VALVE_CONTROL</c> data type structure.
/// </summary>
[LogixData("AUX_VALVE_CONTROL")]
public sealed partial class AUX_VALVE_CONTROL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="AUX_VALVE_CONTROL"/> instance initialized with default values.
    /// </summary>
    public AUX_VALVE_CONTROL() : base("AUX_VALVE_CONTROL")
    {
        EnableIn = new BOOL();
        Actuate = new BOOL();
        DelayType = new BOOL();
        OutputFollowsActuate = new BOOL();
        DelayEnable = new BOOL();
        FeedbackType = new BOOL();
        Feedback1 = new BOOL();
        InputStatus = new BOOL();
        OutputStatus = new BOOL();
        Reset = new BOOL();
        DelayTime = new DINT();
        FeedbackReactionTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="AUX_VALVE_CONTROL"/> instance initialized with the provided element.
    /// </summary>
    public AUX_VALVE_CONTROL(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 68;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Actuate.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        DelayType.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        OutputFollowsActuate.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        DelayEnable.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        FeedbackType.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Feedback1.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        OutputStatus.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        DelayTime.UpdateData(data, offset + 6);
        FeedbackReactionTime.UpdateData(data, offset + 10);
        EnableOut.UpdateData((data[offset + 18] & (1 << 2)) != 0);
        O1.UpdateData((data[offset + 18] & (1 << 3)) != 0);
        FP.UpdateData((data[offset + 18] & (1 << 4)) != 0);
        FaultCode.UpdateData(data, offset + 18);
        DiagnosticCode.UpdateData(data, offset + 22);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Actuate</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Actuate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DelayType</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL DelayType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutputFollowsActuate</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL OutputFollowsActuate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DelayEnable</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL DelayEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackType</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL FeedbackType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Feedback1</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Feedback1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutputStatus</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL OutputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DelayTime</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT DelayTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackReactionTime</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT FeedbackReactionTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="AUX_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}