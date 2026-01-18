using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SFC_STOP</c> data type structure.
/// </summary>
[LogixData("SFC_STOP")]
public sealed partial class SFC_STOP : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SFC_STOP"/> instance initialized with default values.
    /// </summary>
    public SFC_STOP() : base("SFC_STOP")
    {
        Status = new DINT();
        X = new BOOL();
        Reset = new BOOL();
        Count = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SFC_STOP"/> instance initialized with the provided element.
    /// </summary>
    public SFC_STOP(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SFC_STOP"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>X</c> member of the <see cref="SFC_STOP"/> data type.
    /// </summary>
    public BOOL X
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SFC_STOP"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Count</c> member of the <see cref="SFC_STOP"/> data type.
    /// </summary>
    public new DINT Count
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}