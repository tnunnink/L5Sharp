using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_COUNTER</c> data type structure.
/// </summary>
[LogixData("FBD_COUNTER")]
public sealed partial class FBD_COUNTER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_COUNTER"/> instance initialized with default values.
    /// </summary>
    public FBD_COUNTER() : base("FBD_COUNTER")
    {
        EnableIn = new BOOL();
        CUEnable = new BOOL();
        CDEnable = new BOOL();
        PRE = new DINT();
        Reset = new BOOL();
        EnableOut = new BOOL();
        ACC = new DINT();
        CU = new BOOL();
        CD = new BOOL();
        DN = new BOOL();
        OV = new BOOL();
        UN = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_COUNTER"/> instance initialized with the provided element.
    /// </summary>
    public FBD_COUNTER(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CUEnable</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL CUEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CDEnable</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL CDEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ACC</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public DINT ACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CU</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL CU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CD</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL CD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OV</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL OV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UN</c> member of the <see cref="FBD_COUNTER"/> data type.
    /// </summary>
    public BOOL UN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}