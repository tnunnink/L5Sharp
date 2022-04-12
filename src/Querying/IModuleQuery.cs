using L5Sharp.Core;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IModule"/>
    /// elements within the L5X context.  
    /// </summary>  
    public interface IModuleQuery : ILogixQuery<IModule>
    {
        /// <summary>
        /// Filters the collection to include only <see cref="IModule"/> objects with the specified parent name.
        /// </summary>
        /// <param name="parentName">The name of the module that is the immediate parent of the modules to filter.</param>
        /// <returns>A new <see cref="IModuleQuery"/> containing ony modules with the specified parent name.</returns>
        IModuleQuery WithParent(ComponentName parentName);
    }
}