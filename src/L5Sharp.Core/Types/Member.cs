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
    /// Custom name for the "virtual" member. This is for special types that may not have decorated data but need to
    /// represent a member object for which a tag can reference and get/set data for.
    /// </summary>
    private readonly string? _name;

    /// <summary>
    /// 
    /// </summary>
    private readonly Func<LogixType>? _getter;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly Action<LogixType>? _setter;

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
    /// Creates a new "virtual" <see cref="Member"/> object with the provided name, getter, and setter functions. There
    /// is no backing XElement in this scenario, but the provided delegates will be called in place of the get/set for
    /// <see cref="Value"/> of allow special logix type objects to provide a mapping of a member object to it's custom
    /// backing L5X data element. This is used for the custom classes like <see cref="ALARM_ANALOG"/>,
    /// <see cref="ALARM_DIGITAL"/>, <see cref="MESSAGE"/> and others.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="getter">The function that get the logix type value for this member.</param>
    /// <param name="setter">The function that sets the underlying L5X data for this member.</param>
    internal Member(string name, Func<LogixType> getter, Action<LogixType> setter) : base(new XElement(L5XName.Data))
    {
        _name = name;
        _getter = getter;
        _setter = setter;
    }

    /// <summary>
    /// The name of the <see cref="Member"/>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the member name or array element index.</value>
    public string Name => _name ?? Element.MemberName();

    /// <summary>
    /// The value of the <see cref="Member"/>, containing the simple or complex data value for this element.
    /// </summary>
    /// <value>A <see cref="LogixType"/> containing the member data.</value>
    /// <remarks>
    /// 
    /// </remarks>
    public LogixType Value
    {
        get => GetValue();
        set => SetValue(value);
    }

    #region Internals

    /// <summary>
    /// Handles forwarding the call to the correct place to retrieve the actual value for this member. This would be easy
    /// if we could treat everything as decorated data, but there are some exceptions since Logix formats certain types
    /// differently. To solve this we are providing an internal constructor which allows type to provide custom getters
    /// for retrieving data. If not provided, we assume this is a tag/parameter element or an actual decorated member element.
    /// </summary>
    private LogixType GetValue()
    {
        //If a custom getter is provided then return that.
        if (_getter is not null) return _getter();

        //If not currently a data element (tag or parameter) then use the built in element GetData to get the child
        //data element and deserialize that.
        if (!Element.IsData()) return GetData();

        //Otherwise deserialize this decorated member as a LogixType.
        if (Element.IsDecoratedData()) return Element.Deserialize<LogixType>();

        throw new InvalidOperationException($"Member element {Element.Name} is not a valid data element.");
    }

    /// <summary>
    /// Sets the underlying value of the member to the provided logix type. This is a special setter specific to member,
    /// as this is where we update the data for a given data structure. We handle the cases for all base logix types
    /// including null, atomic, string, array, and structure. If the provided type can not be cast to the current type
    /// then we will get a <see cref="InvalidCastException"/>. This will help let the user know that they are setting
    /// values incorrectly.
    /// </summary>
    private void SetValue(LogixType type)
    {
        if (type is null || type == LogixType.Null)
            throw new ArgumentNullException(nameof(type), "Can not set member with null data.");

        //Always use the custom setter if provided.
        if (_setter is not null)
        {
            _setter.Invoke(type);
            return;
        }

        //Otherwise this is a normal decorated or tag member object
        switch (Value)
        {
            case NullType: //Only a tag or parameter element should have this case.
                SetData(type);
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
    /// attribute.
    /// </summary>
    private void SetString(StringType stringType)
    {
        switch (Element.Name.LocalName)
        {
            case L5XName.Data:
            {
                Element.SetAttributeValue(L5XName.Length, stringType.LEN);
                Element.ReplaceNodes(new XCData($"'{stringType}'"));
                return;
            }
            case L5XName.Structure:
            case L5XName.StructureMember:
            {
                var len = Element.Elements().FirstOrDefault(e => e.MemberName() == "LEN");
                len?.SetAttributeValue(L5XName.Value, stringType.LEN);
                var data = Element.Elements().FirstOrDefault(e => e.MemberName() == "DATA");
                data?.ReplaceNodes(new XCData($"'{stringType}'"));
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
    /// element names append "Member" and the have the Name attribute. So we can build them more dynamically this way
    /// and not have to have a method to construct each type manually.
    /// </summary>
    private static XElement CreateElement(string name, LogixType type)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Can not create member with null or empty name", nameof(name));

        if (type is null || type == LogixType.Null)
            throw new ArgumentException("Can not create member with null data type.");

        //Get either the normal data element or if a string then the "Structure" element
        var data = type is StringType stringType ? stringType.SerializeStructure() : type.Serialize();

        //If it happens that the type already contains a member element, then return that with updated name.
        if (data.Name.LocalName.Contains(nameof(Member)))
        {
            data.SetAttributeValue(L5XName.Name, name);
            return data;
        }

        //Otherwise create a new member element with same data and name.
        var member = new XElement($"{data.Name}Member", new XAttribute(L5XName.Name, name));
        member.Add(data.Attributes());
        member.Add(data.Elements());
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
    /// the creation of the <see cref="Member"/>.
    /// </remarks>
    internal static Member ToMember(this XElement element) => new(element);

    /// <summary>
    /// Determines if the current element is one that represents a data element or an element which contains tag data.
    /// </summary>
    /// <param name="element">The element to check.</param>
    /// <returns><c>true</c> if the element is a data element, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// This is a helper to make determining if the element we are working with is a data element which can
    /// be directly deserialized from our logix serializer implementation. This could also help for querying the L5X
    /// element structure.
    /// </remarks>
    internal static bool IsData(this XElement element)
    {
        return element.Name.LocalName is
            L5XName.Data or L5XName.DefaultData or L5XName.DataValue or L5XName.DataValueMember or
            L5XName.Array or L5XName.ArrayMember or L5XName.Element or
            L5XName.Structure or L5XName.StructureMember or
            L5XName.MessageParameters or L5XName.AlarmAnalogParameters or L5XName.AlarmDigitalParameters or
            L5XName.CoordinateSystemParameters or L5XName.AxisParameters or L5XName.MotionGroupParameters;
    }

    internal static bool IsDecoratedData(this XElement element)
    {
        return element.L5XType() is
            L5XName.DataValue or L5XName.DataValueMember or L5XName.Element or
            L5XName.Array or L5XName.ArrayMember or L5XName.Structure or L5XName.StructureMember;
    }
}