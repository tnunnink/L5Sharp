using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_PERMISSIVE</c> data type structure.
/// </summary>
[LogixData("P_PERMISSIVE")]
public sealed partial class P_PERMISSIVE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_PERMISSIVE"/> instance initialized with default values.
    /// </summary>
    public P_PERMISSIVE() : base("P_PERMISSIVE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_Perm00 = new BOOL();
        Inp_Perm01 = new BOOL();
        Inp_Perm02 = new BOOL();
        Inp_Perm03 = new BOOL();
        Inp_Perm04 = new BOOL();
        Inp_Perm05 = new BOOL();
        Inp_Perm06 = new BOOL();
        Inp_Perm07 = new BOOL();
        Inp_Perm08 = new BOOL();
        Inp_Perm09 = new BOOL();
        Inp_Perm10 = new BOOL();
        Inp_Perm11 = new BOOL();
        Inp_Perm12 = new BOOL();
        Inp_Perm13 = new BOOL();
        Inp_Perm14 = new BOOL();
        Inp_Perm15 = new BOOL();
        Inp_Perm16 = new BOOL();
        Inp_Perm17 = new BOOL();
        Inp_Perm18 = new BOOL();
        Inp_Perm19 = new BOOL();
        Inp_Perm20 = new BOOL();
        Inp_Perm21 = new BOOL();
        Inp_Perm22 = new BOOL();
        Inp_Perm23 = new BOOL();
        Inp_Perm24 = new BOOL();
        Inp_Perm25 = new BOOL();
        Inp_Perm26 = new BOOL();
        Inp_Perm27 = new BOOL();
        Inp_Perm28 = new BOOL();
        Inp_Perm29 = new BOOL();
        Inp_Perm30 = new BOOL();
        Inp_Perm31 = new BOOL();
        Inp_BypActive = new BOOL();
        Cfg_OKState = new DINT();
        Cfg_Bypassable = new DINT();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasNav = new DINT();
        Sts_Initialized = new BOOL();
        Sts_PermOK = new BOOL();
        Sts_NBPermOK = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_Perm = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_PERMISSIVE"/> instance initialized with the provided element.
    /// </summary>
    public P_PERMISSIVE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm00</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm00
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm01</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm02</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm02
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm03</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm03
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm04</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm04
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm05</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm05
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm06</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm06
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm07</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm07
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm08</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm08
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm09</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm09
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm10</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm10
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm11</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm11
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm12</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm12
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm13</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm13
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm14</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm14
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm15</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm15
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm16</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm16
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm17</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm17
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm18</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm18
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm19</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm19
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm20</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm20
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm21</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm21
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm22</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm22
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm23</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm23
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm24</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm24
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm25</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm25
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm26</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm26
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm27</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm27
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm28</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm28
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm29</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm29
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm30</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm30
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Perm31</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_Perm31
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_BypActive</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Inp_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OKState</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public DINT Cfg_OKState
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Bypassable</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public DINT Cfg_Bypassable
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public DINT Cfg_HasNav
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PermOK</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Sts_PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NBPermOK</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Sts_NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Perm</c> member of the <see cref="P_PERMISSIVE"/> data type.
    /// </summary>
    public DINT Sts_Perm
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}