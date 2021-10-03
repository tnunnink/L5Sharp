using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Abstractions
{
    public interface IMember : INamedComponent
    {
        IDataType DataType { get; }
        ushort Dimension { get; }
        Radix Radix { get; }
        ExternalAccess ExternalAccess { get; }
        string Description { get; }
    }
}