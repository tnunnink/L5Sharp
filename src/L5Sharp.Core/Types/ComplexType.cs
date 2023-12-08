using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A mutable <see cref="StructureType"/> allowing the user to further extend or transform the structure of a complex
/// logix type after instantiation of the object.
/// </summary>
/// <remarks>
/// This type simply exposes methods for modifying the underlying members collection of a structure type.
/// Users can inherit from this type if they wish to mutate user defined types after instantiation. All structure type
/// elements in the L5X will be deserialized as a complex type unless a specific type is found for them (e.g., TIMER).
/// Predefined types will inherit structure type as they should not be mutated.
/// </remarks>
public class ComplexType : StructureType
{
    /// <inheritdoc />
    public ComplexType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new empty <see cref="ComplexType"/> with the name 'ComplexType' indicating this represents some generic
    /// or non-descriptive type with members configured by the user. 
    /// </summary>
    public ComplexType() : base(nameof(ComplexType))
    {
    }

    /// <summary>
    /// Creates a new <see cref="ComplexType"/> with the provided name.
    /// </summary>
    /// <param name="name">The name of the logix type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    public ComplexType(string name) : base(name)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ComplexType"/> with the provided name and member collection.
    /// </summary>
    /// <param name="name">The name of the logix type.</param>
    /// <param name="members"></param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <exception cref="ArgumentNullException"><c>members</c> is null or any member in <c>members</c> is null.</exception>
    public ComplexType(string name, IEnumerable<LogixMember> members) : base(name, members)
    {
    }

    /// <summary>
    /// Adds the provided member to the end of the type member collection.
    /// </summary>
    /// <param name="member">The <see cref="LogixMember"/> to add.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    public void Add(LogixMember member) => AddMember(member);

    /// <summary>
    /// Adds the provided member collection to the end of the type member collection.
    /// </summary>
    /// <param name="members">The collection of <see cref="LogixMember"/> to add.</param>
    /// <exception cref="ArgumentNullException"><c>members</c> or any object in <c>members</c> is null.</exception>
    public void AddRange(ICollection<LogixMember> members) => AddMembers(members);

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
    public void Insert(int index, LogixMember member) => InsertMember(index, member);

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
    public void Replace(string name, LogixMember member) => ReplaceMember(name, member);
    
    /// <summary>
    /// Replaces a member having the specified name with a new member instance of the same name and
    /// provided <see cref="LogixType"/>.
    /// </summary>
    /// <param name="name">The name of the member to replace.</param>
    /// <param name="type">The <see cref="LogixType"/> data to update the specified member with.</param>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>name</c> does not exists in the structure type.</exception>
    public void Replace(string name, LogixType type) => ReplaceMember(name, type);

    /// <summary>
    /// Replaces a member at the specified index with the provided member instance.
    /// </summary>
    /// <param name="index">The zer-based index at which to replace the member.</param>
    /// <param name="member">The member to replace the current member with.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    public void Replace(int index, LogixMember member) => ReplaceMember(index, member);
}