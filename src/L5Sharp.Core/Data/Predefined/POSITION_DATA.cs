using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>POSITION_DATA</c> data type structure.
/// </summary>
[LogixData("POSITION_DATA")]
public sealed partial class POSITION_DATA : StructureData
{
    /// <summary>
    /// Creates a new <see cref="POSITION_DATA"/> instance initialized with default values.
    /// </summary>
    public POSITION_DATA() : base("POSITION_DATA")
    {
        ID = new DINT();
        X = new REAL();
        Y = new REAL();
        Z = new REAL();
        Rx = new REAL();
        Ry = new REAL();
        Rz = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="POSITION_DATA"/> instance initialized with the provided element.
    /// </summary>
    public POSITION_DATA(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>ID</c> member of the <see cref="POSITION_DATA"/> data type.
    /// </summary>
    public DINT ID
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>X</c> member of the <see cref="POSITION_DATA"/> data type.
    /// </summary>
    public REAL X
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Y</c> member of the <see cref="POSITION_DATA"/> data type.
    /// </summary>
    public REAL Y
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Z</c> member of the <see cref="POSITION_DATA"/> data type.
    /// </summary>
    public REAL Z
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Rx</c> member of the <see cref="POSITION_DATA"/> data type.
    /// </summary>
    public REAL Rx
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Ry</c> member of the <see cref="POSITION_DATA"/> data type.
    /// </summary>
    public REAL Ry
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Rz</c> member of the <see cref="POSITION_DATA"/> data type.
    /// </summary>
    public REAL Rz
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}