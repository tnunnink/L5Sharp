using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using LogixHelper.Abstractions;
using LogixHelper.Builders;
using LogixHelper.Enumerations;
using LogixHelper.Exceptions;
using LogixHelper.Helpers;
using LogixHelper.Utilities;

namespace LogixHelper.Primitives
{
    public class DataType : IDataType, IXSerializable
    {
        private static readonly string[] AtomicNames = { "BOOL", "SINT", "INT", "DINT", "LINT", "REAL" };
        private static readonly string[] StringMemberNames = { "LEN", "DATA" };
        private string _name;
        private string _description;
        private readonly Dictionary<string, DataTypeMember> _members = new Dictionary<string, DataTypeMember>();

        private DataType(string name, DataTypeFamily family, DataTypeClass typeClass,
            string description = null, short length = 1, List<DataTypeMember> members = null)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (family == null) throw new ArgumentNullException(nameof(family));
            if (typeClass == null) throw new ArgumentNullException(nameof(typeClass));

            if (typeClass == DataTypeClass.User) Validate.TagName(name);

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
            Name = element.Attribute(nameof(Name))?.Value;
            Description = element.Element(nameof(Description))?.Value;
            _members = element.Descendants(nameof(Members)).Select(DataTypeMember.Materialize)
                .ToDictionary(m => m.Name);
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
        public IEnumerable<DataTypeMember> Members => _members.Values.AsEnumerable();
        private bool HasConfigurableMembers => Class.Equals(DataTypeClass.User) && Family.Equals(DataTypeFamily.None);

        public DataTypeMember GetMember(string name)
        {
            return _members.ContainsKey(name) ? _members[name] : null;
        }

        public IMemberBuilder AddMember(string name, IDataType dataType)
        {
            if (!HasConfigurableMembers)
                throw new NotConfigurableException(); //todo throw helper

            if (_members.ContainsKey(name))
                throw new MemberNameCollisionException(); //todo throw helper

            var member = new DataTypeMember(name, dataType);
            _members.Add(member.Name, member);
            return new MemberBuilder(member);
        }

        public void RemoveMember(string name)
        {
            if (!HasConfigurableMembers)
                throw new NotConfigurableException();

            if (!_members.ContainsKey(name)) return;

            _members.Remove(name);
        }

        public IMemberBuilder UpdateMember(string name)
        {
            if (!HasConfigurableMembers)
                throw new NotConfigurableException();

            if (!_members.ContainsKey(name))
                throw new MemberNotFoundException();
            
            return new MemberBuilder(_members[name]);
        }

        public IMemberBuilder RenameMember(string oldName, string newName)
        {
            if (!HasConfigurableMembers)
                throw new NotConfigurableException();

            if (!_members.ContainsKey(oldName))
                throw new MemberNotFoundException();

            var member = _members[oldName];
            _members.Remove(oldName);
            
            member.Name = newName;
            _members.Add(member.Name, member);

            return new MemberBuilder(member);
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

        public static DataType StringType(string name, short length, string description = null)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be greater 0");

            return new DataType(name, DataTypeFamily.String, DataTypeClass.User, description, length,
                GenerateStringMembers(length));
        }

        public static IEnumerable<IDataType> Atomic => GetPredefined().Where(x => x.IsAtomic);
        public static IEnumerable<IDataType> Predefined => GetPredefined();

        public static readonly IDataType Bool = new DataType(nameof(Bool).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined);

        public static readonly IDataType Sint = new DataType(nameof(Sint).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined);

        public static readonly IDataType Int = new DataType(nameof(Int).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined);

        public static readonly IDataType Dint = new DataType(nameof(Dint).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined);

        public static readonly IDataType Lint = new DataType(nameof(Lint).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined);

        public static readonly IDataType Real = new DataType(nameof(Real).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined);

        public static readonly IDataType String = new DataType(nameof(String).ToUpper(), DataTypeFamily.String,
            DataTypeClass.Predefined, members: GenerateStringMembers());
        
        public static readonly IDataType Timer = new DataType(nameof(Timer).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined, members: PredefinedGenerator.TimerMembers());

        public static readonly IDataType Counter = new DataType(nameof(Counter).ToUpper(), DataTypeFamily.None,
            DataTypeClass.Predefined, members: PredefinedGenerator.CounterMembers());

        private static List<DataTypeMember> GenerateStringMembers(short length = 82)
        {
            return new List<DataTypeMember>
            {
                new DataTypeMember(StringMemberNames[0], Dint),
                new DataTypeMember(StringMemberNames[1], Sint, dimension: length, radix: Radix.Ascii)
            };
        }

        private static IEnumerable<IDataType> GetPredefined()
        {
            return typeof(DataType).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => typeof(IDataType).IsAssignableFrom(p.FieldType))
                .Select(pi => (IDataType)pi.GetValue(null))
                .ToList();
        }
    }
}