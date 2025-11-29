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
[LogixElement(L5XName.DataValueMember)]
[LogixElement(L5XName.StructureMember)]
[LogixElement(L5XName.ArrayMember)]
[LogixElement(L5XName.Element)]
public sealed class LogixMember : LogixElement
{
    /// <summary>
    /// Custom name for the "virtual" member. This is for special types that may not have decorated data but need to
    /// represent a member object for which a tag can reference and get/set data for.
    /// </summary>
    private readonly string? _name;

    /// <summary>
    /// The custom getter function for a "virtual" member. If not null, this will always be used in place of the normal
    /// getter methods.
    /// </summary>
    private readonly Func<LogixData?>? _getter;

    /// <summary>
    /// The custom setter function for a "virtual" member. If not null, this will always be used in place of the normal
    /// setter methods.
    /// </summary>
    private readonly Action<LogixData>? _setter;

    /// <inheritdoc />
    public LogixMember(XElement element) : base(element)
    {
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
    /// Creates a new "virtual" <see cref="LogixMember"/> object with the provided name, getter, and setter functions. There
    /// is no backing XElement in this scenario. However, the provided delegates will be called in place of the get/set for
    /// <see cref="Value"/> to allow special logix type objects to provide a mapping of a member object to its custom
    /// backing L5X data element. This is used for the custom classes like <see cref="ALARM_ANALOG"/>,
    /// <see cref="ALARM_DIGITAL"/>, <see cref="MESSAGE"/> and others.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="getter">The function that gets the logix type value for this member.</param>
    /// <param name="setter">The function that sets the underlying L5X data for this member.</param>
    internal LogixMember(string name, Func<LogixData?> getter, Action<LogixData> setter) : base(
        new XElement(L5XName.Data))
    {
        _name = name;
        _getter = getter;
        _setter = setter;
    }

    /// <summary>
    /// Creates a new virtual <see cref="LogixMember"/> from the provided <see cref="XAttribute"/> as a data value
    /// member instance.
    /// </summary>
    /// <param name="attribute"></param>
    internal LogixMember(XAttribute attribute) : base(new XElement(L5XName.DataValueMember))
    {
        _name = attribute.Name.LocalName;

        _getter = () =>
            Radix.TryInfer(attribute.Value, out _)
                ? Radix.ParseAtomic(attribute.Value)
                : new STRING(attribute.Value);

        _setter = data => attribute.Parent?.SetAttributeValue(attribute.Name.LocalName, data.ToString());
    }

    /// <summary>
    /// The name of the <see cref="LogixMember"/>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the member name or array element index.</value>
    /// <remarks>
    /// All member names are immutable. To change the name of a member for a given tag or data structure, you
    /// must remove and add a new member element. This can only be done for <c>ComplexType</c> objects.
    /// </remarks>
    public string Name => _name ?? Element.MemberName();

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
    public LogixData Value
    {
        get => GetData();
        set => SetData(value);
    }

    #region Internals

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private LogixData GetData()
    {
        //Always use the custom getter if provided.
        if (_getter is not null)
        {
            return _getter.Invoke() ?? LogixType.Null;
        }

        //Since the member element represents the data value or structure as well, we need to manually handle the
        //construction based on the element type and data type name. We can't use the serializer since it would just
        //give us back the same LogixMember instance.
        return Element.Name.LocalName switch
        {
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(BOOL) => new BOOL(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(SINT) => new SINT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(USINT) => new USINT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(INT) => new INT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(UINT) => new UINT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(DINT) => new DINT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(UDINT) => new UDINT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(LINT) => new LINT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(ULINT) => new ULINT(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(REAL) => new REAL(Element),
            L5XName.DataValueMember or L5XName.Element when Element.DataType() == nameof(LREAL) => new LREAL(Element),
            L5XName.DataValueMember when Element.FirstNode is XCData => new StringData(Element),
            L5XName.ArrayMember => new ArrayData(Element),
            L5XName.Element
                when Element.Element(L5XName.Structure) is { } structure && IsStringData(structure) =>
                new StringData(structure),
            L5XName.Element
                when Element.Element(L5XName.Structure) is { } structure =>
                new StructureData(structure),
            L5XName.StructureMember when IsStringData(Element) => new StringData(Element),
            L5XName.StructureMember => new StructureData(Element),
            _ => throw new InvalidOperationException($"Invalid data element for member: '{Element.Name.LocalName}'")
        };
    }

    /// <summary>
    /// Sets the underlying value of the member to the provided logix type.
    /// </summary>
    /// <param name="data"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <remarks>
    /// This is a special setter specific to member,
    /// as this is where we update the data for a given data structure. We handle the cases for all base logix types,
    /// including null, atomic, string, array, and structure. If the provided type cannot be cast to the current type
    /// then we will get a <see cref="InvalidCastException"/>. This will help let the user know that they are setting
    /// values incorrectly. 
    /// </remarks>
    private void SetData(LogixData? data)
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

        //Otherwise this is a normal decorated data element, and we need to handle the set based on the current value.
        switch (Value)
        {
            case AtomicData current:
                SetAtomic(current, data.As<AtomicData>());
                break;
            case StringData:
                SetString(data.As<StringData>());
                break;
            case ArrayData:
            case StructureData:
                SetStructure(data);
                break;
        }
    }

    /// <summary>
    /// Sets the <c>Value</c> for whatever data element is the element for this member.
    /// This will first convert the incoming type to the current type to ensure no type conversion issues (or throw exception if so).
    /// Then will format with the current radix format.
    /// </summary>
    private void SetAtomic(AtomicData current, AtomicData atomic)
    {
        //Ensure we can convert the provided atomic to the current atomic type using the IConvertible implementation.
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
            case L5XName.Data when Element.Attribute(L5XName.Format)?.Value == DataFormat.String:
            {
                Element.SetAttributeValue(L5XName.Length, stringData.Length);
                Element.ReplaceNodes(new XCData($"'{stringData}'"));
                return;
            }
            case L5XName.Structure when IsStringData(Element):
            case L5XName.StructureMember when IsStringData(Element):
            {
                Element.Elements()
                    .First(e => e.MemberName() == "LEN")
                    .SetAttributeValue(L5XName.Value, stringData.Length);

                Element.Elements()
                    .First(e => e.MemberName() == "DATA")
                    .ReplaceNodes(new XCData($"'{stringData}'"));

                return;
            }
            default:
                throw new InvalidOperationException("Current member type is not a valid string data structure.");
        }
    }

    /// <summary>
    /// Joins members of the current data with the provided data instance on name and sets each value.
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
    /// Determines if the provided element has a structure that represents a <see cref="StringData"/> structure,
    /// structure member.
    /// </summary>
    /// <param name="element">The element to check for the known string data structure.</param>
    /// <returns><c>true</c> if the element has the string type structure, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// This is needed to determine if we are deserializing a structure type or string type. String structure is unique
    /// in that it will have a data value member called DATA with an ASCII radix, a non-null element value, and a
    /// data type attribute value equal to that of the parent structure element attribute. If we don't intercept this
    /// structure before deserializing it, we will encounter exceptions because it doesn't conform to the normal
    /// convention that data value members should represent and atomic structure.
    /// </remarks>
    private static bool IsStringData(XElement element)
    {
        if (element.Name.LocalName is L5XName.Structure or L5XName.StructureMember)
        {
            return element.Elements(L5XName.DataValueMember).Any(e =>
                e.Attribute(L5XName.Name)?.Value == "DATA"
                && e.Attribute(L5XName.DataType)?.Value == e.Parent?.Attribute(L5XName.DataType)?.Value
                && e.Attribute(L5XName.Radix)?.Value == "ASCII"
                && e.FirstNode is XCData);
        }

        return false;
    }

    /// <summary>
    /// Creates a new member element to contain the underlying member data for this object using the provided name
    /// and <see cref="LogixData"/> instance. Members are really only different from the base-decorated data in that the
    /// element names append "Member" and they have the <c>Name</c> attribute.
    /// So we can build them more dynamically this way and not have to have a method to construct each type manually.
    /// </summary>
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

    #endregion
}