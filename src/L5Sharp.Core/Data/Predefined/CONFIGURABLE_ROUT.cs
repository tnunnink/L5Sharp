using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CONFIGURABLE_ROUT</c> data type structure.
/// </summary>
[LogixData("CONFIGURABLE_ROUT")]
public sealed partial class CONFIGURABLE_ROUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CONFIGURABLE_ROUT"/> instance initialized with default values.
    /// </summary>
    public CONFIGURABLE_ROUT() : base("CONFIGURABLE_ROUT")
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
    /// Creates a new <see cref="CONFIGURABLE_ROUT"/> instance initialized with the provided element.
    /// </summary>
    public CONFIGURABLE_ROUT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 52;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Actuate.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        FeedbackType.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Feedback1.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Feedback2.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        OutputStatus.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Reset.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        FeedbackReactionTime.UpdateData(data, offset + 5);
        EnableOut.UpdateData((data[offset + 14] & (1 << 0)) != 0);
        O1.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        O2.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        FP.UpdateData((data[offset + 14] & (1 << 3)) != 0);
        FaultCode.UpdateData(data, offset + 14);
        DiagnosticCode.UpdateData(data, offset + 18);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Actuate</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL Actuate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackType</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL FeedbackType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Feedback1</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL Feedback1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Feedback2</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL Feedback2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutputStatus</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL OutputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackReactionTime</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public DINT FeedbackReactionTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O2</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL O2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="CONFIGURABLE_ROUT"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}