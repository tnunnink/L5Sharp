using System;

namespace L5Sharp.Exceptions
{
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException(IDataType dataType)
            : base($"Member can not be same type as parent type '{dataType.Name}'")
        {
        }
    }
}