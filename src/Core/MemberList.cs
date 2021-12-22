using System;
using System.Collections.Generic;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IMemberList{TMember}" />
    public sealed class MemberList<TMember> : MemberCollection<TMember>, IMemberList<TMember>
        where TMember : IMember<IDataType>
    {
        private readonly IComplexType _parent;

        /// <summary>
        /// Creates a new instance of a <c>MemberList</c> collection with the provided arguments.
        /// </summary>
        /// <param name="parent">
        /// The root parent type of the member collection. Nested members can not reference this type.
        /// </param>
        /// <param name="members">A collection of members to initialize the collection with.</param>
        internal MemberList(IComplexType parent, IEnumerable<TMember>? members = null) : base(members)
        {
            _parent = parent;
        }

        /// <summary>
        /// Clears all <c>IMember</c> objects from the current collection.
        /// </summary>
        public void Clear()
        {
            Collection.Clear();
        }

        /// <summary>
        /// Adds a <c>IMember</c> to the current collection.
        /// </summary>
        /// <param name="member">The <c>IMember</c> to add.</param>
        /// <exception cref="ArgumentNullException">When member is null.</exception>
        /// <exception cref="ComponentNameCollisionException">When the member name already exists in the collection.</exception>
        /// <exception cref="CircularReferenceException">When the member <c>DataType</c> references the parent type.</exception>
        public void Add(TMember member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            if (Collection.ContainsKey(member.Name))
                throw new ComponentNameCollisionException(member.Name, member.GetType());

            if (member.DataType.Equals(_parent))
                throw new CircularReferenceException(_parent);

            Collection.Add(member.Name, member);
        }

        /// <summary>
        /// Adds the provided collection of <c>IMember</c> objects to the current collection.
        /// </summary>
        /// <param name="members">The collection of <c>IMember</c> objects to add to the collection.</param>
        /// <exception cref="ArgumentNullException">When member is null.</exception>
        /// <exception cref="ComponentNameCollisionException">When the member name already exists in the collection.</exception>
        /// <exception cref="CircularReferenceException">When the member data type property references the parent type.</exception>
        public void AddRange(IEnumerable<TMember> members)
        {
            if (members is null)
                throw new ArgumentNullException(nameof(members));

            foreach (var member in members)
            {
                if (Collection.ContainsKey(member.Name))
                    throw new ComponentNameCollisionException(member.Name, member.GetType());

                if (member.DataType.Equals(_parent))
                    throw new CircularReferenceException(_parent);

                Collection.Add(member.Name, member);
            }
        }

        /// <summary>
        /// Updates the provided <c>IMember</c> on the current collection.
        /// </summary>
        /// <remarks>
        /// If the provided <c>IMember</c> does not exist, then it will be added to the collection.
        /// If it does exist, then it will be replaced.
        /// </remarks>
        /// <param name="member">The <c>IMember</c> to update on the collection.</param>
        /// <exception cref="ArgumentNullException">When member is null.</exception>
        /// <exception cref="CircularReferenceException">When the member data type property references the parent type.</exception>
        public void Update(TMember member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            if (!Collection.ContainsKey(member.Name))
                Add(member);

            if (member.DataType.Equals(_parent))
                throw new CircularReferenceException(_parent);

            Collection[member.Name] = member;
        }

        /// <summary>
        /// Removes a <c>IMember</c> with the provided name from the collection.
        /// </summary>
        /// <param name="name">The name of the <c>IMember</c> to remove from the collection.</param>
        /// <exception cref="ArgumentException">When name is null or empty.</exception>
        public void Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name can not be null or empty");

            if (!Collection.ContainsKey(name))
                return;

            Collection.Remove(name);
        }
    }
}