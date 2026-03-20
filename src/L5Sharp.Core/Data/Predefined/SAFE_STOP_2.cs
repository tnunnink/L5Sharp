using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFE_STOP_2</c> data type structure.
/// </summary>
[LogixData("SAFE_STOP_2")]
public sealed partial class SAFE_STOP_2 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFE_STOP_2"/> instance initialized with default values.
    /// </summary>
    public SAFE_STOP_2() : base("SAFE_STOP_2")
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
        CheckDelayActive = new BOOL();
        StopMonitorDelay = new INT();
        StopDelay = new DINT();
        SS2StandstillSpeed = new REAL();
        DecelRefSpeed = new REAL();
        DecelSpeedTolerance = new REAL();
        Mode = new SINT();
        CheckDelay = new INT();
        SOSStandstillSpeed = new REAL();
        StandstillDeadband = new REAL();
        SS2FaultType = new SINT();
        SOSFaultType = new SINT();
        DiagnosticCode = new SINT();
        SpeedLimit = new REAL();
        DecelerationRamp = new REAL();
        StandstillSetpoint = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SAFE_STOP_2"/> instance initialized with the provided element.
    /// </summary>
    public SAFE_STOP_2(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 128;
    
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
        StopMonitorDelayActive.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        CheckDelayActive.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        StopMonitorDelay.UpdateData(data, offset + 6);
        StopDelay.UpdateData(data, offset + 8);
        SS2StandstillSpeed.UpdateData(data, offset + 12);
        DecelRefSpeed.UpdateData(data, offset + 16);
        DecelSpeedTolerance.UpdateData(data, offset + 20);
        Mode.UpdateData(data, offset + 24);
        CheckDelay.UpdateData(data, offset + 25);
        SOSStandstillSpeed.UpdateData(data, offset + 27);
        StandstillDeadband.UpdateData(data, offset + 31);
        SS2FaultType.UpdateData(data, offset + 35);
        SOSFaultType.UpdateData(data, offset + 36);
        DiagnosticCode.UpdateData(data, offset + 37);
        SpeedLimit.UpdateData(data, offset + 38);
        DecelerationRamp.UpdateData(data, offset + 42);
        StandstillSetpoint.UpdateData(data, offset + 46);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Request</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL Request
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RR</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL RR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopMonitorDelayActive</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL StopMonitorDelayActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelayActive</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public BOOL CheckDelayActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopMonitorDelay</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public INT StopMonitorDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopDelay</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public DINT StopDelay
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SS2StandstillSpeed</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL SS2StandstillSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelRefSpeed</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL DecelRefSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelSpeedTolerance</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL DecelSpeedTolerance
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public SINT Mode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelay</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public INT CheckDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SOSStandstillSpeed</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL SOSStandstillSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandstillDeadband</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL StandstillDeadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SS2FaultType</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public SINT SS2FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SOSFaultType</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public SINT SOSFaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SpeedLimit</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL SpeedLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelerationRamp</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL DecelerationRamp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StandstillSetpoint</c> member of the <see cref="SAFE_STOP_2"/> data type.
    /// </summary>
    public REAL StandstillSetpoint
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}