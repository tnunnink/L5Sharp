namespace L5Sharp;

/// <summary>
/// Represents a <i>Logix</i> component or element of the L5X that is able to be identified by name.
/// </summary>
/// <remarks>
/// This is the base interface for all L5X components. All components can be identified by a unique string name that
/// is typically subject to the some naming constraints defined by a Rockwell. Logix internally may create
/// components that do not adhere to the naming constraints, which is why the property is a simple string.
/// Names should be unique any attempt to create duplicated names should fail. This is only validated when adding
/// new components through the <see cref="ILogixContent"/> API.
/// All components also contain a simple string description. The description does not adhere to any constraints. 
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer> 
public interface ILogixComponent
{
    /// <summary>
    /// The name of the <i>Logix</i> component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component name.</value>
    /// <remarks>
    /// The name servers as a unique identifier for various types of components.
    /// In most cases, the component name should satisfy Logix naming constraints of alphanumeric and
    /// underscore ('_') characters, start with a letter, and be between 1 and 40 characters.
    /// However, some internally generated components, such as module defined types, do not have the same
    /// restrictions, which is why this is a simple <see cref="string"/>. The name of a component will only
    /// be validated when attempting to add it to a <see cref="ILogixComponentCollection{TComponent}"/>.
    /// </remarks>
    string Name { get; }
        
    /// <summary>
    /// The description of the <i>Logix</i> component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text description.</value>
    string Description { get; }
}