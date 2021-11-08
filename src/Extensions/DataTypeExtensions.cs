using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Extensions
{
    public static class DataTypeExtensions
    {
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
        
        public static IMember<IDataType> GetMember(this IDataType dataType, string name)
        {
            return dataType switch
            {
                IUserDefined userType => userType.Members.Get(name),
                IPredefined predefined => predefined.GetMember(name),
                IAddOnDefined addOnDefined => addOnDefined.Members.SingleOrDefault(m => m.Name == name),
                _ => null
            };
        }
        
        public static IEnumerable<string> GetMemberNames(this IDataType dataType)
        {
            /*var names = new List<string>();

            foreach (var member in dataType.Members)
            {
                names.Add(member.Name);
                names.AddRange(GetMemberNames(member.DataType));
            }

            return names;*/
            throw new NotImplementedException();
        }

        public static IEnumerable<IDataType> GetDependentTypes(this IDataType dataType)
        {
            return dataType switch
            {
                IUserDefined userType => GetUniqueMemberTypes(userType),
                IPredefined predefined => GetUniqueMemberTypes(predefined),
                IAddOnDefined addOnDefined => GetUniqueMemberTypes(addOnDefined),
                _ => Enumerable.Empty<IDataType>()
            };
        }

        private static IEnumerable<IDataType> GetUniqueMemberTypes(this IUserDefined userDefined)
        {
            var types = new List<IDataType>();

            foreach (var member in userDefined.Members)
            {
                types.Add(member.DataType);
                types.AddRange(member.DataType.GetDependentTypes());
            }

            return types.Distinct();
        }

        private static IEnumerable<IDataType> GetUniqueMemberTypes(IPredefined predefined)
        {
            var types = new List<IDataType>();

            foreach (var member in predefined.Members)
            {
                types.Add(member.DataType);
                types.AddRange(member.DataType.GetDependentTypes());
            }

            return types.Distinct();
        }

        private static IEnumerable<IDataType> GetUniqueMemberTypes(IAddOnDefined addOnDefined)
        {
            var types = new List<IDataType>();

            foreach (var member in addOnDefined.Members)
            {
                types.Add(member.DataType);
                types.AddRange(member.DataType.GetDependentTypes());
            }

            return types.Distinct();
        }
    }
}