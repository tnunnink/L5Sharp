using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Exceptions;

namespace L5Sharp
{
    /// <summary>
    /// Represents a member of a <see cref="ITag{TDataType}"/> component that has a complex structure or type.
    /// </summary>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.</typeparam>
    /// <remarks>
    /// <see cref="ITagMember{TDataType}"/> is effectively a wrapper for a given <see cref="IMember{TDataType}"/> that
    /// is defined by a tag's root data type. A <see cref="ITagMember{TDataType}"/> is internally constructed when a call
    /// for a descendent member is made via the methods for retrieving members.
    /// These members are meant to be immutable objects (which resembles their character in Logix)
    /// that only reveal the state of the underlying data type instance.
    /// Note that when you set the <see cref="Value"/> or <see cref="ILogixComponent.Description"/> of the current
    /// member, you are really mutating the state of the underlying <see cref="IDataType"/> or root
    /// <see cref="ITag{TDataType}"/> instance, respectively.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public interface ITagMember<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Gets the <see cref="TagName"/> value of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        TagName TagName { get; }
        
        /// <summary>
        /// Gets value of the current <see cref="ITagMember{TDataType}"/>.
        /// </summary>
        /// <value>
        /// For <see cref="IAtomicType"/> members, returns the value of the underlying type instance.
        /// If the current tag member is a <see cref="IComplexType"/> or <see cref="IArrayType{TDataType}"/>,
        /// return a null object value.
        /// </value>
        object? Value { get; }

        /// <summary>
        /// Gets the parent <see cref="ITagMember{TDataType}"/> of the current member instance.
        /// </summary>
        ITagMember<IDataType>? Parent { get; }
        
        /// <summary>
        /// Gets the root <see cref="ITag{TDataType}"/> of the current member instance.
        /// </summary>
        ITag<IDataType> Root { get; }

        /// <summary>
        /// Gets a <see cref="ITagMember{TDataType}"/> at the specified one-dimensional index from the
        /// underlying <see cref="IArrayType{TDataType}"/>.
        /// </summary>
        /// <param name="x">The index of the three dimensional array.</param>
        /// <returns>
        /// A new <see cref="ITagMember{TDataType}"/> element from the specified index.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="ITagMember{TDataType}"/> data type is not an <see cref="IArrayType{TDataType}"/>
        /// -or- the dimensions of the current tag type are not a one-dimensional array.</exception>
        /// <exception cref="ArgumentException">index does not form a valid index of the current array dimensions.</exception>
        /// <remarks>
        /// Like <see cref="IArrayType{TDataType}"/>, this overload is for one-dimensional array types only.
        /// If the current tag is not a one-dimensional array of some <see cref="IDataType"/>, this call will fail.  
        /// </remarks>
        ITagMember<IDataType> this[int x] { get; }

        /// <summary>
        /// Gets a <see cref="ITagMember{TDataType}"/> at the specified two-dimensional index from the
        /// underlying <see cref="IArrayType{TDataType}"/>.
        /// </summary>
        /// <param name="x">The X index of the two dimensional array.</param>
        /// <param name="y">The Y index of the two dimensional array.</param>
        /// <returns>
        /// A new <see cref="ITagMember{TDataType}"/> element from the specified index.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="ITagMember{TDataType}"/> data type is not an <see cref="IArrayType{TDataType}"/>
        /// -or- the dimensions of the current tag type are not a two-dimensional array.</exception>
        /// <exception cref="ArgumentException">X and Y do not form a valid index of the current array dimensions.</exception>
        /// <remarks>
        /// Like <see cref="IArrayType{TDataType}"/>, this overload is for two-dimensional array types only.
        /// If the current tag is not a two-dimensional array of some <see cref="IDataType"/>, this call will fail.  
        /// </remarks>
        ITagMember<IDataType> this[int x, int y] { get; }

        /// <summary>
        /// Gets a <see cref="ITagMember{TDataType}"/> at the specified three-dimensional index from the
        /// underlying <see cref="IArrayType{TDataType}"/>.
        /// </summary>
        /// <param name="x">The X index of the three dimensional array.</param>
        /// <param name="y">The Y index of the three dimensional array.</param>
        /// <param name="z">The X index of the three dimensional array.</param>
        /// <returns>
        /// A new <see cref="ITagMember{TDataType}"/> element from the specified index.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="ITagMember{TDataType}"/> data type is not an <see cref="IArrayType{TDataType}"/>
        /// -or- the dimensions of the current tag type are not a three-dimensional array.</exception>
        /// <exception cref="ArgumentException">X, Y, and Z do not form a valid index of the current array dimensions.</exception>
        /// <remarks>
        /// Like <see cref="IArrayType{TDataType}"/>, this overload is for three-dimensional array types only.
        /// If the current tag is not a three-dimensional array of some <see cref="IDataType"/>, this call will fail.  
        /// </remarks>
        ITagMember<IDataType> this[int x, int y, int z] { get; }

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
        /// Determines if the provided <see cref="TagName"/> value exists as a descendent member of the <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        /// <param name="tagName">The <see cref="TagName"/> value to search for in the nested member hierarchy.</param>
        /// <returns>true if a descendent member with the specified tag name path exists; otherwise, false.</returns>
        bool HasMember(TagName tagName);

        /// <summary>
        /// Gets a descendent <see cref="ITagMember{TDataType}"/> instance with the specified tag name value. 
        /// </summary>
        /// <param name="tagName">The <see cref="TagName"/> value of the descendent member to get.</param>
        /// <returns>
        /// A new <see cref="ITagMember{TDataType}"/> instance that represents the descendent member with the specified
        /// tag name.
        /// </returns>
        /// <exception cref="ArgumentNullException">tagName is null.</exception>
        /// <exception cref="InvalidMemberPathException">
        /// A member with the specified tag name does not exist as a descendent member of the tag member data type.
        /// </exception>
        /// <remarks>
        /// This method gets immediate and nested descendent members from the current <see cref="ITagMember{TDataType}"/>
        /// based on the specified tag name value. This means the user can provide the full "dot-down" path to a nested
        /// member for retrieval. This method also retrieves both members of an <see cref="IComplexType"/> and/or
        /// <see cref="IArrayType{TDataType}"/> objects. This method will work as long as the provided path is valid.
        /// </remarks>
        /// <seealso cref="Member{TMemberType}"/>
        ITagMember<IDataType> Member(TagName tagName);

        /// <summary>
        /// Gets a <see cref="ITagMember{TDataType}"/> using the provided member selector delegate function.
        /// </summary>
        /// <param name="selector">
        /// A func delegate that selects a <see cref="IMember{TDataType}"/> of the current tag data type.
        /// </param>
        /// <typeparam name="TMemberType">
        /// The <see cref="IDataType"/> of the member that is being selected.
        /// This allows the returned <see cref="ITagMember{TDataType}"/> to be strongly typed which enables chaining calls.
        /// </typeparam>
        /// <returns>
        /// A new <see cref="ITagMember{TDataType}"/> instance that represents the selected child member of the current
        /// tag data type.
        /// </returns>
        /// <seealso cref="Member"/>
        ITagMember<TMemberType> Member<TMemberType>(Func<TDataType, IMember<TMemberType>> selector)
            where TMemberType : IDataType;

        /// <summary>
        /// Gets all <see cref="ITagMember{TDataType}"/> instances that are immediate descendents of the current <see cref="ITag{TDataType}"/>.
        /// </summary>
        /// <returns>A collection of <see cref="ITagMember{TDataType}"/> objects if any are found; otherwise, an empty collection.</returns>
        IEnumerable<ITagMember<IDataType>> Members();

        /// <summary>
        /// Gets all descendent member <see cref="TagName"/> values of the current <see cref="ITagMember{TDataType}"/>.
        /// </summary>
        /// <returns>A collection of all member <see cref="TagName"/> values if any exist.</returns>
        /// <remarks>
        /// This method returns tag name values relative to the current member instance. This does so by traversing the
        /// nested member hierarchy of the current data type.
        /// </remarks>
        IEnumerable<TagName> TagNames();

        /// <summary>
        /// Attempts to sets the <see cref="Value"/> property of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        /// <param name="value">The <see cref="IAtomicType"/> value to set.</param>
        /// <returns>
        /// true if <see cref="Value"/> was successfully set; otherwise, false. 
        /// </returns>
        /// <remarks>
        /// This method differs from <see cref="SetValue"/> in that if the provided value is invalid or the
        /// current member type is not an <see cref="IAtomicType"/>, instead of throwing an exception,
        /// it will return a result indicating whether the function succeeded of failed.
        /// </remarks>
        bool TrySetValue(IAtomicType? value);

        /// <summary>
        /// Sets the <see cref="Value"/> property of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        /// <param name="value">The <see cref="IAtomicType"/> value to set.</param>
        /// <exception cref="ArgumentNullException">When value is null.</exception>
        /// <exception cref="ArgumentException">When value is not valid for the current member type.</exception>
        /// <exception cref="InvalidOperationException">
        /// When the current member type is not a type that implements <see cref="IAtomicType"/>.
        /// </exception>
        /// <seealso cref="TrySetValue"/>
        void SetValue(IAtomicType value);

        /// <summary>
        /// Sets the value of all members of the current <see cref="ITagMember{TDataType}"/> instance.
        /// </summary>
        /// <param name="dataType">A data type instance that is of the same typ</param>
        void SetData(IComplexType dataType);
    }
}