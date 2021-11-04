using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Extensions
{
    public static class DataTypeExtensions
    {
        public static IEnumerable<IDataType> GetDependentTypes(this IDataType type) 
            => GetUniqueMemberTypes(type);
        
        private static IEnumerable<IDataType> GetUniqueMemberTypes(IDataType dataType)
        {
            var types = new List<IDataType>();

            foreach (var member in dataType.Members)
            {
                types.Add(member.DataType);
                types.AddRange(GetUniqueMemberTypes(member.DataType));
            }

            return types.Distinct();
        }
    }
}