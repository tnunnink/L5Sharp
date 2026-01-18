using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_BOOLEAN_OR</c> data type structure.
/// </summary>
[LogixData("FBD_BOOLEAN_OR")]
public sealed partial class FBD_BOOLEAN_OR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_BOOLEAN_OR"/> instance initialized with default values.
    /// </summary>
    public FBD_BOOLEAN_OR() : base("FBD_BOOLEAN_OR")
    {
        EnableIn = new BOOL();
        In1 = new BOOL();
        In2 = new BOOL();
        In3 = new BOOL();
        In4 = new BOOL();
        In5 = new BOOL();
        In6 = new BOOL();
        In7 = new BOOL();
        In8 = new BOOL();
        EnableOut = new BOOL();
        Out = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_BOOLEAN_OR"/> instance initialized with the provided element.
    /// </summary>
    public FBD_BOOLEAN_OR(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In1</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In2</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In3</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In4</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In5</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In6</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In7</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In8</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL In8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="FBD_BOOLEAN_OR"/> data type.
    /// </summary>
    public BOOL Out
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}