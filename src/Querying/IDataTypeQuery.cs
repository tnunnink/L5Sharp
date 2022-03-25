namespace L5Sharp.Querying
{
    /// <summary>
    /// A <see cref="IComponentQuery{TResult}"/> that adds <see cref="IComplexType"/> specific queries over the source collection,
    /// enabling more advanced fluent query configuration. 
    /// </summary>
    public interface IDataTypeQuery : IComponentQuery<IComplexType>
    {
        /// <summary>
        /// Filters the collection to only data types that depend on the specified type name.
        /// </summary>
        /// <param name="typeName">The name of the child data type to match.</param>
        /// <returns>A new <see cref="IDataTypeQuery"/> containing the filtered collection.</returns>
        IDataTypeQuery DependingOn(string typeName);
    }
}