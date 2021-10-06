using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataType : IDataType, IEquatable<DataType>
    {
        private string _name;
        private string _description;
        private readonly Dictionary<Member, List<Member>> _backingMembers = new Dictionary<Member, List<Member>>();
        private readonly Dictionary<string, Member> _members = new Dictionary<string, Member>();

        public DataType(string name, string description = null, IEnumerable<Member> members = null)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            Name = name;
            Description = description ?? string.Empty;

            if (members == null) return;
            foreach (var member in members)
            {
                member.NameChanged += MemberOnNameChanged;
                _members.Add(member.Name, member);
            }
                
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
            set => _description = value ?? string.Empty;
        }

        public IEnumerable<IMember> Members => _members.Values.Where(m => !m.Hidden).AsEnumerable();

        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Null;
        }

        public Member GetMember(string name)
        {
            return _members.ContainsKey(name) ? _members[name] : null;
        }

        public IEnumerable<IDataType> GetDependentTypes()
        {
            return GetUniqueMemberTypes(this);
        }

        public IEnumerable<IDataType> GetDependentUserTypes()
        {
            return GetUniqueMemberTypes(this).Where(t => t.Class == DataTypeClass.User);
        }

        public void AddMember(Member member) => AddMemberInternal(member);

        public void AddMembers(IEnumerable<Member> members)
        {
            if (members == null) throw new ArgumentNullException(nameof(members));

            foreach (var member in members)
                AddMemberInternal(member);
        }

        public void AddMember(string name, IDataType dataType, string description = null,
            ushort dimension = 0, Radix radix = null, ExternalAccess access = null)
        {
            var member = new Member(name, dataType, dimension, radix, access, description);
            AddMemberInternal(member);
        }

        public void AddMember(string name, IDataType dataType, Action<IMemberBuilder> builder)
        {
            var memberBuilder = new MemberBuilder(name, dataType);
            builder(memberBuilder);

            var member = memberBuilder.Build();

            AddMemberInternal(member);
        }

        public void UpdateMember(string name, Action<IMemberBuilder> builder)
        {
            if (!_members.ContainsKey(name))
                Throw.ComponentNotFoundException(name, typeof(Member));

            var current = _members[name];
            var memberBuilder = new MemberBuilder(current.Name, current.DataType);
            builder(memberBuilder);

            var member = memberBuilder.Build();

            if (member.DataType.Equals(Bool))
                GenerateBitBackingMember(member);

            _members[name] = member;
        }

        public void RemoveMember(string name)
        {
            if (!_members.ContainsKey(name)) return;

            var member = _members[name];

            if (member.DataType.Equals(Bool))
            {
                var backing = Members.SingleOrDefault(m => m.Name == member.Target);
                if (backing != null)
                    _members.Remove(backing.Name);
            }

            _members.Remove(name);
        }

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

        private void AddMemberInternal(Member member)
        {
            if (Family == DataTypeFamily.String)
                Throw.NotConfigurableException(nameof(Members), nameof(Member),
                    "Can not configure members of string family");

            if (_members.ContainsKey(member.Name))
                Throw.ComponentNameCollisionException(member.Name, typeof(Member));

            if (member.DataType.Equals(Bool))
                GenerateBitBackingMember(member);

            _members.Add(member.Name, member);
        }

        private void GenerateBitBackingMember(Member member)
        {
            if (!member.DataType.Equals(Bool)) return;

            // ReSharper disable once StringLiteralTypo
            // All backing members have the appended string to prevent bit overlay error. 
            const string memberPrefix = "ZZZZZZZZZZ";

            // All backing members have a prefix number that is the position of the member in the collection.
            var memberIndex = (ushort)_members.Count;

            //All backing members follow this naming convention and are of type Sint and are hidden
            var backingMember = new Member($"{memberPrefix}{Name}{memberIndex}", Sint, 0,
                Radix.Decimal, ExternalAccess.ReadWrite, string.Empty, true, string.Empty, 0);

            _members.Add(backingMember.Name, backingMember);

            member.Target = backingMember.Name;
        }

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

        private void MemberOnNameChanged(object sender, NameChangedEventArgs e)
        {
            if (_members.ContainsKey(e.OldName)) return;

            var member = _members[e.OldName];

            _members.Remove(e.OldName);
            _members.Add(member.Name, member);
        }
    }
}