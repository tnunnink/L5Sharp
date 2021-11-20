using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// An implementation of <see cref="IDataType"/> that represents an immutable built in complex Logix type.  
    /// </summary>
    /// <remarks>
    /// <c>Predefined</c> types are not exported in a L5X file.
    /// To be able to create these types we need to define them within the library.
    /// This interface serves as the base for all <c>Predefined</c> type. 
    /// </remarks>
    public interface IPredefined : IDataType
    {
        /// <summary>
        /// The members of the <c>Predefined</c> type.
        /// </summary>
        IEnumerable<IMember<IDataType>> Members { get; }
    }
}