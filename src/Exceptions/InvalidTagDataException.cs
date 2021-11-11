using System;

namespace L5Sharp.Exceptions
{
    public class InvalidTagDataException : Exception
    {
        public InvalidTagDataException(IDataType target, IDataType source)
            : base($"Target '{target.Name}' of type {target.GetType()} can not be set to '{source.Name}' of type {source.GetType()}")
        {
        }
    }
}