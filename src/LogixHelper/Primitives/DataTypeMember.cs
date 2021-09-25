using System;
using System.Xml.Linq;
using LogixHelper.Abstractions;
using LogixHelper.Enumerations;
using LogixHelper.Utilities;

namespace LogixHelper.Primitives
{
    public class DataTypeMember : IXSerializable
    {
        private DataTypeMember(string name, IDataType dataType, Radix radix, ExternalAccess access,
            short dimension = 0, bool hidden = false, string target = null, short bitNumber = 0,
            string description = null)
        {
            if (name == null)
                throw new ArgumentNullException();
            if (dataType == null)
                throw new ArgumentNullException();
            
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
            Dimension = Convert.ToInt16(element.Attribute(nameof(Dimension))?.Value);
            Radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);
            ExternalAccess = ExternalAccess.FromName(element.Attribute(nameof(ExternalAccess))?.Value);
            Description = element.Element(nameof(Description))?.Value;
            Hidden = Convert.ToBoolean(element.Attribute(nameof(Hidden))?.Value);
            Target = element.Attribute(nameof(Target))?.Value;
            BitNumber = Convert.ToInt16(element.Attribute(nameof(BitNumber))?.Value);
        }

        internal DataTypeMember(string name, IDataType dataType, string description = null, short dimension = 0,
            Radix radix = null, ExternalAccess access = null)
            : this(name, dataType, radix, access, dimension, description: description)
        {
        }

        public string Name { get; internal set; }
        public IDataType DataType { get; internal set; }
        public string Description { get; internal set; }
        public short Dimension { get; internal set; }
        public Radix Radix { get; internal set; }
        public ExternalAccess ExternalAccess { get; internal set; }
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