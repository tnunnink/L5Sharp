using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    public interface IProgramRepository : IProgramQuery, IComponentRepository<IProgram>
    {
        
    }
}