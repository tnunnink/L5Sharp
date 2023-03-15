using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;


namespace L5Sharp
{
    /// <summary>
    /// A common interface for logix tag and tag member objects. Defines the members and methods for interacting with
    /// hierarchical tag data structures.
    /// </summary>
    /// <remarks>
    /// <see cref="ILogixTag"/> defines a set of common properties and methods for interacting with hierarchical tag data
    /// structure. The strongly typed property <see cref="Data"/> contains the actual structure, array, or value type data
    /// of the current tag. Other properties, such as <see cref="DataType"/>, <see cref="Radix"/>, and <see cref="Dimensions"/>
    /// are simple pointers to the <c>Data</c> property as they are all dependent on the type implementing <see cref="ILogixType"/>.
    /// This interface also includes methods for getting or finding nested complex tag members of the current tag or
    /// tag member.
    /// </remarks>
    public interface ILogixTag : ILogixScoped
    {
        /// <summary>
        /// The full tag name path of the tag or tag member.
        /// </summary>
        /// <value>A <see cref="Core.TagName"/> type representing the tag name of the member.</value>
        TagName TagName { get; }

        /// <summary>
        /// The description of the tag which is either the root tag description or the inherited (pass through)
        /// description of the descendent member.
        /// </summary>
        /// <value>A <see cref="string"/> containing the text description for the tag.</value>
        string Description { get; }

        /// <summary>
        /// The name of the data type that the tag data contains. 
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the tag data type.</value>
        /// <remarks>
        /// This property simply points to the name property of <see cref="Data"/>.
        /// This keeps the values in sync.</remarks>
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
        /// The data value or structure of the <c>TagMember</c>.
        /// </summary>
        /// <value>A <see cref="ILogixType"/> containing the data value or structure of the <c>Tag</c> component.</value>
        /// <remarks>
        /// <para>
        /// This represents the value or structure of a Tag.
        /// For instance, if the data type is a simple <see cref="AtomicType"/> such as <c>BOOL</c>,
        /// then <c>Data</c> will represent a single value. If the data type is a complex <see cref="StructureType"/>
        /// such as <c>TIMER</c>, then <c>Data</c> contains the entire data hierarchy of the tag.
        /// </para>
        /// <para>
        /// To access members of a complex structure type tag, use the built-in methods <see cref="Member"/>,
        /// or <see cref="Members()"/>.
        /// </para>
        ///<para>
        /// When deserializing, a tag's type is not known until runtime. To get specific access tag's type,
        /// you can cast or convert it using extensions <c>AsType()</c> or <c>ToType()</c>.
        /// </para>
        /// </remarks>
        ILogixType Data { get; }

        /// <summary>
        /// The value of the <c>TagMember</c> data.
        /// </summary>
        /// <value>An <see cref="AtomicType"/> representing the value of the tag member.</value>
        /// <remarks>
        /// This property will just return the value of <see cref="Data"/> as an <see cref="AtomicType"/>.
        /// If the tag member's type is not an atomic type, then this property will be null.
        /// Setting the value should only work for atomic type tags.
        /// </remarks>
        AtomicType? Value { get; set; }

        /// <summary>
        /// The root tag of the current <see cref="ILogixTag"/> member.
        /// </summary>
        ILogixTag Root { get; }
        
        /// <summary>
        /// The parent tag or tag member of the current <see cref="ILogixTag"/> member.
        /// </summary>
        ILogixTag Parent { get; }

        /// <summary>
        /// Gets a descendent tag member relative to the current tag member.
        /// </summary>
        /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.</param>
        /// <returns>A <see cref="ILogixTag"/> representing the child member instance.</returns>
        /// <remarks>
        /// Note that <c>tagName</c> can be a path to a member more than one layer down the hierarchical structure
        /// of the tag or tag member. However, it must start with a member of the current tag or tag member, and not the
        /// actual name of the current tag or tag member.
        /// </remarks>
        /// <example>
        /// <c>var member = tag.Member("ArrayMember[1].SubProperty");</c>
        /// </example>
        ILogixTag? Member(TagName tagName);
        
        /// <summary>
        /// Gets all descendent tag members relative to the current tag member.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="ILogixTag"/> objects.</returns>
        IEnumerable<ILogixTag> Members();
        
        /// <summary>
        /// Gets all descendent tag members relative to the current tag member.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="ILogixTag"/> objects.</returns>
        IEnumerable<ILogixTag> MembersAndSelf();
        
        /// <summary>
        /// Gets all descendent tag members that satisfy the specified tag name predicate expression.
        /// </summary>
        /// <param name="predicate">A predicate expression specifying the tag name filter.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="ILogixTag"/> objects that satisfy the predicate.</returns>
        IEnumerable<ILogixTag> Members(Predicate<TagName> predicate);
        
        /// <summary>
        /// Gets all descendent tag members that satisfy the specified tag member predicate expression.
        /// </summary>
        /// <param name="predicate">A predicate expression specifying the tag member filter.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="ILogixTag"/> objects that satisfy the predicate.</returns>
        IEnumerable<ILogixTag> Members(Predicate<ILogixTag> predicate);
        
        
        /// <summary>
        /// Gets all descendent tag members of the tag member specified by the provided tag name path.
        /// </summary>
        /// <param name="tagName">A tag name path to the tag member for which to get members of.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="ILogixTag"/> objects.</returns>
        IEnumerable<ILogixTag> MembersOf(TagName tagName);
    }
}