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
    /// <c>TagMember</c> instances act as wrappers around <see cref="IMember{TDataType}"/>.
    /// They are meant to be immutable objects (which resembles their character in Logix) that only reveal the state of
    /// the underlying data type instance.
    /// Note that when you set the <see cref="Value"/> or <see cref="ILogixComponent.Description"/> of the current
    /// <c>TagMember</c>, you are really setting the value of the underlying <c>DataType</c> or root <c>Tag</c> instance, respectively.
    /// Also, a <c>TagMember</c> is not constructed with the root <c>Tag</c>, but rather is instantiated when a call to a
    /// method or indexer that returns a <c>TagMember</c> is made. 
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the <c>TagMember</c>.</typeparam>
    public interface ITagMember<out TDataType> : ILogixComponent where TDataType : IDataType
    {
        /// <summary>
        /// Gets the full name of the <c>TagMember</c> including the root <c>Tag</c> name.
        /// </summary>
        string TagName { get; }
        
        /// <summary>
        /// Gets the full name of the <c>TagMember</c> without the root Tag name.
        /// </summary>
        string Operand { get; }

        /// <summary>
        /// Gets the DataType name of the <c>TagMember</c>.
        /// </summary>
        string DataType { get; }

        /// <summary>
        /// Gets the Dimensions of the <c>TagMember</c>.
        /// </summary>
        Dimensions Dimensions { get; }

        /// <summary>
        /// Gets the Radix of the <c>TagMember</c>.
        /// </summary>
        /// <value>
        /// If the current <c>TagMember</c> is a <see cref="IComplexType"/>, returns <see cref="Enums.Radix.Null"/>.
        /// If the <c>TagMember</c> type is <see cref="IAtomicType"/>, the will return the member's configured
        /// <c>Radix</c> value.
        /// </value>
        Radix Radix { get; }
        
        /// <summary>
        /// Gets the ExternalAccess of the <c>TagMember</c>.
        /// </summary>
        ExternalAccess ExternalAccess { get; }

        /// <summary>
        /// Gets the Atomic value of the TagMember.
        /// </summary>
        /// <value>
        /// For Atomic typed <c>TagMembers</c>, returns the value of the <see cref="IAtomicType"/> type.
        /// If the current <c>TagMember</c> instance is a complex type, returns null.
        /// </value>
        object? Value { get; }

        /// <summary>
        /// Gets the parent of the current <c>TagMember</c> instance.
        /// </summary>
        /// <value>
        /// A <c>TagMember</c> for all non-root <c>Tag</c> instances. If the current instance is the root, returns null. 
        /// </value>
        ITagMember<IDataType>? Parent { get; }
        
        /// <summary>
        /// Gets the root of the current <c>TagMember</c> instance.
        /// </summary>
        ITag<IDataType> Root { get; }

        /// <summary>
        /// Sets the <c>TagMember</c> <see cref="ILogixComponent.Description"/> with the provided string comment. 
        /// </summary>
        /// <remarks>
        /// <c>TagMember</c> comments are stored and maintained by root <c>Tag</c> instance.
        /// Setting a comment on a member "overrides" the description of the underlying member.
        /// Setting the comment to an empty or null value resets the description to the value of the underlying member.
        /// This functionality mimics the feature called "Pass Through Description" provided by Logix to
        /// help developers maintain documentation for tags. 
        /// </remarks>
        /// <param name="comment">The value of the comment to set.</param>
        void Comment(string comment);

        /// <summary>
        /// Sets the <see cref="Value"/> of the current <c>TagMember</c>.
        /// </summary>
        /// <remarks>
        /// If the current <c>TagMember</c> is an
        /// </remarks>
        /// <param name="value">The atomic value type to set.</param>
        /// <exception cref="ArgumentException">
        /// Throw when the provided value is null or not valid for the current <c>TagMember</c> type.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the current <c>TagMember</c> type is not an <c>IAtomicType</c>.
        /// </exception>
        void SetValue(IAtomicType value);

        /// <summary>
        /// Sets the <see cref="Value"/> property of the current <c>TagMember</c> if it is an <c>IAtomicType</c> member.
        /// </summary>
        /// <remarks>
        /// This method differs from <see cref="SetValue"/> in that if the provided value is invalid or the
        /// current <c>TagMember</c> is not an <c>IAtomicType</c>, it will return without throwing an exception.
        /// </remarks>
        /// <param name="value">The atomic value to set.</param>
        bool TrySetValue(IAtomicType value);

        /// <summary>
        /// Gets an element of the current <c>TagMember</c> array at the provided index.
        /// </summary>
        /// <remarks>
        /// Like <see cref="IMember{TDataType}"/>, only a <c>TagMember</c> with non-empty dimensions will have elements
        /// accessible by index. Otherwise the <c>TagMember</c> is not an array and therefore contains no elements.
        /// This index getter will not fail if the provided index is invalid. It will simply return null.
        /// </remarks>
        /// <param name="index">The array index of the member instance.</param>
        /// <returns>
        /// A <c>TagMember</c> instance representing the element of the provided index.
        /// If the provided index is invalid (i.e. less than zero or greater than the dimensional length), then null.
        /// </returns>
        ITagMember<TDataType>? this[int index] { get; }

        /// <summary>
        /// Gets a child <c>TagMember</c> of the current <c>TagMember</c> instance with the provided TagName. 
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
        /// Gets all child members of the current <c>TagMember</c> instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITagMember<IDataType>> GetMembers();

        IEnumerable<ITagMember<IDataType>> GetMembers(Func<ITagMember<IDataType>, bool> predicate);

        /// <summary>
        /// Gets a list of child member names of the current <c>TagMember</c>
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
        /// Gets list of all nested child member names of the current <c>TagMember</c>
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

        /*
        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic;

        void SetMember<TType>(Func<TDataType, IMember<TType>> expression, string description)
            where TType : IDataType;*/
    }
}