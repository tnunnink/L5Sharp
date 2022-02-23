using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <summary>
    /// A repository that provides additional APIs for querying and manipulating modules.
    /// </summary>
    public interface IModuleRepository : IRepository<IModule>
    {
        /// <summary>
        /// Searches the context for a <see cref="IModule"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name of the module to search.</param>
        /// <returns>If found, a <see cref="IModule"/> instance, including children modules, with the specified name.
        /// Otherwise, null.</returns>
        /// <remarks>
        /// This is analogous to <see cref="IRepository{TComponent}.Find(ComponentName)"/> in that it searches the context
        /// for the specified name and returns null if not found. This method, however, will return all descendant modules
        /// of the found module.
        /// </remarks>
        IModule? DeepFind(ComponentName name);

        /// <summary>
        /// Gets a <see cref="IModule"/> instance with the specified name,  including all child <see cref="IModule"/> objects. 
        /// </summary>
        /// <param name="name">The name of the root module to get.</param>
        /// <returns>A new <see cref="IModule"/> instance with the specified name, including all child modules
        /// found in the current <see cref="ILogixContent"/>.</returns>
        /// <exception cref="ComponentNotFoundException">A module with the provided name was not found.</exception>
        /// <remarks>
        /// This is analogous to <see cref="IRepository{TComponent}.Get(ComponentName)"/> in that it searches the context
        /// for the specified name. This method, however, will return all descendant modules of the found module.
        /// </remarks>
        IModule DeepGet(ComponentName name);

        /// <summary>
        /// Gets the root <see cref="IModule"/> instance with all descendant module children, forming the entire module
        /// tree hierarchy.
        /// </summary>
        /// <returns>
        /// The 'Local' or root <see cref="IModule"/> of the module tree hierarchy, including all descendant modules.
        /// </returns>
        IModule Tree();
    }
}