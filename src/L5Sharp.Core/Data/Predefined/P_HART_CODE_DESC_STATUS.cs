using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_HART_CODE_DESC_STATUS</c> data type structure.
/// </summary>
[LogixData("P_HART_CODE_DESC_STATUS")]
public sealed partial class P_HART_CODE_DESC_STATUS : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_HART_CODE_DESC_STATUS"/> instance initialized with default values.
    /// </summary>
    public P_HART_CODE_DESC_STATUS() : base("P_HART_CODE_DESC_STATUS")
    {
        Code = new DINT();
        Desc = new STRING_32();
        bSts = new SINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_HART_CODE_DESC_STATUS"/> instance initialized with the provided element.
    /// </summary>
    public P_HART_CODE_DESC_STATUS(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Code</c> member of the <see cref="P_HART_CODE_DESC_STATUS"/> data type.
    /// </summary>
    public DINT Code
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Desc</c> member of the <see cref="P_HART_CODE_DESC_STATUS"/> data type.
    /// </summary>
    public STRING_32 Desc
    {
        get => GetMember<STRING_32>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>bSts</c> member of the <see cref="P_HART_CODE_DESC_STATUS"/> data type.
    /// </summary>
    public SINT bSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }
}