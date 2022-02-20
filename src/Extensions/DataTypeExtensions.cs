using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Comparers;
using L5Sharp.Core;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A static class containing extension methods for <see cref="IDataType"/>.
    /// </summary>
    public static class DataTypeExtensions
    {
        /// <summary>
        /// Gets a <see cref="IMember{TDataType}"/> with the specified member name from the <see cref="IDataType"/>
        /// if it exists.
        /// </summary>
        /// <param name="dataType">The current <see cref="IDataType"/> instance.</param>
        /// <param name="name">The name of the <see cref="IMember{TDataType}"/> that is a child of the current type.</param>
        /// <returns>A <see cref="IMember{TDataType}"/> instance that represents the child</returns>
        public static IMember<IDataType>? GetMember(this IDataType dataType, string name) =>
            dataType.GetMembers().FirstOrDefault(m => m.Name == name);

        /// <summary>
        /// Gets the collection of immediate child members for the current <see cref="IDataType"/> if any exist.
        /// </summary>
        /// <param name="dataType">The current <see cref="IDataType"/> instance.</param>
        /// <returns>A collection of <see cref="IMember{TDataType}"/> objects that represent the immediate child
        /// members of the type.</returns>
        public static IEnumerable<IMember<IDataType>> GetMembers(this IDataType dataType)
        {
            return dataType switch
            {
                IComplexType complexType => complexType.Members,
                IArrayType<IDataType> arrayType => arrayType.AsEnumerable(),
                _ => Enumerable.Empty<IMember<IDataType>>()
            };
        }

        /// <summary>
        /// Gets a collection of members that represent the sequence of members to a specific descendant member.
        /// </summary>
        /// <param name="dataType">The current <see cref="IDataType"/> instance.</param>
        /// <param name="tagName">The <see cref="TagName"/> value that represents a path to a descendant member.</param>
        /// <returns>
        /// If all members of the provided tag name exist as a descendants of the current data type, a sequence of
        /// <see cref="IMember{TDataType}"/> that (in order) represent the instances of each member in the provided
        /// tag name value. If the tag name does not represent a valid descendant, then an empty collection of
        /// <see cref="IMember{TDataType}"/>.
        /// </returns>
        /// <exception cref="ArgumentException">tagName is null or empty.</exception>
        public static IEnumerable<IMember<IDataType>> GetMembersTo(this IDataType dataType, TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var results = new List<IMember<IDataType>>();

            var members = dataType.GetMembers().ToList();

            foreach (var memberName in tagName.Members)
            {
                var member = members.Find(m => m.Name == memberName);

                if (member is null) break;

                results.Add(member);
                members = member.DataType.GetMembers().ToList();
            }

            return results.Count == tagName.Members.Count() ? results : Enumerable.Empty<IMember<IDataType>>();
        }

        /// <summary>
        /// Gets a collection of <see cref="TagName"/> values that represent all descendant member names of the <see cref="IDataType"/>.
        /// </summary>
        /// <param name="dataType">The current <see cref="IDataType"/> instance.</param>
        /// <returns>A collection of <see cref="TagName"/> values that represent all the descendent members of the
        /// current type.</returns>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find all tag names.
        /// </remarks>
        public static IEnumerable<TagName> GetTagNames(this IDataType dataType)
        {
            var names = new List<TagName>();

            foreach (var member in dataType.GetMembers())
            {
                names.Add(member.Name);
                names.AddRange(member.DataType.GetTagNames().Select(m => TagName.Combine(member.Name, m)));
            }

            return names;
        }

        /// <summary>
        /// Gets a collection of unique <see cref="IDataType"/> instances that represent all the data types the
        /// current type is dependent on. 
        /// </summary>
        /// <param name="dataType">The current <see cref="IDataType"/> instance.</param>
        /// <returns>
        /// A unique collection of <see cref="IDataType"/> instances if any dependents are found;
        /// otherwise; an empty collection.
        /// </returns>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find all tag names.
        /// </remarks>
        public static IEnumerable<IDataType> GetDependentTypes(this IDataType dataType)
        {
            var set = new HashSet<IDataType>(ComponentNameComparer.Instance);

            foreach (var member in dataType.GetMembers())
            {
                set.Add(member.DataType);
                set.UnionWith(member.DataType.GetDependentTypes());
            }

            return set;
        }

        /// <summary>
        /// Determines if a member with the provided <see cref="TagName"/> exists as a descendant member of the
        /// current <see cref="IDataType"/>.
        /// </summary>
        /// <param name="dataType">The current <see cref="IDataType"/> instance.</param>
        /// <param name="tagName">The <see cref="TagName"/> of the descendant member to find.</param>
        /// <returns>true if a <see cref="IMember{TDataType}"/> with the specified name is contained in the nested
        /// hierarchy of the current <see cref="IDataType"/>; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">tagName is null.</exception>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find all tag names.
        /// </remarks>
        public static bool HasMember(this IDataType dataType, TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var members = dataType.GetMembers().ToList();

            foreach (var memberName in tagName.Members)
            {
                var member = members.Find(m => m.Name == memberName);

                if (member is null)
                    return false;

                members = member.DataType.GetMembers().ToList();
            }

            return true;
        }

        /// <summary>
        /// Recursively sets the values for all atomic type members of the target data type instance with the data
        /// from the source data type instance.
        /// </summary>
        /// <param name="target">The target data type for which data will be written.</param>
        /// <param name="source">The source data type for which the data will be read.</param>
        /// <remarks>
        /// <para>
        /// This method will traverse the data type member hierarchy and set the value of all atomic members of the target
        /// type with the data of the provided source type. This traversal will be in order of the members retrieved by the
        /// <see cref="GetMembers"/> method, which is to say in the order that the members exists for each data type instance.
        /// This means that setting the data for different data types may not necessarily fail, but simply write values
        /// to "non-matching" members of the data type structure.
        /// </para>
        /// <para>
        /// If the data types are not both <see cref="IAtomicType"/>, no state/value will be affected (as none exists).
        /// If the both data types are <see cref="IAtomicType"/>, then the method will call <see cref="IAtomicType.SetValue"/>.
        /// </para>
        /// </remarks>
        public static void SetData(this IDataType target, IDataType source)
        {
            if (target is IAtomicType targetValue && source is IAtomicType sourceValue)
                targetValue.SetValue(sourceValue.Value);

            var memberPairs = target.GetMembers()
                .Zip(source.GetMembers(), (x, y) => new { First = x.DataType, Second = y.DataType });

            foreach (var pair in memberPairs)
                SetData(pair.First, pair.Second);
        }

        /// <summary>
        /// Determines if two <see cref="IDataType"/> instances have an equal structure.
        /// </summary>
        /// <param name="dataType">The current <see cref="IDataType"/> instance.</param>
        /// <param name="other">The other <see cref="IDataType"/> to compare.</param>
        /// <returns>true if both <see cref="IDataType"/> objects have equal structure; otherwise, false.</returns>
        /// <remarks>
        /// An equivalent structure means both <see cref="IDataType"/> instances have equal names and all descendent members
        /// have equal name, data type, and dimensions. Each descendent member will in turn call
        /// <see cref="StructureEquals"/> on it's own type. This means that the entire hierarchical type structure is
        /// compared for equality. 
        /// </remarks>
        public static bool StructureEquals(this IDataType dataType, IDataType other)
        {
            var comparer = DataTypeStructureComparer.Instance;
            return comparer.Equals(dataType, other);
        }
    }
}