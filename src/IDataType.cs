using System;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <c>DataType</c>. This interface is a base for all <c>DataTypes</c>,
    /// including <see cref="IAtomic"/>
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
        /// Gets the <c>Family</c> of the <c>DataType</c> indicating whether the type is a string type or has no family.
        /// </summary>
        DataTypeFamily Family { get; }
        
        /// <summary>
        /// Gets the <c>Class</c> of the <c>DataType</c>.
        /// </summary>
        DataTypeClass Class { get; }
    }
}