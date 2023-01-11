using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ILogixMember
    {
        string Name { get; }
        string Description { get; }
        string DataType { get; }
        Dimensions Dimensions { get; }
        Radix Radix { get; }
        ExternalAccess ExternalAccess { get; }
    }
}