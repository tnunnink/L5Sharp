using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix DataType component. This interface is a base for all data type objects,
    /// including <see cref="IAtomicType"/> and <see cref="IComplexType"/>.
    /// </summary>
    /// <remarks>
    /// Data types are the fundamental building block for other Logix components, primarily <see cref="ITag{TDataType}"/>.
    /// 
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IDataType : ILogixComponent, IInstantiable<IDataType>
    {
        /// <summary>
        /// Gets the <c>DataTypeFamily</c> of the current <c>IDataType</c> instance.
        /// </summary>
        /// <value>
        /// Represents 
        /// </value>
        DataTypeFamily Family { get; }
        
        /// <summary>
        /// Gets the <c>DataTypeClass</c> of the current <c>IDataType</c> instance.
        /// </summary>
        /// <value>
        /// The enumeration option for the current <c>IDataType</c>
        /// </value>
        DataTypeClass Class { get; }
    }
}