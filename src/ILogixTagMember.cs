using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogixTagMember : ILogixMember
    {
        /// <summary>
        /// The full tag name path of the <see cref="TagMember"/>.
        /// </summary>
        /// <value>A <see cref="Core.TagName"/> type representing the tag name of the member.</value>
        TagName TagName { get; }
        
        /// <summary>
        /// The type of member the tag member represents (value, structure, array).
        /// </summary>
        /// <value>A <see cref="Enums.MemberType"/> enum representing the type of member.</value>
        MemberType MemberType { get; }

        /// <summary>
        /// Gets the underlying atomic value of the <see cref="TagMember"/>,
        /// if it is an <see cref="AtomicType"/> member.
        /// </summary>
        /// <returns>A <see cref="AtomicType"/> representing the value of the member if the <see cref="MemberType"/>
        /// is a value member; Otherwise, null.</returns>
        /// <seealso cref="GetValue{TAtomic}"/>
        AtomicType? GetValue();

        /// <summary>
        /// Gets the underlying atomic value of the <see cref="TagMember"/> as the a strongly typed <see cref="AtomicType"/>.
        /// If the member's data is not the specified type, returns null.
        /// </summary>
        /// <returns>A <see cref="AtomicType"/> representing the value of the member if the <see cref="MemberType"/>
        /// is a value member; Otherwise, null.</returns>
        /// <seealso cref="GetValue"/>
        TAtomic? GetValue<TAtomic>() where TAtomic : AtomicType;

        /// <summary>
        /// Gets the descendent tag member specified by the provided tag name path relative to the current tag member.
        /// </summary>
        /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.
        /// This value must be relative to the current member, meaning it should not include the current member name.</param>
        /// <returns>A <see cref="TagMember"/> representing the descendent member instance.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        TagMember? Member(TagName tagName);

        /// <summary>
        /// Gets all descendent tag members relative to the current tag member.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
        IEnumerable<TagMember> Members();
        
        /// <summary>
        /// Gets all descendent tag member relative to the member specified by the provided tag name path.
        /// </summary>
        /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
        IEnumerable<TagMember> Members(TagName tagName);

        /// <summary>
        /// Sets the underlying value type data of the current tag member.
        /// </summary>
        /// <param name="atomicType">A <see cref="AtomicType"/> value representing the value to set.</param>
        void SetValue(AtomicType atomicType);

        /// <summary>
        /// Attempts to set the underlying value type data of the current tag member.
        /// </summary>
        /// <param name="atomicType">A <see cref="AtomicType"/> value representing the value to set.</param>
        /// <returns><c>true</c> if the value was set; otherwise, <c>false</c>.</returns>
        bool TrySetValue(AtomicType atomicType);
    }
}