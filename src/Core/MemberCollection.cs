using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IMemberCollection{TMember}" />
    public class MemberCollection<TMember> : ComponentCollection<TMember>, IMemberCollection<TMember>
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
        public bool DeepContains(string? name) => name is not null && FindMemberByName(this, name) is not null;

        /// <inheritdoc />
        public TMember? DeepGet(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            
            if (name.IsEmpty())
                throw new ArgumentException("Name not be empty.");

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
        public IEnumerable<string> DeepNames()
        {
            var names = new List<string>();

            foreach (var (name, member) in Collection)
            {
                names.Add(name);

                if (member.DataType is IComplexType complex)
                    names.AddRange(complex.Members.DeepNames().Select(n => $"{member.Name}.{n}"));
            }

            return names;
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