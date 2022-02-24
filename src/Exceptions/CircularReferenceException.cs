namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when attempting to add a member to a type that is the same type as the parent.
    /// </summary>
    public class CircularReferenceException : LogixException
    {
        /// <summary>
        /// Creates a new <see cref="CircularReferenceException"/> with the provided data type name.
        /// </summary>
        /// <param name="dataTypeName">THe name of the data type circular reference.</param>
        public CircularReferenceException(string dataTypeName)
            : base($"Member can not be same type as parent type '{dataTypeName}'")
        {
        }
    }
}