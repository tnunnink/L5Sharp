using System;
using System.Xml.Linq;
using LogixHelper.Abstractions;
using LogixHelper.Enumerations;
using LogixHelper.Utilities;

namespace LogixHelper.Primitives
{
    public class DataTypeMember : IXSerializable
    {
        private DataTypeMember(string name, DataType dataType)
        {
            Validate.TagName(name);
            Name = name;
            DataType = dataType;
        }

        public DataTypeMember(string name, DataType dataType,
            string description = null, short dimension = 0, ExternalAccess access = null)
            : this(name, dataType)
        {
            Dimension = dimension;
            Description = description ?? string.Empty;
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
        }

        internal DataTypeMember(string name, DataType dataType, string description, short dimension, Radix radix,
            bool hidden, string target, short bitNumber, ExternalAccess externalAccess) : this(name, dataType)
        {
            Description = description;
            Dimension = dimension;
            Radix = radix;
            Hidden = hidden;
            Target = target;
            BitNumber = bitNumber;
            ExternalAccess = externalAccess;
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

        public string Name { get; set; }
        public DataType DataType { get; set; }
        public string Description { get; set; }
        public short Dimension { get; set; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; set; }
        private bool Hidden { get; }
        private string Target { get; }
        private short BitNumber { get; }

        public XElement Serialize()
        {
            throw new System.NotImplementedException();
        }

        public static DataTypeMember Materialize(XElement element)
        {
            return new DataTypeMember(element);
        }
    }
}