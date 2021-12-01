using System;
using L5Sharp.Enums;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// Represents an exception for assigning data types radix values that they do not support.
    /// </summary>
    public class RadixNotSupportedException : Exception
    {
        /// <summary>
        /// Creates new instance of the exception.
        /// </summary>
        /// <param name="radix">The radix that is not supported.</param>
        /// <param name="dataType">The data type that is not supported by the provided radix.</param>
        public RadixNotSupportedException(Radix radix, ILogixComponent dataType) 
            : base($"Radix {radix.Name} does not support type {dataType.Name}")
        {
        }
    }
}