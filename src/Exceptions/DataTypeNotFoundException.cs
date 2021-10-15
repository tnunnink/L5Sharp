using System;

namespace L5Sharp.Exceptions
{
    public class DataTypeNotFoundException : Exception
    {
        public DataTypeNotFoundException()
        {
        }
        
        public DataTypeNotFoundException(string message)
            : base(message)
        {
        }
        
        public DataTypeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}