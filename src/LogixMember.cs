using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp;

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
/// This class can deserialize elements such as DataValueMember, StructureMember, and ArrayMember, as well as the root
/// Data element and even the Tag elements. This class makes use of <see cref="LogixData"/> for deserialization.
/// This class only defines, name and data type, since Dimension, Radix, and ExternalAccess are all either
/// members or the specific <see cref="LogixType"/>, or not inherent in the data structure when serialized or
/// deserialized.
/// </para>
/// <para>
/// LogixMember also contains an event called <see cref="DataChanged"/> which triggers when <see cref="DataType"/> is
/// set. It also subscribes to the data change event of <see cref="DataType"/> in order to route events up the
/// type/member hierarchy. This allows the root member to notify of a change so subscribers (Tag) can update the L5X. 
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class LogixMember : ILogixSerializable
{
    private LogixType _dataType;

    /// <summary>
    /// Creates a new <see cref="LogixMember"/> object with the provided name and logix type.
    /// </summary>
    /// <param name="name">The name of the member. If <c>null</c> will default to an empty string.</param>
    /// <param name="type">The <see cref="LogixType"/> representing the member's data. If <c>null</c>
    /// will default to <see cref="NullType"/>.</param>
    public LogixMember(string? name, LogixType? type)
    {
        Name = name ?? string.Empty;
        _dataType = type ?? LogixData.Null;
        _dataType.DataChanged += OnDataTypeChanged;
    }

    /// <summary>
    /// Creates a new <see cref="LogixMember"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="L5XException"><c>element</c> does not have required attributes or elements.</exception>
    public LogixMember(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        Name = element.Attribute(L5XName.Name)?.Value ?? (element.Attribute(L5XName.Index)?.Value ?? string.Empty);
        _dataType = LogixData.Deserialize(element);
        _dataType.DataChanged += OnDataTypeChanged;
    }

    /// <summary>
    /// The name of the <see cref="LogixMember"/>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the member name.</value>
    /// <remarks>
    /// Member name can represent the member name, array element index name, or tag name of an L5X element.
    /// </remarks>
    public string Name { get; }

    /// <summary>
    /// The logix type of the <see cref="LogixMember"/>.
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
        set
        {
            _dataType.DataChanged -= OnDataTypeChanged;
            _dataType = Set(value);
            _dataType.DataChanged += OnDataTypeChanged;
            RaiseDataChanged(this);
        }
    }

    /// <summary>
    /// An event that triggers when the <see cref="DataType"/> property changes or is set.
    /// </summary>
    /// <remarks>
    /// This event is allowing us to detect when the value of a immediate or nested logix type member changes.
    /// This is important for tag so that it can know when to update the underlying XML data structure represented
    /// by the in-memory data structure. 
    /// </remarks>
    public event EventHandler? DataChanged;

    /// <inheritdoc />
    public XElement Serialize()
    {
        return _dataType switch
        {
            AtomicType atomicType => SerializeValueMember(Name, atomicType),
            ArrayType arrayType => SerializeArrayMember(Name, arrayType),
            StringType stringType => SerializeStringMember(Name, stringType),
            StructureType structureType => SerializeStructureMember(Name, structureType),
            _ => throw new NotSupportedException($"Can not serialize member of type {DataType.Name}.")
        };
    }

    /// <summary>
    /// Handles generating a new data type value using the current and provided logix type values.
    /// </summary>
    /// <param name="type">The logix type to update the underlying member data type with.</param>
    /// <returns>A new <see cref="LogixType"/> with the update values.</returns>
    /// <exception cref="NotSupportedException">The provided logix type is not supported</exception>
    /// <remarks>
    /// This method is the basis for how underlying values are set in memory.
    /// Atomic and String type objects will set actual value (but return new type), whereas Structure and Array
    /// types will join members and delegate the set down the type/member hierarchy. 
    /// </remarks>
    private LogixType Set(LogixType? type)
    {
        if (type is null) return LogixData.Null;

        return _dataType switch
        {
            NullType => type,
            AtomicType atomicType => SetAtomic(atomicType, type),
            ArrayType arrayType => SetMembers(arrayType, type),
            StringType stringType => SetMembers(stringType, type),
            StructureType structureType => SetMembers(structureType, type),
            _ => throw new NotSupportedException($"Member does not support setting DataType to type {type.GetType()}.")
        };
    }

    /// <summary>
    /// Handles creating a new atomic type with the updated value. This requires converting the incoming value to the
    /// same type as the current type, as we only want to change the value but not the actual atomic type itself.
    /// </summary>
    private static LogixType SetAtomic(LogixType current, LogixType type)
    {
        return current switch
        {
            BOOL value => new BOOL((bool)Convert.ChangeType(type, typeof(bool)), value.Radix),
            DINT value => new DINT((int)Convert.ChangeType(type, typeof(int)), value.Radix),
            INT value => new INT((short)Convert.ChangeType(type, typeof(short)), value.Radix),
            LINT value => new LINT((long)Convert.ChangeType(type, typeof(long)), value.Radix),
            LREAL value => new LREAL((double)Convert.ChangeType(type, typeof(double)), value.Radix),
            REAL value => new REAL((float)Convert.ChangeType(type, typeof(float)), value.Radix),
            SINT value => new SINT((sbyte)Convert.ChangeType(type, typeof(sbyte)), value.Radix),
            UDINT value => new UDINT((uint)Convert.ChangeType(type, typeof(uint)), value.Radix),
            UINT value => new UINT((ushort)Convert.ChangeType(type, typeof(ushort)), value.Radix),
            ULINT value => new ULINT((ulong)Convert.ChangeType(type, typeof(ulong)), value.Radix),
            USINT value => new USINT((byte)Convert.ChangeType(type, typeof(byte)), value.Radix),
            _ => throw new ArgumentException($"Can not set {current.Name} with type {type.Name}.")
        };
    }

    /// <summary>
    /// Handles cloning and setting members of any given logix type that is considered a structure, array, or string type.
    /// This simply joins members on name and delegates the set of each member down the type/member hierarchy,
    /// eventually arriving at atomic type values.
    /// </summary>
    private static LogixType SetMembers(LogixType current, LogixType type)
    {
        if (type is AtomicType)
            throw new ArgumentException($"Can not set {current.Name} with atomic type {type.Name}.");
        
        var clone = current.Clone();

        var pairs = clone.Members.Join(type.Members, m => m.Name, m => m.Name,
            (t, s) => new { Target = t, Source = s });

        foreach (var pair in pairs)
            pair.Target.DataType = pair.Source.DataType;

        return clone;
    }

    /// <summary>
    /// Handles serializing a <see cref="AtomicType"/> as a ValueMember element.
    /// </summary>
    private static XElement SerializeValueMember(string name, AtomicType type)
    {
        var element = new XElement(L5XName.DataValueMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        if (type is not BOOL) element.Add(new XAttribute(L5XName.Radix, type.Radix));
        element.Add(new XAttribute(L5XName.Value, type.ToString()));
        return element;
    }

    /// <summary>
    /// Handles serializing a <see cref="ArrayType"/> as a ArrayMember element.
    /// </summary>
    private static XElement SerializeArrayMember(string name, ArrayType type)
    {
        var element = new XElement(L5XName.ArrayMember);
        element.Add(new XAttribute(L5XName.Name, name));
        element.Add(new XAttribute(L5XName.DataType, type.TypeName));
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
    private static XElement SerializeStructureMember(string name, LogixType type)
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

    /// <summary>
    /// Handles raising the <see cref="DataChanged"/> event for the member.
    /// </summary>
    private void RaiseDataChanged(object sender) => DataChanged?.Invoke(sender, EventArgs.Empty);

    /// <summary>
    /// Captures nested data type data change event and in turn fires local data changed event.
    /// This routes/bubbles up the event sender to the root of the type/member hierarchy. 
    /// </summary>
    private void OnDataTypeChanged(object sender, EventArgs e)
    {
        //If the sender is an atomic type (which is intercepted by atomic types)
        //then we realize this is a value change and need to replace the member type with the new value.
        //This is the only way we can update the atomic value on the L5X for a bit member change, since
        //atomic bit members don't exist in the L5X data structure.
        //Note that setting DataType this way will in turn trigger the member's data changed event. 
        if (sender is AtomicType atomicType)
        {
            DataType = atomicType;
            return;
        }

        //Otherwise the member change was already handled and we just forward the sender up the chain.
        RaiseDataChanged(sender);
    }
}