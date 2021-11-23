using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IMember{TDataType}"/>.
    /// </summary>
    public static class MemberExtensions
    {
        /// <summary>
        /// Gets member names of the given <c>DataType</c>.
        /// </summary>
        /// <param name="member">The current data type.</param>
        /// <returns>A collection of string names that represent the full member name path from the root data type.</returns>
        public static IEnumerable<string> GetMemberNames<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            if (!(member is IArrayMember<IDataType> array))
                return member.DataType.GetMemberNames();

            return array.Select(element => $"{element.Name}");
        }
        
        /// <summary>
        /// Gets all nested member names of the given <c>DataType</c>.
        /// </summary>
        /// <param name="member">The current data type.</param>
        /// <returns>A collection of string names that represent the full member name path from the root data type.</returns>
        public static IEnumerable<string> GetDeepMemberNames<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            var names = new List<string>();

            if (!(member is IArrayMember<IDataType> array))
                return member.DataType.GetDeepMemberNames();

            foreach (var element in array)
            {
                names.Add($"{element.Name}");
                names.AddRange(element.DataType.GetDeepMemberNames());
            }

            return names;
        }
    }
}