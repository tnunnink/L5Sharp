using System;

namespace L5Sharp.Exceptions
{
    public class NameCollisionException : Exception
    {
        public NameCollisionException()
        {
        }
        
        public NameCollisionException(string message)
            : base(message)
        {
        }
        
        public NameCollisionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}