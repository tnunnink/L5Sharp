using System;

namespace L5Sharp.Exceptions
{
    public class ComponentNameCollisionException : Exception
    {
        public ComponentNameCollisionException()
        {
        }
        
        public ComponentNameCollisionException(string message)
            : base(message)
        {
        }
        
        public ComponentNameCollisionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}