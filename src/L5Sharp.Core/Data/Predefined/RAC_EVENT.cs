using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_EVENT</c> data type structure.
/// </summary>
[LogixData("RAC_EVENT")]
public sealed partial class RAC_EVENT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_EVENT"/> instance initialized with default values.
    /// </summary>
    public RAC_EVENT() : base("RAC_EVENT")
    {
        Type = new DINT();
        ID = new DINT();
        Category = new DINT();
        Action = new DINT();
        Value = new DINT();
        Message = new STRING();
        EventTime_L = new LINT();
        EventTime_D = new ArrayData<DINT>(7);
    }
    
    /// <summary>
    /// Creates a new <see cref="RAC_EVENT"/> instance initialized with the provided element.
    /// </summary>
    public RAC_EVENT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Type</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public DINT Type
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ID</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public DINT ID
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Category</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public DINT Category
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Action</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public DINT Action
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public DINT Value
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Message</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public STRING Message
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EventTime_L</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public LINT EventTime_L
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EventTime_D</c> member of the <see cref="RAC_EVENT"/> data type.
    /// </summary>
    public ArrayData<DINT> EventTime_D
    {
        get => GetArray<DINT>();
        set => SetArray(value);
    }
}