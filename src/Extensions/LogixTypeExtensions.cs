using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extensions for various types implementing <see cref="ILogixType"/> interface.
    /// </summary>
    public static class LogixTypeExtensions
    {
        /// <summary>
        /// Returns all <see cref="Member"/> objects contained by the <see cref="ILogixType"/> object.
        /// </summary>
        /// <param name="logixType">The current logix type object.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> objects.</returns>
        /// <remarks>
        /// This is a helper to easily get either a <see cref="StructureType"/> member collection or an
        /// <see cref="ArrayType{TLogixType}"/> element collection, both which are collection of members and defined
        /// the type structure. Calling this for <see cref="AtomicType"/> will return and empty collection.
        /// </remarks>
        public static IEnumerable<Member> FindMembers(this ILogixType logixType)
        {
            return logixType switch
            {
                StructureType structureType => structureType.Members,
                ILogixArray<ILogixType> arrayType => arrayType.Elements,
                _ => Enumerable.Empty<Member>()
            };
        }

        /// <summary>
        /// Recursively searches the logix type structure for a descendent member with the specified tag name path.
        /// </summary>
        /// <param name="logixType">The current logix type object.</param>
        /// <param name="tagName">The tag name path of the member to retrieve.</param>
        /// <returns>A <see cref="Member"/> object if found; otherwise, null.</returns>
        public static Member? FindMember(this ILogixType logixType, TagName tagName)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(tagName))
                    return null;

                var memberName = tagName.First();

                var member = logixType.FindMembers().SingleOrDefault(m =>
                    string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

                if (member is null) return null;

                var remaining = TagName.Combine(tagName.Skip(1));
                if (remaining.IsEmpty) return member;

                logixType = member.DataType;
                tagName = remaining;
            }
        }

        /// <summary>
        /// Explicitly casts the current <see cref="ILogixType"/> to the specified derived type.
        /// </summary>
        /// <param name="logixType">The </param>
        /// <typeparam name="TLogixType"></typeparam>
        /// <returns></returns>
        public static TLogixType To<TLogixType>(this ILogixType logixType) where TLogixType : ILogixType =>
            (TLogixType)logixType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logixType"></param>
        /// <typeparam name="TLogixType"></typeparam>
        /// <returns></returns>
        public static TLogixType? As<TLogixType>(this ILogixType logixType) where TLogixType : class, ILogixType =>
            logixType as TLogixType;
        
        /// <summary>
        /// Returns a new <see cref="ArrayType{TLogixType}"/> of generic <see cref="ILogixType"/> objects.
        /// </summary>
        /// <returns>A <see cref="ArrayType{TLogixType}"/> of generic logix type objects.</returns>
        public static ArrayType<TLogixType> ToArrayType<TLogixType>(this IEnumerable<TLogixType> enumerable)
            where TLogixType : ILogixType
        {
            return new ArrayType<TLogixType>(enumerable.ToArray());
        }
    }
}