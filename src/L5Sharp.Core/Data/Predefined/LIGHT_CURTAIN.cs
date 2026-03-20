using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>LIGHT_CURTAIN</c> data type structure.
/// </summary>
[LogixData("LIGHT_CURTAIN")]
public sealed partial class LIGHT_CURTAIN : StructureData
{
    /// <summary>
    /// Creates a new <see cref="LIGHT_CURTAIN"/> instance initialized with default values.
    /// </summary>
    public LIGHT_CURTAIN() : base("LIGHT_CURTAIN")
    {
        EnableIn = new BOOL();
        ResetType = new BOOL();
        ChannelA = new BOOL();
        ChannelB = new BOOL();
        MuteLightCurtain = new BOOL();
        CircuitReset = new BOOL();
        FaultReset = new BOOL();
        InputFilterTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        CI = new BOOL();
        CRHO = new BOOL();
        LCB = new BOOL();
        LCM = new BOOL();
        II = new BOOL();
        FP = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="LIGHT_CURTAIN"/> instance initialized with the provided element.
    /// </summary>
    public LIGHT_CURTAIN(XElement element) : base(element)
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
        MuteLightCurtain.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        CircuitReset.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        FaultReset.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        InputFilterTime.UpdateData(data, offset + 5);
        EnableOut.UpdateData((data[offset + 13] & (1 << 7)) != 0);
        O1.UpdateData((data[offset + 14] & (1 << 0)) != 0);
        CI.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        CRHO.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        LCB.UpdateData((data[offset + 14] & (1 << 3)) != 0);
        LCM.UpdateData((data[offset + 14] & (1 << 4)) != 0);
        II.UpdateData((data[offset + 14] & (1 << 5)) != 0);
        FP.UpdateData((data[offset + 14] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetType</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL ResetType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MuteLightCurtain</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL MuteLightCurtain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CircuitReset</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL CircuitReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultReset</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL FaultReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputFilterTime</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public DINT InputFilterTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CI</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL CI
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CRHO</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL CRHO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LCB</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL LCB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LCM</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL LCM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>II</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL II
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="LIGHT_CURTAIN"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}