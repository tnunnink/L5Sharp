using L5Sharp.Enums;

namespace L5Sharp;

/// <summary>
/// Represents a <i>Logix</i> component or element of the L5X that is able to be identified by name.
/// </summary>
/// <remarks>
/// This is the base interface for all L5X components. All components can be identified by a unique string name that
/// is typically subject to the some naming constraints defined by a Rockwell. Logix internally may create
/// components that do not adhere to the naming constraints, which is why the property is a simple string.
/// Names should be unique any attempt to create duplicated names should fail. This is only validated when adding
/// new components through the <see cref="LogixContent"/> API.
/// All components also contain a simple string description. The description does not adhere to any constraints. 
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer> 
public interface ILogixComponent : ILogixSerializable
{
    /// <summary>
    /// The <see cref="Use"/> of the component within the L5X file.
    /// </summary>
    /// <remarks>
    /// Typically used when exporting individual components (DataType, AOI, Module) to indicate whether the component
    /// is the target of the L5X content, or exists solely as a context or dependency of the target component. When
    /// saving a project as an L5X, the top level controller component is the target, and all other components will
    /// not have this property, hence why it is nullable. 
    /// </remarks>
    Use? Use { get; }
    
    /// <summary>
    /// The name of the <i>Logix</i> component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component name.</value>
    /// <remarks>
    /// The name servers as a unique identifier for various types of components.
    /// In most cases, the component name should satisfy Logix naming constraints of alphanumeric and
    /// underscore ('_') characters, start with a letter, and be between 1 and 40 characters.
    /// However, some internally generated components, such as module defined types, do not have the same
    /// restrictions, which is why this is a simple <see cref="string"/>.
    /// </remarks>
    string Name { get; }
        
    /// <summary>
    /// The description of the <i>Logix</i> component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text description.</value>
    string Description { get; }
}