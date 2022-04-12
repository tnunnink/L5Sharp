using L5Sharp.Enums;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IComplexType"/> elements
    /// within the L5X context. 
    /// </summary>
    public interface IDataTypeQuery : ILogixQuery<IComplexType>
    {
        /// <summary>
        /// Filters the collection to only data types that depend on the specified type name.
        /// </summary>
        /// <param name="typeName">The name of the child data type to match.</param>
        /// <returns>A new <see cref="IDataTypeQuery"/> containing the filtered collection.</returns>
        /// <remarks>
        /// This query is similar to <see cref="DataTypeQuery.UsedBy"/>, but instead of getting the data types used by the specified
        /// type, this query gets other types that reference the specified type as a member of it's data type structure,
        /// therefore giving you only types that depend on it.
        /// </remarks>
        DataTypeQuery DependingOn(string typeName);

        /// <summary>
        /// Filters the collection to only data types with the specified <see cref="DataTypeFamily"/> value. 
        /// </summary>
        /// <param name="family">The <see cref="DataTypeFamily"/> value to filter on.</param>
        /// <returns>A new <see cref="IDataTypeQuery"/> containing only data types with the specified family.</returns>
        DataTypeQuery OfFamily(DataTypeFamily family);

        /// <summary>
        /// Filters the collection to only data types that are used by the members of the specified type name. 
        /// </summary>
        /// <param name="typeName">The name of the data type for which to filter member type of.</param>
        /// <returns>A new <see cref="IDataTypeQuery"/> containing the data types that are used by the specified
        /// type name.</returns>
        /// <remarks>
        /// This query is similar to <see cref="DataTypeQuery.DependingOn"/>, but instead of getting other data types that reference
        /// the one specified, this query just gets the data types that the specified type references, therefore, giving
        /// you only the types that are used by it. 
        /// </remarks>
        DataTypeQuery UsedBy(string typeName);
    }
}