using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types;

/// <summary>
/// A component of a <see cref="LogixType"/> that defines the structure or hierarchy of the type. 
/// </summary>
/// <remarks>
/// <para>
/// Members are used to define the structure of other <see cref="LogixType"/> objects.
/// Since each member holds a strongly typed reference to it's data type,
/// the structure forms a hierarchical tree of nested members and types.
/// </para>
/// <para>
/// This class effectively maps to the DataValueMember, StructureMember, ArrayMember elements of the L5X tag data
/// structures. This class only defines, name and data type, since Dimension, Radix, and ExternalAccess are all either
/// members or the specific <see cref="LogixType"/>, or not inherent in the data structure when serialized or
/// deserialized.
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Member : ILogixSerializable
{
    private LogixType _dataType;

    /// <summary>
    /// Creates a new <see cref="Member"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="type">The <see cref="LogixType"/> object representing the member's data type data.</param>
    /// <exception cref="ArgumentNullException">name or datatype are null.</exception>
    public Member(string name, LogixType type)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _dataType = type ?? throw new ArgumentNullException(nameof(type));
    }

    /// <summary>
    /// Creates a new <see cref="Member"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="L5XException"><c>element</c> does not have required attributes or elements.</exception>
    public Member(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        Name = element.Attribute(L5XName.Name)?.Value ??
               (element.Attribute(L5XName.Index)?.Value ?? throw new L5XException(L5XName.Name, element));
        _dataType = LogixData.Deserialize(element);
    }

    /// <summary>
    /// The name of the <see cref="Member"/>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the member name.</value>
    /// <remarks>
    /// Member name can represent the member name, array element index name, or tag name of an L5X element.
    /// </remarks>
    public string Name { get; }

    /// <summary>
    /// The logix type of the <see cref="Member"/>.
    /// </summary>
    /// <value>A <see cref="LogixType"/> representing the member data type.</value>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <remarks>
    /// The data type creates property the hierarchical structure of complex types.
    /// This type can be atomic, structure, string, or array.
    /// </remarks>
    public LogixType DataType
    {
        get => _dataType;
        set => _dataType = value is not null ? _dataType.Set(value) : throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Converts a <see cref="KeyValuePair{TKey,TValue}"/> to a <see cref="Member"/>.
    /// </summary>
    /// <param name="pair">The value to convert.</param>
    /// <returns>A new <see cref="Member"/> object with the data of the provided type.</returns>
    public static implicit operator Member(KeyValuePair<string, LogixType> pair) => new(pair.Key, pair.Value);

    /// <summary>
    /// Converts a <see cref="Member"/> to a <see cref="KeyValuePair{TKey,TValue}"/>
    /// </summary>
    /// <param name="member">The value to convert.</param>
    /// <returns>A new <see cref="KeyValuePair{TKey,TValue}"/> struct with the data of the provided type.</returns>
    public static implicit operator KeyValuePair<string, LogixType>(Member member) => new(member.Name, member.DataType);

    /// <inheritdoc />
    public XElement Serialize()
    {
        return DataType switch
        {
            AtomicType atomicType => SerializeValueMember(Name, atomicType),
            ArrayType arrayType => SerializeArrayMember(Name, arrayType),
            StringType stringType => SerializeStringMember(Name, stringType),
            StructureType structureType => SerializeStructureMember(Name, structureType),
            _ => throw new NotSupportedException($"Can not serialize member of type {DataType.Name}.")
        };
    }

    private static XElement SerializeValueMember(string name, AtomicType type)
    {
        var element = new XElement(L5XName.DataValueMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        if (type is not BOOL) element.Add(new XAttribute(L5XName.Radix, type.Radix));
        element.Add(new XAttribute(L5XName.Value, type.ToString()));
        return element;
    }

    private static XElement SerializeArrayMember(string name, ArrayType type)
    {
        var element = new XElement(L5XName.ArrayMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(new XAttribute(L5XName.Dimensions, type.Dimensions));
        if (type.Radix != Radix.Null) element.Add(new XAttribute(L5XName.Radix, type.Radix));
        element.Add(type.Members.Select(m =>
        {
            var index = new XElement(L5XName.Element, new XAttribute(L5XName.Index, m.Name));
            switch (m.DataType)
            {
                case AtomicType atomicType:
                    var value = type.Radix != Radix.Null ? atomicType.ToString(type.Radix) : atomicType.ToString();
                    index.Add(new XAttribute(L5XName.Value, value));
                    break;
                case StringType stringType:
                    element.Add(stringType.SerializeStructure());
                    break;
                case StructureType structureType:
                    index.Add(structureType.Serialize());
                    break;
            }

            return index;
        }));
        return element;
    }

    private static XElement SerializeStructureMember(string name, LogixType type)
    {
        var element = new XElement(L5XName.StructureMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(type.Members.Select(m => m.Serialize()));
        return element;
    }

    private static XElement SerializeStringMember(string name, StringType type)
    {
        var element = new XElement(L5XName.StructureMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(type.LEN)));
        len.Add(new XAttribute(L5XName.DataType, type.LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal));
        len.Add(new XAttribute(L5XName.Value, type.LEN));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(type.DATA)));
        data.Add(new XAttribute(L5XName.DataType, type.Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData(type.ToString()));
        element.Add(data);

        return element;
    }
}