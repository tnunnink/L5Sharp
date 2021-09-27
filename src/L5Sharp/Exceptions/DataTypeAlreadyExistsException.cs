using System;

namespace L5Sharp.Exceptions
{
    public class DataTypeAlreadyExistsException : Exception
    {
        public DataTypeAlreadyExistsException()
        {
        }
        
        public DataTypeAlreadyExistsException(string message)
            : base(message)
        {
        }
        
        public DataTypeAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}