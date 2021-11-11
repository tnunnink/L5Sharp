using System;

namespace L5Sharp.Exceptions
{
    public class InvalidTagDataException : Exception
    {
        public InvalidTagDataException(ILogixComponent target, ILogixComponent source)
            : base($"Source '{source.Name}' of type '{source.GetType()}' does not match Target {target.Name} of type {target.GetType()}")
        {
        }
    }
}