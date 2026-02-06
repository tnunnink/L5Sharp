using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_BIT_FIELD_DISTRIBUTE</c> data type structure.
/// </summary>
[LogixData("FBD_BIT_FIELD_DISTRIBUTE")]
public sealed partial class FBD_BIT_FIELD_DISTRIBUTE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> instance initialized with default values.
    /// </summary>
    public FBD_BIT_FIELD_DISTRIBUTE() : base("FBD_BIT_FIELD_DISTRIBUTE")
    {
        EnableIn = new BOOL();
        Source = new DINT();
        SourceBit = new DINT();
        Length = new DINT();
        DestBit = new DINT();
        Target = new DINT();
        EnableOut = new BOOL();
        Dest = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> instance initialized with the provided element.
    /// </summary>
    public FBD_BIT_FIELD_DISTRIBUTE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 28;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Source.UpdateData(data, offset + 5);
        SourceBit.UpdateData(data, offset + 9);
        Length.UpdateData(data, offset + 13);
        DestBit.UpdateData(data, offset + 17);
        Target.UpdateData(data, offset + 21);
        EnableOut.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        Dest.UpdateData(data, offset + 25);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Source
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceBit</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT SourceBit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Length</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Length
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DestBit</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT DestBit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Target</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Target
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Dest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}