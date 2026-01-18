using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_BOOLEAN_XOR</c> data type structure.
/// </summary>
[LogixData("FBD_BOOLEAN_XOR")]
public sealed partial class FBD_BOOLEAN_XOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_BOOLEAN_XOR"/> instance initialized with default values.
    /// </summary>
    public FBD_BOOLEAN_XOR() : base("FBD_BOOLEAN_XOR")
    {
        EnableIn = new BOOL();
        In1 = new BOOL();
        In2 = new BOOL();
        EnableOut = new BOOL();
        Out = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_BOOLEAN_XOR"/> instance initialized with the provided element.
    /// </summary>
    public FBD_BOOLEAN_XOR(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_BOOLEAN_XOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In1</c> member of the <see cref="FBD_BOOLEAN_XOR"/> data type.
    /// </summary>
    public BOOL In1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In2</c> member of the <see cref="FBD_BOOLEAN_XOR"/> data type.
    /// </summary>
    public BOOL In2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_BOOLEAN_XOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="FBD_BOOLEAN_XOR"/> data type.
    /// </summary>
    public BOOL Out
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}