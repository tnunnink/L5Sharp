using System;

namespace L5Sharp.Exceptions
{
    public class ComponentNameInvalidException : Exception
    {
        public ComponentNameInvalidException(string tagName) : base(
            $"Name {tagName} is not valid. Must contain alphanumeric or '_', start with a letter, and be less than 32 characters")
        {
        }
    }
}