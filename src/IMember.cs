using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a member of a complex data type.
    /// </summary>
    /// <remarks>
    /// Members define complex data structures such as <see cref="IUserDefined"/>, <see cref="IPredefined"/>,
    /// <see cref="IAddOnDefined"/>, and <see cref="IModuleDefined"/>.
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.</typeparam>
    /// <footer>
    /// <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a>
    /// </footer>
    public interface IMember<out TDataType> : IPrototype<IMember<TDataType>> 
        where TDataType : IDataType
    {
        /// <summary>
        /// Gets the <c>Name</c> of the <c>Member</c> component.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the <c>Description</c>  of the <c>Member</c> component.
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// The <c>DataType</c> of the <c>Member</c> Component.
        /// </summary>
        TDataType DataType { get; }

        /// <summary>
        /// Gets the <c>Dimension</c> of the <c>Member</c> component.
        /// </summary>
        Dimensions Dimension { get; }

        /// <summary>
        /// Gets the <c>Radix</c> of the <c>Member</c> component.
        /// </summary>
        Radix Radix { get; }

        /// <summary>
        /// Gets the <c>ExternalAccess</c> of the <c>Member</c> component.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
    }
}