using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_MATH_ADVANCED</c> data type structure.
/// </summary>
[LogixData("FBD_MATH_ADVANCED")]
public sealed partial class FBD_MATH_ADVANCED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_MATH_ADVANCED"/> instance initialized with default values.
    /// </summary>
    public FBD_MATH_ADVANCED() : base("FBD_MATH_ADVANCED")
    {
        EnableIn = new BOOL();
        Source = new REAL();
        EnableOut = new BOOL();
        Dest = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_MATH_ADVANCED"/> instance initialized with the provided element.
    /// </summary>
    public FBD_MATH_ADVANCED(XElement element) : base(element)
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
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Source.UpdateData(data, offset + 5);
        EnableOut.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        Dest.UpdateData(data, offset + 9);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public REAL Source
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public REAL Dest
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}