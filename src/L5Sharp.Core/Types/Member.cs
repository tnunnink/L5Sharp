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
public class Member : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Member"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member. If <c>null</c> will default to an empty string.</param>
    /// <param name="type">The <see cref="LogixType"/> representing the member's data. If <c>null</c>
    /// will default to <see cref="NullType"/>.</param>
    public Member(string? name, LogixType? type) : base(CreateElement(name, type))
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
        get => GetValue();
        set => SetValue(value);
    }


    #region Internals

    /// <summary>
    /// Retrieves the value associated with the Member by deserializing the data element. This will also
    /// handle root tag and parameter elements by forwarding the call down the element tree using GetData.
    /// </summary>
    private LogixType GetValue()
    {
        var elementsWithData = new[]
        {
            L5XName.Tag,
            L5XName.LocalTag,
            L5XName.ConfigTag,
            L5XName.InputTag,
            L5XName.OutputTag,
            L5XName.Parameter 
        };
        
        return Element.Name.ToString() switch
        {
            var name when elementsWithData.Contains(name) => GetData(),
            _ => Element.Deserialize<LogixType>()
        };
    }

    /// <summary>
    /// Sets the underlying value of the member to the provided logix type. This is a special setter since we want to
    /// actually update the data for the member. We handle the cases for the current value being null, atomic, or some
    /// other structure (complex or array).
    /// </summary>
    private void SetValue(LogixType? type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type), "Can not set member with null data.");

        switch (Value)
        {
            //If no data is set, this should be a root tag member, and we can use the underlying SetData to add the
            //new data element. Once set then we only ever update the value from the member.
            case NullType:
                SetData(type);
                return;
            //Only atomics ultimately need their value set. WE are sneakily doing this through the backing element.
            //This will first convert the incoming type to the target type to ensure no type conversion issues.
            //Then will format with the current radix format.
            case AtomicType atomicType:
            {
                var atomic = (AtomicType)Convert.ChangeType(type, atomicType.GetType());
                Element.SetAttributeValue(L5XName.Value, atomic.ToString(atomicType.Radix));
                return;
            }
            //Finally, everything else is a structure in which we join members, and forward the call down the hierarchy
            //until we set all matching data.
            default:
                Value.Members
                    .Join(type.Members, m => m.Name, m => m.Name, (t, s) => new { t, s })
                    .ToList()
                    .ForEach(x => { x.t.Value = x.s.Value; });
                break;
        }
    }

    /// <summary>
    /// Creates a new member element to contain the underlying member data for this object using the provided name
    /// and <see cref="LogixType"/> data.
    /// </summary>
    private static XElement CreateElement(string? name, LogixType? type)
    {
        name ??= string.Empty;
        type ??= LogixType.Null;

        return type switch
        {
            AtomicType atomicType => CreateValueMember(name, atomicType),
            ArrayType arrayType => CreateArrayMember(name, arrayType),
            StringType stringType => CreateStringMember(name, stringType),
            StructureType structureType => CreateStructureMember(name, structureType),
            _ => throw new NotSupportedException($"Can not create member with type {type.Name}.")
        };
    }

    /// <summary>
    /// Handles serializing a <see cref="AtomicType"/> as a ValueMember element.
    /// </summary>
    private static XElement CreateValueMember(string name, AtomicType type)
    {
        var element = new XElement(L5XName.DataValueMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        if (type is not BOOL) element.Add(new XAttribute(L5XName.Radix, type.Radix));
        element.Add(new XAttribute(L5XName.Value, type));
        return element;
    }

    /// <summary>
    /// Handles serializing a <see cref="ArrayType"/> as a ArrayMember element.
    /// </summary>
    private static XElement CreateArrayMember(string name, ArrayType type)
    {
        var element = new XElement(L5XName.ArrayMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(new XAttribute(L5XName.Dimensions, type.Dimensions));
        if (type.Radix != Radix.Null) element.Add(new XAttribute(L5XName.Radix, type.Radix));

        element.Add(type.Members.Select(m =>
        {
            var index = new XElement(L5XName.Element, new XAttribute(L5XName.Index, m.Name));

            switch (m.Value)
            {
                case AtomicType atomicType:
                    var value = type.Radix != Radix.Null ? atomicType.ToString(type.Radix) : atomicType.ToString();
                    index.Add(new XAttribute(L5XName.Value, value));
                    break;
                case StringType stringType:
                    index.Add(stringType.SerializeStructure());
                    break;
                case StructureType structureType:
                    index.Add(structureType.Serialize());
                    break;
            }

            return index;
        }));
        return element;
    }

    /// <summary>
    /// Handles serializing a <see cref="StructureType"/> as a StructureMember element.
    /// </summary>
    private static XElement CreateStructureMember(string name, LogixType type)
    {
        var element = new XElement(L5XName.StructureMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(type.Members.Select(m => m.Serialize()));
        return element;
    }

    /// <summary>
    /// Handles serializing a <see cref="StringType"/> as a StructureMember element.
    /// </summary>
    private static XElement CreateStringMember(string name, StringType type)
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
        data.Add(new XCData($"'{type}'"));
        element.Add(data);

        return element;
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