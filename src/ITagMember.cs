using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITagMember<out TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// The <c>Name</c> of a <c>TagMember</c> refers to the individual tag member name.
        /// </summary>
        /// <remarks>
        /// To get the full tag name use the <see cref="TagName"/> property of the <see cref="ITagMember{TDataType}"/>.
        /// </remarks>
        /// <example>
        /// For example...
        /// </example>
        string Name { get; }

        /// <summary>
        /// The <c>TagName</c> represents the full path (i.e. from the root <c>Tag</c>) to the <c>TagMember</c> name.
        /// </summary>
        string TagName { get; }

        /// <summary>
        /// The name of the <see cref="IDataType"/> of the tag member.
        /// </summary>
        string DataType { get; }

        /// <summary>
        /// 
        /// </summary>
        Dimensions Dimensions { get; }

        /// <summary>
        /// 
        /// </summary>
        Radix Radix { get; }

        /// <summary>
        /// 
        /// </summary>
        ExternalAccess ExternalAccess { get; }

        /// <summary>
        /// 
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        ITagMember<IDataType> Parent { get; }

        void SetRadix(Radix radix);
        
        void SetDescription(string description);

        IAtomic GetData();

        void SetData(IAtomic value);
        
        IEnumerable<string> GetMemberList();
        
        IEnumerable<string> GetDeepMembersList();
        
        ITagMember<IDataType> this[string name] { get; }
        
        ITagMember<IDataType> this[Func<TDataType, IMember<IDataType>> expression] { get; }
        
        ITagMember<TDataType> this[int index] { get; }
        
        IEnumerable<ITagMember<IDataType>> GetMembers();
        
        IEnumerable<ITagMember<IDataType>> GetMembers(Func<ITagMember<IDataType>, bool> predicate);

        ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType;

        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic;

        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic;

        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description)
            where TAtomic : IAtomic;
    }
}