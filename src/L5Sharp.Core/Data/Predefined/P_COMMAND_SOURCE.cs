using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_COMMAND_SOURCE</c> data type structure.
/// </summary>
[LogixData("P_COMMAND_SOURCE")]
public sealed partial class P_COMMAND_SOURCE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_COMMAND_SOURCE"/> instance initialized with default values.
    /// </summary>
    public P_COMMAND_SOURCE() : base("P_COMMAND_SOURCE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_InitializeReq = new BOOL();
        Inp_Hand = new BOOL();
        Inp_Ovrd = new BOOL();
        Inp_ExtInh = new BOOL();
        Cfg_HasOper = new BOOL();
        Cfg_HasOperLocked = new BOOL();
        Cfg_HasProg = new BOOL();
        Cfg_HasProgLocked = new BOOL();
        Cfg_HasExt = new BOOL();
        Cfg_HasMaint = new BOOL();
        Cfg_HasMaintOoS = new BOOL();
        Cfg_OvrdOverLock = new BOOL();
        Cfg_ExtOverLock = new BOOL();
        Cfg_ProgPwrUp = new BOOL();
        Cfg_ProgNormal = new BOOL();
        Cfg_PCmdPriority = new BOOL();
        Cfg_PCmdProgAsLevel = new BOOL();
        Cfg_PCmdLockAsLevel = new BOOL();
        Cfg_ExtAcqAsLevel = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        MCmd_OoS = new BOOL();
        MCmd_IS = new BOOL();
        MCmd_Acq = new BOOL();
        MCmd_Rel = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        OCmd_Oper = new BOOL();
        OCmd_Prog = new BOOL();
        OCmd_Lock = new BOOL();
        OCmd_Unlock = new BOOL();
        OCmd_Normal = new BOOL();
        Sts_Initialized = new BOOL();
        Sts_eSrc = new INT();
        Sts_bSrc = new INT();
        Sts_Hand = new BOOL();
        Sts_OoS = new BOOL();
        Sts_Maint = new BOOL();
        Sts_Ovrd = new BOOL();
        Sts_Ext = new BOOL();
        Sts_Prog = new BOOL();
        Sts_ProgLocked = new BOOL();
        Sts_Oper = new BOOL();
        Sts_OperLocked = new BOOL();
        Sts_ProgOperSel = new BOOL();
        Sts_ProgOperLock = new BOOL();
        Sts_Normal = new BOOL();
        Sts_ProgReqInh = new BOOL();
        Sts_ExtReqInh = new BOOL();
        Sts_MAcqRcvd = new BOOL();
        MRdy_OoS = new BOOL();
        MRdy_IS = new BOOL();
        MRdy_Acq = new BOOL();
        MRdy_Rel = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        ORdy_Oper = new BOOL();
        ORdy_Prog = new BOOL();
        ORdy_Lock = new BOOL();
        ORdy_Unlock = new BOOL();
        ORdy_Normal = new BOOL();
        Out_OwnerSts = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_COMMAND_SOURCE"/> instance initialized with the provided element.
    /// </summary>
    public P_COMMAND_SOURCE(XElement element) : base(element)
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
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_OwnerCmd.UpdateData(data, offset + 5);
        Inp_InitializeReq.UpdateData((data[offset + 9] & (1 << 2)) != 0);
        Inp_Hand.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Inp_ExtInh.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 10] & (1 << 7)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 11] & (1 << 0)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 11] & (1 << 1)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 11] & (1 << 2)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 11] & (1 << 3)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 11] & (1 << 4)) != 0);
        PCmd_Oper.UpdateData((data[offset + 11] & (1 << 5)) != 0);
        PCmd_Prog.UpdateData((data[offset + 11] & (1 << 6)) != 0);
        PCmd_Lock.UpdateData((data[offset + 11] & (1 << 7)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 12] & (1 << 0)) != 0);
        PCmd_Normal.UpdateData((data[offset + 12] & (1 << 1)) != 0);
        MCmd_OoS.UpdateData((data[offset + 12] & (1 << 2)) != 0);
        MCmd_IS.UpdateData((data[offset + 12] & (1 << 3)) != 0);
        MCmd_Acq.UpdateData((data[offset + 12] & (1 << 4)) != 0);
        MCmd_Rel.UpdateData((data[offset + 12] & (1 << 5)) != 0);
        XCmd_Acq.UpdateData((data[offset + 12] & (1 << 6)) != 0);
        XCmd_Rel.UpdateData((data[offset + 12] & (1 << 7)) != 0);
        OCmd_Oper.UpdateData((data[offset + 17] & (1 << 0)) != 0);
        OCmd_Prog.UpdateData((data[offset + 17] & (1 << 1)) != 0);
        OCmd_Lock.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        OCmd_Unlock.UpdateData((data[offset + 17] & (1 << 3)) != 0);
        OCmd_Normal.UpdateData((data[offset + 17] & (1 << 4)) != 0);
        Sts_Initialized.UpdateData((data[offset + 17] & (1 << 5)) != 0);
        Sts_eSrc.UpdateData(data, offset + 17);
        Sts_bSrc.UpdateData(data, offset + 19);
        Sts_Hand.UpdateData((data[offset + 21] & (1 << 6)) != 0);
        Sts_OoS.UpdateData((data[offset + 21] & (1 << 7)) != 0);
        Sts_Maint.UpdateData((data[offset + 22] & (1 << 0)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 22] & (1 << 1)) != 0);
        Sts_Ext.UpdateData((data[offset + 22] & (1 << 2)) != 0);
        Sts_Prog.UpdateData((data[offset + 22] & (1 << 3)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 22] & (1 << 4)) != 0);
        Sts_Oper.UpdateData((data[offset + 22] & (1 << 5)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 22] & (1 << 6)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 22] & (1 << 7)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 23] & (1 << 0)) != 0);
        Sts_Normal.UpdateData((data[offset + 23] & (1 << 1)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 23] & (1 << 2)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 23] & (1 << 3)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 23] & (1 << 4)) != 0);
        MRdy_OoS.UpdateData((data[offset + 23] & (1 << 5)) != 0);
        MRdy_IS.UpdateData((data[offset + 23] & (1 << 6)) != 0);
        MRdy_Acq.UpdateData((data[offset + 23] & (1 << 7)) != 0);
        MRdy_Rel.UpdateData((data[offset + 24] & (1 << 0)) != 0);
        XRdy_Acq.UpdateData((data[offset + 24] & (1 << 1)) != 0);
        XRdy_Rel.UpdateData((data[offset + 24] & (1 << 2)) != 0);
        ORdy_Oper.UpdateData((data[offset + 24] & (1 << 3)) != 0);
        ORdy_Prog.UpdateData((data[offset + 24] & (1 << 4)) != 0);
        ORdy_Lock.UpdateData((data[offset + 24] & (1 << 5)) != 0);
        ORdy_Unlock.UpdateData((data[offset + 24] & (1 << 6)) != 0);
        ORdy_Normal.UpdateData((data[offset + 24] & (1 << 7)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 24);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MCmd_OoS</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MCmd_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MCmd_IS</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MCmd_IS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MCmd_Acq</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MCmd_Rel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OCmd_Oper</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL OCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OCmd_Prog</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL OCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OCmd_Lock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL OCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OCmd_Unlock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL OCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OCmd_Normal</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL OCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MRdy_OoS</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MRdy_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MRdy_IS</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MRdy_IS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MRdy_Acq</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MRdy_Rel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL MRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ORdy_Oper</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL ORdy_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ORdy_Prog</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL ORdy_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ORdy_Lock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL ORdy_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ORdy_Unlock</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL ORdy_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ORdy_Normal</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public BOOL ORdy_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_COMMAND_SOURCE"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}