using System.Reflection;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a specified component could not be found in the context.
    /// </summary>
    public class ComponentNotFoundException : LogixException
    {
        /// <summary>
        /// Creates a new instance of the exception with the provided name.
        /// </summary>
        /// <param name="componentName">The name of the component that was searched.</param>
        /// <param name="componentType">The type of the component that was searched.</param>
        public ComponentNotFoundException(string componentName, MemberInfo componentType) : 
            base($"The {componentType.Name} component with name '{componentName}' was not found in the current context.")
        {
        }
    }
}