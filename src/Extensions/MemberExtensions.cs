using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Utilities;

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

        /// <summary>
        /// Determines if the current member is a dimensionless value type
        /// </summary>
        /// <param name="member">The current member.</param>
        /// <returns>
        /// true if the current member data type is <see cref="IAtomic"/> and the member dimensions are empty.
        /// Otherwise, false.
        /// </returns>
        public static bool IsValueMember<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.DataType is IAtomic && member.Dimensions.AreEmpty;
        }
        
        /// <summary>
        /// Determines if the current member is a dimensionless complex type .
        /// </summary>
        /// <param name="member">The current member.</param>
        /// <returns>
        /// true if the current member data type is <see cref="IComplexType"/> and the member dimensions are empty.
        /// Otherwise, false.
        /// </returns>
        public static bool IsStructureMember<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.DataType is IComplexType && member.Dimensions.AreEmpty;
        }

        /// <summary>
        /// Determines if the current member is an array.
        /// </summary>
        /// <param name="member">The current member.</param>
        /// <returns>
        /// true if the current member dimensions are not empty. Otherwise, false.
        /// </returns>
        public static bool IsArrayMember<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return !member.Dimensions.AreEmpty;
        }

        /// <summary>
        /// Gets a member serializer based on the current member data type and dimensions.
        /// </summary>
        /// <param name="member">The current member</param>
        /// <returns>A new IXSerializer for a generic member.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the serializer could not be determined.</exception>
        internal static string GetDataElementName(this IMember<IDataType> member)
        {
            if (member.IsValueMember())
                return LogixNames.DataValueMember;

            if (member.IsArrayMember())
                return LogixNames.ArrayMember;

            if (member.IsStructureMember())
                return LogixNames.Structure;

            throw new InvalidOperationException(
                $"Could not determine member name for '{member.GetType()}'");
        }
    }
}