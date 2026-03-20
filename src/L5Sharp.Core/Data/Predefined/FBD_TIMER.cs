using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_TIMER</c> data type structure.
/// </summary>
[LogixData("FBD_TIMER")]
public sealed partial class FBD_TIMER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_TIMER"/> instance initialized with default values.
    /// </summary>
    public FBD_TIMER() : base("FBD_TIMER")
    {
        EnableIn = new BOOL();
        TimerEnable = new BOOL();
        PRE = new DINT();
        Reset = new BOOL();
        EnableOut = new BOOL();
        ACC = new DINT();
        EN = new BOOL();
        TT = new BOOL();
        DN = new BOOL();
        Status = new DINT();
        InstructFault = new BOOL();
        PresetInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_TIMER"/> instance initialized with the provided element.
    /// </summary>
    public FBD_TIMER(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 48;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 9] & (1 << 0)) != 0);
        TimerEnable.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        PRE.UpdateData(data, offset + 9);
        Reset.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        EnableOut.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        ACC.UpdateData(data, offset + 13);
        EN.UpdateData((data[offset + 17] & (1 << 4)) != 0);
        TT.UpdateData((data[offset + 17] & (1 << 5)) != 0);
        DN.UpdateData((data[offset + 17] & (1 << 6)) != 0);
        Status.UpdateData(data, offset + 17);
        InstructFault.UpdateData((data[offset + 21] & (1 << 7)) != 0);
        PresetInv.UpdateData((data[offset + 22] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimerEnable</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL TimerEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ACC</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public DINT ACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TT</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL TT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PresetInv</c> member of the <see cref="FBD_TIMER"/> data type.
    /// </summary>
    public BOOL PresetInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}