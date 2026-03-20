using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MAXIMUM_CAPTURE</c> data type structure.
/// </summary>
[LogixData("MAXIMUM_CAPTURE")]
public sealed partial class MAXIMUM_CAPTURE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MAXIMUM_CAPTURE"/> instance initialized with default values.
    /// </summary>
    public MAXIMUM_CAPTURE() : base("MAXIMUM_CAPTURE")
    {
        EnableIn = new BOOL();
        In = new REAL();
        Reset = new BOOL();
        ResetValue = new REAL();
        EnableOut = new BOOL();
        Out = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="MAXIMUM_CAPTURE"/> instance initialized with the provided element.
    /// </summary>
    public MAXIMUM_CAPTURE(XElement element) : base(element)
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
        In.UpdateData(data, offset + 5);
        Reset.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        ResetValue.UpdateData(data, offset + 9);
        EnableOut.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        Out.UpdateData(data, offset + 17);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MAXIMUM_CAPTURE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="MAXIMUM_CAPTURE"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="MAXIMUM_CAPTURE"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetValue</c> member of the <see cref="MAXIMUM_CAPTURE"/> data type.
    /// </summary>
    public REAL ResetValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MAXIMUM_CAPTURE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="MAXIMUM_CAPTURE"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}