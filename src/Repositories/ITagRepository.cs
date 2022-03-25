using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A repository that provides additional APIs for querying and manipulating tag components.
    /// </summary>
    public interface ITagRepository : IComponentRepository<ITag<IDataType>>, ITagQuery
    {
    }
}