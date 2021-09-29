using System;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Member : IMember, IXSerializable
    {
        internal Member(string name, IDataType dataType, Dimensions dimension = null, Radix radix = null,
            ExternalAccess access = null, bool hidden = false, string target = null, ushort bitNumber = 0,
            string description = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            
            Validate.Name(name);
            
            Name = name;
            DataType = dataType;
            
            if (radix != null && !dataType.SupportsRadix(radix))
                Throw.RadixNotSupportedException(radix, dataType);
            Radix = radix ?? Radix.Default(dataType);
            
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Dimension = dimension ?? Dimensions.Empty;
            Hidden = hidden;
            Target = target ?? string.Empty;
            BitNumber = bitNumber;
            Description = description ?? string.Empty;
        }

        private Member(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            DataType = Predefined.ParseType(element.Attribute(nameof(DataType))?.Value);
            Dimension = Dimensions.Parse(element.Attribute(nameof(Dimension))?.Value);
            Radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);
            ExternalAccess = ExternalAccess.FromName(element.Attribute(nameof(ExternalAccess))?.Value);
            Description = element.Element(nameof(Description))?.Value;
            Hidden = Convert.ToBoolean(element.Attribute(nameof(Hidden))?.Value);
            Target = element.Attribute(nameof(Target))?.Value;
            BitNumber = Convert.ToUInt16(element.Attribute(nameof(BitNumber))?.Value);
        }

        public string Name { get; internal set; }
        public IDataType DataType { get; internal set; }
        public Dimensions Dimension { get; internal set; }
        public Radix Radix { get; internal set; }
        public ExternalAccess ExternalAccess { get; internal set; }
        public string Description { get; internal set; }
        internal bool Hidden { get; set; }
        internal string Target { get; set; }
        internal ushort BitNumber { get; set; }

        public XElement Serialize()
        {
            var element = new XElement("Member");
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(DataType), DataType.Name));
            element.Add(new XAttribute(nameof(Dimension), Dimension.ToString()));
            element.Add(new XAttribute(nameof(Radix), Radix));
            element.Add(new XAttribute(nameof(Hidden), Hidden));
            if (!string.IsNullOrEmpty(Target))
                element.Add(new XAttribute(nameof(Target), Target));
            if (!string.IsNullOrEmpty(Target))
                element.Add(new XAttribute(nameof(BitNumber), BitNumber));
            element.Add(new XAttribute(nameof(ExternalAccess), ExternalAccess));

            if (!string.IsNullOrEmpty(Description))
                element.Add(new XElement(nameof(Description), new XCData(Description)));    
            
            return element;
        }

        public static Member Materialize(XElement element)
        {
            return new Member(element);
        }
    }
}