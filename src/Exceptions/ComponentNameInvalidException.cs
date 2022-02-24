namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a component name does not conform to the Logix naming constraints.
    /// </summary>
    public class ComponentNameInvalidException : LogixException
    {
        private const string ConstraintMessage =
            "Name must contain only alphanumeric or '_', start with a letter or '_', and be less than 40 characters";

        /// <summary>
        /// Creates a new instance of the exception with the provided name.
        /// </summary>
        /// <param name="name"></param>
        public ComponentNameInvalidException(string name) : base($"Name '{name}' is not valid. {ConstraintMessage}")
        {
        }
    }
}