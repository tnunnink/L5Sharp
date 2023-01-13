using System.Xml.Serialization;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix component or element of the L5X or Logix context that is able to be identified by name.
    /// </summary>
    /// <remarks>
    /// This interface is the base for all L5X components. All components can be identified by a string name that is
    /// subject to the Logix naming constraints defined by a <see cref="ComponentName"/>. Logix internally may create
    /// components that do not adhere to the naming constraints, which is why the property is a simple string.
    /// Names should be unique any attempt to create duplicated names should fail.
    /// Components also contain a simple string description. The description does not adhere to any constraints. 
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface ILogixComponent : ILogixSerializable, ILogixDeserializable
    {
        /// <summary>
        /// The name of the component.
        /// </summary>
        /// <value>A <see cref="string"/> representing the component name.</value>
        /// <remarks>
        /// The name servers as a unique identifier for various types of components.
        /// The component name should satisfy Logix naming constraints of alphanumeric and '_' characters,
        /// start with a letter, and be between 1 and 40 characters. See <see cref="ComponentName"/> for more
        /// information.
        /// </remarks>
        string Name { get; }
        
        /// <summary>
        /// The description of the component.
        /// </summary>
        /// <value>A <see cref="string"/> representing the component description.</value>
        string Description { get; }
    }
}