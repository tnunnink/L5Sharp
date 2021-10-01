using System;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Member : IMember, IXSerializable
    {
        private string _name;
        private Dimensions _dimension;
        private Radix _radix;

        public Member(string name, IDataType dataType, Dimensions dimension = null, Radix radix = null,
            ExternalAccess access = null, string description = null, bool hidden = false, string target = null,
            ushort bitNumber = 0)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Dimension = dimension ?? Dimensions.Empty;
            Radix = radix ?? Radix.Default(dataType);
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
            Hidden = hidden;
            Target = target ?? string.Empty;
            BitNumber = bitNumber;
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                _name = value;
            }
        }
        public IDataType DataType { get; set; }

        public Dimensions Dimension
        {
            get => _dimension;
            set
            {
                if (value.Y > 0)
                    throw new InvalidOperationException(); //todo throw custom
                _dimension = value;
            }
        }

        public Radix Radix
        {
            get => _radix;
            set
            {
                if (!DataType.IsAtomic) return;
                
                if (!DataType.SupportsRadix(value))
                    Throw.RadixNotSupportedException(value, DataType);
                
                _radix = value;
            }
        }

        public ExternalAccess ExternalAccess { get; set; }
        public string Description { get; set; }
        internal bool Hidden { get; set; }
        internal string Target { get; set; }
        internal ushort BitNumber { get; set; }

        public XElement Serialize()
        {
            var element = new XElement(nameof(Member));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(DataType), DataType.Name));
            element.Add(new XAttribute(nameof(Dimension), Dimension.X.ToString()));
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
    }
}