using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFE_BRAKE_CONTROL</c> data type structure.
/// </summary>
[LogixData("SAFE_BRAKE_CONTROL")]
public sealed partial class SAFE_BRAKE_CONTROL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFE_BRAKE_CONTROL"/> instance initialized with default values.
    /// </summary>
    public SAFE_BRAKE_CONTROL() : base("SAFE_BRAKE_CONTROL")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        RestartType = new BOOL();
        BrakeFeedback1 = new BOOL();
        BrakeFeedback2 = new BOOL();
        InputStatus = new BOOL();
        OutputStatus = new BOOL();
        BrakeEngageL = new BOOL();
        Reset = new BOOL();
        BO1 = new BOOL();
        BO2 = new BOOL();
        TOR = new BOOL();
        RR = new BOOL();
        FP = new BOOL();
        STOtoSBCDelayActive = new BOOL();
        FdbkONChkDlyTimerActive = new BOOL();
        FdbkOFFChkDlyTimerActive = new BOOL();
        STOtoSBCDelay = new INT();
        BrakeFeedbackCheckDelay = new INT();
        FaultType = new SINT();
        DiagnosticCode = new SINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SAFE_BRAKE_CONTROL"/> instance initialized with the provided element.
    /// </summary>
    public SAFE_BRAKE_CONTROL(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 56;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        RestartType.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        BrakeFeedback1.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        BrakeFeedback2.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        OutputStatus.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        BrakeEngageL.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        BO1.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        BO2.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        TOR.UpdateData((data[offset + 6] & (1 << 3)) != 0);
        RR.UpdateData((data[offset + 6] & (1 << 4)) != 0);
        FP.UpdateData((data[offset + 6] & (1 << 5)) != 0);
        STOtoSBCDelayActive.UpdateData((data[offset + 6] & (1 << 6)) != 0);
        FdbkONChkDlyTimerActive.UpdateData((data[offset + 6] & (1 << 7)) != 0);
        FdbkOFFChkDlyTimerActive.UpdateData((data[offset + 7] & (1 << 0)) != 0);
        STOtoSBCDelay.UpdateData(data, offset + 7);
        BrakeFeedbackCheckDelay.UpdateData(data, offset + 9);
        FaultType.UpdateData(data, offset + 11);
        DiagnosticCode.UpdateData(data, offset + 12);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BrakeFeedback1</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL BrakeFeedback1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BrakeFeedback2</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL BrakeFeedback2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutputStatus</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL OutputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BrakeEngageL</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL BrakeEngageL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BO1</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL BO1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BO2</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL BO2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TOR</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL TOR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RR</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL RR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>STOtoSBCDelayActive</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL STOtoSBCDelayActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FdbkONChkDlyTimerActive</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL FdbkONChkDlyTimerActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FdbkOFFChkDlyTimerActive</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public BOOL FdbkOFFChkDlyTimerActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>STOtoSBCDelay</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public INT STOtoSBCDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BrakeFeedbackCheckDelay</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public INT BrakeFeedbackCheckDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultType</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public SINT FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFE_BRAKE_CONTROL"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }
}