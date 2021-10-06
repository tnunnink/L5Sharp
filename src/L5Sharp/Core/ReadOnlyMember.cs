using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    public class ReadOnlyMember : IMember
    {
        internal ReadOnlyMember(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
        {
            Name = name;
            DataType = dataType;
            Dimension = dimension;
            Radix = radix ?? Radix.Decimal;
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
        }

        internal ReadOnlyMember(XElement element)
        {
            Name = element.GetName();

            DataType = element.GetDataTypeName() != null
                ? Predefined.ParseType(element.GetDataTypeName())
                : throw new DataTypeNotFoundException();

            Dimension = element.GetDimension();
            Radix = element.GetRadix() ?? DataType.DefaultRadix;
            ExternalAccess = element.GetExternalAccess() ?? ExternalAccess.ReadWrite;
            Description = element.GetDescription() ?? string.Empty;
        }

        public string Name { get; }
        public IDataType DataType { get; }
        public ushort Dimension { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public string Description { get; }
    }
}