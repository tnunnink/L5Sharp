using System;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An element of a <see cref="LogixData"/> that defines the structure or hierarchy of the data. 
/// </summary>
/// <remarks>
/// <para>
/// Members are used to define the structure of other <see cref="LogixData"/> objects. Since each member holds a
/// strongly typed reference to it's <see cref="Value"/>, the structure forms a hierarchical tree of nested members
/// and types, which is the basis for how Tag data is serialized to an L5X file.
/// </para>
/// <para>
/// A <see cref="Member"/> is a special element class that will only be
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public sealed class Member : LogixElement
{
    /// <summary>
    /// Custom name for the "virtual" member. This is for special types that may not have decorated data but need to
    /// represent a member object for which a tag can reference and get/set data for.
    /// </summary>
    private readonly string? _name;

    /// <summary>
    /// The custom getter function for a "virtual" member. If not null this will always be used in place of the normal
    /// getter methods.
    /// </summary>
    private readonly Func<LogixData>? _getter;

    /// <summary>
    /// The custom setter function for a "virtual" member. If not null this will always be used in place of the normal
    /// setter methods.
    /// </summary>
    private readonly Action<LogixData>? _setter;

    /// <summary>
    /// Creates a new <see cref="Member"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="data">The <see cref="LogixData"/> contianing the member's data.</param>
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="data"/> is null or empty.</exception>
    public Member(string name, LogixData data) : base(CreateMember(name, data))
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// Internal will make <see cref="Member"/> not something that is deserializable since we only look for public
    /// constructors. This library should handle all complex data structure deserialization internally so this is fine.
    /// This is also a class that is the basis for how Tag traverses it's hierarchy. Also this would not work in our
    /// LogixSerializer implementation since there is not mappings of the elements to it that are not already used for
    /// other types. Meaning, even data value, structure, and array members should be deserialized to the
    /// <see cref="LogixData"/> class and not the <see cref="Member"/>. So this is kind of a special internal class
    /// that helps build the logix data structure.
    /// </remarks>
    internal Member(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new "virtual" <see cref="Member"/> object with the provided name, getter, and setter functions. There
    /// is no backing XElement in this scenario, but the provided delegates will be called in place of the get/set for
    /// <see cref="Value"/> to allow special logix type objects to provide a mapping of a member object to it's custom
    /// backing L5X data element. This is used for the custom classes like <see cref="ALARM_ANALOG"/>,
    /// <see cref="ALARM_DIGITAL"/>, <see cref="MESSAGE"/> and others.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="getter">The function that get the logix type value for this member.</param>
    /// <param name="setter">The function that sets the underlying L5X data for this member.</param>
    internal Member(string name, Func<LogixData> getter, Action<LogixData> setter) : base(new XElement(L5XName.Data))
    {
        _name = name;
        _getter = getter;
        _setter = setter;
    }

    /// <summary>
    /// The name of the <see cref="Member"/>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the member name or array element index.</value>
    /// <remarks>
    /// All member names are immutable. To change the name of a member for a given tag or data structure you
    /// must remove and add a new member element. This can only be done for <c>ComplexType</c> objects.
    /// </remarks>
    public string Name => _name ?? Element.MemberName();

    /// <summary>
    /// The value of the <see cref="Member"/>, containing the simple or complex data value for this element.
    /// </summary>
    /// <value>A <see cref="LogixData"/> containing the member data.</value>
    /// <remarks>
    /// <para>
    /// <see cref="Member"/> has special internal methods for setting the underlying value of the data elements.
    /// Note that setting a member value will not change the "type" of the member but rather simply update the underlying
    /// value. This is to prevent the type structure from being inadvertently overwritten. You can change the type
    /// structure to replace members using a <see cref="ComplexData"/> for custom types.
    /// </para>
    /// <para>
    /// Also note that you must set the value with the the correct corresponding logix type
    /// (e.g. AtomicType = AtomicType, StringType = StringType, ArrayType = ArrayType).
    /// Arrays and structures will join provided value structure on the member name and forward the call down the
    /// hierarchy in order to set complex object values.
    /// </para>
    /// </remarks>
    public LogixData Value
    {
        get => GetData();
        set => SetData(value);
    }

    #region Internals

    /// <inheritdoc />
    /// <remarks>
    /// For member elements we want to just call deserialize of the current element, which should represent a data
    /// element (value, array, structure, etc.). However we also provided custom getter to allow special classes to
    /// specify how to retrieve the underlying data, so we will call that if provided. This is a work around for special
    /// data formats (Alarm, Message, etc.) as well as root Tag tag component.
    /// </remarks>
    protected override LogixData GetData()
    {
        return _getter is not null ? _getter() : Element.Deserialize<LogixData>();
    }

    /// <inheritdoc />
    /// <remarks>
    /// Sets the underlying value of the member to the provided logix type. This is a special setter specific to member,
    /// as this is where we update the data for a given data structure. We handle the cases for all base logix types
    /// including null, atomic, string, array, and structure. If the provided type can not be cast to the current type
    /// then we will get a <see cref="InvalidCastException"/>. This will help let the user know that they are setting
    /// values incorrectly.
    /// </remarks>
    protected override void SetData(LogixData? data)
    {
        //Never allow null data.
        if (data is null or NullData)
            throw new ArgumentNullException(nameof(data), "Can not set member with null data.");
        
        //Always use the custom setter if provided.
        if (_setter is not null)
        {
            _setter.Invoke(data);
            return;
        }
        
        //Otherwise this is a normal decorated data element and we need to handle the set based on the current value.
        switch (Value)
        {
            case NullData: //todo perhaps make a decision on if this should or should not be here.
                throw new InvalidOperationException("The member data is null and can not be... ");
            case AtomicData:
                var atomicType = (AtomicData)data;
                SetAtomic(atomicType);
                break;
            case StringData:
                var stringType = (StringData)data;
                SetString(stringType);
                break;
            case ArrayData:
                var arrayType = (ArrayData)data;
                SetStructure(arrayType);
                break;
            case StructureData:
                var structureType = (StructureData)data;
                SetStructure(structureType);
                break;
        }
    }

    /// <summary>
    /// Sets the <c>Value</c> for whatever data element is the element for this member.
    /// This will first convert the incoming type to the current type to ensure no type conversion issues (or throw exception if so).
    /// Then will format with the current radix format.
    /// </summary>
    private void SetAtomic(AtomicData atomic)
    {
        var current = (AtomicData)Value;
        var value = (AtomicData)Convert.ChangeType(atomic, current.GetType());
        Element.SetAttributeValue(L5XName.Value, value.ToString(current.Radix));
    }

    /// <summary>
    /// Sets the string value of a data or data value member element using the given logix type. Also updates the length
    /// attribute.
    /// </summary>
    private void SetString(StringData stringData)
    {
        switch (Element.Name.LocalName)
        {
            case L5XName.Data:
            {
                Element.SetAttributeValue(L5XName.Length, stringData.LEN);
                Element.ReplaceNodes(new XCData($"'{stringData}'"));
                return;
            }
            case L5XName.Structure:
            case L5XName.StructureMember:
            {
                var len = Element.Elements().FirstOrDefault(e => e.MemberName() == "LEN");
                len?.SetAttributeValue(L5XName.Value, stringData.LEN);
                var data = Element.Elements().FirstOrDefault(e => e.MemberName() == "DATA");
                data?.ReplaceNodes(new XCData($"'{stringData}'"));
                return;
            }
        }
    }

    /// <summary>
    /// Joins each child member for this member and the provided type data structure on name and sets each subsequent value.
    /// This will effectively forward the update down the type/member hierarchy until it gets to atomics or string.
    /// </summary>
    private void SetStructure(LogixData data)
    {
        Value.Members
            .Join(data.Members, m => m.Name, m => m.Name, (t, s) => new { t, s })
            .ToList()
            .ForEach(x => { x.t.Value = x.s.Value; });
    }

    /// <summary>
    /// Creates a new member element to contain the underlying member data for this object using the provided name
    /// and <see cref="LogixData"/> data. Members are really only different from the base decorated data in that the
    /// element names append "Member" and the have the <c>Name</c> attribute.
    /// So we can build them more dynamically this way and not have to have a method to construct each type manually.
    /// </summary>
    // ReSharper disable once SuggestBaseTypeForParameter no this should always be a LogixType derivative
    private static XElement CreateMember(string name, LogixData data)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Can not create member with null or empty name", nameof(name));

        if (data is null or NullData)
            throw new ArgumentException("Can not create member with null data type.");

        //Get either the normal data element or if a string then the "Structure" element
        var element = data is StringData stringType ? stringType.SerializeStructure() : data.Serialize();

        //If it happens that the data already contains a member element, then return that with updated name.
        if (element.Name.LocalName.Contains(nameof(Member)))
        {
            element.SetAttributeValue(L5XName.Name, name);
            return element;
        }

        //Otherwise create a new member element with same data and name.
        var member = new XElement($"{element.Name}Member", new XAttribute(L5XName.Name, name));
        member.Add(element.Attributes());
        member.Add(element.Elements());
        return member;
    }

    #endregion
}

/// <summary>
/// Element extensions specifically for helping with logic around working with a <see cref="Member"/>.
/// </summary>
internal static class MemberExtension
{
    /// <summary>
    /// Creates a new <see cref="Member"/> with the current element.
    /// </summary>
    /// <remarks>
    /// This is only to make some syntax nicer in places where I'm using LINQ and what the chain
    /// the creation of the <see cref="Member"/> in place.
    /// </remarks>
    internal static Member ToMember(this XElement element) => new(element);
}