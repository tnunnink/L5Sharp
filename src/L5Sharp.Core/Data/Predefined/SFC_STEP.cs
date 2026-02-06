using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SFC_STEP</c> data type structure.
/// </summary>
[LogixData("SFC_STEP")]
public sealed partial class SFC_STEP : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SFC_STEP"/> instance initialized with default values.
    /// </summary>
    public SFC_STEP() : base("SFC_STEP")
    {
        Status = new DINT();
        X = new BOOL();
        FS = new BOOL();
        SA = new BOOL();
        LS = new BOOL();
        DN = new BOOL();
        OV = new BOOL();
        AlarmEn = new BOOL();
        AlarmLow = new BOOL();
        AlarmHigh = new BOOL();
        Reset = new BOOL();
        PauseTimer = new BOOL();
        PRE = new DINT();
        T = new DINT();
        TMax = new DINT();
        Count = new DINT();
        LimitLow = new DINT();
        LimitHigh = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SFC_STEP"/> instance initialized with the provided element.
    /// </summary>
    public SFC_STEP(XElement element) : base(element)
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
        Status.UpdateData(data, offset + 0);
        X.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        FS.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        SA.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        LS.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        DN.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        OV.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        AlarmEn.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        AlarmLow.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        AlarmHigh.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        PauseTimer.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        PRE.UpdateData(data, offset + 6);
        T.UpdateData(data, offset + 10);
        TMax.UpdateData(data, offset + 14);
        Count.UpdateData(data, offset + 18);
        LimitLow.UpdateData(data, offset + 22);
        LimitHigh.UpdateData(data, offset + 26);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>X</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL X
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FS</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL FS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SA</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL SA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LS</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL LS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OV</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL OV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmEn</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL AlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmLow</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL AlarmLow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmHigh</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL AlarmHigh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseTimer</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL PauseTimer
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>T</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT T
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TMax</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT TMax
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Count</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public new DINT Count
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitLow</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT LimitLow
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitHigh</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT LimitHigh
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}