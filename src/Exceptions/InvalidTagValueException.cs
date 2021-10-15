using System;

namespace L5Sharp.Exceptions
{
    public class InvalidTagValueException : Exception
    {
        public InvalidTagValueException(string message)
            : base(message)
        {
        }
    }
}