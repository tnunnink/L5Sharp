using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="LogixType"/> that represents a structure containing members of different types.
/// </summary>
/// <remarks>
/// <para>
/// This type is a building block for all <c>Predefined</c> data types. Inherit from this class to create custom
/// user defined data types that can be used to create in memory representation of the tags for those types.
/// Inherit from <see cref="ComplexType"/> if you want the ability to mutate the member structure after instantiation.
/// </para>
/// </remarks>
public abstract class StructureType : LogixType
{
    private readonly List<Member> _members;

    /// <summary>
    /// Creates a new <see cref="StructureType"/> instance.
    /// </summary>
    /// <param name="name">The name of the type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    protected StructureType(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty for a structure type object.");

        Name = name;
        _members = new List<Member>();
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> with the provided name and collection of <see cref="Member"/> objects.
    /// </summary>
    /// <param name="name">The name of the structure type.</param>
    /// <param name="members">The members of the structure type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <exception cref="ArgumentNullException"><c>members</c> is null.</exception>
    protected StructureType(string name, IEnumerable<Member> members) : this(name)
    {
        AddMembers(members.ToList());
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="L5XException"><c>element</c> does not have required attributes.</exception>
    protected StructureType(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        Name = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);
        _members = element.Elements().Select(e =>
        {
            var member = new Member(e);
            member.DataType.DataChanged += OnMemberDataChanged;
            return member;
        }).ToList();
    }

    /// <inheritdoc />
    public override string Name { get; }

    /// <inheritdoc />
    public override DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Unknown;

    /// <inheritdoc />
    public override IEnumerable<Member> Members => _members.AsEnumerable();

    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, Name));
        element.Add(_members.Select(m => m.Serialize()));
        return element;
    }

    /// <inheritdoc />
    public override LogixType Set(LogixType type)
    {
        if (type is not StructureType structure)
            throw new ArgumentException($"Can not update {GetType().Name} with {type.GetType().Name}");

        var pairs = _members.Join(structure.Members, m => m.Name, m => m.Name,
            (t, s) => new { Target = t, Source = s });

        foreach (var pair in pairs)
            pair.Target.DataType = pair.Source.DataType;

        RaiseDataChanged();
        return this;
    }

    /// <summary>
    /// Gets the logix type for the specified member. 
    /// </summary>
    /// <param name="name">The member name to find.</param>
    /// <typeparam name="TLogixType">The logix type to return.</typeparam>
    /// <returns>A <see cref="L5Sharp.LogixType"/> representing the data of the specified member.</returns>
    /// <exception cref="InvalidOperationException">A member with the specified name was not found in the member collection.</exception>
    /// <remarks>
    /// This method is for users implementing custom user defined or predefined types.
    /// Use this as the getter for all member's logix types.
    /// The user of CallerMemberName allows the member name to be omitted assuming it matches the name of the calling property.
    /// </remarks>
    protected TLogixType GetMember<TLogixType>([CallerMemberName] string? name = null) where TLogixType : LogixType
    {
        var type = Members.SingleOrDefault(m => m.Name == name)?.DataType;

        if (type is null)
            throw new InvalidOperationException($"No member with name {name} exists for the structure type {Name}");

        return (TLogixType)type;
    }

    /// <summary>
    /// Adds or updates the specified member's logix type with the provided value. 
    /// </summary>
    /// <param name="value">The logix type value to set.</param>
    /// <param name="name">The name of the member to set.</param>
    /// <typeparam name="TLogixType">The logix type parameter of the member.</typeparam>
    /// <exception cref="ArgumentNullException"><c>name</c> or <c>value</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This method is for users implementing custom user defined or predefined types.
    /// Use this as the setter for all members of the type.
    /// </para>
    /// <para>
    /// Internally this will update the underlying member collection.
    /// If the member already exists, the data type value will be set to the provided value,
    /// otherwise a new member will be added at the end of the member collection.
    /// This allows the user to initialize member properties in a default constructor in the derived class.
    /// Note that the order in which members exist in the underlying collection matters when importing logix type tag data.
    /// </para>
    /// </remarks>
    protected void SetMember<TLogixType>(TLogixType value, [CallerMemberName] string? name = null)
        where TLogixType : LogixType
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (value is null) throw new ArgumentNullException(nameof(value));

        var current = _members.SingleOrDefault(e => e.Name == name);

        if (current is null)
        {
            var member = new Member(name, value);
            member.DataType.DataChanged += OnMemberDataChanged;
            _members.Add(member);
            RaiseDataChanged();
            return;
        }

        current.DataType = value;
    }

    /// <summary>
    /// Adds the provided member to the structure type.
    /// </summary>
    /// <param name="member">The member to add.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    protected void AddMember(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member), "Structure type does not allow null members.");
        member.DataType.DataChanged += OnMemberDataChanged;
        _members.Add(member);
        RaiseDataChanged();
    }

    /// <summary>
    /// Adds the provided collection of members to the structure type.
    /// </summary>
    /// <param name="members">The collection of members to add.</param>
    /// <exception cref="ArgumentNullException"><c>members</c> is null or any member in <c>members</c> is null.</exception>
    protected void AddMembers(ICollection<Member> members)
    {
        if (members is null) throw new ArgumentNullException(nameof(members));

        foreach (var member in members)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(members), "Structure type does not allow null members.");
            member.DataType.DataChanged += OnMemberDataChanged;
            _members.Add(member);
        }

        RaiseDataChanged();
    }

    /// <summary>
    /// Clears all members from the structure type.
    /// </summary>
    protected void ClearMembers()
    {
        foreach (var member in _members) member.DataType.DataChanged -= OnMemberDataChanged;
        _members.Clear();
        RaiseDataChanged();
    }

    /// <summary>
    /// Inserts the provided member at the specified index of the structure type. 
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="member">The member to insert.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">index is less than 0. -or- index is greater than the length of
    /// the member collection.</exception>
    protected void InsertMember(int index, Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member), "Structure type does not allow null members.");

        member.DataType.DataChanged += OnMemberDataChanged;
        _members.Insert(index, member);
        RaiseDataChanged();
    }

    /// <summary>
    /// Removes a member with the specified name from the structure type.
    /// </summary>
    /// <param name="name">The name of the member to remove.</param>
    protected void RemoveMember(string name)
    {
        var index = _members.FindIndex(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
        if (index == -1) return;
        _members[index].DataType.DataChanged -= OnMemberDataChanged;
        _members.RemoveAt(index);
        RaiseDataChanged();
    }

    /// <summary>
    /// Removes a member at the specified index from the structure type.
    /// </summary>
    /// <param name="index">The zero-based index of the member to remove.</param>
    protected void RemoveMember(int index)
    {
        _members[index].DataType.DataChanged -= OnMemberDataChanged;
        _members.RemoveAt(index);
        RaiseDataChanged();
    }

    /// <summary>
    /// Replaces a member having the specified name with the provided member instance.
    /// </summary>
    /// <param name="name">The name of the member to replace.</param>
    /// <param name="member">The member to replace the current member with.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>name</c> does not exists in the structure type.</exception>
    protected void ReplaceMember(string name, Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member), "Structure type does not allow null members.");

        var index = _members.FindIndex(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

        if (index == -1)
            throw new ArgumentException($"No member with name {name} was found in the structure.");

        _members[index].DataType.DataChanged -= OnMemberDataChanged;
        member.DataType.DataChanged += OnMemberDataChanged;
        _members[index] = member;
        RaiseDataChanged();
    }

    /// <summary>
    /// Replaces a member having the specified name with a new member instance of the same name and provided <see cref="LogixType"/>.
    /// </summary>
    /// <param name="name">The name of the member to replace.</param>
    /// <param name="type">The <see cref="LogixType"/> data to update the specified member with.</param>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>name</c> does not exists in the structure type.</exception>
    protected void ReplaceMember(string name, LogixType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        var index = _members.FindIndex(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

        if (index == -1)
            throw new ArgumentException($"No member with name {name} was found in the structure.");

        _members[index].DataType.DataChanged -= OnMemberDataChanged;
        var member = new Member(name, type);
        member.DataType.DataChanged += OnMemberDataChanged;
        _members[index] = member;
        RaiseDataChanged();
    }

    /// <summary>
    /// Replaces a member at the specified index with the provided member instance.
    /// </summary>
    /// <param name="index">The zer-based index at which to replace the member.</param>
    /// <param name="member">The member to replace the current member with.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    protected void ReplaceMember(int index, Member member)
    {
        if (member is null) throw new ArgumentNullException(nameof(member), "Structure type does not allow null members.");
        _members[index].DataType.DataChanged -= OnMemberDataChanged;
        member.DataType.DataChanged += OnMemberDataChanged;
        _members[index] = member;
        RaiseDataChanged();
    }

    /// <summary>
    /// This method needs to be attached to each member of the type to enable the bubbling up of nested member data changed events.
    /// </summary>
    private void OnMemberDataChanged(object sender, EventArgs e) => RaiseDataChanged();
}