using System;
using System.Linq;
using System.Linq.Expressions;

namespace L5Sharp.Extensions
{
    public static class TagExtensions
    {
        /*public static ITag<TType> ToType<TType>(this Tag tag) where TType : IDataType, new()
        {
            if (!(tag.DataType is TType))
                throw new InvalidOperationException();//InvalidTypeConversion();
            
            return new Tag<TType>(tag);
        }*/

        public static ITagMember GetMember<TDataType>(this ITagMember tag,
            Expression<Func<TDataType, IMember>> propertyExpression)
            where TDataType : IDataType
        {
            if (!(tag.DataType is TDataType))
                throw new InvalidOperationException();
            
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            return tag.Members.SingleOrDefault(m => m.Name == propertyName);
        }

        public static void SetMemberValue<TDataType, TMember>(this ITagMember tag,
            Expression<Func<TDataType, IMember>> propertyExpression, object value)
            where TDataType : IDataType
        {
            if (!(tag.DataType is TDataType))
                throw new InvalidOperationException();
            
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