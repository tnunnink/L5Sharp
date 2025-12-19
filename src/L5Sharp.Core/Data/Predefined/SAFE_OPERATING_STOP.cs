using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFE_OPERATING_STOP</c> data type structure.
/// </summary>
[LogixData("SAFE_OPERATING_STOP")]
public sealed partial class SAFE_OPERATING_STOP : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFE_OPERATING_STOP"/> instance initialized with default values.
    /// </summary>
    public SAFE_OPERATING_STOP() : base("SAFE_OPERATING_STOP")
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
        Mode = new SINT();
        CheckDelay = new INT();
        StandstillSpeed = new REAL();
        StandstillDeadband = new REAL();
        FaultType = new SINT();
        DiagnosticCode = new SINT();
        StandStillSetpoint = new REAL();
    }

    /// <summary>
    /// Creates a new <see cref="SAFE_OPERATING_STOP"/> instance initialized with the provided element.
    /// </summary>
    public SAFE_OPERATING_STOP(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Request</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL Request
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RR</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL RR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelayActive</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public BOOL CheckDelayActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public SINT Mode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelay</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public INT CheckDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandstillSpeed</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public REAL StandstillSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandstillDeadband</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public REAL StandstillDeadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultType</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public SINT FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandStillSetpoint</c> member of the <see cref="SAFE_OPERATING_STOP"/> data type.
    /// </summary>
    public REAL StandStillSetpoint
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

}
