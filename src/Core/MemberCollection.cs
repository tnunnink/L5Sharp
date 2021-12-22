using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IMemberCollection{TMember}" />
    public sealed class MemberCollection<TMember> : ComponentCollection<TMember>, IMemberCollection<TMember>
        where TMember : IMember<IDataType>
    {
        private const char MemberSeparator = '.';

        /// <summary>
        /// Creates a new instance of a <c>IMembers</c> collection.
        /// </summary>
        /// <param name="members">
        /// A collection of <c>IMember</c> objects to initialize the <c>IMembers</c> collection with.
        /// </param>
        internal MemberCollection(IEnumerable<TMember>? members = null) : base(members)
        {
        }
        
        /// <inheritdoc />
        public IEnumerable<string> GetNames() => Collection.Keys;

        /// <inheritdoc />
        public TMember? DeepGet(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name not be null or empty.");

            return FindMemberByName(this, name);
        }

        /// <inheritdoc />
        public IEnumerable<TMember> DeepGetAll()
        {
            var members = new List<TMember>();

            foreach (var (_, member) in Collection)
            {
                members.Add(member);

                if (member.DataType is IComplexType complex)
                    members.AddRange((IEnumerable<TMember>)complex.Members.DeepGetAll());
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<string> DeepGetNames()
        {
            var names = new List<string>();

            foreach (var (name member) in Collection)
            {
                names.Add(name);

                if (member.DataType is IComplexType complex)
                    names.AddRange(complex.Members.GetNames().Select(n => $"{member.Name}.{n}"));
            }

            return names;
        }

        /// <inheritdoc />
        public IEnumerable<IDataType> GetDependentTypes()
        {
            var types = new List<IDataType>();

            foreach (var (_, member) in Collection)
            {
                types.Add(member.DataType);

                if (member.DataType is IComplexType complex)
                    types.AddRange(complex.Members.GetDependentTypes());
            }

            return types.Distinct();
        }

        /// <summary>
        /// Gets an <c>IMember</c> with the provided name from the current <c>IComplexType</c>
        /// </summary>
        /// <param name="members">The root collection of <c>IMember</c> objects to start traversal.</param>
        /// <param name="name">The full member path to the desired <c>IMember</c> instance.</param>
        /// <returns>
        /// If the member name is found in either immediate or nested members, then a <c>IMember</c>
        /// instance with the specified name; otherwise, null.
        /// </returns>
        private static TMember? FindMemberByName(IEnumerable<TMember> members, string name)
        {
            while (true)
            {
                var names = name.Split(MemberSeparator);
                var target = names.Last();
                var current = names.First();

                var member = members.SingleOrDefault(m => m.Name == current);

                if (member?.DataType is not IComplexType complexType || names.Length <= 1)
                    return member?.Name == target ? member : default;

                name = string.Join(MemberSeparator, names.Skip(1));
                members = (IEnumerable<TMember>)complexType.Members;
            }
        }
    }
}