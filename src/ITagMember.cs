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
        /// Gets the <see cref="TagName"/> value of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        TagName TagName { get; }

        /// <summary>
        /// Gets the name of the <see cref="IDataType"/> that the <see cref="ITag{TDataType}"/> represents.
        /// </summary>
        string DataType { get; }

        /// <summary>
        /// Gets the <see cref="Core.Dimensions"/> value of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        Dimensions Dimensions { get; }

        /// <summary>
        /// Gets the <see cref="Enums.Radix"/> value of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        Radix Radix { get; }
        
        /// <summary>
        /// Gets the <see cref="Enums.ExternalAccess"/> value of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        ExternalAccess ExternalAccess { get; }

        /// <summary>
        /// Gets value of the current <see cref="ITagMember{TDataType}"/>.
        /// </summary>
        /// <value>
        /// For <see cref="IAtomicType"/> members, returns the value of the underlying type instance.
        /// If the current tag member is a <see cref="IComplexType"/> or <see cref="IArrayType{TDataType}"/>, return a null object value.
        /// </value>
        object? Value { get; }

        /// <summary>
        /// Gets the parent member of the current tag member instance.
        /// </summary>
        ITagMember<IDataType>? Parent { get; }
        
        /// <summary>
        /// Gets the root <see cref="ITag{TDataType}"/> instance.
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
        /// Gets a tag member element of the current <see cref="ITagMember{TDataType}"/> array at the provided index.
        /// </summary>
        /// <remarks>
        /// Like <see cref="IArrayType{TDataType}"/>, only a 
        /// </remarks>
        /// <param name="x">The index of the element to get.</param>
        /// <returns>
        /// A <see cref="ITagMember{TDataType}"/> instance representing the element of the provided index.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">The provided index is not within the bounds of the array.</exception>
        /// <exception cref="InvalidOperationException">The current type is not an <see cref="IArrayType{TDataType}"/>.</exception>
        ITagMember<TDataType> this[int x] { get; }
        
        ITagMember<TDataType> this[int x, int y] { get; }
        
        ITagMember<TDataType> this[int x, int y, int z] { get; }

        /// <summary>
        /// Gets a child tag member with the specified name of the current member instance. 
        /// </summary>
        /// <param name="name">The name of the child member to retrieve.</param>
        /// <returns>
        /// A new instance a <see cref="ITagMember{TDataType}"/> that represents the child member if it exists.
        /// If not found, then will return null.
        /// </returns>
        ITagMember<IDataType> this[TagName name] { get; }
        
        ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType;
        
        /// <summary>
        /// Gets all <see cref="ITagMember{TDataType}"/> instances that are immediate children of the current <see cref="ITag{TDataType}"/>.
        /// </summary>
        /// <returns>A collection of <see cref="ITagMember{TDataType}"/> objects if any are found; otherwise, an empty collection.</returns>
        IEnumerable<ITagMember<IDataType>> GetMembers();

        /// <summary>
        /// Attempts to get a descendent <see cref="ITagMember{TDataType}"/> with the provided <see cref="TagName"/> value.
        /// </summary>
        /// <param name="name">The tag name value of the <see cref="ITagMember{TDataType}"/> to get.</param>
        /// <param name="tagMember">When the method returns, contains an instance of the
        /// <see cref="ITagMember{TDataType}"/> that represents the descendent member if found; otherwise, null.</param>
        /// <returns>
        /// true if the member with the specified tag name was found as a descendent of the
        /// current <see cref="ITagMember{TDataType}"/> instance; otherwise, false.
        /// </returns>
        bool TryGetMember(TagName name, out ITagMember<IDataType>? tagMember);
        
        /// <summary>
        /// Gets all descendent member <see cref="TagName"/> values of the current <see cref="ITagMember{TDataType}"/>.
        /// </summary>
        /// <returns>A collection of all member <see cref="TagName"/> values if any exist.</returns>
        /// <remarks>
        /// This method returns tag name values relative to the current member instance. This does so by traversing the
        /// nested member hierarchy of the current data type.
        /// </remarks>
        IEnumerable<TagName> GetTagNames();
    }
}