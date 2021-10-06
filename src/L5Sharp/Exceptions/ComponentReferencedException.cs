using System;

namespace L5Sharp.Exceptions
{
    public class ComponentReferencedException : Exception
    {
        public ComponentReferencedException()
        {
        }
        
        public ComponentReferencedException(string message)
            : base(message)
        {
        }
        
        public ComponentReferencedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}