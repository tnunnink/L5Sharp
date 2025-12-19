using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFE_STOP_1</c> data type structure.
/// </summary>
[LogixData("SAFE_STOP_1")]
public sealed partial class SAFE_STOP_1 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFE_STOP_1"/> instance initialized with default values.
    /// </summary>
    public SAFE_STOP_1() : base("SAFE_STOP_1")
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
        StopMonitorDelayActive = new BOOL();
        StopMonitorDelay = new INT();
        StopDelay = new DINT();
        StandstillSpeed = new REAL();
        DecelRefSpeed = new REAL();
        DecelSpeedTolerance = new REAL();
        FaultType = new SINT();
        DiagnosticCode = new SINT();
        SpeedLimit = new REAL();
        DecelerationRamp = new REAL();
    }

    /// <summary>
    /// Creates a new <see cref="SAFE_STOP_1"/> instance initialized with the provided element.
    /// </summary>
    public SAFE_STOP_1(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Request</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL Request
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RR</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL RR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopMonitorDelayActive</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public BOOL StopMonitorDelayActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopMonitorDelay</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public INT StopMonitorDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopDelay</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public DINT StopDelay
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandstillSpeed</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public REAL StandstillSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelRefSpeed</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public REAL DecelRefSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelSpeedTolerance</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public REAL DecelSpeedTolerance
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultType</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public SINT FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SpeedLimit</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public REAL SpeedLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelerationRamp</c> member of the <see cref="SAFE_STOP_1"/> data type.
    /// </summary>
    public REAL DecelerationRamp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

}
