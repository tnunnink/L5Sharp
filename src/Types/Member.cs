using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Types;

/// <summary>
/// A component of a <see cref="LogixType"/> that defines the structure or members of the type. 
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
/// members or the specific <see cref="LogixType"/> set, or not inherent in the data structure when serialized or
/// deserialized.
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Member : LogixEntity<Member>
{
    /// <inheritdoc />
    public Member(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Member"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="logixType">The <see cref="LogixType"/> object representing the member's data type data.</param>
    /// <exception cref="ArgumentNullException">name or datatype are null.</exception>
    public Member(string name, LogixType logixType) : base(GenerateElement(name, logixType))
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
    /// <returns>A <see cref="LogixType"/> representing the member data type.</returns>
    /// <remarks>
    /// The data type creates property the hierarchical structure of complex types.
    /// This type can be atomic, structure, string, or array. Setting this property updates the underlying entity element
    /// values.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    public LogixType DataType
    {
        get => GetData(Element);
        set => SetData(Element, value);
    }

    // Following region contains methods for getting/deserializing the underlying element member to a logix type.

    #region GetData

    /// <summary>
    /// Handles deserializing a element to a logix type. This will forward call to appropriate method depending on the
    /// element name.  
    /// </summary>
    private static LogixType GetData(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        return element.Name.ToString() switch
        {
            L5XName.Tag => GetTag(element),
            L5XName.Data => GetFormatted(element),
            L5XName.DataValue => GetAtomic(element),
            L5XName.DataValueMember => GetAtomic(element),
            L5XName.Element => GetElement(element),
            L5XName.Array => GetArray(element),
            L5XName.ArrayMember => GetArray(element),
            L5XName.Structure => GetStructure(element),
            L5XName.StructureMember => GetStructure(element),
            L5XName.AlarmAnalogParameters => new ALARM_ANALOG(element),
            L5XName.AlarmDigitalParameters => new ALARM_DIGITAL(element),
            L5XName.MessageParameters => new MESSAGE(element),
            _ => throw new NotSupportedException(
                $"The element name '{element.Name}' is not able to be used to create a logix type.")
        };
    }

    /// <summary>
    /// Handles deserializing a tag element to a logix type. This method will get the child data element and call back
    /// to <see cref="GetData"/>. This is here since we build a member for a Tag component internally to represent the
    /// Value property of the object.
    /// </summary>
    private static LogixType GetTag(XContainer element)
    {
        var data = element.Elements(L5XName.Data)
            .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value != DataFormat.L5K);

        return data is not null ? GetData(data) : LogixType.Null;
    }

    /// <summary>
    /// Handles deserializing a formatted data element to a logix type. This method will forward call down the chain
    /// based on the format of the data structure.
    /// </summary>
    private static LogixType GetFormatted(XElement element)
    {
        DataFormat.TryFromName(element.Attribute(L5XName.Format)?.Value, out var format);

        if (format is null) return LogixType.Null;

        if (format == DataFormat.String)
            return GetString(element);

        return element.FirstNode is XElement root ? GetData(root) : LogixType.Null;
    }

    /// <summary>
    /// Handles deserializing an element to a atomic value type logix type. This method will call upon <see cref="Atomic"/>
    /// to generate the known concrete atomic type we need.
    /// </summary>
    private static AtomicType GetAtomic(XElement element)
    {
        var dataType = element.LogixType();
        var value = element.Attribute(L5XName.Value)?.Value ?? throw new L5XException(L5XName.Value, element);
        return Atomic.Parse(dataType, value);
    }

    /// <summary>
    /// Handles deserializing an array element to a array type logix type.
    /// </summary>
    private static ArrayType GetArray(XElement element) => new(element);

    /// <summary>
    /// Handles deserializing an array index element to a logix type, either atomic or structure depending on the state
    /// of the element.
    /// </summary>
    private static LogixType GetElement(XElement element)
    {
        if (element.Attribute(L5XName.Value) is not null)
        {
            if (element.Parent is null)
                throw new InvalidOperationException("Can not retrieve type from disconnected element.");
            
            var dataType = element.Parent.LogixType();
            var value = element.Attribute(L5XName.Value)!.Value;
            return Atomic.Parse(dataType, value);
        }

        var structure = element.Element(L5XName.Structure);
        return structure is not null ? GetStructure(structure) : LogixType.Null;
    }

    /// <summary>
    /// Handles deserializing an element to a structure type logix type. Will check <see cref="LogixSerializer"/> for registered
    /// type to create the concrete type if available. Otherwise we resort to a more generic string or complex type object.
    /// </summary>
    private static StructureType GetStructure(XElement element)
    {
        var dataType = element.LogixType();

        if (LogixSerializer.IsRegistered(dataType))
            return (StructureType)LogixSerializer.Deserialize(dataType, element);

        return element.HasLogixStringStructure() ? new StringType(element) : new ComplexType(element);
    }

    /// <summary>
    /// Handles deserializing an element to a string type logix type.
    /// </summary>
    private static StringType GetString(XElement element)
    {
        if (element.Parent is null)
            throw new InvalidOperationException("Can not retrieve type from disconnected element.");
        
        var dataType = element.Parent.LogixType();

        if (LogixSerializer.IsRegistered(dataType))
            return (StringType)LogixSerializer.Deserialize(dataType, element);

        return new StringType(element);
    }

    #endregion

    // Following region contains methods for setting the values of the underlying element member given a logix type.

    #region SetData

    /// <summary>
    /// Handles setting the underlying <see cref="XElement"/> properties for a given <see cref="LogixType"/> value.
    /// </summary>
    private static void SetData(XElement element, LogixType value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        switch (element.Name.ToString())
        {
            case L5XName.Tag:
                SetTag(element, value);
                break;
            case L5XName.Data:
                SetFormatted(element, value);
                break;
            case L5XName.DataValue:
                SetAtomic(element, value);
                break;
            case L5XName.DataValueMember:
                SetAtomic(element, value);
                break;
            case L5XName.Array:
                SetArray(element, value);
                break;
            case L5XName.ArrayMember:
                SetArray(element, value);
                break;
            case L5XName.Element:
                SetElement(element, value);
                break;
            case L5XName.Structure:
                SetStructure(element, value);
                break;
            case L5XName.StructureMember:
                SetStructure(element, value);
                break;
            case L5XName.AlarmAnalogParameters:
                SetAlarmAnalog(element, value);
                break;
            case L5XName.AlarmDigitalParameters:
                //todo
                break;
            case L5XName.MessageParameters:
                //todo
                break;
        }
    }

    private static void SetAlarmAnalog(XElement element, LogixType value)
    {
        if (value is not ALARM_ANALOG alarm)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        var pairs = element.Attributes()
            .Join(alarm.Members, a => a.Name, m => m.Name, (a, m) => new { a, m });

        foreach (var pair in pairs)
        {
            
        }
    }

    /// <summary>
    /// Handles setting the root data for a tag element and updating the properties of the tag component.
    /// </summary>
    private static void SetTag(XElement element, LogixType value)
    {
        var data = element.Elements(L5XName.Data)
            .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value != DataFormat.L5K);

        // If uninitialized, add the root data to the base tag element and update the tag attributes.
        if (data?.Attribute(L5XName.Format) is null)
        {
            element.Add(GenerateData(value));
            element.SetAttributeValue(L5XName.DataType, value.Name);
            switch (value)
            {
                case AtomicType atomicType:
                    element.SetAttributeValue(L5XName.Radix, atomicType.Radix);
                    break;
                case ArrayType { Dimensions.IsEmpty: false } arrayType:
                    element.SetAttributeValue(L5XName.Dimensions, arrayType.Dimensions);
                    if (arrayType.Radix != Radix.Null) element.SetAttributeValue(L5XName.Radix, arrayType.Radix);
                    break;
            }

            return;
        }

        SetData(data, value);
    }

    /// <summary>
    /// Handles routing of the set value to appropriate method depending on the current state of the data element.
    /// </summary>
    /// <param name="element">The target data element.</param>
    /// <param name="value">The provided logix type.</param>
    private static void SetFormatted(XElement element, LogixType value)
    {
        DataFormat.TryFromName(element.Attribute(L5XName.Format)?.Value, out var format);
        var tag = element.Parent;

        switch (format)
        {
            case null when tag is null:
                throw new InvalidOperationException("Can not set data for disconnected element with no format.");
            case null:
                SetData(tag, value);
                return;
        }

        if (format == DataFormat.String)
            SetString(element, value);

        var root = element.FirstNode as XElement;

        switch (root)
        {
            case null when tag is null:
                throw new InvalidOperationException("Can not set data for disconnected element with no child.");
            case null:
                element.Remove();
                SetData(tag, value);
                return;
        }

        SetData(root, value);
    }

    /// <summary>
    /// Handles setting the a data value (DataValue, DataValueMember, Element) attributes given an atomic type.
    /// </summary>
    private static void SetAtomic(XElement element, LogixType value)
    {
        if (value is not AtomicType atomicType)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        if (element.Name == L5XName.DataValue)
        {
            //Set both properties in this case.
            element.SetAttributeValue(L5XName.Radix, atomicType.Radix);
            element.SetAttributeValue(L5XName.Value, atomicType);
        }

        if (element.Name == L5XName.DataValueMember)
        {
            // Conform the radix of the member to that of the element.
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>();
            var atomic = radix is not null ? atomicType.ToString(radix) : atomicType.ToString();
            element.SetAttributeValue(L5XName.Value, atomic);
            return;
        }

        if (element.Name != L5XName.Element) return;
        {
            // Conform the radix of the element to that of the parent array element.
            var radix = element.Parent?.Attribute(L5XName.Radix)?.Value.Parse<Radix>();
            var atomic = radix is not null ? atomicType.ToString(radix) : atomicType.ToString();
            element.SetAttributeValue(L5XName.Value, atomic);
        }
    }

    /// <summary>
    /// Handles setting the values of the elements of a array member element given an array type.
    /// </summary>
    private static void SetArray(XElement element, LogixType value)
    {
        if (value is not ArrayType arrayType)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        var pairs = element.Elements()
            .Join(arrayType.Members, e => e.MemberName(), m => m.Name, (e, m) => new { e, m });

        foreach (var pair in pairs)
            SetData(pair.e, pair.m.DataType);
    }

    /// <summary>
    /// Handles setting the values of an array index element given a generic logix type.
    /// </summary>
    private static void SetElement(XElement element, LogixType value)
    {
        var dataType = element.Parent?.LogixType() ?? throw new L5XException(L5XName.DataType, element);

        if (Atomic.IsAtomic(dataType))
        {
            SetAtomic(element, value);
            return;
        }

        var structure = element.Element(L5XName.Structure);

        if (structure is null) throw new L5XException(L5XName.Structure, element);

        if (value.Name != dataType)
            throw new ArgumentException($"Can not set array type {dataType} with structure {value.Name}");

        SetStructure(structure, value);
    }

    /// <summary>
    /// Handles setting the values of the members of a structure member element given a structure type.
    /// Will also handle special case of string member structure.
    /// </summary>
    private static void SetStructure(XElement element, LogixType value)
    {
        if (value is not StructureType structureType)
            throw new ArgumentException(
                $"The underlying element '{element.Name}' can not be set by logix type {value.GetType()}.");

        // String types can be disguised as a structure types.
        if (element.HasLogixStringStructure())
        {
            SetString(element, value);
            return;
        }

        var pairs = element.Elements()
            .Join(structureType.Members, e => e.MemberName(), m => m.Name, (e, m) => new { e, m });

        foreach (var pair in pairs)
            SetData(pair.e, pair.m.DataType);
    }

    /// <summary>
    /// Handles setting the value of a string formatted data element given a string type.
    /// </summary>
    private static void SetString(XElement element, LogixType value)
    {
        if (value is not StringType stringType)
            throw new ArgumentException(
                $"The underlying string type element can not be set by logix type {value.GetType()}.");

        if (element.Parent is null)
            throw new InvalidOperationException("Can not set disconnected data element with not parent.");

        switch (element.Name.ToString())
        {
            case L5XName.Data:
                element.ReplaceWith(stringType.Serialize());
                break;
            case L5XName.Structure:
                element.ReplaceWith(GenerateStringStructure(stringType));
                break;
            case L5XName.StructureMember:
                var name = element.MemberName() ?? throw new InvalidOperationException();
                element.ReplaceWith(GenerateStringMember(name, stringType));
                break;
        }
    }

    #endregion

    // Following region contains methods for generating specific logix data structure elements given a logix type.
    // These are used when creating the member element when only given a logix type and member name.

    #region GenerateElement

    private static XElement GenerateElement(string name, LogixType type)
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
            _ => throw new NotSupportedException($"Can not generate member element for type {type}.")
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

    private static XElement GenerateStructureMember(string name, LogixType type)
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
        if (type.Radix != Radix.Null) element.Add(new XAttribute(L5XName.Radix, type.Radix));
        element.Add(type.Members.Select(m => m.Serialize()));
        return element;
    }

    private static XElement GenerateStringMember(string name, StringType type)
    {
        var element = new XElement(L5XName.StructureMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(GenerateStringMembers(type));
        return element;
    }

    private static XElement GenerateStringStructure(StringType type)
    {
        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(GenerateStringMembers(type));
        return element;
    }

    private static IEnumerable<XElement> GenerateStringMembers(StringType type)
    {
        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(type.LEN)));
        len.Add(new XAttribute(L5XName.DataType, type.LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal));
        len.Add(new XAttribute(L5XName.Value, type.LEN));
        yield return len;

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(type.DATA)));
        data.Add(new XAttribute(L5XName.DataType, type.Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData(type.ToString()));
        yield return data;
    }

    private static XElement GenerateData(LogixType type)
    {
        return type switch
        {
            StringType stringType => stringType.Serialize(),
            ALARM_ANALOG alarmAnalog => GenerateAlarmData(alarmAnalog),
            ALARM_DIGITAL alarmDigital => GenerateAlarmData(alarmDigital),
            MESSAGE message => GenerateMessageData(message),
            ILogixSerializable serializable => GenerateDecoratedData(serializable)
        };
    }

    private static XElement GenerateDecoratedData(ILogixSerializable type)
    {
        var data = new XElement(L5XName.Data, new XAttribute(L5XName.Format, DataFormat.Decorated));
        data.Add(type.Serialize());
        return data;
    }

    private static XElement GenerateAlarmData(ILogixSerializable type)
    {
        var data = new XElement(L5XName.Data, new XAttribute(L5XName.Format, DataFormat.Alarm));
        data.Add(type.Serialize());
        return data;
    }

    private static XElement GenerateMessageData(ILogixSerializable type)
    {
        var data = new XElement(L5XName.Data, new XAttribute(L5XName.Format, DataFormat.Message));
        data.Add(type.Serialize());
        return data;
    }

    #endregion
}