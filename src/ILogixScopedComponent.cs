using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// A type of <see cref="ILogixComponent"/> that adds property to indicate the scope of the component.
    /// </summary>
    public interface ILogixScopedComponent
    {
        /// <summary>
        /// The name of the component.
        /// </summary>
        /// <value>A <see cref="string"/> representing the component name.</value>
        /// <remarks>
        /// The name servers as a unique identifier for various types of components.
        /// The component name should satisfy Logix naming constraints of alphanumeric and '_' characters,
        /// start with a letter, and be between 1 and 40 characters.
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// The description of the component.
        /// </summary>
        /// <value>A <see cref="string"/> representing the component description.</value>
        string Description { get; }
        
        /// <summary>
        /// The scope of the component (Controller, Program, Routine).
        /// </summary>
        /// <value>A <see cref="Enums.Scope"/> enum representing the component scope.</value>
        Scope Scope { get; }
    }
}