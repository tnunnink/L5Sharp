using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_ITF_DVC_PWRMOTION_CMD</c> data type structure.
/// </summary>
[LogixData("RAC_ITF_DVC_PWRMOTION_CMD")]
public sealed partial class RAC_ITF_DVC_PWRMOTION_CMD : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> instance initialized with default values.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_CMD() : base("RAC_ITF_DVC_PWRMOTION_CMD")
    {
        bCmd = new INT();
        Physical = new BOOL();
        Virtual = new BOOL();
        ResetWarn = new BOOL();
        ResetFault = new BOOL();
        Activate = new BOOL();
        Deactivate = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> instance initialized with the provided element.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_CMD(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 4;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        bCmd.UpdateData(data, offset + 0);
        Physical.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        Virtual.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        ResetWarn.UpdateData((data[offset + 3] & (1 << 2)) != 0);
        ResetFault.UpdateData((data[offset + 3] & (1 << 3)) != 0);
        Activate.UpdateData((data[offset + 3] & (1 << 4)) != 0);
        Deactivate.UpdateData((data[offset + 3] & (1 << 5)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>bCmd</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> data type.
    /// </summary>
    public INT bCmd
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Physical</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> data type.
    /// </summary>
    public BOOL Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Virtual</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> data type.
    /// </summary>
    public BOOL Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetWarn</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> data type.
    /// </summary>
    public BOOL ResetWarn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetFault</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> data type.
    /// </summary>
    public BOOL ResetFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Activate</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> data type.
    /// </summary>
    public BOOL Activate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Deactivate</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_CMD"/> data type.
    /// </summary>
    public BOOL Deactivate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}