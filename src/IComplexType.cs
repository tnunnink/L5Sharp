using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// Represents a data type that has a collection of members that form a complex structure.
    /// </summary>
    public interface IComplexType : IDataType
    {
        /// <summary>
        /// Gets the Member collection for the current complex data type.
        /// </summary>
        IEnumerable<IMember<IDataType>> Members { get; }
    }
}