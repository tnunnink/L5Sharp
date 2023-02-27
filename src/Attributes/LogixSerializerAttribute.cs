using System;
using System.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Attributes
{
    /// <summary>
    /// An custom <see cref="Attribute"/> that allows the user to specify a type implementing
    /// <see cref="ILogixSerializer{T}"/> that is to be used for L5X serialization/deserialization.
    /// </summary>
    public class LogixSerializerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a <see cref="LogixSerializerAttribute"/> with the specified serializer type.
        /// </summary>
        /// <param name="type">The <see cref="ILogixSerializer{T}"/> type.</param>
        public LogixSerializerAttribute(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (!typeof(ILogixSerializer<>).IsAssignableFrom(type))
                throw new ArgumentException(
                    $"The specified type {type} does not implement {typeof(ILogixSerializer<>)}");

            if (type.GetConstructor(Type.EmptyTypes) is null)
                throw new InvalidOperationException(
                    $"The specified serializer {type} does not have a parameterless constructor.");

            Type = type;
        }

        /// <summary>
        /// The <see cref="ILogixSerializer{T}"/> type.
        /// </summary>
        public Type Type { get; }
    }
}