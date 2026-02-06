using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_RESTART_INHIBIT</c> data type structure.
/// </summary>
[LogixData("P_RESTART_INHIBIT")]
public sealed partial class P_RESTART_INHIBIT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_RESTART_INHIBIT"/> instance initialized with default values.
    /// </summary>
    public P_RESTART_INHIBIT() : base("P_RESTART_INHIBIT")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_Stopped = new BOOL();
        Inp_Starting = new BOOL();
        Inp_Running = new BOOL();
        Cfg_ThreeColdStarts = new REAL();
        Cfg_FirstFailCold = new REAL();
        Cfg_SubseqFailCold = new REAL();
        Cfg_FirstFailHot = new REAL();
        Cfg_SubseqFailHot = new REAL();
        Cfg_HotRestartOK = new REAL();
        Cfg_RestartHot = new REAL();
        Cfg_HotToCold = new REAL();
        Val_MinToReady = new DINT();
        Val_SecToReady = new DINT();
        Sts_bFdbk = new SINT();
        Sts_State = new SINT();
        Sts_Ready = new BOOL();
        Sts_Err = new BOOL();
        Inp_InitializeReq = new BOOL();
        Sts_Initialized = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_RESTART_INHIBIT"/> instance initialized with the provided element.
    /// </summary>
    public P_RESTART_INHIBIT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 172;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_Stopped.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_Starting.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Inp_Running.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Cfg_ThreeColdStarts.UpdateData(data, offset + 5);
        Cfg_FirstFailCold.UpdateData(data, offset + 9);
        Cfg_SubseqFailCold.UpdateData(data, offset + 13);
        Cfg_FirstFailHot.UpdateData(data, offset + 17);
        Cfg_SubseqFailHot.UpdateData(data, offset + 21);
        Cfg_HotRestartOK.UpdateData(data, offset + 25);
        Cfg_RestartHot.UpdateData(data, offset + 29);
        Cfg_HotToCold.UpdateData(data, offset + 33);
        Val_MinToReady.UpdateData(data, offset + 37);
        Val_SecToReady.UpdateData(data, offset + 41);
        Sts_bFdbk.UpdateData(data, offset + 45);
        Sts_State.UpdateData(data, offset + 46);
        Sts_Ready.UpdateData((data[offset + 47] & (1 << 5)) != 0);
        Sts_Err.UpdateData((data[offset + 47] & (1 << 6)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 47] & (1 << 7)) != 0);
        Sts_Initialized.UpdateData((data[offset + 48] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Stopped</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL Inp_Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Starting</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL Inp_Starting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Running</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL Inp_Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ThreeColdStarts</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_ThreeColdStarts
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FirstFailCold</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_FirstFailCold
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SubseqFailCold</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_SubseqFailCold
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FirstFailHot</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_FirstFailHot
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SubseqFailHot</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_SubseqFailHot
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HotRestartOK</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_HotRestartOK
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RestartHot</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_RestartHot
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HotToCold</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public REAL Cfg_HotToCold
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinToReady</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public DINT Val_MinToReady
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SecToReady</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public DINT Val_SecToReady
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bFdbk</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public SINT Sts_bFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_State</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public SINT Sts_State
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ready</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL Sts_Ready
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_RESTART_INHIBIT"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}