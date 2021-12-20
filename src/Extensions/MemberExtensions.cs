using System;
using L5Sharp.Common;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IMember{TDataType}"/>.
    /// </summary>
    public static class MemberExtensions
    {
        /*/// <summary>
        /// Gets member names of the given <c>DataType</c>.
        /// </summary>
        /// <param name="member">The current data type.</param>
        /// <returns>A collection of string names that represent the full member name path from the root data type.</returns>
        public static IEnumerable<string> GetMemberNames<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return !member.IsArray() ? member.DataType.GetMemberNames() : member.Select(element => $"{element.Name}");
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

            if (!member.IsArray())
                return member.DataType.GetDeepMemberNames();

            foreach (var element in member)
            {
                names.Add($"{element.Name}");
                names.AddRange(element.DataType.GetDeepMemberNames());
            }

            return names;
        }*/

        /// <summary>
        /// Determines if the current Member is a dimensionless value type.
        /// </summary>
        /// <param name="member">The current Member.</param>
        /// <returns>
        /// true if the current Member's DataType property implements <see cref="IAtomicType"/> and the member Dimensions
        /// are empty. Otherwise, false.
        /// </returns>
        public static bool IsValue<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.DataType is IAtomicType && member.Dimension.AreEmpty;
        }
        
        /// <summary>
        /// Determines if the current Member is a dimensionless complex type.
        /// </summary>
        /// <param name="member">The current Member.</param>
        /// <returns>
        /// true if the current Member's DataType property implements <see cref="IComplexType"/> and the member
        /// Dimensions are empty. Otherwise, false.
        /// </returns>
        public static bool IsStructure<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.DataType is IComplexType && member.Dimension.AreEmpty;
        }

        /// <summary>
        /// Determines if the current Member is an array.
        /// </summary>
        /// <param name="member">The current Member.</param>
        /// <returns>
        /// true if the current Member's Dimensions property are not empty. Otherwise, false.
        /// </returns>
        public static bool IsArray<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return !member.Dimension.AreEmpty;
        }

        /// <summary>
        /// Gets a member serializer based on the current member data type and dimensions.
        /// </summary>
        /// <param name="member">The current member</param>
        /// <returns>A new IXSerializer for a generic member.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the serializer could not be determined.</exception>
        internal static string GetDataElementName(this IMember<IDataType> member)
        {
            if (member.IsValue())
                return LogixNames.DataValueMember;

            if (member.IsArray())
                return LogixNames.ArrayMember;

            if (member.IsStructure())
                return LogixNames.Structure;

            throw new InvalidOperationException(
                $"Could not determine member name for '{member.GetType()}'");
        }
    }
}