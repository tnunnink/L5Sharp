using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <c>DataType</c>. This interface is a base for all <c>DataTypes</c>,
    /// including <see cref="IAtomic"/>, <see cref="IUserDefined"/>, <see cref="IAddOnDefined"/>
    /// </summary>
    /// <remarks>
    /// Data types are the fundamental building block for other Logix components, primarily <see cref="ITag{TDataType}"/>.
    /// By treating them not just as a configuration or definition, but as the unit of value, we start to realize that
    /// all <c>Tags</c> are in fact instances of <c>DataTypes</c>. 
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IDataType : ILogixComponent, IInstantiable<IDataType>
    {
        /// <summary>
        /// Gets the <c>Radix</c> value of the <c>DataType</c>.
        /// </summary>
        /// <remarks><c>Radix</c> is only configurable for value types.
        /// Types that contain complex structures or members that are of complex types should always have a value of
        /// <see cref="L5Sharp.Enums.Radix.Null"/>. 
        /// </remarks>
        Radix Radix { get; }
        
        /// <summary>
        /// Gets the <c>Family</c> of the <c>DataType</c> indicating whether the type is a string type or has no family.
        /// </summary>
        DataTypeFamily Family { get; }
        
        /// <summary>
        /// Gets the <c>Class</c> of the <c>DataType</c>.
        /// </summary>
        /// 
        DataTypeClass Class { get; }
        
        /// <summary>
        /// Gets the <c>DataFormat</c> of the <c>DataType</c> indicating how the data for the type is formatted on import/export.
        /// </summary>
        TagDataFormat DataFormat { get; }
    }
}