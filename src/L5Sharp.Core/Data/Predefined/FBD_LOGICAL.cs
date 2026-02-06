using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_LOGICAL</c> data type structure.
/// </summary>
[LogixData("FBD_LOGICAL")]
public sealed partial class FBD_LOGICAL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_LOGICAL"/> instance initialized with default values.
    /// </summary>
    public FBD_LOGICAL() : base("FBD_LOGICAL")
    {
        EnableIn = new BOOL();
        SourceA = new DINT();
        SourceB = new DINT();
        EnableOut = new BOOL();
        Dest = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_LOGICAL"/> instance initialized with the provided element.
    /// </summary>
    public FBD_LOGICAL(XElement element) : base(element)
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
        SourceA.UpdateData(data, offset + 5);
        SourceB.UpdateData(data, offset + 9);
        EnableOut.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        Dest.UpdateData(data, offset + 13);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_LOGICAL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceA</c> member of the <see cref="FBD_LOGICAL"/> data type.
    /// </summary>
    public DINT SourceA
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceB</c> member of the <see cref="FBD_LOGICAL"/> data type.
    /// </summary>
    public DINT SourceB
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_LOGICAL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_LOGICAL"/> data type.
    /// </summary>
    public DINT Dest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}