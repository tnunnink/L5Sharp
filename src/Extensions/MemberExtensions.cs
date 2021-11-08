using System;
using L5Sharp.Core;

namespace L5Sharp.Extensions
{
    public static class MemberExtensions
    {
        public static IMember<TDataType> As<TDataType>(this IMember<IDataType> member)
            where TDataType : IDataType
        {
            if (!(member.DataType is TDataType dataType)) return null;

            return new Member<TDataType>(member.Name, dataType, member.Dimensions, member.Radix, member.ExternalAccess,
                member.Description);
        }

        public static ITagMember<TDataType> As<TDataType>(this ITagMember<IDataType> member)
            where TDataType : IDataType
        {
            if (!(member.GetValue() is TDataType dataType))
                throw new InvalidCastException();

            var casted = new Member<TDataType>(member.Name, dataType, member.Dimensions, member.Radix,
                member.ExternalAccess, member.Description);

            return new TagMember<TDataType>(casted, member.Parent);
        }
    }
}