using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A repository that provides additional APIs for querying and manipulating modules.
    /// </summary>
    public interface IModuleRepository : IComponentRepository<IModule>, IModuleQuery
    {
    }
}