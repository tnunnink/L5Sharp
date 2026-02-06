using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_LEAD_LAG_STANDBY_MOTOR</c> data type structure.
/// </summary>
[LogixData("P_LEAD_LAG_STANDBY_MOTOR")]
public sealed partial class P_LEAD_LAG_STANDBY_MOTOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> instance initialized with default values.
    /// </summary>
    public P_LEAD_LAG_STANDBY_MOTOR() : base("P_LEAD_LAG_STANDBY_MOTOR")
    {
        Inp_OtherSel = new DINT();
        Cfg_Prio = new DINT();
        OSet_Pref = new DINT();
        Val_Pref = new DINT();
        Val_Rank = new DINT();
        Inp_Demote = new BOOL();
        PCmd_Start = new BOOL();
        PCmd_Stop = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        Sts_Available = new BOOL();
        Sts_Stopped = new BOOL();
        Sts_Starting = new BOOL();
        Sts_Running = new BOOL();
        Sts_Stopping = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> instance initialized with the provided element.
    /// </summary>
    public P_LEAD_LAG_STANDBY_MOTOR(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 24;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Inp_OtherSel.UpdateData(data, offset + 0);
        Cfg_Prio.UpdateData(data, offset + 5);
        OSet_Pref.UpdateData(data, offset + 9);
        Val_Pref.UpdateData(data, offset + 15);
        Val_Rank.UpdateData(data, offset + 19);
        Inp_Demote.UpdateData((data[offset + 24] & (1 << 0)) != 0);
        PCmd_Start.UpdateData((data[offset + 24] & (1 << 1)) != 0);
        PCmd_Stop.UpdateData((data[offset + 24] & (1 << 2)) != 0);
        PCmd_Lock.UpdateData((data[offset + 24] & (1 << 3)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 24] & (1 << 4)) != 0);
        Sts_Available.UpdateData((data[offset + 24] & (1 << 5)) != 0);
        Sts_Stopped.UpdateData((data[offset + 24] & (1 << 6)) != 0);
        Sts_Starting.UpdateData((data[offset + 24] & (1 << 7)) != 0);
        Sts_Running.UpdateData((data[offset + 25] & (1 << 0)) != 0);
        Sts_Stopping.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Inp_OtherSel</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public DINT Inp_OtherSel
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Prio</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public DINT Cfg_Prio
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OSet_Pref</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public DINT OSet_Pref
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Pref</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public DINT Val_Pref
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Rank</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public DINT Val_Rank
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Demote</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL Inp_Demote
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Start</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL PCmd_Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Stop</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL PCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopped</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL Sts_Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Starting</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL Sts_Starting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Running</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL Sts_Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopping</c> member of the <see cref="P_LEAD_LAG_STANDBY_MOTOR"/> data type.
    /// </summary>
    public BOOL Sts_Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}