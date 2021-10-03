using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Member : IMember 
    {
        private string _name;
        private IDataType _dataType;
        private Radix _radix;
        private ExternalAccess _externalAccess;
        private string _description;

        internal Member(string name, IDataType dataType, ushort dimension, Radix radix,
            ExternalAccess access, string description, bool hidden, string target, ushort bitNumber)
        {
            Name = name;
            DataType = dataType;
            Dimension = dimension;
            Radix = radix ?? Radix.Default(dataType);
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Description = description;
            Hidden = hidden;
            Target = target ?? string.Empty;
            BitNumber = bitNumber;
        }
        
        public Member(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess access = null, string description = null) 
            : this(name, dataType, dimension, radix, access, description, false, string.Empty, 0)
        {
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

        public IDataType DataType
        {
            get => _dataType;
            set => _dataType = value ?? Predefined.Null;
        }

        public ushort Dimension { get; set; }

        public Radix Radix
        {
            get => _radix;
            set
            {
                Validate.Radix(value, _dataType);
                _radix = value;
            }
        }

        public ExternalAccess ExternalAccess
        {
            get => _externalAccess;
            set
            {
                Validate.ExternalAccess(value);
                _externalAccess = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value ?? string.Empty;
            }
        }
        internal bool Hidden { get; set; }
        internal string Target { get; set; }
        internal ushort BitNumber { get; set; }
    }
}