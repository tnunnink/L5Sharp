using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>UP_DOWN_ACCUM</c> data type structure.
/// </summary>
[LogixData("UP_DOWN_ACCUM")]
public sealed partial class UP_DOWN_ACCUM : StructureData
{
    /// <summary>
    /// Creates a new <see cref="UP_DOWN_ACCUM"/> instance initialized with default values.
    /// </summary>
    public UP_DOWN_ACCUM() : base("UP_DOWN_ACCUM")
    {
        EnableIn = new BOOL();
        Initialize = new BOOL();
        InitialValue = new REAL();
        InPlus = new REAL();
        InMinus = new REAL();
        Hold = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="UP_DOWN_ACCUM"/> instance initialized with the provided element.
    /// </summary>
    public UP_DOWN_ACCUM(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 32;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Initialize.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        InitialValue.UpdateData(data, offset + 5);
        InPlus.UpdateData(data, offset + 9);
        InMinus.UpdateData(data, offset + 13);
        Hold.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        EnableOut.UpdateData((data[offset + 21] & (1 << 3)) != 0);
        Out.UpdateData(data, offset + 21);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public REAL InitialValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InPlus</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public REAL InPlus
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InMinus</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public REAL InMinus
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Hold</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public BOOL Hold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="UP_DOWN_ACCUM"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}