using System;

namespace L5Sharp.Exceptions
{
    public class InvalidTagValueException : Exception
    {
        public InvalidTagValueException(object value, Type dataType)
            : base($"Value '{value}' is not valid for type '{dataType}'")
        {
        }
    }
}