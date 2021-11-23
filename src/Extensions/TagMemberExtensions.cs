﻿using System;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ITagMember{TDataType}"/>.
    /// </summary>
    public static class TagMemberExtensions
    {
        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> is a dimensionless value type
        /// </summary>
        /// <param name="member">The current member to inspect</param>
        /// <returns>
        /// <b>True</b> if the current member data type is <see cref="IAtomic"/> and the member dimensions are empty.
        /// Otherwise, <b>True</b>/>
        /// </returns>
        public static bool IsValueMember<TDataType>(this ITagMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.GetTypeClass() == DataTypeClass.Atomic && member.Dimensions.AreEmpty;
        }

        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> is a dimensionless complex type 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsStructureMember<TDataType>(this ITagMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.GetTypeClass() != DataTypeClass.Atomic && member.Dimensions.AreEmpty;
        }

        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> is an array of value types.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsArrayMember<TDataType>(this ITagMember<TDataType> member)
            where TDataType : IDataType
        {
            return !member.Dimensions.AreEmpty;
        }

        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> and element of an array.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsArrayElement<TDataType>(this ITagMember<TDataType> member)
            where TDataType : IDataType
        {
            return !member.Parent.Dimensions.AreEmpty;
        }

        /// <summary>
        /// Gets the <see cref="DataTypeClass"/> of the current <c>TagMember</c>.
        /// </summary>
        /// <returns>A <c>DataTypeClass</c> value representing the type of the generic tag.</returns>
        public static DataTypeClass GetTypeClass<TDataType>(this ITagMember<TDataType> tagMember)
            where TDataType : IDataType
        {
            var type = tagMember.GetType().GetGenericArguments()[0];

            if (typeof(IAtomic).IsAssignableFrom(type))
                return DataTypeClass.Atomic;

            if (typeof(IUserDefined).IsAssignableFrom(type))
                return DataTypeClass.User;

            if (typeof(IPredefined).IsAssignableFrom(type))
                return DataTypeClass.Predefined;

            if (typeof(IAddOnDefined).IsAssignableFrom(type))
                return DataTypeClass.AddOnDefined;

            return typeof(IModuleDefined).IsAssignableFrom(type) ? DataTypeClass.Io : null;
        }

        public static void SetData<TDataType>(this ITagMember<TDataType> target, ITagMember<TDataType> source)
            where TDataType : IDataType
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target), "The target tag can not be null");

            if (source == null)
                throw new ArgumentNullException(nameof(source), "The source tag can not be null");

            var targetData = target.GetData();
            var sourceData = source.GetData();

            if (!targetData.StructureEquals(sourceData))
                throw new InvalidTagDataException(targetData, sourceData);

            if (targetData is IAtomic atomic && target.Dimensions.AreEmpty)
            {
                atomic.SetValue(sourceData);
                return;
            }

            foreach (var targetMember in target.GetMembers())
            {
                var sourceMember = source[targetMember.Name];
                targetMember.SetData(sourceMember);
            }
        }
    }
}