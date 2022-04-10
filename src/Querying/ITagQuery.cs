namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="IComponentQuery{TResult}"/> that adds advanced querying for <see cref="ITag{TDataType}"/>
    /// elements within the L5X context.  
    /// </summary>
    public interface ITagQuery : IComponentQuery<ITag<IDataType>>
    {
        /// <summary>
        /// Filters the collection to include only tags with the specified data type name. 
        /// </summary>
        /// <param name="typeName">The name of the data type to search.</param>
        /// <returns>
        /// A mew <see cref="ITagQuery"/> containing only tags of the specified type.
        /// </returns>
        ITagQuery WithDataType(string typeName);
    }
}