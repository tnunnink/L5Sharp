using System;
using L5Sharp.Enums;

namespace L5Sharp.Exceptions
{
    public class RadixNotSupportedException : Exception
    {
        public RadixNotSupportedException(Radix radix, ILogixComponent dataType) 
            : base($"Radix {radix.Name} not supported by type {dataType.Name}")
        {
        }
    }
}