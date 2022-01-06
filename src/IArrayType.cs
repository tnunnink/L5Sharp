using System;
using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// A <see cref="IDataType"/> that represents an array or enumeration of <see cref="IMember{TDataType}"/> instances.
    /// </summary>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> that the array represents.</typeparam>
    public interface IArrayType<out TDataType> : IDataType, IEnumerable<IMember<TDataType>> where TDataType : IDataType 
    {
        /// <summary>
        /// Gets the <see cref="Dimensions"/> of the <see cref="IArrayType{TDataType}"/>.
        /// </summary>
        /// <value>
        /// A <see cref="Core.Dimensions"/> instance that defines the elements of the <see cref="IArrayType{TDataType}"/>.
        /// </value>
        Dimensions Dimensions { get; }
        
        /// <summary>
        /// Gets the <see cref="IMember{TDataType}"/> at the specified one dimensional index.
        /// </summary>
        /// <param name="x">The index of the one dimensional array.</param>
        /// <returns>
        /// An <see cref="IMember{TDataType}"/> element of the array if the index within range.
        /// </returns>
        /// <exception cref="ArgumentException">X does not form a valid index of the current array dimensions.</exception>
        /// <exception cref="InvalidOperationException"><see cref="IArrayType{TDataType}"/> is not one dimensional.</exception>
        /// <remarks>
        /// This overload is for one dimensional arrays only. You can access the element by providing the X index.
        /// If the <see cref="IArrayType{TDataType}"/> is not one dimensional, this call will fail.  
        /// </remarks>
        IMember<TDataType> this[int x] { get; }
        
        /// <summary>
        /// Gets the <see cref="IMember{TDataType}"/> at the specified two dimensional index.
        /// </summary>
        /// <param name="x">The X index of the two dimensional array.</param>
        /// <param name="y">The Y index of the two dimensional array.</param>
        /// <returns>
        /// An <see cref="IMember{TDataType}"/> element of the array if the index within range.
        /// </returns>
        /// <exception cref="ArgumentException">X and Y do not form a valid index of the current array dimensions.</exception>
        /// <exception cref="InvalidOperationException"><see cref="IArrayType{TDataType}"/> is not two dimensional.</exception>
        /// <remarks>
        /// This overload is for two dimensional arrays only. You can access the element by providing the X and Y index.
        /// If the <see cref="IArrayType{TDataType}"/> is not two dimensional, this call will fail.  
        /// </remarks>
        IMember<TDataType> this[int x, int y] { get; }

        /// <summary>
        /// Gets the <see cref="IMember{TDataType}"/> at the specified three dimensional index.
        /// </summary>
        /// <param name="x">The X index of the three dimensional array.</param>
        /// <param name="y">The Y index of the three dimensional array.</param>
        /// <param name="z">The Z index of the three dimensional array.</param>
        /// <returns>
        /// An <see cref="IMember{TDataType}"/> element of the array if the index within range.
        /// </returns>
        /// <exception cref="ArgumentException">X, Y, and Z do not form a valid index of the current array dimensions.</exception>
        /// <exception cref="InvalidOperationException"><see cref="IArrayType{TDataType}"/> is not three dimensional.</exception>
        /// <remarks>
        /// This overload is for three dimensional arrays only. You can access the element by providing the X, Y, and Z index.
        /// If the <see cref="IArrayType{TDataType}"/> is not three dimensional, this call will fail.  
        /// </remarks>
        IMember<TDataType> this[int x, int y, int z] { get; }
    }
}