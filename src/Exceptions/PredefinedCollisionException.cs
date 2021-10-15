using System;

namespace L5Sharp.Exceptions
{
    public class PredefinedCollisionException : Exception
    {
        public PredefinedCollisionException()
        {
        }
        
        public PredefinedCollisionException(string message)
            : base(message)
        {
        }
        
        public PredefinedCollisionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}