using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
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
        Dimensions Dimension { get; }
        
        /// <summary>
        /// This Radix(Style) of the member component.
        /// </summary>
        Radix Radix { get; }
        
        /// <summary>
        /// This External Access of the member component.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
        
        
    }
}