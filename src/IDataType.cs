using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <c>DataType</c>. This interface is a base for all <c>DataTypes</c>,
    /// </summary>
    public interface IDataType : ILogixComponent, IInstantiable<IDataType>
    {
        /// <summary>
        /// The <c>Radix</c> value of the <c>DataType</c>.
        /// </summary>
        Radix Radix { get; }
        DataTypeFamily Family { get; }
        DataTypeClass Class { get; }
        TagDataFormat DataFormat { get; }
    }
}