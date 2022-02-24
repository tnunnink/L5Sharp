using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberCollection : IMemberCollection<IMember<IDataType>>
    {
        private readonly List<IMember<IDataType>> _members;
        private readonly IComplexType _parent;

        /// <summary>
        /// Creates a new <see cref="MemberCollection"/> with the provided parent <see cref="IComplexType"/> and optional members.
        /// </summary>
        /// <param name="parent">
        /// The root parent type of the member collection. Nested members can not reference this type.
        /// </param>
        /// <param name="members">A collection of members to initialize the collection with.</param>
        public MemberCollection(IComplexType parent, IEnumerable<IMember<IDataType>>? members = null)
        {
            _parent = parent;
            _members = members is not null ? new List<IMember<IDataType>>(members) : new List<IMember<IDataType>>();
        }


        /// <inheritdoc />
        public int Count => _members.Count;

        /// <inheritdoc />
        public void Add(IMember<IDataType> member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            if (_members.Any(m => m.Name == member.Name))
                throw new ComponentNameCollisionException(member.Name, member.GetType());

            if (member.DataType.Equals(_parent))
                throw new CircularReferenceException(_parent.Name);

            _members.Add(member);
        }

        /// <inheritdoc />
        public void Clear() => _members.Clear();

        /// <inheritdoc />
        public bool Contains(ComponentName name) => _members.Any(m => m.Name == name);

        /// <inheritdoc />
        public IMember<IDataType>? Find(Predicate<IMember<IDataType>> match) => _members.Find(match);

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> FindAll(Predicate<IMember<IDataType>> match) => _members.FindAll(match);

        /// <inheritdoc />
        public IMember<IDataType>? Get(ComponentName name) => _members.Find(m => m.Name == name);

        /// <inheritdoc />
        public void Insert(int index, IMember<IDataType> member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            if (_members.Any(m => m.Name == member.Name))
                throw new ComponentNameCollisionException(member.Name, member.GetType());

            if (member.DataType.Equals(_parent))
                throw new CircularReferenceException(_parent.Name);

            _members.Insert(index, member);
        }

        /// <inheritdoc />
        public bool Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            var member = _members.Find(m => m.Name == name);

            return member is not null && _members.Remove(member);
        }

        /// <inheritdoc />
        public void Update(IMember<IDataType> member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            if (member.DataType.Equals(_parent))
                throw new CircularReferenceException(_parent.Name);

            var index = _members.FindIndex(m => m.Name == member.Name);

            if (index < 0)
            {
                _members.Add(member);
                return;
            }

            _members[index] = member;
        }

        /// <inheritdoc />
        public void UpdateMany(IEnumerable<IMember<IDataType>> components)
        {
            if (components is null)
                throw new ArgumentNullException(nameof(components));

            foreach (var component in components)
                Update(component);
        }

        /// <inheritdoc />
        public IEnumerator<IMember<IDataType>> GetEnumerator() => _members.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}