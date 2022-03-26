using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInstructionRepository : IInstructionQuery, IComponentRepository<IAddOnInstruction>
    {
        
    }
}