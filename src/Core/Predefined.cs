using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
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

        public bool Equals(Predefined other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Members.SequenceEqual(other.Members);
        }

        public IDataType Create()
        {
            return New();
        }

        /// <summary>
        /// Return new instance of the current type. This will be used when creating tags for the specified type
        /// </summary>
        /// <returns></returns>
        protected abstract IDataType New();

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
            var fields = GetType().GetProperties().Where(p =>
                p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMember<>))).ToList();

            foreach (var member in fields.Select(p => (IMember<IDataType>) p.GetValue(this)))
                RegisterTypeMember(member);
        }
        
        protected void RegisterMemberFields()
        {
            var fields = GetType().GetFields().Where(f =>
                f.FieldType.IsGenericType &&
                f.FieldType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMember<>))).ToList();

            foreach (var member in fields.Select(p => (IMember<IDataType>) p.GetValue(this)))
                RegisterTypeMember(member);
        }

        protected void RegisterMember(IMember<IDataType> member) => RegisterTypeMember(member);

        private void RegisterTypeMember(IMember<IDataType> member)
        {
            if (_members.ContainsKey(member.Name))
                throw new ComponentNameCollisionException(member.Name, typeof(IMember<>));

            if (member.DataType.Equals(this))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{member.DataType.Name}'");
            
            _members.Add(member.Name, member);
        }
    }
}