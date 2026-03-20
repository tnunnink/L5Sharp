using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CAMSHAFT_MONITOR</c> data type structure.
/// </summary>
[LogixData("CAMSHAFT_MONITOR")]
public sealed partial class CAMSHAFT_MONITOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CAMSHAFT_MONITOR"/> instance initialized with default values.
    /// </summary>
    public CAMSHAFT_MONITOR() : base("CAMSHAFT_MONITOR")
    {
        EnableIn = new BOOL();
        MotionRequest = new BOOL();
        ChannelA = new BOOL();
        ChannelB = new BOOL();
        InputStatus = new BOOL();
        Reset = new BOOL();
        MechanicalDelayTime = new DINT();
        MaxPulsePeriod = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        MeasuredStartTime = new DINT();
        MeasuredStopTime = new DINT();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CAMSHAFT_MONITOR"/> instance initialized with the provided element.
    /// </summary>
    public CAMSHAFT_MONITOR(XElement element) : base(element)
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
        MotionRequest.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        ChannelA.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        ChannelB.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Reset.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        MechanicalDelayTime.UpdateData(data, offset + 5);
        MaxPulsePeriod.UpdateData(data, offset + 9);
        EnableOut.UpdateData((data[offset + 17] & (1 << 6)) != 0);
        O1.UpdateData((data[offset + 17] & (1 << 7)) != 0);
        FP.UpdateData((data[offset + 18] & (1 << 0)) != 0);
        MeasuredStartTime.UpdateData(data, offset + 18);
        MeasuredStopTime.UpdateData(data, offset + 22);
        FaultCode.UpdateData(data, offset + 26);
        DiagnosticCode.UpdateData(data, offset + 30);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MotionRequest</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL MotionRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MechanicalDelayTime</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public DINT MechanicalDelayTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxPulsePeriod</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public DINT MaxPulsePeriod
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MeasuredStartTime</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public DINT MeasuredStartTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MeasuredStopTime</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public DINT MeasuredStopTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="CAMSHAFT_MONITOR"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}