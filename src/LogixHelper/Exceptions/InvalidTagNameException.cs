using System;

namespace LogixHelper.Exceptions
{
    public class InvalidTagNameException : Exception
    {
        public InvalidTagNameException()
        {
        }
        
        public InvalidTagNameException(string message)
            : base(message)
        {
        }
        
        public InvalidTagNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}