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