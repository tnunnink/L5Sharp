using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SELECT</c> data type structure.
/// </summary>
[LogixData("SELECT")]
public sealed partial class SELECT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SELECT"/> instance initialized with default values.
    /// </summary>
    public SELECT() : base("SELECT")
    {
        EnableIn = new BOOL();
        In1 = new REAL();
        In2 = new REAL();
        SelectorIn = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SELECT"/> instance initialized with the provided element.
    /// </summary>
    public SELECT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 24;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In1.UpdateData(data, offset + 5);
        In2.UpdateData(data, offset + 9);
        SelectorIn.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        EnableOut.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        Out.UpdateData(data, offset + 17);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SELECT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In1</c> member of the <see cref="SELECT"/> data type.
    /// </summary>
    public REAL In1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In2</c> member of the <see cref="SELECT"/> data type.
    /// </summary>
    public REAL In2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SelectorIn</c> member of the <see cref="SELECT"/> data type.
    /// </summary>
    public BOOL SelectorIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SELECT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="SELECT"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}