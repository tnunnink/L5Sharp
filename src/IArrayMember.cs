using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// A <c>ArrayMember</c> is a member that represents a collection of <see cref="IMember{TDataType}"/>.
    /// </summary>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the collection of elements of the array.</typeparam>
    public interface IArrayMember<out TDataType> : IMember<TDataType>, IEnumerable<IMember<TDataType>> 
        where TDataType : IDataType
    {
        /// <summary>
        /// Gets an <c>Element</c> at the specified index.
        /// </summary>
        /// <param name="index">The index of the <c>Element</c> to retrieve.</param>
        /// <returns>An instance of <see cref="IMember{TDataType}"/> if the index is in range of the array.</returns>
        IMember<TDataType> this[int index] { get; }
    }
}