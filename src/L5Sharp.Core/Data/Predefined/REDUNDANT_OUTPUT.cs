using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>REDUNDANT_OUTPUT</c> data type structure.
/// </summary>
[LogixData("REDUNDANT_OUTPUT")]
public sealed partial class REDUNDANT_OUTPUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="REDUNDANT_OUTPUT"/> instance initialized with default values.
    /// </summary>
    public REDUNDANT_OUTPUT() : base("REDUNDANT_OUTPUT")
    {
        EnableIn = new BOOL();
        FeedbackType = new BOOL();
        Enable = new BOOL();
        Feedback1 = new BOOL();
        Feedback2 = new BOOL();
        FaultReset = new BOOL();
        EnableOut = new BOOL();
        O1 = new BOOL();
        O2 = new BOOL();
        O1FF = new BOOL();
        O2FF = new BOOL();
        FP = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="REDUNDANT_OUTPUT"/> instance initialized with the provided element.
    /// </summary>
    public REDUNDANT_OUTPUT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FeedbackType</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL FeedbackType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Enable</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL Enable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Feedback1</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL Feedback1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Feedback2</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL Feedback2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultReset</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL FaultReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O2</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL O2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1FF</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL O1FF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O2FF</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL O2FF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="REDUNDANT_OUTPUT"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}