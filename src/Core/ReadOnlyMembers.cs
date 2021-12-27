using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ReadOnlyMembers<TMember> : IReadOnlyMembers<TMember> where TMember : IMember<IDataType>
    {
        private const char MemberSeparator = '.';
        protected List<TMember> Members = new();

        /// <summary>
        /// Creates a new instance of a <c>IMembers</c> collection.
        /// </summary>
        /// <param name="members">
        /// A collection of <c>IMember</c> objects to initialize the <c>IMembers</c> collection with.
        /// </param>
        public ReadOnlyMembers(IEnumerable<TMember>? members = null)
        {
            if (members is not null)
                RegisterMembers(members);
        }

        public int Count { get; }

        /// <inheritdoc />
        public bool Contains(string? name) => name is not null && FindMemberByName(this, name) is not null;

        /// <inheritdoc />
        public TMember? GetMember(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            
            if (name.IsEmpty())
                throw new ArgumentException("Name not be empty.");

            return FindMemberByName(this, name);
        }

        /// <inheritdoc />
        public IEnumerable<TMember> GetMembers()
        {
            var members = new List<TMember>();

            foreach (var member in Members)
            {
                members.Add(member);

                if (member.DataType is IComplexType complex)
                    members.AddRange((IEnumerable<TMember>)complex.Members.GetMembers());
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<string> GetNames()
        {
            var names = new List<string>();

            foreach (var member in Members)
            {
                names.Add(member.Name);

                if (member.DataType is IComplexType complex)
                    names.AddRange(complex.Members.GetNames().Select(n => $"{member.Name}.{n}"));
            }

            return names;
        }
        
        /// <summary>
        /// Initializes the base collection with the provided components collection.
        /// </summary>
        /// <param name="members">A collection of components to initialize the collection with.</param>
        /// <exception cref="ArgumentNullException">If the provided component is null.</exception>
        /// <exception cref="ComponentNameCollisionException">If a duplicate name is encountered.</exception>
        private void RegisterMembers(IEnumerable<TMember> members)
        {
            foreach (var member in members)
            {
                if (member is null)
                    throw new ArgumentNullException(nameof(member));

                if (Members.Any(m => m.Name == member.Name))
                    throw new ComponentNameCollisionException(member.Name, member.GetType());

                Members.Add(member);
            }
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

        /// <inheritdoc />
        public IEnumerator<TMember> GetEnumerator()
        {
            return Members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}