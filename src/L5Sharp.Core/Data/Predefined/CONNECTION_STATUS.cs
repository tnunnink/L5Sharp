using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CONNECTION_STATUS</c> data type structure.
/// </summary>
[LogixData("CONNECTION_STATUS")]
public sealed partial class CONNECTION_STATUS : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CONNECTION_STATUS"/> instance initialized with default values.
    /// </summary>
    public CONNECTION_STATUS() : base("CONNECTION_STATUS")
    {
        RunMode = new BOOL();
        ConnectionFaulted = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CONNECTION_STATUS"/> instance initialized with the provided element.
    /// </summary>
    public CONNECTION_STATUS(XElement element) : base(element)
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
        RunMode.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        ConnectionFaulted.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>RunMode</c> member of the <see cref="CONNECTION_STATUS"/> data type.
    /// </summary>
    public BOOL RunMode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ConnectionFaulted</c> member of the <see cref="CONNECTION_STATUS"/> data type.
    /// </summary>
    public BOOL ConnectionFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}