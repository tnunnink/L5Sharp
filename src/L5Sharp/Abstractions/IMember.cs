using L5Sharp.Enumerations;

namespace L5Sharp.Abstractions
{
    public interface IMember : IComponent
    {
        IDataType DataType { get; }
        ushort Dimension { get; }
        Radix Radix { get; }
        ExternalAccess ExternalAccess { get; }
        string Description { get; }
    }
}