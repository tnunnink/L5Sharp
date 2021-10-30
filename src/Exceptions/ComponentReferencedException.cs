using System;

namespace L5Sharp.Exceptions
{
    public class ComponentReferencedException : Exception
    {
        public ComponentReferencedException(string name, Type type)
            : base($"Component {name} is of type {type} is currently referenced")
        {
        }
    }
}