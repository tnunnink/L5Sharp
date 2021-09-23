using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using LogixHelper.Abstractions;
using LogixHelper.Enumerations;

[assembly: InternalsVisibleTo("LogixHelper.Tests")]

namespace LogixHelper.Primitives
{
    public class DataType : IXSerializable
    {
        private readonly Dictionary<string, DataTypeMember> _members = new Dictionary<string, DataTypeMember>();
        internal DataType(string name, string description, DataTypeFamily typeFamily = null, DataTypeClass typeClass = null)
        {
            //todo validate name

            Name = name;
            Family = typeFamily ?? DataTypeFamily.None;
            Class = typeClass ?? DataTypeClass.User;
            Description = description;
        }

        private DataType(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            Family = DataTypeFamily.FromName(element.Attribute(nameof(Family))?.Value);
            Class = DataTypeClass.FromName(element.Attribute(nameof(Class))?.Value);
            Description = element.Element(nameof(Description))?.Value;
        }
        
        public string Name { get; set; }
        public DataTypeFamily Family { get; private set; }
        public DataTypeClass Class { get; private set; }
        public string Description { get; set; }
        public IEnumerable<DataTypeMember> Members => _members.Values;

        
        internal void AddMember(string name, DataType dataType, string description)
        {
            if (_members.ContainsKey(name)) return;
            
            _members.Add(name, new DataTypeMember());
        }

        public XElement Serialize()
        {
            throw new System.NotImplementedException();
        }

        public static DataType Materialize(XElement element)
        {
            return new DataType(element);
        }
    }
}