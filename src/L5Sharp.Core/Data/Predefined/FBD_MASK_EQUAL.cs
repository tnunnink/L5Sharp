using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_MASK_EQUAL</c> data type structure.
/// </summary>
[LogixData("FBD_MASK_EQUAL")]
public sealed partial class FBD_MASK_EQUAL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_MASK_EQUAL"/> instance initialized with default values.
    /// </summary>
    public FBD_MASK_EQUAL() : base("FBD_MASK_EQUAL")
    {
        EnableIn = new BOOL();
        Source = new DINT();
        Mask = new DINT();
        Compare = new DINT();
        EnableOut = new BOOL();
        Dest = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_MASK_EQUAL"/> instance initialized with the provided element.
    /// </summary>
    public FBD_MASK_EQUAL(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 16;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Source.UpdateData(data, offset + 5);
        Mask.UpdateData(data, offset + 9);
        Compare.UpdateData(data, offset + 13);
        EnableOut.UpdateData((data[offset + 17] & (1 << 1)) != 0);
        Dest.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public DINT Source
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mask</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public DINT Mask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Compare</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public DINT Compare
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public BOOL Dest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}