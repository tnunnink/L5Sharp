using System;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when attempting to add a member to a type that is the same type as the parent.
    /// </summary>
    public class CircularReferenceException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="CircularReferenceException"/> with the provided data type.
        /// </summary>
        /// <param name="dataType"></param>
        public CircularReferenceException(IDataType dataType)
            : base($"Member can not be same type as parent type '{dataType.Name}'")
        {
        }
    }
}