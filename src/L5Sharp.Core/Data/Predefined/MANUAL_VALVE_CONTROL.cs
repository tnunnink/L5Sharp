using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MANUAL_VALVE_CONTROL</c> data type structure.
/// </summary>
[LogixData("MANUAL_VALVE_CONTROL")]
public sealed partial class MANUAL_VALVE_CONTROL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MANUAL_VALVE_CONTROL"/> instance initialized with default values.
    /// </summary>
    public MANUAL_VALVE_CONTROL() : base("MANUAL_VALVE_CONTROL")
    {
        EnableIn = new BOOL();
        Enable = new BOOL();
        Keyswitch = new BOOL();
        Bottom = new BOOL();
        FlywheelStopped = new BOOL();
        SafetyEnable = new BOOL();
        Actuate = new BOOL();
        InputStatus = new BOOL();
        OutputStatus = new BOOL();
        Reset = new BOOL();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="MANUAL_VALVE_CONTROL"/> instance initialized with the provided element.
    /// </summary>
    public MANUAL_VALVE_CONTROL(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 32;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Enable.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Keyswitch.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Bottom.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        FlywheelStopped.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        SafetyEnable.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Actuate.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        OutputStatus.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        EnableOut.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        O1.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        FP.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        FaultCode.UpdateData(data, offset + 10);
        DiagnosticCode.UpdateData(data, offset + 14);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Enable</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Enable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Keyswitch</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Keyswitch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Bottom</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Bottom
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FlywheelStopped</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL FlywheelStopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SafetyEnable</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL SafetyEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Actuate</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Actuate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutputStatus</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL OutputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="MANUAL_VALVE_CONTROL"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}