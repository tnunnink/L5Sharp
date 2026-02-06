using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFETY_MAT</c> data type structure.
/// </summary>
[LogixData("SAFETY_MAT")]
public sealed partial class SAFETY_MAT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFETY_MAT"/> instance initialized with default values.
    /// </summary>
    public SAFETY_MAT() : base("SAFETY_MAT")
    {
        EnableIn = new BOOL();
        RestartType = new BOOL();
        ChannelA = new BOOL();
        ChannelB = new BOOL();
        InputStatus = new BOOL();
        Reset = new BOOL();
        ShortCircuitDetectDelayTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        SRCA = new BOOL();
        SRCB = new BOOL();
        FP = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SAFETY_MAT"/> instance initialized with the provided element.
    /// </summary>
    public SAFETY_MAT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 44;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        RestartType.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        ChannelA.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        ChannelB.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Reset.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        ShortCircuitDetectDelayTime.UpdateData(data, offset + 5);
        EnableOut.UpdateData((data[offset + 13] & (1 << 6)) != 0);
        O1.UpdateData((data[offset + 13] & (1 << 7)) != 0);
        SRCA.UpdateData((data[offset + 14] & (1 << 0)) != 0);
        SRCB.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        FP.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        FaultCode.UpdateData(data, offset + 14);
        DiagnosticCode.UpdateData(data, offset + 18);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShortCircuitDetectDelayTime</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public DINT ShortCircuitDetectDelayTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SRCA</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL SRCA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SRCB</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL SRCB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFETY_MAT"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}