using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 76;
    
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
        StopMonitorDelay.UpdateData(data, offset + 6);
        StopDelay.UpdateData(data, offset + 8);
        StandstillSpeed.UpdateData(data, offset + 12);
        DecelRefSpeed.UpdateData(data, offset + 16);
        DecelSpeedTolerance.UpdateData(data, offset + 20);
        FaultType.UpdateData(data, offset + 24);
        DiagnosticCode.UpdateData(data, offset + 25);
        SpeedLimit.UpdateData(data, offset + 26);
        DecelerationRamp.UpdateData(data, offset + 30);
        
        return offset + GetSize();
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