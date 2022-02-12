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
        /// Gets the value of the <see cref="DataTypeFamily"/> for the <see cref="IDataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating whether the data type belongs to the string family or has no family.
        /// </value>
        DataTypeFamily Family { get; }
        
        /// <summary>
        /// Gets the value of the <see cref="DataTypeClass"/> for the <see cref="IDataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating the class for which the current data type belongs.
        /// This could be atomic, user, predefined, etc.
        /// </value>
        DataTypeClass Class { get; }
    }
}