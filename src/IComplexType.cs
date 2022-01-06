using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// Represents a complex data structure, or an <see cref="IDataType"/> with a collection of <see cref="IMember{TDataType}"/>.
    /// </summary>
    /// <remarks>
    /// This type is the basis for forming complex structure of nested members and data types.
    /// All non atomic types derive from <c>IComplexType</c>. 
    /// </remarks>
    public interface IComplexType : IDataType
    {
        /// <summary>
        /// Gets the collection of <c>IMember</c> objects for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// </remarks>
        IEnumerable<IMember<IDataType>> Members { get; }

        /*/// <summary>
        /// Determines if the current <see cref="IComplexType"/> is equal to another based on the provided comparison parameter.
        /// </summary>
        /// <param name="other">Another <see cref="IComplexType"/> instance to compare.</param>
        /// <param name="comparison">An equality comparison option to determine how to compare the types.</param>
        /// <returns>true if the</returns>
        bool Equals(IComplexType? other, DataTypeComparison comparison);*/
        
        /// <summary>
        /// Gets all nested dependent <c>IDataType</c> objects for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to get a unique set
        /// of dependent data types for the current <c>IComplexType</c>.
        /// </remarks>
        /// <returns>
        /// A collection of unique data types that the current <c>IComplexType</c> depends on, if any exists.
        /// If none exist, an empty collection of <c>IDataType</c>.
        /// </returns>
        IEnumerable<IDataType> GetDependentTypes();
    }
}