using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface ITag : ITagMember
    {
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; }
        IComponent Parent { get; }
        void UpdateDataType(IDataType dataType);
        ITag ChangeTagType(TagType type);
        IEnumerable<string> ListMembersNames();
    }

    public interface ITag<out TDataType> : ITag where TDataType : IDataType
    {
        TDataType GetDataType();
        /*ITagMember GetMember<TProperty>(Expression<Func<TDataType, TProperty>> propertyExpression) where TProperty : IMember;*/
        ITag<TType> AsType<TType>() where TType : IDataType;
    }
}