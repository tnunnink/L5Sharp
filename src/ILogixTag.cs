using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp
{
    /// <summary>
    /// A root interface for logix tag components and objects. Defines the members and methods for interacting with
    /// tag data structures.
    /// </summary>
    /// <see cref="Components.Tag"/>
    /// <see cref="TagMember"/>
    public interface ILogixTag
    {
        /// <summary>
        /// The full tag name path of the <see cref="TagMember"/>.
        /// </summary>
        /// <value>A <see cref="Core.TagName"/> type representing the tag name of the member.</value>
        TagName TagName { get; }

        /// <summary>
        /// The data value or structure of the tag member.
        /// </summary>
        /// <value>A <see cref="ILogixType"/> containing the data value or structure of the tag component.</value>
        /// <remarks>
        /// ...
        /// </remarks>
        ILogixType Data { get; }
        
        /// <summary>
        /// The name of the data type that represents the tag's type. 
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the tag data type.</value>
        /// <remarks>This simply points to the name property of <see cref="Data"/>. This keeps the values in sync.</remarks>
        public string DataType { get; }

        /// <summary>
        /// The dimensions of the tag, indicating the length and dimensions of it's array.
        /// </summary>
        /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions of the tag.</value>
        /// <remarks>
        /// This value will always point to the dimensions property of <see cref="Data"/>, assuming it is an
        /// <see cref="ArrayType{TLogixType}"/>.
        /// If <c>Data</c> is not an array type, this property will always return <see cref="Core.Dimensions.Empty"/>.
        /// </remarks>
        public Dimensions Dimensions { get; }

        /// <summary>
        /// The radix format of the tags data value. Only applies if the tag is an <see cref="AtomicType"/>.
        /// </summary>
        /// <value>A <see cref="Enums.Radix"/> option representing data format of the tag value.</value>
        /// <remarks>
        /// This value will always point to the radix property of <see cref="Data"/>, assuming it is an
        /// <see cref="AtomicType"/>.
        /// If <c>Data</c> is not an atomic type, this property will always return <see cref="Enums.Radix.Null"/>.
        /// </remarks>
        Radix Radix { get; }

        /// <summary>
        /// The type of member the tag represents (value, structure, array, string).
        /// </summary>
        /// <value>A <see cref="Enums.MemberType"/> enum representing the type of member.</value>
        MemberType MemberType { get; }
        
        /// <summary>
        /// Gets a descendent tag member relative to the current tag member.
        /// </summary>
        /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.</param>
        /// <returns>A <see cref="TagMember"/> representing the child member instance.</returns>
        TagMember? Member(TagName tagName);
        
        /// <summary>
        /// Gets all descendent tag members relative to the current tag member.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
        IEnumerable<TagMember> Members();
        
        /// <summary>
        /// Gets all descendent tag members that satisfy the specified tag name predicate expression.
        /// </summary>
        /// <param name="predicate">A predicate expression specifying the tag name filter.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects that satisfy the predicate.</returns>
        IEnumerable<TagMember> Members(Predicate<TagName> predicate);
        
        /// <summary>
        /// Gets all descendent tag members that satisfy the specified tag member predicate expression.
        /// </summary>
        /// <param name="predicate">A predicate expression specifying the tag member filter.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects that satisfy the predicate.</returns>
        IEnumerable<TagMember> Members(Predicate<TagMember> predicate);
        
        
        /// <summary>
        /// Gets all descendent tag members of the tag member specified by the provided tag name path.
        /// </summary>
        /// <param name="tagName">A tag name path to the tag member for which to get members of.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
        IEnumerable<TagMember> MembersOf(TagName tagName);

        /// <summary>
        /// Sets the tag member <c>Data</c> property to the provided <see cref="AtomicType"/> if the
        /// tag's data property is already an atomic type. Otherwise, does not set the value.
        /// </summary>
        /// <param name="atomicType">A <see cref="AtomicType"/> value representing the value to set.</param>
        /// <returns><c>true</c> if the value was set; otherwise, <c>false</c>.</returns>
        /// <remarks>This is a helper to avoid having to check or cast <c>Data</c> before setting it's value.</remarks>
        bool SetValue(AtomicType atomicType);
    }
}