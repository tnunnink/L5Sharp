using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Extensions
{
    public static class DataTypeExtensions
    {
        public static bool IsValueType(this IDataType dataType)
        {
            return dataType is IAtomic;
        }

        public static bool AreSameClass(this IDataType dataType, IDataType other)
        {
            return dataType.Class.Equals(other.Class);
        }

        public static IMember<IDataType> GetMember(this IDataType dataType, string name)
        {
            return dataType switch
            {
                IUserDefined userType => userType.Members.Get(name),
                IPredefined predefined => predefined.Members.SingleOrDefault(m => m.Name == name),
                IAddOnDefined addOnDefined => addOnDefined.Members.SingleOrDefault(m => m.Name == name),
                _ => null
            };
        }

        public static IEnumerable<IMember<IDataType>> GetMembers(this IDataType dataType)
        {
            return dataType switch
            {
                IUserDefined userType => userType.Members,
                IPredefined predefined => predefined.Members,
                IAddOnDefined addOnDefined => addOnDefined.Members,
                _ => Enumerable.Empty<IMember<IDataType>>()
            };
        }

        public static IEnumerable<string> GetMemberNames(this IDataType dataType)
        {
            var names = new List<string>();

            foreach (var member in dataType.GetMembers())
            {
                names.Add(member.Name);
                names.AddRange(member.DataType.GetMemberNames().Select(n => $"{member.Name}.{n}"));
            }

            return names;
        }

        public static IEnumerable<IDataType> GetDependentTypes(this IDataType dataType)
        {
            var types = new List<IDataType>();

            foreach (var member in dataType.GetMembers())
            {
                types.Add(member.DataType);
                types.AddRange(member.DataType.GetDependentTypes());
            }

            return types.Distinct();
        }

        public static bool StructureEquals(this IDataType target, IDataType source)
        {
            if (source == null) return false;
            if (ReferenceEquals(target, source)) return true;
            if (target.IsValueType() && source.IsValueType() && target.GetType() == source.GetType()) return true;
            if (target.IsValueType() || source.IsValueType()) return false;
            return target.Name == source.Name && 
                   target.GetMembers().SequenceEqual(source.GetMembers(), new MemberStructureComparer());
        }
    }

    public class MemberStructureComparer : IEqualityComparer<IMember<IDataType>>
    {
        public bool Equals(IMember<IDataType> x, IMember<IDataType> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Name.Equals(y.Name)
                   && x.DataType.Name.Equals(y.DataType.Name)
                   && Equals(x.Dimensions, y.Dimensions)
                   && x.DataType.StructureEquals(y.DataType);
        }

        public int GetHashCode(IMember<IDataType> obj)
        {
            return HashCode.Combine(obj.DataType.Name, obj.Dimensions);
        }
    }
}