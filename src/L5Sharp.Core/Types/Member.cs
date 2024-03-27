using System;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An element of a <see cref="LogixType"/> that defines the structure or hierarchy of the data. 
/// </summary>
/// <remarks>
/// <para>
/// Members are used to define the structure of other <see cref="LogixType"/> objects. Since each member holds a
/// strongly typed reference to it's <see cref="Value"/>, the structure forms a hierarchical tree of nested members
/// and types.
/// </para>
/// <para>
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public sealed class Member : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Member"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member. If <c>null</c> will default to an empty string.</param>
    /// <param name="type">The <see cref="LogixType"/> representing the member's data. If <c>null</c>
    /// will default to <see cref="NullType"/>.</param>
    public Member(string name, LogixType type) : base(CreateElement(name, type))
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// Internal will make <see cref="Member"/> not something that is deserializable since we only look for public
    /// constructors. This library should handle all complex data structure deserialization internally so this is fine.
    /// This is also a class that is the basis for how Tag traverses it's hierarchy. Also this would not work in our
    /// LogixSerializer implementation since there is not mappings of the elements to it that are not already used for
    /// other types. Meaning, even data value, structure, and array members should be deserialized to the
    /// <see cref="LogixType"/> class and not the <see cref="Member"/>. So this is kind of a special internal class
    /// that helps build the logix data structure. 
    /// </remarks>
    internal Member(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name of the <see cref="Member"/>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the member name or array element index.</value>
    public string Name => Element.MemberName();

    /// <summary>
    /// The logix type of the <see cref="Member"/>.
    /// </summary>
    /// <value>A <see cref="LogixType"/> representing the member data type.</value>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <remarks>
    /// The data type creates property the hierarchical structure of complex types.
    /// This type can be atomic, structure, string, or array.
    /// </remarks>
    public LogixType Value
    {
        get => GetData();
        set => SetValue(value);
    }


    #region Internals

    /// <summary>
    /// Sets the underlying value of the member to the provided logix type. This is a special setter specific to member,
    /// as this is where we update the data for a given data structure. We handle the cases for logix types null,
    /// atomic, string, array, and structure. If the provided type can not be cast to the current type then we will get
    /// a <see cref="InvalidCastException"/>. This will help let the user know that they are setting values incorrectly.
    /// </summary>
    private void SetValue(LogixType type)
    {
        if (type is null || type == LogixType.Null)
            throw new ArgumentNullException(nameof(type), "Can not set member with null data type.");

        switch (Value)
        {
            case NullType:
                SetNull(type);
                break;
            case AtomicType current:
                var atomicType = (AtomicType)type;
                SetAtomic(atomicType, current);
                break;
            case StringType:
                var stringType = (StringType)type;
                SetString(stringType);
                break;
            case ArrayType:
                var arrayType = (ArrayType)type;
                SetStructure(arrayType);
                break;
            case StructureType:
                var structureType = (StructureType)type;
                SetStructure(structureType);
                break;
        }
    }

    private void SetNull(LogixType type)
    {
        //If this is the data element and there is no parent element, we need to update it in place.
        if (Element.IsDataElement() && Element.Parent is null)
        {
            
            return;
        }
        
        SetData(type);
    }

    /// <summary>
    /// Sets the value for whatever data value member element is the element for this member.
    /// This will first convert the incoming type to the target type to ensure no type conversion issues.
    /// Then will format with the current radix format.
    /// </summary>
    private void SetAtomic(AtomicType atomic, AtomicType current)
    {
        var value = (AtomicType)Convert.ChangeType(atomic, current.GetType());
        Element.SetAttributeValue(L5XName.Value, value.ToString(current.Radix));
    }

    /// <summary>
    /// Sets the string value of a data or data value member element using the given logix type. Also updates the length
    /// attribute. Will throw exception if the provided type can not be cast to a <see cref="StringType"/>.
    /// </summary>
    private void SetString(StringType stringType)
    {
        switch (Element.Name.LocalName)
        {
            case L5XName.Data:
                SetData(stringType);
                return;
            case L5XName.Structure:
            case L5XName.StructureMember:
            {
                var len = Element.Elements().FirstOrDefault(e => e.MemberName() == "LEN");
                len?.SetAttributeValue(L5XName.Value, stringType.LEN);
                var data = Element.Elements().FirstOrDefault(e => e.MemberName() == "DATA");
                data?.SetValue(new XCData($"'{stringType}'"));
                return;
            }
        }
    }

    /// <summary>
    /// Joins each child member for this member and the provided type data structure on name and sets each subsequent value.
    /// This will effectively forward the update down the type/member hierarchy until it gets to atomics or string.
    /// </summary>
    private void SetStructure(LogixType type)
    {
        Value.Members
            .Join(type.Members, m => m.Name, m => m.Name, (t, s) => new { t, s })
            .ToList()
            .ForEach(x => { x.t.Value = x.s.Value; });
    }

    /// <summary>
    /// Creates a new member element to contain the underlying member data for this object using the provided name
    /// and <see cref="LogixType"/> data. Members are really only different from the logix type structures in that the
    /// element names end in Member and the have the Name attribute. So we can build them more dynamically this way.
    /// </summary>
    private static XElement CreateElement(string name, LogixType type)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Can not create member with null or empty name", nameof(name));

        if (type is null || type == LogixType.Null)
            throw new ArgumentException("Can not create member with null data type.");

        //Get either the normal data element or if a string then the "Structure" element
        var data = type is StringType stringType ? stringType.Serialize(L5XName.Structure) : type.Serialize();

        //If it happens that the type is already a Member element then return that with updated name.
        if (data.Name.LocalName.Contains(nameof(Member)))
        {
            data.SetAttributeValue(L5XName.Name, name);
            return data;
        }

        //Otherwise create a new Member element with same data and name.
        var member = new XElement($"{data.Name}Member", new XAttribute(L5XName.Name, name));
        member.Add(data.Attributes());
        member.Add(data.Elements());
        return member;
    }

    #endregion
}

internal static class MemberExtension
{
    /// <summary>
    /// Creates a new <see cref="Member"/> with the current element.
    /// </summary>
    /// <remarks>
    /// This is only to make some syntax nicer in places where I'm using LINQ and what the chain
    /// the creation of the <see cref="Member"/>.
    /// </remarks>
    internal static Member ToMember(this XElement element) => new(element);
}