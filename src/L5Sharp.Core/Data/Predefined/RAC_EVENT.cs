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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 144;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Type.UpdateData(data, offset + 0);
        ID.UpdateData(data, offset + 4);
        Category.UpdateData(data, offset + 8);
        Action.UpdateData(data, offset + 12);
        Value.UpdateData(data, offset + 16);
        Message.UpdateData(data, offset + 20);
        EventTime_L.UpdateData(data, offset + 108);
        EventTime_D.UpdateData(data, offset + 116);
        
        return offset + GetSize();
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