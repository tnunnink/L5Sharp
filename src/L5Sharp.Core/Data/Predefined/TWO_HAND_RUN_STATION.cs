using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>TWO_HAND_RUN_STATION</c> data type structure.
/// </summary>
[LogixData("TWO_HAND_RUN_STATION")]
public sealed partial class TWO_HAND_RUN_STATION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="TWO_HAND_RUN_STATION"/> instance initialized with default values.
    /// </summary>
    public TWO_HAND_RUN_STATION() : base("TWO_HAND_RUN_STATION")
    {
        EnableIn = new BOOL();
        ActivePinType = new BOOL();
        ActivePin = new BOOL();
        RightButtonNormallyOpen = new BOOL();
        RightButtonNormallyClosed = new BOOL();
        LeftButtonNormallyOpen = new BOOL();
        LeftButtonNormallyClosed = new BOOL();
        FaultReset = new BOOL();
        EnableOut = new BOOL();
        BP = new BOOL();
        SA = new BOOL();
        BT = new BOOL();
        CB = new BOOL();
        SAF = new BOOL();
        RBF = new BOOL();
        LBF = new BOOL();
        FP = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="TWO_HAND_RUN_STATION"/> instance initialized with the provided element.
    /// </summary>
    public TWO_HAND_RUN_STATION(XElement element) : base(element)
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
        ActivePinType.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        ActivePin.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        RightButtonNormallyOpen.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        RightButtonNormallyClosed.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        LeftButtonNormallyOpen.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        LeftButtonNormallyClosed.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        FaultReset.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        EnableOut.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        BP.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        SA.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        BT.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        CB.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        SAF.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        RBF.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        LBF.UpdateData((data[offset + 10] & (1 << 7)) != 0);
        FP.UpdateData((data[offset + 11] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ActivePinType</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL ActivePinType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ActivePin</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL ActivePin
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RightButtonNormallyOpen</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL RightButtonNormallyOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RightButtonNormallyClosed</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL RightButtonNormallyClosed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LeftButtonNormallyOpen</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL LeftButtonNormallyOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LeftButtonNormallyClosed</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL LeftButtonNormallyClosed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultReset</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL FaultReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BP</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL BP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SA</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL SA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BT</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL BT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CB</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL CB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SAF</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL SAF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RBF</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL RBF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LBF</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL LBF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="TWO_HAND_RUN_STATION"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}