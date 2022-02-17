using System;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when attempting to delete a component that is being referenced elsewhere within a
    /// Logix context...
    /// </summary>
    public class ComponentReferencedException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="ComponentReferencedException"/> with the provided name and type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ComponentReferencedException(string name, Type type)
            : base($"Component {name} is of type {type} is currently referenced")
        {
        }
    }
}