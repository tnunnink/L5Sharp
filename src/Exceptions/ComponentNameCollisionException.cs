using System;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a component with a given name already exists in a collection or context. 
    /// </summary>
    /// <remarks>
    /// Logix identifies various components by name. Therefore the name of a component must be unique.
    /// </remarks>
    public class ComponentNameCollisionException : LogixException
    {
        private const string Reason = "Component names must be unique";

        /// <summary>
        /// Creates a new <see cref="ComponentNameCollisionException"/> with the specified name and component type. 
        /// </summary>
        /// <param name="name">The name of the component that already exists.</param>
        /// <param name="type">The type of the component that the exception is being thrown for.</param>
        public ComponentNameCollisionException(string name, Type type) : base(
            $"The component '{type}' with name '{name}' already exists in the current context. {Reason}")
        {
            Name = name;
        }

        /// <summary>
        /// The name of the component that already exists.
        /// </summary>
        public string Name { get; }
    }
}