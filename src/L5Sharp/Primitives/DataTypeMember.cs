using System;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class DataTypeMember : IXSerializable
    {
        private DataTypeMember(string name, IDataType dataType, Radix radix, ExternalAccess access,
            ushort dimension = 0, bool hidden = false, string target = null, ushort bitNumber = 0,
            string description = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            
            Validate.TagName(name);
            
            Name = name;
            DataType = dataType;
            Radix = radix ?? Radix.Default(dataType);
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Dimension = dimension;
            Hidden = hidden;
            Target = target ?? string.Empty;
            BitNumber = bitNumber;
            Description = description ?? string.Empty;
        }

        private DataTypeMember(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            DataType = new DataType(element.Attribute(nameof(DataType))?.Value);
            Dimension = Convert.ToUInt16(element.Attribute(nameof(Dimension))?.Value);
            Radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);
            ExternalAccess = ExternalAccess.FromName(element.Attribute(nameof(ExternalAccess))?.Value);
            Description = element.Element(nameof(Description))?.Value;
            Hidden = Convert.ToBoolean(element.Attribute(nameof(Hidden))?.Value);
            Target = element.Attribute(nameof(Target))?.Value;
            BitNumber = Convert.ToUInt16(element.Attribute(nameof(BitNumber))?.Value);
        }

        internal DataTypeMember(string name, IDataType dataType, string description = null, ushort dimension = 0,
            Radix radix = null, ExternalAccess access = null)
            : this(name, dataType, radix, access, dimension, description: description)
        {
        }

        public string Name { get; internal set; }
        public IDataType DataType { get; internal set; }
        public string Description { get; internal set; }
        public ushort Dimension { get; internal set; }
        public Radix Radix { get; internal set; }
        public ExternalAccess ExternalAccess { get; internal set; }
        private bool Hidden { get; }
        private string Target { get; }
        private ushort BitNumber { get; }

        public XElement Serialize()
        {
            var element = new XElement("Member");
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(DataType), DataType.Name));
            element.Add(new XAttribute(nameof(Dimension), Dimension));
            element.Add(new XAttribute(nameof(Radix), Radix));
            element.Add(new XAttribute(nameof(Hidden), Hidden));
            if (Hidden) element.Add(new XAttribute(nameof(Target), Target));
            if (Hidden) element.Add(new XAttribute(nameof(BitNumber), BitNumber));
            element.Add(new XAttribute(nameof(ExternalAccess), ExternalAccess));

            if (!string.IsNullOrEmpty(Description))
                element.Add(new XElement(nameof(Description), new XCData(Description)));    
            
            return element;
        }

        public static DataTypeMember Materialize(XElement element)
        {
            return new DataTypeMember(element);
        }
    }
}