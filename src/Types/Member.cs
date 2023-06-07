using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Types;

/// <summary>
/// A component of a <see cref="L5Sharp.LogixType"/> that defines the structure or members of the type. 
/// </summary>
/// <remarks>
/// <para>
/// Members are used to define the structure of other <see cref="L5Sharp.LogixType"/> objects.
/// Since each member holds a strongly typed reference to it's data type,
/// the structure forms a hierarchical tree of nested members and types.
/// </para>
/// <para>
/// This class effectively maps to the DataValueMember, StructureMember, ArrayMember elements of the L5X tag data
/// structures. This class only defines, name and data type, since Dimension, Radix, and ExternalAccess are all either
/// members or the specific <see cref="L5Sharp.LogixType"/> set, or not inherent in the data structure when serialized or
/// deserialized.
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Member : LogixEntity<Member>
{
    /// <summary>
    /// Creates a new <see cref="Member"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="logixType">The <see cref="L5Sharp.LogixType"/> object representing the member's data type data.</param>
    /// <exception cref="ArgumentNullException">name or datatype are null.</exception>
    public Member(string name, L5Sharp.LogixType logixType) : base(GenerateElement(name, logixType))
    {
    }

    /// <inheritdoc />
    public Member(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name of the <see cref="Member"/> instance.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the member name.</returns>
    /// <remarks>
    /// The is the name of the member element for structure, array, and data value members.
    /// For array index element members, the name is the index of the array.
    /// This property is immutable as it defines the structure of the current type.
    /// </remarks>
    public string Name => Element.MemberName() ?? throw new L5XException(Element);

    /// <summary>
    /// The logix type of the <see cref="Member"/> instance.
    /// </summary>
    /// <returns>A <see cref="L5Sharp.LogixType"/> representing the member data type.</returns>
    /// <remarks>
    /// The data type creates the hierarchical structure of complex types.
    /// This type can be atomic, structure, string, or array. Setting this property updates the underlying entity element
    /// values.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <exception cref="ArgumentException">The provided logix type does not match the current data type.</exception>
    public L5Sharp.LogixType DataType
    {
        get => LogixType.Deserialize(Element);
        set => SetData(Element, value);
    }

    // Following region contains methods for setting the values of the underlying element member given a logix type.
    // These are used when updating the member element.

    #region SetData

    /// <summary>
    /// Handles setting the underlying <see cref="XElement"/> properties for a given <see cref="L5Sharp.LogixType"/> value.
    /// </summary>
    private static void SetData(XElement element, L5Sharp.LogixType value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        switch (element.Name.ToString())
        {
            case L5XName.Tag:
                SetTag(element, value);
                break;
            case L5XName.DataValueMember:
                SetValueMember(element, value);
                break;
            case L5XName.ArrayMember:
                SetArrayMember(element, value);
                break;
            case L5XName.Element:
                SetElementMember(element, value);
                break;
            case L5XName.StructureMember:
                SetStructureMember(element, value);
                break;
        }
    }

    /// <summary>
    /// Handles setting the root data for a tag element and updating the properties of the tag component.
    /// </summary>
    private static void SetTag(XElement element, L5Sharp.LogixType value)
    {
        var data = element.Elements(L5XName.Data)
            .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value != DataFormat.L5K)?.Elements().FirstOrDefault();

        // We will always accept the value at the root tag element to allow complete replacement of the type/data.
        if (data is null)
        {
            element.Add(GenerateData(value));
        }
        else
        {
            data.ReplaceWith(GenerateData(value));
        }

        UpdateTagProperties(element, value);
    }

    /// <summary>
    /// Updates the data type, dimensions, and radix properties of the root tag to keep data in sync.
    /// </summary>
    private static void UpdateTagProperties(XElement element, L5Sharp.LogixType value)
    {
        element.SetAttributeValue(L5XName.DataType, value.Name);

        switch (value)
        {
            case AtomicType atomicType:
                element.SetAttributeValue(L5XName.Radix, atomicType.Radix);
                break;
            case ArrayType<AtomicType> { Dimensions.IsEmpty: false } atomicArray:
                element.SetAttributeValue(L5XName.Dimensions, atomicArray.Dimensions);
                element.SetAttributeValue(L5XName.Radix, atomicArray[0].Radix.ToString());
                break;
            case ArrayType<StructureType> { Dimensions.IsEmpty: false } structureArray:
                element.SetAttributeValue(L5XName.Dimensions, structureArray.Dimensions);
                break;
        }
    }

    /// <summary>
    /// Handles setting the value attribute of a data value member element given an atomic type.
    /// </summary>
    private static void SetValueMember(XElement element, L5Sharp.LogixType value)
    {
        if (value is not AtomicType atomicType)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        element.SetAttributeValue(L5XName.Value, atomicType);
    }

    /// <summary>
    /// Handles setting the values of the elements of a array member element given an array type.
    /// </summary>
    private static void SetArrayMember(XElement element, L5Sharp.LogixType value)
    {
        if (value is not ArrayType<L5Sharp.LogixType> arrayType)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        var dataType = element.Attribute(L5XName.DataType)?.Value;
        if (dataType != value.Name)
            throw new ArgumentException(
                $"The type name {value.Name} does not match the current array type {dataType}.");

        if (arrayType.Dimensions.Length > element.Elements().Count())
            throw new ArgumentOutOfRangeException(nameof(value),
                "The length of the provided array type is out of range for the current array.");

        var pairs = element.Elements().Join(value.Members, e => e.MemberName(), m => m.Name, (e, m) => new { e, m });

        foreach (var pair in pairs)
            SetData(pair.e, pair.m.DataType);
    }

    /// <summary>
    /// Handles setting the values of an array index element given a generic logix type.
    /// </summary>
    private static void SetElementMember(XElement element, L5Sharp.LogixType value)
    {
        switch (value)
        {
            case AtomicType:
                SetValueMember(element, value);
                break;
            case StringType:
                SetStringMember(element, value);
                break;
            case StructureType:
                SetStructureMember(element, value);
                break;
        }
    }

    /// <summary>
    /// Handles setting the values of the members of a structure member element given a structure type.
    /// Will also handle special case of string member structure.
    /// </summary>
    private static void SetStructureMember(XElement element, L5Sharp.LogixType value)
    {
        if (value is not StructureType)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        var dataType = element.Attribute(L5XName.DataType)?.Value;
        if (dataType != value.Name)
            throw new ArgumentException(
                $"The type name {value.Name} does not match the current structure type {dataType}.");

        if (element.HasLogixStringStructure())
        {
            SetStringMember(element, value);
            return;
        }

        var pairs = element.Elements().Join(value.Members, e => e.MemberName(), m => m.Name, (e, m) => new { e, m });

        foreach (var pair in pairs)
            SetData(pair.e, pair.m.DataType);
    }

    /// <summary>
    /// Handles setting the values of a string structure member type.
    /// </summary>
    private static void SetStringMember(XElement element, L5Sharp.LogixType value)
    {
        if (value is not StringType stringType)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        var len = element.Elements(L5XName.DataValueMember)
            .FirstOrDefault(e => e.MemberName() == nameof(stringType.LEN));
        len?.SetAttributeValue(L5XName.Value, stringType.LEN);

        var data = element.Elements(L5XName.DataValueMember)
            .FirstOrDefault(e => e.MemberName() == nameof(stringType.DATA));

        if (data is not null)
            data.Value = stringType.ToString();
    }

    #endregion

    // Following region contains methods for generating specific logix data structure elements given a logix type.
    // These are used when creating the member element.

    #region GenerateElement

    private static XElement GenerateElement(string name, L5Sharp.LogixType type)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (type is null)
            throw new ArgumentNullException(nameof(type));

        return type switch
        {
            AtomicType atomicType => GenerateValueMember(name, atomicType),
            ArrayType arrayType => GenerateArrayMember(name, arrayType),
            StringType stringType => GenerateStringMember(name, stringType),
            StructureType structureType => GenerateStructureMember(name, structureType),
            _ => throw new NotSupportedException(
                $"The logix type {type.GetType()} is not supported by the {typeof(Member)} entity.")
        };
    }

    private static XElement GenerateValueMember(string name, AtomicType type)
    {
        var element = new XElement(L5XName.DataValueMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));

        if (type is not BOOL)
            element.Add(new XAttribute(L5XName.Radix, type.Radix));

        element.Add(new XAttribute(L5XName.Value, type.ToString()));
        return element;
    }

    private static XElement GenerateStructureMember(string name, L5Sharp.LogixType type)
    {
        var element = new XElement(L5XName.StructureMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(type.Members.Select(m => m.Serialize()));
        return element;
    }

    private static XElement GenerateArrayMember(string name, ArrayType type)
    {
        var element = new XElement(L5XName.ArrayMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(new XAttribute(L5XName.Dimensions, type.Dimensions));
        element.Add(type.Members.Select(m => GenerateElementMember(m.Name, m.DataType)));
        return element;
    }

    private static XElement GenerateElementMember(string name, L5Sharp.LogixType type)
    {
        var element = new XElement(L5XName.Element, new XAttribute(L5XName.Index, name));

        switch (type)
        {
            case AtomicType atomicType:
                element.Add(new XAttribute(L5XName.Value, atomicType.ToString()));
                break;
            case StringType stringType:
                element.Add(GenerateStringStructure(stringType));
                break;
            case StructureType structureType:
                element.Add(structureType.Serialize());
                break;
        }

        return element;
    }

    private static XElement GenerateStringStructure(StringType type)
    {
        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, type.Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(type.LEN)));
        len.Add(new XAttribute(L5XName.DataType, type.LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal.Value));
        len.Add(new XAttribute(L5XName.Value, type.LEN.ToString()));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(type.DATA)));
        data.Add(new XAttribute(L5XName.DataType, type.Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
        data.Add(new XCData(type.ToString()));
        element.Add(data);

        return element;
    }

    private static XElement GenerateStringMember(string name, L5Sharp.LogixType type)
    {
        var element = new XElement(L5XName.StructureMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, "LEN"));
        len.Add(new XAttribute(L5XName.DataType, "DINT"));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal.Value));
        len.Add(new XAttribute(L5XName.Value, type.ToString().Length));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, "DATA"));
        data.Add(new XAttribute(L5XName.DataType, name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
        data.Add(new XCData(type.ToString()));
        element.Add(data);

        return element;
    }

    private static XElement GenerateData(L5Sharp.LogixType type)
    {
        return type switch
        {
            StringType stringType => stringType.Serialize(),
            ALARM_ANALOG alarmAnalog => GenerateAlarmData(alarmAnalog),
            ALARM_DIGITAL alarmDigital => GenerateAlarmData(alarmDigital),
            MESSAGE message => GenerateMessageData(message),
            ILogixSerializable serializable => GenerateDecoratedData(serializable),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static XElement GenerateDecoratedData(ILogixSerializable type)
    {
        var decorated = new XElement(L5XName.Data, new XAttribute(L5XName.Format, DataFormat.Decorated));
        decorated.Add(type.Serialize());
        return decorated;
    }

    private static XElement GenerateAlarmData(ILogixSerializable type)
    {
        var decorated = new XElement(L5XName.Data, new XAttribute(L5XName.Format, DataFormat.Alarm));
        decorated.Add(type.Serialize());
        return decorated;
    }

    private static XElement GenerateMessageData(ILogixSerializable type)
    {
        var decorated = new XElement(L5XName.Data, new XAttribute(L5XName.Format, DataFormat.Message));
        decorated.Add(type.Serialize());
        return decorated;
    }

    #endregion
}