using System;

namespace LogixHelper.Exceptions
{
    public class NotConfigurableException : Exception
    {
        public NotConfigurableException()
        {
        }
        
        public NotConfigurableException(string message)
            : base(message)
        {
        }
        
        public NotConfigurableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}