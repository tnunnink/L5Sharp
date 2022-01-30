using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a collection of <see cref="IModule"/> objects that are children of a certain parent module, allowing
    /// the <see cref="IModule"/> components to form a hierarchical data structure.
    /// </summary>
    public interface IModuleCollection : IEnumerable<IModule>
    {
        /// <summary>
        /// Gets a <see cref="IModule"/> object at the specified index or slot number of the <see cref="IModuleCollection"/>.
        /// </summary>
        /// <param name="slot">The slot number at which to get the Module instance.</param>
        IModule? this[int slot] { get; }

        /// <summary>
        /// Adds a new <see cref="IModule"/> with the provided name and definition to the <see cref="IModuleCollection"/>.
        /// </summary>
        /// <param name="name">The name of the module to add.</param>
        /// <param name="definition"></param>
        /// <param name="description"></param>
        /// <param name="port"></param>
        void Add(ComponentName name, ModuleDefinition definition, string? description = null, Port? port = null);

        
        bool Remove(ComponentName name);
        
        bool Remove(int slot);
    }
}