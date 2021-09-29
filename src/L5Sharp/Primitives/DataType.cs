using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class DataType : IDataType, IEquatable<DataType>, IXSerializable
    {
        private static readonly string[] StringMemberNames = { "LEN", "DATA" };
        private string _name;
        private readonly Dictionary<string, Member> _members = new Dictionary<string, Member>();

        private DataType(string name, DataTypeFamily family, string description = null, List<Member> members = null)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (family == null) throw new ArgumentNullException(nameof(family));

            Name = name;
            Family = family;
            Description = description ?? string.Empty;

            if (members == null) return;
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        private DataType(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            Family = DataTypeFamily.FromName(element.Attribute(nameof(Family))?.Value);
            Description = element.Element(nameof(Description))?.Value;
            var members = element.Element(nameof(Members))?.Descendants().Select(Member.Materialize);
            if (members == null) return;
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        public DataType(string name, string description = null)
            : this(name, DataTypeFamily.None, description)
        {
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
        public DataTypeFamily Family { get; }
        public DataTypeClass Class => DataTypeClass.User;
        public bool IsAtomic => false;
        public string Description { get; set; }

        public IEnumerable<IMember> Members => _members.Values.Where(m => !m.Hidden).AsEnumerable();

        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Null;
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(DataType));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(Family), Family));
            element.Add(new XAttribute(nameof(Class), Class));

            if (!string.IsNullOrEmpty(Description))
                element.Add(new XElement(nameof(Description), new XCData(Description)));

            element.Add(new XElement(nameof(Members), _members.Values.Select(m => m.Serialize())));
            return element;
        }

        public static DataType Materialize(XElement element)
        {
            return new DataType(element);
        }

        public static DataType StringType(string name, ushort length, string description = null)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be greater 0");

            return new DataType(name, DataTypeFamily.String, description, GenerateStringMembers(length));
        }

        public Member GetMember(string name)
        {
            return _members.ContainsKey(name) ? _members[name] : null;
        }

        public void AddMember(string name, IDataType dataType, string description = null,
            Dimensions dimension = null, Radix radix = null, ExternalAccess access = null)
        {
            if (Family == DataTypeFamily.String)
                Throw.NotConfigurableException(nameof(Members), nameof(Member),
                    "Can not configure members of string family");
            
            if (_members.ContainsKey(name))
                Throw.NameCollisionException(name, typeof(Member));

            var member = new Member(name, dataType, dimension, radix, access, description: description);

            if (dataType.Equals(Bool))
                GenerateBitBackingMember(member);

            _members.Add(member.Name, member);
        }

        public void AddMember(string name, IDataType dataType, Action<IMemberBuilder> builder)
        {
            if (Family == DataTypeFamily.String)
                Throw.NotConfigurableException(nameof(Members), nameof(Member),
                    "Can not configure members of string family");
            
            if (_members.ContainsKey(name))
                Throw.NameCollisionException(name, typeof(Member));

            var memberBuilder = new MemberBuilder(name, dataType);
            builder(memberBuilder);

            var member = memberBuilder.Build();

            if (dataType.Equals(Bool))
                GenerateBitBackingMember(member);

            _members.Add(member.Name, member);
        }

        public void UpdateMember(string name, Action<IMemberBuilder> builder)
        {
            if (Family == DataTypeFamily.String)
                Throw.NotConfigurableException(nameof(Members), nameof(Member),
                    "Can not configure members of string family");
            
            if (!_members.ContainsKey(name))
                Throw.ItemNotFoundException(name);

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
            if (Family == DataTypeFamily.String)
                Throw.NotConfigurableException(nameof(Members), nameof(Member),
                    "Can not configure members of string family");
            
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

        public void RenameMember(string oldName, string newName)
        {
            if (Family == DataTypeFamily.String)
                Throw.NotConfigurableException(nameof(Members), nameof(Member),
                    "Can not configure members of string family");
            
            if (!_members.ContainsKey(oldName))
                Throw.ItemNotFoundException(oldName);

            var member = _members[oldName];
            _members.Remove(oldName);

            member.Name = newName;
            _members.Add(member.Name, member);
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

        public static Predefined Bool => Predefined.Bool;
        public static Predefined Sint => Predefined.Sint;
        public static Predefined Int => Predefined.Int;
        public static Predefined Dint => Predefined.Dint;
        public static Predefined Lint => Predefined.Lint;
        public static Predefined Real => Predefined.Real;
        public static Predefined String => Predefined.String;
        public static Predefined Timer => Predefined.Timer;
        public static Predefined Counter => Predefined.Counter;

        private static List<Member> GenerateStringMembers(ushort length = 82)
        {
            return new List<Member>
            {
                new Member(StringMemberNames[0], Dint),
                new Member(StringMemberNames[1], Sint, new Dimensions(82), Radix.Ascii)
            };
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
            var backingMember = new Member($"{memberPrefix}{Name}{memberIndex}", Sint, hidden: true);

            _members.Add(backingMember.Name, backingMember);

            member.Target = backingMember.Name;
        }
    }
}