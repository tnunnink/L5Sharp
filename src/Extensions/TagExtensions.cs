using System;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Abstractions;

namespace L5Sharp.Extensions
{
    public static class TagExtensions
    {
        public static ITagMember GetMember<TDataType, TProperty>(this ITag<TDataType> tag,
            Expression<Func<TDataType, TProperty>> propertyExpression)
            where TProperty : IMember 
            where TDataType : IDataType
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            return tag.Members.SingleOrDefault(m => m.Name == propertyName);
        }
    }
}