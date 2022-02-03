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
        void Add(ComponentName name, ModuleDefinition definition, string? description = null);
    }
}