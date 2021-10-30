using System;

namespace L5Sharp.Exceptions
{
    public class ComponentNotConfigurableException : Exception
    {
        public ComponentNotConfigurableException(string propertyName, Type type, string reason)
            : base($"The property {propertyName} is not not configurable for this instance of {type}. {reason}")
        {
        }
    }
}