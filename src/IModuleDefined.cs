using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// A Logix Component that represents a <c>ModuleDefined</c> data type. 
    /// </summary>
    /// <remarks>
    /// Module defined data types are those that are defined by modules in the IO tree of a <c>Controller</c>.
    /// These types are not directly exportable in an L5X, except for the fact that the structure can be inferred from
    /// the <c>Module</c> component config, input, and output tags.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IModuleDefined : IDataType
    {
        /// <summary>
        /// Gets the name of the <c>ModuleDefined</c> type.
        /// </summary>
        /// <remarks>
        /// Since a module defined type name does not appear to be subject to Logix name constraints,
        /// it must be of type <see cref="string"/>.
        /// </remarks>
        new string Name { get; } 
        
        /// <summary>
        /// Gets the member collection of the <c>ModuleDefined</c> type.
        /// </summary>
        IEnumerable<IMember<IDataType>> Members { get; }
    }
}