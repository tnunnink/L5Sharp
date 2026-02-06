using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>EMERGENCY_STOP</c> data type structure.
/// </summary>
[LogixData("EMERGENCY_STOP")]
public sealed partial class EMERGENCY_STOP : StructureData
{
    /// <summary>
    /// Creates a new <see cref="EMERGENCY_STOP"/> instance initialized with default values.
    /// </summary>
    public EMERGENCY_STOP() : base("EMERGENCY_STOP")
    {
        EnableIn = new BOOL();
        ResetType = new BOOL();
        ChannelA = new BOOL();
        ChannelB = new BOOL();
        CircuitReset = new BOOL();
        FaultReset = new BOOL();
        EnableOut = new BOOL();
        O1 = new BOOL();
        CI = new BOOL();
        CRHO = new BOOL();
        II = new BOOL();
        FP = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="EMERGENCY_STOP"/> instance initialized with the provided element.
    /// </summary>
    public EMERGENCY_STOP(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 52;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        ResetType.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        ChannelA.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        ChannelB.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        CircuitReset.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        FaultReset.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        EnableOut.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        O1.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        CI.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        CRHO.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        II.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        FP.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetType</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL ResetType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CircuitReset</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL CircuitReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultReset</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL FaultReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CI</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL CI
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CRHO</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL CRHO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>II</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL II
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="EMERGENCY_STOP"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}