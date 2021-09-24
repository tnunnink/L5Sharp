using System;
using System.Xml.Linq;
using LogixHelper.Abstractions;
using LogixHelper.Enumerations;
using LogixHelper.Utilities;

namespace LogixHelper.Primitives
{
    public class DataTypeMember : IXSerializable
    {
        private string _name;

        internal DataTypeMember(string name, DataType dataType, Radix radix, ExternalAccess access,
            short dimension = 0, bool hidden = false, string target = null, short bitNumber = 0,
            string description = null)
        {
            Name = name;
            DataType = dataType;
            Radix = radix ?? dataType.DefaultRadix;
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Dimension = dimension;
            Hidden = hidden;
            Target = target;
            BitNumber = bitNumber;
            Description = description ?? string.Empty;
        }
        
        private DataTypeMember(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            DataType = new DataType(element.Attribute(nameof(DataType))?.Value);
            Dimension = Convert.ToInt16(element.Attribute(nameof(Dimension))?.Value);
            Radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);
            ExternalAccess = ExternalAccess.FromName(element.Attribute(nameof(ExternalAccess))?.Value);
            Description = element.Element(nameof(Description))?.Value;
            Hidden = Convert.ToBoolean(element.Attribute(nameof(Hidden))?.Value);
            Target = element.Attribute(nameof(Target))?.Value;
            BitNumber = Convert.ToInt16(element.Attribute(nameof(BitNumber))?.Value);
        }
        
        public DataTypeMember(string name, DataType dataType, short dimension = 0, string description = null, 
            Radix radix = null, ExternalAccess access = null) 
            : this(name, dataType, radix, access, dimension, description: description)
        {
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.TagName(value);
                _name = value;
            }
        }
        public DataType DataType { get; set; }
        public string Description { get; set; }
        public short Dimension { get; set; }
        public Radix Radix { get; set; }
        public ExternalAccess ExternalAccess { get; set; }
        private bool Hidden { get; }
        private string Target { get; }
        private short BitNumber { get; }

        public XElement Serialize()
        {
            var element = new XElement(nameof(DataTypeMember));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(DataType), DataType));
            element.Add(new XAttribute(nameof(Dimension), Dimension));
            element.Add(new XAttribute(nameof(Radix), Radix));
            element.Add(new XAttribute(nameof(ExternalAccess), ExternalAccess));
            element.Add(new XAttribute(nameof(Hidden), Hidden));
            element.Add(new XAttribute(nameof(Target), Target));
            element.Add(new XAttribute(nameof(BitNumber), BitNumber));
            element.Add(new XElement(nameof(Description), new XCData(Description)));

            return element;
        }

        public static DataTypeMember Materialize(XElement element)
        {
            return new DataTypeMember(element);
        }
    }
}