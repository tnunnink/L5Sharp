using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using LogixHelper.Abstractions;
using LogixHelper.Enumerations;
using LogixHelper.Utilities;

[assembly: InternalsVisibleTo("LogixHelper.Tests")]

namespace LogixHelper.Primitives
{
    public class DataType : IXSerializable
    {
        private static readonly string[] AtomicNames = { "BOOL", "SINT", "INT", "DINT", "LINT", "REAL" };
        private static readonly string[] StringMemberNames = { "LEN", "DATA" };
        private string _name;
        private string _description;
        private readonly Dictionary<string, DataTypeMember> _members = new Dictionary<string, DataTypeMember>();

        internal DataType(string name, DataTypeFamily family, DataTypeClass typeClass,
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
            //todo members
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
        public IReadOnlyCollection<DataTypeMember> Members => _members.Values;
        public static IReadOnlyCollection<DataType> Predefined => GetPredefined();
        public Radix DefaultRadix => Equals(Real) ? Radix.Float : Radix.Decimal;

        public void AddMember(string name, DataType dataType, string description)
        {
            if (!IsUserDefined || !Family.Equals(DataTypeFamily.None))
                throw new Exception();

            if (_members.ContainsKey(name)) return;
            
            _members.Add(name, new DataTypeMember(name, dataType, description: description));
        }

        public void RemoveMember(string name)
        {
            if (!IsUserDefined || !Family.Equals(DataTypeFamily.None))
                throw new Exception();
            
            if (!_members.ContainsKey(name)) return;
            
            _members.Remove(name);
        }

        public void UpdateMember(string name, DataTypeMember member)
        {
            
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(DataType));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(Family), Family));
            element.Add(new XAttribute(nameof(Class), Class));
            element.Add(new XElement(nameof(Description), new XCData(Description)));

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

        public static readonly PredefinedType Bool =
            new PredefinedType(new DataType("BOOL", DataTypeFamily.None, DataTypeClass.ProductDefined));
        public static readonly DataType Sint = new DataType("SINT", DataTypeFamily.None, DataTypeClass.ProductDefined);
        public static readonly DataType Int = new DataType("INT", DataTypeFamily.None, DataTypeClass.ProductDefined);
        public static readonly DataType Dint = new DataType("DINT", DataTypeFamily.None, DataTypeClass.ProductDefined);
        public static readonly DataType Lint = new DataType("LINT", DataTypeFamily.None, DataTypeClass.ProductDefined);
        public static readonly DataType Real = new DataType("REAL", DataTypeFamily.None, DataTypeClass.ProductDefined);
        
        private static IReadOnlyCollection<DataType> GetPredefined()
        {
            var type = typeof(DataType);
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => type.IsAssignableFrom(p.FieldType))
                .Select(pi => (DataType)pi.GetValue(null))
                .ToList();
        }

        private void InitializeStringMembers(short length)
        {
            _members.Add(StringMemberNames[0], new DataTypeMember(StringMemberNames[0], Dint));
            _members.Add(StringMemberNames[1], new DataTypeMember(StringMemberNames[1], Sint, dimension:length));
        }
    }
}