using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SERIAL_PORT_CONTROL</c> data type structure.
/// </summary>
[LogixData("SERIAL_PORT_CONTROL")]
public sealed partial class SERIAL_PORT_CONTROL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SERIAL_PORT_CONTROL"/> instance initialized with default values.
    /// </summary>
    public SERIAL_PORT_CONTROL() : base("SERIAL_PORT_CONTROL")
    {
        LEN = new DINT();
        POS = new DINT();
        ERROR = new DINT();
        EN = new BOOL();
        EU = new BOOL();
        DN = new BOOL();
        EM = new BOOL();
        ER = new BOOL();
        UL = new BOOL();
        RN = new BOOL();
        FD = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SERIAL_PORT_CONTROL"/> instance initialized with the provided element.
    /// </summary>
    public SERIAL_PORT_CONTROL(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 16;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        LEN.UpdateData(data, offset + 4);
        POS.UpdateData(data, offset + 8);
        ERROR.UpdateData(data, offset + 12);
        EN.UpdateData((data[offset + 17] & (1 << 0)) != 0);
        EU.UpdateData((data[offset + 17] & (1 << 1)) != 0);
        DN.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        EM.UpdateData((data[offset + 17] & (1 << 3)) != 0);
        ER.UpdateData((data[offset + 17] & (1 << 4)) != 0);
        UL.UpdateData((data[offset + 17] & (1 << 5)) != 0);
        RN.UpdateData((data[offset + 17] & (1 << 6)) != 0);
        FD.UpdateData((data[offset + 17] & (1 << 7)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>LEN</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public DINT LEN
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>POS</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public DINT POS
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ERROR</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public DINT ERROR
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EU</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL EU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EM</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL EM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ER</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL ER
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UL</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL UL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RN</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL RN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FD</c> member of the <see cref="SERIAL_PORT_CONTROL"/> data type.
    /// </summary>
    public BOOL FD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}