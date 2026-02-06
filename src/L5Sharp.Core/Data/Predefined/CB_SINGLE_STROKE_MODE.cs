using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CB_SINGLE_STROKE_MODE</c> data type structure.
/// </summary>
[LogixData("CB_SINGLE_STROKE_MODE")]
public sealed partial class CB_SINGLE_STROKE_MODE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CB_SINGLE_STROKE_MODE"/> instance initialized with default values.
    /// </summary>
    public CB_SINGLE_STROKE_MODE() : base("CB_SINGLE_STROKE_MODE")
    {
        EnableIn = new BOOL();
        AckType = new BOOL();
        TakeoverMode = new BOOL();
        Enable = new BOOL();
        SafetyEnable = new BOOL();
        StandardEnable = new BOOL();
        Start = new BOOL();
        PressInMotion = new BOOL();
        MotionMonitorFault = new BOOL();
        SafetyEnableAck = new BOOL();
        SlideZone = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CB_SINGLE_STROKE_MODE"/> instance initialized with the provided element.
    /// </summary>
    public CB_SINGLE_STROKE_MODE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 36;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        AckType.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        TakeoverMode.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Enable.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        SafetyEnable.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        StandardEnable.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Start.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        PressInMotion.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        MotionMonitorFault.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        SafetyEnableAck.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        SlideZone.UpdateData(data, offset + 6);
        EnableOut.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        O1.UpdateData((data[offset + 14] & (1 << 3)) != 0);
        DiagnosticCode.UpdateData(data, offset + 14);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AckType</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL AckType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TakeoverMode</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL TakeoverMode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Enable</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL Enable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SafetyEnable</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL SafetyEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandardEnable</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL StandardEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Start</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PressInMotion</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL PressInMotion
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MotionMonitorFault</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL MotionMonitorFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SafetyEnableAck</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL SafetyEnableAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SlideZone</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public DINT SlideZone
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="CB_SINGLE_STROKE_MODE"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}