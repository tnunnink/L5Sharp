using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Types;

/// <summary>
/// A mutable <see cref="StructureType"/> allowing the user to further extend or transform the structure of a complex
/// logix type.
/// </summary>
public class ComplexType : StructureType
{
    /// <inheritdoc />
    public ComplexType(XElement element) : base(element)
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
    public ComplexType(string name, IEnumerable<Member> members) : base(name, members)
    {
    }

    /// <summary>
    /// Adds the provided member to the end of the type's members collection.
    /// </summary>
    /// <param name="member">The <see cref="Member"/> to add.</param>
    /// <exception cref="ArgumentNullException"><c>member</c> is null.</exception>
    public void Add(Member member) => AddMember(member);

    /// <summary>
    /// Adds the provided member collection to the end of the type's members collection.
    /// </summary>
    /// <param name="members">The collection of <see cref="Member"/> to add.</param>
    /// <exception cref="ArgumentNullException"><c>members</c> or any object in <c>members</c> is null.</exception>
    public void AddRange(ICollection<Member> members) => AddMembers(members);

    /// <summary>
    /// Removes all members from the current type.
    /// </summary>
    public void Clear() => ClearMembers();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="member"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException">index is less than 0 or greater than or equal to the number of elements in source.</exception>
    public void Insert(int index, Member member) => InsertMember(index, member);

    /// <summary>
    /// Removed a member with the specified name from the complex type.
    /// </summary>
    /// <param name="name">The name of the member to remove.</param>
    public void Remove(string name) => RemoveMember(name);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void Remove(int index) => RemoveMember(index);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="member"></param>
    public void Replace(string name, Member member) => ReplaceMember(name, member);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="member"></param>
    public void Replace(int index, Member member) => ReplaceMember(index, member);
}