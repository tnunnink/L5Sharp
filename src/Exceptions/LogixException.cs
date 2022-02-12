using System;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is the base for all other custom Logix exceptions in the library.
    /// </summary>
    public class LogixException : Exception
    {
        private const string DefaultMessage = "";
        
        /// <summary>
        /// Creates a new <see cref="LogixException"/> with the default message.
        /// </summary>
        public LogixException() : base(DefaultMessage)
        {
        }

        /// <summary>
        /// Creates a new <see cref="LogixException"/> with the provided message.
        /// </summary>
        /// <param name="message">The message indicating the details of the exception being thrown.</param>
        public LogixException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="LogixException"/> with the provided message and inner exception.
        /// </summary>
        /// <param name="message">The message indicating the details of the exception being thrown.</param>
        /// <param name="innerException">The inner exception to wrap.</param>
        public LogixException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}