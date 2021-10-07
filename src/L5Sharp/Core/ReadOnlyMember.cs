using System;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class ReadOnlyMember : IMember
    {
        public ReadOnlyMember(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "Name can not be null");
            Validate.Name(name);
            
            Name = name;
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "DataType can not be null");
            Dimension = dimension;
            Radix = radix ?? dataType.DefaultRadix;
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
        }

        public string Name { get; }
        public IDataType DataType { get; }
        public ushort Dimension { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public string Description { get; }
    }
}