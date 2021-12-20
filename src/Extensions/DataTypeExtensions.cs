using System.Collections.Generic;
using System.Linq;
using L5Sharp.Common;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IDataType"/>.
    /// </summary>
    public static class DataTypeExtensions
    {
        /*/// <summary>
        /// Gets a single member with the specified name from the provided <c>DataType</c>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <param name="name">The name of the member to retrieve.</param>
        /// <returns>A <c>Member</c> instance specified by the provided name if it exists, null if it does not.</returns>
        public static IMember<IDataType>? GetMember(this IDataType dataType, string name) =>
            dataType is IComplexType complexType ? complexType.Members.SingleOrDefault(m => m.Name == name) : null;

        /// <summary>
        /// Gets the collection of <c>Members</c> for the provided <c>DataType</c>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>A collection of members for the provided data type if there are any, an empty collection if not.</returns>
        public static IEnumerable<IMember<IDataType>> GetMembers(this IDataType dataType) =>
            dataType is IComplexType complexType ? complexType.Members : Enumerable.Empty<IMember<IDataType>>();*/


        public static bool StructureEquals(this IComplexType target, IComplexType? source)
        {
            if (ReferenceEquals(source, null)) return false;
            if (ReferenceEquals(target, source)) return true;
            return target.Name == source.Name &&
                   target.GetMembers().SequenceEqual(source.GetMembers(), new StructureComparer());
        }
    }
}