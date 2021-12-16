using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IDataType"/>.
    /// </summary>
    public static class DataTypeExtensions
    {
        /// <summary>
        /// Indicates whether the current IDataType is <see cref="IAtomic"/>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>true if the current data type is IAtomic. Otherwise, false.</returns>
        public static bool IsAtomicType(this IDataType dataType)
        {
            return dataType is IAtomic;
        }
        
        /// <summary>
        /// Indicates whether the current IDataType is <see cref="IComplexType"/>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>true if the current data type is IComplexType. Otherwise, false.</returns>
        public static bool IsComplexType(this IDataType dataType)
        {
            return dataType is IComplexType;
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
        public static IMember<IDataType>? GetMember(this IDataType dataType, string name) =>
            dataType is IComplexType complexType ? complexType.Members.SingleOrDefault(m => m.Name == name) : null;

        /// <summary>
        /// Gets the collection of <c>Members</c> for the provided <c>DataType</c>.
        /// </summary>
        /// <param name="dataType">The current data type.</param>
        /// <returns>A collection of members for the provided data type if there are any, an empty collection if not.</returns>
        public static IEnumerable<IMember<IDataType>> GetMembers(this IDataType dataType) =>
            dataType is IComplexType complexType ? complexType.Members : Enumerable.Empty<IMember<IDataType>>();

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

                names.AddRange(member.Select(e => $"{member.Name}{e.Name}"));

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

        /// <summary>
        /// Creates an array member representing the current collection of data type instances.
        /// </summary>
        /// <param name="dataTypes">The current collection of data type instances to initialize the ArrayMember with.</param>
        /// <param name="name">The name of the ArrayMember.</param>
        /// <param name="dimensions">The dimensions of the ArrayMember.</param>
        /// <param name="radix">The Radix of the ArrayMember.</param>
        /// <param name="access">The ExternalAccess of the ArrayMember.</param>
        /// <param name="description">The Description of the ArrayMember.</param>
        /// <typeparam name="TDataType"></typeparam>
        /// <returns></returns>
        /*public static IMember<TDataType> ToArrayMember<TDataType>(this IEnumerable<TDataType> dataTypes,
            ComponentName name, Dimensions dimensions, Radix? radix = null, ExternalAccess? access = null,
            string? description = null) where TDataType : IDataType =>
            new Member<TDataType>(name, dataTypes.ToList(), dimensions, radix, access, description);*/

        public static bool StructureEquals(this IDataType target, IDataType source)
        {
            if (source == null) return false;
            if (ReferenceEquals(target, source)) return true;
            if (target.IsAtomicType() && source.IsAtomicType() && target.GetType() == source.GetType()) return true;
            if (target.IsAtomicType() || source.IsAtomicType()) return false;
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