using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_LIMIT</c> data type structure.
/// </summary>
[LogixData("FBD_LIMIT")]
public sealed partial class FBD_LIMIT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_LIMIT"/> instance initialized with default values.
    /// </summary>
    public FBD_LIMIT() : base("FBD_LIMIT")
    {
        EnableIn = new BOOL();
        LowLimit = new REAL();
        Test = new REAL();
        HighLimit = new REAL();
        EnableOut = new BOOL();
        Dest = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_LIMIT"/> instance initialized with the provided element.
    /// </summary>
    public FBD_LIMIT(XElement element) : base(element)
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
        LowLimit.UpdateData(data, offset + 5);
        Test.UpdateData(data, offset + 9);
        HighLimit.UpdateData(data, offset + 13);
        EnableOut.UpdateData((data[offset + 17] & (1 << 1)) != 0);
        Dest.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowLimit</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public REAL LowLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Test</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public REAL Test
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLimit</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public REAL HighLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public BOOL Dest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}