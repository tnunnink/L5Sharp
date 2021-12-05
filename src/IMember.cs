using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a member of a complex structure such as <see cref="IComplexType"/>.
    /// </summary>
    /// <remarks>
    /// Members make up the structure of a complex data type and tag components.
    /// <see cref="IMember{TDataType}"/> is meant to be a dimensionless member of a given data type (Atomic or Complex).
    /// <see cref="IArrayMember{TDataType}"/> is meant to be a member with single dimensional element.
    /// Use the static factory <see cref="Member"/> class to create instances of IMember and IArrayMember.
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.</typeparam>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public interface IMember<out TDataType> : IPrototype<IMember<TDataType>> 
        where TDataType : IDataType
    {
        /// <summary>
        /// Gets the Name of the Member component.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the Description of the Member component.
        /// </summary>
        string? Description { get; }
        
        /// <summary>
        /// The DataType of the Member Component.
        /// </summary>
        TDataType DataType { get; }

        /// <summary>
        /// Gets the Dimension of the Member component.
        /// </summary>
        Dimensions Dimension { get; }

        /// <summary>
        /// Gets the Radix of the Member component.
        /// </summary>
        Radix Radix { get; }

        /// <summary>
        /// Gets the ExternalAccess of the Member component.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
    }
}