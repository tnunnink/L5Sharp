using System;

namespace LogixHelper.Exceptions
{
    public class RadixNotSupportedException : Exception
    {
        public RadixNotSupportedException()
        {
        }
        
        public RadixNotSupportedException(string message)
            : base(message)
        {
        }
        
        public RadixNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}