using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix Component or item of a Logix Controller that is able to be identified by a name.
    /// </summary>
    /// <example>
    /// Types of Components include DataType, Tag, Module, AddOnInstruction, Program, Routine, Task, etc.
    /// </example>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface ILogixComponent
    {
        /// <summary>
        /// The name of the Logix component.
        /// <remarks>
        /// The name servers as a unique identifier for various types of components.
        /// The component name should satisfy Logix naming constraints of alphanumeric and '_' characters,
        /// start with a letter, and be between 1 and 40 characters. See <see cref="ComponentName"/> for more
        /// information.
        /// </remarks>
        /// </summary>
        public ComponentName Name { get; }
        
        /// <summary>
        /// A <see cref="string"/> that describes the Logix component.
        /// </summary>
        public string Description { get; }
    }
}