using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MOVING_STD_DEV</c> data type structure.
/// </summary>
[LogixData("MOVING_STD_DEV")]
public sealed partial class MOVING_STD_DEV : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MOVING_STD_DEV"/> instance initialized with default values.
    /// </summary>
    public MOVING_STD_DEV() : base("MOVING_STD_DEV")
    {
        EnableIn = new BOOL();
        In = new REAL();
        InFault = new BOOL();
        Initialize = new BOOL();
        SampleEnable = new BOOL();
        NumberOfSamples = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        Average = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        InFaulted = new BOOL();
        NumberOfSampInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="MOVING_STD_DEV"/> instance initialized with the provided element.
    /// </summary>
    public MOVING_STD_DEV(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 60;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In.UpdateData(data, offset + 5);
        InFault.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        Initialize.UpdateData((data[offset + 9] & (1 << 2)) != 0);
        SampleEnable.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        NumberOfSamples.UpdateData(data, offset + 9);
        EnableOut.UpdateData((data[offset + 17] & (1 << 4)) != 0);
        Out.UpdateData(data, offset + 17);
        Average.UpdateData(data, offset + 21);
        Status.UpdateData(data, offset + 25);
        InstructFault.UpdateData((data[offset + 29] & (1 << 5)) != 0);
        InFaulted.UpdateData((data[offset + 29] & (1 << 6)) != 0);
        NumberOfSampInv.UpdateData((data[offset + 29] & (1 << 7)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFault</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SampleEnable</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL SampleEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NumberOfSamples</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public DINT NumberOfSamples
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Average</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public REAL Average
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFaulted</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL InFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NumberOfSampInv</c> member of the <see cref="MOVING_STD_DEV"/> data type.
    /// </summary>
    public BOOL NumberOfSampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}