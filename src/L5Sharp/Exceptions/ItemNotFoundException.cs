using System;

namespace L5Sharp.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
        {
        }
        
        public ItemNotFoundException(string message)
            : base(message)
        {
        }
        
        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}