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
        /// Gets the collection of <see cref="IMember{TDataType}"/> objects for the complex data type.
        /// </summary>
        IEnumerable<IMember<IDataType>> Members { get; }
    }
}