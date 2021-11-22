using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Exceptions;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    internal sealed class UserDefinedMembers : IUserDefinedMembers
    {
        private readonly List<IMember<IDataType>> _members = new List<IMember<IDataType>>();
        private readonly IUserDefined _dataType;

        public UserDefinedMembers(IUserDefined userDefined, IEnumerable<IMember<IDataType>> members = null)
        {
            _dataType = userDefined ?? throw new ArgumentNullException(nameof(userDefined));
            AddRange(members);
        }

        public int Count => _members.Count;

        public bool Contains(ComponentName name)
        {
            return _members.Any(m => m.Name == name);
        }

        public IMember<IDataType> Get(ComponentName name)
        {
            return _members.Find(m => m.Name == name);
        }

        public void Add(IMember<IDataType> member)
        {
            if (member == null) return;

            if (member.DataType != null && member.DataType.Equals(_dataType))
                throw new CircularReferenceException(member.DataType);

            if (Contains(member.Name))
                throw new ComponentNameCollisionException(member.Name, typeof(IMember<IDataType>));

            _members.Add(member);
        }

        public void AddRange(IEnumerable<IMember<IDataType>> members)
        {
            if (members == null) return;

            foreach (var member in members)
                Add(member);
        }

        public void Update(IMember<IDataType> member)
        {
            if (member == null) return;

            if (member.DataType != null && member.DataType.Equals(_dataType))
                throw new CircularReferenceException(member.DataType);

            if (!Contains(member.Name))
                Add(member);

            var index = _members.FindIndex(m => m.Name == member.Name);
            _members[index] = member;
        }

        public void UpdateRange(IEnumerable<IMember<IDataType>> members)
        {
            if (members == null) return;

            foreach (var member in members)
                Update(member);
        }

        public void Insert(int index, IMember<IDataType> member)
        {
            if (member == null) return;

            if (member.DataType != null && member.DataType.Equals(_dataType))
                throw new CircularReferenceException(member.DataType);

            if (Contains(member.Name))
                throw new ComponentNameCollisionException(member.Name, typeof(IMember<IDataType>));

            _members.Insert(index, member);
        }

        public void Rename(ComponentName current, ComponentName name)
        {
            if (current == null) throw new ArgumentNullException(nameof(current));
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (Contains(name))
                throw new ComponentNameCollisionException(name, typeof(IMember<IDataType>));

            var index = _members.FindIndex(m => m.Name == current);

            if (index < 0) return;

            var member = _members[index];

            var renamed = member.Dimension.AreEmpty
                ? Member.Create(name, member.DataType, member.Radix, member.ExternalAccess, member.Description)
                : Member.Create(name, member.DataType, member.Dimension, member.Radix, member.ExternalAccess,
                    member.Description);

            _members[index] = renamed;
        }

        public void Remove(ComponentName name)
        {
            _members.RemoveAll(m => m.Name == name);
        }

        public IEnumerator<IMember<IDataType>> GetEnumerator()
        {
            return _members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}