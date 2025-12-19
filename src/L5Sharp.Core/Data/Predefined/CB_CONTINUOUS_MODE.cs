using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CB_CONTINUOUS_MODE</c> data type structure.
/// </summary>
[LogixData("CB_CONTINUOUS_MODE")]
public sealed partial class CB_CONTINUOUS_MODE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CB_CONTINUOUS_MODE"/> instance initialized with default values.
    /// </summary>
    public CB_CONTINUOUS_MODE() : base("CB_CONTINUOUS_MODE")
    {
        EnableIn = new BOOL();
        AckType = new BOOL();
        TakeoverMode = new BOOL();
        Enable = new BOOL();
        SafetyEnable = new BOOL();
        StandardEnable = new BOOL();
        ArmContinuous = new BOOL();
        Start = new BOOL();
        StopAtTop = new BOOL();
        PressInMotion = new BOOL();
        MotionMonitorFault = new BOOL();
        SafetyEnableAck = new BOOL();
        Mode = new DINT();
        SlideZone = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        CA = new BOOL();
        DiagnosticCode = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="CB_CONTINUOUS_MODE"/> instance initialized with the provided element.
    /// </summary>
    public CB_CONTINUOUS_MODE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AckType</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL AckType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TakeoverMode</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL TakeoverMode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Enable</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL Enable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SafetyEnable</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL SafetyEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandardEnable</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL StandardEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ArmContinuous</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL ArmContinuous
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Start</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopAtTop</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL StopAtTop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PressInMotion</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL PressInMotion
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MotionMonitorFault</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL MotionMonitorFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SafetyEnableAck</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL SafetyEnableAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public DINT Mode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SlideZone</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public DINT SlideZone
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CA</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public BOOL CA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="CB_CONTINUOUS_MODE"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
