using System.Reflection;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when attempting to delete a component that is being referenced elsewhere within a
    /// Logix context...
    /// </summary>
    public class ComponentReferencedException : LogixException
    {
        private const string Reason = "Can not remove referenced components.";
        
        /// <summary>
        /// Creates a new <see cref="ComponentReferencedException"/> with the provided name and type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ComponentReferencedException(string name, MemberInfo type)
            : base($"The {type.Name} component with name '{name}' is currently referenced in the context. {Reason}")
        {
        }
    }
}