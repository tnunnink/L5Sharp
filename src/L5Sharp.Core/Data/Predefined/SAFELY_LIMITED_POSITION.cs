using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFELY_LIMITED_POSITION</c> data type structure.
/// </summary>
[LogixData("SAFELY_LIMITED_POSITION")]
public sealed partial class SAFELY_LIMITED_POSITION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFELY_LIMITED_POSITION"/> instance initialized with default values.
    /// </summary>
    public SAFELY_LIMITED_POSITION() : base("SAFELY_LIMITED_POSITION")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        RestartType = new BOOL();
        ColdStartType = new BOOL();
        Request = new BOOL();
        Reset = new BOOL();
        O1 = new BOOL();
        RR = new BOOL();
        FP = new BOOL();
        CheckDelayActive = new BOOL();
        CheckDelay = new INT();
        PositiveTravelLimit = new REAL();
        NegativeTravelLimit = new REAL();
        FaultType = new SINT();
        DiagnosticCode = new SINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SAFELY_LIMITED_POSITION"/> instance initialized with the provided element.
    /// </summary>
    public SAFELY_LIMITED_POSITION(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 48;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        RestartType.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        ColdStartType.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Request.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Reset.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        O1.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        RR.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        FP.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        CheckDelayActive.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        CheckDelay.UpdateData(data, offset + 6);
        PositiveTravelLimit.UpdateData(data, offset + 8);
        NegativeTravelLimit.UpdateData(data, offset + 12);
        FaultType.UpdateData(data, offset + 16);
        DiagnosticCode.UpdateData(data, offset + 17);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Request</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL Request
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RR</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL RR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelayActive</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public BOOL CheckDelayActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelay</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public INT CheckDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositiveTravelLimit</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public REAL PositiveTravelLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NegativeTravelLimit</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public REAL NegativeTravelLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultType</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public SINT FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFELY_LIMITED_POSITION"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }
}