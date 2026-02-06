using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>TIMER</c> data type structure.
/// </summary>
[LogixData("TIMER")]
public sealed partial class TIMER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="TIMER"/> instance initialized with default values.
    /// </summary>
    public TIMER() : base("TIMER")
    {
        PRE = new DINT();
        ACC = new DINT();
        EN = new BOOL();
        TT = new BOOL();
        DN = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="TIMER"/> instance initialized with the provided element.
    /// </summary>
    public TIMER(XElement element) : base(element)
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
        EN.UpdateData((data[offset + 13] & (1 << 0)) != 0);
        TT.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        DN.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ACC</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public DINT ACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TT</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public BOOL TT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="TIMER"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}