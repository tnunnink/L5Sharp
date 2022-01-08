using System;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a component with a given name already exists in a collection or context. 
    /// </summary>
    /// <remarks>
    /// Logix identifies various components by name. Therefore the name of a component must be unique.
    /// </remarks>
    public class ComponentNameCollisionException : Exception
    {
        private const string Reason = "Component names must be unique";

        /// <summary>
        /// A default constructor that throws the exception with the default message. Use this if the name and type
        /// are not known.
        /// </summary>
        public ComponentNameCollisionException() : base(Reason)
        {
        }

        /// <summary>
        /// Creates a new instance of the exception with the provided name and type parameter.
        /// </summary>
        /// <param name="name">The name of the component that already exists.</param>
        /// <param name="type">The type of the component that the exception is being thrown for.</param>
        public ComponentNameCollisionException(string name, Type type) : base(
            $"The component '{type}' with name '{name}' already exists in the current context. {Reason}")
        {
        }
    }
}