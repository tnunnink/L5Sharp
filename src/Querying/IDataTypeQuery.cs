namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="IComponentQuery{TResult}"/> that adds advanced querying for <see cref="IComplexType"/> elements
    /// within the L5X context.  
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