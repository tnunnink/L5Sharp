using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface IMember : IComponent
    {
        /// <summary>
        /// The DataType of the member component.
        /// </summary>
        IDataType DataType { get; }
        
        /// <summary>
        /// The Dimensions of the member component
        /// </summary>
        ushort Dimension { get; }
        
        /// <summary>
        /// This Radix(Style) of the member component.
        /// </summary>
        Radix Radix { get; }
        
        /// <summary>
        /// This External Access of the member component.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
    }

    public interface IMem<out TDataType> : IComponent where TDataType : IDataType
    {
        public TDataType DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
    }
}