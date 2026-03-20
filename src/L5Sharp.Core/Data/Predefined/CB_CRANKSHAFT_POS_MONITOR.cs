using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CB_CRANKSHAFT_POS_MONITOR</c> data type structure.
/// </summary>
[LogixData("CB_CRANKSHAFT_POS_MONITOR")]
public sealed partial class CB_CRANKSHAFT_POS_MONITOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CB_CRANKSHAFT_POS_MONITOR"/> instance initialized with default values.
    /// </summary>
    public CB_CRANKSHAFT_POS_MONITOR() : base("CB_CRANKSHAFT_POS_MONITOR")
    {
        EnableIn = new BOOL();
        CamProfile = new BOOL();
        Enable = new BOOL();
        BrakeCam = new BOOL();
        TakeoverCam = new BOOL();
        DynamicCam = new BOOL();
        InputStatus = new BOOL();
        Reverse = new BOOL();
        Reset = new BOOL();
        PressMotionStatus = new BOOL();
        EnableOut = new BOOL();
        TZ = new BOOL();
        DZ = new BOOL();
        UZ = new BOOL();
        FP = new BOOL();
        SlideZone = new DINT();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CB_CRANKSHAFT_POS_MONITOR"/> instance initialized with the provided element.
    /// </summary>
    public CB_CRANKSHAFT_POS_MONITOR(XElement element) : base(element)
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
        CamProfile.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Enable.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        BrakeCam.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        TakeoverCam.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        DynamicCam.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Reverse.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        PressMotionStatus.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        EnableOut.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        TZ.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        DZ.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        UZ.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        FP.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        SlideZone.UpdateData(data, offset + 10);
        FaultCode.UpdateData(data, offset + 14);
        DiagnosticCode.UpdateData(data, offset + 18);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CamProfile</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL CamProfile
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Enable</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL Enable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BrakeCam</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL BrakeCam
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TakeoverCam</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL TakeoverCam
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DynamicCam</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL DynamicCam
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reverse</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL Reverse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PressMotionStatus</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL PressMotionStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TZ</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL TZ
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DZ</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL DZ
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UZ</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL UZ
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SlideZone</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public DINT SlideZone
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="CB_CRANKSHAFT_POS_MONITOR"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}