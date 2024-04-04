using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A mutable <see cref="StructureData"/> allowing the user to further extend or transform the structure of a complex
/// logix type after instantiation of the object.
/// </summary>
/// <remarks>
/// This type simply exposes methods for modifying the underlying members collection of a structure type.
/// Users can inherit from this type if they wish to mutate user defined types after instantiation. All structure type
/// elements in the L5X will be deserialized as a complex type unless a specific type is found for them (e.g., TIMER).
/// Predefined types will inherit structure type as they should not be mutated.
/// </remarks>
public class ComplexData : StructureData
{
    /// <summary>
    /// Creates a new empty <see cref="ComplexData"/> with the name 'ComplexType' indicating this represents some generic
    /// or non-descriptive type with members configured by the user. 
    /// </summary>
    public ComplexData() : base(nameof(ComplexData))
    {
    }

    /// <summary>
    /// Creates a new <see cref="ComplexData"/> with the provided name.
    /// </summary>
    /// <param name="name">The name of the logix type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    public ComplexData(string name) : base(name)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ComplexData"/> with the provided name and member collection.
    /// </summary>
    /// <param name="name">The name of the logix type.</param>
    /// <param name="members"></param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <exception cref="ArgumentNullException"><c>members</c> is null or any member in <c>members</c> is null.</exception>
    public ComplexData(string name, IEnumerable<Member> members) : base(name, members)
    {
    }
    
    /// <inheritdoc />
    public ComplexData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Adds the provided member to the end of the type member collection.
    /// </summary>
    /// <param name="member">The <see cref="Member"/> to add.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    public void Add(Member member) => AddMember(member);

    /// <summary>
    /// Adds the provided member collection to the end of the type member collection.
    /// </summary>
    /// <param name="members">The collection of <see cref="Member"/> to add.</param>
    /// <exception cref="ArgumentNullException"><c>members</c> or any object in <c>members</c> is null.</exception>
    public void AddRange(ICollection<Member> members) => AddMembers(members);

    /// <summary>
    /// Removes all members from the current type.
    /// </summary>
    public void Clear() => ClearMembers();

    /// <summary>
    /// Inserts the provided member at the specified index of the type member collection. 
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="member">The member to insert.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">index is less than 0. -or- index is greater than the length of
    /// the member collection.</exception>
    public void Insert(int index, Member member) => InsertMember(index, member);

    /// <summary>
    /// Removes a member with the specified name from the type member collection.
    /// </summary>
    /// <param name="name">The name of the member to remove.</param>
    public void Remove(string name) => RemoveMember(name);

    /// <summary>
    /// Removes a member at the specified index from the type member collection.
    /// </summary>
    /// <param name="index">The zero-based index of the member to remove.</param>
    public void Remove(int index) => RemoveMember(index);

    /// <summary>
    /// Replaces a member having the specified name with the provided member instance.
    /// </summary>
    /// <param name="name">The name of the member to replace.</param>
    /// <param name="member">The member to replace the current member with.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>name</c> does not exists in the structure type.</exception>
    public void Replace(string name, Member member) => ReplaceMember(name, member);
    
    /// <summary>
    /// Replaces a member having the specified name with a new member instance of the same name and
    /// provided <see cref="LogixData"/>.
    /// </summary>
    /// <param name="name">The name of the member to replace.</param>
    /// <param name="data">The <see cref="LogixData"/> data to update the specified member with.</param>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>name</c> does not exists in the structure type.</exception>
    public void Replace(string name, LogixData data) => ReplaceMember(name, data);

    /// <summary>
    /// Replaces a member at the specified index with the provided member instance.
    /// </summary>
    /// <param name="index">The zer-based index at which to replace the member.</param>
    /// <param name="member">The member to replace the current member with.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    public void Replace(int index, Member member) => ReplaceMember(index, member);
}