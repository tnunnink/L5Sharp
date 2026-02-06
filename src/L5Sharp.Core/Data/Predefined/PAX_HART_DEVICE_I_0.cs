using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PAX_HART_DEVICE_I_0</c> data type structure.
/// </summary>
[LogixData("PAX_HART_DEVICE:I:0")]
public sealed partial class PAX_HART_DEVICE_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PAX_HART_DEVICE_I_0"/> instance initialized with default values.
    /// </summary>
    public PAX_HART_DEVICE_I_0() : base("PAX_HART_DEVICE:I:0")
    {
        RunMode = new BOOL();
        ConnectionFaulted = new BOOL();
        DiagnosticActive = new BOOL();
        DiagnosticSequenceCount = new SINT();
        CurrentSaturated = new BOOL();
        CurrentFixed = new BOOL();
        MoreStatusAvailable = new BOOL();
        CurrentMismatch = new BOOL();
        ConfigurationChanged = new BOOL();
        Malfunction = new BOOL();
        LoopCurrent = new CHANNEL_AI_I_0();
        PV = new CHANNEL_AI_HART_I_0();
        SV = new CHANNEL_AI_HART_I_0();
        TV = new CHANNEL_AI_HART_I_0();
        QV = new CHANNEL_AI_HART_I_0();
        Static = new AB_5000_HART_Static_Struct_I_0();
        ChDataAtSignal4 = new REAL();
        ChDataAtSignal20 = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="PAX_HART_DEVICE_I_0"/> instance initialized with the provided element.
    /// </summary>
    public PAX_HART_DEVICE_I_0(XElement element) : base(element)
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
        RunMode.UpdateData((data[offset + 2] & (1 << 0)) != 0);
        ConnectionFaulted.UpdateData((data[offset + 2] & (1 << 1)) != 0);
        DiagnosticActive.UpdateData((data[offset + 2] & (1 << 2)) != 0);
        DiagnosticSequenceCount.UpdateData(data, offset + 2);
        CurrentSaturated.UpdateData((data[offset + 4] & (1 << 3)) != 0);
        CurrentFixed.UpdateData((data[offset + 4] & (1 << 4)) != 0);
        MoreStatusAvailable.UpdateData((data[offset + 4] & (1 << 5)) != 0);
        CurrentMismatch.UpdateData((data[offset + 4] & (1 << 6)) != 0);
        ConfigurationChanged.UpdateData((data[offset + 4] & (1 << 7)) != 0);
        Malfunction.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        LoopCurrent.UpdateData(data, offset + 6);
        PV.UpdateData(data, offset + 6);
        SV.UpdateData(data, offset + 6);
        TV.UpdateData(data, offset + 6);
        QV.UpdateData(data, offset + 6);
        Static.UpdateData(data, offset + 6);
        ChDataAtSignal4.UpdateData(data, offset + 6);
        ChDataAtSignal20.UpdateData(data, offset + 10);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>RunMode</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL RunMode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ConnectionFaulted</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL ConnectionFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticActive</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL DiagnosticActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticSequenceCount</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public SINT DiagnosticSequenceCount
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrentSaturated</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL CurrentSaturated
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrentFixed</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL CurrentFixed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MoreStatusAvailable</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL MoreStatusAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrentMismatch</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL CurrentMismatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ConfigurationChanged</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL ConfigurationChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Malfunction</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public BOOL Malfunction
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LoopCurrent</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public CHANNEL_AI_I_0 LoopCurrent
    {
        get => GetMember<CHANNEL_AI_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public CHANNEL_AI_HART_I_0 PV
    {
        get => GetMember<CHANNEL_AI_HART_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SV</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public CHANNEL_AI_HART_I_0 SV
    {
        get => GetMember<CHANNEL_AI_HART_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TV</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public CHANNEL_AI_HART_I_0 TV
    {
        get => GetMember<CHANNEL_AI_HART_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>QV</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public CHANNEL_AI_HART_I_0 QV
    {
        get => GetMember<CHANNEL_AI_HART_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Static</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public AB_5000_HART_Static_Struct_I_0 Static
    {
        get => GetMember<AB_5000_HART_Static_Struct_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChDataAtSignal4</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public REAL ChDataAtSignal4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChDataAtSignal20</c> member of the <see cref="PAX_HART_DEVICE_I_0"/> data type.
    /// </summary>
    public REAL ChDataAtSignal20
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}