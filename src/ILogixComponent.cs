using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix component or item of a Logix controller that is able to be identified by a name.
    /// </summary>
    /// <example>Examples of a component include DataType, Tags, Modules, AddOnInstructions, Programs,
    /// Routines, Tasks, etc.</example>
    /// <footer>
    /// <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a>
    /// </footer>
    public interface ILogixComponent
    {
        /// <summary>
        /// The <see cref="ComponentName"/> property of the Logix component.
        /// <remarks>
        /// The name servers as a unique identifier for various types of components.
        /// The component name should satisfy Logix naming constraints of alphanumeric and '_' characters,
        /// start with a letter, and be between 1 and 40 characters
        /// </remarks>
        /// </summary>
        public ComponentName Name { get; }
        
        /// <summary>
        /// A <see cref="string"/> that describes the Logix component.
        /// </summary>
        public string Description { get; }
    }
}