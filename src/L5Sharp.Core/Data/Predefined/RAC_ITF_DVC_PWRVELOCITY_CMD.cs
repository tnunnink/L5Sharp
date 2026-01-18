using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_ITF_DVC_PWRVELOCITY_CMD</c> data type structure.
/// </summary>
[LogixData("RAC_ITF_DVC_PWRVELOCITY_CMD")]
public sealed partial class RAC_ITF_DVC_PWRVELOCITY_CMD : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> instance initialized with default values.
    /// </summary>
    public RAC_ITF_DVC_PWRVELOCITY_CMD() : base("RAC_ITF_DVC_PWRVELOCITY_CMD")
    {
        bCmd = new INT();
        Physical = new BOOL();
        Virtual = new BOOL();
        ResetWarn = new BOOL();
        ResetFault = new BOOL();
        Activate = new BOOL();
        Deactivate = new BOOL();
        CmdDir = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> instance initialized with the provided element.
    /// </summary>
    public RAC_ITF_DVC_PWRVELOCITY_CMD(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>bCmd</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public INT bCmd
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Physical</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public BOOL Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Virtual</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public BOOL Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetWarn</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public BOOL ResetWarn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetFault</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public BOOL ResetFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Activate</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public BOOL Activate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Deactivate</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public BOOL Deactivate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CmdDir</c> member of the <see cref="RAC_ITF_DVC_PWRVELOCITY_CMD"/> data type.
    /// </summary>
    public BOOL CmdDir
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}