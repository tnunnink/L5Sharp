using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>COUNTER</c> data type structure.
/// </summary>
[LogixData("COUNTER")]
public sealed partial class COUNTER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="COUNTER"/> instance initialized with default values.
    /// </summary>
    public COUNTER() : base("COUNTER")
    {
        PRE = new DINT();
        ACC = new DINT();
        CU = new BOOL();
        CD = new BOOL();
        DN = new BOOL();
        OV = new BOOL();
        UN = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="COUNTER"/> instance initialized with the provided element.
    /// </summary>
    public COUNTER(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 12;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        PRE.UpdateData(data, offset + 4);
        ACC.UpdateData(data, offset + 8);
        CU.UpdateData((data[offset + 13] & (1 << 0)) != 0);
        CD.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        DN.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        OV.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        UN.UpdateData((data[offset + 13] & (1 << 4)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ACC</c> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public DINT ACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CU</c> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL CU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CD</c> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL CD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OV</c> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL OV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UN</c> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL UN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}