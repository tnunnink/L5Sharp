using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IDataType : ILogixComponent
    {
        Radix Radix { get; }
        DataTypeFamily Family { get; }
        DataTypeClass Class { get; }
        TagDataFormat DataFormat { get; }
    }
}