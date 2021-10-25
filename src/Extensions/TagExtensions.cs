using System;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Extensions
{
    public static class TagExtensions
    {
        public static ITag<TType> ToType<TType>(this Tag tag) where TType : IDataType, new()
        {
            if (!(tag.DataType is TType))
                throw new InvalidOperationException();//InvalidTypeConversion();
            
            return new Tag<TType>(tag);
        }
        
        public static ITag<TType> ToType<TType>(this ITag<IDataType> tag) where TType : IDataType, new()
        {
            if (!(tag.DataType is TType))
                throw new InvalidOperationException();//InvalidTypeConversion();
            
            return new Tag<TType>(tag);
        }
        
        public static ITagMember<IDataType> GetMember<TDataType, TMember>(this ITagMember<TDataType> tag,
            Expression<Func<TDataType, TMember>> propertyExpression)
            where TDataType : IDataType
            where TMember : IMember
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            return tag.Members.SingleOrDefault(m => m.Name == propertyName);
        }

        public static void SetMemberValue<TDataType, TMember>(this ITagMember<TDataType> tag,
            Expression<Func<TDataType, TMember>> propertyExpression, object value)
            where TDataType : IDataType
            where TMember : IMember
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            var member = tag.Members.SingleOrDefault(m => m.Name == propertyName);

            if (member == null)
                throw new InvalidOperationException();

            member.SetValue(value);
        }
    }
}