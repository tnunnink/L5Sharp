using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// A member component that is a collection of <see cref="IElement{TDataType}"/>.
    /// </summary>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the collection of elements of the array.</typeparam>
    public interface IArrayMember<out TDataType> : IMember<TDataType>, IEnumerable<IElement<TDataType>> 
        where TDataType : IDataType
    {
        /// <summary>
        /// Gets an <see cref="IElement{TDataType}"/> at the specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="IElement{TDataType}"/>.</param>
        IElement<TDataType> this[int index] { get; }
    }
}