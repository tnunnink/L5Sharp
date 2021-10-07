using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataType : IDataType, IEquatable<DataType>
    {
        // ReSharper disable once StringLiteralTypo This is the prefix for boolean backing members
        private const string BackingMemberPrefix = "ZZZZZZZZZZ";
        private string _name;
        private string _description;
        private readonly List<Member> _backing = new List<Member>();
        private readonly List<Member> _members = new List<Member>();
        
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

        public IEnumerable<IMember> Members => _members.Where(m => !m.Hidden).AsEnumerable();

        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Null;
        }

        public bool ContainsMember(string name) => HasMemberName(name);

        public Member GetMember(string name)
        {
            return _members.SingleOrDefault(m => m.Name == name);
        }

        public IEnumerable<IDataType> GetDependentTypes()
        {
            return GetUniqueMemberTypes(this);
        }

        public IEnumerable<IDataType> GetDependentUserTypes()
        {
            return GetUniqueMemberTypes(this).Where(t => t.Class == DataTypeClass.User);
        }

        public void AddMember(Member member) => AddMemberComponent(member);
        
        public void AddMember(string name, IDataType dataType, string description = null,
            ushort dimension = 0, Radix radix = null, ExternalAccess access = null) =>
            AddMemberComponent(new Member(name, dataType, dimension, radix, access, description));
        
        public void AddMembers(IEnumerable<Member> members) => members.ToList().ForEach(AddMemberComponent);
        
        public void RemoveMember(Member member) => RemoveMemberComponent(member);
        
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

        public static IDataType Null => Predefined.Null;

        public static IDataType Bool => Predefined.Bool;

        public static IDataType Sint => Predefined.Sint;

        public static IDataType Int => Predefined.Int;

        public static IDataType Dint => Predefined.Dint;

        public static IDataType Lint => Predefined.Lint;

        public static IDataType Real => Predefined.Real;

        public static IDataType String => Predefined.String;

        public static IDataType Timer => Predefined.Timer;

        public static IDataType Counter => Predefined.Counter;

        /// <summary>
        /// Gets a enumeration of all members both public and backing for use in serialization
        /// </summary>
        /// <returns>Enumeration of all data type members</returns>
        /// <remarks>
        /// Serializer needs access to backing members so that if can correctly compose the L5X structure
        /// </remarks>
        internal IEnumerable<Member> GetAllMembers()
        {
            var members = new List<Member>();
            
            for (var i = 0; i < _members.Count + _backing.Count; i++)
            {
                members.Add(_backing.SingleOrDefault(b => b.Name.EndsWith(i.ToString())) != null
                    ? _backing[i]
                    : _members[i]);
            }

            return members;
        }

        /// <summary>
        /// Adds the member to the data type's member collection. Will also call registration of backing members.
        /// </summary>
        /// <param name="member">The member to add</param>
        /// <exception cref="ArgumentNullException">Thrown when member is null</exception>
        private void AddMemberComponent(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member can nt be null");
            
            if (HasMemberName(member.Name))
                Throw.ComponentNameCollisionException(member.Name, typeof(Member));

            member.PropertyChanged += OnMemberPropertyChanged;
            
            _members.Add(member);
            
            RegisterBackingMembers();
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

            member.PropertyChanged -= OnMemberPropertyChanged;
            
            _members.Remove(member);
            
            RegisterBackingMembers();
        }

        /// <summary>
        /// Re-registers the backing members of the type. Called When member property changed event fires
        /// </summary>
        /// <param name="sender">The member object tat fired the event</param>
        /// <param name="e">The property changed event arguments</param>
        private void OnMemberPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RegisterBackingMembers();
        }

        /// <summary>
        /// Iterates the member collection and creates and "backing" members that are need by boolean types.
        /// </summary>
        private void RegisterBackingMembers()
        {
            _backing.Clear();
            
            foreach (var member in _members)
                RegisterBackingMember(member);
        }

        /// <summary>
        /// Assigns the backing member to the target of a boolean member.
        /// Will create the backing member if it does not exists. Otherwise will retrieve and set target and bit number
        /// This functionality is based on how Logix allocates bool members to conserve memory. When we serialize or
        /// deserialize we need to account for these members. 
        /// </summary>
        /// <param name="member"></param>
        /// <remarks>
        /// Backing members appear to be named using the following convention:
        /// {MemberPrefix}{DataTypeName}{IndexOfMember}
        /// </remarks>
        private void RegisterBackingMember(Member member)
        {
            if (!member.DataType.Equals(Predefined.Bool)) return;

            var index = _members.IndexOf(member);
            var previous = index - 1 >= 0 ? _members[index - 1] : null;
            
            if (!string.IsNullOrEmpty(previous?.Target) && previous.BitNumber < 7)
            {
                var target = _backing.SingleOrDefault(x => x.Name == previous.Target);
                member.Target = target?.Name;
                member.BitNumber = (ushort)(previous.BitNumber + 1);
                return;
            }
            
            //The backing member name only uses the first 10 characters of the data type name
            var memberIndex = index + _backing.Count;
            var backingName = Name.Length > 10 ? Name[..10] : Name;
            
            var backing = new Member($"{BackingMemberPrefix}{backingName}{memberIndex}", Sint, 0,
                Radix.Decimal, ExternalAccess.ReadWrite, string.Empty, true, string.Empty, 0);
            
            _backing.Add(backing);

            member.Target = backing.Name;
            member.BitNumber = 0;
        }

        /// <summary>
        /// Gets a single member by name from the type's member collection
        /// </summary>
        /// <param name="name">The name of the member to get</param>
        /// <returns></returns>
        private Member GetMemberByName(string name)
        {
            return _members.SingleOrDefault(m => m.Name == name);
        }

        /// <summary>
        /// Determines if the members collection contains a member with the provided name
        /// </summary>
        /// <param name="name">The name of the member to find</param>
        /// <returns>True when a member with the provided name exists</returns>
        private bool HasMemberName(string name)
        {
            return _members.Any(m => m.Name == name);
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