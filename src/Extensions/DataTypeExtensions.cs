using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;

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

        public static void SetData(this IDataType target, IDataType source)
        {
            if (target.GetType() != source.GetType() || target.Name != source.Name)
                throw new InvalidTagDataException(target, source);

            if (target is IAtomic atomicTarget && source is IAtomic atomicSource &&
                atomicTarget.GetType() == atomicSource.GetType())
            {
                atomicTarget.SetValue(atomicSource);
                return;
            }

            foreach (var member in target.GetMembers())
            {
                var data = source.GetMember(member.Name)?.DataType;
                if (data == null) continue;
                member.DataType.SetData(data);
            }
        }
    }
}