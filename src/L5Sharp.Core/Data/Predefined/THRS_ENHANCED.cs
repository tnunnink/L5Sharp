using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>THRS_ENHANCED</c> data type structure.
/// </summary>
[LogixData("THRS_ENHANCED")]
public sealed partial class THRS_ENHANCED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="THRS_ENHANCED"/> instance initialized with default values.
    /// </summary>
    public THRS_ENHANCED() : base("THRS_ENHANCED")
    {
        EnableIn = new BOOL();
        Enable = new BOOL();
        Disconnected = new BOOL();
        RightButtonNormallyOpen = new BOOL();
        RightButtonNormallyClosed = new BOOL();
        LeftButtonNormallyOpen = new BOOL();
        LeftButtonNormallyClosed = new BOOL();
        InputStatus = new BOOL();
        Reset = new BOOL();
        DiscrepancyTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        BR = new BOOL();
        SB = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="THRS_ENHANCED"/> instance initialized with the provided element.
    /// </summary>
    public THRS_ENHANCED(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 92;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Enable.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Disconnected.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        RightButtonNormallyOpen.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        RightButtonNormallyClosed.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        LeftButtonNormallyOpen.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        LeftButtonNormallyClosed.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        DiscrepancyTime.UpdateData(data, offset + 6);
        EnableOut.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        O1.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        FP.UpdateData((data[offset + 14] & (1 << 3)) != 0);
        BR.UpdateData((data[offset + 14] & (1 << 4)) != 0);
        SB.UpdateData((data[offset + 14] & (1 << 5)) != 0);
        FaultCode.UpdateData(data, offset + 14);
        DiagnosticCode.UpdateData(data, offset + 18);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Enable</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL Enable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Disconnected</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL Disconnected
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RightButtonNormallyOpen</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL RightButtonNormallyOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RightButtonNormallyClosed</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL RightButtonNormallyClosed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LeftButtonNormallyOpen</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL LeftButtonNormallyOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LeftButtonNormallyClosed</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL LeftButtonNormallyClosed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiscrepancyTime</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public DINT DiscrepancyTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BR</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL BR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SB</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL SB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}