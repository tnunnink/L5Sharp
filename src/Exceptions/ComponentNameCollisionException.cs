using System;

namespace L5Sharp.Exceptions
{
    public class ComponentNameCollisionException : Exception
    {
        public ComponentNameCollisionException(string name, Type type) 
            : base($"Name {name} already exists for {type}")
        {
        }
    }
}