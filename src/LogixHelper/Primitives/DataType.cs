using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using LogixHelper.Abstractions;
using LogixHelper.Enumerations;
using LogixHelper.Exceptions;
using LogixHelper.Utilities;

[assembly: InternalsVisibleTo("LogixHelper.Tests")]

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
            string description = null, short length = 1)
        {
            if (typeClass == null)
                throw new ArgumentNullException();
            if (family == null)
                throw new ArgumentNullException();
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            //Class needs to be initialized first since the setting of other properties will depend on its value
            Class = typeClass;

            Family = family;
            if (family.Equals(DataTypeFamily.String))
                InitializeStringMembers(length);

            Name = name;
            Description = description ?? string.Empty;
        }

        private DataType(XElement element)
        {
            //Class needs to be initialized first since the setting of other properties will depend on its value
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
                if (IsUserDefined)
                {
                    Validate.TagName(value);
                    Validate.DataTypeName(value);
                }

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
                if (!IsUserDefined) return;
                _description = value;
            }
        }
        public bool IsUserDefined => Class.Equals(DataTypeClass.User);
        public IEnumerable<DataTypeMember> Members => _members.Values;
        public static IEnumerable<IDataType> Predefined => GetPredefined();
        private bool IsConfigurable => Class.Equals(DataTypeClass.User) && Family.Equals(DataTypeFamily.None);

        public void AddMember(string name, IDataType dataType, string description)
        {
            if (!IsConfigurable)
                throw new NotConfigurableException();

            if (_members.ContainsKey(name)) return;

            _members.Add(name, new DataTypeMember(name, dataType, description: description));
        }

        public void RemoveMember(string name)
        {
            if (!IsConfigurable)
                throw new NotConfigurableException();

            if (!_members.ContainsKey(name)) return;

            _members.Remove(name);
        }

        public void UpdateMember(string name, DataTypeMember member)
        {
            if (!IsConfigurable)
                throw new NotConfigurableException();
            
            if (!_members.ContainsKey(name)) return;

            if (member.Name == name)
            {
                _members[name] = member;
                return;
            }

            _members.Remove(name);
            _members.Add(member.Name, member);
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(DataType));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(Family), Family));
            element.Add(new XAttribute(nameof(Class), Class));
            element.Add(new XElement(nameof(Description), new XCData(Description)));
            element.Add(new XElement(nameof(Members), Members.Select(m => m.Serialize())));
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

            return new DataType(name, DataTypeFamily.String, DataTypeClass.User, description, length);
        }

        public static readonly IDataType Bool = new DataType(nameof(Bool).ToUpper(), DataTypeFamily.None,
            DataTypeClass.ProductDefined);

        public static readonly IDataType Sint = new DataType(nameof(Sint).ToUpper(), DataTypeFamily.None,
            DataTypeClass.ProductDefined);

        public static readonly IDataType Int = new DataType(nameof(Int).ToUpper(), DataTypeFamily.None,
            DataTypeClass.ProductDefined);

        public static readonly IDataType Dint = new DataType(nameof(Dint).ToUpper(), DataTypeFamily.None,
            DataTypeClass.ProductDefined);

        public static readonly IDataType Lint = new DataType(nameof(Lint).ToUpper(), DataTypeFamily.None,
            DataTypeClass.ProductDefined);

        public static readonly IDataType Real = new DataType(nameof(Real).ToUpper(), DataTypeFamily.None,
            DataTypeClass.ProductDefined);

        private static IEnumerable<IDataType> GetPredefined()
        {
            return typeof(DataType).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => typeof(IDataType).IsAssignableFrom(p.FieldType))
                .Select(pi => (IDataType)pi.GetValue(null))
                .ToList();
        }

        private void InitializeStringMembers(short length)
        {
            _members.Add(StringMemberNames[0], new DataTypeMember(StringMemberNames[0], Dint));
            _members.Add(StringMemberNames[1], new DataTypeMember(StringMemberNames[1], Sint, length));
        }
    }
}