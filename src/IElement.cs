using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// An element of an array
    /// </summary>
    /// <typeparam name="TDataType"></typeparam>
    public interface IElement<out TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// The index name of the array element.
        /// </summary>
        string Index { get; }
        
        /// <summary>
        /// The <see cref="IDataType"/> of the array element. 
        /// </summary>
        TDataType DataType { get; }
        
        /// <summary>
        /// The <see cref="L5Sharp.Enums.Radix"/> of the array element.
        /// </summary>
        Radix Radix { get; }
        
        /// <summary>
        /// The <see cref="L5Sharp.Enums.ExternalAccess"/> of the array element.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
        
        /// <summary>
        /// A comment is a <see cref="string"/> describing the array element.
        /// </summary>
        string Comment { get; }
    }
}