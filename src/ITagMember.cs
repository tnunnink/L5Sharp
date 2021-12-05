using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataType"></typeparam>
    public interface ITagMember<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Gets the current <c>TagMember</c> full name including the root <c>Tag</c> name.
        /// </summary>
        string TagName { get; }
        
        /// <summary>
        /// Gets the current <c>TagMember</c> full name without the root <c>Tag</c> name.
        /// </summary>
        string Operand { get; }

        /// <summary>
        /// Gets the name of the <see cref="IDataType"/> for the TagMember.
        /// </summary>
        new string DataType { get; }

        /*/// <summary>
        /// The value of the <see cref="L5Sharp.Core.Dimensions"/> for the <c>TagMember</c>.
        /// </summary>
        Dimensions Dimensions { get; }*/

        /// <summary>
        /// Sets the member description with the provided string value. 
        /// </summary>
        /// <remarks>
        /// A <c>TagMember</c> comment is maintained by root tag instance. Setting a comment on a member overrides the description for the
        /// Logix uses a feature called "Pass Through Description" to help developers maintain documentation. This 
        /// </remarks>
        /// <param name="comment">The value of the string description to set.</param>
        void Comment(string comment);
        
        /// <summary>
        /// Gets the parent member of the current tag member.
        /// </summary>
        ITagMember<IDataType> Parent { get; }

        /// <summary>
        /// Gets the member's atomic value if is a value type.
        /// </summary>
        /// <returns></returns>
        IAtomic GetData();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void SetData(IAtomic value);
        
        /// <summary>
        /// Gets a list of child member names of the current <c>TagMember</c>
        /// </summary>
        /// <remarks>
        /// Only returns list of immediate child member names. This method does not recursively traverse nested members.
        /// For a full nested list of member names use <see cref="GetDeepMembersNames"/>.
        /// </remarks>
        /// <returns>
        /// An enumerable collection of string names that represent the child members if any exist.
        /// If none exist, returns an empty collection.
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
        
        /// <summary>
        /// Gets a child member of the current <c>TagMember</c> by name. 
        /// </summary>
        /// <param name="name">The name of the child member to retrieve.</param>
        /// <returns>
        /// A new instance a <see cref="ITagMember{TDataType}"/> that represents the child member if it exists.
        /// If not found, then will return null.
        /// </returns>
        ITagMember<IDataType> this[string name] { get; }
        
        /// <summary>
        /// Gets a child member of the current <c>TagMember</c> by a Func delegate of the current TDataType. 
        /// </summary>
        /// <param name="expression">
        /// The Func expression of the current TDataType that points to the <see cref="IMember{TDataType}"/> instance
        /// to retrieve.
        /// </param>
        /// <example>
        /// Creating a tag of type <see cref="L5Sharp.Types.Timer"/> will allow us to retrieve members in the following manner:
        /// <code>
        /// var tag = Tag.Create("TimerTag", new Timer());
        /// var presetMember = tag[t => t.PRE];   
        /// </code>
        /// </example>
        /// <returns>
        /// A new instance a <see cref="ITagMember{TDataType}"/> that represents the child member if it exists.
        /// If not found, then will return null.
        /// </returns>
        ITagMember<IDataType> this[Func<TDataType, IMember<IDataType>> expression] { get; }
        
        ITagMember<TDataType> this[int index] { get; }
        
        IEnumerable<ITagMember<IDataType>> GetMembers();
        
        IEnumerable<ITagMember<IDataType>> GetMembers(Func<ITagMember<IDataType>, bool> predicate);

        ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType;

        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic;

        /*
        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic;
            */
        
        void SetMember<TType>(Func<TDataType, IMember<TType>> expression, string description)
            where TType : IDataType;
    }
}