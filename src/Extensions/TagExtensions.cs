using System;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Enums;

namespace L5Sharp.Extensions
{
    public static class TagExtensions
    {
        public static ITagMember GetMember<TDataType>(this ITagMember tag,
            Expression<Func<TDataType, IMember>> propertyExpression)
            where TDataType : IDataType => tag.GetMemberInternal(propertyExpression);

        public static void SetMemberDescription<TDataType>(this ITagMember tag,
            Expression<Func<TDataType, IMember>> propertyExpression, string description)
            where TDataType : IDataType
        {
            var member = tag.GetMemberInternal(propertyExpression);

            if (member == null)
                throw new InvalidOperationException();

            member.SetDescription(description);
        }
        
        public static void SetMemberRadix<TDataType>(this ITagMember tag,
            Expression<Func<TDataType, IMember>> propertyExpression, Radix radix)
            where TDataType : IDataType
        {
            var member = tag.GetMemberInternal(propertyExpression);

            if (member == null)
                throw new InvalidOperationException();

            member.SetRadix(radix);
        }

        public static void SetMemberValue<TDataType>(this ITagMember tag,
            Expression<Func<TDataType, IMember>> propertyExpression, object value)
            where TDataType : IDataType
        {
            var member = tag.GetMemberInternal(propertyExpression);

            if (member == null)
                throw new InvalidOperationException();

            member.SetValue(value);
        }

        private static ITagMember GetMemberInternal<TDataType>(this ITagMember tag,
            Expression<Func<TDataType, IMember>> propertyExpression)
            where TDataType : IDataType
        {
            if (!(tag.DataType is TDataType))
                throw new InvalidOperationException();
            
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            return tag.Members.SingleOrDefault(m =>
                string.Equals(m.Name, propertyName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}