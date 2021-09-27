using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class DataType : IDataType
    {
        private const string ResourceNamespace = "Resources";
        private const string PredefinedFileName = "Predefined.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(DataType));
        private static readonly XDocument PredefinedData = LoadPredefined();
        private static readonly string[] AtomicNames = { "BOOL", "SINT", "INT", "DINT", "LINT", "REAL" };
        private static readonly string[] StringMemberNames = { "LEN", "DATA" };
        private string _name;
        private string _description;
        private readonly Dictionary<string, Member> _members = new Dictionary<string, Member>();


        private DataType(string name, DataTypeFamily family, DataTypeClass typeClass,
            string description = null, List<Member> members = null)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (family == null) throw new ArgumentNullException(nameof(family));
            if (typeClass == null) throw new ArgumentNullException(nameof(typeClass));

            if (typeClass == DataTypeClass.User)
            {
                Validate.TagName(name);
                Validate.DataTypeName(name);
            }

            _name = name;
            Class = typeClass;
            Family = family;
            _description = description ?? string.Empty;

            if (members == null) return;
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        private DataType(XElement element)
        {
            Class = DataTypeClass.FromName(element.Attribute(nameof(Class))?.Value);
            Family = DataTypeFamily.FromName(element.Attribute(nameof(Family))?.Value);
            _name = element.Attribute(nameof(Name))?.Value;
            Description = element.Element(nameof(Description))?.Value;

            var members = element.Element(nameof(Members))?.Descendants().Select(Member.Materialize);
            if (members == null) return;
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        public DataType(string name, string description = null)
            : this(name, DataTypeFamily.None, DataTypeClass.User, description)
        {
        }


        public string Name
        {
            get => _name;
            set
            {
                if (!Class.Equals(DataTypeClass.User))
                    throw new NotConfigurableException();

                Validate.TagName(value);
                Validate.DataTypeName(value);
                _name = value;
            }
        }

        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic => AtomicNames.Contains(Name);

        public string Description
        {
            get => _description;
            set
            {
                if (!Class.Equals(DataTypeClass.User)) return;
                _description = value;
            }
        }

        public IEnumerable<Member> Members => _members.Values.Where(m => !m.Hidden).AsEnumerable();
        private bool HasConfigurableMembers => Class.Equals(DataTypeClass.User) && Family.Equals(DataTypeFamily.None);


        public Member GetMember(string name)
        {
            return _members.ContainsKey(name) ? _members[name] : null;
        }

        public void AddMember(string name, IDataType dataType, string description = null,
            ushort dimension = 0, Radix radix = null, ExternalAccess access = null)
        {
            if (!HasConfigurableMembers)
                Throw.NotConfigurableException(nameof(Members), nameof(DataType),
                    "Members have a predefined structure that are not configurable");

            if (_members.ContainsKey(name))
                Throw.NameCollisionException(name);

            var member = new Member(name, dataType, dimension, radix, access, description: description);

            if (dataType.Equals(Bool))
                GenerateBitBackingMember(member);

            _members.Add(member.Name, member);
        }

        public void AddMember(string name, IDataType dataType, Action<IMemberBuilder> builder)
        {
            if (!HasConfigurableMembers)
                Throw.NotConfigurableException(nameof(Members), nameof(DataType),
                    "Members have a predefined structure that are not configurable");

            if (_members.ContainsKey(name))
                Throw.NameCollisionException(name);

            var memberBuilder = new MemberBuilder(name, dataType);
            builder(memberBuilder);

            var member = memberBuilder.Build();

            if (dataType.Equals(Bool))
                GenerateBitBackingMember(member);

            _members.Add(member.Name, member);
        }

        public void UpdateMember(string name, Action<IMemberBuilder> builder)
        {
            if (!HasConfigurableMembers)
                Throw.NotConfigurableException(nameof(Members), nameof(DataType),
                    "Members have a predefined structure that are not configurable");

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
            if (!HasConfigurableMembers)
                throw new NotConfigurableException();

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
            if (!HasConfigurableMembers)
                Throw.NotConfigurableException(nameof(Members), nameof(DataType),
                    "Members have a predefined structure that are not configurable");

            if (!_members.ContainsKey(oldName))
                Throw.ItemNotFoundException(oldName);

            var member = _members[oldName];
            _members.Remove(oldName);

            member.Name = newName;
            _members.Add(member.Name, member);
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(DataType));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(Family), Family));
            element.Add(new XAttribute(nameof(Class), Class));
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

            return new DataType(name, DataTypeFamily.String, DataTypeClass.User, description,
                GenerateStringMembers(length));
        }

        public static IDataType FromName(string name)
        {
            var predefined = GetPredefined();

            foreach (var dataType in predefined)
                if (dataType.Name == name)
                    return dataType;

            return new DataType(name);
        }

        public bool SupportsRadix(Radix radix)
        {
            if (!IsAtomic) return radix == Radix.Null;

            if (this == (DataType)Real)
            {
                return radix == Radix.Float || radix == Radix.Exponential;
            }

            if (this == (DataType)Bool)
            {
                return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex;
            }

            if (this == (DataType)Lint)
            {
                return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex
                       || radix == Radix.Ascii || radix == Radix.DateTime || radix == Radix.DateTimeNs;
            }

            return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex ||
                   radix == Radix.Ascii;
        }

        public bool Equals(IDataType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((IDataType)obj);
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

        public static IEnumerable<IDataType> GetAtomic() => GetStaticMembers().Where(x => x.IsAtomic);
        public static IEnumerable<IDataType> GetPredefined() => GetStaticMembers();


        public static readonly IDataType Bool = Load(nameof(Bool).ToUpper());

        public static readonly IDataType Sint = Load(nameof(Sint).ToUpper());

        public static readonly IDataType Int = Load(nameof(Int).ToUpper());

        public static readonly IDataType Dint = Load(nameof(Dint).ToUpper());

        public static readonly IDataType Lint = Load(nameof(Lint).ToUpper());

        public static readonly IDataType Real = Load(nameof(Real).ToUpper());

        public static readonly IDataType String = Load(nameof(String).ToUpper());

        public static readonly IDataType Timer = Load(nameof(Timer).ToUpper());

        public static readonly IDataType Counter = Load(nameof(Counter).ToUpper());

        private static List<Member> GenerateStringMembers(ushort length = 82)
        {
            return new List<Member>
            {
                new Member(StringMemberNames[0], Dint),
                new Member(StringMemberNames[1], Sint, dimension: length, radix: Radix.Ascii)
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

        private static IEnumerable<IDataType> GetStaticMembers()
        {
            return typeof(DataType).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => typeof(IDataType).IsAssignableFrom(p.FieldType))
                .Select(pi => (IDataType)pi.GetValue(null))
                .ToList();
        }

        private static IDataType Load(string name)
        {
            var element = PredefinedData.Descendants(nameof(DataType))
                .SingleOrDefault(x => x.Attribute(nameof(Name))?.Value == name);

            return Materialize(element);
        }

        private static XDocument LoadPredefined()
        {
            using var stream = Resources.GetStream(PredefinedFileName, ResourceNamespace);
            if (stream == null)
                throw new InvalidOperationException(
                    $"Could not load template resource file {PredefinedFileName} from {ResourceNamespace}");

            return XDocument.Load(stream);
        }
    }
}