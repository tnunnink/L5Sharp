using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <see cref="LogixData"/> that represents a structure containing members of different types.
/// </summary>
/// <remarks>
/// <para>
/// This type is a building block for all <c>Predefined</c> data types. Inherit from this class to create custom
/// user-defined data types that can be used to create in memory representation of the tags for those types.
/// Inherit from <see cref="StructureData"/> if you want the ability to mutate the member structure after instantiation.
/// </para>
/// </remarks>
public class StructureData : LogixData, IDictionary<string, LogixData>
{
    /// <summary>
    /// Creates a new default <see cref="StructureData"/> instance.
    /// </summary>
    public StructureData() : base(CreateStructure(nameof(StructureData), []))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StructureData"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException"><c>element</c> does not have required attributes or child elements.</exception>
    public StructureData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="StructureData"/> instance.
    /// </summary>
    /// <param name="name">The name of the type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    public StructureData(string name) : base(CreateStructure(name, []))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StructureData"/> with the provided name and collection of <see cref="LogixMember"/> objects.
    /// </summary>
    /// <param name="name">The name of the structure type.</param>
    /// <param name="members">The members of the structure type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    public StructureData(string name, IEnumerable<LogixMember> members) : base(CreateStructure(name, members))
    {
    }

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => Element.Elements().Select(e => new LogixMember(e));

    /// <inheritdoc />
    public int Count => Element.Elements().Count();

    /// <inheritdoc />
    bool ICollection<KeyValuePair<string, LogixData>>.IsReadOnly => false;

    /// <inheritdoc />
    public ICollection<string> Keys
    {
        get { return Element.Elements().Select(e => e.MemberName()).ToArray(); }
    }

    /// <inheritdoc />
    public ICollection<LogixData> Values
    {
        get { return Element.Elements().Select(e => e.Deserialize<LogixData>()).ToArray(); }
    }

    /// <inheritdoc />
    public override void Update(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not StructureData structure)
            throw new ArgumentException($"Can not update structure with data of type '{data.GetType()}'");

        var matches = Members.Join(structure.Members,
            m => m.Name,
            m => m.Name,
            (x, y) => new { Target = x.Value, Source = y.Value },
            StringComparer.OrdinalIgnoreCase
        );

        foreach (var match in matches)
        {
            match.Target.Update(match.Source);
        }
    }

    /// <inheritdoc />
    public LogixData this[string name]
    {
        get => GetMember<LogixData>();
        set => SetMember(value, name);
    }

    /// <summary>
    /// Adds the specified <see cref="LogixMember"/> to the current <see cref="StructureData"/> type.
    /// </summary>
    /// <param name="member">The <see cref="LogixMember"/> to add to the structure.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="member"/> is null.</exception>
    public void Add(LogixMember member)
    {
        AddMember(member);
    }

    /// <summary>
    /// Adds a new member with the provided name and <see cref="LogixData"/> value to the <see cref="StructureData"/> type.
    /// </summary>
    /// <param name="name">The name of the member to be added.</param>
    /// <param name="value">The <see cref="LogixData"/> value of the member to be added.</param>
    public void Add(string name, LogixData value)
    {
        AddMember(new LogixMember(name, value));
    }

    /// <summary>
    /// Adds a new member with the provided key value pair to the <see cref="StructureData"/> type. 
    /// </summary>
    /// <param name="item">The key value pair that containers the name and value of the member to add.</param>
    public void Add(KeyValuePair<string, LogixData> item)
    {
        AddMember(new LogixMember(item.Key, item.Value));
    }

    /// <summary>
    /// Adds the provided collection of members to the structure type.
    /// </summary>
    /// <param name="members">The collection of members to add.</param>
    /// <exception cref="ArgumentNullException"><c>members</c> is null or any member in <c>members</c> is null.</exception>
    public void AddMany(ICollection<LogixMember> members)
    {
        if (members is null) throw new ArgumentNullException(nameof(members));

        foreach (var member in members)
        {
            AddMember(member);
        }
    }

    /// <summary>
    /// Clears all members from the structure type.
    /// </summary>
    public void Clear() => Element.RemoveNodes();

    /// <summary>
    /// Determines whether the <see cref="StructureData"/> contains member instance using the provide key value pair.
    /// </summary>
    /// <param name="item">The key-value pair to locate in the <see cref="StructureData"/>.</param>
    /// <returns><c>true</c> if the specified key-value pair exists; otherwise, <c>false</c>.</returns>
    /// <remarks>This method only considers the key of the provided key value pair.</remarks>
    public bool Contains(KeyValuePair<string, LogixData> item)
    {
        return Element.Elements().Any(e => e.MemberName() == item.Key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool ContainsKey(string name)
    {
        return Element.Elements().Any(e => e.MemberName() == name);
    }

    /// <summary>
    /// Copies the elements of the <see cref="StructureData"/> to the specified array, starting at the specified array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. It must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified arrayIndex is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when the destination array is not long enough to contain all elements of the collection.</exception>
    public void CopyTo(KeyValuePair<string, LogixData>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        if (array.Length - arrayIndex < Count)
            throw new ArgumentException("Destination array is not long enough to copy all elements.");

        foreach (var member in Members)
        {
            array[arrayIndex++] = new KeyValuePair<string, LogixData>(member.Name, member.Value);
        }
    }

    /// <summary>
    /// Attempts to get the value associated with the specified member name in the <see cref="StructureData"/>.
    /// </summary>
    /// <param name="name">The name of the member to attempt to retrieve.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified member name
    /// if the member is found; otherwise, the default value for the type of the <see cref="LogixData"/> object.</param>
    /// <returns><c>true</c> if the member was found; otherwise, <c>false</c>.</returns>
    public bool TryGetValue(string name, out LogixData value)
    {
        var element = Element.Elements().SingleOrDefault(e => e.MemberName().IsEquivalent(name));

        if (element is null)
        {
            value = null!;
            return false;
        }

        value = element.Deserialize<LogixData>();
        return true;
    }

    /// <summary>
    /// Removes a member with the specified name from the structure data.
    /// </summary>
    /// <param name="name">The name of the element to remove.</param>
    /// <returns>true if the element was successfully removed; otherwise, false.</returns>
    public bool Remove(string name)
    {
        var element = Element.Elements().SingleOrDefault(e => e.MemberName() == name);

        if (element is null)
            return false;

        element.Remove();
        return true;
    }

    /// <summary>
    /// Removes the member with the specified key-value pair from the structure.
    /// </summary>
    /// <param name="item">The key-value pair containing the key to remove.</param>
    /// <returns>True if the member was found and removed; otherwise, false.</returns>
    /// <remarks>Only the key is used for matching - the value is ignored.</remarks>
    public bool Remove(KeyValuePair<string, LogixData> item)
    {
        var element = Element.Elements().SingleOrDefault(e => e.MemberName() == item.Key);

        if (element == null)
            return false;

        element.Remove();
        return true;
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, LogixData>> GetEnumerator()
    {
        return Members.Select(m => new KeyValuePair<string, LogixData>(m.Name, m.Value)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Gets the logix data value for the specified member. 
    /// </summary>
    /// <param name="name">The member name to find.</param>
    /// <typeparam name="TData">The logix type to return.</typeparam>
    /// <returns>A <see cref="LogixData"/> representing the data of the specified member.</returns>
    /// <exception cref="InvalidOperationException">A member with the specified name was not found in the member collection.</exception>
    /// <remarks>
    /// This method is for users implementing custom user-defined types.
    /// Use this as the getter for all member's logix types.
    /// The user of CallerMemberName allows the member name to be omitted assuming it matches the name of the calling property.
    /// </remarks>
    protected TData GetMember<TData>([CallerMemberName] string? name = null) where TData : LogixData
    {
        var element = Element.Elements().SingleOrDefault(m => m.MemberName().IsEquivalent(name));

        if (element is null)
            throw new InvalidOperationException($"No member with name '{name}' exists for type {Name}");

        return element.Deserialize<LogixData>().As<TData>();
    }

    /// <summary>
    /// Retrieves an array of elements of type <typeparamref name="TData"/> from the structure data element.
    /// </summary>
    /// <typeparam name="TData">The specific type of elements to retrieve.</typeparam>
    /// <param name="name">The name of the array member associated with the data. Defaults to the caller's member name.</param>
    /// <returns>An array of elements of type <typeparamref name="TData"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no member with the specified <paramref name="name"/> exists or when the member is not an array.</exception>
    protected TData[] GetArray<TData>([CallerMemberName] string? name = null) where TData : LogixData
    {
        var element = Element.Elements().SingleOrDefault(m => m.MemberName().IsEquivalent(name));

        if (element is null)
            throw new InvalidOperationException($"No member with name '{name}' exists for type {Name}");

        if (element.Name.LocalName is not L5XName.ArrayMember)
            throw new InvalidOperationException($"Member '{name}' is not an array member element.");

        return element.Deserialize<LogixData>().As<ArrayData>().Cast<TData>().ToArray();
    }

    /// <summary>
    /// Adds or updates the value for the specified member of the <see cref="StructureData"/> type.  
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <param name="name">The name of the structure member to set. Defaults to the caller member name.</param>
    /// <typeparam name="TData">The <see cref="LogixData"/> parameter of the member.</typeparam>
    /// <exception cref="ArgumentNullException"><c>name</c> or <c>value</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method meant for classes deriving from <see cref="StructureData"/>. This sett allows deriving classes
    /// to implement member properties and get/set member values. 
    /// </para>
    /// <para>
    /// This method also handles member initialization. If the member already exists, the data value will be set
    /// to the provided value, otherwise a new member will be added to the underling XML structure.
    /// This allows the user to initialize member properties in a default constructor in the derived class.
    /// Note that the order in which members exist in the underlying collection matters when importing logix type tag data.
    /// </para>
    /// </remarks>
    protected void SetMember<TData>(TData value, [CallerMemberName] string? name = null)
        where TData : LogixData
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (value is null or NullData) throw new ArgumentNullException(nameof(value));

        var existing = Element.Elements().SingleOrDefault(m => m.MemberName().IsEquivalent(name));

        if (existing is null)
        {
            var member = new LogixMember(name, value);
            Element.Add(member.Serialize());
            return;
        }

        existing.Deserialize<LogixData>().Update(value);
    }

    /// <summary>
    /// Adds or updates the array for the specified member of the <see cref="StructureData"/> type.
    /// </summary>
    /// <typeparam name="TData">The data type of the elements in the array, derived from <see cref="LogixData"/>.</typeparam>
    /// <param name="array">The array of elements to assign to the structure member.</param>
    /// <param name="name">The name of the structure member to set. Defaults to the caller member name.</param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null or contains null data, or <c>name</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method meant for classes deriving from <see cref="StructureData"/>. This sett allows deriving classes
    /// to implement member properties and get/set member values. 
    /// </para>
    /// <para>
    /// This method also handles member initialization. If the member already exists, the data value will be set
    /// to the provided value, otherwise a new member will be added to the underling XML structure.
    /// This allows the user to initialize member properties in a default constructor in the derived class.
    /// Note that the order in which members exist in the underlying collection matters when importing logix type tag data.
    /// </para>
    /// </remarks>
    protected void SetArray<TData>(TData[] array, [CallerMemberName] string? name = null) where TData : LogixData, new()
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (array is null) throw new ArgumentNullException(nameof(array));

        var data = array.ToArrayData();
        var existing = Element.Elements().SingleOrDefault(m => m.MemberName().IsEquivalent(name));

        if (existing is null)
        {
            var member = new LogixMember(name, data);
            Element.Add(member.Serialize());
            return;
        }

        existing.Deserialize<LogixData>().Update(data);
    }

    /// <summary>
    /// Converts an <see cref="XElement"/> into a structured representation by transforming its attributes
    /// into child elements representing data members.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to transform into a structured format.</param>
    /// <returns>An <see cref="XElement"/> with attributes converted into data member elements.</returns>
    /// <remarks>
    /// This is used for special formatted data where the members are attributes of the element.
    /// In these cases, if we transform the element structure, we don't have to have custom code to handle
    /// reading/writing values in the logix member. When we go to serialize the data, we can reverse the
    /// transformation so that it will import successfully.
    /// </remarks>
    protected static XElement ToStructure<TStructure>(XElement element) where TStructure : StructureData
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        var members = new List<XElement>();

        //We will rely on the property declarations of the structure type to determine the correct data type for the members.
        var propertyLookup = typeof(TStructure).GetProperties()
            .Where(p => typeof(LogixData).IsAssignableFrom(p.PropertyType))
            .ToDictionary(p => p.Name, p => p.PropertyType);

        foreach (var attribute in element.Attributes())
        {
            var name = attribute.Name.LocalName;

            //Use the type registry to get the name. These should all be atomic types.
            var dataType = LogixType.NameFor(propertyLookup[name]);

            //Try to infer the radix format. If not a valid format, assume decimal (for the case of boolean)
            var radix = Radix.TryInfer(attribute.Value, out var format) ? format : Radix.Decimal;

            //Handle boolean keyword values and convert to 1/0
            var value = attribute.Value switch
            {
                "true" => "1",
                "false" => "0",
                _ => attribute.Value
            };

            var member = new XElement(L5XName.DataValueMember);
            member.SetAttributeValue(L5XName.Name, name);
            member.SetAttributeValue(L5XName.DataType, dataType);
            member.SetAttributeValue(L5XName.Radix, radix);
            member.SetAttributeValue(L5XName.Value, value);
            members.Add(member);
        }

        element.ReplaceAll(members);
        return element;
    }

    /// <summary>
    /// Adds a specified member to the structure.
    /// </summary>
    /// <param name="member">The <see cref="LogixMember"/> instance to add to the structure.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="member"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a member with the same name already exists in the structure.
    /// </exception>
    private void AddMember(LogixMember member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member), "Can not add a null member to a structure type.");

        if (Element.Elements().Any(e => e.MemberName().IsEquivalent(member.Name)))
            throw new InvalidOperationException($"Member already exists in structure type: '{member.Name}'");

        Element.Add(member.Serialize());
    }

    /// <summary>
    /// Creates the default <see cref="XElement"/> representing the underlying structure type.
    /// </summary>
    private static XElement CreateStructure(string? name, IEnumerable<LogixMember> members)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Can not create structure type with null or empty name.");

        if (members is null)
            throw new ArgumentNullException(nameof(members));

        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(members.Select(m => m.Serialize()));
        return element;
    }
}