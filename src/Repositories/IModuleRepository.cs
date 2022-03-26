using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleRepository : IComponentRepository<IModule>, IModuleQuery
    {
    }
}