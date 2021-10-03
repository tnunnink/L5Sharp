using System;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Member : IMember 
    {
        private string _name;
        private IDataType _dataType;
        private Radix _radix;

        internal Member(string name, IDataType dataType, ushort dimension, Radix radix,
            ExternalAccess access, string description, bool hidden, string target, ushort bitNumber)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? Predefined.Null;
            Dimension = dimension;
            Radix = radix ?? Radix.Default(dataType);
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
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
    }
}