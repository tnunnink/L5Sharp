using System.Collections.Generic;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A repository that provides additional APIs for querying and manipulating tag components.
    /// </summary>
    public interface ITagRepository : IComponentRepository<ITag<IDataType>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDataType"></typeparam>
        /// <returns></returns>
        public IEnumerable<ITag<TDataType>> WithType<TDataType>() where TDataType : IDataType;
    }
}