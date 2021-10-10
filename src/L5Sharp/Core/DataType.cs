using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataType : IDataType, IEquatable<DataType>
    {
        private string _name;
        private string _description;
        private readonly Dictionary<string, Member> _members = new Dictionary<string, Member>();
        
        public DataType(string name, string description = null)
        {
            Name = name;
            Description = description ?? string.Empty;
        }

        public DataType(string name, Member member, string description = null) : this(name, description)
        {
            AddMemberComponent(member);
        }
        
        public DataType(string name, IEnumerable<Member> members, string description = null) : this(name, description)
        {
            foreach (var member in members)
                AddMemberComponent(member);
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                Validate.DataTypeName(value);
                _name = value;
            }
        }

        public DataTypeFamily Family => DataTypeFamily.None;

        public DataTypeClass Class => DataTypeClass.User;

        public bool IsAtomic => false;

        public object DefaultValue => null;

        public Radix DefaultRadix => Radix.Null;

        public TagDataFormat DataFormat => TagDataFormat.Decorated;

        public string Description
        {
            get => _description;
            set => _description =
                value ?? throw new ArgumentNullException(nameof(value), "Description can not be null");
        }

        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();

        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Null;
        }

        public bool IsValidValue(object value)
        {
            return value == null;
        }

        public Member GetMember(string name) => GetMemberByName(name);

        public IEnumerable<IDataType> GetDependentTypes() => GetUniqueMemberTypes(this);

        public IEnumerable<IDataType> GetDependentUserTypes() =>
            GetUniqueMemberTypes(this).Where(t => t.Class == DataTypeClass.User);

        public bool ContainsNullType() => GetUniqueMemberTypes(this).Any(t => t.Equals(Predefined.Undefined));

        public void AddMember(string name, IDataType dataType, string description = null,
            ushort dimension = 0, Radix radix = null, ExternalAccess access = null) =>
            AddMemberComponent(new Member(name, dataType, dimension, radix, access, description));

        public void RemoveMember(string name) => RemoveMemberComponent(GetMemberByName(name));

        public bool Equals(DataType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _name == other._name
                   && Equals(_members, other._members)
                   && Equals(Family, other.Family)
                   && Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DataType)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public static bool operator ==(DataType left, DataType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataType left, DataType right)
        {
            return !Equals(left, right);
        }
        
        /// <summary>
        /// Adds a member to the data type member collection
        /// </summary>
        /// <param name="member">The member to add</param>
        /// <exception cref="ArgumentNullException">Thrown when member is null</exception>
        private void AddMemberComponent(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member can nt be null");
            
            if (HasMemberName(member.Name))
                Throw.ComponentNameCollisionException(member.Name, typeof(Member));

            if (member.DataType.Equals(this))
                throw new CircularReferenceException(
                    $"Member can not have same type as parent type '{member.DataType.Name}'");

            _members.Add(member.Name, member);
        }

        /// <summary>
        /// Removes the member from the data type's member collection.
        /// </summary>
        /// <param name="member"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void RemoveMemberComponent(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member can nt be null");
            
            if (!HasMemberName(member.Name)) return;

            _members.Remove(member.Name);
        }

        /// <summary>
        /// Gets a single member by name from the type's member collection
        /// </summary>
        /// <param name="name">The name of the member to get</param>
        /// <returns></returns>
        private Member GetMemberByName(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
        }

        /// <summary>
        /// Determines if the members collection contains a member with the provided name
        /// </summary>
        /// <param name="name">The name of the member to find</param>
        /// <returns>True when a member with the provided name exists</returns>
        private bool HasMemberName(string name)
        {
            return _members.ContainsKey(name);
        }

        /// <summary>
        /// Recursively walks the member collections and find all unique data types of the structure
        /// </summary>
        /// <param name="dataType">The datatype to walk</param>
        /// <returns>An enumeration of all unique data types</returns>
        private static IEnumerable<IDataType> GetUniqueMemberTypes(IDataType dataType)
        {
            var types = new List<IDataType>();

            foreach (var member in dataType.Members)
            {
                types.Add(member.DataType);
                types.AddRange(GetUniqueMemberTypes(member.DataType));
            }

            return types.Distinct();
        }
    }
}