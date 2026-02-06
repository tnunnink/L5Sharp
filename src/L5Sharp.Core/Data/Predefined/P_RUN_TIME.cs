using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_RUN_TIME</c> data type structure.
/// </summary>
[LogixData("P_RUN_TIME")]
public sealed partial class P_RUN_TIME : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_RUN_TIME"/> instance initialized with default values.
    /// </summary>
    public P_RUN_TIME() : base("P_RUN_TIME")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_Starting = new BOOL();
        Inp_Running = new BOOL();
        PCmd_ClearStarts = new BOOL();
        PCmd_ClearMaxHrs = new BOOL();
        PCmd_ClearTotHrs = new BOOL();
        Val_Starts = new DINT();
        Val_CurRunHrs = new REAL();
        Val_MaxRunHrs = new REAL();
        Val_TotRunHrs = new REAL();
        Sts_Initialized = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_RUN_TIME"/> instance initialized with the provided element.
    /// </summary>
    public P_RUN_TIME(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 68;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_Starting.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Inp_Running.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        PCmd_ClearStarts.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        PCmd_ClearMaxHrs.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        PCmd_ClearTotHrs.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Val_Starts.UpdateData(data, offset + 5);
        Val_CurRunHrs.UpdateData(data, offset + 9);
        Val_MaxRunHrs.UpdateData(data, offset + 13);
        Val_TotRunHrs.UpdateData(data, offset + 17);
        Sts_Initialized.UpdateData((data[offset + 22] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Starting</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL Inp_Starting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Running</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL Inp_Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearStarts</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL PCmd_ClearStarts
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearMaxHrs</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL PCmd_ClearMaxHrs
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearTotHrs</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL PCmd_ClearTotHrs
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Starts</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public DINT Val_Starts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CurRunHrs</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public REAL Val_CurRunHrs
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxRunHrs</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public REAL Val_MaxRunHrs
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TotRunHrs</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public REAL Val_TotRunHrs
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_RUN_TIME"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}