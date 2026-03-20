using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_ONESHOT</c> data type structure.
/// </summary>
[LogixData("FBD_ONESHOT")]
public sealed partial class FBD_ONESHOT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_ONESHOT"/> instance initialized with default values.
    /// </summary>
    public FBD_ONESHOT() : base("FBD_ONESHOT")
    {
        EnableIn = new BOOL();
        InputBit = new BOOL();
        EnableOut = new BOOL();
        OutputBit = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_ONESHOT"/> instance initialized with the provided element.
    /// </summary>
    public FBD_ONESHOT(XElement element) : base(element)
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
        InputBit.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        EnableOut.UpdateData((data[offset + 9] & (1 << 2)) != 0);
        OutputBit.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_ONESHOT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputBit</c> member of the <see cref="FBD_ONESHOT"/> data type.
    /// </summary>
    public BOOL InputBit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_ONESHOT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutputBit</c> member of the <see cref="FBD_ONESHOT"/> data type.
    /// </summary>
    public BOOL OutputBit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}