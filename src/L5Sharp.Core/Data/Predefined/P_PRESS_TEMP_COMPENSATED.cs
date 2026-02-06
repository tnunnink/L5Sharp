using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_PRESS_TEMP_COMPENSATED</c> data type structure.
/// </summary>
[LogixData("P_PRESS_TEMP_COMPENSATED")]
public sealed partial class P_PRESS_TEMP_COMPENSATED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_PRESS_TEMP_COMPENSATED"/> instance initialized with default values.
    /// </summary>
    public P_PRESS_TEMP_COMPENSATED() : base("P_PRESS_TEMP_COMPENSATED")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_TAct = new REAL();
        Inp_PAct = new REAL();
        Inp_DPAct = new REAL();
        Inp_FAct = new REAL();
        Cfg_LoFlowCutoff = new REAL();
        Cfg_TStd = new REAL();
        Cfg_PStd = new REAL();
        Cfg_TOffset = new REAL();
        Cfg_POffset = new REAL();
        Cfg_DPRef = new REAL();
        Cfg_FRef = new REAL();
        Cfg_UseDP = new BOOL();
        Out_Flow = new REAL();
        Sts_Initialized = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrTStd = new BOOL();
        Sts_ErrPStd = new BOOL();
        Sts_ErrDPRef = new BOOL();
        Sts_ErrFRef = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_PRESS_TEMP_COMPENSATED"/> instance initialized with the provided element.
    /// </summary>
    public P_PRESS_TEMP_COMPENSATED(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 64;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_TAct.UpdateData(data, offset + 5);
        Inp_PAct.UpdateData(data, offset + 9);
        Inp_DPAct.UpdateData(data, offset + 13);
        Inp_FAct.UpdateData(data, offset + 17);
        Cfg_LoFlowCutoff.UpdateData(data, offset + 21);
        Cfg_TStd.UpdateData(data, offset + 25);
        Cfg_PStd.UpdateData(data, offset + 29);
        Cfg_TOffset.UpdateData(data, offset + 33);
        Cfg_POffset.UpdateData(data, offset + 37);
        Cfg_DPRef.UpdateData(data, offset + 41);
        Cfg_FRef.UpdateData(data, offset + 45);
        Cfg_UseDP.UpdateData((data[offset + 49] & (1 << 3)) != 0);
        Out_Flow.UpdateData(data, offset + 49);
        Sts_Initialized.UpdateData((data[offset + 53] & (1 << 4)) != 0);
        Sts_Err.UpdateData((data[offset + 53] & (1 << 5)) != 0);
        Sts_ErrTStd.UpdateData((data[offset + 53] & (1 << 6)) != 0);
        Sts_ErrPStd.UpdateData((data[offset + 53] & (1 << 7)) != 0);
        Sts_ErrDPRef.UpdateData((data[offset + 54] & (1 << 0)) != 0);
        Sts_ErrFRef.UpdateData((data[offset + 54] & (1 << 1)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_TAct</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Inp_TAct
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PAct</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Inp_PAct
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DPAct</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Inp_DPAct
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FAct</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Inp_FAct
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoFlowCutoff</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Cfg_LoFlowCutoff
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TStd</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Cfg_TStd
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PStd</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Cfg_PStd
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TOffset</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Cfg_TOffset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_POffset</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Cfg_POffset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DPRef</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Cfg_DPRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FRef</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Cfg_FRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseDP</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Cfg_UseDP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Flow</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public REAL Out_Flow
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrTStd</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Sts_ErrTStd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPStd</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Sts_ErrPStd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDPRef</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Sts_ErrDPRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFRef</c> member of the <see cref="P_PRESS_TEMP_COMPENSATED"/> data type.
    /// </summary>
    public BOOL Sts_ErrFRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}