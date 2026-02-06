using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FLIP_FLOP_D</c> data type structure.
/// </summary>
[LogixData("FLIP_FLOP_D")]
public sealed partial class FLIP_FLOP_D : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FLIP_FLOP_D"/> instance initialized with default values.
    /// </summary>
    public FLIP_FLOP_D() : base("FLIP_FLOP_D")
    {
        EnableIn = new BOOL();
        D = new BOOL();
        Clear = new BOOL();
        Clock = new BOOL();
        EnableOut = new BOOL();
        Q = new BOOL();
        QNot = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FLIP_FLOP_D"/> instance initialized with the provided element.
    /// </summary>
    public FLIP_FLOP_D(XElement element) : base(element)
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
        D.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Clear.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Clock.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        EnableOut.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Q.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        QNot.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FLIP_FLOP_D"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>D</c> member of the <see cref="FLIP_FLOP_D"/> data type.
    /// </summary>
    public BOOL D
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Clear</c> member of the <see cref="FLIP_FLOP_D"/> data type.
    /// </summary>
    public new BOOL Clear
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Clock</c> member of the <see cref="FLIP_FLOP_D"/> data type.
    /// </summary>
    public BOOL Clock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FLIP_FLOP_D"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Q</c> member of the <see cref="FLIP_FLOP_D"/> data type.
    /// </summary>
    public BOOL Q
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>QNot</c> member of the <see cref="FLIP_FLOP_D"/> data type.
    /// </summary>
    public BOOL QNot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}