using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of extensions for the <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IArrayType{TDataType}"/> using the enumerable collection of data types.
        /// </summary>
        /// <param name="dataTypes">The current collection of data types.</param>
        /// <typeparam name="TDataType">The data type of the array.</typeparam>
        /// <returns>A new <see cref="IArrayType{TDataType}"/> instance initialized with the current data type collection.</returns>
        public static IArrayType<TDataType> ToArrayType<TDataType>(this IEnumerable<TDataType> dataTypes)
            where TDataType : IDataType
        {
            var list = dataTypes.ToList();

            var dimension = list.Count <= ushort.MaxValue
                ? (ushort)list.Count
                : throw new ArgumentOutOfRangeException(nameof(dataTypes),
                    $"The number of types must be less than {ushort.MaxValue} to form a single dimensional array.");

            return new ArrayType<TDataType>(dimension, list);
        }
    }
}