using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace L5Sharp.Core;

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
    /// <summary>
    /// Creates a new <see cref="StructureType"/> instance.
    /// </summary>
    /// <param name="name">The name of the type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    protected StructureType(string name) : base(CreateStructure(name, Enumerable.Empty<Member>()))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> with the provided name and collection of <see cref="Member"/> objects.
    /// </summary>
    /// <param name="name">The name of the structure type.</param>
    /// <param name="members">The members of the structure type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    protected StructureType(string name, IEnumerable<Member> members) : base(CreateStructure(name, members))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException"><c>element</c> does not have required attributes or child elements.</exception>
    protected StructureType(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Element.Elements().Select(e => new Member(e));

    /// <summary>
    /// Gets the logix type for the specified member. 
    /// </summary>
    /// <param name="name">The member name to find.</param>
    /// <typeparam name="TLogixType">The logix type to return.</typeparam>
    /// <returns>A <see cref="LogixType"/> representing the data of the specified member.</returns>
    /// <exception cref="InvalidOperationException">A member with the specified name was not found in the member collection.</exception>
    /// <remarks>
    /// This method is for users implementing custom user defined or predefined types.
    /// Use this as the getter for all member's logix types.
    /// The user of CallerMemberName allows the member name to be omitted assuming it matches the name of the calling property.
    /// </remarks>
    protected TLogixType GetMember<TLogixType>([CallerMemberName] string? name = null) where TLogixType : LogixType
    {
        var element = Element.Elements().SingleOrDefault(m => m.MemberName() == name);

        if (element is null)
            throw new InvalidOperationException($"No member with name '{name}' exists for type {Name}");

        return element.Deserialize<TLogixType>();
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
        if (value is null or NullType) throw new ArgumentNullException(nameof(value));

        var existing = Element.Elements().SingleOrDefault(m => m.MemberName() == name)?.ToMember();

        if (existing is null)
        {
            var member = new Member(name, value);
            Element.Add(member.Serialize());
            return;
        }

        existing.Value = value;
    }

    /// <summary>
    /// Adds the provided member to the structure type.
    /// </summary>
    /// <param name="member">The member to add.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    protected void AddMember(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member), "Can not add a null member to a structure type object.");

        Element.Add(member.Serialize());
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
                throw new ArgumentNullException(nameof(members),
                    "Can not add a null member to a structure type object.");

            Element.Add(member.Serialize());
        }
    }

    /// <summary>
    /// Clears all members from the structure type.
    /// </summary>
    protected void ClearMembers() => Element.RemoveNodes();

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
            throw new ArgumentNullException(nameof(member), "Can not add a null member to a structure type object.");

        var target = Element.Elements().ElementAt(index);
        target.AddBeforeSelf(member.Serialize());
    }

    /// <summary>
    /// Removes a member with the specified name from the structure type.
    /// </summary>
    /// <param name="name">The name of the member to remove.</param>
    protected void RemoveMember(string name)
    {
        Element.Elements().SingleOrDefault(e => e.MemberName().IsEquivalent(name))?.Remove();
    }

    /// <summary>
    /// Removes a member at the specified index from the structure type.
    /// </summary>
    /// <param name="index">The zero-based index of the member to remove.</param>
    protected void RemoveMember(int index)
    {
        var target = Element.Elements().ElementAt(index);
        target.Remove();
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
            throw new ArgumentNullException(nameof(member), "Can not add a null member to a structure type object.");

        var target = Element.Elements().SingleOrDefault(e => e.MemberName().IsEquivalent(name));

        if (target is null)
            throw new ArgumentException($"No member with name {name} was found in the structure.");

        target.ReplaceWith(member.Serialize());
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
        if (type is null or NullType)
            throw new ArgumentNullException(nameof(type), "Can not replace member with null data.");

        var target = Element.Elements().SingleOrDefault(e => e.MemberName().IsEquivalent(name));

        if (target is null)
            throw new ArgumentException($"No member with name {name} was found in the structure.");

        var member = new Member(name, type);
        target.ReplaceWith(member.Serialize());
    }

    /// <summary>
    /// Replaces a member at the specified index with the provided member instance.
    /// </summary>
    /// <param name="index">The zer-based index at which to replace the member.</param>
    /// <param name="member">The member to replace the current member with.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    protected void ReplaceMember(int index, Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member), "Structure type does not allow null members.");

        var target = Element.Elements().ElementAt(index);

        target.ReplaceWith(member.Serialize());
    }

    /// <summary>
    /// Creates the default <see cref="XElement"/> representing the underlying structure type.
    /// </summary>
    private static XElement CreateStructure(string? name, IEnumerable<Member> members)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Can not create structure type with null or empty name.");

        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(members.Select(m => m.Serialize()));
        return element;
    }
}