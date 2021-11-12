using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Extensions
{
    public static class DataTypeExtensions
    {
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

        public static bool HasEquivalentStructure(this IDataType target, IDataType source)
        {
            if (target == null || source == null) return false;
            if (target.GetType() != source.GetType()) return false;
            if (ReferenceEquals(target, source)) return true;
            if (target is IAtomic) return true;
            return true;//todo we want to compare member types at this point
        }
    }
}