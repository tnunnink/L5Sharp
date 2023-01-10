using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    public class DataTypeMember
    {
        public string Name { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public Dimensions Dimensions { get; set; } = Dimensions.Empty;
        public Radix Radix { get; set; } = Radix.Decimal;
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.ReadWrite;
    }
}