using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FIVE_POS_MODE_SELECTOR</c> data type structure.
/// </summary>
[LogixData("FIVE_POS_MODE_SELECTOR")]
public sealed partial class FIVE_POS_MODE_SELECTOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FIVE_POS_MODE_SELECTOR"/> instance initialized with default values.
    /// </summary>
    public FIVE_POS_MODE_SELECTOR() : base("FIVE_POS_MODE_SELECTOR")
    {
        EnableIn = new BOOL();
        Input1 = new BOOL();
        Input2 = new BOOL();
        Input3 = new BOOL();
        Input4 = new BOOL();
        Input5 = new BOOL();
        FaultReset = new BOOL();
        EnableOut = new BOOL();
        O1 = new BOOL();
        O2 = new BOOL();
        O3 = new BOOL();
        O4 = new BOOL();
        O5 = new BOOL();
        NM = new BOOL();
        MMS = new BOOL();
        FP = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="FIVE_POS_MODE_SELECTOR"/> instance initialized with the provided element.
    /// </summary>
    public FIVE_POS_MODE_SELECTOR(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input1</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input2</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input3</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input4</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input5</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultReset</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL FaultReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O2</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O3</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O4</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O5</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NM</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL NM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MMS</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL MMS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="FIVE_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
