namespace L5Sharp
{
    /// <summary>
    /// Represents an <c>IDataType</c> that has <c>IMember</c> collection.
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
        IMemberCollection<IMember<IDataType>> Members { get; }
    }
}