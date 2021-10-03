using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class ReadOnlyMember : IMember
    {
        internal ReadOnlyMember(XElement element)
        {
            Name = element.GetName();

            DataType = element.GetDataTypeName() != null
                ? Predefined.ParseType(element.GetDataTypeName())
                : throw new DataTypeNotFoundException();

            Dimension = element.GetDimension();
            Radix = element.GetRadix() ?? Radix.Default(DataType);
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