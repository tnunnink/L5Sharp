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
/// A <see cref="LogixMember"/> is a special element class that will only be
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public sealed class LogixMember : LogixElement
{
    /// <summary>
    /// The backing attribute for the member. This is only used for custom data formats like alarm and message, where
    /// the data members are attributes of a single element as opposed to child data value or structure elements.
    /// We will support this internally in our LogixMember class by first checking if this field is not null, and if not,
    /// extracting the name and value from it. If this field is null, then we assume the standard decorated member format.
    /// </summary>
    private readonly XAttribute? _attribute;

    /// <inheritdoc />
    public LogixMember(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="LogixMember"/> for the provided element and attribute.
    /// The attribute represents the attribute of the provided member to use for parsing name and value.
    /// This constructor is here to support custom data formats like alarm, message, etc.
    /// </summary>
    /// <param name="element">The root data element.</param>
    /// <param name="attribute">The attribute containing the name/value pair for the member.</param>
    internal LogixMember(XElement element, XAttribute attribute) : base(element)
    {
        _attribute = attribute;
    }

    /// <summary>
    /// Creates a new <see cref="LogixMember"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="data">The <see cref="LogixData"/> contianing the member's data.</param>
    /// <exception cref="ArgumentException"><paramref name="name"/> or <paramref name="data"/> is null or empty.</exception>
    public LogixMember(string name, LogixData data) : base(CreateMember(name, data))
    {
    }

    /// <summary>
    /// The name of the <see cref="LogixMember"/>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the member name or array element index.</value>
    /// <remarks>
    /// All member names are immutable. To change the name of a member for a given tag or data structure, you
    /// must remove and add a new member element. This can only be done for <c>ComplexType</c> objects.
    /// </remarks>
    public string Name =>
        _attribute is not null ? _attribute.MemberName() : Element.MemberName();

    /// <summary>
    /// The value of the <see cref="LogixMember"/>, containing the simple or complex data value for this element.
    /// </summary>
    /// <value>A <see cref="LogixData"/> containing the member data.</value>
    /// <remarks>
    /// <para>
    /// <see cref="LogixMember"/> has special internal methods for setting the underlying value of the data elements.
    /// Note that setting a member value will not change the "type" of the member but rather simply update the underlying
    /// value. This is to prevent the type structure from being inadvertently overwritten. You can change the type
    /// structure to replace members using a <see cref="StructureData"/> for custom types.
    /// </para>
    /// <para>
    /// Also note that you must set the value with the correct corresponding logix type
    /// (e.g., AtomicType = AtomicType, StringType = StringType, ArrayType = ArrayType).
    /// Arrays and structures will join the provided value structure on the member name and forward the call down the
    /// hierarchy to set complex object values.
    /// </para>
    /// </remarks>
    public LogixData Value =>
        _attribute is not null ? AtomicData.Parse(_attribute.Value) : Element.Deserialize<LogixData>();


    /// <summary>
    /// Creates a new XML member element based on the specified name and data.
    /// </summary>
    /// <param name="name">The name of the member to create. This value must not be null or empty.</param>
    /// <param name="data">The data used to generate the member. This value must not be null or of a null data type.</param>
    /// <returns>An <see cref="XElement"/> representing the created member element.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="name"/> is null or empty, or when <paramref name="data"/> is null or invalid.
    /// </exception>
    private static XElement CreateMember(string name, LogixData data)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Can not create member with null or empty name", nameof(name));

        if (data is null or NullData)
            throw new ArgumentException("Can not create member with null data type.");

        //Intercept string types to serialize as a nested structure element, otherwise default to normal serializer.
        var element = data is StringData stringType ? stringType.ToStructureElement() : data.Serialize();

        var elementName = element.Name.LocalName.EndsWith(L5XName.Member)
            ? element.Name.LocalName
            : $"{element.Name.LocalName}{L5XName.Member}";

        var member = new XElement(elementName, new XAttribute(L5XName.Name, name));
        member.Add(element.Attributes().Where(a => a.Name != L5XName.Name));
        member.Add(element.Elements());
        return member;
    }
}