using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IDataType"/>.
    /// </summary>
    public static class DataTypeExtensions
    {
        /// <summary>
        /// Indicates whether the current <c>DataType</c> is <see cref="IAtomic"/> (i.e. a value type).
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>true if the current data type is <see cref="IAtomic"/>, otherwise false.</returns>
        public static bool IsValueType(this IDataType dataType)
        {
            return dataType is IAtomic;
        }

        /// <summary>
        /// Determines if two <c>DataTypes</c> are of the same <see cref="L5Sharp.Enums.DataTypeClass"/>
        /// </summary>
        /// <param name="dataType">The current data type to compare.</param>
        /// <param name="other">The other data type to compare.</param>
        /// <returns>true is the <c>DataTypes</c> have the same value of <c>DataTypeClass</c></returns>
        public static bool AreSameClass(this IDataType dataType, IDataType other)
        {
            return dataType.Class.Equals(other.Class);
        }

        /// <summary>
        /// Gets a single member with the specified name from the provided <c>DataType</c>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <param name="name">The name of the member to retrieve.</param>
        /// <returns>A <c>Member</c> instance specified by the provided name if it exists, null if it does not.</returns>
        public static IMember<IDataType> GetMember(this IDataType dataType, string name)
        {
            if (!(dataType is IComplexType complexType))
                return null;

            return complexType.Members.SingleOrDefault(m => m.Name == name);
        }

        /// <summary>
        /// Gets the collection of <c>Members</c> for the provided <c>DataType</c>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>A collection of members for the provided data type if there are any, an empty collection if not.</returns>
        public static IEnumerable<IMember<IDataType>> GetMembers(this IDataType dataType)
        {
            if (!(dataType is IComplexType complexType))
                return Enumerable.Empty<IMember<IDataType>>();

            return complexType.Members;
        }
        
        /// <summary>
        /// Gets member names of the given <c>DataType</c>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>A collection of string names that represent the full member name path from the root data type.</returns>
        public static IEnumerable<string> GetMemberNames(this IDataType dataType)
        {
            return dataType.GetMembers().Select(member => member.Name);
        }

        /// <summary>
        /// Gets all nested member names of the given <c>DataType</c>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>A collection of string names that represent the full member name path from the root data type.</returns>
        public static IEnumerable<string> GetDeepMemberNames(this IDataType dataType)
        {
            var names = new List<string>();

            foreach (var member in dataType.GetMembers())
            {
                names.Add(member.Name);

                if (member is IArrayMember<IDataType> array)
                    names.AddRange(array.Select(e => $"{member.Name}{e.Name}"));

                names.AddRange(member.DataType.GetDeepMemberNames().Select(n => $"{member.Name}.{n}"));
            }

            return names;
        }

        /// <summary>
        /// Gets all nested dependent types for the provided data type.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>A collection of unique data types that the current type depends on.</returns>
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