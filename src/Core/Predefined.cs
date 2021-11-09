using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public abstract class Predefined : IPredefined, IEquatable<Predefined>
    {
        private readonly Dictionary<string, IMember<IDataType>> _members =
            new Dictionary<string, IMember<IDataType>>(StringComparer.OrdinalIgnoreCase);

        protected Predefined(string name)
        {
            Validate.Name(name);
            Name = name;
        }

        public string Name { get; }
        public virtual string Description => null;
        public Radix Radix => Radix.Null;
        public virtual DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Predefined;
        public virtual TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IEnumerable<IMember<IDataType>> Members => _members.Values.AsEnumerable();

        public IMember<IDataType> GetMember(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
        }

        public IMember<TType> GetMember<TType>(string name) where TType : IDataType
        {
            return _members.TryGetValue(name, out var member) ? member.As<TType>() : null;
        }

        public bool Equals(Predefined other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(Members, other.Members);
        }

        public virtual IDataType Instantiate()
        {
            return new Undefined();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Predefined)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_members, Name, Family);
        }

        public static bool operator ==(Predefined left, Predefined right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Predefined left, Predefined right)
        {
            return !Equals(left, right);
        }

        protected void RegisterMemberProperties()
        {
            var properties = GetType().GetProperties().Where(p =>
                p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMember<>))).ToList();

            foreach (var member in properties.Select(property => (IMember<IDataType>) property.GetValue(this)))
            {
                if (_members.ContainsKey(member.Name))
                    throw new ComponentNameCollisionException(member.Name, typeof(IMember<>));

                if (member.DataType.Equals(this))
                    throw new CircularReferenceException(
                        $"Member can not be same type as parent type '{member.DataType.Name}'");

                _members.Add(member.Name, member);
            }
        }

        protected void RegisterMember(IMember<IDataType> member)
        {
            _members.Add(member.Name, member);
        }
    }
}