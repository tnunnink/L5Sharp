using System;

namespace L5Sharp.Exceptions
{
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException(string message)
            : base(message)
        {
        }
    }
}