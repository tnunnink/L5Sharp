using System.Collections.Generic;
using L5Sharp.Common;
using L5Sharp.Core;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A static class for extension methods for <see cref="IDataType"/>.
    /// </summary>
    public static class DataTypeExtensions
    {
        /*/// <inheritdoc />
        public IEnumerable<IDataType> GetDependentTypes(this IComplexType complexType)
        {
            var set = new HashSet<IDataType>(ComponentNameComparer.Instance);

            foreach (var member in complexType.Members)
            {
                set.Add(member.DataType);

                if (member.DataType is not IComplexType complex) continue;

                foreach (var dataType in complex.GetDependentTypes())
                    set.Add(dataType);
            }

            return set;
        }*/
    }
}