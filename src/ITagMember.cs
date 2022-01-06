using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a child tag of a given root <see cref="ITag{TDataType}"/>.
    /// </summary>
    /// <remarks>
    /// <c>ITagMember</c> instances act as wrappers around <see cref="IMember{TDataType}"/>.
    /// They are meant to be immutable objects (which resembles their character in Logix) that only reveal the state of
    /// the underlying data type instance.
    /// Note that when you set the <see cref="Value"/> or <see cref="ILogixComponent.Description"/> of the current
    /// <c>ITagMember</c>, you are really setting the value of the underlying <c>DataType</c> or root <c>Tag</c> instance, respectively.
    /// Also, a <c>ITagMember</c> is not constructed with the root <c>Tag</c>, but rather is instantiated when a call to a
    /// method or indexer that returns a <c>ITagMember</c> is made. 
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the <c>ITagMember</c>.</typeparam>
    public interface ITagMember<out TDataType> : ILogixComponent where TDataType : IDataType
    {
        /// <summary>
        /// Gets the full name of the <c>ITagMember</c> including the root <c>Tag</c> name.
        /// </summary>
        string TagName { get; }
        
        /// <summary>
        /// Gets the full name of the <c>ITagMember</c> without the root Tag name.
        /// </summary>
        string Operand { get; }

        /// <summary>
        /// Gets the DataType name of the <c>ITagMember</c>.
        /// </summary>
        string DataType { get; }

        /// <summary>
        /// Gets the Dimensions of the <c>ITagMember</c>.
        /// </summary>
        Dimensions Dimensions { get; }

        /// <summary>
        /// Gets the Radix of the <c>ITagMember</c>.
        /// </summary>
        /// <value>
        /// If the current <c>ITagMember</c> is a <see cref="IComplexType"/>, returns <see cref="Enums.Radix.Null"/>.
        /// If the <c>ITagMember</c> type is <see cref="IAtomicType"/>, the will return the member's configured
        /// <c>Radix</c> value.
        /// </value>
        Radix Radix { get; }
        
        /// <summary>
        /// Gets the ExternalAccess of the <c>ITagMember</c>.
        /// </summary>
        ExternalAccess ExternalAccess { get; }

        /// <summary>
        /// Gets the Atomic value of the TagMember.
        /// </summary>
        /// <value>
        /// For Atomic typed <c>TagMembers</c>, returns the value of the <see cref="IAtomicType"/> type.
        /// If the current <c>ITagMember</c> instance is a complex type, returns null.
        /// </value>
        object? Value { get; }

        /// <summary>
        /// Gets the parent of the current <c>ITagMember</c> instance.
        /// </summary>
        /// <value>
        /// A <c>ITagMember</c> for all non-root <c>Tag</c> instances. If the current instance is the root, returns null. 
        /// </value>
        ITagMember<IDataType>? Parent { get; }
        
        /// <summary>
        /// Gets the root of the current <c>ITagMember</c> instance.
        /// </summary>
        ITag<IDataType> Root { get; }

        /// <summary>
        /// Sets the <c>ITagMember</c> <see cref="ILogixComponent.Description"/> with the provided string comment. 
        /// </summary>
        /// <remarks>
        /// <c>ITagMember</c> comments are stored and maintained by root <c>Tag</c> instance.
        /// Setting a comment on a member "overrides" the description of the underlying member.
        /// Setting the comment to an empty or null value resets the description to the value of the underlying member.
        /// This functionality mimics the feature called "Pass Through Description" provided by Logix to
        /// help developers maintain documentation for tags. 
        /// </remarks>
        /// <param name="comment">The value of the comment to set.</param>
        void Comment(string comment);

        /// <summary>
        /// Sets the <see cref="Value"/> property of the current <c>ITagMember</c> instance.
        /// </summary>
        /// <param name="value">The <c>IAtomicType</c> value to set.</param>
        /// <exception cref="ArgumentNullException">When value is null.</exception>
        /// <exception cref="ArgumentException">When value is not valid for the current <c>ITagMember</c> type.</exception>
        /// <exception cref="InvalidOperationException">
        /// When the current <c>ITagMember</c> data type is not a type that implements <c>IAtomicType</c>.
        /// </exception>
        /// <seealso cref="TrySetValue"/>
        void SetValue(IAtomicType value);

        /// <summary>
        /// Sets the <see cref="Value"/> property of the current <c>ITagMember</c> instance.
        /// </summary>
        /// <param name="value">The <c>IAtomicType</c> value to set.</param>
        /// <returns>
        /// true if the value property was successfully set; otherwise, false. 
        /// </returns>
        /// <remarks>
        /// This method differs from <see cref="SetValue"/> in that if the provided value is invalid or the
        /// current <c>ITagMember</c> is not an <c>IAtomicType</c>, it will not throw an exception,
        /// but instead return false, indicating that the call failed.
        /// </remarks>
        bool TrySetValue(IAtomicType value);

        /// <summary>
        /// Gets an element of the current <c>ITagMember</c> array at the provided index.
        /// </summary>
        /// <remarks>
        /// Like <see cref="IMember{TDataType}"/>, only a <c>ITagMember</c> with non-empty dimensions will have elements
        /// accessible by index. Otherwise the <c>ITagMember</c> is not an array and therefore contains no elements.
        /// This index getter will not fail if the provided index is invalid. It will simply return null.
        /// </remarks>
        /// <param name="index">The array index of the member instance.</param>
        /// <returns>
        /// A <c>ITagMember</c> instance representing the element of the provided index.
        /// If the provided index is invalid (i.e. less than zero or greater than the dimensional length), then null.
        /// </returns>
        ITagMember<TDataType>? this[int index] { get; }

        /// <summary>
        /// Gets a child tag member with the specified name of the current member instance. 
        /// </summary>
        /// <param name="name">The name of the child member to retrieve.</param>
        /// <returns>
        /// A new instance a <see cref="ITagMember{TDataType}"/> that represents the child member if it exists.
        /// If not found, then will return null.
        /// </returns>
        ITagMember<IDataType>? this[string name] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType;

        /// <summary>
        /// Gets all child members of the current <c>ITagMember</c> instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITagMember<IDataType>> GetMembers();

        /// <summary>
        /// Gets a list of child member names of the current <c>ITagMember</c>
        /// </summary>
        /// <remarks>
        /// Only returns list of immediate child member names. This method does not recursively traverse nested members.
        /// For a full nested list of member names use <see cref="GetDeepMembersNames"/>.
        /// </remarks>
        /// <returns>
        /// An enumerable collection of string names that represent the member's name (not TagName or Operand).
        /// If no child members exist, returns an empty collection.
        /// </returns>
        IEnumerable<string> GetMemberNames();
        
        /// <summary>
        /// Gets list of all nested child member names of the current <c>ITagMember</c>
        /// </summary>
        /// <remarks>
        /// Will recursively traverse all nested child members to get a full list of member names. 
        /// For a list of immediate child member names use <see cref="GetMemberNames"/>.
        /// </remarks>
        /// <returns>
        /// An enumerable collection of string names that represent the child members if any exist.
        /// If none exist, returns an empty collection.
        /// </returns>
        IEnumerable<string> GetDeepMembersNames();
    }
}