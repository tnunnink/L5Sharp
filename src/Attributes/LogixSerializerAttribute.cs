using System;
using L5Sharp.Serialization;

namespace L5Sharp.Attributes
{
    /// <summary>
    /// An custom <see cref="Attribute"/> that indicates the <see cref="ILogixSerializer{T}"/> type for a given type.
    /// </summary>
    public class LogixSerializerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a <see cref="LogixSerializerAttribute"/> with the specified serializer type.
        /// </summary>
        /// <param name="type">The <see cref="ILogixSerializer{T}"/> type.</param>
        public LogixSerializerAttribute(Type type)
        {
            //todo check that type is indeed a ILogixSerializer
            
            Type = type;
        }
        
        /// <summary>
        /// The <see cref="ILogixSerializer{T}"/> type.
        /// </summary>
        public Type Type { get; }
    }
}