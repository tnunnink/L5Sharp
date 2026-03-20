using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PULSE_MULTIPLIER</c> data type structure.
/// </summary>
[LogixData("PULSE_MULTIPLIER")]
public sealed partial class PULSE_MULTIPLIER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PULSE_MULTIPLIER"/> instance initialized with default values.
    /// </summary>
    public PULSE_MULTIPLIER() : base("PULSE_MULTIPLIER")
    {
        EnableIn = new BOOL();
        In = new DINT();
        Initialize = new BOOL();
        InitialValue = new DINT();
        Mode = new BOOL();
        WordSize = new DINT();
        Multiplier = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        WordSizeInv = new BOOL();
        OutOverflow = new BOOL();
        LostPrecision = new BOOL();
        MultiplierInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="PULSE_MULTIPLIER"/> instance initialized with the provided element.
    /// </summary>
    public PULSE_MULTIPLIER(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 48;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In.UpdateData(data, offset + 5);
        Initialize.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        InitialValue.UpdateData(data, offset + 9);
        Mode.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        WordSize.UpdateData(data, offset + 13);
        Multiplier.UpdateData(data, offset + 17);
        EnableOut.UpdateData((data[offset + 25] & (1 << 3)) != 0);
        Out.UpdateData(data, offset + 25);
        Status.UpdateData(data, offset + 29);
        InstructFault.UpdateData((data[offset + 33] & (1 << 4)) != 0);
        WordSizeInv.UpdateData((data[offset + 33] & (1 << 5)) != 0);
        OutOverflow.UpdateData((data[offset + 33] & (1 << 6)) != 0);
        LostPrecision.UpdateData((data[offset + 33] & (1 << 7)) != 0);
        MultiplierInv.UpdateData((data[offset + 34] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT In
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT InitialValue
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL Mode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WordSize</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT WordSize
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Multiplier</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT Multiplier
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WordSizeInv</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL WordSizeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutOverflow</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL OutOverflow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LostPrecision</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL LostPrecision
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MultiplierInv</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL MultiplierInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}